using Microsoft.AspNetCore.Mvc;
using MySecureBackend.WebApi.Models;
using MySecureBackend.WebApi.Repositories;
using MySecureBackend.WebApi.Services;

namespace MySecureBackend.WebApi.Controllers;

    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class Environment2dController : ControllerBase
    {
        private readonly IEnvironmentRepository _Environment2dRepository;
        private readonly IObjectRepository _ObjectRepository;
        private readonly IAuthenticationService _envAuthenticationService;

        public Environment2dController(IEnvironmentRepository EnvironmentRepository, IObjectRepository ObjectRepository, IAuthenticationService envAuthenticationService)
        {
            _Environment2dRepository = EnvironmentRepository;
            _ObjectRepository = ObjectRepository;
            _envAuthenticationService = envAuthenticationService;
        }

        [HttpGet(Name = "GetEnvironment2Ds")]
        public async Task<ActionResult<List<EnvironmentObject>>> GetAsync()
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized("Niet geautoriseerd");

            var enviroment2D = await _Environment2dRepository.SelectUserAsync(userIdString);
            return Ok(enviroment2D);
        }

        [HttpGet("{environmentObjectId}", Name = "GetEnvironmentObjectById")]
        public async Task<ActionResult<EnvironmentObject>> GetByIdAsync(Guid environmentObjectId)
        {
            var enviroment2D = await _Environment2dRepository.SelectAsync(environmentObjectId);

            if (enviroment2D == null)
                return NotFound(new ProblemDetails { Detail = $"Environment {environmentObjectId} not found." });

            return Ok(enviroment2D);
        }

        [HttpPost(Name = "AddEvironmentObject")]
        public async Task<ActionResult<EnvironmentObject>> AddAsync(EnvironmentObject environmentObject)
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized("Niet geautoriseerd!");

            var activeEnvironments = await _Environment2dRepository.SelectUserAsync(userIdString);
            if (activeEnvironments.Count() >= 5)
                return BadRequest(new ProblemDetails { Detail = "Maximum werelden van 5 is behaald!" });

            if (activeEnvironments.Any(env => env.EnvName == environmentObject.EnvName))
                return BadRequest(new ProblemDetails { Detail = "Je kan niet 2 werelden met dezelfde naam maken!" });

            environmentObject.EnvGuid = Guid.NewGuid();
            environmentObject.UserId = userIdString;

            await _Environment2dRepository.InsertAsync(environmentObject);

            return CreatedAtRoute("GetEnviromentObject", new { enviroment2dId = environmentObject.EnvGuid }, environmentObject);
    }

        [HttpPut("{environmentObjectId}", Name = "UpdateEnvironmentObject")]
        public async Task<ActionResult<EnvironmentObject>> UpdateAsync(Guid environmentObjectId, EnvironmentObject environmentObject)
        {
            var enviroment = await _Environment2dRepository.SelectAsync(environmentObjectId);

            if (enviroment == null)
                return NotFound(new ProblemDetails { Detail = $"Environment {environmentObjectId} not found." });

            if (environmentObject.EnvGuid != environmentObjectId)
                return Conflict(new ProblemDetails { Detail = "De id van de environment is niet gelijk aan de huidige id." });

            await _Environment2dRepository.UpdateAsync(environmentObject);

            return Ok(environmentObject);
        }

        [HttpDelete("{environmentObjectId}", Name = "DeleteEnvironmentObject")]
        public async Task<ActionResult> DeleteAsync(Guid environmentObjectId)
        {
            var environmentObject = await _Environment2dRepository.SelectAsync(environmentObjectId);

            if (environmentObject == null)
                return NotFound(new ProblemDetails { Detail = $"EnvironmentObject {environmentObjectId} not found" });

            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (environmentObject.UserId != userIdString)
            {
                return Forbid();
            }

            await _ObjectRepository.DeleteEnvAsync(environmentObjectId);
            await _Environment2dRepository.DeleteAsync(environmentObjectId);

            return Ok();
        }
    }
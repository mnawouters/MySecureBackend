using Microsoft.AspNetCore.Mvc;
using MySecureBackend.WebApi.Models;
using MySecureBackend.WebApi.Repositories;
using MySecureBackend.WebApi.Services;

namespace MySecureBackend.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class Environment2dController : ControllerBase
    {
        private readonly IEnvironmentRepository _Environment2dRepository;
        private readonly IAuthenticationService _envAuthenticationService;

        public Environment2dController(IEnvironmentRepository EnvironmentRepository, IAuthenticationService envAuthenticationService)
        {
            _Environment2dRepository = EnvironmentRepository;
            _envAuthenticationService = envAuthenticationService;
        }

        [HttpGet(Name = "GetEnvironment2Ds")]
        public async Task<ActionResult<List<EnvironmentObject>>> GetAsync()
        {
            var userIdString = _envAuthenticationService.GetCurrentAuthenticatedUserId();
            if (string.IsNullOrWhiteSpace(userIdString) || !Guid.TryParse(userIdString, out var userGuid))
                return Unauthorized();

            var environmentObjects = await _Environment2dRepository.SelectAsync();
            var userEnvironments = environmentObjects.Where(e => e.Id == userGuid).ToList();

            return Ok(userEnvironments);
        }

        [HttpGet("{environmentObjectId}", Name = "GetEnvironmentObjectById")]
        public async Task<ActionResult<EnvironmentObject>> GetByIdAsync(Guid environmentObjectId)
        {
            var userIdString = _envAuthenticationService.GetCurrentAuthenticatedUserId();
            if (string.IsNullOrWhiteSpace(userIdString) || !Guid.TryParse(userIdString, out var userGuid))
                return Unauthorized();

            var environmentObject = await _Environment2dRepository.SelectAsync(environmentObjectId);

            if (environmentObject == null)
                return NotFound(new ProblemDetails { Detail = $"environmentObject {environmentObjectId} not found" });

            if (environmentObject.Id != userGuid)
                return Forbid();

            return Ok(environmentObject);
        }

        [HttpPost(Name = "AddEvironmentObject")]
        public async Task<ActionResult<EnvironmentObject>> AddAsync(EnvironmentObject environmentObject)
        {
            var userIdString = _envAuthenticationService.GetCurrentAuthenticatedUserId();
            if (string.IsNullOrWhiteSpace(userIdString) || !Guid.TryParse(userIdString, out var userGuid))
                return Unauthorized();

            environmentObject.EnvGuid = Guid.NewGuid();
            // ensure the environment is linked to the currently authenticated user
            environmentObject.Id = userGuid;

            await _Environment2dRepository.InsertAsync(environmentObject);

            return CreatedAtRoute("GetEnvironmentObjectById", new { environmentObjectId = environmentObject.EnvGuid }, environmentObject);
        }

        [HttpPut("{environmentObjectId}", Name = "UpdateEnvironmentObject")]
        public async Task<ActionResult<EnvironmentObject>> UpdateAsync(Guid environmentObjectId, EnvironmentObject environmentObject)
        {
            var userIdString = _envAuthenticationService.GetCurrentAuthenticatedUserId();
            if (string.IsNullOrWhiteSpace(userIdString) || !Guid.TryParse(userIdString, out var userGuid))
                return Unauthorized();

            var existingEnvironmentObject = await _Environment2dRepository.SelectAsync(environmentObjectId);

            if (existingEnvironmentObject == null)
                return NotFound(new ProblemDetails { Detail = $"EnvironmentObject {environmentObjectId} not found" });

            // Only allow updates by the owner
            if (existingEnvironmentObject.Id != userGuid)
                return Forbid();

            if (environmentObject.EnvGuid != environmentObjectId)
                return Conflict(new ProblemDetails { Detail = "The id of the EnvironmentObject in the route does not match the id of the EnvironmentObject in the body" });

            // Prevent changing the owner — enforce the owner to remain the authenticated user
            environmentObject.Id = existingEnvironmentObject.Id;

            await _Environment2dRepository.UpdateAsync(environmentObject);

            return Ok(environmentObject);
        }

        [HttpDelete("{environmentObjectId}", Name = "DeleteEnvironmentObject")]
        public async Task<ActionResult> DeleteAsync(Guid environmentObjectId)
        {
            var userIdString = _envAuthenticationService.GetCurrentAuthenticatedUserId();
            if (string.IsNullOrWhiteSpace(userIdString) || !Guid.TryParse(userIdString, out var userGuid))
                return Unauthorized();

            var environmentObject = await _Environment2dRepository.SelectAsync(environmentObjectId);

            if (environmentObject == null)
                return NotFound(new ProblemDetails { Detail = $"EnvironmentObject {environmentObjectId} not found" });

            if (environmentObject.Id != userGuid)
                return Forbid();

            await _Environment2dRepository.DeleteAsync(environmentObjectId);

            return Ok();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MySecureBackend.WebApi.Models;
using MySecureBackend.WebApi.Repositories;
using MySecureBackend.WebApi.Services;

namespace MySecureBackend.WebApi.Controllers;

    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ObjectController : ControllerBase
    {
        private readonly IObjectRepository _ObjectRepository;
        private readonly IAuthenticationService _objAuthenticationService;
        private readonly IEnvironmentRepository _Environment2dRepository;

        public ObjectController(IObjectRepository ObjectRepository, IEnvironmentRepository environmentRepository, IAuthenticationService objAuthenticationService)
        {
            _ObjectRepository = ObjectRepository;
            _objAuthenticationService = objAuthenticationService;
        _Environment2dRepository = environmentRepository;
        }

        [HttpGet(Name = "GetObject2Ds")]
        public async Task<ActionResult<List<ObjectRepo>>> GetAsync()
        {
            var objectRepo = await _ObjectRepository.SelectAsync();
            return Ok(objectRepo);
        }

        [HttpGet("{objectId}", Name = "GetObjectById")]
        public async Task<ActionResult<ObjectRepo>> GetByIdAsync(Guid ObjectId)
        {
            var objectRepo = await _ObjectRepository.SelectAsync(ObjectId);

            if (objectRepo == null)
                return NotFound(new ProblemDetails { Detail = $"object {ObjectId} not found" });

            return Ok(objectRepo);
        }

        [HttpGet("environment/{environmentId}", Name = "GetObjectByEnvironment")]
        public async Task<ActionResult<IEnumerable<ObjectRepo>>> GetByEnvironmentAsync(Guid environmentId)
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized("Niet geautoriseerd");

            var environment = await _Environment2dRepository.SelectAsync(environmentId);

            if (environment == null)
                return NotFound(new ProblemDetails { Detail = $"Environment {environmentId} not found." });

            if (environment.UserId != userIdString)
                return Forbid();

            var objects = await _ObjectRepository.SelectByEnvironmentAsync(environmentId);
                return Ok(objects);
        }

        [HttpPost(Name = "AddObject")]
        public async Task<ActionResult<ObjectRepo>> AddAsync(ObjectRepo objectRepo)
        {
            objectRepo.ObjGuid = Guid.NewGuid();

            await _ObjectRepository.InsertAsync(objectRepo);

            return CreatedAtRoute("GetObjectById", new { objectId = objectRepo.ObjGuid }, objectRepo);
        }

        [HttpPut("{ObjectId}", Name = "UpdateObject")]
        public async Task<ActionResult<ObjectRepo>> UpdateAsync(Guid objectId, ObjectRepo objectRepo)
        {
            var existingObject = await _ObjectRepository.SelectAsync(objectId);

            if (existingObject == null)
                return NotFound(new ProblemDetails { Detail = $"Object {objectId} not found" });

            if (objectRepo.ObjGuid != objectId)
                return Conflict(new ProblemDetails { Detail = "The id of the Object in the route does not match the id of the Object in the body" });

            await _ObjectRepository.UpdateAsync(objectRepo);

            return Ok(objectRepo);
        }

        [HttpDelete("{ObjectId}", Name = "DeleteObject")]
        public async Task<ActionResult> DeleteAsync(Guid objectId)
        {
            var objectRepo = await _ObjectRepository.SelectAsync(objectId);

            if (objectRepo == null)
                return NotFound(new ProblemDetails { Detail = $"Object {objectId} not found" });

            await _ObjectRepository.DeleteAsync(objectId);

            return Ok();
        }
    }
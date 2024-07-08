using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController(IPlatformRepository platformRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet(Name = nameof(GetAllPlatforms))]
        public ActionResult<IEnumerable<PlatformReadResponse>> GetAllPlatforms()
        {
            IEnumerable<Platform> platforms = platformRepository.GetAllPlatforms();

            IEnumerable<PlatformReadResponse> platformsReadResponse =
                mapper.Map<IEnumerable<PlatformReadResponse>>(platforms);

            return Ok(platformsReadResponse);
        }

        [HttpGet("{id}", Name = nameof(GetPlatform))]
        public ActionResult<PlatformReadResponse> GetPlatform(int id)
        {
            Platform? platform = platformRepository.GetPlatform(id);

            if(platform == null)
                return NotFound();

            PlatformReadResponse platformReadResponse = mapper.Map<PlatformReadResponse>(platform);

            return Ok(platformReadResponse);
        }

        [HttpPost(Name = nameof(CreatePlatform))]
        public ActionResult<PlatformReadResponse> CreatePlatform([FromBody] PlatformCreateRequest platformCreateRequest)
        {
            if(platformCreateRequest == null)
                return BadRequest();

            Platform platform = mapper.Map<Platform>(platformCreateRequest);

            platformRepository.CreatePlatform(platform);
            if (platformRepository.SaveChanges())
            {
                PlatformReadResponse platformReadResponse = mapper.Map<PlatformReadResponse>(platform);

                return CreatedAtRoute(
                    routeName: nameof(GetPlatform),
                    routeValues: new {id= platformReadResponse.Id},
                    value: platformReadResponse
                );
            }

            return BadRequest();
        }
    }
}
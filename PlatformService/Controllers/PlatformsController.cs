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
        [HttpGet(nameof(GetAllPlatforms))]
        public ActionResult<IEnumerable<PlatformReadResponse>> GetAllPlatforms()
        {
            IEnumerable<Platform> platforms = platformRepository.GetAllPlatforms();

            IEnumerable<PlatformReadResponse> platformsReadResponse =
                mapper.Map<IEnumerable<PlatformReadResponse>>(platforms);

            return Ok(platformsReadResponse);
        }

        [HttpGet(nameof(GetPlatform))]
        public ActionResult<PlatformReadResponse> GetPlatform(int id)
        {
            Platform? platform = platformRepository.GetPlatform(id);

            //El resultado podría ser NULL

            PlatformReadResponse platformReadResponse = mapper.Map<PlatformReadResponse>(platform);

            return Ok(platformReadResponse);
        }
    }
}
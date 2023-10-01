using ChromeExtension.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChromeExtension.Controllers{
    [ApiController]
    [Route("api/")]
    public class TaskController : ControllerBase{

        private readonly IMemoryService _memoryService;

        public TaskController(IMemoryService memoryService)
        {
            _memoryService = memoryService;
        }

        /// <summary>
        /// Allocates and creates memory location for the storage of the videos
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// </remarks>
        /// <returns>A response with 201 and a message</returns>
        [HttpGet("StartRecording")]
        public IActionResult StartRecording()
        {
            return Ok(_memoryService.AllocateMemory());
        }

        /// <summary>
        /// Saves video recording
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// <code>
        /// POST api/SaveRecording
        /// {
        ///     "byteStreams": [],
        ///     "savePath": "www.bahdman/VideoRecordings\\7787/recording.mp4",
        ///     "key": "1"
        /// }
        /// </code>
        /// </remarks>
        /// <returns>A response with 200 and a message</returns>
        [HttpPost("SaveRecording")]
        public IActionResult SaveRecording([FromBody] byte[] byteStream, string savePath, int key)
        {
            return Ok( _memoryService.SaveVideo(byteStream, savePath, key));
        }
        
        /// <summary>
        /// Get all saved recordings
        /// </summary>
        /// <returns>A response with 200 and a message</returns>
        [HttpGet("GetAllRecordings")]
        public IActionResult GetAllRecordings()
        {
            return Ok(_memoryService.GetAllVideoPath());
        }

        /// <summary>
        /// Downloads video recording
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// <code>
        /// GET api/VideoRecordings
        /// </code>
        /// </remarks>
        /// <returns>A response with 200 and a message</returns>
        [HttpGet("VideoRecordings")]
        public async Task<IActionResult> Download(string fileUrl)
        {
            var response = _memoryService.VerifyPath(fileUrl);
            if(response.Data == true)
            {
                byte[] bytes = await System.IO.File.ReadAllBytesAsync(fileUrl);
                return File(bytes, "video/mp4", "videoRecordings.mp4");
                
            }
            
            return Ok(response);
        }

        [HttpGet("TestApi")]
        public IActionResult TestApi()
        {
            return Ok("Works fine");
        }
    }
}
using ChromeExtension.Handlers;
using ChromeExtension.Models;
using ChromeExtension.Services.Interfaces;

namespace ChromeExtension.Services.Implementations{

    public class MemoryRepository : IMemoryService
    {

        private readonly IWebHostEnvironment _env;

        public MemoryRepository(IWebHostEnvironment env)
        {
            _env = env;
        }
        public ApiResponse<string>  AllocateMemory()
        {
            try{
                
                var handler = new ApiHandler(_env);            

                return ApiResponse<string>.Success(handler.PathGenerator(), "Memory allocation successfull", 201);

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ApiResponse<string>.Fail("Failed to allocate Memory", 500);
            }
        }

        public ApiResponse<IList<string>> GetAllVideoPath()
        {
            try{
                var handler = new ApiHandler(_env);

                if(handler.GetAllFilePaths() == null)
                {
                    return ApiResponse<IList<string>>.Fail("No item found", 404);
                } 
                
                return ApiResponse<IList<string>>.Success(handler.GetAllFilePaths(),"Items retrieved successfully");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ApiResponse<IList<string>>.Fail("Unable to complete process", 500);
            }
        }

        public ApiResponse<VideoResponse> SaveVideo(byte[] byteStream, string savePath, int key)
        {
            try{
                var handler = new ApiHandler();
                var handlerResponse = handler.SaveVideo(byteStream, savePath, key);

                if(handlerResponse == "failed" || handlerResponse == "error")
                {
                    return ApiResponse<VideoResponse>.Fail("Failed to save video", 400);
                }
                var response  = new VideoResponse(){
                    FilePath = handlerResponse,
                    key = key,
                    Status = true
                };

                return ApiResponse<VideoResponse>.Success(response, "Item has successfully been saved", 201);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ApiResponse<VideoResponse>.Fail("Unable to complete process", 500);
            }            
        }

        public ApiResponse<VideoResponse> VerifyPath(string fileUrl)
        {
            try{
                var handler = new ApiHandler(_env);
                // string modifiedUrl = fileUrl.Remove()
                // C:\\Users\\user\\source\\repos\\HNG\\FifthTask\\ChromeExtension\\wwwroot\\VideoRecordings\\7787
                // www.bahdman.net/download\\VideoRecordings\\7787
                var handlerResponse = handler.CheckFilePath(fileUrl);
                if(handlerResponse == "not found")
                {
                    return ApiResponse<VideoResponse>.Fail("Video Not Found", 404);
                }

                var response = new VideoResponse()
                {
                    FilePath = handlerResponse,
                    key = 0,
                    Status = true
                };

                return ApiResponse<VideoResponse>.Success( response, "File exists for download", 200);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ApiResponse<VideoResponse>.Fail("Unable to complete process", 500);
            }           
        }
    }
}
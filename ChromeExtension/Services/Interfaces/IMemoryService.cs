using ChromeExtension.Models;

namespace ChromeExtension.Services.Interfaces{
    public interface IMemoryService
    {
        ApiResponse<string> AllocateMemory();
        ApiResponse<VideoResponse> SaveVideo(byte[] byteStream, string filePath, int key);
        // Task<ApiResponse<string>> GetAllVideoPath();
        ApiResponse<IList<string>> GetAllVideoPath();
        ApiResponse<bool> VerifyPath(string fileUrl);
    }
}
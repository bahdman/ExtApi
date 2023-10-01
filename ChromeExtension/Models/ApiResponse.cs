namespace ChromeExtension.Models{
    public class ApiResponse<T>
    {
        public T Data {get; set;}
        public string Message{get; set;}
        public int StatusCode{get; set;}
    

        public ApiResponse(T data, string message, int statusCode)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;        
        }

        public ApiResponse()
        {

        }

        public static ApiResponse<T> Fail(string errorMessage, int statusCode)
        {
            return new ApiResponse<T>{Message = errorMessage, StatusCode = statusCode};
        }

        public static ApiResponse<T> Success(T data, string successMessage, int statusCode = 200)
        {
            return new ApiResponse<T>{Data = data, Message = successMessage, StatusCode = statusCode};
        }
    }
}
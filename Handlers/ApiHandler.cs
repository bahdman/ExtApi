using ChromeExtension.Models;

namespace ChromeExtension.Handlers{
    public class ApiHandler
    {
        private readonly  IWebHostEnvironment _env;

        public ApiHandler()
        {
        }

        public ApiHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        private string PathHandler(string path)
        {
            var envPath = _env.WebRootPath;
            var siteUrl = "https://bahdman.bsite.net/";

            var charSpacing = string.Empty;
            var refinedPath = string.Empty;

            if(path.Contains(envPath))
            {
                charSpacing = path.Replace(envPath, siteUrl);
                refinedPath = charSpacing.Replace("\\", "/");
                return refinedPath;
            }

            charSpacing = path.Replace(siteUrl, envPath);
            refinedPath = charSpacing.Replace("/", "\\");
            // refinedPath = path.Replace(siteUrl, envPath);
            return refinedPath;
            // C:\\Users\\user\\source\\repos\\HNG\\FifthTask\\ChromeExtension\\wwwroot\\VideoRecordings\\7787
        }

        public string PathGenerator()
        {
            try{
                // var path = _env.WebRootPath + "\\VideoRecordings\\";
                var path = Path.Combine(_env.WebRootPath, "VideoRecordings");
                var generatedPath = string.Empty;
                var rand = new Random();
                if(Directory.Exists(path))
                {
                    var status = true;
                    while(status)
                    {
                        var key = rand.Next(1000,10000).ToString();

                        generatedPath = Path.Combine(path, key);
                        status = !Directory.Exists(generatedPath);
                        if(status)
                        {
                            Directory.CreateDirectory(generatedPath);
                            status = false;
                        }
                    }  

                    return PathHandler(generatedPath);        
                }
                var folderKey = rand.Next(1000,10000).ToString();

                var fileInfo = Directory.CreateDirectory(Path.Combine(path , folderKey));

                return PathHandler(fileInfo.FullName);
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "error";
            }
                       
        }

        public IList<string> GetAllFilePaths()
        {
            try{
                var path = Path.Combine(_env.WebRootPath , "VideoRecordings");
                if(Directory.Exists(path))
                {
                    var directory = new DirectoryInfo(path);

                    var directories = directory.GetDirectories().ToList();
                    var listItem = new List<string>();

                    foreach(var item in directories)
                    {
                        listItem.Add(PathHandler(Path.Combine(item.FullName, "recording.mp4")));
                    }

                    return listItem;
                }

                return null;

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        // public string SaveVideo(byte[] bytes, string savePath)
        // {
        //     if(Path.Exists(savePath))
        //     {
        //         var path = Path.Combine(savePath, "recording.mp4");
        //         using (FileStream fs = new (path, FileMode.Create)){
        //             fs.Seek(0, SeekOrigin.Begin);
        //             fs.Write(bytes, 0, bytes.Length);
        //         };

        //         return "saved";
        //     }

        //     return "failed";
        // }

        public VideoResponse SaveVideo(byte[] bytes, string savePath, int key)
        {
            try
            {
                var videoResponse = new VideoResponse();
                if (Path.Exists(savePath))
                {
                    
                    var filePath = Path.Combine(savePath, "recording.mp4");

                    FileMode fileMode = key < 1 ? FileMode.Create : FileMode.Append;

                    using (FileStream fs = new(filePath, fileMode))
                    {
                        fs.Seek(0, SeekOrigin.Begin);

                        int bufferSize = 4096;
                        int offset = 0;

                        while (offset < bytes.Length)
                        {
                            int bytesToCopy = Math.Min(bufferSize, bytes.Length - offset);
                            fs.Write(bytes, offset, bytesToCopy);
                            offset += bytesToCopy;
                        }

                        var test = new AudioHandler(); //Rough work.... would need bg service
                        var response = test.GenerateAudioFile(filePath);
                        if(response == true)
                        {
                            var transcriptPath = TranscribeAudio(filePath).Result;

                            videoResponse = new VideoResponse()
                            {
                                FilePath = filePath,
                                TranscriptUrl = transcriptPath,
                                key = key,
                                Status = true
                            };
                        }             

                        return videoResponse;
                    }
                }
                else
                {
                    videoResponse = new VideoResponse()
                    {
                        FilePath = savePath,
                        TranscriptUrl = "",
                        key = key,
                        Status = false
                    };

                    return videoResponse;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var videoResponse = new VideoResponse()
                {
                    FilePath = savePath,
                    TranscriptUrl = "",
                    key = key,
                    Status = false
                };

                return videoResponse;
            }
        }


        public string CheckFilePath(string filePath)
        {
            try{
                var refinedPath = PathHandler(filePath);
                if(Path.Exists(refinedPath))
                {
                    return refinedPath;
                } 

                return "not found";

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "not found";
            }
            
        }


        //TODO:: Add this as a background service
        public async Task<string> TranscribeAudio(string path)
        {
            try{
                // var refinedPath = path.Replace("")
                var transcriptHandler = new TranscriptHandler();
                string transcriptPath = "C:\\Users\\user\\source\\repos\\HNG\\FifthTask\\ChromeExtension\\wwwroot\\Transcript\\trans.srt";

                var responseData = await transcriptHandler.Transcribe(path);

                if(!File.Exists(transcriptPath))
                {
                    File.Create(transcriptPath);
                }

                await File.WriteAllTextAsync(transcriptPath, responseData.ToString());  

                return transcriptPath;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

    }
}
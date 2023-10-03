using NAudio.Wave;

namespace ChromeExtension.Handlers{
    public class AudioHandler{
        public bool GenerateAudioFile(string videoPath)
        {
            try{
                string audioFilePath = videoPath.Replace("recording.mp4", "audio.wav");

                // Create a MediaFoundationReader to read audio from the video file
                using (var reader = new MediaFoundationReader(videoPath))
                {
                    // Specify the output audio file format (e.g., WAV)
                    var outputFormat = new WaveFormat(44100, 16, 2); // Sample rate, bit depth, channels

                    // Create a WaveFileWriter to write the extracted audio to a file
                    using (var writer = new WaveFileWriter(audioFilePath, outputFormat))
                    {
                        // Read and write audio data in chunks
                        byte[] buffer = new byte[8192];
                        int bytesRead;
                        while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            writer.Write(buffer, 0, bytesRead);
                        }
                    }

                }
                
                return true;

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
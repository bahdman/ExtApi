# ExtApi

Endpoint Doc (chrome ext)

Quick usage (contains requests and only success responses atm)

Url: https://bahdman.bsite.net/swagger
==== SUMMARY ===
Methods: {
StartRecording: GET  => Create and allocates path where video should be 
stored.
SaveRecording: POST => Saves recording
GetAllRecordings GET => Get all video saved paths
VideoRecordings GET => Downloads Video (specify the path)
TestApi GET => Tests the api to ensure it works
}


Sample Usage
Test the api to ensure it works.
https://bahdman.bsite.net/api/TestApi
Method : Get
response : "Works fine"
Main Methods
https://bahdman.bsite.net/api/StartRecording  ==> allocates memory and 
path where video would be saved
Method : GET
Success Reponse {
  "data": "https://bahdman.bsite.net//VideoRecordings/7111",
  "message": "Memory allocation successful",
  "statusCode": 201
}

https://bahdman.bsite.net/api/SaveRecording ==> handles saving of the 
video.
Method : POST
request body {
byteStreams : byte[],
savePath : " https://bahdman.bsite.net//VideoRecordings/7111",
key : "0" => chunk count (increment this for every chunk sent)
}

Success Response{
FilePath : 
“https://bahdman.bsite.net//VideoRecordings/7111/recording.mp4”,
Key : 1,
Status : true
}

https://bahdman.bsite.net/api/GetAllRecordings => returns all saved paths
Method : GET
Success Response{
Data : [all saved paths],
Message: “Items retrieved successfully”,
StatusCode: 200
}

https://bahdman.bsite.net/api/VideoRecordings => downloads specified 
file(url needed)
Method : GET
request{
fileUrl: “https://bahdman.bsite.net//VideoRecordings/1476/recording.mp4”
Success Response : returns file

# ExtApi


## API Documentation

### Live Link: (https://bahdman.bsite.net/)

#### StartRecording

- **Endpoint:** `GET api/StartRecording`
- **Description:** Allocates and creates memory location for the storage of the videos.
- **Sample Request:** N/A
- **Response:**
  - HTTP Status: 201 (Created)
  - Message: Memory allocation successful

#### SaveRecording

- **Endpoint:** `POST api/SaveRecording`
- **Description:** Saves video recording.
- **Sample Request:**
  ```json
  POST api/SaveRecording
  {
      "byteStreams": [],
      "savePath": "www.bahdman.bsite.net/VideoRecordings/7787/recording.mp4",
      "key": 1
  }
  ```
- **Response:**
  - HTTP Status: 200 (OK)
  - Message: Video saved successfully

#### GetAllRecordings

- **Endpoint:** `GET api/GetAllRecordings`
- **Description:** Get all saved recordings.
- **Sample Request:** N/A
- **Response:**
  - HTTP Status: 200 (OK)
  - Message: List of video paths

#### Download

- **Endpoint:** `GET api/VideoRecordings`
- **Description:** Downloads video recording.
- **Sample Request:** `GET api/VideoRecordings?fileUrl={fileUrl}`
- **Response:**
  - HTTP Status: 200 (OK)
  - Message: Video file for download

#### TestApi

- **Endpoint:** `GET api/TestApi`
- **Description:** Test API endpoint.
- **Sample Request:** N/A
- **Response:**
  - HTTP Status: 200 (OK)
  - Message: Test API works fine


### Error Responses

#### StartRecording

- **HTTP Status:** 500 (Internal Server Error)
- **Message:** Memory allocation failed due to an internal server error.

#### SaveRecording

- **HTTP Status:** 400 (Bad Request)
- **Message:** Failed to save the video due to invalid data or format.

- **HTTP Status:** 500 (Internal Server Error)
- **Message:** Failed to save the video due to an internal server error.

#### GetAllRecordings

- **HTTP Status:** 404 (Not Found)
- **Message:** No video recordings found in the database.

- **HTTP Status:** 500 (Internal Server Error)
- **Message:** Failed to retrieve video recordings due to an internal server error.

#### Download

- **HTTP Status:** 404 (Not Found)
- **Message:** The requested video file was not found.

- **HTTP Status:** 500 (Internal Server Error)
- **Message:** Failed to download the video due to an internal server error.

#### TestApi

- **HTTP Status:** 500 (Internal Server Error)
- **Message:** Test API encountered an internal server error.


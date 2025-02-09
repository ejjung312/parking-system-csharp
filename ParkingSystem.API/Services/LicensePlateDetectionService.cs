using System.Net.Http;
using System.Net.Http.Headers;
using OpenCvSharp;

namespace ParkingSystem.API.Services
{
    public class LicensePlateDetectionService : ILicensePlateDetectionService
    {
        private readonly PrepHttpClient _client;

        public LicensePlateDetectionService(PrepHttpClient client)
        {
            _client = client;
        }

        public async Task<byte[]> SendFrame(Mat frame)
        {
            string uri = _client.BaseAddress + "predict_license_plate";

            byte[] bytes = frame.ToBytes();
            var content = new ByteArrayContent(bytes);
            //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            byte[] responseImage = await response.Content.ReadAsByteArrayAsync();

            return responseImage;
        }
    }
}

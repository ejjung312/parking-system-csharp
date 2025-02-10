using OpenCvSharp;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ParkingSystem.API.Services
{
    public class ParkingDetectionService : IParkingDetectionService
    {
        private readonly PrepHttpClient _client;

        public ParkingDetectionService(PrepHttpClient client)
        {
            _client = client;
        }

        public async Task<byte[]> SendFrame(Mat frame)
        {
            string uri = _client.BaseAddress + "predict_parking_monitoring";

            byte[] bytes = frame.ToBytes();
            var content = new ByteArrayContent(bytes);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            byte[] responseImage = await response.Content.ReadAsByteArrayAsync();

            return responseImage;
        }
    }
}

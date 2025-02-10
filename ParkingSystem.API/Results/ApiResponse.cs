using System.Text.Json.Serialization;

namespace ParkingSystem.API.Results
{
    // JSON 응답을 매핑할 C# 클래스
    public class ApiResponse
    {
        [JsonPropertyName("processed_img")]
        public string ProcessedImg { get; set; }  // processed_img -> ProcessedImg

        [JsonPropertyName("license_plate_img")]
        public string LicensePlateImg { get; set; } // license_plate_img -> LicensePlateImg

        [JsonPropertyName("license_plate_text")]
        public string LicensePlateText { get; set; } // license_plate_text -> LicensePlateText
    }
}

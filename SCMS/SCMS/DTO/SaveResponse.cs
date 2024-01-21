using SCMS.Controllers.Helper;

namespace SCMS.DTO
{
    public class SaveResponse
    {
        public string StatusCode { get; set; }
        public string Message { get; set; } 
        public JsonContent Data { get; set; }
        public LoginResponse Session { get; set; }
    }
}

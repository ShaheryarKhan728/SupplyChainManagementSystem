﻿namespace SCMS.DTO
{
    public class SaveResponse
    {
        public string StatusCode { get; set; }
        public string Message { get; set; } 
        public JsonContent Data { get; set; }
    }
}

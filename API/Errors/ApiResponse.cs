﻿namespace Re_ABP_Backend.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Authorized Error",
                403 => "You are not allowed here",
                404 => "Resource not found",
                500 => "Server error",
                _ => "Unknown Status Code"
            };
        }
    }
}
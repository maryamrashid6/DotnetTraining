using System.Net;
using TodoApi.Services.Core.Enums;

namespace TodoApi.Services.Dtos
{
    public class AutoResponseDto<T>
    {
        public AutoResponseDto()
        {
            Warnings = new List<string>();
            Errors = new List<string>();
            Success = true;
            StatusCode = HttpStatusCode.OK;
        }

        public AutoResponseDto(bool success)
        {
            Warnings = new List<string>();
            Errors = new List<string>();
            Success = success;
            StatusCode = HttpStatusCode.OK;
        }

        public AutoResponseDto(bool success, string message)
        {
            Warnings = new List<string>();
            Errors = new List<string>();
            Success = success;
            Message = message;
            StatusCode = HttpStatusCode.OK;
        }

        public bool Success { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Exception { get; set; }
        public DateTime CurrentUtcDateTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Set error or message according to response type
        /// </summary>
        /// <param name="content">Error or message to pass in response model</param>
        /// <param name="type"></param>
        public void AddError(string content, ResponseType type = ResponseType.Message)
        {
            if (!string.IsNullOrEmpty(content))
            {
                if (type == ResponseType.Message)
                    Message = content;

                else if (type == ResponseType.Error)
                    Errors.Add(content);

                StatusCode = HttpStatusCode.BadRequest;
                Success = false;
            }
        }

        public void AddWarning(string content)
        {
            Warnings.Add(content);
        }

        public void AddError(List<string> errorList)
        {
            Errors.AddRange(errorList);
            Success = false;
        }

        public void AddMessage(string content)
        {
            Message = content;
        }

        public void SetStatusCode(HttpStatusCode code)
        {
            StatusCode = code;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Dtos.Responses
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = false;
        public T? Data { get; set; }
        public string? Message { get; set; }
        public ServiceResponse()
        {
        }

        public ServiceResponse(bool success, T data, string message)
        {
            Success = success;
            Data = data;
            Message = message;
        }
    }
}

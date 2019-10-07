using System;
using System.Collections.Generic;
using System.Text;

namespace Zip.Challenge.Common.Dto
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public Exception Error { get; set; }
    }
}

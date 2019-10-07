using System.Text.Json;

namespace Zip.Challenge.Common.Exceptions
{
    public class ErrorDetail
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

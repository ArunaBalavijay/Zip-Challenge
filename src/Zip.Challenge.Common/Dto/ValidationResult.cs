using System;
using System.Collections.Generic;
using System.Text;

namespace Zip.Challenge.Common.Dto
{
    public class ValidationResult
    {
        public ValidationResult(int status, Dictionary<string, string> errors = null)
        {
            Status = status;
            Errors = errors;
            Success = errors == null;
            Title = errors == null ? GetSuccessTitle() : "One or more validation errors occurred.";
        }

        public string Title { get; set; }
        public int Status { get; set; }
        public bool Success { get; set; }
        public Dictionary<string, string> Errors { get; set; }

        private string GetSuccessTitle()
        {
            switch (Status)
            {
                case 200:
                    return "Success";
                case 202:
                    return "Accepted";
                default:
                    return null;
            }
        }
    }
}

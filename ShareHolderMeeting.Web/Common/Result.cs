using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web
{
    public class Result
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public int Id { get; set; }

        public Result(bool success, string errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }
        public Result(bool success, string errorMessage, int id)
        {
            Success = success;
            ErrorMessage = errorMessage;
            Id = id;
        }
    }
}
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Controllers
{
    public class ControllerHelper
    {
        public static dynamic TranslateErrorToClient(Result result)
        {

            return new { Status = result.IsSuccess, Message = result.Error, Id = 0 };
        }

        public static dynamic TranslateErrorToClient(Result<int> result)
        {
            return new { Status = result.IsSuccess, Message = result.Error, Id = result.Value };
        }      
    }

    
}
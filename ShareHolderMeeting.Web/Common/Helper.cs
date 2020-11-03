using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Common
{
    public class Helper
    {
        public static dynamic TranslateErrorToClient(Result result)
        {

            return new { Status = result.IsSuccess, Message = result.Error, Id = 0 };
        }

        public static dynamic TranslateErrorToClient(Result<int> result)
        {
            return new { Status = result.IsSuccess, Message = result.Error, Id = result.Value };
        }

        public static string MergeErrors(IEnumerable<string> brokerRules)
        {
            var result = "";

            foreach (var rule in brokerRules)
            {
                result += ";" + rule;
            }
            return result;
        }

    }

    
}
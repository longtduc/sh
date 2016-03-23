using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models.CoreServices
{
    public class InputErrors
    {
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
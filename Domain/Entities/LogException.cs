using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class LogException
    {
        public int Id { get; set; }
        public string Message { get; private set; }
        public LogException(string message)
        {
            this.Message = message;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        //public int Id { get; set; }

        protected Result(bool isSuccess, string error)
        {
            if (isSuccess && error != string.Empty)
                throw new InvalidOperationException();

            if (!isSuccess && error == string.Empty)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result OK()
        {
            return new Result(true, "");
        }

        public static Result Fail(string error)
        {
            return new Result(false, error);
        }

        public static Result<T> OK<T>(T value)
        {
            return new Result<T>(value, true, "");
        }      

        public static Result<T> Fail<T>(string error)
        {
            return new Result<T>(default(T), false, error);
        }        
    }

    public class Result<T> : Result
    {
        private T _value;

        public T Value
        {
            get
            {
                //Contract.Requires(Success);
                return _value;
            }

            private set { _value = value; }
        }

        protected internal Result(T value, bool success, string error)
            : base(success, error)
        {
            //Contract.Requires(value != null || !success);
            Value = value;
        }
    }
}
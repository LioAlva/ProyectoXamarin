using System;
using System.Collections.Generic;
using System.Text;

namespace PModelo.Classes
{
    public class ResponseT<T>
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public T Result { get; set; }
    }
}

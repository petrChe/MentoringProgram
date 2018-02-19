using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram5
{
    /// <summary>
    /// Собственный класс исключений
    /// </summary>
    [Serializable]
    public class MyOwnException : Exception
    {
        public MyOwnException()
            : base()
        {
        }

        public MyOwnException(string message)
            : base(message)
        {
        }

        public MyOwnException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected MyOwnException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

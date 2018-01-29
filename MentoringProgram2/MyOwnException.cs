using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram2
{
    /// <summary>
    /// Собственный класс исключений
    /// </summary>
    [Serializable]
    public class MyOwnException : Exception
    {
        //Номер символа в строке, можно использовать при возбуждении исключения
        public int symbolPosition { get; private set; }

        public MyOwnException(int symbolPosition)
            :base()
        {
            this.symbolPosition = symbolPosition;
        }

        public MyOwnException(int symbolPosition, string message) 
            : base(message)
        {
            this.symbolPosition = symbolPosition;
        }

        public MyOwnException(int symbolPosition, string message, Exception innerException)
            : base(message, innerException)
        {
            this.symbolPosition = symbolPosition;
        }

        protected MyOwnException(int symbolPosition, SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.symbolPosition = symbolPosition;
        }
    }
}

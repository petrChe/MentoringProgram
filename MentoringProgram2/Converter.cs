using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram2
{
    //Класс конвертер
    public class Converter
    {
        //Метод конвертации строки в число
        public int ConvertStringToInt(string str)
        {
            var result = 0;

            try
            {
                var chars = str.ToCharArray();
                for (var i = 0; i < chars.Length; i++)
                {
                    if (Char.IsLetter(chars[i]))
                        // вынесите пожалуйста все в ресурсы // можешь создать отдельное приложение для всех своих будущих задач, учись писать красиво + это незаменимая штука если приклад мультиязычный
                        throw new MyOwnException(i + 1, "В строке не должно быть буквенных символов. Позиция элемента - "); // избавиться от i + 1 (назвать мне как минимум 2 способа)
                    if(Char.IsWhiteSpace(chars[i]))
                        throw new MyOwnException(i + 1, "В строке не должно быть пробелов. Позиция элемента - ");
                    if(Char.IsPunctuation(chars[i]))
                        throw new MyOwnException(i +1, "В строке не должно быть знаков пунктуации. Позиция элемента - ");
                }
            }
            catch (MyOwnException)
            {
                throw;
            }

            return result;
        }
    }
}

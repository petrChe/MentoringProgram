using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram.Helpers
{
    public class HelperConsts
    {
        //Формат даты
        public const string DataFormat = "dd.MM.yyyy";

        //Общая ошибка
        public const string CommonError = "Произошла ошибка. Обратитесь к администратору";

        public static List<string> filesExtensions = new List<string>
        {
            "txt", "jpg", "doc"
        };
    }
}

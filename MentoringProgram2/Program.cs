using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Настройка кодировки
            Console.OutputEncoding = Encoding.UTF8;

            //Задание 1
            while (true)
            {
                var str = Console.ReadLine();

                if (str.ToLower() == "exit")
                    break;

                if (string.IsNullOrEmpty(str))
                    Console.WriteLine("Вы ввели пустую строку");
                else
                {
                    Console.WriteLine(string.Format("Первый символ строки - {0}", str.First()));
                }
            }

            //Задание 2
            try
            {
                Converter converter = new Converter();
                Console.WriteLine("Введите число");
                var numberStr = Console.ReadLine();

                converter.ConvertStringToInt(numberStr);
            }
            catch (MyOwnException ex)
            {
                Console.WriteLine(ex.Message + ex.symbolPosition);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}

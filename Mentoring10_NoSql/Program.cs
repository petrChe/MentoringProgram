using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Mentoring10_NoSql
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoServer mongo = MongoServer.Create();
            mongo.Connect();

            var db = mongo.GetDatabase("booksLib");

            mongo.Disconnect();

            Console.ReadLine();
        }
    }
}

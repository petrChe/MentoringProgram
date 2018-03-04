using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mentoring6_storing_system;

namespace Mentoring6
{
    public class Preparator
    {
        public List<Book> Books { get; private set; }
        public List<Newspaper> Newspapers { get; private set; }
        public List<Patent> Patents { get; private set; }

        public Preparator()
        {
            CreateObjects();
        }

        private void CreateObjects()
        {
            #region Заполнение данных
            Books = new List<Book> 
            {
                new Book
                {
                    Name = "Война и мир",
                    AuthorOrInventor = "Л.Н. Толстой",
                    //PublishingHouseName = "Издательство Караганда",
                    //PublishingYear = 1,
                    PageCount = 1274,
                    Note = "Книга для очень терпеливых людей",
                    ISBN = " 2-266-11156-7"
                },
                new Book
                {
                    Name = "Рыбалка без границ",
                    AuthorOrInventor = "Л.П. Сабанеев",
                    PlaceOfPublication = "Караганда",
                    PublishingHouseName = "Издательство Караганда",
                    PublishingYear = 1,
                    PageCount = 275,
                    Note = "Книга для любителей рыбалки",
                    ISBN = " 2-267-11156-9"
                },
                new Book
                {
                    Name = "Мастер и Маргарита",
                    AuthorOrInventor = "М.A. Булгаков",
                    PlaceOfPublication = "Караганда",
                    PublishingHouseName = "Издательство Караганда",
                    PublishingYear = 1,
                    PageCount = 275,
                    Note = "Интересный роман",
                    ISBN = " 2-267-13456-6"
                }
            };

            Newspapers = new List<Newspaper> 
            {
                new Newspaper
                {
                    Name  = "Комсомольская правда",
                    PlaceOfPublication = "Москва",
                    PublishingHouseName = "Издательство КП",
                    PublishingYear = 2,
                    Note = "Популярная российская газета",
                    Number = 256,
                    Date = new DateTime(2018,2,1),
                    ISSN = "ISSN 0028-0836"
                },
                new Newspaper
                {
                    Name  = "АиФ",
                    PlaceOfPublication = "Москва",
                    PublishingHouseName = "Издательство АиФ",
                    PublishingYear = 3,
                    Note = "Популярная российская газета",
                    Number = 789,
                    Date = new DateTime(2018,2,1),
                    ISSN = "ISSN 0028-0436"
                },
                new Newspaper
                {
                    Name  = "Московский комсомолец",
                    PlaceOfPublication = "Москва",
                    PublishingHouseName = "Издательство МК",
                    PublishingYear = 4,
                    Note = "Популярная российская газета",
                    Number = 145,
                    Date = new DateTime(2018,2,1),
                    ISSN = "ISSN 0028-6836"
                }
            };

            Patents = new List<Patent> 
            {
                new Patent
                {
                    Name = "Лазер",
                    AuthorOrInventor = "И.И. Иванов",
                    Country = "Россия",
                    RegistrationNumber = "rn-789654",
                    RequestDate = new DateTime(2001,1,1),
                    PublishingDate = new DateTime(2001,1,8),
                    PageCount = 1024,
                    Note = "Смертельное оружие"
                },
                new Patent
                {
                    Name = "Колесо",
                    AuthorOrInventor = "П.П. Петров",
                    Country = "Россия",
                    RegistrationNumber = "rn-7897854",
                    RequestDate = new DateTime(2005,3,1),
                    PublishingDate = new DateTime(2005,7,24),
                    PageCount = 1456,
                    Note = "Круглое изобретение"
                },
                new Patent
                {
                    Name = "3D-очки",
                    AuthorOrInventor = "М.Д. Сабитов",
                    Country = "Казахстан",
                    RegistrationNumber = "rn-789729",
                    RequestDate = new DateTime(2010,3,10),
                    PublishingDate = new DateTime(2010,9,14),
                    PageCount = 1562,
                    Note = "Очки, но не простые"
                }
            };
            #endregion
        }
    }
}

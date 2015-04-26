using ParticipantsOfWar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace ParticipantsOfWar.DAL
{
    public class ArchiveInitializer : DropCreateDatabaseAlways<ArchiveContext>
    {
        protected override void Seed(ArchiveContext context)
        {
            var types = new List<ParticipantType>
            {
                new ParticipantType{Name = "Участники ВОВ", Priority = 1},
                new ParticipantType{Name = "Труженники тыла",  Priority = 2},   
                new ParticipantType{Name = "Дети войны", Priority = 3},
                new ParticipantType{Name = "Участники горячих точек", Priority = 4},
                new ParticipantType{Name= "Репрессированные", Priority = 5}
            };
            types.ForEach(s => context.ParticipantTypes.Add(s));
            context.SaveChanges();

            var doctypes = new List<DocumentType>
            {
                new DocumentType{Name = "Биография"},
                new DocumentType{Name = "Паспорт"},   
                new DocumentType{Name = "Военный билет"},
                new DocumentType{Name = "Справка"}
            };
            doctypes.ForEach(s => context.DocumentTypes.Add(s));
            context.SaveChanges();


            var p = new List<Participant>
            {
            new Participant
            {                
                Surname="Крамской", 
                Firstname="Борис",
                Middlename="Аркадьевич",
                Birthday = DateTime.Parse("1910-04-01"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type = context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = " Рядовой, сапер, 133-й гвардейский тяжелый танко-самоходный полк 33-й гвардейской механизированной дивизии. Погиб 25 октября 1956 г. Похоронен в г. Кечкемете."
            },
            new Participant
            {                
                Surname="Абашев", 
                Firstname="Магафур",
                Middlename="Заретдинович",
                Birthday = DateTime.Parse("1936-04-01"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Призван Юкаменским РВК Удмуртской АССР. Рядовой, орудийный номер, 100-й гвардейский артиллерийский полк 33-й гвардейской механизированной дивизии. Погиб 26 октября 1956 г. Похоронен на кладбище Керепеши в г. Будапеште."
            },
             new Participant
            {                
                Surname="Аксенов", 
                Firstname="Анатолий",
                Middlename="Николаевич",
                Deathday = DateTime.Parse("1946-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Рядовой. Погиб в 1946 г. Похоронен на братском кладбище в г. Порт-Артуре."
            },
                new Participant
            {                
                Surname="Албул", 
                Firstname="Владимир",
                Middlename="Трофимович", 
                Birthday = DateTime.Parse("1923-04-01"), 
                Deathday = DateTime.Parse("1949-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Старший лейтенант. Погиб в 1949 г. Похоронен на братском кладбище в г. Порт-Артуре."
            },
               new Participant
            {                
                Surname="АНИКИН", 
                Firstname="Алексей",
                Middlename="Петрович", 
                Birthday = DateTime.Parse("1934-01-01"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Призван Вурнарским РВК. Ефрейтор, заряжающий, 99-й отдельный гвардейский разведывательный батальон 2-й гвардейской механизированной дивизии. Погиб 4 ноября 1956 г. Похоронен на кладбище Керепеши в г. Будапеште."
            },
              new Participant
            {                
                Surname="АРУТЮНОВ", 
                Firstname="Овик",
                Middlename="Гурганович", 
                Birthday = DateTime.Parse("1935-01-21"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Призван Кировабадским ГВК. Сержант, командир отделения, 108-й гвардейский парашютно-десантный полк 7-й гвардейской воздушно-десантной дивизии. Погиб 4 ноября 1956 г. Похоронен в братской могиле в п. Альшонемеди, предместье г. Будапешта."
            },  
              new Participant
            {                
                Surname="АРХИПОВ", 
                Firstname="Виктор",
                Middlename="Алексеевич", 
                Birthday = DateTime.Parse("1907-01-21"), 
                Deathday = DateTime.Parse("1946-12-01"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Сержант. Погиб в 1946 г. Похоронен на братском кладбище Циньюаньцзе в г. Даляне."
            },
              new Participant
            {                
                Surname="БАЗАЛИЕВ", 
                Firstname="Федор",
                Middlename="Александрович", 
                Birthday = DateTime.Parse("1923-01-21"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Призван Анапским РВК. Старший лейтенант, командир моторизованной роты 71-го танкового полка 33-й гвардейской механизированной дивизии. Погиб 26 октября 1956 г. Похоронен на кладбище советских воинов на северной окраине г. Тимишоары, Румыния."
            },
              new Participant
            {                
                Surname="БАКАРА", 
                Firstname="Илья",
                Middlename="Антонович", 
                Birthday = DateTime.Parse("1927-01-21"), 
                Deathday = DateTime.Parse("1946-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Погиб в 1946 г. Похоронен на братском кладбище в г. Порт-Артуре."
            }, 
             new Participant
            {                
                Surname="ВАРОСЯН", 
                Firstname="Арамоис",
                Middlename="Мовсесович", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Призван Степанаванским РВК. Рядовой, шофер, 42-й отдельный гвардейский саперный батальон 17-й гвардейской механизированной дивизии. Погиб 25 октября 1956 г. Похоронен на воинском кладбище в п. Хаймашкер, медье Веспрем."
            },      
                  new Participant
            {                
                Surname="ГАМЕНЮК", 
                Firstname="Алексей",
                Middlename="Антонович", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Призван Ухтинским РВК Коми АССР. Рядовой, наводчик пулемета, 106-й гвардейский механизированный полк 33-й гвардейской механизированной дивизии. Умер от ран 14 ноября 1956 г. Похоронен на кладбище Керепеши в г. Будапеште."
            },         new Participant
            {                
                Surname="Крамской", 
                Firstname="Борис",
                Middlename="Аркадьевич",
                Birthday = DateTime.Parse("1910-04-01"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = " Рядовой, сапер, 133-й гвардейский тяжелый танко-самоходный полк 33-й гвардейской механизированной дивизии. Погиб 25 октября 1956 г. Похоронен в г. Кечкемете."
            },
            new Participant
            {                
                Surname="ЗАБОЛОЦКИЙ", 
                Firstname="Владимир",
                Middlename="Несторович",
                Birthday = DateTime.Parse("1936-04-01"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Призван Юкаменским РВК Удмуртской АССР. Рядовой, орудийный номер, 100-й гвардейский артиллерийский полк 33-й гвардейской механизированной дивизии. Погиб 26 октября 1956 г. Похоронен на кладбище Керепеши в г. Будапеште."
            },  
             new Participant
            {                
                Surname="ИБАЕВ", 
                Firstname="Забит",
                Middlename="Ширин-оглы",
                Deathday = DateTime.Parse("1946-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Рядовой. Погиб в 1946 г. Похоронен на братском кладбище в г. Порт-Артуре."
            },  
                new Participant
            {                
                Surname="КАДИ", 
                Firstname="РОВ",
                Middlename="Музафар", 
                Birthday = DateTime.Parse("1923-04-01"), 
                Deathday = DateTime.Parse("1949-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Старший лейтенант. Погиб в 1949 г. Похоронен на братском кладбище в г. Порт-Артуре."
            },  
               new Participant
            {                
                Surname="ЛАВРИК", 
                Firstname="Николай",
                Middlename="Романович", 
                Birthday = DateTime.Parse("1934-01-01"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Призван Вурнарским РВК. Ефрейтор, заряжающий, 99-й отдельный гвардейский разведывательный батальон 2-й гвардейской механизированной дивизии. Погиб 4 ноября 1956 г. Похоронен на кладбище Керепеши в г. Будапеште."
            },  
              new Participant
            {                
                Surname="ЛАМНОВ", 
                Firstname="Алексей",
                Middlename="Васильевич", 
                Birthday = DateTime.Parse("1935-01-21"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Призван Кировабадским ГВК. Сержант, командир отделения, 108-й гвардейский парашютно-десантный полк 7-й гвардейской воздушно-десантной дивизии. Погиб 4 ноября 1956 г. Похоронен в братской могиле в п. Альшонемеди, предместье г. Будапешта."
            },    
              new Participant
            {                
                Surname="МАКУШЕВ", 
                Firstname="Виктор",
                Middlename="Николаевич", 
                Birthday = DateTime.Parse("1907-01-21"), 
                Deathday = DateTime.Parse("1946-12-01"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Сержант. Погиб в 1946 г. Похоронен на братском кладбище Циньюаньцзе в г. Даляне."
            },
              new Participant
            {                
                Surname="МАРКУЛОВ", 
                Firstname="Анатолий",
                Middlename="Александрович", 
                Birthday = DateTime.Parse("1923-01-21"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Призван Анапским РВК. Старший лейтенант, командир моторизованной роты 71-го танкового полка 33-й гвардейской механизированной дивизии. Погиб 26 октября 1956 г. Похоронен на кладбище советских воинов на северной окраине г. Тимишоары, Румыния."
            },    
              new Participant
            {                
                Surname="НАБОК", 
                Firstname="Илья",
                Middlename="Антонович", 
                Birthday = DateTime.Parse("1927-01-21"), 
                Deathday = DateTime.Parse("1946-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Погиб в 1946 г. Похоронен на братском кладбище в г. Порт-Артуре."
            },   
             new Participant
            {                
                Surname="Набоков", 
                Firstname="Арамоис",
                Middlename="Мовсесович", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Призван Степанаванским РВК. Рядовой, шофер, 42-й отдельный гвардейский саперный батальон 17-й гвардейской механизированной дивизии. Погиб 25 октября 1956 г. Похоронен на воинском кладбище в п. Хаймашкер, медье Веспрем."
            },      
                  new Participant
            {                
                Surname="Образцов", 
                Firstname="Алексей",
                Middlename="Антонович", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники ВОВ"),
                Description = "Призван Ухтинским РВК Коми АССР. Рядовой, наводчик пулемета, 106-й гвардейский механизированный полк 33-й гвардейской механизированной дивизии. Умер от ран 14 ноября 1956 г. Похоронен на кладбище Керепеши в г. Будапеште."
            },         





             new Participant
            {                
                Surname="Павленко", 
                Firstname="Борис",
                Middlename="Аркадьевич",
                Birthday = DateTime.Parse("1910-04-01"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Труженники тыла"),
                Description = " Рядовой, сапер, 133-й гвардейский тяжелый танко-самоходный полк 33-й гвардейской механизированной дивизии. Погиб 25 октября 1956 г. Похоронен в г. Кечкемете."
            },
            new Participant
            {                
                Surname="Павлов", 
                Firstname="Магафур",
                Middlename="Заретдинович",
                Birthday = DateTime.Parse("1936-04-01"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Труженники тыла"),
                Description = "Призван Юкаменским РВК Удмуртской АССР. Рядовой, орудийный номер, 100-й гвардейский артиллерийский полк 33-й гвардейской механизированной дивизии. Погиб 26 октября 1956 г. Похоронен на кладбище Керепеши в г. Будапеште."
            },
             new Participant
            {                
                Surname="Иванов", 
                Firstname="Анатолий",
                Middlename="Николаевич",
                Deathday = DateTime.Parse("1946-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Труженники тыла"),
                Description = "Рядовой. Погиб в 1946 г. Похоронен на братском кладбище в г. Порт-Артуре."
            },
                new Participant
            {                
                Surname="Такташев", 
                Firstname="Владимир",
                Middlename="Трофимович", 
                Birthday = DateTime.Parse("1923-04-01"), 
                Deathday = DateTime.Parse("1949-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Труженники тыла"),
                Description = "Старший лейтенант. Погиб в 1949 г. Похоронен на братском кладбище в г. Порт-Артуре."
            },
               new Participant
            {                
                Surname="Павлов", 
                Firstname="Алексей",
                Middlename="Петрович", 
                Birthday = DateTime.Parse("1934-01-01"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Труженники тыла"),
                Description = "Призван Вурнарским РВК. Ефрейтор, заряжающий, 99-й отдельный гвардейский разведывательный батальон 2-й гвардейской механизированной дивизии. Погиб 4 ноября 1956 г. Похоронен на кладбище Керепеши в г. Будапеште."
            },
              new Participant
            {                
                Surname="Петров", 
                Firstname="Овик",
                Middlename="Гурганович", 
                Birthday = DateTime.Parse("1935-01-21"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Труженники тыла"),
                Description = "Призван Кировабадским ГВК. Сержант, командир отделения, 108-й гвардейский парашютно-десантный полк 7-й гвардейской воздушно-десантной дивизии. Погиб 4 ноября 1956 г. Похоронен в братской могиле в п. Альшонемеди, предместье г. Будапешта."
            },  
              new Participant
            {                
                Surname="Краснобаев", 
                Firstname="Виктор",
                Middlename="Алексеевич", 
                Birthday = DateTime.Parse("1907-01-21"), 
                Deathday = DateTime.Parse("1946-12-01"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Труженники тыла"),
                Description = "Сержант. Погиб в 1946 г. Похоронен на братском кладбище Циньюаньцзе в г. Даляне."
            },
              new Participant
            {                
                Surname="Шувалов", 
                Firstname="Федор",
                Middlename="Александрович", 
                Birthday = DateTime.Parse("1923-01-21"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Труженники тыла"),
                Description = "Призван Анапским РВК. Старший лейтенант, командир моторизованной роты 71-го танкового полка 33-й гвардейской механизированной дивизии. Погиб 26 октября 1956 г. Похоронен на кладбище советских воинов на северной окраине г. Тимишоары, Румыния."
            },
              new Participant
            {                
                Surname="Ямщиков", 
                Firstname="Илья",
                Middlename="Антонович", 
                Birthday = DateTime.Parse("1927-01-21"), 
                Deathday = DateTime.Parse("1946-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Дети войны"),
                Description = "Погиб в 1946 г. Похоронен на братском кладбище в г. Порт-Артуре."
            }, 
             new Participant
            {                
                Surname="Терпугов", 
                Firstname="Арамоис",
                Middlename="Мовсесович", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Дети войны"),
                Description = "Призван Степанаванским РВК. Рядовой, шофер, 42-й отдельный гвардейский саперный батальон 17-й гвардейской механизированной дивизии. Погиб 25 октября 1956 г. Похоронен на воинском кладбище в п. Хаймашкер, медье Веспрем."
            },      
                  new Participant
            {                
                Surname="Ошеров", 
                Firstname="Алексей",
                Middlename="Антонович", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Дети войны"),
                Description = "Призван Ухтинским РВК Коми АССР. Рядовой, наводчик пулемета, 106-й гвардейский механизированный полк 33-й гвардейской механизированной дивизии. Умер от ран 14 ноября 1956 г. Похоронен на кладбище Керепеши в г. Будапеште."
            },         new Participant
            {                
                Surname="Нелюбин", 
                Firstname="Борис",
                Middlename="Аркадьевич",
                Birthday = DateTime.Parse("1910-04-01"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Дети войны"),
                Description = " Рядовой, сапер, 133-й гвардейский тяжелый танко-самоходный полк 33-й гвардейской механизированной дивизии. Погиб 25 октября 1956 г. Похоронен в г. Кечкемете."
            },
            new Participant
            {                
                Surname="Щавелев", 
                Firstname="Владимир",
                Middlename="Несторович",
                Birthday = DateTime.Parse("1936-04-01"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники горячих точек"),
                Description = "Призван Юкаменским РВК Удмуртской АССР. Рядовой, орудийный номер, 100-й гвардейский артиллерийский полк 33-й гвардейской механизированной дивизии. Погиб 26 октября 1956 г. Похоронен на кладбище Керепеши в г. Будапеште."
            },  
             new Participant
            {                
                Surname="Уваров", 
                Firstname="Забит",
                Middlename="Ширин-оглы",
                Deathday = DateTime.Parse("1946-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники горячих точек"),
                Description = "Рядовой. Погиб в 1946 г. Похоронен на братском кладбище в г. Порт-Артуре."
            },  
                new Participant
            {                
                Surname="Факеев", 
                Firstname="Иван",
                Middlename="Денисович", 
                Birthday = DateTime.Parse("1923-04-01"), 
                Deathday = DateTime.Parse("1949-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники горячих точек"),
                Description = "Старший лейтенант. Погиб в 1949 г. Похоронен на братском кладбище в г. Порт-Артуре."
            },  
               new Participant
            {                
                Surname="ЛАВРИК", 
                Firstname="Николай",
                Middlename="Романович", 
                Birthday = DateTime.Parse("1934-01-01"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники горячих точек"),
                Description = "Призван Вурнарским РВК. Ефрейтор, заряжающий, 99-й отдельный гвардейский разведывательный батальон 2-й гвардейской механизированной дивизии. Погиб 4 ноября 1956 г. Похоронен на кладбище Керепеши в г. Будапеште."
            },  
              new Participant
            {                
                Surname="Хабаров", 
                Firstname="Алексей",
                Middlename="Васильевич", 
                Birthday = DateTime.Parse("1935-01-21"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Участники горячих точек"),
                Description = "Призван Кировабадским ГВК. Сержант, командир отделения, 108-й гвардейский парашютно-десантный полк 7-й гвардейской воздушно-десантной дивизии. Погиб 4 ноября 1956 г. Похоронен в братской могиле в п. Альшонемеди, предместье г. Будапешта."
            },    
              new Participant
            {                
                Surname="Цаплин", 
                Firstname="Виктор",
                Middlename="Николаевич", 
                Birthday = DateTime.Parse("1907-01-21"), 
                Deathday = DateTime.Parse("1946-12-01"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Репрессированные"),
                Description = "Сержант. Погиб в 1946 г. Похоронен на братском кладбище Циньюаньцзе в г. Даляне."
            },
              new Participant
            {                
                Surname="Чабанов", 
                Firstname="Анатолий",
                Middlename="Александрович", 
                Birthday = DateTime.Parse("1923-01-21"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Репрессированные"),
                Description = "Призван Анапским РВК. Старший лейтенант, командир моторизованной роты 71-го танкового полка 33-й гвардейской механизированной дивизии. Погиб 26 октября 1956 г. Похоронен на кладбище советских воинов на северной окраине г. Тимишоары, Румыния."
            },    
              new Participant
            {                
                Surname="Черников", 
                Firstname="Илья",
                Middlename="Антонович", 
                Birthday = DateTime.Parse("1927-01-21"), 
                Deathday = DateTime.Parse("1946-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Репрессированные"),
                Description = "Погиб в 1946 г. Похоронен на братском кладбище в г. Порт-Артуре."
            },   
             new Participant
            {                
                Surname="Чижов", 
                Firstname="Арамоис",
                Middlename="Мовсесович", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Репрессированные"),
                Description = "Призван Степанаванским РВК. Рядовой, шофер, 42-й отдельный гвардейский саперный батальон 17-й гвардейской механизированной дивизии. Погиб 25 октября 1956 г. Похоронен на воинском кладбище в п. Хаймашкер, медье Веспрем."
            },      
                  new Participant
            {                
                Surname="Юзвик", 
                Firstname="Алексей",
                Middlename="Антонович", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="Репрессированные"),
                Description = "Призван Ухтинским РВК Коми АССР. Рядовой, наводчик пулемета, 106-й гвардейский механизированный полк 33-й гвардейской механизированной дивизии. Умер от ран 14 ноября 1956 г. Похоронен на кладбище Керепеши в г. Будапеште."
            }

            };

            p.ForEach(s => context.Participants.Add(s));
            context.SaveChanges();


            DirectoryInfo directory = new DirectoryInfo(HttpContext.Current.Server.MapPath("/Content/participantPhotos"));
            var photos = directory.GetFiles();

            var participants = context.Participants.ToArray();
            for (int i = 0; i < participants.Length; i++)
            {
                FileInfo fI = photos[i % photos.Length];
                long numBytes = fI.Length;
                FileStream fStream = new FileStream(fI.FullName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                byte[] data = br.ReadBytes((int)numBytes);

                participants[i].Photos = new List<Photo>();
                participants[i].Photos.Add(new Photo { PhotoBytes = data, Description = "", Extension = fI.Extension });
                context.Set<Participant>().Attach(participants[i]);
                context.SaveChanges();

            }



            DirectoryInfo docdirectory = new DirectoryInfo(HttpContext.Current.Server.MapPath("/Content/participantDocuments"));
            var docs = docdirectory.GetFiles();

            participants = context.Participants.ToArray();
            for (int i = 0; i < participants.Length; i++)
            {
                participants[i].Documents = new List<Document>();
                foreach(var file in docs)
                {
                    FileInfo fI = file;
                    long numBytes = fI.Length;
                    FileStream fStream = new FileStream(fI.FullName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fStream);
                    byte[] data = br.ReadBytes((int)numBytes);
                    string name = string.Empty;
                    if (fI.Extension.Length == 5) name = fI.Name.Substring(0, fI.Name.Length - 5);
                    if (fI.Extension.Length == 4) name = fI.Name.Substring(0, fI.Name.Length - 4);
                    participants[i].Documents.Add(new Document { DocumentBytes = data, Extension = fI.Extension, type = doctypes.Where(x => x.Name == name).FirstOrDefault() });
                }
                context.Set<Participant>().Attach(participants[i]);
                context.SaveChanges();

            }

        }
    }
}
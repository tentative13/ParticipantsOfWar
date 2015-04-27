namespace ParticipantsOfWar.Migrations
{
    using ParticipantsOfWar.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<ParticipantsOfWar.DAL.ArchiveContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ParticipantsOfWar.DAL.ArchiveContext context)
        {
            var types = new List<ParticipantType>
            {
                new ParticipantType{Name = "��������� ���", Priority = 1},
                new ParticipantType{Name = "���������� ����",  Priority = 2},   
                new ParticipantType{Name = "���� �����", Priority = 3},
                new ParticipantType{Name = "��������� ������� �����", Priority = 4},
                new ParticipantType{Name= "����������������", Priority = 5}
            };
            types.ForEach(s => context.ParticipantTypes.AddOrUpdate(s));
            context.SaveChanges();

            var doctypes = new List<DocumentType>
            {
                new DocumentType{Name = "���������"},
                new DocumentType{Name = "�������"},   
                new DocumentType{Name = "������� �����"},
                new DocumentType{Name = "�������"}
            };
            doctypes.ForEach(s => context.DocumentTypes.AddOrUpdate(s));
            context.SaveChanges();


            var p = new List<Participant>
            {
            new Participant
            {                
                Surname="��������", 
                Firstname="�����",
                Middlename="����������",
                Birthday = DateTime.Parse("1910-04-01"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type = context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = " �������, �����, 133-� ����������� ������� �����-���������� ���� 33-� ����������� ���������������� �������. ����� 25 ������� 1956 �. ��������� � �. ���������."
            },
            new Participant
            {                
                Surname="������", 
                Firstname="�������",
                Middlename="������������",
                Birthday = DateTime.Parse("1936-04-01"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� ���������� ��� ���������� ����. �������, ��������� �����, 100-� ����������� �������������� ���� 33-� ����������� ���������������� �������. ����� 26 ������� 1956 �. ��������� �� �������� �������� � �. ���������."
            },
             new Participant
            {                
                Surname="�������", 
                Firstname="��������",
                Middlename="����������",
                Deathday = DateTime.Parse("1946-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "�������. ����� � 1946 �. ��������� �� �������� �������� � �. ����-������."
            },
                new Participant
            {                
                Surname="�����", 
                Firstname="��������",
                Middlename="����������", 
                Birthday = DateTime.Parse("1923-04-01"), 
                Deathday = DateTime.Parse("1949-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� ���������. ����� � 1949 �. ��������� �� �������� �������� � �. ����-������."
            },
               new Participant
            {                
                Surname="������", 
                Firstname="�������",
                Middlename="��������", 
                Birthday = DateTime.Parse("1934-01-01"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� ���������� ���. ��������, ����������, 99-� ��������� ����������� ���������������� �������� 2-� ����������� ���������������� �������. ����� 4 ������ 1956 �. ��������� �� �������� �������� � �. ���������."
            },
              new Participant
            {                
                Surname="��������", 
                Firstname="����",
                Middlename="����������", 
                Birthday = DateTime.Parse("1935-01-21"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� ������������� ���. �������, �������� ���������, 108-� ����������� ���������-��������� ���� 7-� ����������� ��������-��������� �������. ����� 4 ������ 1956 �. ��������� � �������� ������ � �. �����������, ���������� �. ���������."
            },  
              new Participant
            {                
                Surname="�������", 
                Firstname="������",
                Middlename="����������", 
                Birthday = DateTime.Parse("1907-01-21"), 
                Deathday = DateTime.Parse("1946-12-01"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "�������. ����� � 1946 �. ��������� �� �������� �������� ����������� � �. ������."
            },
              new Participant
            {                
                Surname="��������", 
                Firstname="�����",
                Middlename="�������������", 
                Birthday = DateTime.Parse("1923-01-21"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� �������� ���. ������� ���������, �������� �������������� ���� 71-�� ��������� ����� 33-� ����������� ���������������� �������. ����� 26 ������� 1956 �. ��������� �� �������� ��������� ������ �� �������� ������� �. ���������, �������."
            },
              new Participant
            {                
                Surname="������", 
                Firstname="����",
                Middlename="���������", 
                Birthday = DateTime.Parse("1927-01-21"), 
                Deathday = DateTime.Parse("1946-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "����� � 1946 �. ��������� �� �������� �������� � �. ����-������."
            }, 
             new Participant
            {                
                Surname="�������", 
                Firstname="�������",
                Middlename="����������", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� �������������� ���. �������, �����, 42-� ��������� ����������� �������� �������� 17-� ����������� ���������������� �������. ����� 25 ������� 1956 �. ��������� �� �������� �������� � �. ���������, ����� �������."
            },      
                  new Participant
            {                
                Surname="�������", 
                Firstname="�������",
                Middlename="���������", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� ��������� ��� ���� ����. �������, �������� ��������, 106-� ����������� ���������������� ���� 33-� ����������� ���������������� �������. ���� �� ��� 14 ������ 1956 �. ��������� �� �������� �������� � �. ���������."
            },         new Participant
            {                
                Surname="��������", 
                Firstname="�����",
                Middlename="����������",
                Birthday = DateTime.Parse("1910-04-01"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = " �������, �����, 133-� ����������� ������� �����-���������� ���� 33-� ����������� ���������������� �������. ����� 25 ������� 1956 �. ��������� � �. ���������."
            },
            new Participant
            {                
                Surname="����������", 
                Firstname="��������",
                Middlename="����������",
                Birthday = DateTime.Parse("1936-04-01"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� ���������� ��� ���������� ����. �������, ��������� �����, 100-� ����������� �������������� ���� 33-� ����������� ���������������� �������. ����� 26 ������� 1956 �. ��������� �� �������� �������� � �. ���������."
            },  
             new Participant
            {                
                Surname="�����", 
                Firstname="�����",
                Middlename="�����-����",
                Deathday = DateTime.Parse("1946-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "�������. ����� � 1946 �. ��������� �� �������� �������� � �. ����-������."
            },  
                new Participant
            {                
                Surname="����", 
                Firstname="���",
                Middlename="�������", 
                Birthday = DateTime.Parse("1923-04-01"), 
                Deathday = DateTime.Parse("1949-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� ���������. ����� � 1949 �. ��������� �� �������� �������� � �. ����-������."
            },  
               new Participant
            {                
                Surname="������", 
                Firstname="�������",
                Middlename="���������", 
                Birthday = DateTime.Parse("1934-01-01"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� ���������� ���. ��������, ����������, 99-� ��������� ����������� ���������������� �������� 2-� ����������� ���������������� �������. ����� 4 ������ 1956 �. ��������� �� �������� �������� � �. ���������."
            },  
              new Participant
            {                
                Surname="������", 
                Firstname="�������",
                Middlename="����������", 
                Birthday = DateTime.Parse("1935-01-21"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� ������������� ���. �������, �������� ���������, 108-� ����������� ���������-��������� ���� 7-� ����������� ��������-��������� �������. ����� 4 ������ 1956 �. ��������� � �������� ������ � �. �����������, ���������� �. ���������."
            },    
              new Participant
            {                
                Surname="�������", 
                Firstname="������",
                Middlename="����������", 
                Birthday = DateTime.Parse("1907-01-21"), 
                Deathday = DateTime.Parse("1946-12-01"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "�������. ����� � 1946 �. ��������� �� �������� �������� ����������� � �. ������."
            },
              new Participant
            {                
                Surname="��������", 
                Firstname="��������",
                Middlename="�������������", 
                Birthday = DateTime.Parse("1923-01-21"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� �������� ���. ������� ���������, �������� �������������� ���� 71-�� ��������� ����� 33-� ����������� ���������������� �������. ����� 26 ������� 1956 �. ��������� �� �������� ��������� ������ �� �������� ������� �. ���������, �������."
            },    
              new Participant
            {                
                Surname="�����", 
                Firstname="����",
                Middlename="���������", 
                Birthday = DateTime.Parse("1927-01-21"), 
                Deathday = DateTime.Parse("1946-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "����� � 1946 �. ��������� �� �������� �������� � �. ����-������."
            },   
             new Participant
            {                
                Surname="�������", 
                Firstname="�������",
                Middlename="����������", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� �������������� ���. �������, �����, 42-� ��������� ����������� �������� �������� 17-� ����������� ���������������� �������. ����� 25 ������� 1956 �. ��������� �� �������� �������� � �. ���������, ����� �������."
            },      
                  new Participant
            {                
                Surname="��������", 
                Firstname="�������",
                Middlename="���������", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ���"),
                Description = "������� ��������� ��� ���� ����. �������, �������� ��������, 106-� ����������� ���������������� ���� 33-� ����������� ���������������� �������. ���� �� ��� 14 ������ 1956 �. ��������� �� �������� �������� � �. ���������."
            },         
             new Participant
            {                
                Surname="��������", 
                Firstname="�����",
                Middlename="����������",
                Birthday = DateTime.Parse("1910-04-01"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="���������� ����"),
                Description = " �������, �����, 133-� ����������� ������� �����-���������� ���� 33-� ����������� ���������������� �������. ����� 25 ������� 1956 �. ��������� � �. ���������."
            },
            new Participant
            {                
                Surname="������", 
                Firstname="�������",
                Middlename="������������",
                Birthday = DateTime.Parse("1936-04-01"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="���������� ����"),
                Description = "������� ���������� ��� ���������� ����. �������, ��������� �����, 100-� ����������� �������������� ���� 33-� ����������� ���������������� �������. ����� 26 ������� 1956 �. ��������� �� �������� �������� � �. ���������."
            },
             new Participant
            {                
                Surname="������", 
                Firstname="��������",
                Middlename="����������",
                Deathday = DateTime.Parse("1946-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="���������� ����"),
                Description = "�������. ����� � 1946 �. ��������� �� �������� �������� � �. ����-������."
            },
                new Participant
            {                
                Surname="��������", 
                Firstname="��������",
                Middlename="����������", 
                Birthday = DateTime.Parse("1923-04-01"), 
                Deathday = DateTime.Parse("1949-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="���������� ����"),
                Description = "������� ���������. ����� � 1949 �. ��������� �� �������� �������� � �. ����-������."
            },
               new Participant
            {                
                Surname="������", 
                Firstname="�������",
                Middlename="��������", 
                Birthday = DateTime.Parse("1934-01-01"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="���������� ����"),
                Description = "������� ���������� ���. ��������, ����������, 99-� ��������� ����������� ���������������� �������� 2-� ����������� ���������������� �������. ����� 4 ������ 1956 �. ��������� �� �������� �������� � �. ���������."
            },
              new Participant
            {                
                Surname="������", 
                Firstname="����",
                Middlename="����������", 
                Birthday = DateTime.Parse("1935-01-21"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="���������� ����"),
                Description = "������� ������������� ���. �������, �������� ���������, 108-� ����������� ���������-��������� ���� 7-� ����������� ��������-��������� �������. ����� 4 ������ 1956 �. ��������� � �������� ������ � �. �����������, ���������� �. ���������."
            },  
              new Participant
            {                
                Surname="����������", 
                Firstname="������",
                Middlename="����������", 
                Birthday = DateTime.Parse("1907-01-21"), 
                Deathday = DateTime.Parse("1946-12-01"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="���������� ����"),
                Description = "�������. ����� � 1946 �. ��������� �� �������� �������� ����������� � �. ������."
            },
              new Participant
            {                
                Surname="�������", 
                Firstname="�����",
                Middlename="�������������", 
                Birthday = DateTime.Parse("1923-01-21"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="���������� ����"),
                Description = "������� �������� ���. ������� ���������, �������� �������������� ���� 71-�� ��������� ����� 33-� ����������� ���������������� �������. ����� 26 ������� 1956 �. ��������� �� �������� ��������� ������ �� �������� ������� �. ���������, �������."
            },
              new Participant
            {                
                Surname="�������", 
                Firstname="����",
                Middlename="���������", 
                Birthday = DateTime.Parse("1927-01-21"), 
                Deathday = DateTime.Parse("1946-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="���� �����"),
                Description = "����� � 1946 �. ��������� �� �������� �������� � �. ����-������."
            }, 
             new Participant
            {                
                Surname="��������", 
                Firstname="�������",
                Middlename="����������", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="���� �����"),
                Description = "������� �������������� ���. �������, �����, 42-� ��������� ����������� �������� �������� 17-� ����������� ���������������� �������. ����� 25 ������� 1956 �. ��������� �� �������� �������� � �. ���������, ����� �������."
            },      
                  new Participant
            {                
                Surname="������", 
                Firstname="�������",
                Middlename="���������", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="���� �����"),
                Description = "������� ��������� ��� ���� ����. �������, �������� ��������, 106-� ����������� ���������������� ���� 33-� ����������� ���������������� �������. ���� �� ��� 14 ������ 1956 �. ��������� �� �������� �������� � �. ���������."
            },         new Participant
            {                
                Surname="�������", 
                Firstname="�����",
                Middlename="����������",
                Birthday = DateTime.Parse("1910-04-01"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="���� �����"),
                Description = " �������, �����, 133-� ����������� ������� �����-���������� ���� 33-� ����������� ���������������� �������. ����� 25 ������� 1956 �. ��������� � �. ���������."
            },
            new Participant
            {                
                Surname="�������", 
                Firstname="��������",
                Middlename="����������",
                Birthday = DateTime.Parse("1936-04-01"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ������� �����"),
                Description = "������� ���������� ��� ���������� ����. �������, ��������� �����, 100-� ����������� �������������� ���� 33-� ����������� ���������������� �������. ����� 26 ������� 1956 �. ��������� �� �������� �������� � �. ���������."
            },  
             new Participant
            {                
                Surname="������", 
                Firstname="�����",
                Middlename="�����-����",
                Deathday = DateTime.Parse("1946-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ������� �����"),
                Description = "�������. ����� � 1946 �. ��������� �� �������� �������� � �. ����-������."
            },  
                new Participant
            {                
                Surname="������", 
                Firstname="����",
                Middlename="���������", 
                Birthday = DateTime.Parse("1923-04-01"), 
                Deathday = DateTime.Parse("1949-11-12"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ������� �����"),
                Description = "������� ���������. ����� � 1949 �. ��������� �� �������� �������� � �. ����-������."
            },  
               new Participant
            {                
                Surname="������", 
                Firstname="�������",
                Middlename="���������", 
                Birthday = DateTime.Parse("1934-01-01"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ������� �����"),
                Description = "������� ���������� ���. ��������, ����������, 99-� ��������� ����������� ���������������� �������� 2-� ����������� ���������������� �������. ����� 4 ������ 1956 �. ��������� �� �������� �������� � �. ���������."
            },  
              new Participant
            {                
                Surname="�������", 
                Firstname="�������",
                Middlename="����������", 
                Birthday = DateTime.Parse("1935-01-21"), 
                Deathday = DateTime.Parse("1956-11-04"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="��������� ������� �����"),
                Description = "������� ������������� ���. �������, �������� ���������, 108-� ����������� ���������-��������� ���� 7-� ����������� ��������-��������� �������. ����� 4 ������ 1956 �. ��������� � �������� ������ � �. �����������, ���������� �. ���������."
            },    
              new Participant
            {                
                Surname="������", 
                Firstname="������",
                Middlename="����������", 
                Birthday = DateTime.Parse("1907-01-21"), 
                Deathday = DateTime.Parse("1946-12-01"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="����������������"),
                Description = "�������. ����� � 1946 �. ��������� �� �������� �������� ����������� � �. ������."
            },
              new Participant
            {                
                Surname="�������", 
                Firstname="��������",
                Middlename="�������������", 
                Birthday = DateTime.Parse("1923-01-21"), 
                Deathday = DateTime.Parse("1956-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="����������������"),
                Description = "������� �������� ���. ������� ���������, �������� �������������� ���� 71-�� ��������� ����� 33-� ����������� ���������������� �������. ����� 26 ������� 1956 �. ��������� �� �������� ��������� ������ �� �������� ������� �. ���������, �������."
            },    
              new Participant
            {                
                Surname="��������", 
                Firstname="����",
                Middlename="���������", 
                Birthday = DateTime.Parse("1927-01-21"), 
                Deathday = DateTime.Parse("1946-10-26"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="����������������"),
                Description = "����� � 1946 �. ��������� �� �������� �������� � �. ����-������."
            },   
             new Participant
            {                
                Surname="�����", 
                Firstname="�������",
                Middlename="����������", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="����������������"),
                Description = "������� �������������� ���. �������, �����, 42-� ��������� ����������� �������� �������� 17-� ����������� ���������������� �������. ����� 25 ������� 1956 �. ��������� �� �������� �������� � �. ���������, ����� �������."
            },      
                  new Participant
            {                
                Surname="�����", 
                Firstname="�������",
                Middlename="���������", 
                Birthday = DateTime.Parse("1934-01-21"), 
                Deathday = DateTime.Parse("1956-10-25"),
                type =  context.ParticipantTypes.FirstOrDefault(x=>x.Name=="����������������"),
                Description = "������� ��������� ��� ���� ����. �������, �������� ��������, 106-� ����������� ���������������� ���� 33-� ����������� ���������������� �������. ���� �� ��� 14 ������ 1956 �. ��������� �� �������� �������� � �. ���������."
            }

            };

            p.ForEach(s => context.Participants.AddOrUpdate(s));
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
                participants[i].Photos.Add(new Photo { PhotoBytes = data, Description = "����������", Extension = fI.Extension });
                context.Set<Participant>().Attach(participants[i]);
                context.SaveChanges();

            }

            DirectoryInfo docdirectory = new DirectoryInfo(HttpContext.Current.Server.MapPath("/Content/participantDocuments"));
            var docs = docdirectory.GetFiles();

            participants = context.Participants.ToArray();
            for (int i = 0; i < participants.Length; i++)
            {
                participants[i].Documents = new List<Document>();
                foreach (var file in docs)
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

using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class MemberRepository : IRepository<Member>
    {
        private static readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "members.txt");
        private const int ID_LENGTH = 5;
        private const int FULLNAME_LENGTH = 30;
        private const int EMAIL_LENGTH = 35;
        private const int PHONE_LENGTH = 15;
        private const int DATE_LENGTH = 10;  
        private const int IS_ACTIVE_LENGTH = 1;

        public MemberRepository()
        {
            
            string directory = Path.GetDirectoryName(_path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            
            if (!File.Exists(_path))
            {
                File.Create(_path).Close();
            }
        }
        public void Add(Member entity)
        {
            var members = GetAll();
            

            entity.Id = members.Any() ? members.Max(m => m.Id) + 1 : 1;
            if(entity.MembershipDate == default(DateTime))

            {
                entity.MembershipDate = DateTime.Now;
            }
            entity.IsActive = true;

            string line = ConvertLine(entity);
            File.AppendAllText(_path, line + Environment.NewLine);
        }

        public void Delete(int id)
        {
            var members = GetAll();
            int removeCount =members.RemoveAll(m => m.Id == id);
            if(removeCount == 0)
            {
                throw new Exception("Üzv tapılmadı!");
            }
            SaveAll(members);
        }

        public List<Member> GetAll()
        {
            var members = new List<Member>();
            if(!File.Exists(_path)) return members;

            foreach(var line in File.ReadAllLines(_path))
            {
                if(!string.IsNullOrWhiteSpace(line))
                {
                    members.Add(ParseFromLine(line));
                }
            }
            return members;
        }

        public Member GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public List<Member> Search(string keyword)
        {
            var members = GetAll();

            if (string.IsNullOrWhiteSpace(keyword))
                return members;

            keyword = keyword.ToLower();

            return members.Where(x =>
                x.FullName.ToLower().Contains(keyword) ||
                x.Email.ToLower().Contains(keyword) ||
                x.PhoneNumber.Contains(keyword)
            ).ToList();
        }

        public void Uptade(Member entity)
        {
            var members = GetAll();
            var index = members.FindIndex(x => x.Id == entity.Id);
            if(index == -1)
            {
                throw new Exception("Üzv tapılmadı!");
            }

            //if( members.Any(m =>m.Id != entity.Id && m.Email.ToLower() == entity.Email.ToLower()) )
            //{
            //    throw new Exception($"Bu email artıq istifadə olunur: {entity.Email}");
            //}
            //if (members.Any(m => m.Id != entity.Id && m.PhoneNumber == entity.PhoneNumber))
            //{
            //    throw new Exception($"Bu telefon nömrəsi artıq istifadə olunur: {entity.PhoneNumber}");
            //}
            members[index] = entity;
            SaveAll(members);
        }

        private string ConvertLine(Member member)
        {
            string id = member.Id.ToString().PadLeft(ID_LENGTH, '0');
            string fullName = member.FullName.PadRight(FULLNAME_LENGTH);
            string email = member.Email.PadRight(EMAIL_LENGTH);
            string phone = member.PhoneNumber.PadRight(PHONE_LENGTH);
            string date = member.MembershipDate.ToString("yyyy-MM-dd"); 
            string isActive = member.IsActive ? "1" : "0";

            return id + fullName + email + phone + date + isActive;
        }

        private Member ParseFromLine(string line)
        {
            if (line.Length < 96)
            {
                throw new Exception("Fayl formatı düzgün deyil!");
            }

            int pos = 0;
            Member member = new Member();

            
            member.Id = int.Parse(line.Substring(pos, ID_LENGTH).Trim());
            pos += ID_LENGTH;

            
            member.FullName = line.Substring(pos, FULLNAME_LENGTH).Trim();
            pos += FULLNAME_LENGTH;

            
            member.Email = line.Substring(pos, EMAIL_LENGTH).Trim();
            pos += EMAIL_LENGTH;

            
            member.PhoneNumber = line.Substring(pos, PHONE_LENGTH).Trim();
            pos += PHONE_LENGTH;

           
            string dateStr = line.Substring(pos, DATE_LENGTH).Trim();
            member.MembershipDate = DateTime.ParseExact(dateStr, "yyyy-MM-dd", null);
            pos += DATE_LENGTH;

            
            member.IsActive = line.Substring(pos, IS_ACTIVE_LENGTH) == "1";

            return member;
        }

        private void SaveAll(List<Member> members)
        {
            var lines = members.Select(m => ConvertLine(m));
            File.WriteAllLines(_path, lines);
        }
    }
}


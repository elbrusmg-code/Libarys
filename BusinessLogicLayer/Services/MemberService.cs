using BusinessLogicLayer.Services.Contracts;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer.Services
{
    public class MemberService : IMemberService
    {
        private readonly IRepository<Member> _memberRepository;

        public MemberService(IRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public void Add(Member member)
        {
            ValidateMember(member);
            var members = _memberRepository.GetAll();
            if (members.Any( m => m.Email.ToLower() == member.Email.ToLower()))
            {
                throw new Exception($"Bu email artıq mövcuddur: {member.Email}");
            }
            if(members.Any( m => m.PhoneNumber == member.PhoneNumber))
            {
                throw new Exception($"Bu telefon nömrəsi artıq mövcuddur: {member.PhoneNumber}");
            }
            if(member.MembershipDate == default(DateTime))
            {
                member.MembershipDate = DateTime.Now;
            }
            member.IsActive = true;
            _memberRepository.Add(member);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new Exception("ID müsbət olmalıdır!");
            }
            var member = _memberRepository.GetById(id);
            if (member == null)
            {
                throw new Exception($"Üzv tapılmadı! ID: {id}");
            }
            _memberRepository.Delete(id);
        }

        public List<Member> GetAll()
        {
            return _memberRepository.GetAll();
        }

        public Member GetById(int id)
        {
            if(id <= 0)
            {
                throw new Exception("ID müsbət olmalıdır!");
            }
            var member = _memberRepository.GetById(id);
            if (member == null)
            {
                throw new Exception($"Üzv tapılmadı! ID: {id}");
            }
            return member;
        }

        public List<Member> Search(string keyword)
        {
            if(string.IsNullOrWhiteSpace(keyword))
            {
                return GetAll();
            }
            return _memberRepository.Search(keyword);
        }

        public void Update(Member member)
        {
            if (member.Id <= 0)
            {
                throw new Exception("ID müsbət olmalıdır!");
            }

            var existingMember = _memberRepository.GetById(member.Id);
            if (existingMember == null)
            {
                throw new Exception($"Üzv tapılmadı! ID: {member.Id}");
            }

            ValidateMember(member);

            var existingMembers = _memberRepository.GetAll();
            if (existingMembers.Any(m => m.Id != member.Id && m.Email.ToLower() == member.Email.ToLower()))
            {
                throw new Exception($"Bu email artıq istifadə olunur: {member.Email}");
            }

            if (existingMembers.Any(m => m.Id != member.Id && m.PhoneNumber == member.PhoneNumber))
            {
                throw new Exception($"Bu telefon nömrəsi artıq istifadə olunur: {member.PhoneNumber}");
            }

            _memberRepository.Uptade(member);
        }
        private void ValidateMember(Member member)
        {
            if (string.IsNullOrWhiteSpace(member.FullName))
            {
                throw new Exception("Ad və soyad boş ola bilməz!");
            }
            if (member.FullName.Length > 30)
            {
                throw new Exception("Ad və soyad maksimum 30 simvol ola bilər!");
            }

            if (string.IsNullOrWhiteSpace(member.Email))
            {
                throw new Exception("Email boş ola bilməz!");
            }
            if (member.Email.Length > 35)
            {
                throw new Exception("Email maksimum 35 simvol ola bilər!");
            }
            if (!IsValidEmail(member.Email))
            {
                throw new Exception("Email formatı düzgün deyil!");
            }

            if (string.IsNullOrWhiteSpace(member.PhoneNumber))
            {
                throw new Exception("Telefon nömrəsi boş ola bilməz!");
            }
            if (member.PhoneNumber.Length > 15)
            {
                throw new Exception("Telefon nömrəsi maksimum 15 simvol ola bilər!");
            }
            if (!IsValidPhoneNumber(member.PhoneNumber))
            {
                throw new Exception("Telefon nömrəsi formatı düzgün deyil! (+994XXXXXXXXX)");
            }

            if (member.MembershipDate > DateTime.Now)
            {
                throw new Exception("Üzvlük tarixi gələcək ola bilməz!");
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if(!email.EndsWith("@gmail.com"))
            {
                return false;
            }
            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPhoneNumber(string phone)
        {
            string pattern = @"^\+994[0-9]{9}$";
            return Regex.IsMatch(phone, pattern);
        }
    }
}

using BusinessLogicLayer.Dtos;
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
        public void Add(MemberCreateDto member)
        {
            var members = new Member
            {
                FullName = member.FullName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                IsActive= true
            };
            ValidateMember(members);
            _memberRepository.Add(members);
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

        public void Update(MemberUpdateDto member)
        {
            if (member.Id <= 0)
                throw new Exception("ID müsbət olmalıdır!");

            var members = _memberRepository.GetById(member.Id);
            if (member == null)
                throw new Exception($"Üzv tapılmadı! ID: {member.Id}");

            ValidateMember(members);

            member.FullName = member.FullName;
            member.Email = member.Email;
            member.PhoneNumber = member.PhoneNumber;
            member.IsActive = member.IsActive;

            _memberRepository.Uptade(members);
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

using BusinessLogicLayer.Dtos;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services.Contracts
{
    public interface IMemberService
    {
        void Add(MemberCreateDto member);
        void Update(MemberUpdateDto member);
        void Delete(int id);
        Member GetById(int id);
        List<Member> GetAll();
        List<Member> Search(string keyword);
    }
}

using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services.Contracts
{
    public interface IMemberService
    {
        void Add(Member member);
        void Update(Member member);
        void Delete(int id);
        Member GetById(int id);
        List<Member> GetAll();
        List<Member> Search(string keyword);
    }
}

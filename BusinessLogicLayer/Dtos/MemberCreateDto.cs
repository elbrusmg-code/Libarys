using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Dtos
{
    public class MemberCreateDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class MemberUpdateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}

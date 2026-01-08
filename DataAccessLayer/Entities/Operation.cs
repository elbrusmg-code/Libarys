using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Operation : Entity
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }

        public string BookTitle { get; set; }
        public string MemberName { get; set; }

        public override string ToString()
        {
            string returnInfo = IsReturned
                ? ReturnDate?.ToString("yyyy-MM-dd") ?? "N/A"
                : "Hələ qaytarılmayıb";

            return $"{Id,5}|{MemberId,5}|{MemberName,-30}|{BookId,5}|{BookTitle,-30}|{BorrowDate:yyyy-MM-dd}|{returnInfo,-15}|{(IsReturned ? "Bəli" : "Xeyr"),5}";
        }
    }
}

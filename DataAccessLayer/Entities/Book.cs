using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Book :Entity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublishedYear { get; set; }
        public int CategoryId { get; set; }
        public int? MemberId { get; set; }
        public bool IsAvailable { get; set; }

        public override string ToString()
        {
            return $"{Id,5}|{Title,-30}|{Author,-25}|{ISBN,-13}|{PublishedYear,4}|{CategoryId,5}|{MemberId?.ToString() ?? "0",5}|{(IsAvailable ? "1" : "0"),1}";
        }
    }
}

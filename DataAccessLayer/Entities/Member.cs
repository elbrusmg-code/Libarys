namespace DataAccessLayer.Entities
{
    public class Member : Entity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime MembershipDate { get; set; }

        public bool IsActive { get; set; }
        public override string ToString()
        {
            return $"{Id,5}|{FullName,-30}|{Email,-35}|{PhoneNumber,-15}|{MembershipDate:yyyy-MM-dd}|{(IsActive ? "1" : "0"),1}";
        }
    }
}

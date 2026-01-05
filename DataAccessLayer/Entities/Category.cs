namespace DataAccessLayer.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }


        public override string ToString()
        {
            return $"{Id,5}|{Name,-30}|{Description,-50}";
        }

    }
}

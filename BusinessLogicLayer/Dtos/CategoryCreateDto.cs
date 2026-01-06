using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Dtos
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

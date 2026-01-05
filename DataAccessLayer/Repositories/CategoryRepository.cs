using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {

        private const string _path = @"C:\\Users\\User\\OneDrive\\Documentos\\codeacademy\\LibarySystemManag\\Data\\categories.txt";

        private const int ID_LENGTH = 5;
        private const int NAME_LENGTH = 30;
        private const int  DESCRIPTION_LENGTH = 50; 


        public CategoryRepository()
        {
            string directory = Path.GetDirectoryName(_path);
            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if(!File.Exists(_path))
            {
                File.Create(_path).Close();
            }
        }
        public void Add(Category entity)
        {
            var categories = GetAll();
            entity.Id = categories.Any() ? categories.Max(x => x.Id) + 1 : 1;
            string line = ConvertLine(entity);
            File.AppendAllText(_path, line + Environment.NewLine);
        }

        public void Delete(int id)
        {
            var categories = GetAll();
            int removeCount = categories.RemoveAll(x => x.Id == id);
            if(removeCount == 0)
            {
                throw new Exception("Kateqoriya tapılmadı!");
            }
            SaveAll(categories);
        }

        public List<Category> GetAll()
        {
            var categories = new List<Category>();
            if (!File.Exists(_path))
                return categories;

            foreach (var line in File.ReadAllLines(_path))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    categories.Add(ParseFromLine(line));
                }
            }
            return categories;
        }

        public Category GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public List<Category> Search(string keyword)
        {
            var categories = GetAll();
            if (string.IsNullOrWhiteSpace(keyword))
                return categories;

            keyword = keyword.ToLower();


            return categories.Where(x => 
            x.Name.ToLower().Contains(keyword) || 
            x.Description.ToLower().Contains(keyword)).ToList();

        }

        public void Uptade(Category entity)
        {
            var categories = GetAll();
            var index = categories.FindIndex(x => x.Id == entity.Id);

            if (index == -1)
            {
                throw new Exception("Kateqoriya tapılmadı!");
            }

            categories[index] = entity;
            SaveAll(categories);
        }

        private string ConvertLine(Category category)
        {
            string id = category.Id.ToString().PadLeft(ID_LENGTH, '0');
            string name = category.Name.PadRight(NAME_LENGTH);
            string desc = category.Description.PadRight(DESCRIPTION_LENGTH);
            return id + name + desc;
        }

        private Category ParseFromLine(string line )
        {
            if(line.Length < ID_LENGTH + NAME_LENGTH + DESCRIPTION_LENGTH)
            {
                throw new Exception("Fayl formatı düzgün deyil!");
            }

            int pos = 0;
            Category category = new Category();

            category.Id = int.Parse(line.Substring(pos, ID_LENGTH).Trim());
            pos += ID_LENGTH;
            category.Name = line.Substring(pos, NAME_LENGTH).Trim();
            pos += NAME_LENGTH;
            category.Description = line.Substring(pos, DESCRIPTION_LENGTH).Trim();
            pos += DESCRIPTION_LENGTH;

            return category;
        }

        private void SaveAll(List<Category> categories )
        {
            var line = categories.Select(c => ConvertLine(c));
            File.WriteAllLines(_path, line);
        }
    }
}

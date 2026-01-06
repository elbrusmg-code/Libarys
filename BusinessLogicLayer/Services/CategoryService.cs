using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Services.Contracts;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Add(CategoryCreateDto category)
        {
            var categorys = new Category
            {
                Name = category.Name,
                Description = category.Description,
            };
            ValidateCategory(categorys);
            _categoryRepository.Add(categorys);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new Exception("ID müsbət olmalıdır!");
            }
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                throw new Exception($"Kateqoriya tapılmadı! ID: {id}");
            }
            _categoryRepository.Delete(id);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception($"Kateqoriya tapılmadı! ID: {id}");
            }
            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                throw new Exception($"Kateqoriya tapılmadı! ID: {id}");
            }
            return category;
        }

        public List<Category> Search(string keyword)
        {
           if(string.IsNullOrWhiteSpace(keyword))
            {
                return GetAll();
            }
           return _categoryRepository.Search(keyword);
        }

        public void Update(CategoryUpdateDto category)
        {
            var existingCategorys = _categoryRepository.GetAll();
            if (category.Id <= 0)
            {
                throw new Exception("ID müsbət olmalıdır!");
            }
            var categorys =  _categoryRepository.GetById(category.Id);
            if (category == null)
            {
                throw new Exception($"Kateqoriya tapılmadı! ID: {category.Id}");
            }
            if(existingCategorys.Any( c => c.Id != category.Id && c.Name.ToLower() == category.Name.ToLower() ))
            {
                throw new Exception($"Bu kateqoriya adı artıq istifadə olunur: {category.Name}");
            }
            categorys.Name = category.Name;
            categorys.Description = category.Description;
            _categoryRepository.Uptade(categorys);
        }

        private void ValidateCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new Exception("Kateqoriya adı boş ola bilməz!");
            }
            if (category.Name.Length > 30)
            {
                throw new Exception("Kateqoriya adı maksimum 30 simvol ola bilər!");
            }

            if (string.IsNullOrWhiteSpace(category.Description))
            {
                throw new Exception("Təsvir boş ola bilməz!");
            }
            if (category.Description.Length > 50)
            {
                throw new Exception("Təsvir maksimum 50 simvol ola bilər!");
            }
        }
    }
}


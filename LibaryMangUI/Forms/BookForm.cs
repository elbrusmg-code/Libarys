using BusinessLogicLayer.Services.Contracts;

namespace LibaryMangUI.Forms
{
    internal class BookForm
    {
        private IBookService bookService;
        private ICategoryService categoryService;

        public BookForm(IBookService bookService, ICategoryService categoryService)
        {
            this.bookService = bookService;
            this.categoryService = categoryService;
        }

        internal void ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}
//using BusinessLogicLayer.Services;
//using BusinessLogicLayer.Services.Contracts;
//using DataAccessLayer.Entities;
//using DataAccessLayer.Repositories;

//namespace ManagmentUI
//{
//    class Program
//    {
//        // Services
//        private static IBookService _bookService;
//        private static ICategoryService _categoryService;
//        private static IMemberService _memberService;

//        static void Main(string[] args)
//        {
//            // Services yaradırıq
//            InitializeServices();

//            Console.OutputEncoding = System.Text.Encoding.UTF8;

//            while (true)
//            {
//                ShowMainMenu();
//                string choice = Console.ReadLine();

//                switch (choice)
//                {
//                    case "1":
//                        BookMenu();
//                        break;
//                    case "2":
//                        CategoryMenu();
//                        break;
//                    case "3":
//                        MemberMenu();
//                        break;
//                    case "0":
//                        Console.WriteLine("\n👋 Proqramdan çıxılır...");
//                        return;
//                    default:
//                        Console.WriteLine("\n❌ Yanlış seçim! Yenidən cəhd edin.");
//                        break;
//                }

//                Console.WriteLine("\nDavam etmək üçün bir düyməyə basın...");
//                Console.ReadKey();
//            }
//        }

//        // ============================================
//        // SERVİCE İNİTİALİZE
//        // ============================================
//        static void InitializeServices()
//        {
//            var bookRepository = new BookRepository();
//            var categoryRepository = new CategoryRepository();
//            var memberRepository = new MemberRepository();

//            _bookService = new BookService(bookRepository, categoryRepository);
//            _categoryService = new CategoryService(categoryRepository);
//            _memberService = new MemberService(memberRepository);
//        }

//        // ============================================
//        // ANA MENYU
//        // ============================================
//        static void ShowMainMenu()
//        {
//            Console.Clear();
//            Console.WriteLine("╔════════════════════════════════════════╗");
//            Console.WriteLine("║   KİTABXANA İDARƏETMƏ SİSTEMİ         ║");
//            Console.WriteLine("╚════════════════════════════════════════╝");
//            Console.WriteLine();
//            Console.WriteLine("  1. 📚 Kitab İdarəetməsi");
//            Console.WriteLine("  2. 📁 Kateqoriya İdarəetməsi");
//            Console.WriteLine("  3. 👥 Üzv İdarəetməsi");
//            Console.WriteLine("  0. 🚪 Çıxış");
//            Console.WriteLine();
//            Console.Write("Seçiminiz: ");
//        }

//        // ============================================
//        // KİTAB MENYU
//        // ============================================
//        static void BookMenu()
//        {
//            while (true)
//            {
//                Console.Clear();
//                Console.WriteLine("╔════════════════════════════════════════╗");
//                Console.WriteLine("║         KİTAB İDARƏETMƏSİ             ║");
//                Console.WriteLine("╚════════════════════════════════════════╝");
//                Console.WriteLine();
//                Console.WriteLine("  1. ➕ Yeni Kitab Əlavə Et");
//                Console.WriteLine("  2. 📋 Bütün Kitabları Göstər");
//                Console.WriteLine("  3. 🔍 Kitab Axtar");
//                Console.WriteLine("  4. ✏️  Kitab Yenilə");
//                Console.WriteLine("  5. 🗑️  Kitab Sil");
//                Console.WriteLine("  0. ⬅️  Geri");
//                Console.WriteLine();
//                Console.Write("Seçiminiz: ");

//                string choice = Console.ReadLine();

//                switch (choice)
//                {
//                    case "1":
//                        AddBook();
//                        break;
//                    case "2":
//                        ShowAllBooks();
//                        break;
//                    case "3":
//                        SearchBooks();
//                        break;
//                    case "4":
//                        UpdateBook();
//                        break;
//                    case "5":
//                        DeleteBook();
//                        break;
//                    case "0":
//                        return;
//                    default:
//                        Console.WriteLine("\n❌ Yanlış seçim!");
//                        break;
//                }

//                if (choice != "0")
//                {
//                    Console.WriteLine("\nDavam etmək üçün bir düyməyə basın...");
//                    Console.ReadKey();
//                }
//            }
//        }

//        static void AddBook()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ YENİ KİTAB ƏLAVƏ ET ═══\n");

//            try
//            {
//                Console.Write("Kitab adı: ");
//                string title = Console.ReadLine();

//                Console.Write("Müəllif: ");
//                string author = Console.ReadLine();

//                Console.Write("ISBN (13 rəqəm): ");
//                string isbn = Console.ReadLine();

//                Console.Write("Nəşr ili: ");
//                int year = int.Parse(Console.ReadLine());

//                Console.Write("Kateqoriya ID: ");
//                int categoryId = int.Parse(Console.ReadLine());

//                Book book = new Book
//                {
//                    Title = title,
//                    Author = author,
//                    ISBN = isbn,
//                    PublishedYear = year,
//                    CategoryId = categoryId
//                };

//                _bookService.Add(book);
//                Console.WriteLine("\n✅ Kitab uğurla əlavə edildi!");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        static void ShowAllBooks()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ BÜTÜN KİTABLAR ═══\n");

//            try
//            {
//                var books = _bookService.GetAll();

//                if (books.Count == 0)
//                {
//                    Console.WriteLine("📭 Heç bir kitab tapılmadı.");
//                    return;
//                }

//                Console.WriteLine($"Cəmi: {books.Count} kitab\n");
//                Console.WriteLine("─────────────────────────────────────────────────────────────────────────────────");
//                Console.WriteLine($"{"ID",-5} {"Kitab Adı",-30} {"Müəllif",-25} {"ISBN",-15} {"İl",-6}");
//                Console.WriteLine("─────────────────────────────────────────────────────────────────────────────────");

//                foreach (var book in books)
//                {
//                    Console.WriteLine($"{book.Id,-5} {book.Title,-30} {book.Author,-25} {book.ISBN,-15} {book.PublishedYear,-6}");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        static void SearchBooks()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ KİTAB AXTAR ═══\n");

//            try
//            {
//                Console.Write("Axtarış sözü (ad, müəllif və ya ISBN): ");
//                string keyword = Console.ReadLine();

//                var books = _bookService.Search(keyword);

//                if (books.Count == 0)
//                {
//                    Console.WriteLine("\n📭 Heç bir nəticə tapılmadı.");
//                    return;
//                }

//                Console.WriteLine($"\nTapıldı: {books.Count} kitab\n");
//                Console.WriteLine("─────────────────────────────────────────────────────────────────────────────────");
//                Console.WriteLine($"{"ID",-5} {"Kitab Adı",-30} {"Müəllif",-25} {"ISBN",-15}");
//                Console.WriteLine("─────────────────────────────────────────────────────────────────────────────────");

//                foreach (var book in books)
//                {
//                    Console.WriteLine($"{book.Id,-5} {book.Title,-30} {book.Author,-25} {book.ISBN,-15}");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        static void UpdateBook()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ KİTAB YENİLƏ ═══\n");

//            try
//            {
//                Console.Write("Yeniləmək istədiyiniz kitabın ID-si: ");
//                int id = int.Parse(Console.ReadLine());

//                var book = _bookService.GetById(id);

//                Console.WriteLine($"\nCari məlumat:");
//                Console.WriteLine($"Ad: {book.Title}");
//                Console.WriteLine($"Müəllif: {book.Author}");
//                Console.WriteLine($"ISBN: {book.ISBN}");
//                Console.WriteLine($"İl: {book.PublishedYear}");
//                Console.WriteLine($"Kateqoriya ID: {book.CategoryId}");

//                Console.WriteLine("\n--- Yeni məlumatlar (boş buraxsanız dəyişməz) ---");

//                Console.Write("Yeni ad: ");
//                string title = Console.ReadLine();
//                if (!string.IsNullOrWhiteSpace(title))
//                    book.Title = title;

//                Console.Write("Yeni müəllif: ");
//                string author = Console.ReadLine();
//                if (!string.IsNullOrWhiteSpace(author))
//                    book.Author = author;

//                Console.Write("Yeni ISBN: ");
//                string isbn = Console.ReadLine();
//                if (!string.IsNullOrWhiteSpace(isbn))
//                    book.ISBN = isbn;

//                Console.Write("Yeni il: ");
//                string yearStr = Console.ReadLine();
//                if (!string.IsNullOrWhiteSpace(yearStr))
//                    book.PublishedYear = int.Parse(yearStr);

//                Console.Write("Yeni kateqoriya ID: ");
//                string catStr = Console.ReadLine();
//                if (!string.IsNullOrWhiteSpace(catStr))
//                    book.CategoryId = int.Parse(catStr);

//                _bookService.Update(book);
//                Console.WriteLine("\n✅ Kitab uğurla yeniləndi!");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        static void DeleteBook()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ KİTAB SİL ═══\n");

//            try
//            {
//                Console.Write("Silmək istədiyiniz kitabın ID-si: ");
//                int id = int.Parse(Console.ReadLine());

//                var book = _bookService.GetById(id);
//                Console.WriteLine($"\nSilinəcək kitab: {book.Title} - {book.Author}");

//                Console.Write("\nƏminsiniz? (b/x): ");
//                string confirm = Console.ReadLine();

//                if (confirm.ToLower() == "b")
//                {
//                    _bookService.Delete(id);
//                    Console.WriteLine("\n✅ Kitab uğurla silindi!");
//                }
//                else
//                {
//                    Console.WriteLine("\n❌ Əməliyyat ləğv edildi.");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        // ============================================
//        // KATEQORİYA MENYU
//        // ============================================
//        static void CategoryMenu()
//        {
//            while (true)
//            {
//                Console.Clear();
//                Console.WriteLine("╔════════════════════════════════════════╗");
//                Console.WriteLine("║      KATEQORİYA İDARƏETMƏSİ           ║");
//                Console.WriteLine("╚════════════════════════════════════════╝");
//                Console.WriteLine();
//                Console.WriteLine("  1. ➕ Yeni Kateqoriya Əlavə Et");
//                Console.WriteLine("  2. 📋 Bütün Kateqoriyaları Göstər");
//                Console.WriteLine("  3. 🔍 Kateqoriya Axtar");
//                Console.WriteLine("  4. ✏️  Kateqoriya Yenilə");
//                Console.WriteLine("  5. 🗑️  Kateqoriya Sil");
//                Console.WriteLine("  0. ⬅️  Geri");
//                Console.WriteLine();
//                Console.Write("Seçiminiz: ");

//                string choice = Console.ReadLine();

//                switch (choice)
//                {
//                    case "1":
//                        AddCategory();
//                        break;
//                    case "2":
//                        ShowAllCategories();
//                        break;
//                    case "3":
//                        SearchCategories();
//                        break;
//                    case "4":
//                        UpdateCategory();
//                        break;
//                    case "5":
//                        DeleteCategory();
//                        break;
//                    case "0":
//                        return;
//                    default:
//                        Console.WriteLine("\n❌ Yanlış seçim!");
//                        break;
//                }

//                if (choice != "0")
//                {
//                    Console.WriteLine("\nDavam etmək üçün bir düyməyə basın...");
//                    Console.ReadKey();
//                }
//            }
//        }

//        static void AddCategory()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ YENİ KATEQORİYA ƏLAVƏ ET ═══\n");

//            try
//            {
//                Console.Write("Kateqoriya adı: ");
//                string name = Console.ReadLine();

//                Console.Write("Təsvir: ");
//                string description = Console.ReadLine();

//                Category category = new Category
//                {
//                    Name = name,
//                    Description = description
//                };

//                _categoryService.Add(category);
//                Console.WriteLine("\n✅ Kateqoriya uğurla əlavə edildi!");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        static void ShowAllCategories()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ BÜTÜN KATEQORİYALAR ═══\n");

//            try
//            {
//                var categories = _categoryService.GetAll();

//                if (categories.Count == 0)
//                {
//                    Console.WriteLine("📭 Heç bir kateqoriya tapılmadı.");
//                    return;
//                }

//                Console.WriteLine($"Cəmi: {categories.Count} kateqoriya\n");
//                Console.WriteLine("───────────────────────────────────────────────────────────────────────");
//                Console.WriteLine($"{"ID",-5} {"Ad",-30} {"Təsvir",-40}");
//                Console.WriteLine("───────────────────────────────────────────────────────────────────────");

//                foreach (var cat in categories)
//                {
//                    Console.WriteLine($"{cat.Id,-5} {cat.Name,-30} {cat.Description,-40}");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        static void SearchCategories()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ KATEQORİYA AXTAR ═══\n");

//            try
//            {
//                Console.Write("Axtarış sözü: ");
//                string keyword = Console.ReadLine();

//                var categories = _categoryService.Search(keyword);

//                if (categories.Count == 0)
//                {
//                    Console.WriteLine("\n📭 Heç bir nəticə tapılmadı.");
//                    return;
//                }

//                Console.WriteLine($"\nTapıldı: {categories.Count} kateqoriya\n");
//                Console.WriteLine("───────────────────────────────────────────────────────────────────────");
//                Console.WriteLine($"{"ID",-5} {"Ad",-30} {"Təsvir",-40}");
//                Console.WriteLine("───────────────────────────────────────────────────────────────────────");

//                foreach (var cat in categories)
//                {
//                    Console.WriteLine($"{cat.Id,-5} {cat.Name,-30} {cat.Description,-40}");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        static void UpdateCategory()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ KATEQORİYA YENİLƏ ═══\n");

//            try
//            {
//                Console.Write("Yeniləmək istədiyiniz kateqoriyanın ID-si: ");
//                int id = int.Parse(Console.ReadLine());

//                var category = _categoryService.GetById(id);

//                Console.WriteLine($"\nCari məlumat:");
//                Console.WriteLine($"Ad: {category.Name}");
//                Console.WriteLine($"Təsvir: {category.Description}");

//                Console.WriteLine("\n--- Yeni məlumatlar ---");

//                Console.Write("Yeni ad: ");
//                string name = Console.ReadLine();
//                if (!string.IsNullOrWhiteSpace(name))
//                    category.Name = name;

//                Console.Write("Yeni təsvir: ");
//                string desc = Console.ReadLine();
//                if (!string.IsNullOrWhiteSpace(desc))
//                    category.Description = desc;

//                _categoryService.Update(category);
//                Console.WriteLine("\n✅ Kateqoriya uğurla yeniləndi!");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        static void DeleteCategory()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ KATEQORİYA SİL ═══\n");

//            try
//            {
//                Console.Write("Silmək istədiyiniz kateqoriyanın ID-si: ");
//                int id = int.Parse(Console.ReadLine());

//                var category = _categoryService.GetById(id);
//                Console.WriteLine($"\nSilinəcək kateqoriya: {category.Name}");

//                Console.Write("\nƏminsiniz? (b/x): ");
//                string confirm = Console.ReadLine();

//                if (confirm.ToLower() == "b")
//                {
//                    _categoryService.Delete(id);
//                    Console.WriteLine("\n✅ Kateqoriya uğurla silindi!");
//                }
//                else
//                {
//                    Console.WriteLine("\n❌ Əməliyyat ləğv edildi.");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        // ============================================
//        // ÜZV MENYU
//        // ============================================
//        static void MemberMenu()
//        {
//            while (true)
//            {
//                Console.Clear();
//                Console.WriteLine("╔════════════════════════════════════════╗");
//                Console.WriteLine("║         ÜZV İDARƏETMƏSİ               ║");
//                Console.WriteLine("╚════════════════════════════════════════╝");
//                Console.WriteLine();
//                Console.WriteLine("  1. ➕ Yeni Üzv Əlavə Et");
//                Console.WriteLine("  2. 📋 Bütün Üzvləri Göstər");
//                Console.WriteLine("  3. 🔍 Üzv Axtar");
//                Console.WriteLine("  4. ✏️  Üzv Yenilə");
//                Console.WriteLine("  5. 🗑️  Üzv Sil");
//                Console.WriteLine("  0. ⬅️  Geri");
//                Console.WriteLine();
//                Console.Write("Seçiminiz: ");

//                string choice = Console.ReadLine();

//                switch (choice)
//                {
//                    case "1":
//                        AddMember();
//                        break;
//                    case "2":
//                        ShowAllMembers();
//                        break;
//                    case "3":
//                        SearchMembers();
//                        break;
//                    case "4":
//                        UpdateMember();
//                        break;
//                    case "5":
//                        DeleteMember();
//                        break;
//                    case "0":
//                        return;
//                    default:
//                        Console.WriteLine("\n❌ Yanlış seçim!");
//                        break;
//                }

//                if (choice != "0")
//                {
//                    Console.WriteLine("\nDavam etmək üçün bir düyməyə basın...");
//                    Console.ReadKey();
//                }
//            }
//        }

//        static void AddMember()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ YENİ ÜZV ƏLAVƏ ET ═══\n");

//            try
//            {
//                Console.Write("Ad və soyad: ");
//                string fullName = Console.ReadLine();

//                Console.Write("Email: ");
//                string email = Console.ReadLine();

//                Console.Write("Telefon (+994XXXXXXXXX): ");
//                string phone = Console.ReadLine();

//                Member member = new Member
//                {
//                    FullName = fullName,
//                    Email = email,
//                    PhoneNumber = phone
//                };

//                _memberService.Add(member);
//                Console.WriteLine("\n✅ Üzv uğurla əlavə edildi!");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        static void ShowAllMembers()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ BÜTÜN ÜZVLƏR ═══\n");

//            try
//            {
//                var members = _memberService.GetAll();

//                if (members.Count == 0)
//                {
//                    Console.WriteLine("📭 Heç bir üzv tapılmadı.");
//                    return;
//                }

//                Console.WriteLine($"Cəmi: {members.Count} üzv\n");
//                Console.WriteLine("─────────────────────────────────────────────────────────────────────────────────");
//                Console.WriteLine($"{"ID",-5} {"Ad və Soyad",-30} {"Email",-35} {"Telefon",-15}");
//                Console.WriteLine("─────────────────────────────────────────────────────────────────────────────────");

//                foreach (var member in members)
//                {
//                    Console.WriteLine($"{member.Id,-5} {member.FullName,-30} {member.Email,-35} {member.PhoneNumber,-15}");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        static void SearchMembers()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ ÜZV AXTAR ═══\n");

//            try
//            {
//                Console.Write("Axtarış sözü (ad, email və ya telefon): ");
//                string keyword = Console.ReadLine();

//                var members = _memberService.Search(keyword);

//                if (members.Count == 0)
//                {
//                    Console.WriteLine("\n📭 Heç bir nəticə tapılmadı.");
//                    return;
//                }

//                Console.WriteLine($"\nTapıldı: {members.Count} üzv\n");
//                Console.WriteLine("─────────────────────────────────────────────────────────────────────────────────");
//                Console.WriteLine($"{"ID",-5} {"Ad və Soyad",-30} {"Email",-35} {"Telefon",-15}");
//                Console.WriteLine("─────────────────────────────────────────────────────────────────────────────────");

//                foreach (var member in members)
//                {
//                    Console.WriteLine($"{member.Id,-5} {member.FullName,-30} {member.Email,-35} {member.PhoneNumber,-15}");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        static void UpdateMember()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ ÜZV YENİLƏ ═══\n");

//            try
//            {
//                Console.Write("Yeniləmək istədiyiniz üzvün ID-si: ");
//                int id = int.Parse(Console.ReadLine());

//                var member = _memberService.GetById(id);

//                Console.WriteLine($"\nCari məlumat:");
//                Console.WriteLine($"Ad: {member.FullName}");
//                Console.WriteLine($"Email: {member.Email}");
//                Console.WriteLine($"Telefon: {member.PhoneNumber}");

//                Console.WriteLine("\n--- Yeni məlumatlar ---");

//                Console.Write("Yeni ad: ");
//                string name = Console.ReadLine();
//                if (!string.IsNullOrWhiteSpace(name))
//                    member.FullName = name;

//                Console.Write("Yeni email: ");
//                string email = Console.ReadLine();
//                if (!string.IsNullOrWhiteSpace(email))
//                    member.Email = email;

//                Console.Write("Yeni telefon: ");
//                string phone = Console.ReadLine();
//                if (!string.IsNullOrWhiteSpace(phone))
//                    member.PhoneNumber = phone;

//                _memberService.Update(member);
//                Console.WriteLine("\n✅ Üzv uğurla yeniləndi!");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }

//        static void DeleteMember()
//        {
//            Console.Clear();
//            Console.WriteLine("═══ ÜZV SİL ═══\n");

//            try
//            {
//                Console.Write("Silmək istədiyiniz üzvün ID-si: ");
//                int id = int.Parse(Console.ReadLine());

//                var member = _memberService.GetById(id);
//                Console.WriteLine($"\nSilinəcək üzv: {member.FullName}");

//                Console.Write("\nƏminsiniz? (b/x): ");
//                string confirm = Console.ReadLine();

//                if (confirm.ToLower() == "b")
//                {
//                    _memberService.Delete(id);
//                    Console.WriteLine("\n✅ Üzv uğurla silindi!");
//                }
//                else
//                {
//                    Console.WriteLine("\n❌ Əməliyyat ləğv edildi.");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"\n❌ Xəta: {ex.Message}");
//            }
//        }
//    }
//}

using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Contracts;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace ManagmentUI
{
    class Program
    {
        private static IBookService _bookService;
        private static ICategoryService _categoryService;
        private static IMemberService _memberService;

        static void Main(string[] args)
        {
            InitializeServices();
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                ShowMainMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BookMenu();
                        break;
                    case "2":
                        CategoryMenu();
                        break;
                    case "3":
                        MemberMenu();
                        break;
                    case "0":
                        ShowSuccess("\n👋 Proqramdan çıxılır...");
                        return;
                    default:
                        ShowError("Yanlış seçim! Yenidən cəhd edin.");
                        break;
                }

                Console.WriteLine("\nDavam etmək üçün bir düyməyə basın...");
                Console.ReadKey();
            }
        }

        static void InitializeServices()
        {
            var bookRepository = new BookRepository();
            var categoryRepository = new CategoryRepository();
            var memberRepository = new MemberRepository();

            _bookService = new BookService(bookRepository, categoryRepository, memberRepository);
            _categoryService = new CategoryService(categoryRepository);
            _memberService = new MemberService(memberRepository);
        }

        // ============================================
        // HELPER METHODS - Rəngli Mesajlar
        // ============================================
        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ {message}");
            Console.ResetColor();
        }

        static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ {message}");
            Console.ResetColor();
        }

        static void ShowWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n⚠️  {message}");
            Console.ResetColor();
        }

        static void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"ℹ️  {message}");
            Console.ResetColor();
        }

        // ============================================
        // CƏDVƏL HELPER METHODS
        // ============================================
        static string TruncateString(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty.PadRight(maxLength);

            return value.Length <= maxLength
                ? value.PadRight(maxLength)
                : value.Substring(0, maxLength - 3) + "...";
        }

        static void DrawLine(char left, char middle, char right, params int[] widths)
        {
            Console.Write(left);
            for (int i = 0; i < widths.Length; i++)
            {
                Console.Write(new string('─', widths[i] + 2)); // +2 for padding
                if (i < widths.Length - 1)
                    Console.Write(middle);
            }
            Console.WriteLine(right);
        }

        static void DrawRow(params string[] columns)
        {
            Console.Write("│");
            foreach (var col in columns)
            {
                Console.Write($" {col} │");
            }
            Console.WriteLine();
        }

        // ============================================
        // ANA MENYU
        // ============================================
        static void ShowMainMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                              ║");
            Console.WriteLine("║          📚 KİTABXANA İDARƏETMƏ SİSTEMİ 📚                 ║");
            Console.WriteLine("║                                                              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  ┌────────────────────────────────────────────────────┐");
            Console.WriteLine("  │  1. 📚 Kitab İdarəetməsi                          │");
            Console.WriteLine("  │  2. 📁 Kateqoriya İdarəetməsi                     │");
            Console.WriteLine("  │  3. 👥 Üzv İdarəetməsi                            │");
            Console.WriteLine("  │  0. 🚪 Çıxış                                       │");
            Console.WriteLine("  └────────────────────────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("  ➤ Seçiminiz: ");
        }

        // ============================================
        // KİTAB MENYU
        // ============================================
        static void BookMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                  📚 KİTAB İDARƏETMƏSİ                       ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  ┌────────────────────────────────────────────────────┐");
                Console.WriteLine("  │  1. ➕ Yeni Kitab Əlavə Et                        │");
                Console.WriteLine("  │  2. 📋 Bütün Kitabları Göstər                     │");
                Console.WriteLine("  │  3. 🔍 Kitab Axtar                                │");
                Console.WriteLine("  │  4. ✏️  Kitab Yenilə                              │");
                Console.WriteLine("  │  5. 🗑️  Kitab Sil                                 │");
                Console.WriteLine("  │  0. ⬅️  Geri                                       │");
                Console.WriteLine("  └────────────────────────────────────────────────────┘");
                Console.WriteLine();
                Console.Write("  ➤ Seçiminiz: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddBook(); break;
                    case "2": ShowAllBooks(); break;
                    case "3": SearchBooks(); break;
                    case "4": UpdateBook(); break;
                    case "5": DeleteBook(); break;
                    case "0": return;
                    default: ShowError("Yanlış seçim!"); break;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\n➤ Davam etmək üçün bir düyməyə basın...");
                    Console.ReadKey();
                }
            }
        }

        static void AddBook()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("          ➕ YENİ KİTAB ƏLAVƏ ET");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("📖 Kitab adı: ");
                string title = Console.ReadLine();

                Console.Write("✍️  Müəllif: ");
                string author = Console.ReadLine();

                Console.Write("🔢 ISBN (13 rəqəm): ");
                string isbn = Console.ReadLine();

                Console.Write("📅 Nəşr ili: ");
                int year = int.Parse(Console.ReadLine());

                Console.Write("📁 Kateqoriya ID: ");
                int categoryId = int.Parse(Console.ReadLine());

                Book book = new Book
                {
                    Title = title,
                    Author = author,
                    ISBN = isbn,
                    PublishedYear = year,
                    CategoryId = categoryId
                };

                _bookService.Add(book);
                ShowSuccess("Kitab uğurla əlavə edildi!");
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        static void ShowAllBooks()
        {
            //Console.Clear();
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine("\n═══ BÜTÜN KİTABLAR ═══\n");
            //Console.ResetColor();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                                           📚 BÜTÜN KİTABLAR");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                var books = _bookService.GetAll();

                if (books.Count == 0)
                {
                    ShowWarning("Heç bir kitab tapılmadı.");
                    return;
                }

                ShowInfo($"Cəmi: {books.Count} kitab\n");

                // Sütun genişlikləri
                int[] widths = { 3, 28, 23, 13, 4, 10 };

                // Üst xətt
                DrawLine('┌', '┬', '┐', widths);

                // Başlıq
                DrawRow(
                    TruncateString("ID", widths[0]),
                    TruncateString("Başlıq", widths[1]),
                    TruncateString("Müəllif", widths[2]),
                    TruncateString("ISBN", widths[3]),
                    TruncateString("İl", widths[4]),
                    TruncateString("Mövcud", widths[5])
                );

                // Başlıq ayırıcı
                DrawLine('├', '┼', '┤', widths);

                // Məlumatlar
                foreach (var book in books)
                {
                    string status = book.IsAvailable ? "✓ Bəli" : "✗ Xeyr";

                    DrawRow(
                        TruncateString(book.Id.ToString(), widths[0]),
                        TruncateString(book.Title, widths[1]),
                        TruncateString(book.Author, widths[2]),
                        TruncateString(book.ISBN, widths[3]),
                        TruncateString(book.PublishedYear.ToString(), widths[4]),
                        TruncateString(status, widths[5])
                    );
                }

                // Alt xətt
                DrawLine('└', '┴', '┘', widths);
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        static void SearchBooks()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("                  🔍 KİTAB AXTAR");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("🔎 Axtarış sözü (ad, müəllif və ya ISBN): ");
                string keyword = Console.ReadLine();

                var books = _bookService.Search(keyword);

                if (books.Count == 0)
                {
                    ShowWarning("Heç bir nəticə tapılmadı.");
                    return;
                }

                ShowSuccess($"Tapıldı: {books.Count} kitab\n");

                int[] widths = { 3, 28, 23, 13, 10 };

                DrawLine('┌', '┬', '┐', widths);

                DrawRow(
                    TruncateString("ID", widths[0]),
                    TruncateString("Başlıq", widths[1]),
                    TruncateString("Müəllif", widths[2]),
                    TruncateString("ISBN", widths[3]),
                    TruncateString("Mövcud", widths[4])
                );

                DrawLine('├', '┼', '┤', widths);

                foreach (var book in books)
                {
                    string status = book.IsAvailable ? "✓ Bəli" : "✗ Xeyr";

                    DrawRow(
                        TruncateString(book.Id.ToString(), widths[0]),
                        TruncateString(book.Title, widths[1]),
                        TruncateString(book.Author, widths[2]),
                        TruncateString(book.ISBN, widths[3]),
                        TruncateString(status, widths[4])
                    );
                }

                DrawLine('└', '┴', '┘', widths);
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        static void UpdateBook()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("                 ✏️  KİTAB YENİLƏ");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("🔢 Yeniləmək istədiyiniz kitabın ID-si: ");
                int id = int.Parse(Console.ReadLine());

                var book = _bookService.GetById(id);

                Console.WriteLine();
                ShowInfo("Cari məlumat:");
                Console.WriteLine($"  📖 Ad: {book.Title}");
                Console.WriteLine($"  ✍️  Müəllif: {book.Author}");
                Console.WriteLine($"  🔢 ISBN: {book.ISBN}");
                Console.WriteLine($"  📅 İl: {book.PublishedYear}");
                Console.WriteLine($"  📁 Kateqoriya ID: {book.CategoryId}");
                Console.Write($"  📊 Status: ");
                if (book.IsAvailable)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[Mövcuddur]");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Götürülüb]");
                }
                Console.ResetColor();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("─── Yeni məlumatlar (boş buraxsanız dəyişməz) ───");
                Console.ResetColor();

                Console.Write("📖 Yeni ad: ");
                string title = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(title))
                    book.Title = title;

                Console.Write("✍️  Yeni müəllif: ");
                string author = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(author))
                    book.Author = author;

                Console.Write("🔢 Yeni ISBN: ");
                string isbn = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(isbn))
                    book.ISBN = isbn;

                Console.Write("📅 Yeni il: ");
                string yearStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(yearStr))
                    book.PublishedYear = int.Parse(yearStr);

                Console.Write("📁 Yeni kateqoriya ID: ");
                string catStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(catStr))
                    book.CategoryId = int.Parse(catStr);

                _bookService.Update(book);
                ShowSuccess("Kitab uğurla yeniləndi!");
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        static void DeleteBook()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("                 🗑️  KİTAB SİL");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("🔢 Silmək istədiyiniz kitabın ID-si: ");
                int id = int.Parse(Console.ReadLine());

                var book = _bookService.GetById(id);

                Console.WriteLine();
                ShowWarning($"Silinəcək kitab: {book.Title} - {book.Author}");
                Console.WriteLine();
                Console.Write("Əminsiniz? (b/x): ");
                string confirm = Console.ReadLine();

                if (confirm.ToLower() == "b")
                {
                    _bookService.Delete(id);
                    ShowSuccess("Kitab uğurla silindi!");
                }
                else
                {
                    ShowInfo("Əməliyyat ləğv edildi.");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        // ============================================
        // KATEQORİYA MENYU
        // ============================================
        static void CategoryMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║              📁 KATEQORİYA İDARƏETMƏSİ                      ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  ┌────────────────────────────────────────────────────┐");
                Console.WriteLine("  │  1. ➕ Yeni Kateqoriya Əlavə Et                   │");
                Console.WriteLine("  │  2. 📋 Bütün Kateqoriyaları Göstər                │");
                Console.WriteLine("  │  3. 🔍 Kateqoriya Axtar                           │");
                Console.WriteLine("  │  4. ✏️  Kateqoriya Yenilə                         │");
                Console.WriteLine("  │  5. 🗑️  Kateqoriya Sil                            │");
                Console.WriteLine("  │  0. ⬅️  Geri                                       │");
                Console.WriteLine("  └────────────────────────────────────────────────────┘");
                Console.WriteLine();
                Console.Write("  ➤ Seçiminiz: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddCategory(); break;
                    case "2": ShowAllCategories(); break;
                    case "3": SearchCategories(); break;
                    case "4": UpdateCategory(); break;
                    case "5": DeleteCategory(); break;
                    case "0": return;
                    default: ShowError("Yanlış seçim!"); break;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\n➤ Davam etmək üçün bir düyməyə basın...");
                    Console.ReadKey();
                }
            }
        }

        static void AddCategory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("        ➕ YENİ KATEQORİYA ƏLAVƏ ET");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("📁 Kateqoriya adı: ");
                string name = Console.ReadLine();

                Console.Write("📝 Təsvir: ");
                string description = Console.ReadLine();

                Category category = new Category
                {
                    Name = name,
                    Description = description
                };

                _categoryService.Add(category);
                ShowSuccess("Kateqoriya uğurla əlavə edildi!");
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        static void ShowAllCategories()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n═══ BÜTÜN KATEQORİYALAR ═══\n");
            Console.ResetColor();

            try
            {
                var categories = _categoryService.GetAll();

                if (categories.Count == 0)
                {
                    ShowWarning("Heç bir kateqoriya tapılmadı.");
                    return;
                }

                ShowInfo($"Cəmi: {categories.Count} kateqoriya\n");

                int[] widths = { 3, 28, 45 };

                DrawLine('┌', '┬', '┐', widths);

                DrawRow(
                    TruncateString("ID", widths[0]),
                    TruncateString("Kateqoriya Adı", widths[1]),
                    TruncateString("Təsvir", widths[2])
                );

                DrawLine('├', '┼', '┤', widths);

                foreach (var cat in categories)
                {
                    DrawRow(
                        TruncateString(cat.Id.ToString(), widths[0]),
                        TruncateString(cat.Name, widths[1]),
                        TruncateString(cat.Description, widths[2])
                    );
                }

                DrawLine('└', '┴', '┘', widths);
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        static void SearchCategories()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("              🔍 KATEQORİYA AXTAR");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("🔎 Axtarış sözü: ");
                string keyword = Console.ReadLine();

                var categories = _categoryService.Search(keyword);

                if (categories.Count == 0)
                {
                    ShowWarning("Heç bir nəticə tapılmadı.");
                    return;
                }

                ShowSuccess($"Tapıldı: {categories.Count} kateqoriya\n");

                int[] widths = { 3, 28, 45 };

                DrawLine('┌', '┬', '┐', widths);

                DrawRow(
                    TruncateString("ID", widths[0]),
                    TruncateString("Kateqoriya Adı", widths[1]),
                    TruncateString("Təsvir", widths[2])
                );

                DrawLine('├', '┼', '┤', widths);

                foreach (var cat in categories)
                {
                    DrawRow(
                        TruncateString(cat.Id.ToString(), widths[0]),
                        TruncateString(cat.Name, widths[1]),
                        TruncateString(cat.Description, widths[2])
                    );
                }

                DrawLine('└', '┴', '┘', widths);
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        static void UpdateCategory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("             ✏️  KATEQORİYA YENİLƏ");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("🔢 Yeniləmək istədiyiniz kateqoriyanın ID-si: ");
                int id = int.Parse(Console.ReadLine());

                var category = _categoryService.GetById(id);

                Console.WriteLine();
                ShowInfo("Cari məlumat:");
                Console.WriteLine($"  📁 Ad: {category.Name}");
                Console.WriteLine($"  📝 Təsvir: {category.Description}");

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("─── Yeni məlumatlar ───");
                Console.ResetColor();

                Console.Write("📁 Yeni ad: ");
                string name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                    category.Name = name;

                Console.Write("📝 Yeni təsvir: ");
                string desc = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(desc))
                    category.Description = desc;

                _categoryService.Update(category);
                ShowSuccess("Kateqoriya uğurla yeniləndi!");
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        static void DeleteCategory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("             🗑️  KATEQORİYA SİL");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("🔢 Silmək istədiyiniz kateqoriyanın ID-si: ");
                int id = int.Parse(Console.ReadLine());

                var category = _categoryService.GetById(id);

                Console.WriteLine();
                ShowWarning($"Silinəcək kateqoriya: {category.Name}");
                Console.WriteLine();
                Console.Write("Əminsiniz? (b/x): ");
                string confirm = Console.ReadLine();

                if (confirm.ToLower() == "b")
                {
                    _categoryService.Delete(id);
                    ShowSuccess("Kateqoriya uğurla silindi!");
                }
                else
                {
                    ShowInfo("Əməliyyat ləğv edildi.");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        // ============================================
        // ÜZV MENYU
        // ============================================
        static void MemberMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                 👥 ÜZV İDARƏETMƏSİ                          ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  ┌────────────────────────────────────────────────────┐");
                Console.WriteLine("  │  1. ➕ Yeni Üzv Əlavə Et                          │");
                Console.WriteLine("  │  2. 📋 Bütün Üzvləri Göstər                       │");
                Console.WriteLine("  │  3. 🔍 Üzv Axtar                                  │");
                Console.WriteLine("  │  4. ✏️  Üzv Yenilə                                │");
                Console.WriteLine("  │  5. 🗑️  Üzv Sil                                   │");
                Console.WriteLine("  │  0. ⬅️  Geri                                       │");
                Console.WriteLine("  └────────────────────────────────────────────────────┘");
                Console.WriteLine();
                Console.Write("  ➤ Seçiminiz: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddMember(); break;
                    case "2": ShowAllMembers(); break;
                    case "3": SearchMembers(); break;
                    case "4": UpdateMember(); break;
                    case "5": DeleteMember(); break;
                    case "0": return;
                    default: ShowError("Yanlış seçim!"); break;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\n➤ Davam etmək üçün bir düyməyə basın...");
                    Console.ReadKey();
                }
            }
        }

        static void AddMember()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("            ➕ YENİ ÜZV ƏLAVƏ ET");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("👤 Ad və soyad: ");
                string fullName = Console.ReadLine();

                Console.Write("📧 Email: ");
                string email = Console.ReadLine();

                Console.Write("📱 Telefon (+994XXXXXXXXX): ");
                string phone = Console.ReadLine();

                Member member = new Member
                {
                    FullName = fullName,
                    Email = email,
                    PhoneNumber = phone
                };

                _memberService.Add(member);
                ShowSuccess("Üzv uğurla əlavə edildi!");
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        static void ShowAllMembers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n═══ BÜTÜN ÜZVLƏR ═══\n");
            Console.ResetColor();

            try
            {
                var members = _memberService.GetAll();

                if (members.Count == 0)
                {
                    ShowWarning("Heç bir üzv tapılmadı.");
                    return;
                }

                ShowInfo($"Cəmi: {members.Count} üzv\n");

                // Sütun genişlikləri
                int[] widths = { 3, 28, 30, 15, 10 };

                // Üst xətt
                DrawLine('┌', '┬', '┐', widths);

                // Başlıq
                DrawRow(
                    TruncateString("ID", widths[0]),
                    TruncateString("Ad və Soyad", widths[1]),
                    TruncateString("Email", widths[2]),
                    TruncateString("Telefon", widths[3]),
                    TruncateString("Status", widths[4])
                );

                // Başlıq ayırıcı
                DrawLine('├', '┼', '┤', widths);

                // Məlumatlar
                foreach (var member in members)
                {
                    string status = member.IsActive ? "✓ Aktiv" : "✗ Deaktiv";

                    DrawRow(
                        TruncateString(member.Id.ToString(), widths[0]),
                        TruncateString(member.FullName, widths[1]),
                        TruncateString(member.Email, widths[2]),
                        TruncateString(member.PhoneNumber, widths[3]),
                        TruncateString(status, widths[4])
                    );
                }

                // Alt xətt
                DrawLine('└', '┴', '┘', widths);
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        static void SearchMembers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("                  🔍 ÜZV AXTAR");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("🔎 Axtarış sözü (ad, email və ya telefon): ");
                string keyword = Console.ReadLine();

                var members = _memberService.Search(keyword);

                if (members.Count == 0)
                {
                    ShowWarning("Heç bir nəticə tapılmadı.");
                    return;
                }

                ShowSuccess($"Tapıldı: {members.Count} üzv\n");

                int[] widths = { 3, 28, 30, 15, 10 };

                DrawLine('┌', '┬', '┐', widths);

                DrawRow(
                    TruncateString("ID", widths[0]),
                    TruncateString("Ad və Soyad", widths[1]),
                    TruncateString("Email", widths[2]),
                    TruncateString("Telefon", widths[3]),
                    TruncateString("Status", widths[4])
                );

                DrawLine('├', '┼', '┤', widths);

                foreach (var member in members)
                {
                    string status = member.IsActive ? "✓ Aktiv" : "✗ Deaktiv";

                    DrawRow(
                        TruncateString(member.Id.ToString(), widths[0]),
                        TruncateString(member.FullName, widths[1]),
                        TruncateString(member.Email, widths[2]),
                        TruncateString(member.PhoneNumber, widths[3]),
                        TruncateString(status, widths[4])
                    );
                }

                DrawLine('└', '┴', '┘', widths);
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        static void UpdateMember()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("                 ✏️  ÜZV YENİLƏ");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("🔢 Yeniləmək istədiyiniz üzvün ID-si: ");
                int id = int.Parse(Console.ReadLine());

                var member = _memberService.GetById(id);

                Console.WriteLine();
                ShowInfo("Cari məlumat:");
                Console.WriteLine($"  👤 Ad: {member.FullName}");
                Console.WriteLine($"  📧 Email: {member.Email}");
                Console.WriteLine($"  📱 Telefon: {member.PhoneNumber}");
                Console.Write($"  📊 Status: ");
                if (member.IsActive)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[Aktiv]");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Deaktiv]");
                }
                Console.ResetColor();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("─── Yeni məlumatlar (boş buraxsanız dəyişməz) ───");
                Console.ResetColor();

                Console.Write("👤 Yeni ad: ");
                string name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                    member.FullName = name;

                Console.Write("📧 Yeni email: ");
                string email = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(email))
                    member.Email = email;

                Console.Write("📱 Yeni telefon: ");
                string phone = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(phone))
                    member.PhoneNumber = phone;

                Console.Write("📊 Üzv statusu (1=Aktiv, 0=Deaktiv, boş=dəyişməz): ");
                string statusStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(statusStr))
                {
                    if (statusStr == "1")
                        member.IsActive = true;
                    else if (statusStr == "0")
                        member.IsActive = false;
                }

                _memberService.Update(member);
                ShowSuccess("Üzv uğurla yeniləndi!");
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        static void DeleteMember()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("                 🗑️  ÜZV SİL");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("🔢 Silmək istədiyiniz üzvün ID-si: ");
                int id = int.Parse(Console.ReadLine());

                var member = _memberService.GetById(id);

                Console.WriteLine();
                ShowWarning($"Silinəcək üzv: {member.FullName}");
                Console.WriteLine();
                Console.Write("Əminsiniz? (b/x): ");
                string confirm = Console.ReadLine();

                if (confirm.ToLower() == "b")
                {
                    _memberService.Delete(id);
                    ShowSuccess("Üzv uğurla silindi!");
                }
                else
                {
                    ShowInfo("Əməliyyat ləğv edildi.");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }
    }
}
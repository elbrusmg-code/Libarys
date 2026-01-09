
using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Contracts;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System.Text;

namespace ManagmentUI
{
    class Program
    {
        private static IBookService _bookService;
        private static ICategoryService _categoryService;
        private static IMemberService _memberService;

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
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
            var operationsRepository = new OperationsRepository();
            _bookService = new BookService(bookRepository, categoryRepository, memberRepository,operationsRepository);
            _categoryService = new CategoryService(categoryRepository);
            _memberService = new MemberService(memberRepository,bookRepository);
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
                Console.WriteLine("  │  4. ✏️  Kitab Yenilə                               │");
                Console.WriteLine("  │  5. 🗑️  Kitab Sil                                 │");
                Console.WriteLine("  │  6. 📤 Kitab Götür (Borrow)                       │");
                Console.WriteLine("  │  7. 📥 Kitab Qaytar (Return)                      │");
                Console.WriteLine("  │  8. 📊 Əməliyyat Tarixçəsi                        │"); 
                Console.WriteLine("  │  9. 📋 Aktiv Əməliyyatlar                         │"); 
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
                    case "6": BorrowBookUI(); break;
                    case "7": ReturnBookUI(); break;
                    case "8": ShowOperationHistory(); break; 
                    case "9": ShowActiveOperations(); break;
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
            var categories = _categoryService.GetAll();
            if(categories.Count ==0)

            {
                Console.WriteLine("Evvelce Kategory Elave edin.");
                return;

            }
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

                //Console.Write("🔢 ISBN (13 rəqəm): ");
                //string isbn = Console.ReadLine();
                Console.WriteLine("🔢 ISBN daxil edin (nümunə):");
                Console.WriteLine("   ISBN-10 : 0306406152");
                Console.WriteLine("   ISBN-13 : 9780306406157");
                Console.Write("ISBN: ");
                string isbn = Console.ReadLine();
                Console.Write("📅 Nəşr ili: ");
                int year = int.Parse(Console.ReadLine());

                Console.Write("📁 Kateqoriya ID: ");
                int categoryId = int.Parse(Console.ReadLine());

                var dto = new BookCteateDto
                {
                    Title = title,
                    Author = author,
                    ISBN = isbn,
                    PublishedYear = year,
                    CategoryId = categoryId
                };

                _bookService.Add(dto);
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
                int[] widths = { 3, 28, 23, 13, 4, 22 };

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
                    //string status = book.IsAvailable ? "✓ Bəli" : "✗ Xeyr";
                    //string status = book.IsAvailable
                    //    ? "✓ Mövcud"
                    //    : $"👤 Götürülüb ({book.MemberId})";
                    string status;

                    if (book.IsAvailable)
                    {
                        status = "✓ Mövcud";
                    }
                    else
                    {
                        status = $"👤 Götürülüb ({book.MemberId})";
                    }
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
                //Console.Write($"  📊 Status: ");
                //if (book.IsAvailable)
                //{
                //    Console.ForegroundColor = ConsoleColor.Green;
                //    Console.WriteLine("[Mövcuddur]");
                //}
                //else
                //{
                //    Console.ForegroundColor = ConsoleColor.Red;
                //    Console.WriteLine("[Götürülüb]");
                //}
              
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
                Console.Write($"Mövcuddur? (b/x) (cari: {(book.IsAvailable ? "Bəli" : "Xeyr")}): ");
                string avail = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(avail))
                    book.IsAvailable = avail.ToLower() == "b";

                var dto = new BookUptadeDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    ISBN = book.ISBN,
                    PublishedYear = book.PublishedYear,
                    CategoryId = book.CategoryId,
                    IsAvailable = book.IsAvailable,
                };
                _bookService.Update(dto);
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
        static void BorrowBookUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("               📤 KİTAB GÖTÜR (BORROW)");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("📚 Kitab ID: ");
                int bookId = int.Parse(Console.ReadLine());

                Console.Write("👤 Üzv ID: ");
                int memberId = int.Parse(Console.ReadLine());

                _bookService.BorrowBook(bookId, memberId);

                ShowSuccess("Kitab uğurla götürüldü!");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }
        static void ReturnBookUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.WriteLine("               📥 KİTAB QAYTAR (RETURN)");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.Write("📚 Kitab ID: ");
                int bookId = int.Parse(Console.ReadLine());

                _bookService.ReturnBook(bookId);

                ShowSuccess("Kitab uğurla qaytarıldı!");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }
        static void ShowOperationHistory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                                      📊 ƏMƏLİYYAT TARİXÇƏSİ");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                Console.WriteLine("Filter seçin:");
                Console.WriteLine("  1. Bütün əməliyyatlar");
                Console.WriteLine("  2. Kitaba görə");
                Console.WriteLine("  3. Üzvə görə");
                Console.Write("\n➤ Seçim: ");
                string filterChoice = Console.ReadLine();

                int? bookId = null;
                int? memberId = null;

                switch (filterChoice)
                {
                    case "2":
                        Console.Write("📚 Kitab ID: ");
                        bookId = int.Parse(Console.ReadLine());
                        break;
                    case "3":
                        Console.Write("👤 Üzv ID: ");
                        memberId = int.Parse(Console.ReadLine());
                        break;
                }

                var operations = _bookService.GetOperationHistory(bookId, memberId);

                if (operations.Count == 0)
                {
                    ShowWarning("Heç bir əməliyyat tapılmadı.");
                    return;
                }

                ShowInfo($"Cəmi: {operations.Count} əməliyyat\n");

                // Sütun genişlikləri
                int[] widths = { 3, 25, 20, 12, 12, 10 };

                DrawLine('┌', '┬', '┐', widths);

                DrawRow(
                    TruncateString("ID", widths[0]),
                    TruncateString("Kitab", widths[1]),
                    TruncateString("Üzv", widths[2]),
                    TruncateString("Götürülmə", widths[3]),
                    TruncateString("Qaytarılma", widths[4]),
                    TruncateString("Status", widths[5])
                );

                DrawLine('├', '┼', '┤', widths);

                foreach (var op in operations)
                {
                    string borrowDate = op.BorrowDate.ToString("dd.MM.yyyy");
                    string returnDate = op.ReturnDate?.ToString("dd.MM.yyyy") ?? "-";
                    string status = op.IsReturned ? "✓ Qayıdıb" : "✗ Götürülüb";

                    DrawRow(
                        TruncateString(op.Id.ToString(), widths[0]),
                        TruncateString(op.BookTitle, widths[1]),
                        TruncateString(op.MemberName, widths[2]),
                        TruncateString(borrowDate, widths[3]),
                        TruncateString(returnDate, widths[4]),
                        TruncateString(status, widths[5])
                    );
                }

                DrawLine('└', '┴', '┘', widths);
            }
            catch (Exception ex)
            {
                ShowError($"Xəta: {ex.Message}");
            }
        }

        static void ShowActiveOperations()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                              📋 AKTİV ƏMƏLİYYATLAR");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                var operations = _bookService.GetActiveOperations();

                if (operations.Count == 0)
                {
                    ShowWarning("Hazırda aktiv əməliyyat yoxdur.");
                    return;
                }

                ShowInfo($"Cəmi: {operations.Count} aktiv əməliyyat\n");

                // Sütun genişlikləri
                int[] widths = { 3, 8, 8, 12, 10 };

                DrawLine('┌', '┬', '┐', widths);

                DrawRow(
                    TruncateString("ID", widths[0]),
                    TruncateString("Kitab ID", widths[1]),
                    TruncateString("Üzv ID", widths[2]),
                    TruncateString("Götürülmə", widths[3]),
                    TruncateString("Gün sayı", widths[4])
                );

                DrawLine('├', '┼', '┤', widths);

                foreach (var op in operations)
                {
                    string borrowDate = op.BorrowDate.ToString("dd.MM.yyyy");
                    int daysHeld = (DateTime.Now - op.BorrowDate).Days;
                    string daysInfo = $"{daysHeld} gün";

                    DrawRow(
                        TruncateString(op.Id.ToString(), widths[0]),
                        TruncateString(op.BookId.ToString(), widths[1]),
                        TruncateString(op.MemberId.ToString(), widths[2]),
                        TruncateString(borrowDate, widths[3]),
                        TruncateString(daysInfo, widths[4])
                    );
                }

                DrawLine('└', '┴', '┘', widths);
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

                var categorys = new CategoryCreateDto
                {
                    Name = name,
                    Description = description
                };

                _categoryService.Add(categorys);
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

                var categorys = new CategoryUpdateDto
                {
                    Id = id,
                    Name = name,
                    Description = desc
                };
                _categoryService.Update(categorys);
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

                var members = new MemberCreateDto
                {
                    FullName = fullName,
                    Email = email,
                    PhoneNumber = phone
                };
                _memberService.Add(members);
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

                var members = new MemberUpdateDto
                {
                    Id = member.Id,
                    FullName = member.FullName,
                    Email = member.Email,
                    PhoneNumber = member.PhoneNumber,
                    IsActive = member.IsActive,
                };
                _memberService.Update(members);
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
                    _bookService.DeactivateMemberAndReturnBooks(id);
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
// See https://aka.ms/new-console-template for more information
using System;
using LibrarySystem;

namespace LibrarySystem
{   
    class Program
    {
        static void Main()
        {
            Console.Clear();
            Library library = new Library();          //  Library with 4 default books   

            // iniate admin and add to users 
            Admin admin = new Admin();
            library.AddUser(admin);

            User? currentUser = null;


            while (true)
            {
                Console.WriteLine("\n=== User Menu ===");
                Console.WriteLine("[1]. Username (login)");
                Console.WriteLine("[2]. New User");
                Console.WriteLine("[3]. Exit");
                Console.Write("Select: > ");
                int key;

                if (!int.TryParse(Console.ReadLine(), out key))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                switch (key)
                {
                    case 1: // login by username

                        Console.Write("Enter username: ");
                        string loginName = (Console.ReadLine() ?? "").Trim().ToLower();

                        currentUser = library.FindUserByName(loginName);
                        if (currentUser == null)
                        {
                            Console.WriteLine("User not found. Please create a new user first.");
                            break;
                        }

                        Console.Clear();
                        LibraryMenu(library, currentUser);
                        break;

                    case 2: // register new user

                        Console.Write("Enter new username: ");
                        string newName = (Console.ReadLine() ?? "").Trim().ToLower();

                        if (string.IsNullOrWhiteSpace(newName))
                        {
                            Console.WriteLine("Username cannot be empty or null!");
                            break;
                        }

                        //  newName cannot contains spaces
                        if (newName.Contains(" "))
                        {
                            Console.WriteLine("Username cannot contain spaces!");
                            break;
                        }

                        // check if newName exists
                        if (library.FindUserByName(newName) != null)
                        {
                            Console.WriteLine("Username already exists. Please choose a different name.");
                            break;
                        }

                        User newUser = new User(newName);
                        library.AddUser(newUser);
                        currentUser = newUser;

                        Console.WriteLine($"New user '{currentUser.UserName}' created with ID {currentUser.UserId}.");

                        Console.Clear();
                        LibraryMenu(library, currentUser);
                        break;

                    case 3: // exit

                        Console.WriteLine("Exiting the program. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        // ===== Library Menu for a logged-in user =====
        static void LibraryMenu(Library library, User currentUser)
        {
            int choice;

            while (true)
            {
                Console.WriteLine($"\nWelcome [{currentUser.UserName}] to the Library System");
                Console.WriteLine("[1]. View Available Books");
                Console.WriteLine("[2]. Borrow Books");
                Console.WriteLine("[3]. Return Books");
                Console.WriteLine("[4]. Display All Books info (for Admin)");
                Console.WriteLine("[5]. Display All Users info (for Admin)");
                Console.WriteLine("[6]. User Logout");
                Console.Write("Please select an option: > ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1: // view all books

                        List<Book> availableBooks = library.GetAvailableBooks();
                        if (availableBooks.Count == 0)
                            Console.WriteLine("No books available.");
                        else
                        {
                            ListAvailablebooks(availableBooks);
                        }
                        break;

                    case 2: // borrow

                        // library.DisplayAllBooks();
                        List<Book> availableBorrow = library.GetAvailableBooks();
                        if (availableBorrow.Count == 0)
                        {
                            Console.WriteLine("No books available to borrow.");
                            break;
                        }
                        ListAvailablebooks(availableBorrow);
                        Console.Write("Enter Book ID to borrow: ");
                        if (int.TryParse(Console.ReadLine(), out int borrowId))
                        {
                            library.BorrowBook(borrowId, currentUser.UserId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                        break;

                    case 3: // return

                        Console.WriteLine("\nYour borrowed books:");
                        List<Book> borrowed = currentUser.GetBorrowedBooks();
                        if (borrowed.Count == 0)
                        {
                            Console.WriteLine("You have no borrowed books.");
                            break;
                        }

                        foreach (Book b in borrowed)
                        {
                            Console.WriteLine($"BookID: {b.BookId} \tTitle: {b.Title}");
                        }

                        Console.Write("Enter Book ID to return: ");
                        if (int.TryParse(Console.ReadLine(), out int returnId))
                        {
                            library.ReturnBook(returnId, currentUser.UserId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                        break;

                    case 4: // display all books info (admin)   

                        if (!(currentUser is Admin))
                        {
                            Console.WriteLine("Access denied. Only admin can view all books info.");
                            break;
                        }

                        library.DisplayAllBooks();
                        break;


                    case 5: // display all users info (admin)

                        if (!(currentUser is Admin))
                        {
                            Console.WriteLine("Access denied. Only admin can view all users info.");
                            break;
                        }

                        List<User> allUsers = library.GetUsers();
                        if (allUsers.Count == 0)
                        {
                            Console.WriteLine("No users found.");
                            break;
                        }

                        Console.WriteLine("\nAll users info:");
                        foreach (User u in allUsers)
                        {
                            Console.WriteLine(u.DisplayInfo());
                        }
                        break;


                    case 6:
                        Console.WriteLine($"[{currentUser.UserName}] is Logging out...");
                        Console.Clear();
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        // Book List Display
        public static void ListAvailablebooks(List<Book> books)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No available books.");
                return;
            }

            Console.WriteLine("\nAvailable books:");
            foreach (Book book in books)
            {
                Console.WriteLine($"BookID: {book.BookId} \tTitle: {book.Title}");
            }
        }
    }
}
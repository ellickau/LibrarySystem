using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public class Library
    {
        // fields
        private readonly List<Book> _books;
        private readonly List<User> _users;

        // constructor
        public Library()
        {
            _books = new List<Book>
            {
                new Book("The Great Gatsby"),
                new Book("1984"),
                new Book("A Tale of Two Cities"),
                new Book("Pride and Prejudice"),
                new Book("Bill Gates Biography"),
                new Book("Twilight"),
                new Book("The World is Flat"),
                new Book("Harry Potter"), 
                new Book("The Lord of the Rings")
            };

            _users = new List<User>();
        }

        // ---------------------------
        // basic add book 
        // ---------------------------
        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        // ---------------------------
        // basic add Users 
        // ---------------------------
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        // ---------------------------
        // Books Finder
        // ---------------------------
        public Book FindBookById(int bookId)
        {
            foreach (Book book in _books)
            {
                if (book.BookId == bookId)
                {
                    return book;    // found the matching book
                }
            }
            return null; // not found
        }

        // ---------------------------
        // Users Finder by ID
        // ---------------------------
        public User FindUserById(int userId)
        {
            foreach (User user in _users)
            {
                if (user.UserId == userId)
                {
                    return user;    // found the matching user
                }
            }
            return null; // not found
        }

       // ---------------------------
        // Users finder by Name
        // ---------------------------
        public User FindUserByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            
            foreach (var u in _users) // the Library’s private users list
                if (u.UserName.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return u;
            return null;
        }

        // ---------------------------
        // Get Available Books 
        // ---------------------------
        public List<Book> GetAvailableBooks()
        {
            List<Book> available = new List<Book>();

            foreach (Book book in _books)
            {
                if (!book.GetIsCheckedOut())   // only add if it's NOT checked out
                {
                    available.Add(book);
                }
            }

            return available;
        }

        // ---------------------------
        // Get Available Users 
        // ---------------------------
        public List<User> GetUsers()
        {
            return _users;
        }

        // ---------------------------
        // Borrow Books
        // ---------------------------
        public bool BorrowBook(int bookId, int userId)
        {
            Book book = FindBookById(bookId);
            User user = FindUserById(userId);

            if (book == null || user == null)
            {
                Console.WriteLine("Error: Invalid user or book ID.");
                return false;
            }

            string result = user.BorrowBook(book);
            Console.WriteLine(result);
            return result.StartsWith("You have successfully");
        }

        // ---------------------------
        //  Return Books
        // ---------------------------
        public bool ReturnBook(int bookId, int userId)
        {
            Book book = FindBookById(bookId);
            User user = FindUserById(userId);

            if (book == null || user == null)
            {
                Console.WriteLine("Error: Invalid user or book ID.");
                return false;
            }

            string result = user.ReturnBook(book);
            Console.WriteLine(result);
            return result.StartsWith("You have successfully");
        }


        // ---------------------------
        // Display All Books
        // ---------------------------
        public void DisplayAllBooks()
        {
            Console.WriteLine("\nAll books in library:");
            foreach (var book in _books)
                Console.WriteLine(book.DisplayInfo());
        }
    }
}

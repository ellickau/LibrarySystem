using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public class User
    {
        private static int _nextId = 1;   // shared counter
        public int UserId { get; protected set; } 
        public string UserName { get; private set; }
        private readonly List<Book> _borrowedBooks;

        public User(string userName)
        {
            UserId = _nextId++;
            UserName = userName;
            _borrowedBooks = new List<Book>();
        }

        public List<Book> GetBorrowedBooks()
        {
            return _borrowedBooks;   
        }

        public string BorrowBook(Book book)
        {
            if (book == null)
                return "Error: Invalid book.";

            if (_borrowedBooks.Contains(book))
                return $"You already borrowed \"{book.Title}\".";

            if (!book.CheckOut(UserName))
                return $"Error: \"{book.Title}\" is already checked out.";

            _borrowedBooks.Add(book);
            return $"You have successfully borrowed \"{book.Title}\".";
        }


        public string ReturnBook(Book book)
        {
            if (book == null)
                return "Error: Invalid book.";

            if (!_borrowedBooks.Contains(book))
                return $"You don't have \"{book.Title}\".";

            if (!book.Return())
                return $"Error: \"{book.Title}\" was not checked out.";

            _borrowedBooks.Remove(book);
            return $"You have successfully returned \"{book.Title}\".";
        }

        public string DisplayInfo()
        {
            return $"UserID: {UserId} \tUser Name: {UserName} \tBorrowed Books: {_borrowedBooks.Count}";
        }

    }
}
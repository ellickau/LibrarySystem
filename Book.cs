using System;
using System.Net;


namespace LibrarySystem
{
    public class Book
    {
        private static int _nextId = 1;
        private bool _isCheckedOut;
        private string? _borrowedBy;
        public int BookId { get; private set; }
        public string Title { get; private set; }


        public Book(string title)
        {
            BookId = _nextId++;
            Title = title;
            _isCheckedOut = false;
            _borrowedBy = null;
        }

        public bool GetIsCheckedOut()
        {
            return _isCheckedOut;
        }
        
        public string? GetBorrowedBy()
        {
            return _borrowedBy;
        }


        public string DisplayInfo()
        {
            string status = " ";
            if (_isCheckedOut)
            {
               status = $"By {_borrowedBy}";
            }    

            return $"BookID: {BookId, -5} \tTitle: {Title, -25} \tChecked Out: {_isCheckedOut} \t{status}";
        }


        public bool CheckOut(string userName)
        {
            if (_isCheckedOut) return false;
            _isCheckedOut = true;
            _borrowedBy = userName;
            return true;
        }

        
        public bool Return()
        {
            if (!_isCheckedOut) return false;
            _isCheckedOut = false;
            return true;    
        }

    }
}
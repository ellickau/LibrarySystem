using System;

    namespace LibrarySystem
    {
        public class Admin : User
        {

        public const int AdminId = 99;
        public const string AdminName = "admin";

        public Admin() : base(AdminName)
        {
            this.UserId = AdminId;
        }
        }
    }

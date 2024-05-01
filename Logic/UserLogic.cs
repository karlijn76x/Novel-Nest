﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novel_Nest_Core.Interfaces;
using Novel_Nest_Core.Models;


namespace Novel_Nest_Core
{
    public class UserLogic
{

        private UserDB userDB;

        public UserLogic()
        {
          userDB = new UserDB(new MyDbContext("Server=127.0.0.1;Database=Novel_Nest_Db;Uid=root;"));
        }

        public void CreateUser(UserModel user)
        {
            Console.WriteLine(user);
        }
}
}

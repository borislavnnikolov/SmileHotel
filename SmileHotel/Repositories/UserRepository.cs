﻿using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using SmileHotel.Models;

namespace SmileHotel.Repositories
{
    public class UserRepository
    {
        private string connetionString = @"server=remotemysql.com;database=8wdwbHzKHP;UID=8wdwbHzKHP;password=kPRONAzKzw";
        private MySqlConnection cnn;
        private string query;
        private MySqlDataReader dataReader;

        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            this.query = "SELECT * FROM Users;";
            this.cnn = new MySqlConnection(this.connetionString);
            this.cnn.Open();

            MySqlCommand SqlQuery = new MySqlCommand(this.query, this.cnn);
            this.dataReader = SqlQuery.ExecuteReader();

            while (this.dataReader.Read())
            {
                User userToAdd = new User();
                userToAdd.Id = this.dataReader.GetInt32(0);
                userToAdd.Name = this.dataReader.GetString(1);
                userToAdd.PhoneNumber = this.dataReader.GetString(2);
                users.Add(userToAdd);
            }

            this.cnn.Close();

            return users;
        }

        public User GetUser(int id)
        {
            var user = new User();

            this.query = "SELECT * FROM Users WHERE ID = " + id + ";";
            this.cnn = new MySqlConnection(this.connetionString);
            this.cnn.Open();

            MySqlCommand SqlQuery = new MySqlCommand(this.query, this.cnn);
            this.dataReader = SqlQuery.ExecuteReader();
            this.dataReader.Read();
            user.Id = this.dataReader.GetInt32(0);
            user.Name = this.dataReader.GetString(1);
            user.PhoneNumber = this.dataReader.GetString(2);
            this.cnn.Close();

            return user;
        }

        public User AddOrUpdateUser(User user)
        {
            if (user.Id <= 0)
            {
                List<User> users = this.GetAllUsers();

                int maxID = 0;
                foreach (User user1 in users)
                {
                    if (maxID < user1.Id)
                    {
                        maxID = user1.Id;
                    }
                }

                maxID++;

                this.query = "INSERT INTO Users (`ID`, `Name`, `PhoneNumber`) VALUES ('" + maxID.ToString() + "','" + user.Name + "', '" + user.PhoneNumber + "');";
                this.cnn = new MySqlConnection(this.connetionString);
                this.cnn.Open();

                MySqlCommand SqlQuery = new MySqlCommand(this.query, this.cnn);
                SqlQuery.ExecuteNonQuery();
                this.cnn.Close();
            }
            else
            {
                this.query = "UPDATE Users " +
                    "SET Name = '" + user.Name + "' , PhoneNumber = '" + user.PhoneNumber + "'" +
                    " WHERE ID = " + user.Id + ";";

                this.cnn = new MySqlConnection(this.connetionString);
                this.cnn.Open();
                MySqlCommand SqlQuery = new MySqlCommand(this.query, this.cnn);
                SqlQuery.ExecuteNonQuery();
                this.cnn.Close();
            }

            return user;
        }

        public bool DeleteUser(int id)
        {
            try
            {
                this.query = "DELETE FROM Users WHERE ID = " + id.ToString() + ";";
                this.cnn = new MySqlConnection(this.connetionString);
                this.cnn.Open();

                MySqlCommand SqlQuery = new MySqlCommand(this.query, this.cnn);
                SqlQuery.ExecuteNonQuery();
                this.cnn.Close();
            }
            catch (SqlException e)
            {
                return false;
            }

            return true;
        }
    }
}
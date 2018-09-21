﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10;
using Task10.Models;

namespace KnowledgeManagementSystem.DAL.Repositories
{
    //public class UserRepository : IUserRepository
    public class UserRepository<T> : IUserRepository<T> where T : class, new()
    {
        public SqlConnection connection = null;

        private string connectionString = @"Data Source=LAPTOP-4GR3EOJP\SQLEXPRESS_AIDA;Initial Catalog=KnowledgeManagementSystemDB;Integrated Security=True";

        public bool Delete(int id = default)
        {
            try
            {
                string sqlRequest = string.Format("DELETE FROM KnowledgeBase.[dbo].[User] WHERE UserId = {0}", id);

                using (var connection = new SqlConnection(connectionString))
                {
                    SqlCommand c = new SqlCommand();
                    connection.Open();
                    c.Connection = connection;
                    c.CommandText = sqlRequest;
                    int numberOfUpdate = c.ExecuteNonQuery();
                    if (numberOfUpdate > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (NullReferenceException)
            {
                return false;
            }

        }

        public T Get(int id = default)
        {
            try
            {
                UserModel User = new UserModel();

                using (var connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = string.Format("SELECT * FROM KnowledgeBase.dbo.[User] WHERE UserId = {0} ", id);
                    SqlParameter IdParam = new SqlParameter("@IdParam", id);
                    command.Parameters.Add(IdParam);

                    IDataReader dr = command.ExecuteReader();
                    if (dr.Read())
                    {

                        User.Id = (int)dr["UserId"];
                        User.Name = dr["UserName"].ToString();
                        User.Password = dr["UserPassword"].ToString();
                        User.UserRole = (UserRoleID)dr["UserRole"];
                        User.Email = dr["UserEmail"].ToString();
                        return User as T;
                    }
                    else return null;
                }
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public List<T> GetAll()
        {
            List<UserModel> AllUsers = new List<UserModel>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM KnowledgeBase.[dbo].[User]";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserModel User = new UserModel();
                        User.Id = (int)reader["UserId"];
                        User.UserRole = (UserRoleID)reader["UserRole"];
                        User.Name = reader["UserName"].ToString();
                        User.Email = reader["UserEmail"].ToString();
                        User.Password = reader["UserPassword"].ToString();

                        AllUsers.Add(User);
                    }
                }
                return AllUsers as List<T>; ;
            }
        }

        public bool Save(UserModel entity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                if (this.Get(entity.Id) != null)
                {
                    command.CommandText = string.Format("UPDATE KnowledgeBase.dbo.[User] SET " +
                    "UserName = '{0}', " +
                    "UserEmail = '{1}', " +
                    "UserPassword = '{2}', " +
                    "UserRole = '{3}' " +
                    "WHERE UserId = {4}",
                    entity.Name,
                    entity.Email,
                    entity.Password,
                    entity.UserRole,
                    entity.Id);

                    var result = command.ExecuteNonQuery();

                    if (result == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    command.CommandText = string.Format("INSERT INTO  KnowledgeBase.[dbo].[User]" +
                    "(UserId," +
                    "UserName, " +
                    "UserEmail, " +
                    "UserPassword, " +
                    "UserRole) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                    entity.Id,
                    entity.Name,
                    entity.Email,
                    entity.Password,
                    (int)entity.UserRole);

                    var result = command.ExecuteNonQuery();

                    if (result == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        public List<string> GetNames(T model)
        {
            List<string> listOfNames = new List<string>();
            foreach (var item in model.GetType().GetProperties())
            {
                if (item.Name != "UserId")
                    listOfNames.Add(item.Name);
            }
            return listOfNames;
        }

        public List<string> GetValues(T model)
        {
            List<string> listOfValues = new List<string>();
            foreach (var item in model.GetType().GetProperties())
            {
                if (item.Name != "UserId")
                {
                    listOfValues.Add(item.GetValue(model, null).ToString());
                }
            }
            return listOfValues;
        }

        public bool AddData()
        {
            IUserRepository<UserModel> userRepository = new UserRepository<UserModel>();

            connectionString = @"Data Source=LAPTOP-4GR3EOJP\SQLEXPRESS_AIDA;Initial Catalog=Database1;Integrated Security=True";

            using (IDbConnection connection2 = new SqlConnection(connectionString))
            {
                connection2.Open();
                var AddCommand = connection2.CreateCommand();
                AddCommand.CommandText = "INSERT INTO Database1.[dbo].[User] values(1, 'Ivan', '11111',  1, 'imail.ru'),(2, 'Petrov', '12324567',  3, 'p@gml.com'),(3, 'Sidorov', '22222',  2, 's@ml.ru'),(4, 'Popov', '123',  1, 'p@x.ru'),(5, 'Chirkova', '123',  2, 'c@ml.ru'),(6, 'Mihaylova', '125543',  3, 'm@yae.ru'),(7, 'Kolchin', '1I8Y623',  3, 'ko@ma.ru'),(8, 'Voron', '1231234554',  3, 'vo@ml.ru'),(9, 'Charikova', '1278753',  2, 'ch@l.com'),(10, 'Gluchova', '178768723',  3, 'g@l.ru')";
            }

            return true;
        }

        public bool DeleteData()
        {
            IUserRepository<UserModel> userRepository = new UserRepository<UserModel>();

            connectionString = @"Data Source=LAPTOP-4GR3EOJP\SQLEXPRESS_AIDA;Initial Catalog=Database1;Integrated Security=True";

            using (IDbConnection connection1 = new SqlConnection(connectionString))
            {
                connection1.Open();
                var DeleteCommand = connection1.CreateCommand();
                DeleteCommand.CommandText = "truncate table Database1.[dbo].[User]";
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

using EverythingFootballDemo;
using EverythingFootballDemo.DAL;

namespace EverythingFootballDemo.BLL
{
    public class Users
    {
        private static string connString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationConnectionString"].ConnectionString;
        public bool SaveUser(User user) {
            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                sqlConn.Open();
                var isUserSavedSuccessfully = false;
                try
                {
                    string query = "INSERT INTO Users (UserName, Password, EmailAddress, CreditCardNumber, SecurityCode, CCExpMonth, CCExpYear, NameOnCard, PhoneNumber)";
                    query += " VALUES (@userName, @passWord, @emailAdd, @ccNumber, @secCode, @ccExpMonth, @ccExpYear, @nameOnCard, @phoneNumber)";

                    SqlCommand myCommand = new SqlCommand(query, sqlConn);
                    myCommand.Parameters.AddWithValue("@userName", user.UserName);
                    myCommand.Parameters.AddWithValue("@passWord", user.Password);
                    myCommand.Parameters.AddWithValue("@emailAdd", user.EmailID);
                    myCommand.Parameters.AddWithValue("@ccNumber", user.CreditCardNumber);
                    myCommand.Parameters.AddWithValue("@secCode", user.SecurityCode);
                    myCommand.Parameters.AddWithValue("@ccExpMonth", user.CCExpMonth);
                    myCommand.Parameters.AddWithValue("@ccExpYear", user.CCExpYear);
                    myCommand.Parameters.AddWithValue("@nameOnCard", user.NameOnCard);
                    myCommand.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);

                    myCommand.ExecuteNonQuery();
                    isUserSavedSuccessfully = true;
                    return isUserSavedSuccessfully;
                }
                catch (Exception e)
                {
                    if (e.Message != null)
                    {
                        isUserSavedSuccessfully = false;
                        return isUserSavedSuccessfully;
                    }
                }
                finally {
                    sqlConn.Close();
                }
                return isUserSavedSuccessfully;
            }
        }
        public User GetUserInfo(string userName, string passWord) {
            var userInfo = new User();
            using (SqlConnection sqlConn = new SqlConnection(connString)) {
                try
                {
                    sqlConn.Open();
                    var sqlQuery = "select * from Users where UserName = @userName and Password = @passWord";
                    SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    cmd.Parameters.AddWithValue("@passWord", passWord);

                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    if (dt.Rows.Count > 0)
                    {
                        var listUsers = new List<User>();
                        foreach (DataRow row in dt.Rows)
                        {
                            var objUser = new User();
                            objUser.UserID = Convert.ToInt16(row["ID"].ToString());
                            objUser.UserName = row["UserName"].ToString();
                            objUser.EmailID = row["EmailAddress"].ToString();
                            objUser.CreditCardNumber = Convert.ToInt64(row["CreditCardNumber"].ToString());
                            objUser.SecurityCode = Convert.ToInt16(row["SecurityCode"].ToString());
                            objUser.NameOnCard = row["NameOnCard"].ToString();
                            objUser.CCExpMonth = row["CCExpMonth"].ToString();
                            objUser.CCExpYear = row["CCExpYear"].ToString();
                            objUser.PhoneNumber = Convert.ToInt64(row["PhoneNumber"].ToString());

                            listUsers.Add(objUser);
                        }
                        userInfo = listUsers.FirstOrDefault();
                    }
                    return userInfo;
                }
                catch (Exception e) {
                    if (e.Message != null) {
                        return null;
                    }
                }
                finally {
                    sqlConn.Close();
                }
                return userInfo;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FeastFreedom.Models {
    
    public class UserDBHandler  {
        private SqlConnection conn;
        public void connection() {
            string connString = ConfigurationManager.ConnectionStrings["myConnection"].ToString();
            conn = new SqlConnection(connString);
        }

        public List<User> getUsers() {
            connection();
            List<User> iUser = new List<User>();
            string query = "select * from Users";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);                
            DataTable dt = new DataTable();

            conn.Open();
            adapter.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)  {
                iUser.Add(new User  {
                    UserId = Convert.ToInt32(dr["UserId"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    Password = Convert.ToString(dr["Password"]),
                    BillingAddress = Convert.ToString(dr["BillingAddress"]),
                    SecurityQuestionOne = Convert.ToString(dr["SecurityQuestionOne"]),
                    SecurityQuestionTwo = Convert.ToString(dr["SecurityQuestionTwo"]),
                    RoleId = Convert.ToInt32(dr["RoleId"])
                }) ;
            }

            /*public int UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int RoleId { get; set; }*/

            return iUser;
        }

        public bool createUser(User iUser)  {
            connection();
            string query = "insert into Users (FirstName, LastName, Email, Password) values('" +
                iUser.FirstName + "', '" + iUser.LastName + "', '" + iUser.Email + "', '" + iUser.Password + "', '" + iUser.SecurityQuestionOne + "', '" +
                iUser.SecurityQuestionTwo + "', '" + iUser.RoleId + "')";

            return true;
        }
    }

    
}
﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormConnection.Model
{
    class Student
    {
        public int roll { get; set; }
        public string name { get; set; }
        public int marks { get; set; }

        static string cnn = ConfigurationManager.ConnectionStrings["studentCnn"].ConnectionString;
        #region Insert
        public bool add(Student stu)
        {
            bool isCompleted = false;
            SqlConnection conn = new SqlConnection(cnn);
            try
            {
                string query = "INSERT INTO stud_info (Roll,name,marks) values (@roll,@name,@marks)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@roll", stu.roll);
                cmd.Parameters.AddWithValue("@name", stu.name);
                cmd.Parameters.AddWithValue("@marks", stu.marks);
                conn.Open();
                int rowsAffected=cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isCompleted = true;
                }
                else
                {
                    isCompleted = false;
                }


            }
            catch(Exception e)
            {
                MessageBox.Show($"Error occured {e.Message}");
            }
            finally
            {
                conn.Close();
            }
            return isCompleted;
        }
        #endregion


        #region Select 
        public DataTable read()
        {
            SqlConnection conn = new SqlConnection(cnn);
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT * FROM  stud_info";
                SqlCommand cmd = new SqlCommand(query,conn);
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                conn.Open();
                adapt.Fill(dt);
            }
            catch (Exception)
            {

                
            }
            finally
            {
                conn.Close();
            }
            return dt;

        }
        #endregion

    }
}

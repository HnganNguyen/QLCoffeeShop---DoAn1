﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataProvider
    {
        private const string _connection = @"Data Source=NTHTHAM\SQLEXPRESS;Initial Catalog=QLCoffeeShop;Integrated Security=True;Encrypt=False";

        private static DataProvider _instance;

        public static DataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataProvider();
                }
                return _instance;
            }
        }
        private DataProvider() { }
        public DataTable ExcuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connec = new SqlConnection(_connection))
            {
                connec.Open();
                using (SqlCommand command = new SqlCommand(query, connec))
                {
                    if (parameter != null)
                    {
                        string[] listParams = query.Split(new char[] { ' ', '(', ')', ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                        int i = 0;
                        foreach (string item in listParams)
                        {
                            if (item.StartsWith("@") && i < parameter.Length)
                            {
                                command.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(data);
                    }
                }
                connec.Close();
            }
            return data;
        }

        public int ExcuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connec = new SqlConnection(_connection))
            {
                connec.Open();
                SqlCommand command = new SqlCommand(query, connec);

                if (parameter != null)
                {
                    string[] ListPara = query.Split(new char[] { ' ', ',', '(', ')', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    int i = 0;
                    foreach (string item in ListPara)
                    {
                        if (item.Contains('@') && i < parameter.Length) // Kiểm tra tránh lỗi vượt mảng
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();
                connec.Close();
            }
            return data;
        }

        public object ExcuteScalar(string query, object[] parameter = null)
        {
            object data = null;
            using (SqlConnection connec = new SqlConnection(_connection))
            {
                connec.Open();
                using (SqlCommand command = new SqlCommand(query, connec))
                {
                    if (parameter != null)
                    {
                        string[] ListPara = query.Split(' ');
                        int i = 0;
                        foreach (string item in ListPara)
                        {
                            if (item.Contains('@') && i < parameter.Length)
                            {
                                command.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }
                    data = command.ExecuteScalar();
                }
                connec.Close();
            }
            return data;
        }

    }
}

using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MultiServiceEnterpriseSystem.DAL
{
    public static class DbHelper
    {
        private static string _serverName = @"(localdb)\MSSQLLocalDB";

        public static string GetConnectionString(string dbName)
        {
            var configStr = ConfigurationManager.ConnectionStrings[dbName]?.ConnectionString;
            if (!string.IsNullOrWhiteSpace(configStr))
            {
                return configStr;
            }

            return $"Server={_serverName};Database={dbName};Integrated Security=True;TrustServerCertificate=True";
        }

        public static void SetServerName(string serverName)
        {
            if (!string.IsNullOrWhiteSpace(serverName))
            {
                _serverName = serverName.Trim();
            }
        }

        public static SqlConnection GetConnection(string dbName)
        {
            return new SqlConnection(GetConnectionString(dbName));
        }

        public static bool TestConnection(string dbName, out string message)
        {
            try
            {
                using var conn = GetConnection(dbName);
                conn.Open();
                message = "Kết nối CSDL thành công!";
                return true;
            }
            catch (Exception ex)
            {
                message = $"Lỗi kết nối: {ex.Message}";
                return false;
            }
        }

        public static DataTable ExecuteQuery(string dbName, string sql, params SqlParameter[] parameters)
        {
            using var conn = GetConnection(dbName);
            using var cmd = new SqlCommand(sql, conn);
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static int ExecuteNonQuery(string dbName, string sql, params SqlParameter[] parameters)
        {
            using var conn = GetConnection(dbName);
            using var cmd = new SqlCommand(sql, conn);
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }
            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public static object? ExecuteScalar(string dbName, string sql, params SqlParameter[] parameters)
        {
            using var conn = GetConnection(dbName);
            using var cmd = new SqlCommand(sql, conn);
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }
            conn.Open();
            return cmd.ExecuteScalar();
        }
    }
}


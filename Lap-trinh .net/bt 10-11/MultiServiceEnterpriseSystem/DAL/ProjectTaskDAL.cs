using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MultiServiceEnterpriseSystem.DTO;

namespace MultiServiceEnterpriseSystem.DAL
{
    public class ProjectTaskDAL
    {
        private const string DbName = "ProjectTaskDB";

        public List<DuAnDTO> GetProjects()
        {
            var list = new List<DuAnDTO>();
            const string sql = "SELECT MaDuAn, TenDuAn, NgayBatDau, NgayKetThuc, TrangThai FROM dbo.DuAn ORDER BY MaDuAn DESC";
            using var conn = DbHelper.GetConnection(DbName);
            using var cmd = new SqlCommand(sql, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new DuAnDTO
                {
                    MaDuAn = reader.GetInt32(0),
                    TenDuAn = reader.GetString(1),
                    NgayBatDau = reader.IsDBNull(2) ? null : reader.GetDateTime(2),
                    NgayKetThuc = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                    TrangThai = reader.IsDBNull(4) ? "Đang thực hiện" : reader.GetString(4)
                });
            }
            return list;
        }

        public bool InsertProject(DuAnDTO da)
        {
            const string sql = "INSERT INTO dbo.DuAn (TenDuAn, NgayBatDau, NgayKetThuc, TrangThai) VALUES (@TenDuAn, @NgayBatDau, @NgayKetThuc, @TrangThai)";
            return DbHelper.ExecuteNonQuery(DbName, sql,
                new SqlParameter("@TenDuAn", da.TenDuAn),
                new SqlParameter("@NgayBatDau", (object?)da.NgayBatDau ?? DBNull.Value),
                new SqlParameter("@NgayKetThuc", (object?)da.NgayKetThuc ?? DBNull.Value),
                new SqlParameter("@TrangThai", da.TrangThai ?? "Đang thực hiện")) > 0;
        }

        public List<CongViecDTO> GetTasks(int? projectId = null, string? status = null)
        {
            var list = new List<CongViecDTO>();
            string sql = @"
                SELECT cv.MaTask, cv.MaDuAn, da.TenDuAn, cv.TenTask, cv.MoTa, cv.HanNop, cv.DoUuTien, cv.TrangThai,
                       (SELECT STRING_AGG(tv.HoTen, N', ') 
                        FROM dbo.PhanCong pc 
                        INNER JOIN dbo.ThanhVien tv ON tv.MaThanhVien = pc.MaThanhVien 
                        WHERE pc.MaTask = cv.MaTask) AS NguoiThucHien
                FROM dbo.CongViec cv
                INNER JOIN dbo.DuAn da ON da.MaDuAn = cv.MaDuAn
                WHERE (1=1)";

            var parameters = new List<SqlParameter>();
            if (projectId.HasValue && projectId.Value > 0)
            {
                sql += " AND cv.MaDuAn = @MaDuAn";
                parameters.Add(new SqlParameter("@MaDuAn", projectId.Value));
            }
            if (!string.IsNullOrWhiteSpace(status) && status != "Tất cả")
            {
                sql += " AND cv.TrangThai = @TrangThai";
                parameters.Add(new SqlParameter("@TrangThai", status));
            }
            sql += " ORDER BY cv.MaTask DESC;";

            using var conn = DbHelper.GetConnection(DbName);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddRange(parameters.ToArray());
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new CongViecDTO
                {
                    MaTask = reader.GetInt32(0),
                    MaDuAn = reader.GetInt32(1),
                    TenDuAn = reader.GetString(2),
                    TenTask = reader.GetString(3),
                    MoTa = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                    HanNop = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                    DoUuTien = reader.IsDBNull(6) ? "Trung bình" : reader.GetString(6),
                    TrangThai = reader.IsDBNull(7) ? "To Do" : reader.GetString(7),
                    NguoiThucHien = reader.IsDBNull(8) ? "Chưa giao" : reader.GetString(8)
                });
            }
            return list;
        }

        public bool UpdateTaskStatus(int taskId, string newStatus)
        {
            const string sql = "UPDATE dbo.CongViec SET TrangThai = @TrangThai WHERE MaTask = @MaTask";
            return DbHelper.ExecuteNonQuery(DbName, sql,
                new SqlParameter("@TrangThai", newStatus),
                new SqlParameter("@MaTask", taskId)) > 0;
        }

        public int GetTotalTaskCount()
        {
            const string sql = "SELECT COUNT(*) FROM dbo.CongViec;";
            var res = DbHelper.ExecuteScalar(DbName, sql);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }

        public DataTable GetTaskStatusStats()
        {
            const string sql = @"
                SELECT TrangThai AS [Trạng thái], COUNT(*) AS [Số lượng việc]
                FROM dbo.CongViec
                GROUP BY TrangThai;";
            return DbHelper.ExecuteQuery(DbName, sql);
        }
    }
}


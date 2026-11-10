using bai6._3.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace bai6._3.DAL;

public sealed class ProjectTaskDal
{
    private static SqlConnection CreateConnection() => new(DatabaseConfig.ConnectionString);

    public List<ProjectDto> GetProjects()
    {
        const string sql = "SELECT MaDuAn, TenDuAn, NgayBatDau, NgayKetThuc, TrangThai FROM dbo.DuAn ORDER BY TenDuAn;";
        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var result = new List<ProjectDto>();
        while (reader.Read())
        {
            result.Add(new ProjectDto
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                StartDate = reader.IsDBNull(2) ? null : reader.GetDateTime(2),
                EndDate = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                Status = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
            });
        }
        return result;
    }

    public int SaveProject(ProjectDto project)
    {
        const string insertSql = "INSERT INTO dbo.DuAn (TenDuAn, NgayBatDau, NgayKetThuc, TrangThai) OUTPUT INSERTED.MaDuAn VALUES (@name, @start, @end, @status);";
        const string updateSql = "UPDATE dbo.DuAn SET TenDuAn = @name, NgayBatDau = @start, NgayKetThuc = @end, TrangThai = @status WHERE MaDuAn = @id;";
        using var connection = CreateConnection();
        using var command = new SqlCommand(project.Id == 0 ? insertSql : updateSql, connection);
        command.Parameters.Add("@name", SqlDbType.NVarChar, 150).Value = project.Name;
        command.Parameters.Add("@start", SqlDbType.DateTime).Value = (object?)project.StartDate ?? DBNull.Value;
        command.Parameters.Add("@end", SqlDbType.DateTime).Value = (object?)project.EndDate ?? DBNull.Value;
        command.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = (object?)project.Status ?? DBNull.Value;
        if (project.Id == 0)
        {
            connection.Open();
            return Convert.ToInt32(command.ExecuteScalar());
        }
        command.Parameters.Add("@id", SqlDbType.Int).Value = project.Id;
        connection.Open();
        command.ExecuteNonQuery();
        return project.Id;
    }

    public void DeleteProject(int projectId)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            using (var command = new SqlCommand("DELETE FROM dbo.PhanCong WHERE MaTask IN (SELECT MaTask FROM dbo.CongViec WHERE MaDuAn = @id);", connection, transaction))
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = projectId;
                command.ExecuteNonQuery();
            }
            using (var command = new SqlCommand("DELETE FROM dbo.CongViec WHERE MaDuAn = @id;", connection, transaction))
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = projectId;
                command.ExecuteNonQuery();
            }
            using (var command = new SqlCommand("DELETE FROM dbo.DuAn WHERE MaDuAn = @id;", connection, transaction))
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = projectId;
                command.ExecuteNonQuery();
            }
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public List<MemberDto> GetMembers()
    {
        const string sql = "SELECT MaThanhVien, HoTen, Email, ChuyenMon FROM dbo.ThanhVien ORDER BY HoTen;";
        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var result = new List<MemberDto>();
        while (reader.Read())
        {
            result.Add(new MemberDto
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Specialty = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
            });
        }
        return result;
    }

    public void AddMember(MemberDto member)
    {
        const string sql = "INSERT INTO dbo.ThanhVien (HoTen, Email, ChuyenMon) VALUES (@name, @email, @specialty);";
        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = member.Name;
        command.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = member.Email;
        command.Parameters.Add("@specialty", SqlDbType.NVarChar, 100).Value = string.IsNullOrWhiteSpace(member.Specialty) ? DBNull.Value : member.Specialty;
        connection.Open();
        command.ExecuteNonQuery();
    }

    public List<TaskDto> GetTasks(int? projectId = null, string? status = null)
    {
        const string sql = "SELECT t.MaTask, t.MaDuAn, p.TenDuAn, t.TenTask, t.MoTa, t.HanNop, t.DoUuTien, t.TrangThai, COALESCE(STRING_AGG(m.HoTen, N', '), N'') AS AssignedMembers FROM dbo.CongViec t INNER JOIN dbo.DuAn p ON p.MaDuAn = t.MaDuAn LEFT JOIN dbo.PhanCong a ON a.MaTask = t.MaTask LEFT JOIN dbo.ThanhVien m ON m.MaThanhVien = a.MaThanhVien WHERE (@projectId IS NULL OR t.MaDuAn = @projectId) AND (@status IS NULL OR t.TrangThai = @status) GROUP BY t.MaTask, t.MaDuAn, p.TenDuAn, t.TenTask, t.MoTa, t.HanNop, t.DoUuTien, t.TrangThai ORDER BY t.HanNop, t.TenTask;";
        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@projectId", SqlDbType.Int).Value = (object?)projectId ?? DBNull.Value;
        command.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(status) ? DBNull.Value : status;
        connection.Open();
        using var reader = command.ExecuteReader();
        var result = new List<TaskDto>();
        while (reader.Read())
        {
            result.Add(new TaskDto
            {
                Id = reader.GetInt32(0),
                ProjectId = reader.GetInt32(1),
                ProjectName = reader.GetString(2),
                Name = reader.GetString(3),
                Description = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                DueDate = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                Priority = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                Status = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                AssignedMembers = reader.GetString(8)
            });
        }
        return result;
    }

    public int SaveTask(TaskDto task)
    {
        const string insertSql = "INSERT INTO dbo.CongViec (MaDuAn, TenTask, MoTa, HanNop, DoUuTien, TrangThai) OUTPUT INSERTED.MaTask VALUES (@projectId, @name, @description, @dueDate, @priority, @status);";
        const string updateSql = "UPDATE dbo.CongViec SET MaDuAn = @projectId, TenTask = @name, MoTa = @description, HanNop = @dueDate, DoUuTien = @priority, TrangThai = @status WHERE MaTask = @id;";
        using var connection = CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            int taskId;
            using (var command = new SqlCommand(task.Id == 0 ? insertSql : updateSql, connection, transaction))
            {
                AddTaskParameters(command, task);
                if (task.Id == 0)
                {
                    taskId = Convert.ToInt32(command.ExecuteScalar());
                }
                else
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = task.Id;
                    command.ExecuteNonQuery();
                    taskId = task.Id;
                }
            }

            using (var command = new SqlCommand("DELETE FROM dbo.PhanCong WHERE MaTask = @taskId;", connection, transaction))
            {
                command.Parameters.Add("@taskId", SqlDbType.Int).Value = taskId;
                command.ExecuteNonQuery();
            }
            foreach (var memberId in task.MemberIds.Distinct())
            {
                using var command = new SqlCommand("INSERT INTO dbo.PhanCong (MaTask, MaThanhVien) VALUES (@taskId, @memberId);", connection, transaction);
                command.Parameters.Add("@taskId", SqlDbType.Int).Value = taskId;
                command.Parameters.Add("@memberId", SqlDbType.Int).Value = memberId;
                command.ExecuteNonQuery();
            }
            transaction.Commit();
            return taskId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void DeleteTask(int taskId)
    {
        using var connection = CreateConnection();
        using var command = new SqlCommand("DELETE FROM dbo.CongViec WHERE MaTask = @id;", connection);
        command.Parameters.Add("@id", SqlDbType.Int).Value = taskId;
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void UpdateTaskStatus(int taskId, string status)
    {
        using var connection = CreateConnection();
        using var command = new SqlCommand("UPDATE dbo.CongViec SET TrangThai = @status WHERE MaTask = @id;", connection);
        command.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = status;
        command.Parameters.Add("@id", SqlDbType.Int).Value = taskId;
        connection.Open();
        command.ExecuteNonQuery();
    }

    public List<int> GetTaskMemberIds(int taskId)
    {
        using var connection = CreateConnection();
        using var command = new SqlCommand("SELECT MaThanhVien FROM dbo.PhanCong WHERE MaTask = @taskId;", connection);
        command.Parameters.Add("@taskId", SqlDbType.Int).Value = taskId;
        connection.Open();
        using var reader = command.ExecuteReader();
        var result = new List<int>();
        while (reader.Read())
            result.Add(reader.GetInt32(0));
        return result;
    }

    public List<OverdueTaskDto> GetOverdueTasks()
    {
        const string sql = "SELECT t.MaTask, p.TenDuAn, t.TenTask, t.HanNop, t.TrangThai, COALESCE(STRING_AGG(m.HoTen, N', '), N'') AS AssignedMembers FROM dbo.CongViec t INNER JOIN dbo.DuAn p ON p.MaDuAn = t.MaDuAn LEFT JOIN dbo.PhanCong a ON a.MaTask = t.MaTask LEFT JOIN dbo.ThanhVien m ON m.MaThanhVien = a.MaThanhVien WHERE t.HanNop < GETDATE() AND t.TrangThai <> N'Done' GROUP BY t.MaTask, p.TenDuAn, t.TenTask, t.HanNop, t.TrangThai ORDER BY t.HanNop;";
        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var result = new List<OverdueTaskDto>();
        while (reader.Read())
        {
            result.Add(new OverdueTaskDto
            {
                TaskId = reader.GetInt32(0),
                ProjectName = reader.GetString(1),
                TaskName = reader.GetString(2),
                DueDate = reader.GetDateTime(3),
                Status = reader.GetString(4),
                AssignedMembers = reader.GetString(5)
            });
        }
        return result;
    }

    public List<MemberProgressDto> GetMemberProgress(int projectId)
    {
        const string sql = "SELECT m.MaThanhVien, m.HoTen, COUNT(*) AS TotalTasks, SUM(CASE WHEN t.TrangThai = N'Done' THEN 1 ELSE 0 END) AS CompletedTasks FROM dbo.PhanCong a INNER JOIN dbo.CongViec t ON t.MaTask = a.MaTask INNER JOIN dbo.ThanhVien m ON m.MaThanhVien = a.MaThanhVien WHERE t.MaDuAn = @projectId GROUP BY m.MaThanhVien, m.HoTen ORDER BY m.HoTen;";
        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@projectId", SqlDbType.Int).Value = projectId;
        connection.Open();
        using var reader = command.ExecuteReader();
        var result = new List<MemberProgressDto>();
        while (reader.Read())
        {
            result.Add(new MemberProgressDto
            {
                MemberId = reader.GetInt32(0),
                MemberName = reader.GetString(1),
                TotalTasks = reader.GetInt32(2),
                CompletedTasks = reader.GetInt32(3)
            });
        }
        return result;
    }

    private static void AddTaskParameters(SqlCommand command, TaskDto task)
    {
        command.Parameters.Add("@projectId", SqlDbType.Int).Value = task.ProjectId;
        command.Parameters.Add("@name", SqlDbType.NVarChar, 200).Value = task.Name;
        command.Parameters.Add("@description", SqlDbType.NVarChar, -1).Value = string.IsNullOrWhiteSpace(task.Description) ? DBNull.Value : task.Description;
        command.Parameters.Add("@dueDate", SqlDbType.DateTime).Value = (object?)task.DueDate ?? DBNull.Value;
        command.Parameters.Add("@priority", SqlDbType.NVarChar, 20).Value = (object?)task.Priority ?? DBNull.Value;
        command.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = (object?)task.Status ?? DBNull.Value;
    }
}

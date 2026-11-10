using System;
using System.Collections.Generic;
using System.Data;
using MultiServiceEnterpriseSystem.DAL;
using MultiServiceEnterpriseSystem.DTO;

namespace MultiServiceEnterpriseSystem.BUS
{
    public class ProjectTaskBUS
    {
        private readonly ProjectTaskDAL _dal = new();

        public List<DuAnDTO> GetAllProjects() => _dal.GetProjects();

        public bool AddProject(DuAnDTO da)
        {
            if (string.IsNullOrWhiteSpace(da.TenDuAn))
                throw new ArgumentException("Tên dự án không được để trống!");
            return _dal.InsertProject(da);
        }

        public List<CongViecDTO> GetTasks(int? projectId = null, string? status = null)
        {
            return _dal.GetTasks(projectId, status);
        }

        public bool UpdateTaskStatus(int taskId, string newStatus)
        {
            if (taskId <= 0)
                throw new ArgumentException("Mã task không hợp lệ!");
            return _dal.UpdateTaskStatus(taskId, newStatus);
        }

        public int GetTotalTaskCount() => _dal.GetTotalTaskCount();
        public DataTable GetTaskStatusStats() => _dal.GetTaskStatusStats();
    }
}


using bai6._3.DAL;
using bai6._3.DTO;
using System.Net.Mail;

namespace bai6._3.BUS;

public sealed class ProjectTaskBus
{
    public static readonly string[] TaskStatuses = ["To Do", "In Progress", "Review", "Done"];
    public static readonly string[] ProjectStatuses = ["Đang chạy", "Hoàn thành", "Tạm dừng"];
    public static readonly string[] Priorities = ["Cao", "Trung bình", "Thấp"];

    private readonly ProjectTaskDal _dal = new();

    public List<ProjectDto> GetProjects() => _dal.GetProjects();

    public int SaveProject(ProjectDto project)
    {
        if (string.IsNullOrWhiteSpace(project.Name))
            throw new ArgumentException("Tên dự án không được để trống.");
        if (project.Name.Length > 150)
            throw new ArgumentException("Tên dự án tối đa 150 ký tự.");
        if (project.StartDate.HasValue && project.EndDate.HasValue && project.EndDate < project.StartDate)
            throw new ArgumentException("Ngày kết thúc phải bằng hoặc sau ngày bắt đầu.");
        if (!ProjectStatuses.Contains(project.Status))
            throw new ArgumentException("Trạng thái dự án không hợp lệ.");
        return _dal.SaveProject(project);
    }

    public void DeleteProject(int projectId)
    {
        if (projectId <= 0)
            throw new ArgumentException("Dự án không hợp lệ.");
        _dal.DeleteProject(projectId);
    }

    public List<MemberDto> GetMembers() => _dal.GetMembers();

    public void AddMember(MemberDto member)
    {
        if (string.IsNullOrWhiteSpace(member.Name) || member.Name.Length > 100)
            throw new ArgumentException("Họ tên thành viên phải có từ 1 đến 100 ký tự.");
        if (string.IsNullOrWhiteSpace(member.Email) || member.Email.Length > 100 || !MailAddress.TryCreate(member.Email, out _))
            throw new ArgumentException("Email không hợp lệ hoặc vượt quá 100 ký tự.");
        if (member.Specialty.Length > 100)
            throw new ArgumentException("Chuyên môn tối đa 100 ký tự.");
        _dal.AddMember(member);
    }

    public List<TaskDto> GetTasks(int? projectId = null, string? status = null)
    {
        if (projectId is <= 0)
            throw new ArgumentException("Dự án không hợp lệ.");
        if (!string.IsNullOrWhiteSpace(status) && !TaskStatuses.Contains(status))
            throw new ArgumentException("Trạng thái công việc không hợp lệ.");
        return _dal.GetTasks(projectId, status);
    }

    public int SaveTask(TaskDto task)
    {
        if (task.ProjectId <= 0)
            throw new ArgumentException("Vui lòng chọn dự án.");
        if (string.IsNullOrWhiteSpace(task.Name) || task.Name.Length > 200)
            throw new ArgumentException("Tên công việc phải có từ 1 đến 200 ký tự.");
        if (!Priorities.Contains(task.Priority))
            throw new ArgumentException("Độ ưu tiên không hợp lệ.");
        if (!TaskStatuses.Contains(task.Status))
            throw new ArgumentException("Trạng thái công việc không hợp lệ.");
        return _dal.SaveTask(task);
    }

    public void DeleteTask(int taskId)
    {
        if (taskId <= 0)
            throw new ArgumentException("Công việc không hợp lệ.");
        _dal.DeleteTask(taskId);
    }

    public void UpdateTaskStatus(int taskId, string status)
    {
        if (taskId <= 0 || !TaskStatuses.Contains(status))
            throw new ArgumentException("Công việc hoặc trạng thái không hợp lệ.");
        _dal.UpdateTaskStatus(taskId, status);
    }

    public List<int> GetTaskMemberIds(int taskId)
    {
        if (taskId <= 0)
            throw new ArgumentException("Công việc không hợp lệ.");
        return _dal.GetTaskMemberIds(taskId);
    }

    public List<OverdueTaskDto> GetOverdueTasks() => _dal.GetOverdueTasks();

    public List<MemberProgressDto> GetMemberProgress(int projectId)
    {
        if (projectId <= 0)
            throw new ArgumentException("Vui lòng chọn dự án.");
        return _dal.GetMemberProgress(projectId);
    }
}

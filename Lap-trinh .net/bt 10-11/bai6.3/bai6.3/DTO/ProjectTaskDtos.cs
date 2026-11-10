using System.ComponentModel;

namespace bai6._3.DTO;

public sealed class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = "Đang chạy";
}

public sealed class MemberDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
}

public sealed class TaskDto
{
    [Browsable(false)]
    public int Id { get; set; }
    [Browsable(false)]
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    [Browsable(false)]
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public string Priority { get; set; } = "Trung bình";
    public string Status { get; set; } = "To Do";
    public string AssignedMembers { get; set; } = string.Empty;
    [Browsable(false)]
    public List<int> MemberIds { get; set; } = [];
}

public sealed class OverdueTaskDto
{
    [Browsable(false)]
    public int TaskId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string TaskName { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string AssignedMembers { get; set; } = string.Empty;
}

public sealed class MemberProgressDto
{
    [Browsable(false)]
    public int MemberId { get; set; }
    public string MemberName { get; set; } = string.Empty;
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public decimal CompletionPercent => TotalTasks == 0 ? 0 : Math.Round(CompletedTasks * 100m / TotalTasks, 2);
}

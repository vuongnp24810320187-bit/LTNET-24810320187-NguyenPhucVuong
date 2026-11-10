using bai6._3.BUS;
using bai6._3.DTO;
using Microsoft.Data.SqlClient;

namespace bai6._3
{
    public partial class Form1 : Form
    {
        private readonly ProjectTaskBus _bus = new();
        private bool _loadingTaskSelection;

        public Form1()
        {
            InitializeComponent();
            ConfigureControls();
            WireEvents();
        }

        private void ConfigureControls()
        {
            projectStatusComboBox.Items.AddRange(ProjectTaskBus.ProjectStatuses);
            projectStatusComboBox.SelectedIndex = 0;
            taskPriorityComboBox.Items.AddRange(ProjectTaskBus.Priorities);
            taskPriorityComboBox.SelectedItem = "Trung bình";
            taskStatusComboBox.Items.AddRange(ProjectTaskBus.TaskStatuses);
            taskStatusComboBox.SelectedItem = "To Do";
            taskStatusFilterComboBox.Items.Add("Tất cả");
            taskStatusFilterComboBox.Items.AddRange(ProjectTaskBus.TaskStatuses);
            taskStatusFilterComboBox.SelectedIndex = 0;
        }

        private void WireEvents()
        {
            Load += (_, _) => RunSafely(LoadAll);
            saveProjectButton.Click += (_, _) => RunSafely(SaveProject);
            deleteProjectButton.Click += (_, _) => RunSafely(DeleteProject);
            projectsGrid.SelectionChanged += (_, _) => ShowSelectedProject();
            refreshTasksButton.Click += (_, _) => RunSafely(RefreshTasks);
            saveTaskButton.Click += (_, _) => RunSafely(SaveTask);
            deleteTaskButton.Click += (_, _) => RunSafely(DeleteTask);
            updateTaskStatusButton.Click += (_, _) => RunSafely(UpdateTaskStatus);
            tasksGrid.SelectionChanged += (_, _) => RunSafely(ShowSelectedTask);
            addMemberButton.Click += (_, _) => RunSafely(AddMember);
            refreshReportButton.Click += (_, _) => RunSafely(RefreshReport);
            mainTabs.SelectedIndexChanged += (_, _) => RunSafely(RefreshCurrentTab);
            toDoListBox.Tag = "To Do";
            inProgressListBox.Tag = "In Progress";
            reviewListBox.Tag = "Review";
            doneListBox.Tag = "Done";
            toDoListBox.DoubleClick += KanbanTaskDoubleClick;
            inProgressListBox.DoubleClick += KanbanTaskDoubleClick;
            reviewListBox.DoubleClick += KanbanTaskDoubleClick;
            doneListBox.DoubleClick += KanbanTaskDoubleClick;
        }

        private void LoadAll()
        {
            var projects = _bus.GetProjects();
            projectsGrid.DataSource = projects;
            projectsGrid.ClearSelection();
            projectsGrid.CurrentCell = null;
            FormatProjectGrid();

            var filterProjects = new List<ProjectDto> { new() { Id = 0, Name = "Tất cả dự án" } };
            filterProjects.AddRange(projects);
            projectFilterComboBox.DataSource = filterProjects;
            projectFilterComboBox.DisplayMember = nameof(ProjectDto.Name);
            projectFilterComboBox.ValueMember = nameof(ProjectDto.Id);
            projectFilterComboBox.SelectedIndex = 0;

            taskProjectComboBox.DataSource = new List<ProjectDto>(projects);
            taskProjectComboBox.DisplayMember = nameof(ProjectDto.Name);
            taskProjectComboBox.ValueMember = nameof(ProjectDto.Id);
            reportProjectComboBox.DataSource = new List<ProjectDto>(projects);
            reportProjectComboBox.DisplayMember = nameof(ProjectDto.Name);
            reportProjectComboBox.ValueMember = nameof(ProjectDto.Id);

            var members = _bus.GetMembers();
            membersGrid.DataSource = members;
            membersGrid.ClearSelection();
            membersGrid.CurrentCell = null;
            membersGrid.Columns[nameof(MemberDto.Name)]!.HeaderText = "Họ tên";
            membersGrid.Columns[nameof(MemberDto.Email)]!.HeaderText = "Email";
            membersGrid.Columns[nameof(MemberDto.Specialty)]!.HeaderText = "Chuyên môn";
            membersGrid.Columns[nameof(MemberDto.Id)]!.Visible = false;
            assignedMembersCheckedListBox.DataSource = new List<MemberDto>(members);
            assignedMembersCheckedListBox.DisplayMember = nameof(MemberDto.Name);
            assignedMembersCheckedListBox.ValueMember = nameof(MemberDto.Id);

            RefreshTasks();
            RefreshOverdue();
            RefreshReport();
        }

        private void RefreshCurrentTab()
        {
            if (mainTabs.SelectedTab == tasksTab)
                RefreshTasks();
            else if (mainTabs.SelectedTab == overdueTab)
                RefreshOverdue();
            else if (mainTabs.SelectedTab == reportsTab)
                RefreshReport();
            else if (mainTabs.SelectedTab == kanbanTab)
                RefreshKanban();
        }

        private void RefreshTasks()
        {
            var project = projectFilterComboBox.SelectedItem as ProjectDto;
            var status = taskStatusFilterComboBox.SelectedItem?.ToString();
            var tasks = _bus.GetTasks(project?.Id > 0 ? project.Id : null, status == "Tất cả" ? null : status);
            tasksGrid.DataSource = tasks;
            tasksGrid.ClearSelection();
            tasksGrid.CurrentCell = null;
            FormatTaskGrid();
            RefreshKanban();
        }

        private void RefreshKanban()
        {
            var project = projectFilterComboBox.SelectedItem as ProjectDto;
            var tasks = _bus.GetTasks(project?.Id > 0 ? project.Id : null);
            BindKanbanColumn(toDoListBox, tasks, "To Do");
            BindKanbanColumn(inProgressListBox, tasks, "In Progress");
            BindKanbanColumn(reviewListBox, tasks, "Review");
            BindKanbanColumn(doneListBox, tasks, "Done");
        }

        private static void BindKanbanColumn(ListBox listBox, List<TaskDto> tasks, string status)
        {
            listBox.DisplayMember = nameof(TaskDto.Name);
            listBox.DataSource = tasks.Where(task => task.Status == status).ToList();
        }

        private void KanbanTaskDoubleClick(object? sender, EventArgs e)
        {
            if (sender is not ListBox listBox || listBox.SelectedItem is not TaskDto task || listBox.Tag is not string status)
                return;
            var nextStatus = status switch
            {
                "To Do" => "In Progress",
                "In Progress" => "Review",
                "Review" => "Done",
                _ => "Done"
            };
            _bus.UpdateTaskStatus(task.Id, nextStatus);
            LoadAll();
        }

        private void RefreshOverdue()
        {
            overdueGrid.DataSource = _bus.GetOverdueTasks();
            overdueGrid.Columns[nameof(OverdueTaskDto.ProjectName)]!.HeaderText = "Dự án";
            overdueGrid.Columns[nameof(OverdueTaskDto.TaskName)]!.HeaderText = "Công việc";
            overdueGrid.Columns[nameof(OverdueTaskDto.DueDate)]!.HeaderText = "Hạn nộp";
            overdueGrid.Columns[nameof(OverdueTaskDto.DueDate)]!.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            overdueGrid.Columns[nameof(OverdueTaskDto.Status)]!.HeaderText = "Trạng thái";
            overdueGrid.Columns[nameof(OverdueTaskDto.AssignedMembers)]!.HeaderText = "Thành viên";
        }

        private void RefreshReport()
        {
            if (reportProjectComboBox.SelectedItem is not ProjectDto project || project.Id <= 0)
            {
                progressGrid.DataSource = new List<MemberProgressDto>();
                return;
            }
            progressGrid.DataSource = _bus.GetMemberProgress(project.Id);
            progressGrid.Columns[nameof(MemberProgressDto.MemberName)]!.HeaderText = "Thành viên";
            progressGrid.Columns[nameof(MemberProgressDto.TotalTasks)]!.HeaderText = "Tổng công việc";
            progressGrid.Columns[nameof(MemberProgressDto.CompletedTasks)]!.HeaderText = "Đã hoàn thành";
            progressGrid.Columns[nameof(MemberProgressDto.CompletionPercent)]!.HeaderText = "Hoàn thành (%)";
            progressGrid.Columns[nameof(MemberProgressDto.CompletionPercent)]!.DefaultCellStyle.Format = "N2";
        }

        private void FormatProjectGrid()
        {
            projectsGrid.Columns[nameof(ProjectDto.Id)]!.Visible = false;
            projectsGrid.Columns[nameof(ProjectDto.Name)]!.HeaderText = "Tên dự án";
            projectsGrid.Columns[nameof(ProjectDto.StartDate)]!.HeaderText = "Ngày bắt đầu";
            projectsGrid.Columns[nameof(ProjectDto.StartDate)]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            projectsGrid.Columns[nameof(ProjectDto.EndDate)]!.HeaderText = "Ngày kết thúc";
            projectsGrid.Columns[nameof(ProjectDto.EndDate)]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            projectsGrid.Columns[nameof(ProjectDto.Status)]!.HeaderText = "Trạng thái";
        }

        private void FormatTaskGrid()
        {
            tasksGrid.Columns[nameof(TaskDto.ProjectName)]!.HeaderText = "Dự án";
            tasksGrid.Columns[nameof(TaskDto.Name)]!.HeaderText = "Công việc";
            tasksGrid.Columns[nameof(TaskDto.DueDate)]!.HeaderText = "Hạn nộp";
            tasksGrid.Columns[nameof(TaskDto.DueDate)]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            tasksGrid.Columns[nameof(TaskDto.Priority)]!.HeaderText = "Ưu tiên";
            tasksGrid.Columns[nameof(TaskDto.Status)]!.HeaderText = "Trạng thái";
            tasksGrid.Columns[nameof(TaskDto.AssignedMembers)]!.HeaderText = "Thành viên";
        }

        private void SaveProject()
        {
            var selected = projectsGrid.CurrentRow?.DataBoundItem as ProjectDto;
            _bus.SaveProject(new ProjectDto
            {
                Id = selected?.Id ?? 0,
                Name = projectNameTextBox.Text.Trim(),
                StartDate = projectStartPicker.Checked ? projectStartPicker.Value : null,
                EndDate = projectEndPicker.Checked ? projectEndPicker.Value : null,
                Status = projectStatusComboBox.SelectedItem?.ToString() ?? ProjectTaskBus.ProjectStatuses[0]
            });
            LoadAll();
            MessageBox.Show("Đã lưu dự án.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeleteProject()
        {
            if (projectsGrid.CurrentRow?.DataBoundItem is not ProjectDto project)
                throw new InvalidOperationException("Vui lòng chọn dự án cần xóa.");
            if (MessageBox.Show($"Xóa dự án '{project.Name}' cùng các công việc và phân công liên quan?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            _bus.DeleteProject(project.Id);
            projectNameTextBox.Clear();
            LoadAll();
        }

        private void ShowSelectedProject()
        {
            if (projectsGrid.CurrentRow?.DataBoundItem is not ProjectDto project)
                return;
            projectNameTextBox.Text = project.Name;
            projectStatusComboBox.SelectedItem = project.Status;
            projectStartPicker.Checked = project.StartDate.HasValue;
            if (project.StartDate.HasValue)
                projectStartPicker.Value = project.StartDate.Value;
            projectEndPicker.Checked = project.EndDate.HasValue;
            if (project.EndDate.HasValue)
                projectEndPicker.Value = project.EndDate.Value;
        }

        private void SaveTask()
        {
            if (taskProjectComboBox.SelectedItem is not ProjectDto project)
                throw new InvalidOperationException("Vui lòng tạo và chọn dự án trước.");
            var selected = tasksGrid.CurrentRow?.DataBoundItem as TaskDto;
            var task = new TaskDto
            {
                Id = selected?.Id ?? 0,
                ProjectId = project.Id,
                Name = taskNameTextBox.Text.Trim(),
                Description = taskDescriptionTextBox.Text.Trim(),
                DueDate = taskDueDatePicker.Checked ? taskDueDatePicker.Value : null,
                Priority = taskPriorityComboBox.SelectedItem?.ToString() ?? "Trung bình",
                Status = taskStatusComboBox.SelectedItem?.ToString() ?? "To Do",
                MemberIds = assignedMembersCheckedListBox.CheckedItems.Cast<MemberDto>().Select(member => member.Id).ToList()
            };
            _bus.SaveTask(task);
            LoadAll();
            MessageBox.Show("Đã lưu công việc và phân công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeleteTask()
        {
            if (tasksGrid.CurrentRow?.DataBoundItem is not TaskDto task)
                throw new InvalidOperationException("Vui lòng chọn công việc cần xóa.");
            if (MessageBox.Show($"Xóa công việc '{task.Name}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            _bus.DeleteTask(task.Id);
            LoadAll();
        }

        private void UpdateTaskStatus()
        {
            if (tasksGrid.CurrentRow?.DataBoundItem is not TaskDto task)
                throw new InvalidOperationException("Vui lòng chọn công việc cần chuyển trạng thái.");
            var status = taskStatusComboBox.SelectedItem?.ToString() ?? "To Do";
            _bus.UpdateTaskStatus(task.Id, status);
            LoadAll();
        }

        private void ShowSelectedTask()
        {
            if (_loadingTaskSelection || tasksGrid.CurrentRow?.DataBoundItem is not TaskDto task)
                return;
            _loadingTaskSelection = true;
            try
            {
                taskNameTextBox.Text = task.Name;
                taskDescriptionTextBox.Text = task.Description;
                taskDueDatePicker.Checked = task.DueDate.HasValue;
                if (task.DueDate.HasValue)
                    taskDueDatePicker.Value = task.DueDate.Value;
                taskPriorityComboBox.SelectedItem = task.Priority;
                taskStatusComboBox.SelectedItem = task.Status;
                for (var index = 0; index < taskProjectComboBox.Items.Count; index++)
                {
                    if (taskProjectComboBox.Items[index] is ProjectDto project && project.Id == task.ProjectId)
                    {
                        taskProjectComboBox.SelectedIndex = index;
                        break;
                    }
                }
                var assignedMemberIds = _bus.GetTaskMemberIds(task.Id).ToHashSet();
                for (var index = 0; index < assignedMembersCheckedListBox.Items.Count; index++)
                {
                    if (assignedMembersCheckedListBox.Items[index] is MemberDto member)
                        assignedMembersCheckedListBox.SetItemChecked(index, assignedMemberIds.Contains(member.Id));
                }
            }
            finally
            {
                _loadingTaskSelection = false;
            }
        }

        private void AddMember()
        {
            _bus.AddMember(new MemberDto
            {
                Name = memberNameTextBox.Text.Trim(),
                Email = memberEmailTextBox.Text.Trim(),
                Specialty = memberSpecialtyTextBox.Text.Trim()
            });
            memberNameTextBox.Clear();
            memberEmailTextBox.Clear();
            memberSpecialtyTextBox.Clear();
            LoadAll();
        }

        private void RunSafely(Action action)
        {
            try
            {
                action();
            }
            catch (SqlException exception)
            {
                MessageBox.Show($"Không thể truy cập dữ liệu. Kiểm tra SQL Server/LocalDB, chạy SQL\\CreateProjectTaskDB.sql và thử lại.\n\n{exception.Message}", "Lỗi cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

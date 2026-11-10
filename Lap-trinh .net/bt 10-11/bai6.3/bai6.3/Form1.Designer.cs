namespace bai6._3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl mainTabs = null!;
        private TabPage projectsTab = null!;
        private TabPage tasksTab = null!;
        private TabPage membersTab = null!;
        private TabPage overdueTab = null!;
        private TabPage reportsTab = null!;
        private TabPage kanbanTab = null!;
        private GroupBox toDoGroupBox = null!;
        private GroupBox inProgressGroupBox = null!;
        private GroupBox reviewGroupBox = null!;
        private GroupBox doneGroupBox = null!;
        private ListBox toDoListBox = null!;
        private ListBox inProgressListBox = null!;
        private ListBox reviewListBox = null!;
        private ListBox doneListBox = null!;
        private DataGridView projectsGrid = null!;
        private TextBox projectNameTextBox = null!;
        private DateTimePicker projectStartPicker = null!;
        private DateTimePicker projectEndPicker = null!;
        private ComboBox projectStatusComboBox = null!;
        private Button saveProjectButton = null!;
        private Button deleteProjectButton = null!;
        private ComboBox projectFilterComboBox = null!;
        private ComboBox taskStatusFilterComboBox = null!;
        private Button refreshTasksButton = null!;
        private DataGridView tasksGrid = null!;
        private ComboBox taskProjectComboBox = null!;
        private TextBox taskNameTextBox = null!;
        private TextBox taskDescriptionTextBox = null!;
        private DateTimePicker taskDueDatePicker = null!;
        private ComboBox taskPriorityComboBox = null!;
        private ComboBox taskStatusComboBox = null!;
        private CheckedListBox assignedMembersCheckedListBox = null!;
        private Button saveTaskButton = null!;
        private Button deleteTaskButton = null!;
        private Button updateTaskStatusButton = null!;
        private DataGridView membersGrid = null!;
        private TextBox memberNameTextBox = null!;
        private TextBox memberEmailTextBox = null!;
        private TextBox memberSpecialtyTextBox = null!;
        private Button addMemberButton = null!;
        private DataGridView overdueGrid = null!;
        private ComboBox reportProjectComboBox = null!;
        private Button refreshReportButton = null!;
        private DataGridView progressGrid = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mainTabs = new TabControl();
            projectsTab = new TabPage();
            tasksTab = new TabPage();
            membersTab = new TabPage();
            overdueTab = new TabPage();
            reportsTab = new TabPage();
            kanbanTab = new TabPage();
            toDoGroupBox = new GroupBox();
            inProgressGroupBox = new GroupBox();
            reviewGroupBox = new GroupBox();
            doneGroupBox = new GroupBox();
            toDoListBox = new ListBox();
            inProgressListBox = new ListBox();
            reviewListBox = new ListBox();
            doneListBox = new ListBox();
            projectsGrid = new DataGridView();
            projectNameTextBox = new TextBox();
            projectStartPicker = new DateTimePicker();
            projectEndPicker = new DateTimePicker();
            projectStatusComboBox = new ComboBox();
            saveProjectButton = new Button();
            deleteProjectButton = new Button();
            projectFilterComboBox = new ComboBox();
            taskStatusFilterComboBox = new ComboBox();
            refreshTasksButton = new Button();
            tasksGrid = new DataGridView();
            taskProjectComboBox = new ComboBox();
            taskNameTextBox = new TextBox();
            taskDescriptionTextBox = new TextBox();
            taskDueDatePicker = new DateTimePicker();
            taskPriorityComboBox = new ComboBox();
            taskStatusComboBox = new ComboBox();
            assignedMembersCheckedListBox = new CheckedListBox();
            saveTaskButton = new Button();
            deleteTaskButton = new Button();
            updateTaskStatusButton = new Button();
            membersGrid = new DataGridView();
            memberNameTextBox = new TextBox();
            memberEmailTextBox = new TextBox();
            memberSpecialtyTextBox = new TextBox();
            addMemberButton = new Button();
            overdueGrid = new DataGridView();
            reportProjectComboBox = new ComboBox();
            refreshReportButton = new Button();
            progressGrid = new DataGridView();
            mainTabs.SuspendLayout();
            projectsTab.SuspendLayout();
            tasksTab.SuspendLayout();
            membersTab.SuspendLayout();
            overdueTab.SuspendLayout();
            reportsTab.SuspendLayout();
            kanbanTab.SuspendLayout();
            toDoGroupBox.SuspendLayout();
            inProgressGroupBox.SuspendLayout();
            reviewGroupBox.SuspendLayout();
            doneGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)projectsGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tasksGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)membersGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)overdueGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)progressGrid).BeginInit();
            SuspendLayout();
            // 
            // mainTabs
            // 
            mainTabs.Controls.Add(projectsTab);
            mainTabs.Controls.Add(tasksTab);
            mainTabs.Controls.Add(membersTab);
            mainTabs.Controls.Add(overdueTab);
            mainTabs.Controls.Add(reportsTab);
            mainTabs.Controls.Add(kanbanTab);
            mainTabs.Dock = DockStyle.Fill;
            mainTabs.Font = new Font("Segoe UI", 10F);
            mainTabs.Location = new Point(0, 0);
            mainTabs.Name = "mainTabs";
            mainTabs.SelectedIndex = 0;
            mainTabs.Size = new Size(1180, 740);
            mainTabs.TabIndex = 0;
            // 
            // projectsTab
            // 
            projectsTab.Controls.Add(projectsGrid);
            projectsTab.Controls.Add(projectNameTextBox);
            projectsTab.Controls.Add(projectStartPicker);
            projectsTab.Controls.Add(projectEndPicker);
            projectsTab.Controls.Add(projectStatusComboBox);
            projectsTab.Controls.Add(saveProjectButton);
            projectsTab.Controls.Add(deleteProjectButton);
            projectsTab.Controls.Add(new Label { AutoSize = true, Location = new Point(830, 34), Text = "Tên dự án" });
            projectsTab.Controls.Add(new Label { AutoSize = true, Location = new Point(830, 106), Text = "Ngày bắt đầu" });
            projectsTab.Controls.Add(new Label { AutoSize = true, Location = new Point(830, 178), Text = "Ngày kết thúc" });
            projectsTab.Controls.Add(new Label { AutoSize = true, Location = new Point(830, 250), Text = "Trạng thái" });
            projectsTab.Location = new Point(4, 29);
            projectsTab.Name = "projectsTab";
            projectsTab.Padding = new Padding(3);
            projectsTab.Size = new Size(1172, 707);
            projectsTab.Text = "Dự án";
            projectsTab.UseVisualStyleBackColor = true;
            // 
            // projectsGrid
            // 
            projectsGrid.AllowUserToAddRows = false;
            projectsGrid.AllowUserToDeleteRows = false;
            projectsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            projectsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            projectsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            projectsGrid.Location = new Point(12, 12);
            projectsGrid.MultiSelect = false;
            projectsGrid.Name = "projectsGrid";
            projectsGrid.ReadOnly = true;
            projectsGrid.RowHeadersVisible = false;
            projectsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            projectsGrid.Size = new Size(795, 675);
            projectsGrid.TabIndex = 0;
            // 
            // projectNameTextBox
            // 
            projectNameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            projectNameTextBox.Location = new Point(830, 58);
            projectNameTextBox.MaxLength = 150;
            projectNameTextBox.Name = "projectNameTextBox";
            projectNameTextBox.Size = new Size(320, 25);
            projectNameTextBox.TabIndex = 1;
            // 
            // projectStartPicker
            // 
            projectStartPicker.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            projectStartPicker.Format = DateTimePickerFormat.Short;
            projectStartPicker.Location = new Point(830, 130);
            projectStartPicker.Name = "projectStartPicker";
            projectStartPicker.ShowCheckBox = true;
            projectStartPicker.Size = new Size(320, 25);
            projectStartPicker.TabIndex = 2;
            // 
            // projectEndPicker
            // 
            projectEndPicker.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            projectEndPicker.Format = DateTimePickerFormat.Short;
            projectEndPicker.Location = new Point(830, 202);
            projectEndPicker.Name = "projectEndPicker";
            projectEndPicker.ShowCheckBox = true;
            projectEndPicker.Size = new Size(320, 25);
            projectEndPicker.TabIndex = 3;
            // 
            // projectStatusComboBox
            // 
            projectStatusComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            projectStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            projectStatusComboBox.FormattingEnabled = true;
            projectStatusComboBox.Location = new Point(830, 274);
            projectStatusComboBox.Name = "projectStatusComboBox";
            projectStatusComboBox.Size = new Size(320, 25);
            projectStatusComboBox.TabIndex = 4;
            // 
            // saveProjectButton
            // 
            saveProjectButton.Location = new Point(830, 322);
            saveProjectButton.Name = "saveProjectButton";
            saveProjectButton.Size = new Size(150, 36);
            saveProjectButton.TabIndex = 5;
            saveProjectButton.Text = "Lưu dự án";
            saveProjectButton.UseVisualStyleBackColor = true;
            // 
            // deleteProjectButton
            // 
            deleteProjectButton.Location = new Point(992, 322);
            deleteProjectButton.Name = "deleteProjectButton";
            deleteProjectButton.Size = new Size(158, 36);
            deleteProjectButton.TabIndex = 6;
            deleteProjectButton.Text = "Xóa dự án";
            deleteProjectButton.UseVisualStyleBackColor = true;
            // 
            // tasksTab
            // 
            tasksTab.Controls.Add(projectFilterComboBox);
            tasksTab.Controls.Add(taskStatusFilterComboBox);
            tasksTab.Controls.Add(refreshTasksButton);
            tasksTab.Controls.Add(tasksGrid);
            tasksTab.Controls.Add(taskProjectComboBox);
            tasksTab.Controls.Add(taskNameTextBox);
            tasksTab.Controls.Add(taskDescriptionTextBox);
            tasksTab.Controls.Add(taskDueDatePicker);
            tasksTab.Controls.Add(taskPriorityComboBox);
            tasksTab.Controls.Add(taskStatusComboBox);
            tasksTab.Controls.Add(assignedMembersCheckedListBox);
            tasksTab.Controls.Add(saveTaskButton);
            tasksTab.Controls.Add(deleteTaskButton);
            tasksTab.Controls.Add(updateTaskStatusButton);
            tasksTab.Controls.Add(new Label { AutoSize = true, Location = new Point(12, 13), Text = "Lọc dự án" });
            tasksTab.Controls.Add(new Label { AutoSize = true, Location = new Point(267, 13), Text = "Trạng thái" });
            tasksTab.Controls.Add(new Label { AutoSize = true, Location = new Point(850, 13), Text = "Dự án" });
            tasksTab.Controls.Add(new Label { AutoSize = true, Location = new Point(850, 77), Text = "Tên công việc" });
            tasksTab.Controls.Add(new Label { AutoSize = true, Location = new Point(850, 141), Text = "Mô tả" });
            tasksTab.Controls.Add(new Label { AutoSize = true, Location = new Point(850, 253), Text = "Hạn nộp" });
            tasksTab.Controls.Add(new Label { AutoSize = true, Location = new Point(850, 317), Text = "Ưu tiên" });
            tasksTab.Controls.Add(new Label { AutoSize = true, Location = new Point(1005, 317), Text = "Trạng thái" });
            tasksTab.Controls.Add(new Label { AutoSize = true, Location = new Point(850, 381), Text = "Phân công thành viên" });
            tasksTab.Location = new Point(4, 29);
            tasksTab.Name = "tasksTab";
            tasksTab.Padding = new Padding(3);
            tasksTab.Size = new Size(1172, 707);
            tasksTab.Text = "Công việc";
            tasksTab.UseVisualStyleBackColor = true;
            // 
            // projectFilterComboBox
            // 
            projectFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            projectFilterComboBox.FormattingEnabled = true;
            projectFilterComboBox.Location = new Point(12, 37);
            projectFilterComboBox.Name = "projectFilterComboBox";
            projectFilterComboBox.Size = new Size(235, 25);
            projectFilterComboBox.TabIndex = 0;
            // 
            // taskStatusFilterComboBox
            // 
            taskStatusFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            taskStatusFilterComboBox.FormattingEnabled = true;
            taskStatusFilterComboBox.Location = new Point(267, 37);
            taskStatusFilterComboBox.Name = "taskStatusFilterComboBox";
            taskStatusFilterComboBox.Size = new Size(180, 25);
            taskStatusFilterComboBox.TabIndex = 1;
            // 
            // refreshTasksButton
            // 
            refreshTasksButton.Location = new Point(460, 34);
            refreshTasksButton.Name = "refreshTasksButton";
            refreshTasksButton.Size = new Size(105, 31);
            refreshTasksButton.TabIndex = 2;
            refreshTasksButton.Text = "Lọc / Tải lại";
            refreshTasksButton.UseVisualStyleBackColor = true;
            // 
            // tasksGrid
            // 
            tasksGrid.AllowUserToAddRows = false;
            tasksGrid.AllowUserToDeleteRows = false;
            tasksGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            tasksGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tasksGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tasksGrid.Location = new Point(12, 80);
            tasksGrid.MultiSelect = false;
            tasksGrid.Name = "tasksGrid";
            tasksGrid.ReadOnly = true;
            tasksGrid.RowHeadersVisible = false;
            tasksGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tasksGrid.Size = new Size(810, 607);
            tasksGrid.TabIndex = 3;
            // 
            // taskProjectComboBox
            // 
            taskProjectComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            taskProjectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            taskProjectComboBox.FormattingEnabled = true;
            taskProjectComboBox.Location = new Point(850, 37);
            taskProjectComboBox.Name = "taskProjectComboBox";
            taskProjectComboBox.Size = new Size(300, 25);
            taskProjectComboBox.TabIndex = 4;
            // 
            // taskNameTextBox
            // 
            taskNameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            taskNameTextBox.Location = new Point(850, 101);
            taskNameTextBox.MaxLength = 200;
            taskNameTextBox.Name = "taskNameTextBox";
            taskNameTextBox.Size = new Size(300, 25);
            taskNameTextBox.TabIndex = 5;
            // 
            // taskDescriptionTextBox
            // 
            taskDescriptionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            taskDescriptionTextBox.Location = new Point(850, 165);
            taskDescriptionTextBox.Multiline = true;
            taskDescriptionTextBox.Name = "taskDescriptionTextBox";
            taskDescriptionTextBox.ScrollBars = ScrollBars.Vertical;
            taskDescriptionTextBox.Size = new Size(300, 76);
            taskDescriptionTextBox.TabIndex = 6;
            // 
            // taskDueDatePicker
            // 
            taskDueDatePicker.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            taskDueDatePicker.Format = DateTimePickerFormat.Short;
            taskDueDatePicker.Location = new Point(850, 277);
            taskDueDatePicker.Name = "taskDueDatePicker";
            taskDueDatePicker.ShowCheckBox = true;
            taskDueDatePicker.Size = new Size(300, 25);
            taskDueDatePicker.TabIndex = 7;
            // 
            // taskPriorityComboBox
            // 
            taskPriorityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            taskPriorityComboBox.FormattingEnabled = true;
            taskPriorityComboBox.Location = new Point(850, 341);
            taskPriorityComboBox.Name = "taskPriorityComboBox";
            taskPriorityComboBox.Size = new Size(145, 25);
            taskPriorityComboBox.TabIndex = 8;
            // 
            // taskStatusComboBox
            // 
            taskStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            taskStatusComboBox.FormattingEnabled = true;
            taskStatusComboBox.Location = new Point(1005, 341);
            taskStatusComboBox.Name = "taskStatusComboBox";
            taskStatusComboBox.Size = new Size(145, 25);
            taskStatusComboBox.TabIndex = 9;
            // 
            // assignedMembersCheckedListBox
            // 
            assignedMembersCheckedListBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            assignedMembersCheckedListBox.FormattingEnabled = true;
            assignedMembersCheckedListBox.Location = new Point(850, 405);
            assignedMembersCheckedListBox.Name = "assignedMembersCheckedListBox";
            assignedMembersCheckedListBox.Size = new Size(300, 112);
            assignedMembersCheckedListBox.TabIndex = 10;
            // 
            // saveTaskButton
            // 
            saveTaskButton.Location = new Point(850, 532);
            saveTaskButton.Name = "saveTaskButton";
            saveTaskButton.Size = new Size(142, 36);
            saveTaskButton.TabIndex = 11;
            saveTaskButton.Text = "Lưu công việc";
            saveTaskButton.UseVisualStyleBackColor = true;
            // 
            // deleteTaskButton
            // 
            deleteTaskButton.Location = new Point(1005, 532);
            deleteTaskButton.Name = "deleteTaskButton";
            deleteTaskButton.Size = new Size(145, 36);
            deleteTaskButton.TabIndex = 12;
            deleteTaskButton.Text = "Xóa";
            deleteTaskButton.UseVisualStyleBackColor = true;
            // 
            // updateTaskStatusButton
            // 
            updateTaskStatusButton.Location = new Point(850, 582);
            updateTaskStatusButton.Name = "updateTaskStatusButton";
            updateTaskStatusButton.Size = new Size(300, 36);
            updateTaskStatusButton.TabIndex = 13;
            updateTaskStatusButton.Text = "Cập nhật trạng thái";
            updateTaskStatusButton.UseVisualStyleBackColor = true;
            // 
            // membersTab
            // 
            membersTab.Controls.Add(membersGrid);
            membersTab.Controls.Add(memberNameTextBox);
            membersTab.Controls.Add(memberEmailTextBox);
            membersTab.Controls.Add(memberSpecialtyTextBox);
            membersTab.Controls.Add(addMemberButton);
            membersTab.Controls.Add(new Label { AutoSize = true, Location = new Point(830, 34), Text = "Họ tên" });
            membersTab.Controls.Add(new Label { AutoSize = true, Location = new Point(830, 106), Text = "Email" });
            membersTab.Controls.Add(new Label { AutoSize = true, Location = new Point(830, 178), Text = "Chuyên môn" });
            membersTab.Location = new Point(4, 29);
            membersTab.Name = "membersTab";
            membersTab.Padding = new Padding(3);
            membersTab.Size = new Size(1172, 707);
            membersTab.Text = "Thành viên";
            membersTab.UseVisualStyleBackColor = true;
            // 
            // membersGrid
            // 
            membersGrid.AllowUserToAddRows = false;
            membersGrid.AllowUserToDeleteRows = false;
            membersGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            membersGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            membersGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            membersGrid.Location = new Point(12, 12);
            membersGrid.MultiSelect = false;
            membersGrid.Name = "membersGrid";
            membersGrid.ReadOnly = true;
            membersGrid.RowHeadersVisible = false;
            membersGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            membersGrid.Size = new Size(795, 675);
            membersGrid.TabIndex = 0;
            // 
            // memberNameTextBox
            // 
            memberNameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            memberNameTextBox.Location = new Point(830, 58);
            memberNameTextBox.MaxLength = 100;
            memberNameTextBox.Name = "memberNameTextBox";
            memberNameTextBox.Size = new Size(320, 25);
            memberNameTextBox.TabIndex = 1;
            // 
            // memberEmailTextBox
            // 
            memberEmailTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            memberEmailTextBox.Location = new Point(830, 130);
            memberEmailTextBox.MaxLength = 100;
            memberEmailTextBox.Name = "memberEmailTextBox";
            memberEmailTextBox.Size = new Size(320, 25);
            memberEmailTextBox.TabIndex = 2;
            // 
            // memberSpecialtyTextBox
            // 
            memberSpecialtyTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            memberSpecialtyTextBox.Location = new Point(830, 202);
            memberSpecialtyTextBox.MaxLength = 100;
            memberSpecialtyTextBox.Name = "memberSpecialtyTextBox";
            memberSpecialtyTextBox.Size = new Size(320, 25);
            memberSpecialtyTextBox.TabIndex = 3;
            // 
            // addMemberButton
            // 
            addMemberButton.Location = new Point(830, 250);
            addMemberButton.Name = "addMemberButton";
            addMemberButton.Size = new Size(320, 36);
            addMemberButton.TabIndex = 4;
            addMemberButton.Text = "Thêm thành viên";
            addMemberButton.UseVisualStyleBackColor = true;
            // 
            // overdueTab
            // 
            overdueTab.Controls.Add(overdueGrid);
            overdueTab.Location = new Point(4, 29);
            overdueTab.Name = "overdueTab";
            overdueTab.Padding = new Padding(12);
            overdueTab.Size = new Size(1172, 707);
            overdueTab.Text = "Quá hạn";
            overdueTab.UseVisualStyleBackColor = true;
            // 
            // overdueGrid
            // 
            overdueGrid.AllowUserToAddRows = false;
            overdueGrid.AllowUserToDeleteRows = false;
            overdueGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            overdueGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            overdueGrid.Dock = DockStyle.Fill;
            overdueGrid.Name = "overdueGrid";
            overdueGrid.ReadOnly = true;
            overdueGrid.RowHeadersVisible = false;
            overdueGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            overdueGrid.TabIndex = 0;
            // 
            // reportsTab
            // 
            reportsTab.Controls.Add(reportProjectComboBox);
            reportsTab.Controls.Add(refreshReportButton);
            reportsTab.Controls.Add(progressGrid);
            reportsTab.Controls.Add(new Label { AutoSize = true, Location = new Point(12, 18), Text = "Dự án cần thống kê" });
            reportsTab.Location = new Point(4, 29);
            reportsTab.Name = "reportsTab";
            reportsTab.Padding = new Padding(12);
            reportsTab.Size = new Size(1172, 707);
            reportsTab.Text = "Báo cáo tiến độ";
            reportsTab.UseVisualStyleBackColor = true;
            // 
            // reportProjectComboBox
            // 
            reportProjectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            reportProjectComboBox.FormattingEnabled = true;
            reportProjectComboBox.Location = new Point(12, 45);
            reportProjectComboBox.Name = "reportProjectComboBox";
            reportProjectComboBox.Size = new Size(300, 25);
            reportProjectComboBox.TabIndex = 0;
            // 
            // refreshReportButton
            // 
            refreshReportButton.Location = new Point(328, 42);
            refreshReportButton.Name = "refreshReportButton";
            refreshReportButton.Size = new Size(150, 31);
            refreshReportButton.TabIndex = 1;
            refreshReportButton.Text = "Thống kê";
            refreshReportButton.UseVisualStyleBackColor = true;
            // 
            // progressGrid
            // 
            progressGrid.AllowUserToAddRows = false;
            progressGrid.AllowUserToDeleteRows = false;
            progressGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            progressGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            progressGrid.Location = new Point(12, 88);
            progressGrid.Name = "progressGrid";
            progressGrid.ReadOnly = true;
            progressGrid.RowHeadersVisible = false;
            progressGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            progressGrid.Size = new Size(1148, 599);
            progressGrid.TabIndex = 2;
            // 
            // kanbanTab
            // 
            kanbanTab.Controls.Add(toDoGroupBox);
            kanbanTab.Controls.Add(inProgressGroupBox);
            kanbanTab.Controls.Add(reviewGroupBox);
            kanbanTab.Controls.Add(doneGroupBox);
            kanbanTab.Location = new Point(4, 29);
            kanbanTab.Name = "kanbanTab";
            kanbanTab.Padding = new Padding(3);
            kanbanTab.Size = new Size(1172, 707);
            kanbanTab.Text = "Kanban";
            kanbanTab.UseVisualStyleBackColor = true;
            // 
            // toDoGroupBox
            // 
            toDoGroupBox.Controls.Add(toDoListBox);
            toDoGroupBox.Location = new Point(8, 6);
            toDoGroupBox.Name = "toDoGroupBox";
            toDoGroupBox.Size = new Size(280, 670);
            toDoGroupBox.TabIndex = 0;
            toDoGroupBox.TabStop = false;
            toDoGroupBox.Text = "Cần làm";
            // 
            // inProgressGroupBox
            // 
            inProgressGroupBox.Controls.Add(inProgressListBox);
            inProgressGroupBox.Location = new Point(294, 6);
            inProgressGroupBox.Name = "inProgressGroupBox";
            inProgressGroupBox.Size = new Size(280, 670);
            inProgressGroupBox.TabIndex = 1;
            inProgressGroupBox.TabStop = false;
            inProgressGroupBox.Text = "Đang thực hiện";
            // 
            // reviewGroupBox
            // 
            reviewGroupBox.Controls.Add(reviewListBox);
            reviewGroupBox.Location = new Point(580, 6);
            reviewGroupBox.Name = "reviewGroupBox";
            reviewGroupBox.Size = new Size(280, 670);
            reviewGroupBox.TabIndex = 2;
            reviewGroupBox.TabStop = false;
            reviewGroupBox.Text = "Đợi duyệt";
            // 
            // doneGroupBox
            // 
            doneGroupBox.Controls.Add(doneListBox);
            doneGroupBox.Location = new Point(866, 6);
            doneGroupBox.Name = "doneGroupBox";
            doneGroupBox.Size = new Size(280, 670);
            doneGroupBox.TabIndex = 3;
            doneGroupBox.TabStop = false;
            doneGroupBox.Text = "Đã hoàn thành";
            // 
            // toDoListBox
            // 
            toDoListBox.Dock = DockStyle.Fill;
            toDoListBox.FormattingEnabled = true;
            toDoListBox.ItemHeight = 17;
            toDoListBox.Location = new Point(3, 23);
            toDoListBox.Name = "toDoListBox";
            toDoListBox.Size = new Size(274, 644);
            toDoListBox.TabIndex = 0;
            // 
            // inProgressListBox
            // 
            inProgressListBox.Dock = DockStyle.Fill;
            inProgressListBox.FormattingEnabled = true;
            inProgressListBox.ItemHeight = 17;
            inProgressListBox.Location = new Point(3, 23);
            inProgressListBox.Name = "inProgressListBox";
            inProgressListBox.Size = new Size(274, 644);
            inProgressListBox.TabIndex = 0;
            // 
            // reviewListBox
            // 
            reviewListBox.Dock = DockStyle.Fill;
            reviewListBox.FormattingEnabled = true;
            reviewListBox.ItemHeight = 17;
            reviewListBox.Location = new Point(3, 23);
            reviewListBox.Name = "reviewListBox";
            reviewListBox.Size = new Size(274, 644);
            reviewListBox.TabIndex = 0;
            // 
            // doneListBox
            // 
            doneListBox.Dock = DockStyle.Fill;
            doneListBox.FormattingEnabled = true;
            doneListBox.ItemHeight = 17;
            doneListBox.Location = new Point(3, 23);
            doneListBox.Name = "doneListBox";
            doneListBox.Size = new Size(274, 644);
            doneListBox.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1180, 740);
            Controls.Add(mainTabs);
            MinimumSize = new Size(1050, 680);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý công việc và tiến độ dự án";
            mainTabs.ResumeLayout(false);
            projectsTab.ResumeLayout(false);
            projectsTab.PerformLayout();
            tasksTab.ResumeLayout(false);
            tasksTab.PerformLayout();
            membersTab.ResumeLayout(false);
            membersTab.PerformLayout();
            overdueTab.ResumeLayout(false);
            reportsTab.ResumeLayout(false);
            reportsTab.PerformLayout();
            kanbanTab.ResumeLayout(false);
            toDoGroupBox.ResumeLayout(false);
            inProgressGroupBox.ResumeLayout(false);
            reviewGroupBox.ResumeLayout(false);
            doneGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)projectsGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)tasksGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)membersGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)overdueGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)progressGrid).EndInit();
            ResumeLayout(false);
        }
    }
}

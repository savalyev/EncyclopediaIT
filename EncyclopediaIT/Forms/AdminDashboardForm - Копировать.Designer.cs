namespace EncyclopediaIT.Forms
{
    partial class AdminDashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.adminTabControl = new System.Windows.Forms.TabControl();
            this.usersTabPage = new System.Windows.Forms.TabPage();
            this.refreshUsersButton = new System.Windows.Forms.Button();
            this.deleteUserButton = new System.Windows.Forms.Button();
            this.blockUserButton = new System.Windows.Forms.Button();
            this.usersDataGridView = new System.Windows.Forms.DataGridView();
            this.contentTabPage = new System.Windows.Forms.TabPage();
            this.refreshTechButton = new System.Windows.Forms.Button();
            this.deleteTechButton = new System.Windows.Forms.Button();
            this.editTechButton = new System.Windows.Forms.Button();
            this.addTechButton = new System.Windows.Forms.Button();
            this.technologiesDataGridView = new System.Windows.Forms.DataGridView();
            this.commentsTabPage = new System.Windows.Forms.TabPage();
            this.refreshCommentsButton = new System.Windows.Forms.Button();
            this.deleteCommentButton = new System.Windows.Forms.Button();
            this.approveCommentButton = new System.Windows.Forms.Button();
            this.commentsDataGridView = new System.Windows.Forms.DataGridView();
            this.reportsTabPage = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.exportReportButton = new System.Windows.Forms.Button();
            this.reportDataGridView = new System.Windows.Forms.DataGridView();
            this.generateReportButton = new System.Windows.Forms.Button();
            this.toDatePicker = new System.Windows.Forms.DateTimePicker();
            this.fromDatePicker = new System.Windows.Forms.DateTimePicker();
            this.reportTypeComboBox = new System.Windows.Forms.ComboBox();
            this.adminTabControl.SuspendLayout();
            this.usersTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).BeginInit();
            this.contentTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.technologiesDataGridView)).BeginInit();
            this.commentsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commentsDataGridView)).BeginInit();
            this.reportsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reportDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // adminTabControl
            // 
            this.adminTabControl.Controls.Add(this.usersTabPage);
            this.adminTabControl.Controls.Add(this.contentTabPage);
            this.adminTabControl.Controls.Add(this.commentsTabPage);
            this.adminTabControl.Controls.Add(this.reportsTabPage);
            this.adminTabControl.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.adminTabControl.Location = new System.Drawing.Point(3, 12);
            this.adminTabControl.Name = "adminTabControl";
            this.adminTabControl.SelectedIndex = 0;
            this.adminTabControl.Size = new System.Drawing.Size(785, 426);
            this.adminTabControl.TabIndex = 0;
            // 
            // usersTabPage
            // 
            this.usersTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.usersTabPage.Controls.Add(this.refreshUsersButton);
            this.usersTabPage.Controls.Add(this.deleteUserButton);
            this.usersTabPage.Controls.Add(this.blockUserButton);
            this.usersTabPage.Controls.Add(this.usersDataGridView);
            this.usersTabPage.Location = new System.Drawing.Point(4, 27);
            this.usersTabPage.Name = "usersTabPage";
            this.usersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.usersTabPage.Size = new System.Drawing.Size(777, 395);
            this.usersTabPage.TabIndex = 0;
            this.usersTabPage.Text = "Управление пользователями";
            // 
            // refreshUsersButton
            // 
            this.refreshUsersButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshUsersButton.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.refreshUsersButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.refreshUsersButton.Location = new System.Drawing.Point(265, 356);
            this.refreshUsersButton.Name = "refreshUsersButton";
            this.refreshUsersButton.Size = new System.Drawing.Size(255, 33);
            this.refreshUsersButton.TabIndex = 3;
            this.refreshUsersButton.Text = "Обновить";
            this.refreshUsersButton.UseVisualStyleBackColor = true;
            this.refreshUsersButton.Click += new System.EventHandler(this.refreshUsersButton_Click);
            // 
            // deleteUserButton
            // 
            this.deleteUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteUserButton.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deleteUserButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.deleteUserButton.Location = new System.Drawing.Point(526, 356);
            this.deleteUserButton.Name = "deleteUserButton";
            this.deleteUserButton.Size = new System.Drawing.Size(243, 33);
            this.deleteUserButton.TabIndex = 2;
            this.deleteUserButton.Text = "Удалить";
            this.deleteUserButton.UseVisualStyleBackColor = true;
            this.deleteUserButton.Click += new System.EventHandler(this.deleteUserButton_Click);
            // 
            // blockUserButton
            // 
            this.blockUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.blockUserButton.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.blockUserButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.blockUserButton.Location = new System.Drawing.Point(6, 356);
            this.blockUserButton.Name = "blockUserButton";
            this.blockUserButton.Size = new System.Drawing.Size(253, 33);
            this.blockUserButton.TabIndex = 1;
            this.blockUserButton.Text = "Заблокировать/разблокировать";
            this.blockUserButton.UseVisualStyleBackColor = true;
            this.blockUserButton.Click += new System.EventHandler(this.blockUserButton_Click);
            // 
            // usersDataGridView
            // 
            this.usersDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.usersDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.usersDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.usersDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.usersDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.usersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.usersDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.usersDataGridView.Location = new System.Drawing.Point(6, 3);
            this.usersDataGridView.Name = "usersDataGridView";
            this.usersDataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.usersDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.usersDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.usersDataGridView.Size = new System.Drawing.Size(763, 347);
            this.usersDataGridView.TabIndex = 0;
            // 
            // contentTabPage
            // 
            this.contentTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.contentTabPage.Controls.Add(this.refreshTechButton);
            this.contentTabPage.Controls.Add(this.deleteTechButton);
            this.contentTabPage.Controls.Add(this.editTechButton);
            this.contentTabPage.Controls.Add(this.addTechButton);
            this.contentTabPage.Controls.Add(this.technologiesDataGridView);
            this.contentTabPage.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contentTabPage.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.contentTabPage.Location = new System.Drawing.Point(4, 27);
            this.contentTabPage.Name = "contentTabPage";
            this.contentTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.contentTabPage.Size = new System.Drawing.Size(777, 395);
            this.contentTabPage.TabIndex = 1;
            this.contentTabPage.Text = "Управление контентом";
            // 
            // refreshTechButton
            // 
            this.refreshTechButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshTechButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.refreshTechButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.refreshTechButton.Location = new System.Drawing.Point(529, 141);
            this.refreshTechButton.Name = "refreshTechButton";
            this.refreshTechButton.Size = new System.Drawing.Size(193, 34);
            this.refreshTechButton.TabIndex = 4;
            this.refreshTechButton.Text = "Обновить";
            this.refreshTechButton.UseVisualStyleBackColor = true;
            this.refreshTechButton.Click += new System.EventHandler(this.refreshTechButton_Click);
            // 
            // deleteTechButton
            // 
            this.deleteTechButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteTechButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deleteTechButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.deleteTechButton.Location = new System.Drawing.Point(529, 101);
            this.deleteTechButton.Name = "deleteTechButton";
            this.deleteTechButton.Size = new System.Drawing.Size(193, 34);
            this.deleteTechButton.TabIndex = 3;
            this.deleteTechButton.Text = "Удалить";
            this.deleteTechButton.UseVisualStyleBackColor = true;
            this.deleteTechButton.Click += new System.EventHandler(this.deleteTechButton_Click);
            // 
            // editTechButton
            // 
            this.editTechButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editTechButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editTechButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.editTechButton.Location = new System.Drawing.Point(529, 61);
            this.editTechButton.Name = "editTechButton";
            this.editTechButton.Size = new System.Drawing.Size(193, 34);
            this.editTechButton.TabIndex = 2;
            this.editTechButton.Text = "Редактировать";
            this.editTechButton.UseVisualStyleBackColor = true;
            this.editTechButton.Click += new System.EventHandler(this.editTechButton_Click);
            // 
            // addTechButton
            // 
            this.addTechButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addTechButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addTechButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.addTechButton.Location = new System.Drawing.Point(529, 21);
            this.addTechButton.Name = "addTechButton";
            this.addTechButton.Size = new System.Drawing.Size(193, 34);
            this.addTechButton.TabIndex = 1;
            this.addTechButton.Text = "Добавить";
            this.addTechButton.UseVisualStyleBackColor = true;
            this.addTechButton.Click += new System.EventHandler(this.addTechButton_Click);
            // 
            // technologiesDataGridView
            // 
            this.technologiesDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.technologiesDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.technologiesDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.technologiesDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.technologiesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.technologiesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.technologiesDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.technologiesDataGridView.Location = new System.Drawing.Point(6, 6);
            this.technologiesDataGridView.Name = "technologiesDataGridView";
            this.technologiesDataGridView.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.technologiesDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.technologiesDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.technologiesDataGridView.Size = new System.Drawing.Size(517, 388);
            this.technologiesDataGridView.TabIndex = 0;
            // 
            // commentsTabPage
            // 
            this.commentsTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.commentsTabPage.Controls.Add(this.refreshCommentsButton);
            this.commentsTabPage.Controls.Add(this.deleteCommentButton);
            this.commentsTabPage.Controls.Add(this.approveCommentButton);
            this.commentsTabPage.Controls.Add(this.commentsDataGridView);
            this.commentsTabPage.Location = new System.Drawing.Point(4, 27);
            this.commentsTabPage.Name = "commentsTabPage";
            this.commentsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.commentsTabPage.Size = new System.Drawing.Size(777, 395);
            this.commentsTabPage.TabIndex = 2;
            this.commentsTabPage.Text = "Модерация комментариев";
            // 
            // refreshCommentsButton
            // 
            this.refreshCommentsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshCommentsButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.refreshCommentsButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.refreshCommentsButton.Location = new System.Drawing.Point(442, 86);
            this.refreshCommentsButton.Name = "refreshCommentsButton";
            this.refreshCommentsButton.Size = new System.Drawing.Size(193, 34);
            this.refreshCommentsButton.TabIndex = 3;
            this.refreshCommentsButton.Text = "Обновить";
            this.refreshCommentsButton.UseVisualStyleBackColor = true;
            this.refreshCommentsButton.Click += new System.EventHandler(this.refreshCommentsButton_Click);
            // 
            // deleteCommentButton
            // 
            this.deleteCommentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteCommentButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deleteCommentButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.deleteCommentButton.Location = new System.Drawing.Point(442, 46);
            this.deleteCommentButton.Name = "deleteCommentButton";
            this.deleteCommentButton.Size = new System.Drawing.Size(193, 34);
            this.deleteCommentButton.TabIndex = 2;
            this.deleteCommentButton.Text = "Удалить";
            this.deleteCommentButton.UseVisualStyleBackColor = true;
            this.deleteCommentButton.Click += new System.EventHandler(this.deleteCommentButton_Click);
            // 
            // approveCommentButton
            // 
            this.approveCommentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.approveCommentButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.approveCommentButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.approveCommentButton.Location = new System.Drawing.Point(442, 6);
            this.approveCommentButton.Name = "approveCommentButton";
            this.approveCommentButton.Size = new System.Drawing.Size(193, 34);
            this.approveCommentButton.TabIndex = 1;
            this.approveCommentButton.Text = "Одобрить";
            this.approveCommentButton.UseVisualStyleBackColor = true;
            this.approveCommentButton.Click += new System.EventHandler(this.approveCommentButton_Click);
            // 
            // commentsDataGridView
            // 
            this.commentsDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.commentsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commentsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.commentsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.commentsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.commentsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.commentsDataGridView.DefaultCellStyle = dataGridViewCellStyle10;
            this.commentsDataGridView.Location = new System.Drawing.Point(6, 6);
            this.commentsDataGridView.Name = "commentsDataGridView";
            this.commentsDataGridView.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.commentsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.commentsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.commentsDataGridView.Size = new System.Drawing.Size(430, 388);
            this.commentsDataGridView.TabIndex = 0;
            // 
            // reportsTabPage
            // 
            this.reportsTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.reportsTabPage.Controls.Add(this.label2);
            this.reportsTabPage.Controls.Add(this.label1);
            this.reportsTabPage.Controls.Add(this.exportReportButton);
            this.reportsTabPage.Controls.Add(this.reportDataGridView);
            this.reportsTabPage.Controls.Add(this.generateReportButton);
            this.reportsTabPage.Controls.Add(this.toDatePicker);
            this.reportsTabPage.Controls.Add(this.fromDatePicker);
            this.reportsTabPage.Controls.Add(this.reportTypeComboBox);
            this.reportsTabPage.Location = new System.Drawing.Point(4, 27);
            this.reportsTabPage.Name = "reportsTabPage";
            this.reportsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.reportsTabPage.Size = new System.Drawing.Size(777, 395);
            this.reportsTabPage.TabIndex = 3;
            this.reportsTabPage.Text = "Отчеты";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(419, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "ПО";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(242, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "С";
            // 
            // exportReportButton
            // 
            this.exportReportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportReportButton.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exportReportButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.exportReportButton.Location = new System.Drawing.Point(495, 353);
            this.exportReportButton.Name = "exportReportButton";
            this.exportReportButton.Size = new System.Drawing.Size(276, 36);
            this.exportReportButton.TabIndex = 5;
            this.exportReportButton.Text = "Экспорт";
            this.exportReportButton.UseVisualStyleBackColor = true;
            this.exportReportButton.Click += new System.EventHandler(this.exportReportButton_Click);
            // 
            // reportDataGridView
            // 
            this.reportDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.reportDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.reportDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.reportDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.reportDataGridView.DefaultCellStyle = dataGridViewCellStyle14;
            this.reportDataGridView.Location = new System.Drawing.Point(6, 51);
            this.reportDataGridView.Name = "reportDataGridView";
            this.reportDataGridView.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.reportDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.reportDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.reportDataGridView.Size = new System.Drawing.Size(483, 338);
            this.reportDataGridView.TabIndex = 4;
            // 
            // generateReportButton
            // 
            this.generateReportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generateReportButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.generateReportButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.generateReportButton.Location = new System.Drawing.Point(612, 7);
            this.generateReportButton.Name = "generateReportButton";
            this.generateReportButton.Size = new System.Drawing.Size(159, 34);
            this.generateReportButton.TabIndex = 3;
            this.generateReportButton.Text = "Сгенирировать";
            this.generateReportButton.UseVisualStyleBackColor = true;
            this.generateReportButton.Click += new System.EventHandler(this.generateReportButton_Click);
            // 
            // toDatePicker
            // 
            this.toDatePicker.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toDatePicker.Location = new System.Drawing.Point(460, 7);
            this.toDatePicker.Name = "toDatePicker";
            this.toDatePicker.Size = new System.Drawing.Size(146, 26);
            this.toDatePicker.TabIndex = 2;
            // 
            // fromDatePicker
            // 
            this.fromDatePicker.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fromDatePicker.Location = new System.Drawing.Point(267, 7);
            this.fromDatePicker.Name = "fromDatePicker";
            this.fromDatePicker.Size = new System.Drawing.Size(146, 26);
            this.fromDatePicker.TabIndex = 1;
            // 
            // reportTypeComboBox
            // 
            this.reportTypeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reportTypeComboBox.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.reportTypeComboBox.FormattingEnabled = true;
            this.reportTypeComboBox.Location = new System.Drawing.Point(6, 6);
            this.reportTypeComboBox.Name = "reportTypeComboBox";
            this.reportTypeComboBox.Size = new System.Drawing.Size(229, 30);
            this.reportTypeComboBox.TabIndex = 0;
            // 
            // AdminDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.adminTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AdminDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Панель администратора";
            this.adminTabControl.ResumeLayout(false);
            this.usersTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).EndInit();
            this.contentTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.technologiesDataGridView)).EndInit();
            this.commentsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.commentsDataGridView)).EndInit();
            this.reportsTabPage.ResumeLayout(false);
            this.reportsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reportDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl adminTabControl;
        private System.Windows.Forms.TabPage usersTabPage;
        private System.Windows.Forms.TabPage contentTabPage;
        private System.Windows.Forms.DataGridView usersDataGridView;
        private System.Windows.Forms.Button blockUserButton;
        private System.Windows.Forms.Button refreshUsersButton;
        private System.Windows.Forms.Button deleteUserButton;
        private System.Windows.Forms.Button refreshTechButton;
        private System.Windows.Forms.Button deleteTechButton;
        private System.Windows.Forms.Button editTechButton;
        private System.Windows.Forms.Button addTechButton;
        private System.Windows.Forms.DataGridView technologiesDataGridView;
        private System.Windows.Forms.TabPage commentsTabPage;
        private System.Windows.Forms.Button refreshCommentsButton;
        private System.Windows.Forms.Button deleteCommentButton;
        private System.Windows.Forms.Button approveCommentButton;
        private System.Windows.Forms.DataGridView commentsDataGridView;
        private System.Windows.Forms.TabPage reportsTabPage;
        private System.Windows.Forms.Button generateReportButton;
        private System.Windows.Forms.DateTimePicker toDatePicker;
        private System.Windows.Forms.DateTimePicker fromDatePicker;
        private System.Windows.Forms.ComboBox reportTypeComboBox;
        private System.Windows.Forms.Button exportReportButton;
        private System.Windows.Forms.DataGridView reportDataGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
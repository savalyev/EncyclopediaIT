namespace EncyclopediaIT.Forms
{
    partial class UserProfileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserProfileForm));
            this.profileTabControl = new System.Windows.Forms.TabControl();
            this.infoTabPage = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.registrationDateTextBox = new System.Windows.Forms.TextBox();
            this.bookmarksTabPage = new System.Windows.Forms.TabPage();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.removeBookmarkButton = new System.Windows.Forms.Button();
            this.openBookmarkButton = new System.Windows.Forms.Button();
            this.bookmarksListView = new System.Windows.Forms.ListView();
            this.commentsTabPage = new System.Windows.Forms.TabPage();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.commentRatingLabel = new System.Windows.Forms.Label();
            this.commentPreviewTextBox = new System.Windows.Forms.TextBox();
            this.deleteCommentButton = new System.Windows.Forms.Button();
            this.editCommentButton = new System.Windows.Forms.Button();
            this.commentsListView = new System.Windows.Forms.ListView();
            this.profileTabControl.SuspendLayout();
            this.infoTabPage.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.bookmarksTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.commentsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // profileTabControl
            // 
            this.profileTabControl.Controls.Add(this.infoTabPage);
            this.profileTabControl.Controls.Add(this.bookmarksTabPage);
            this.profileTabControl.Controls.Add(this.commentsTabPage);
            this.profileTabControl.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.profileTabControl.Location = new System.Drawing.Point(12, 12);
            this.profileTabControl.Name = "profileTabControl";
            this.profileTabControl.SelectedIndex = 0;
            this.profileTabControl.Size = new System.Drawing.Size(776, 426);
            this.profileTabControl.TabIndex = 0;
            // 
            // infoTabPage
            // 
            this.infoTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.infoTabPage.Controls.Add(this.panel1);
            this.infoTabPage.Location = new System.Drawing.Point(4, 27);
            this.infoTabPage.Name = "infoTabPage";
            this.infoTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.infoTabPage.Size = new System.Drawing.Size(768, 395);
            this.infoTabPage.TabIndex = 0;
            this.infoTabPage.Text = "Информация";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Controls.Add(this.emailTextBox);
            this.panel1.Controls.Add(this.usernameTextBox);
            this.panel1.Controls.Add(this.registrationDateTextBox);
            this.panel1.Location = new System.Drawing.Point(29, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(703, 153);
            this.panel1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 147);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // saveButton
            // 
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.saveButton.Location = new System.Drawing.Point(453, 90);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(247, 60);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "СОХРАНИТЬ ИЗМЕНЕНИЯ";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // emailTextBox
            // 
            this.emailTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.emailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.emailTextBox.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.emailTextBox.Location = new System.Drawing.Point(159, 109);
            this.emailTextBox.Multiline = true;
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(288, 41);
            this.emailTextBox.TabIndex = 1;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.usernameTextBox.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.usernameTextBox.Location = new System.Drawing.Point(159, 15);
            this.usernameTextBox.Multiline = true;
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.ReadOnly = true;
            this.usernameTextBox.Size = new System.Drawing.Size(288, 41);
            this.usernameTextBox.TabIndex = 0;
            // 
            // registrationDateTextBox
            // 
            this.registrationDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.registrationDateTextBox.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.registrationDateTextBox.Location = new System.Drawing.Point(159, 62);
            this.registrationDateTextBox.Multiline = true;
            this.registrationDateTextBox.Name = "registrationDateTextBox";
            this.registrationDateTextBox.ReadOnly = true;
            this.registrationDateTextBox.Size = new System.Drawing.Size(288, 41);
            this.registrationDateTextBox.TabIndex = 2;
            // 
            // bookmarksTabPage
            // 
            this.bookmarksTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.bookmarksTabPage.Controls.Add(this.pictureBox3);
            this.bookmarksTabPage.Controls.Add(this.pictureBox2);
            this.bookmarksTabPage.Controls.Add(this.removeBookmarkButton);
            this.bookmarksTabPage.Controls.Add(this.openBookmarkButton);
            this.bookmarksTabPage.Controls.Add(this.bookmarksListView);
            this.bookmarksTabPage.Location = new System.Drawing.Point(4, 27);
            this.bookmarksTabPage.Name = "bookmarksTabPage";
            this.bookmarksTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.bookmarksTabPage.Size = new System.Drawing.Size(768, 395);
            this.bookmarksTabPage.TabIndex = 1;
            this.bookmarksTabPage.Text = "Закладки";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(610, 25);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(50, 37);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(610, 88);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 37);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // removeBookmarkButton
            // 
            this.removeBookmarkButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.removeBookmarkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeBookmarkButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.removeBookmarkButton.Location = new System.Drawing.Point(440, 88);
            this.removeBookmarkButton.Name = "removeBookmarkButton";
            this.removeBookmarkButton.Size = new System.Drawing.Size(164, 37);
            this.removeBookmarkButton.TabIndex = 2;
            this.removeBookmarkButton.Text = "Удалить";
            this.removeBookmarkButton.UseVisualStyleBackColor = true;
            this.removeBookmarkButton.Click += new System.EventHandler(this.removeBookmarkButton_Click);
            // 
            // openBookmarkButton
            // 
            this.openBookmarkButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.openBookmarkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openBookmarkButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.openBookmarkButton.Location = new System.Drawing.Point(440, 25);
            this.openBookmarkButton.Name = "openBookmarkButton";
            this.openBookmarkButton.Size = new System.Drawing.Size(164, 37);
            this.openBookmarkButton.TabIndex = 1;
            this.openBookmarkButton.Text = "Открыть";
            this.openBookmarkButton.UseVisualStyleBackColor = true;
            this.openBookmarkButton.Click += new System.EventHandler(this.openBookmarkButton_Click);
            // 
            // bookmarksListView
            // 
            this.bookmarksListView.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bookmarksListView.HideSelection = false;
            this.bookmarksListView.Location = new System.Drawing.Point(6, 6);
            this.bookmarksListView.Name = "bookmarksListView";
            this.bookmarksListView.Size = new System.Drawing.Size(428, 388);
            this.bookmarksListView.TabIndex = 0;
            this.bookmarksListView.UseCompatibleStateImageBehavior = false;
            // 
            // commentsTabPage
            // 
            this.commentsTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(234)))), ((int)(((byte)(230)))));
            this.commentsTabPage.Controls.Add(this.pictureBox5);
            this.commentsTabPage.Controls.Add(this.pictureBox4);
            this.commentsTabPage.Controls.Add(this.commentRatingLabel);
            this.commentsTabPage.Controls.Add(this.commentPreviewTextBox);
            this.commentsTabPage.Controls.Add(this.deleteCommentButton);
            this.commentsTabPage.Controls.Add(this.editCommentButton);
            this.commentsTabPage.Controls.Add(this.commentsListView);
            this.commentsTabPage.Location = new System.Drawing.Point(4, 27);
            this.commentsTabPage.Name = "commentsTabPage";
            this.commentsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.commentsTabPage.Size = new System.Drawing.Size(768, 395);
            this.commentsTabPage.TabIndex = 2;
            this.commentsTabPage.Text = "Комментарии";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(517, 175);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(50, 33);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 6;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(517, 225);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(50, 33);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            // 
            // commentRatingLabel
            // 
            this.commentRatingLabel.AutoSize = true;
            this.commentRatingLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.commentRatingLabel.Location = new System.Drawing.Point(301, 6);
            this.commentRatingLabel.Name = "commentRatingLabel";
            this.commentRatingLabel.Size = new System.Drawing.Size(185, 22);
            this.commentRatingLabel.TabIndex = 4;
            this.commentRatingLabel.Text = "Выберите комментарий";
            // 
            // commentPreviewTextBox
            // 
            this.commentPreviewTextBox.Location = new System.Drawing.Point(301, 31);
            this.commentPreviewTextBox.Multiline = true;
            this.commentPreviewTextBox.Name = "commentPreviewTextBox";
            this.commentPreviewTextBox.Size = new System.Drawing.Size(461, 138);
            this.commentPreviewTextBox.TabIndex = 3;
            this.commentPreviewTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // deleteCommentButton
            // 
            this.deleteCommentButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.deleteCommentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteCommentButton.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deleteCommentButton.Location = new System.Drawing.Point(301, 225);
            this.deleteCommentButton.Name = "deleteCommentButton";
            this.deleteCommentButton.Size = new System.Drawing.Size(210, 33);
            this.deleteCommentButton.TabIndex = 2;
            this.deleteCommentButton.Text = "Удалить";
            this.deleteCommentButton.UseVisualStyleBackColor = true;
            this.deleteCommentButton.Click += new System.EventHandler(this.deleteCommentButton_Click);
            // 
            // editCommentButton
            // 
            this.editCommentButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.editCommentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editCommentButton.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editCommentButton.Location = new System.Drawing.Point(301, 175);
            this.editCommentButton.Name = "editCommentButton";
            this.editCommentButton.Size = new System.Drawing.Size(210, 33);
            this.editCommentButton.TabIndex = 1;
            this.editCommentButton.Text = "Редактировать";
            this.editCommentButton.UseVisualStyleBackColor = true;
            this.editCommentButton.Click += new System.EventHandler(this.editCommentButton_Click);
            // 
            // commentsListView
            // 
            this.commentsListView.HideSelection = false;
            this.commentsListView.Location = new System.Drawing.Point(6, 6);
            this.commentsListView.Name = "commentsListView";
            this.commentsListView.Size = new System.Drawing.Size(289, 388);
            this.commentsListView.TabIndex = 0;
            this.commentsListView.UseCompatibleStateImageBehavior = false;
            this.commentsListView.SelectedIndexChanged += new System.EventHandler(this.commentsListView_SelectedIndexChanged);
            // 
            // UserProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.profileTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "UserProfileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Личный кабинет";
            this.profileTabControl.ResumeLayout(false);
            this.infoTabPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.bookmarksTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.commentsTabPage.ResumeLayout(false);
            this.commentsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl profileTabControl;
        private System.Windows.Forms.TabPage infoTabPage;
        private System.Windows.Forms.TabPage bookmarksTabPage;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox registrationDateTextBox;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Button removeBookmarkButton;
        private System.Windows.Forms.Button openBookmarkButton;
        private System.Windows.Forms.ListView bookmarksListView;
        private System.Windows.Forms.TabPage commentsTabPage;
        private System.Windows.Forms.Button deleteCommentButton;
        private System.Windows.Forms.Button editCommentButton;
        private System.Windows.Forms.ListView commentsListView;
        private System.Windows.Forms.TextBox commentPreviewTextBox;
        private System.Windows.Forms.Label commentRatingLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
    }
}
namespace Prechcik.ApbStat
{
    partial class ApbStat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApbStat));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.userName = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.regButton = new System.Windows.Forms.Button();
            this.loginButton = new System.Windows.Forms.Button();
            this.serverStatus = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pathBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(12, 314);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(381, 91);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(319, 233);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "Select File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(6, 18);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(128, 20);
            this.userName.TabIndex = 4;
            this.userName.Text = "username";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(7, 44);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(127, 20);
            this.password.TabIndex = 5;
            this.password.Text = "password";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.regButton);
            this.groupBox1.Controls.Add(this.loginButton);
            this.groupBox1.Controls.Add(this.password);
            this.groupBox1.Controls.Add(this.userName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 127);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login";
            // 
            // regButton
            // 
            this.regButton.Location = new System.Drawing.Point(32, 98);
            this.regButton.Name = "regButton";
            this.regButton.Size = new System.Drawing.Size(75, 23);
            this.regButton.TabIndex = 7;
            this.regButton.Text = "Register";
            this.regButton.UseVisualStyleBackColor = true;
            this.regButton.Click += new System.EventHandler(this.RegButtonClick);
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(32, 70);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 23);
            this.loginButton.TabIndex = 6;
            this.loginButton.Text = "Log in";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.LoginButtonClick);
            // 
            // serverStatus
            // 
            this.serverStatus.AutoSize = true;
            this.serverStatus.Location = new System.Drawing.Point(9, 181);
            this.serverStatus.Name = "serverStatus";
            this.serverStatus.Size = new System.Drawing.Size(43, 13);
            this.serverStatus.TabIndex = 7;
            this.serverStatus.Text = "Status: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(165, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 210);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 174);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // pathBox
            // 
            this.pathBox.Location = new System.Drawing.Point(12, 238);
            this.pathBox.Name = "pathBox";
            this.pathBox.Size = new System.Drawing.Size(301, 20);
            this.pathBox.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(124, 285);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(205, 285);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "Stop";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 417);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pathBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.serverStatus);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "APB Stat";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label serverStatus;
        private System.Windows.Forms.Button regButton;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pathBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}


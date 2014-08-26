namespace apbstat
{
    using System;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        OpenFileDialog dialog;
        TextBox txtbox;
        string uname;
        string path;
        bool logged = false;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(); // create a new timer

        public Form1()
        {
            InitializeComponent();
            userName.Text = Properties.Settings.Default.userName;
            password.Text = Properties.Settings.Default.passWord;
            Properties.Settings.Default.Save();
            txtbox = textBox1;
            uname = userName.Text;
            path = Properties.Settings.Default.filePath;
            pathBox.Text = path;


        }

        public void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Log Files|*.log";
            openFileDialog1.Title = "Select a TempChatSessionFile.log file inside APB/APGGame/Logs";
            dialog = openFileDialog1;


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.filePath = openFileDialog1.FileName;
                Properties.Settings.Default.Save();
                path = openFileDialog1.FileName;
                pathBox.Text = path;
            }
        }

        void tickk(object sender, EventArgs e)
        {
            tick2(dialog, txtbox, uname, path);
        }


        static void tick2(OpenFileDialog openFileDialog, TextBox textBox1, string userName, string path)
        {
            var kills = read(path, "Kill Reward");
            var assists = read(path, "Assist Reward");
            var medals = read(path, "Medal Awarded");
            Stream stream = File.Open(@path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            stream.Close();
            //testtest
            textBox1.AppendText(DateTime.Now.ToString("HH:mm:ss") + " - Reading... \n Found " + kills + " kills, " + assists + " assists and " + medals + " medals! Saving into the database!\n");
            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString("http://www.prechcik.pl/apbinsert.php?user=" + userName + "&kills=" + kills + "&assists=" + assists + "&medals= " + medals);
            }
        }


        static string read(string path, string word)
        {
            string line;
            var total = 0;
            System.IO.StreamReader file =
            new System.IO.StreamReader(@path, System.Text.Encoding.UTF8);


            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(word))
                {
                    total++;
                }
            }
            file.Close();

            return "" + total;

        }


        private void loginButton_Click(object sender, EventArgs e)
        {
            string usr = userName.Text;
            string pwd = password.Text;

            Properties.Settings.Default.userName = usr;
            Properties.Settings.Default.passWord = pwd;

            pwd = CreateMD5(pwd);

            using (WebClient client = new WebClient())
            {


                string htmlCode = client.DownloadString("http://www.prechcik.pl/checkapb.php?user=" + usr + "&password=" + pwd);
                if (htmlCode != "")
                {
                    serverStatus.Text = "Status: Login failed. Username or password may be wrong.";
                    logged = false;
                }
                else
                {
                    serverStatus.Text = "Status: Logged in!";
                    logged = true;
                }
            }

        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        private void regButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.apbstat.prechcik.pl/register.php");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (logged == false)
            {
                textBox1.AppendText("You must log in first before you want to submit your statistics!\n");
            }
            else
            {

                timer.Interval = 15000; //300000 = 5 minutes
                timer.Tick += new EventHandler(tickk); //add the event handler
                timer.Start(); //start the timer
                textBox1.AppendText("Starting..\n\n");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer.Stop();
            textBox1.AppendText("Stopped.\n");
        }
    }
}
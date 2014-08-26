namespace apbstat
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;

    public partial class ApbStat : Form
    {
        private readonly TextBox txtbox;
        private readonly string uname;
        private readonly Timer timer = new Timer(); // create a new timer
        private OpenFileDialog dialog;
        private string path;
        private bool logged;

        public ApbStat()
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

        private static void Tick2(OpenFileDialog openFileDialog, TextBoxBase textBox1, string userName, string path)
        {
            var kills = Read(path, "Kill Reward");
            var assists = Read(path, "Assist Reward");
            var medals = Read(path, "Medal Awarded");
            Stream stream = File.Open(@path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            stream.Close();

            // testtest
            textBox1.AppendText(string.Format("{0} - Reading... \n Found {1} kills, {2} assists and {3} medals! Saving into the database!\n", DateTime.Now.ToString("HH:mm:ss"), kills, assists, medals));

            using (var client = new WebClient())
            {
                var htmlCode = client.DownloadString(string.Format("http://www.prechcik.pl/apbinsert.php?user={0}&kills={1}&assists={2}&medals= {3}", userName, kills, assists, medals));
            }
        }

        private static string Read(string path, string word)
        {
            string line;
            var total = 0;
            var file = new StreamReader(@path, Encoding.UTF8);

            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(word))
                {
                    total++;
                }
            }

            file.Close();

            return total.ToString(CultureInfo.InvariantCulture);
        }

        private static string CreateMd5(string input)
        {
            // Use input string to calculate MD5 hash
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();

            foreach (var hashByte in hashBytes)
            {
                sb.Append(hashByte.ToString("X2"));
            }

            return sb.ToString();
        }

        private void LoginButtonClick(object sender, EventArgs e)
        {
            var usr = userName.Text;
            var pwd = password.Text;

            Properties.Settings.Default.userName = usr;
            Properties.Settings.Default.passWord = pwd;

            pwd = CreateMd5(pwd);

            using (var client = new WebClient())
            {
                var htmlCode = client.DownloadString(string.Format("http://www.prechcik.pl/checkapb.php?user={0}&password={1}", usr, pwd));

                if (string.IsNullOrWhiteSpace(htmlCode))
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

        private void Button1Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog
            {
                Filter = "Log Files|*.log",
                Title = "Select a TempChatSessionFile.log file inside APB/APGGame/Logs"
            };
            dialog = openFileDialog1;

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Properties.Settings.Default.filePath = openFileDialog1.FileName;
            Properties.Settings.Default.Save();
            path = openFileDialog1.FileName;
            pathBox.Text = path;
        }

        private void Tickk(object sender, EventArgs e)
        {
            Tick2(dialog, txtbox, uname, path);
        }

        private void RegButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.apbstat.prechcik.pl/register.php");
        }

        private void Button2Click(object sender, EventArgs e)
        {
            if (logged == false)
            {
                textBox1.AppendText("You must log in first before you want to submit your statistics!\n");
            }
            else
            {
                timer.Interval = 15000; // 300000 = 5 minutes
                timer.Tick += Tickk; // add the event handler
                timer.Start(); // start the timer
                textBox1.AppendText("Starting..\n\n");
            }
        }

        private void Button3Click(object sender, EventArgs e)
        {
            timer.Stop();
            textBox1.AppendText("Stopped.\n");
        }
    }
}

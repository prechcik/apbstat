namespace Prechcik.ApbStat
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;

    using Prechcik.ApbStat.Utilities;

    public partial class ApbStat : Form
    {
        private string path;
        private bool logged;
        private DBConnect conn;

        public ApbStat()
        {
            conn = new DBConnect();
            InitializeComponent();
            userName.Text = Properties.Settings.Default.userName;
            password.Text = Properties.Settings.Default.passWord;
            Properties.Settings.Default.Save();
            path = Properties.Settings.Default.filePath;
            pathBox.Text = path;
        }

        private string User { get; set; }

        private LogScanner LogScanner { get; set; }

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

        private void AppendLineToTextbox(string line)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendLineToTextbox), new object[] { line });
                return;
            }

            textBox1.AppendText(string.Format("{0}{1}", line, Environment.NewLine));
        }

        private void LoginButtonClick(object sender, EventArgs e)
        {
            var usr = userName.Text;
            var pwd = password.Text;

            Properties.Settings.Default.userName = usr;
            Properties.Settings.Default.passWord = pwd;

            pwd = CreateMd5(pwd);

            if (conn.checkaccount(usr, pwd) == true)
            {
                logged = true;
                serverStatus.Text = "Status: Logged in!";
                User = usr;
            }
            else
            {
                logged = false;
                serverStatus.Text = "Status: Failed to log in!";
            }

        }

        private void Button1Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog
            {
                Filter = "Log Files|*.log",
                Title = "Select a TempChatSessionFile.log file inside APB/APGGame/Logs"
            };

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Properties.Settings.Default.filePath = openFileDialog1.FileName;
            Properties.Settings.Default.Save();
            path = openFileDialog1.FileName;
            pathBox.Text = path;
        }

        private void RegButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.apbstat.prechcik.pl/register.php");
        }

        private void Button2Click(object sender, EventArgs e)
        {
            if (logged == false)
            {
                AppendLineToTextbox("You must log in first before you want to submit your statistics!");
            }
            else
            {
                AppendLineToTextbox("Starting...");
                scannerBackgroundWorker.DoWork += delegate
                    {
                        LogScanner = new LogScanner { FileLocation = path };

                        LogScanner.OnLogRestarted += LogScannerOnOnLogRestarted;
                        LogScanner.OnNewKillsAssistsStunsOrArrests += LogScannerOnOnNewKillsAssistsStunsOrArrests;

                        LogScanner.BeginLogScanning();
                    };
                scannerBackgroundWorker.RunWorkerAsync();
            }
        }

        private void LogScannerOnOnNewKillsAssistsStunsOrArrests(object sender, KillsAssistsStunsOrArrestsEventArgs args)
        {
            AppendLineToTextbox(
                string.Format(
                    "Found {0} new kills, {1} new assists, {2} new stuns, {3} new arrests and {4} new medals.",
                    args.Kills,
                    args.Assists,
                    args.Stuns,
                    args.Arrests,
                    args.Medals));

            conn.insertData(User, args.Kills.ToString(), args.Assists.ToString(), args.Medals.ToString());
        }

        private void LogScannerOnOnLogRestarted(object sender, LogRestartedEventArgs args)
        {
            AppendLineToTextbox("Changed district.");
            AppendLineToTextbox(string.Format("New log start time is: {0}", args.NewStartOfLog.ToString("u")));
        }

        private void Button3Click(object sender, EventArgs e)
        {
            LogScanner.EndLogScanning();
            scannerBackgroundWorker.CancelAsync();
            AppendLineToTextbox("Stopped");
        }
    }
}

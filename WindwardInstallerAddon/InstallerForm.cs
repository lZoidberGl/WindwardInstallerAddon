using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.IO.Compression;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace WindwardInstallerAddon
{
    public partial class InstallerForm : Form
    {
        bool DevMode = false;
        WebClient Client = new WebClient();
        string path = "";
        List<string> FinishedHash = new List<string>();

        public InstallerForm()
        {
            InitializeComponent();
            MessageBox.Show("Знай, каждый раз, когда ты скачиваешь Windward от сюда, где-то в Канаде грустит Майкл Ляшенко (Разработчик). Задумайся, если игра понравилась, купи ее!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Установщик Windward v" + Application.ProductVersion + "_b2";
            CheckGameStatus();
            CheckForFiles();
            CheckForUpdates();
            ReadSavedPath();  
        }

        private void ReadSavedPath()
        {
            if (File.Exists("Path.txt"))
            {
                GamePath.Text = File.ReadAllText("Path.txt");
                path = File.ReadAllText("Path.txt");
            }
        }

        private void CheckForUpdates()
        {
            string NewVer = Client.DownloadString("https://dl.dropboxusercontent.com/s/elkr2ge5vwpir21/WIAversion.txt?dl=1");//https://www.dropbox.com/s/elkr2ge5vwpir21/WIAversion.txt?dl=0
            NewerVers.Text = NewVer;
            NewVer = NewerVers.Text.Remove(1, 1);
            string ThisVer = Application.ProductVersion.Remove(1, 1);
            int neededversion = Int32.Parse(NewVer);
            int thisversion = Int32.Parse(ThisVer);

            if (neededversion > thisversion)
            {
                MessageBox.Show("Доступно обновление до: " + NewerVers.Text + "b!");
            }
            else
            {
                UpdateProg.Enabled = false;
                UpdateProg.Text = "У вас последняя версия программы";
            }
        }

        private void CheckForFiles()
        {

            if (File.Exists(Path.Combine(Application.StartupPath,"updated.txt")))
            {
                string Changes = Client.DownloadString("https://dl.dropboxusercontent.com/s/h8jekwey483p73v/WIAchanges.txt?dl=1"); // https://www.dropbox.com/s/h8jekwey483p73v/WIAchanges.txt?dl=0
                MessageBox.Show("Обновление успешно завершено!\nИзменения:\n" + Changes);
                File.Delete("updated.txt");
            }
        }

        private void CheckGameStatus()
        {
            if (GamePath.Text.Contains("Windward"))
            {
                if (File.Exists(Path.Combine(GamePath.Text, "Windward.exe")))
                {
                    StatusPanel.BackColor = Color.LimeGreen;
                    DeleteButton.Enabled = true;
                    UpdateButton.Enabled = true;
                    InstallButton.Text = "Переустановить";
                }
                else
                {
                    StatusPanel.BackColor = Color.Red;
                    DeleteButton.Enabled = false;
                    UpdateButton.Enabled = false;
                    InstallButton.Text = "Скачать и установить игру";
                }
                  
            }
            else
            {
                StatusPanel.BackColor = Color.Red;
                DeleteButton.Enabled = false;
                UpdateButton.Enabled = false;
                InstallButton.Text = "Скачать и установить игру";
            }


        }

        private void CheckGameVersion()
        {
            if (File.Exists(Path.Combine(GamePath.Text, "GameVersion.txt")))
            {
                if (File.Exists(Path.Combine(GamePath.Text, "GameVersion.txt")))
                {
                    string NewGameVer = Client.DownloadString("https://dl.dropboxusercontent.com/s/yyio07lp9n2mkg1/GameVersion.txt?dl=1");//https://www.dropbox.com/s/yyio07lp9n2mkg1/GameVersion.txt?dl=0
                    string ThisGameVer = File.ReadAllText(Path.Combine(GamePath.Text, "GameVersion.txt"));
                    if (NewGameVer.Contains(ThisGameVer))
                    {
                        MessageBox.Show("У вас последняя версия: " + ThisGameVer + "!");
                    }
                    else
                    {
                        DialogResult answ = MessageBox.Show("Доступна более новая версия игры: " + NewGameVer + "! Хотите обновиться?", "Новая версия", MessageBoxButtons.YesNo);
                        if (answ == DialogResult.Yes)
                        {
                            DownloadGameUpdate();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Файл версии не обнаружен, обновление не доступно! Требуется переустановка игры.");
                UpdateButton.Enabled = false;
            }
        }

        private void DownloadGameUpdate()
        {
            Client.DownloadProgressChanged += Client_DownloadProgressChanged;
            Client.DownloadFileCompleted += Client_DownloadFileCompleted;

            if (File.Exists("GameUpdate.zip"))
                File.Delete("GameUpdate.zip");
            //https://www.dropbox.com/s/2btzdtv25n154db/WindwardUpdate.zip?dl=0
            Client.DownloadFileAsync(new Uri("https://dl.dropboxusercontent.com/s/2btzdtv25n154db/WindwardUpdate.zip?dl=1"), Path.Combine(Application.StartupPath, "GameUpdate.zip"));
        }

        private void BrowseGame_Click(object sender, EventArgs e)
        {
            BrowseDirectory();
        }

        private void BrowseDirectory()
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.ShowDialog();
            GamePath.Text = fd.SelectedPath.ToString();
            path = fd.SelectedPath.ToString();
        }

        private void GamePath_TextChanged(object sender, EventArgs e)
        {
            CheckGameStatus();
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            if (InstallButton.Text == "Установить")
            {
                CancelDownload.Enabled = true;
                DownloadGameFiles();
            }
            else
            {
                DeleteGame();
                CancelDownload.Enabled = true;
                DownloadGameFiles();
            }
            InstallButton.Enabled = false;
        }

        private void DownloadGameFiles()
        {
            Client.DownloadProgressChanged += Client_DownloadProgressChanged;
            Client.DownloadFileCompleted += Client_DownloadFileCompleted;
            //File original https://www.dropbox.com/s/iwkvri38sytcvxr/WindwardGame.zip?dl=0
            //File right link https://dl.dropboxusercontent.com/s/iwkvri38sytcvxr/WindwardGame.zip?dl=1
            Client.DownloadFileAsync(new Uri("https://dl.dropboxusercontent.com/s/iwkvri38sytcvxr/WindwardGame.zip?dl=1"), Path.Combine(Application.StartupPath, "Windward.zip") );
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled) { }
            else
            {
                if (File.Exists("Windward.zip"))
                {
                    MessageBox.Show("Дальше последует установка, укажите место установки. Обратите внимание, что папка для игры будет создана автоматически!");
                    BrowseDirectory();
                }

                if (File.Exists("GameUpdate.zip"))
                {
                    MessageBox.Show("Файлы обновления скачаны! Далее последует распаковка.");
                }

                Unzipper.WorkerSupportsCancellation = true;
                Unzipper.RunWorkerAsync();
            }           

            ProgressBar.Value = 0;
            Status.Text = "0MB / 0MB    0%";

            CancelDownload.Enabled = false;
        }

        private void DeleteRemainingFiles()
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "Windward.zip")))
            {
                File.Delete(Path.Combine(Application.StartupPath, "Windward.zip"));
                GamePath.Text = Path.Combine(GamePath.Text, "Windward");
            }


            if (File.Exists(Path.Combine(Application.StartupPath, "update.zip")))
            {
                File.Delete(Path.Combine(Application.StartupPath, "update.zip"));
                CmdWork();
            }

            if (File.Exists(Path.Combine(Application.StartupPath, "GameUpdate.zip")))
            {               
                File.Delete(Path.Combine(Application.StartupPath, "GameUpdate.zip"));
                MessageBox.Show("Обновление игры закончено!");
                GenerateVersionFile();
            }

        }

        private void GenerateVersionFile()
        {
            File.Delete(Path.Combine(GamePath.Text, "GameVersion.txt"));
            string NewGameVer = Client.DownloadString("https://dl.dropboxusercontent.com/s/yyio07lp9n2mkg1/GameVersion.txt?dl=1");//https://www.dropbox.com/s/yyio07lp9n2mkg1/GameVersion.txt?dl=0
            File.WriteAllText(Path.Combine(GamePath.Text, "GameVersion.txt"), NewGameVer);
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
            string mb_recieved = BytesToString(e.BytesReceived);
            string mb_toRecieve = BytesToString(e.TotalBytesToReceive);
            Status.Text = "Загрузка: " + mb_recieved + " / " + mb_toRecieve + "          " + e.ProgressPercentage + "%";
        }

        string deb = "";
        int noneeded = 0;

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            noneeded = 0;
            UpdateButton.Enabled = false;
            MessageBox.Show("Внимание, данная функция все-еще тестируется!\nСейчас будет проведен анализ файлов, что займет от нескольких секунд до 5 минут (зависит от параметров компьютера).");

            if (path != "")
            {
                string[] originalfiles = { @"Windward\steam_api.dll", @"Windward\Windward.exe", @"Windward\WWServer.exe", @"Windward\Windward_Data\level0", @"Windward\Windward_Data\level1", @"Windward\Windward_Data\mainData", @"Windward\Windward_Data\output_log.txt", @"Windward\Windward_Data\PlayerConnectionConfigFile", @"Windward\Windward_Data\resources.assets", @"Windward\Windward_Data\sharedassets0.assets", @"Windward\Windward_Data\sharedassets0.assets.resS", @"Windward\Windward_Data\sharedassets1.assets", @"Windward\Windward_Data\sharedassets2.assets", @"Windward\Windward_Data\Managed\Assembly-CSharp-firstpass.dll", @"Windward\Windward_Data\Managed\Assembly-CSharp.dll", @"Windward\Windward_Data\Managed\Assembly-UnityScript-firstpass.dll", @"Windward\Windward_Data\Managed\Boo.Lang.dll", @"Windward\Windward_Data\Managed\Mono.CSharp.dll", @"Windward\Windward_Data\Managed\Mono.Security.dll", @"Windward\Windward_Data\Managed\mscorlib.dll", @"Windward\Windward_Data\Managed\System.Core.dll", @"Windward\Windward_Data\Managed\System.dll", @"Windward\Windward_Data\Managed\System.Xml.dll", @"Windward\Windward_Data\Managed\TouchScript.dll", @"Windward\Windward_Data\Managed\TouchScript.Windows.dll", @"Windward\Windward_Data\Managed\UnityEngine.dll", @"Windward\Windward_Data\Managed\UnityEngine.UI.dll", @"Windward\Windward_Data\Managed\UnityScript.Lang.dll", @"Windward\Windward_Data\Managed\XInputDotNetPure.dll", @"Windward\Windward_Data\Mono\mono.dll", @"Windward\Windward_Data\Mono\etc\mono\browscap.ini", @"Windward\Windward_Data\Mono\etc\mono\config", @"Windward\Windward_Data\Mono\etc\mono\1.0\DefaultWsdlHelpGenerator.aspx", @"Windward\Windward_Data\Mono\etc\mono\1.0\machine.config", @"Windward\Windward_Data\Mono\etc\mono\2.0\DefaultWsdlHelpGenerator.aspx", @"Windward\Windward_Data\Mono\etc\mono\2.0\machine.config", @"Windward\Windward_Data\Mono\etc\mono\2.0\settings.map", @"Windward\Windward_Data\Mono\etc\mono\2.0\web.config", @"Windward\Windward_Data\Mono\etc\mono\2.0\Browsers\Compat.browser", @"Windward\Windward_Data\Mono\etc\mono\mconfig\config.xml", @"Windward\Windward_Data\Plugins\CSteamworks.dll", @"Windward\Windward_Data\Plugins\steam_api.dll", @"Windward\Windward_Data\Plugins\XInputDotNetPure.dll", @"Windward\Windward_Data\Plugins\XInputInterface.dll", @"Windward\Windward_Data\Resources\unity default resources", @"Windward\Windward_Data\Resources\unity_builtin_extra" };
                string[] filepaths = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                ProgressBar.Style = ProgressBarStyle.Marquee;
                foreach (string file in filepaths)
                {
                  string filenew = file.Remove(0, GamePath.Text.Length - 8);
                    if (originalfiles.Contains(filenew))
                    {
                        GenerateHash(file);
                    }
                                           else
                    {
                        noneeded++;
                        deb = deb + filenew + Environment.NewLine;
                    }
                      

                }
                if (DevMode == true)
                MessageBox.Show("Ненужных файлов:" + noneeded.ToString() + Environment.NewLine + deb);               

                TryToCompare();
            }
        }

        string[] NeedToDownload = {""};

        private void TryToCompare()
        {
            Client.DownloadFile("https://dl.dropboxusercontent.com/s/8c4nrletjevj4tk/WindwardHash.txt?dl=1", "WindwardHash.txt");
            string[] lines1 = File.ReadAllLines("WindwardHash.txt");
            string[] lines2 = FinishedHash.ToArray();

            List<string> OriginLines = new List<string>(lines1);
            List<string> DownloadList = new List<string>(NeedToDownload);
            int passed = 0;
            int notpassed = 0;

            foreach (string lineOrigin in lines1)
            {
                if (lines2.Contains(lineOrigin))
                {
                    passed++;
                }
                else
                {
                    notpassed++;
                    DownloadList.Add(lineOrigin.Substring(0, lineOrigin.Length - 33));
                }

            }
            NeedToDownload = DownloadList.ToArray();
            ProgressBar.Style = ProgressBarStyle.Continuous;
            if (notpassed == 0)
            {
                MessageBox.Show("Файлы:\nПрошло проверку: " + passed.ToString() + "\nНе прошло проверку: " + notpassed.ToString() + "\nИтог: Все файлы последней версии!", "Результат");
                UpdateButton.Enabled = true;
                try
                {
                    File.Delete("WindwardHash.txt");
                }
                catch { MessageBox.Show("Не удалось удать файл: WindwardHash.txt\nПопытайтесь удалить его вручную!"); }
            }
           else
            {
                DialogResult answ = MessageBox.Show("Файлы:\nПрошло проверку: " + passed.ToString() + "\nНе прошло проверку: " + notpassed.ToString() + "\nХотите-ли обновить файлы, не прошедшие проверку?", "Результат", MessageBoxButtons.YesNo);
                if (answ == DialogResult.Yes)
                {
                    if (DevMode == true)
                    MessageBox.Show("Будет загружено файлов: " + notpassed.ToString() + ".\n" + string.Join(Environment.NewLine, NeedToDownload));

                    CreateDirectories();
                    DowndloadUpdatedFiles(NeedToDownload);
               }
            }
        }

        private void CreateDirectories()
        {
            string[] NeededDirectories = {@"Windward\Windward_Data",
                                          @"Windward\Windward_Data\Managed",
                                          @"Windward\Windward_Data\Mono",
                                          @"Windward\Windward_Data\Plugins",
                                          @"Windward\Windward_Data\Resources",
                                          @"Windward\Windward_Data\Mono\etc",
                                          @"Windward\Windward_Data\Mono\etc\mono",
                                          @"Windward\Windward_Data\Mono\etc\mono\1.0",
                                          @"Windward\Windward_Data\Mono\etc\mono\2.0",
                                          @"Windward\Windward_Data\Mono\etc\mono\mconfig",
                                          @"Windward\Windward_Data\Mono\etc\mono\2.0\Browsers"
            };

            foreach (string dir in NeededDirectories)
            {
                string OwnDir = GamePath.Text.Substring(0, GamePath.Text.Length - 8) + dir;
                if (!Directory.Exists(OwnDir))
                {
                    Directory.CreateDirectory(OwnDir);
                }
            }
        }

        private bool downloadComplete = false;
        int fileCount = 0;
        int fileDownloaded = 1;
        private void DowndloadUpdatedFiles(string[] FilesToDownload)
        {
            Client.DownloadFile(new Uri("https://dl.dropboxusercontent.com/s/t7oqnvhelk1rtcs/links.txt?dl=1"), "links.txt");
            string[] links = File.ReadAllLines("links.txt");

            Client.DownloadProgressChanged += Client_DownloadProgressChangedUpdate;
            Client.DownloadFileCompleted += Client_DownloadFileCompletedUpdate;
            

            List<string> ToDownloadList = new List<string>();

            foreach (string link in links)
            {
                string file = link.Substring(0, link.IndexOf("+"));

                if (FilesToDownload.Contains(file))
                {
                    ToDownloadList.Add(link);
                }

            }

            string[] ToDownload = ToDownloadList.ToArray();

            fileCount = ToDownload.Length;

            foreach (string fileToDown in ToDownload)
            {
                try
                {
                    string file = fileToDown.Substring(0, fileToDown.IndexOf("+"));
                    CancelDownload.Enabled = true;
                    Client.DownloadFileAsync(new Uri(fileToDown.Remove(0, fileToDown.IndexOf("+") + 1)), Path.Combine(GamePath.Text, file.Remove(0, 9)));
                    while (!downloadComplete)
                    {
                        Application.DoEvents();
                    }

                    downloadComplete = false;
                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }

        }

        private void Client_DownloadProgressChangedUpdate( object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
            string mb_recieved = BytesToString(e.BytesReceived);
            string mb_toRecieve = BytesToString(e.TotalBytesToReceive);
            Status.Text = "Файл: "+ fileDownloaded.ToString() + "/" + fileCount.ToString() + " Загрузка: " + mb_recieved + " / " + mb_toRecieve + "          " + e.ProgressPercentage + "%";
        }

        private void Client_DownloadFileCompletedUpdate(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                downloadComplete = true;
            });

            if (fileDownloaded == fileCount)
            {
                MessageBox.Show("Загрузка файлов завершена!");
                try
                {
                    File.Delete("links.txt");
                    File.Delete("WindwardHash.txt");
                }
                catch { MessageBox.Show("Не удалось удалить файлы: links.txt и WindwardHash.txt \n Попытайтесь удалить их вручную!"); }
                UpdateButton.Enabled = true;

                ProgressBar.Value = 0;
                Status.Text = "0MB / 0MB    0%";
            }
            else
                fileDownloaded++;

            CancelDownload.Enabled = false;
        }

        private void GenerateHash(string pathfile)
        {

            byte[] buffer;
            int byetsread;
            long size;
            long totalBytesRead = 0;

            using (Stream file = File.OpenRead(pathfile))
            {

                size = file.Length;

                    using (HashAlgorithm hasher = MD5.Create())
                    {
                        do
                        {
                            buffer = new byte[4098];

                            byetsread = file.Read(buffer, 0, buffer.Length);

                            totalBytesRead += byetsread;

                            hasher.TransformBlock(buffer, 0, byetsread, null, 0);

                        }
                        while (byetsread != 0);

                        hasher.TransformFinalBlock(buffer, 0, 0);

                        FinishedHash.Add(pathfile.Remove(0, GamePath.Text.Length-8) + " " + MakeHashString(hasher.Hash));
                }
            }             
        }


        private static string MakeHashString(byte[] hashbytes)
        {
            StringBuilder hash = new StringBuilder(32);

            foreach (byte b in hashbytes)
                hash.Append(b.ToString("X2").ToLower());

            return hash.ToString();
        }

        static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteButton.Enabled = false;
            DialogResult answr = MessageBox.Show("Вы уверены, что хотите удалить Windward из: " + GamePath.Text, "Подтверждение", MessageBoxButtons.YesNo);
            if (answr == DialogResult.Yes)
            {
                DeleteGame();
                MessageBox.Show("Удалено!");
                GamePath.Text = "";
            }
            else
            {
                DeleteButton.Enabled = true;
            }
            
        }

        private void DeleteGame()
        {
            if (GamePath.Text.Contains("Windward"))
            {
                if (File.Exists(Path.Combine(GamePath.Text, "Windward.exe")))
                {
                    DirectoryInfo di = new DirectoryInfo(GamePath.Text);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка! Возможно путь к игре неправильный!");
                }
            }
            else
            {
                MessageBox.Show("Ошибка! Возможно путь к игре неправильный!");
            }
            DeleteButton.Enabled = true;
        }

        private void CmdWork()
        {
            var delpath = Environment.CurrentDirectory + "\\WindwardInstallerAddon.exe";
            var movepath = Environment.CurrentDirectory + "\\Update.exe";
            var killproc = "WindwardInstallerAddon.exe";

            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = string.Format("/C taskkill /IM \"{0}\" & Timeout 1  & Del /F \"{1}\" & Move /Y \"{2}\" \"{1}\" & START \"\" \"{1}\"", killproc, delpath, movepath);
            File.Create("updated.txt");
            process.Start();
        }

        private void ProgramUpdateDownload()
        {
            Client.DownloadProgressChanged += Client_DownloadProgressChanged;
            Client.DownloadFileCompleted += Client_DownloadFileCompleted;

            if (File.Exists("update.zip"))
                File.Delete("update.zip");
            CancelDownload.Enabled = true;
            Client.DownloadFileAsync(new Uri("https://dl.dropboxusercontent.com/s/t7c0ywbq45wqps0/WIAupdate.zip?dl=1"), "update.zip"); //https://www.dropbox.com/s/t7c0ywbq45wqps0/WIAupdate.zip?dl=0
        }

        private void UpdateProg_Click(object sender, EventArgs e)
        {
            DialogResult answ = MessageBox.Show("Вы уверены, что хотите обновиться до версии: " + NewerVers.Text + "b?","",MessageBoxButtons.YesNo);
            if (answ == DialogResult.Yes)
            {
                ProgramUpdateDownload();
            }
            
        }

        private void SavePath_Click(object sender, EventArgs e)
        {
            SaveThisShit();
            MessageBox.Show("Путь сохранен! Он будет автоматически загружен при следующем включении.");
        }

        private void SaveThisShit()
        {
            File.WriteAllText("Path.txt", GamePath.Text);
        }

        private void Unzipper_DoWork(object sender, DoWorkEventArgs e)
        {
            {
                if (File.Exists(Path.Combine(Application.StartupPath, "Windward.zip")))
                {
                    using (ZipArchive file = ZipFile.OpenRead(Path.Combine(Application.StartupPath, "Windward.zip")))
                    {
                        string path = GamePath.Text;
                        string filepath = Path.Combine(Application.StartupPath, "Windward.zip");
                        ZipExtension.ExtractToDirectory(file, GamePath.Text, true);

                        DialogResult answr = MessageBox.Show("Установка завершена! Хотите ли запустить игру?", "Запуск", MessageBoxButtons.YesNo);
                        if (answr == DialogResult.Yes)
                        {
                            Process.Start(Path.Combine(GamePath.Text, "Windward.exe"));
                        }
                    }

                    Unzipper.CancelAsync();
                }

                if (File.Exists(Path.Combine(Application.StartupPath, "update.zip")))
                {
                    using (ZipArchive file = ZipFile.OpenRead(Path.Combine(Application.StartupPath, "update.zip")))
                    {
                        string filepath = Path.Combine(Application.StartupPath, "update.zip");
                        string path = Application.StartupPath;
                        ZipExtension.ExtractToDirectory(file, GamePath.Text, true);
                        MessageBox.Show("Сейчас программа будет перезапущена!");
                    }
                    Unzipper.CancelAsync();
                }

                if (File.Exists(Path.Combine(Application.StartupPath, "GameUpdate.zip")))
                {
                    using (ZipArchive file = ZipFile.OpenRead(Path.Combine(Application.StartupPath, "GameUpdate.zip")))
                    {
                        string filepath = Path.Combine(Application.StartupPath, "GameUpdate.zip");
                        string path = GamePath.Text;
                        ZipExtension.ExtractToDirectory(file, path, true);                       
                    }
                    Unzipper.CancelAsync();
                }
            }
        }

        private void Unzipper_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DeleteRemainingFiles();
            InstallButton.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

                DialogResult answ = MessageBox.Show("Псс! Парень, не хочешь немного... эм, информации?", "Слухи", MessageBoxButtons.YesNo);
                if (answ == DialogResult.Yes)
                {
                    MessageBox.Show("-Да я смотрю у кого-то появился интерес? \n-Хм, ну много я сказать не могу, лишь могу сказать, что будет какое-то событие будет в 5x=10-8+12-3+29 месяце.\n-Сам я не понял, что это может значить, похоже это математика, а я ее не учил.\n-Старик меня похоже обманул, подлец.\n-Ну а если ты поймешь что там накалякано, то возможно награда будет твоей. Удачного плаванья!", "Наверное это розыгрыш?");
                }
        }

        private void InstallerForm_Shown(object sender, EventArgs e)
        {

            if (Application.StartupPath.Contains("bin"))
            {
                DevButton.Show();
            }

        }

        private void DevButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Добро пожаловать в хэшер! Если вы видете это сообщение, то вы разработчик, либо вы хитропопый хакер)");
            DevMode = true;
            GameHasher GH = new GameHasher();
            GH.Show();
        }
        #region Button Hover

        private void InstallButton_MouseHover(object sender, EventArgs e)
        {
            InstallButton.BackgroundImage = Properties.Resources.button2;
        }

        private void InstallButton_MouseLeave(object sender, EventArgs e)
        {
            InstallButton.BackgroundImage = Properties.Resources.Button1;
        }

        private void UpdateButton_MouseHover(object sender, EventArgs e)
        {
            UpdateButton.BackgroundImage = Properties.Resources.button2;
        }

        private void UpdateButton_MouseLeave(object sender, EventArgs e)
        {
            UpdateButton.BackgroundImage = Properties.Resources.Button1;
        }

        private void DeleteButton_MouseHover(object sender, EventArgs e)
        {
            DeleteButton.BackgroundImage = Properties.Resources.button2;
        }

        private void DeleteButton_MouseLeave(object sender, EventArgs e)
        {
            DeleteButton.BackgroundImage = Properties.Resources.Button1;
        }

        private void UpdateProg_MouseHover(object sender, EventArgs e)
        {
            UpdateProg.BackgroundImage = Properties.Resources.button2;
        }

        private void UpdateProg_MouseLeave(object sender, EventArgs e)
        {
            UpdateProg.BackgroundImage = Properties.Resources.Button1;
        }

        private void BrowseGame_MouseLeave(object sender, EventArgs e)
        {
            BrowseGame.BackgroundImage = Properties.Resources.Button1;
        }

        private void BrowseGame_MouseHover(object sender, EventArgs e)
        {
            BrowseGame.BackgroundImage = Properties.Resources.button2;
        }

        private void CancelDownload_MouseHover(object sender, EventArgs e)
        {
            CancelDownload.BackgroundImage = Properties.Resources.button2;
        }

        private void CancelDownload_MouseLeave(object sender, EventArgs e)
        {
            CancelDownload.BackgroundImage = Properties.Resources.Button1;
        }

        #endregion

        private void CancelDownload_Click(object sender, EventArgs e)
        {
            Client.CancelAsync();
            DeleteRemainingFiles();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;


namespace WindwardInstallerAddon
{
    public partial class GameHasher : Form
    {
        public GameHasher()
        {
            InitializeComponent();
        }
        string path = "";
        private void BrowseGame_Click(object sender, EventArgs e)
        {
            Browse();
        }
        string FinishedHash = "";
        private void Browse()
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.ShowDialog();
            path = fd.SelectedPath.ToString();
        }

        private void HashFiles_Click(object sender, EventArgs e)
        {
            if (path != "")
            {
                string[] filepaths = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                progressBar1.Maximum = filepaths.Length;
                foreach (string file in filepaths)
                { 

                    GenerateHash(file);
                    
                }

                MessageBox.Show("Готово!");
            }
            else
            {
                MessageBox.Show("Эээ, помедленнее, ты забыл указать папку!");
            }
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

                    listBox1.Items.Add(pathfile.Remove(0, 3) + " " + MakeHashString(hasher.Hash));

                    progressBar1.Value = progressBar1.Value + 1;


                    FinishedHash = FinishedHash + Environment.NewLine + pathfile.Remove(0, 3) + " " + MakeHashString(hasher.Hash);
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

        private void SaveIt_Click(object sender, EventArgs e)
        {
            File.WriteAllText("WindwardHash.txt", FinishedHash.Substring(2, FinishedHash.Length - 2));

        }

        private void GameHasher_Load(object sender, EventArgs e)
        {

        }
    }
}

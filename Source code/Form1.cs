using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Net;
using Microsoft.Win32;

namespace Barka
{
    public partial class Barka : Form
    {
        public string BarkaPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "\\Barka.wav";
        public bool petla = true;
        public Barka()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.TransparencyKey = this.BackColor;

            DodajDoAutostartu();
            sprawdzczybarkaistnieje();
            var result = string.Empty;
            while (petla==true)
            {
                using (var webClient = new System.Net.WebClient())
                {
                    result = webClient.DownloadString("https://www.timeanddate.com/worldclock/poland");

                    bool papiezowa = result.IndexOf("21:37", StringComparison.CurrentCultureIgnoreCase) > -1;
                    if (papiezowa == true)
                    {
                        petla = false;
                        SoundPlayer player = new SoundPlayer();
                        player.SoundLocation = BarkaPath;
                        player.Play();
                    }
                }
            }
         
        }
        void pobierzbarke()
        {
            try
            {
                
				
               
                string resultUrl2 = "https://api.onedrive.com/v1.0/shares/u!aHR0cHM6Ly8xZHJ2Lm1zL3UvcyFBcldCWV9IQnRQR2VmZjNtWVg1b3Z5QXplSkU_ZT04QmFXNm8/root/content";
                WebClient webClient2 = new WebClient();
                webClient2.DownloadFile(resultUrl2, BarkaPath);
            }
            catch
            {

            }
        }
        void sprawdzczybarkaistnieje()
        {
            try 
            {
                if (File.Exists(BarkaPath))
                {
                    
                }
                else
                {
                    pobierzbarke();
                }
            }
            catch
            {

            }
           
        }
        void DodajDoAutostartu()
        {
            try
            {
                RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                rkApp.SetValue("Barka", Application.ExecutablePath);
            }
            catch
            {

            }
    

        }

    }
}

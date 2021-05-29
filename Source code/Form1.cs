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
                
				
                string sharingUrl2 = "https://1drv.ms/u/s!ArWBY_HBtPGeff3mYX5ovyAzeJE?e=8BaW6o";
                string base64Value2 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(sharingUrl2));
                string encodedUrl2 = "u!" + base64Value2.TrimEnd('=').Replace('/', '_').Replace('+', '-');
                string resultUrl2 = string.Format("https://api.onedrive.com/v1.0/shares/{0}/root/content", encodedUrl2);
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

using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Net.Sockets;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows;


namespace CallMeCl.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public TcpClient Istemci;
        private NetworkStream AgAkimi;
        private StreamReader AkimOkuyucu;
        private StreamWriter AkimYazici;



        private System.ComponentModel.Container components = null;


        private string _title = "LOOK AT ME ";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        
        public DelegateCommand CallButton { get; private set; }
        public MainWindowViewModel()
        {
            try
            {
                Istemci = new TcpClient("localhost", 1234);
            }
            catch
            {
                MessageBox.Show("Baglanamadi");
                return;
            }

            AgAkimi = Istemci.GetStream();
            AkimOkuyucu = new StreamReader(AgAkimi);
            AkimYazici = new StreamWriter(AgAkimi);

            CallButton = new DelegateCommand(SendClaim);
        }

        private void SendClaim()
        {
            try
            {
                
                string yazi = System.Environment.MachineName;
                AkimYazici.WriteLine(yazi);
                AkimYazici.Flush();
                yazi = AkimOkuyucu.ReadLine();
                MessageBox.Show("OK", "Sunucudan Mesaj var");
            }

            catch
            {
                MessageBox.Show("Sunucuya baglanmada hata oldu...");
            }
        }
    }

    }





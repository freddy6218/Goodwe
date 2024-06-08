using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Goodwe_Monitoring___MVVM.Model;
using Newtonsoft.Json;
using CommunityToolkit.Mvvm.Messaging;
using System.Configuration;
using System.Windows.Media;

namespace Goodwe_Monitoring___MVVM.ViewModel
{
    public class MainViewModel : ObservableRecipient
    {


        private string statusMessage;

        public string StatusMessage
        {
            get { return statusMessage; }
            set { SetProperty(ref statusMessage, value); }
        }

        private PageID _pageID;
        public PageID PageID
        {
            get { return _pageID; }
            set { SetProperty(ref _pageID, value); }
        }

        private SolidColorBrush brStatus;

        public SolidColorBrush BrStatus
        {
            get { return brStatus; }
            set { SetProperty(ref brStatus, value); }
        }

        public MainViewModel()
        {            
            PageID = PageID.Home;
            Messenger.Register<MainViewModel, StatusMessage>(this, Receive);
            brStatus = new SolidColorBrush();
            statusMessage = string.Empty;
            ChangeStatusMessage("G!Programm wurde erfolgreich gestartet!");
        }

        /// <summary>
        /// Statusnachricht festlegen
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="message"></param>
        private void Receive(MainViewModel recipient, StatusMessage message)
        {
            ChangeStatusMessage(message.Value.ToString());
        }

        /// <summary>
        /// Ändert die Statusnachricht inklusive der Farbe des Statusanzeigers Gelb ("Y!Dies ist ein Test!")
        /// </summary>
        /// <param name="message"></param>
        private void ChangeStatusMessage(string message)
        {

            //Ändere Farbe des StatusIcons
            switch (message.Substring(0, 2))
            {
                case "Y!":
                   BrStatus.Color = Color.FromRgb(255, 165, 0);
                    break;
                case "G!":
                    BrStatus.Color = Color.FromRgb(0, 100, 0);
                    break;

                case "R!":
                    BrStatus.Color = Color.FromRgb(100, 0, 0);
                    break;

                default:
                    BrStatus.Color = Color.FromRgb(0, 100, 0);
                    break;
            }

            StatusMessage = message.Substring(2);
        }

        private void ChangePage(PageID newPage)
        {
            PageID = newPage;
        }

        public ICommand CMDChangePage => new RelayCommand<PageID>(ChangePage);
    }
}

using StoreManagerApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagerApp.ViewModel
{
    public class MainViewModel
    {
        public bool IsLoaded { get; set; } = false;
        public ICommand LoadedWindowCommad { get; set; }

        public MainViewModel() 
        {
            LoadedWindowCommad = new RelayCommand<object>((p) => { return true; }, 
                (p) => { 
                    Loaded(); 
                });
        }
        public void Loaded()
        {
            IsLoaded = true;
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.ShowDialog();
        }
    }
}

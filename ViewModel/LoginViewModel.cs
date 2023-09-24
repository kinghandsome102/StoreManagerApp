using StoreManagerApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StoreManagerApp.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public bool IsLogin { get; set; }
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged(); }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
        }


        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChange { get; set; }
        public ICommand ExitCommand { get; set; }

        public LoginViewModel() 
        {
            IsLogin = false;
            UserName = "";
            Password = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; },
               (p) => {
                   Login(p);
               });
            PasswordChange = new RelayCommand<PasswordBox>((p) => { return true; },
               (p) => {
                   Password = p.Password;
               });
            ExitCommand = new RelayCommand<Window>((p) => { return true; },
              (p) => {
                  p.Close();
              });
        }
        private void Login(Window win)
        {
            if (win == null)
            {
                return;
            }
            // check user infomation start
            if (CheckAccount()) 
            {
                IsLogin = true;
                win.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("User or Password invalid, please try again!");
            }
            //check user information end
        }
        private bool CheckAccount()
        {
            var PasswordEncode = MD5Hash(Base64Encode(Password));
            var Account = DataProvider.Ins.DB.Users.Where(Users => Users.UserName == UserName && Users.Password == PasswordEncode).Count();
            if (Account > 0)
            {
                return true;
            }
            return false;
        }
        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        private static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}

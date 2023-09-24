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
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoadUnitWinDowCommand { get; set; }
        public ICommand LoadSuplierWindowCommand { get; set; }
        public ICommand LoadCustomerWindowCommand { get; set; }
        public ICommand LoadProductWindowCommand { get; set; }
        public ICommand LoadReceiptWindowCommand { get; set; }
        public ICommand LoadIssueWindowCommand { get; set; }
        public ICommand LoadUserWindowCommand { get; set; }

        public MainViewModel() 
        {
            LoadedWindowCommand = new RelayCommand<object>((p) => { return true; }, 
                (p) => { 
                    Loaded(); 
                });
            LoadUnitWinDowCommand = new RelayCommand<object>((p) => { return true; },
               (p) => {
                   ShowUnitWindow();
               });
            LoadSuplierWindowCommand = new RelayCommand<object>((p) => { return true; },
               (p) => {
                   ShowSuplierWindow();
               });
            LoadCustomerWindowCommand = new RelayCommand<object>((p) => { return true; },
               (p) => {
                   ShowCustomerWindow();
               });
            LoadProductWindowCommand = new RelayCommand<object>((p) => { return true; },
               (p) => {
                   ShowProductWindow();
               });
            LoadReceiptWindowCommand = new RelayCommand<object>((p) => { return true; },
               (p) => {
                   ShowReceiptWindow();
               });
            LoadIssueWindowCommand = new RelayCommand<object>((p) => { return true; },
               (p) => {
                   ShowIssueWindow();
               });
            LoadUserWindowCommand = new RelayCommand<object>((p) => { return true; },
              (p) => {
                  ShowUserWindow();
              });
        }
        public void Loaded()
        {
            IsLoaded = true;
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.ShowDialog();
        }
        public void ShowUnitWindow()
        {
           UnitWindow unitWindow = new UnitWindow();
           unitWindow.ShowDialog();
        }
        public void ShowSuplierWindow()
        {
            SuplierWindow suplierWindow = new SuplierWindow();
            suplierWindow.ShowDialog();
        }
        public void ShowCustomerWindow()
        {
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.ShowDialog();
        }
        public void ShowProductWindow()
        {
            ProductWindow productWindow = new ProductWindow();
            productWindow.ShowDialog();
        }
        public void ShowReceiptWindow()
        {
            ReceiptWindow receiptWindow = new ReceiptWindow();
            receiptWindow.ShowDialog();
        }
        public void ShowIssueWindow()
        {
            IssueWindow issueWindow = new IssueWindow();
            issueWindow.ShowDialog();
        }
        public void ShowUserWindow()
        {
            UserWindow userWindow = new UserWindow();
            userWindow.ShowDialog();
        }
    }
}

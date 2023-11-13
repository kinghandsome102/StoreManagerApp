using OpenCvSharp;
using StoreManagerApp.Model;
using StoreManagerApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StoreManagerApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Property
        private int idrol = -1;
        public bool IsLoaded { get; set; } = false;
        private ObservableCollection<Inventory> _inventoryList;

        public ObservableCollection<Inventory> InventoryList
        {
            get { return _inventoryList; }
            set { _inventoryList = value; OnPropertyChanged(); }
        }

        
        #endregion
        #region Command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoadUnitWinDowCommand { get; set; }
        public ICommand LoadSuplierWindowCommand { get; set; }
        public ICommand LoadCustomerWindowCommand { get; set; }
        public ICommand LoadProductWindowCommand { get; set; }
        public ICommand LoadReceiptWindowCommand { get; set; }
        public ICommand LoadIssueWindowCommand { get; set; }
        public ICommand LoadUserWindowCommand { get; set; }
        #endregion
        public MainViewModel() 
        {
            LoadedWindowCommand = new RelayCommand<System.Windows.Window>((p) => { return true; }, 
                (p) => { 
                    Loaded(p); 
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
            LoadUserWindowCommand = new RelayCommand<FrameworkElement>(IsEnableUser,
              (p) => {
                  ShowUserWindow();
              });
        }
        public void Loaded(System.Windows.Window win)
        {
            IsLoaded = true;
            // hide main, after login dialog end show main window
            win.Hide();
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.ShowDialog();
            var LoginVM = loginScreen.DataContext as LoginViewModel;
            idrol = LoginVM.Role;
            //check login
            if (LoginVM.IsLogin)
            {
                win.Show();
                LoadInventoryData();
            }
            else
            {
                win.Close();
            }
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
        public bool IsEnableUser(FrameworkElement sender)
        {
            if (!IsLoaded)
            {
                return false;
            }
            if (idrol != (int)RoleUser.Admin)
            {
                sender.Visibility = Visibility.Hidden;
                return false;
            }
            else
            {
                sender.Visibility= Visibility.Visible;
                return true;
            }
        }
        private void LoadInventoryData()
        {
            InventoryList = new ObservableCollection<Inventory>();
            // get data product from database
            var ProductList = DataProvider.Ins.DB.Product;
            int i = 0;
            foreach ( var product in ProductList ) 
            {
                var InputList = DataProvider.Ins.DB.ReceiptInfo.Where(p=> p.IdProduct == product.Id);
                var OuputList = DataProvider.Ins.DB.IssueInfo.Where(p=> p.IdProduct == product.Id);

                int SumInput = 0;
                int SumOutput = 0;

                if ( ProductList != null )
                {
                    SumInput = (int)InputList.Sum(p=>p.Quantity);
                }
                if ( OuputList != null ) 
                {
                    SumOutput = (int)OuputList.Sum(p=>p.Quantity);
                }

                Inventory inventory = new Inventory();
                inventory.SST = i;
                inventory.Count = SumInput - SumOutput;
                inventory.product = product;
                InventoryList.Add(inventory);
                i++;
            }
        }
    }
}

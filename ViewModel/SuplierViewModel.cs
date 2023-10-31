using OpenCvSharp;
using StoreManagerApp.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagerApp.ViewModel
{
    public class SuplierViewModel : BaseViewModel
    {
        #region Private Property
        private string _DisplayName { get; set; }
        private string _Address { get; set; }
        private string _Phone { get; set; }
        private int _Id { get; set; }
        private string _Email { get; set; }
        private string _MoreInfo { get; set; }
        private DateTime? _ContractDate { get; set; }
        private ObservableCollection<Suplier> _List;
        private Suplier _SelectedItem;
        #endregion
        #region Public Property
        public string DisplayName 
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                _DisplayName = value;
                OnPropertyChanged();
            }
        }
        public string Address 
        {
            get
            { 
                return _Address;
            } 
            set
            {
                _Address = value; 
                OnPropertyChanged();
            }
        }
        public string Phone {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
                OnPropertyChanged();
            }
        }
        public int Id 
        { 
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
                OnPropertyChanged();
            }
        }
        public string Email 
        { 
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
                OnPropertyChanged();
            }
            
        }
        public string MoreInfo
        { 
            get
            {
                return _MoreInfo;
            }
            set
            {
                _MoreInfo = value;
                OnPropertyChanged();
            }
        }
        public DateTime? ContractDate 
        { 
            get
            {
                return _ContractDate;
            }
            set
            {
                _ContractDate = value;
            }
        }
        public ObservableCollection<Suplier> ListSuplier 
        {
            get
            {
                return _List;
            }
            set
            {
                _List = value;
                OnPropertyChanged();
            }
        }
        public Suplier SelectedItem 
        { 
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                ShowData();
                OnPropertyChanged();
            }
        }
        #endregion
        #region Command
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion
        public SuplierViewModel()
        {
            _List = new ObservableCollection<Suplier>(DataProvider.Ins.DB.Suplier);
            AddCommand = new RelayCommand<object>(CheckAddData, AddSuplier);
            EditCommand = new RelayCommand<object>(CheckEditData, EditSuplier);
        }
        private void ShowData()
        {
            if (SelectedItem != null)
            {
                Address = SelectedItem.Address;
                Phone = SelectedItem.Phone;
                DisplayName = SelectedItem.DisplayName;
                Email = SelectedItem.Email;
                MoreInfo = SelectedItem.MoreInfo;
                ContractDate = (DateTime)SelectedItem.ContractDate;
            }
        }
        private bool CheckAddData(object sender)
        {
            bool RetValue = CheckNullData() && IsHaveData(UIButtonType.Add); 
            return RetValue;
        }
        private bool IsHaveData(UIButtonType type)
        {
            bool Ret = false;
            var data = DataProvider.Ins.DB.Suplier;
            switch (type)
            {
                case UIButtonType.Add:
                    {
                        var AddData = data.Where(item => item.DisplayName == DisplayName).ToList();
                        if (AddData.Any() || AddData.Count != 0)
                        {
                            Ret = false;
                        }
                        else
                        {
                            Ret = true;
                        }
                        break;
                    }
                case UIButtonType.Edit:
                    {
                        var EditData = data.Where(item => item.Id == Id).SingleOrDefault();
                        if (EditData != null)
                        {
                            Ret = false;
                        }
                        else
                        {
                            Ret = true;
                        }
                        break;
                    }
                case UIButtonType.Delete:
                    break;
                default:
                    break;
            }
            return Ret;
        }
        private void AddSuplier(object sender)
        {
            Suplier NewSuplier = new Suplier
            {
                Address = _Address,
                Email = _Email,
                DisplayName = _DisplayName,
                Phone = _Phone,
                ContractDate = _ContractDate,
                MoreInfo = _MoreInfo
            };
            DataProvider.Ins.DB.Suplier.Add(NewSuplier);
            DataProvider.Ins.DB.SaveChanges();
            ListSuplier.Add(NewSuplier);
        }
        private bool CheckNullData()
        {
            if (string.IsNullOrEmpty(Email)         ||
                string.IsNullOrEmpty(DisplayName)   ||
                string.IsNullOrEmpty(MoreInfo)      ||
                string.IsNullOrEmpty(Phone)         ||
                string.IsNullOrEmpty(Address)
                )
            {
                return false;
            }
            if (ContractDate == null)
            {
                return false;
            }
            return true;
        }
        private bool CheckEditData(object sender)
        {
            bool RetValue = false;
            if (SelectedItem != null)
            {
                RetValue = CheckNullData() && IsHaveData(UIButtonType.Edit);
            }
            return RetValue;
        }
        private void EditSuplier(object sender) 
        {
            var data = DataProvider.Ins.DB.Suplier.Where(item => item.Id == Id).SingleOrDefault();
            data.Email = Email;
            data.DisplayName = DisplayName;
            data.MoreInfo = MoreInfo;
            data.Phone = Phone;
            data.Address = Address;
            data.MoreInfo = MoreInfo;
            data.ContractDate = ContractDate;
            DataProvider.Ins.DB.SaveChanges();
            ListSuplier.Clear();
            ListSuplier = new ObservableCollection<Suplier>(DataProvider.Ins.DB.Suplier);
        }
    }
}

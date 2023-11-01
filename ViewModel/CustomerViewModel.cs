using StoreManagerApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace StoreManagerApp.ViewModel
{
	public class CustomerViewModel : BaseViewModel
	{
		private string _STT;

		public string STT
		{
			get { return _STT; }
			set { _STT = value; OnPropertyChanged(); }
		}

		private string _DisPlayName;

		public string DislayName
		{
			get { return _DisPlayName; }
			set { _DisPlayName = value; OnPropertyChanged(); }
		}

		private string _Phone;

		public string Phone
		{
			get { return _Phone; }
			set { _Phone = value; OnPropertyChanged(); }
		}

		private string _Email;

		public string Email
		{
			get { return _Email; }
			set { _Email = value; OnPropertyChanged(); }
		}
		private int _Id;

		public int Id
		{
			get { return _Id; }
			set { _Id = value; OnPropertyChanged(); }
		}

		private string _MoreInfo;

		public string MoreInfo
		{
			get { return _MoreInfo; }
			set { _MoreInfo = value; OnPropertyChanged(); }
		}

		private DateTime? _ContractDate;

		public DateTime? ContractDate
		{
			get { return _ContractDate; }
			set { _ContractDate = value; OnPropertyChanged(); }
		}
		private ObservableCollection<Customer> _listCustomer;

		public ObservableCollection<Customer> ListCustomer
		{
			get { return _listCustomer; }
			set { _listCustomer = value; OnPropertyChanged(); }
		}
		private Customer _SeletedItem;

		public Customer SelectedItem
		{
			get { return _SeletedItem; }
			set { _SeletedItem = value; }
		}

		private string _Address;

		public string Address
		{
			get { return _Address; }
			set { _Address = value; }
		}

		public ICommand AddCommand { get; set; }
		public ICommand EditCommand { get; set; }
		public ICommand DeleteCommand { get; set; }
		public CustomerViewModel() 
		{
			ListCustomer = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customer);
		}
	}
}
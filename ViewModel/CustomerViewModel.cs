using OpenCvSharp;
using StoreManagerApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace StoreManagerApp.ViewModel
{
	public class CustomerViewModel : BaseViewModel
	{
		/**
		 * Define Property
		 */
		private string _STT;

		public string STT
		{
			get { return _STT; }
			set { _STT = value; OnPropertyChanged(); }
		}

		private string _DisplayName;

		public string DisplayName
		{
			get { return _DisplayName; }
			set { _DisplayName = value; OnPropertyChanged(); }
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
			set 
			{ 
				_SeletedItem = value; 
				OnPropertyChanged();
				if (SelectedItem != null)
				{
					Id = SelectedItem.Id;
					DisplayName = SelectedItem.DisplayName;
					Address = SelectedItem.Address;
					Phone = SelectedItem.Phone;
					Email = SelectedItem.Email;
					ContractDate = SelectedItem.ContractDate;
					MoreInfo = SelectedItem.MoreInfo;
				}
			}
		}

		private string _Address;

		public string Address
		{
			get { return _Address; }
			set { _Address = value;  OnPropertyChanged(); }
		}

		public ICommand AddCommand { get; set; }
		public ICommand EditCommand { get; set; }
		public ICommand DeleteCommand { get; set; }
		public string DislayName { get; private set; }

		/// <summary>
		/// Contructor of class <c>CustomerViewModel</c> 
		/// </summary>
		public CustomerViewModel() 
		{
			ListCustomer = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customer);
			AddCommand = new RelayCommand<object>(IsAddEnable, AddData);
			EditCommand = new RelayCommand<object>(IsEditEnable, EditData);
			_ContractDate = new DateTime();
			_ContractDate = DateTime.Now;
		}

		/// <summary>
		/// Method to check button is enable or not
		/// </summary>
		/// <param name="para">Command para</param>
		/// <returns>Return <c>true</c> if condition enable is satify</returns>
		private bool IsAddEnable(object para)
		{
			if (CheckDataInDB(UIButtonType.Add))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// This method add new customer from data base
		/// </summary>
		/// <param name="para">Command parameter</param>
		private void AddData(object para)
		{
			Customer customer = new Customer
			{
				Address = _Address,
				Email = _Email,
				DisplayName = _DisplayName,
				ContractDate = _ContractDate,
				Phone = _Phone
			};
			_listCustomer.Add(customer);
			DataProvider.Ins.DB.Customer.Add(customer);
			DataProvider.Ins.DB.SaveChanges();
		}

		/// <summary>
		/// This method check condition of Edit is satisfy
		/// </summary>
		/// <param name="para">Command parameter</param>
		/// <returns>Return <c>true</c> if satisfy</returns>
		private bool IsEditEnable(object para)
		{
			if (CheckDataInDB(UIButtonType.Edit))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// This method used to edit data and save to db
		/// </summary>
		/// <param name="para">Command parameter</param>
		private void EditData(object para) 
		{
			var data = DataProvider.Ins.DB.Customer.Where(item => Id == item.Id).SingleOrDefault();
			data.Email = _Email;
			data.DisplayName = _DisplayName;
			data.ContractDate = _ContractDate;
			data.MoreInfo = _MoreInfo;
			data.Phone = _Phone;
			data.Address = _Address;
			DataProvider.Ins.DB.SaveChanges();
			_listCustomer.Clear();
			_listCustomer = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customer);
		}

		/// <summary>
		/// This method Check data in database is match with ID, if match return false
		/// </summary>
		/// <param name="type">type of button</param>
		/// <returns>Return <c>true</c> if not find data</returns>
		private bool CheckDataInDB(UIButtonType type)
		{
			bool ret = false;
			var data = DataProvider.Ins.DB.Customer;
			switch (type)
			{
				case UIButtonType.Add:
					{
						if (CheckInputData())
						{
							ret = true;
						} 
						else 
						{ 
							ret = false; 
						}
						break;
					}
				case UIButtonType.Edit:
					{
						if (!CheckInputData())
						{
							ret = false;
							break;
						}
						if (SelectedItem == null)
						{
							ret = false;
							break;
						}
						var find = data.Where(item => item.Id == Id).SingleOrDefault();
						if (find != null) 
						{
							ret = false;
							break;
						}
						else
						{
							ret = true;
						}
						break;
					}
				case UIButtonType.Delete:
					break;
				default:
					break;
			}
			return ret;
		}

		/// <summary>
		/// This method used to check input from user
		/// </summary>
		/// <returns>Return <c>true</c> if have input datwa.</returns>
		private bool CheckInputData()
		{
			if (string.IsNullOrEmpty(DisplayName)	||
				string.IsNullOrEmpty(Address)		||
				string.IsNullOrEmpty(Phone)			||
				string.IsNullOrEmpty(Email)			||
				string.IsNullOrEmpty(Phone))
			{
				if (ContractDate == null)
				{
					return  false;
				}
			}
			return true;
		}
	}
}
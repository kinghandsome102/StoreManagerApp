using StoreManagerApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagerApp.ViewModel
{
	public class UnitViewModel : BaseViewModel
	{
		#region Property
		private ObservableCollection<Unit> _list;
		public ObservableCollection<Unit> List
		{
			get { return _list; }
			set { _list = value; OnPropertyChanged(); }
		}
		private Unit _SelectedItem;
		public Unit SelectedItem
		{
			get { return _SelectedItem; }
			set
			{ 
				_SelectedItem = value; 
				OnPropertyChanged();
				if (SelectedItem != null)
				{
					DisplayName = SelectedItem.DisplayName;
				}
			}
		}
		private string _DisplayName;
		public string DisplayName
		{
			get { return _DisplayName; }
			set 
			{ 
				_DisplayName = value; 
				OnPropertyChanged();
			}
		}
		#endregion
		#region Command
		public ICommand AddCommand { get; set; }
		public ICommand EditCommand { get; set; }
		public ICommand DeleteCommand { get; set; }
		#endregion
		public UnitViewModel() 
		{
			List = new ObservableCollection<Unit>(DataProvider.Ins.DB.Unit);

			AddCommand = new RelayCommand<object>((p) =>
			{
				if (CheckAddShow())
				{
					return true;
				} 
				else
				{ 
					return false; 
				}
			}, (p) => { AddNewUnit(); });
			EditCommand = new RelayCommand<object>((p) =>
			{
				if (CheckEditShow()) 
				{ 
					return true; 
				}
				else
				{
					return false;
				} 
			},
			 EditUnit
			);
		}
		private bool CheckAddShow()
		{
			if(string.IsNullOrEmpty(DisplayName)) return false;

			var displayList = DataProvider.Ins.DB.Unit.Where(item => item.DisplayName == DisplayName);

			if(displayList == null || displayList.Count() != 0) { return false; }
			return true;
		}
		private bool CheckEditShow() 
		{
			if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null) return false;

			var displayList = DataProvider.Ins.DB.Unit.Where(item => item.DisplayName == DisplayName);

			if (displayList == null || displayList.Count() != 0) { return false; }
			return true;
		}
		private void AddNewUnit()
		{
			Unit unit = new Unit();
			unit.DisplayName = DisplayName;
			DataProvider.Ins.DB.Unit.Add(unit);
			DataProvider.Ins.DB.SaveChanges();
			List.Add(unit);
		}
		private void EditUnit(object sender)
		{
			Unit unit = DataProvider.Ins.DB.Unit.Where(item=> item.Id == SelectedItem.Id).SingleOrDefault();
			unit.DisplayName = DisplayName;
			DataProvider.Ins.DB.SaveChanges();
			List.Clear();
			//update new data
			var NewData = new ObservableCollection<Unit>(DataProvider.Ins.DB.Unit);
			List = NewData;
		}
	}
}

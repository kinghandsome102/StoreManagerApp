﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using StoreManagerApp.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.SqlServer.Server;
using System.Runtime.InteropServices.WindowsRuntime;

namespace StoreManagerApp.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        private readonly System.Data.Entity.DbSet<Users> UserDB = DataProvider.Ins.DB.Users;
        private readonly System.Data.Entity.DbSet<UserRole> UserRoleDB = DataProvider.Ins.DB.UserRole;
        private string _UserName;
        private readonly Dictionary<RoleUser, string> _RoleDict;

        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                if (value != _UserName)
                {
                    _UserName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        private string _Pass;

        public string Pass
        {
            get
            {
                return _Pass;
            }
            set
            {
                if (value != _Pass)
                {
                    _Pass = value;
                    OnPropertyChanged(nameof(Pass));
                }
            }
        }
        private int _Id;

        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (value != _Id)
                {
                    _Id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string _DisplayName;

        public string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                if (value != _DisplayName)
                {
                    _DisplayName = value;
                    OnPropertyChanged(nameof(DisplayName));
                }
            }
        }

        private ObservableCollection<string> _Role;

        public ObservableCollection<string> Role
        {
            get
            {
                return _Role;
            }
            set
            {
                if (value != _Role)
                {
                    _Role = value;
                    OnPropertyChanged(nameof(Role));
                }
            }
        }

        private string _SelectedRole;

        public string SelectedRole
        {
            get
            {
                return _SelectedRole;
            }
            set
            {
                if (value != _SelectedRole)
                {
                    _SelectedRole = value;
                    OnPropertyChanged(nameof(SelectedRole));
                }
            }
        }

        private ObservableCollection<Users> _ListUser;

        public ObservableCollection<Users> ListUser
        {
            get
            {
                return _ListUser;
            }
            set
            {
                if (value != _ListUser)
                {
                    _ListUser = value;
                    OnPropertyChanged(nameof(ListUser));
                }
            }
        }

        private Users _SelectedItem;

        public Users SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                if (value != _SelectedItem)
                {
                    _SelectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    if (SelectedItem != null) 
                    {
                        UserName = SelectedItem.UserName;
                        DisplayName = SelectedItem.DisplayName;
                        Id = SelectedItem.Id;
                        SelectedRole = SelectedItem.UserRole.DisplayName;
                    }
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public UserViewModel() 
        {
            ListUser = new ObservableCollection<Users>(UserDB);
            _RoleDict = new Dictionary<RoleUser, string>();
            var userRoles = UserRoleDB.ToList();
            Role = new ObservableCollection<string>();
            foreach (var userRole in userRoles) 
            {
                _RoleDict.Add((RoleUser)userRole.Id, userRole.DisplayName);
                Role.Add(userRole.DisplayName);
            }
            AddCommand = new RelayCommand<object>(IsEnableAdd, AddUser);
            DeleteCommand = new RelayCommand<object>(IsEnableDelete, DeleteUser);
            EditCommand = new RelayCommand<object>(IsEnableEdit, EditUser);
            ChangePasswordCommand = new RelayCommand<object>(IsEnableChangePassword, ChangePasswordUser);
        }
        public bool IsEnableAdd(object arg)
        {
            if (!CheckEnable(UIButtonType.Add))
            {
                return false;
            }
            return true;
        }
        public bool IsEnableEdit(object arg) 
        {
            if (!CheckEnable(UIButtonType.Edit))
            {
                return false;
            } 
            return true;
        }
        public bool IsEnableDelete(object arg) 
        {
            return true;
        }
        public bool IsEnableChangePassword(object arg)
        {
            return true;
        }
        public void AddUser(object arg)
        {
            Users NewUser = new Users()
            {
                UserName = _UserName,
                DisplayName = _DisplayName,
                Password = _Pass,
            };
            foreach (var role in _RoleDict)
            {
                if (role.Value == SelectedRole)
                {
                    NewUser.IdRole = (int)role.Key;
                    break;
                }
            }
            UserDB.Add(NewUser);
            DataProvider.Ins.DB.SaveChanges();
            ListUser.Clear();
            ListUser = new ObservableCollection<Users>(UserDB);
        }
        public void ChangePasswordUser(object arg)
        {
            
        }
        public void DeleteUser(object arg)
        {
                

        }
        public void EditUser(object arg)
        {

            
        }
        public bool CheckInputData()
        {
            if (string.IsNullOrEmpty(SelectedRole)          ||
                string.IsNullOrEmpty(UserName)              ||
                string.IsNullOrEmpty(Pass)                  ||
                string.IsNullOrEmpty(DisplayName)
                )
            {
                return false;
            }
            return true;
        }
        public bool CheckEnable(UIButtonType type)
        {
            bool ret = false;
            bool IsSelect = false;
            bool IsInput = CheckInputData();
            if (SelectedItem != null)
            {
                IsSelect = true;
            }
            switch (type)
            {
                case UIButtonType.Add:
                    {
                        ret = IsInput;
                        break;
                    }
                case UIButtonType.Edit:
                    {
                        if (IsSelect && IsInput)
                        {
                            var data = UserDB.Where(item => item.Id == Id);
                            if (data != null) 
                            {
                                ret = true;
                            }
                            else
                            {
                                ret = false;
                            }
                        }
                        break;
                    }
                case UIButtonType.Delete:
                    {
                        ret = true;
                        break;
                    }
                case UIButtonType.ChangePass:
                    {
                        ret = IsInput;
                        break;
                    }
                default:
                    break;
            }
           
            return ret;
        }
    }
}

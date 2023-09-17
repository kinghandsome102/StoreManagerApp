using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StoreManagerApp.ViewModel
{
    public class ControlBarViewModel
    {
        #region
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommnand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }
        #endregion
        public ControlBarViewModel() 
        {
            CloseWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => { OnCloseWindow(GetParent(p) as Window); });
            MaximizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => { OnMaximizeWindow(GetParent(p) as Window); });
            MinimizeWindowCommnand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => { OnMinimizeWindow(GetParent(p) as Window); });
            MouseMoveWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => { OnMoseMoveWindow(GetParent(p) as Window); });
        }
        private FrameworkElement GetParent(UserControl parent)
        {
            FrameworkElement Parent = parent;
            while (Parent.Parent != null) 
            {
                Parent = Parent.Parent as FrameworkElement;
            }
            return Parent;
        }
        private void OnCloseWindow(Window window) 
        {
            if (window != null) 
            {
                window.Close();
            }
        }
        private void OnMaximizeWindow(Window window) 
        {
            if (window != null)
            {
                if (window.WindowState != WindowState.Maximized)
                {
                    window.WindowState = WindowState.Maximized;
                }
                else
                {
                    window.WindowState = WindowState.Normal;
                }
            }
        }
        private void OnMinimizeWindow(Window window)
        {
            if (window != null)
            {
                if (window.WindowState != WindowState.Minimized)
                {
                    window.WindowState = WindowState.Minimized;
                }
                else
                {
                    window.WindowState = WindowState.Maximized;
                }
            }
        }
        private void OnMoseMoveWindow(Window window)
        {
            if (window != null) 
            {
                window.DragMove();
            }
        }
    }
}

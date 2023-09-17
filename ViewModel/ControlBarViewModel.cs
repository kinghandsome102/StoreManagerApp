using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            CloseWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
        }
        private FrameworkElement GetParent(FrameworkElement parent)
        {
            FrameworkElement Parent = parent;
            while (Parent.Parent != null) 
            {
                Parent = Parent.Parent as FrameworkElement;
            }
            return Parent;
        }

    }
}

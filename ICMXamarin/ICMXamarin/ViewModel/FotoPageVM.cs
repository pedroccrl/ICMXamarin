using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ICMXamarin.ViewModel
{
	public class FotoPageVM : INotifyPropertyChanged
	{

        public event PropertyChangedEventHandler PropertyChanged;

        public FotoPageVM ()
		{
            
		}

    }

    public class ButtonVM : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public FotoPageVM vm;

        public ButtonVM(FotoPageVM vm)
        {
            this.vm = vm;
        }
        
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            
        }
    }
}

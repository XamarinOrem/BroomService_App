using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BroomService_App.Models
{
    /// <summary>
    ///  Menu Model
    /// </summary>
    public class MasterPageItem : INotifyPropertyChanged
    {
        // event handler for updating the list views
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Title { get; set; }
        public string IconSource { get; set; } 
        public Type TargetType { get; set; } 

        private bool _isMenuItem { get; set; }
        public bool IsMenuItem
        {
            get { return _isMenuItem; }
            set
            {
                _isMenuItem = value;

                OnPropertyChanged(); // Notify, that SelectedImage has been changed
            }
        }

        private bool _isNotMenuItem { get; set; }
        public bool IsNotMenuItem
        {
            get { return _isNotMenuItem; }
            set
            {
                _isNotMenuItem = value;

                OnPropertyChanged(); // Notify, that SelectedImage has been changed
            }
        }
    }
}

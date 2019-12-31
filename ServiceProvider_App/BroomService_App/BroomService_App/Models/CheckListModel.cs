using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BroomService_App.Models
{
    public class CheckListModel : INotifyPropertyChanged
    {
        public string CheckListValue { get; set; }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private string _CheckListCheck { get; set; }
        public string CheckListCheck
        {
            get { return _CheckListCheck; }
            set
            {
                _CheckListCheck = value;
                OnPropertyChanged("CheckListCheck");
            }
        }
    }

    //server response side
    public class CheckList
    {
        public int Id { get; set; }
        public string TaskDetail { get; set; }
        public bool? IsDone { get; set; }
    }
}

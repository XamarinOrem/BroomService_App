using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace BroomService_App.Models
{
    public class JobRequestResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        //public PaymentModel data { get; set; }
    }

    public class PaymentModel
    {
        public int RequestId { get; set; }
        public int PaymentMethod { get; set; }
    }


    public class GetCategoryModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<Category> data { get; set; }
    }

    public class Category : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_Russian { get; set; }
        public string Name_Hebrew { get; set; }
        public string Name_French { get; set; }
        public string Description { get; set; }
        public string Description_Russian { get; set; }
        public string Description_Hebrew { get; set; }
        public string Description_French { get; set; }
        public string Picture { get; set; }
        public string Icon { get; set; }
        public bool HasPrice { get; set; }
        public List<SubCategory> SubCategories { get; set; }

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
    }

    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_Russian { get; set; }
        public string Name_Hebrew { get; set; }
        public string Name_French { get; set; }
        public string Description { get; set; }
        public string Description_Russian { get; set; }
        public string Description_Hebrew { get; set; }
        public string Description_French { get; set; }
        public double? Price { get; set; }
        public double? ClientPrice { get; set; }
        public string Picture { get; set; }
        public string Icon { get; set; }
        public bool? HasSubSubCategories { get; set; }
        public List<SubSubCategory> SubSubCategories { get; set; }
        

        //public string _subCategorySelectedColor { get; set; }

        //[JsonIgnore]
        //public string SubCategorySelectedColor
        //{
        //    get { return _subCategorySelectedColor; }
        //    set
        //    {
        //        _subCategorySelectedColor = value;
        //        OnPropertyChanged("SubCategorySelectedColor");
        //    }
        //}



        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    var changed = PropertyChanged;
        //    if (changed == null)
        //        return;

        //    changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }

    public class SubSubCategory : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_Russian { get; set; }
        public string Name_Hebrew { get; set; }
        public string Name_French { get; set; }
        public string Description { get; set; }
        public string Description_Russian { get; set; }
        public string Description_Hebrew { get; set; }
        public string Description_French { get; set; }
        public double? Price { get; set; }
        public double? ClientPrice { get; set; }
        public string Picture { get; set; }
        public string Icon { get; set; }
        [JsonIgnore]
        public bool IsSelected { get; set; }


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

        [JsonIgnore]
        private bool _IsSelectedImage;
        public bool IsSelectedImage
        {
            get { return _IsSelectedImage; }
            set
            {
                _IsSelectedImage = value;
                OnPropertyChanged("IsSelectedImage");
            }
        }
    }

    public class ReferenceImagesModel
    {
        public ImageSource ReferenceImages { get; set; }

        public byte[] ImageBytes { get; set; }

    }
}

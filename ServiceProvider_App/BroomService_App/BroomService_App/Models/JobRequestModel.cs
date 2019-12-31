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
    }


    public class GetCategoryModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<Category> data { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_Russian { get; set; }
        public string Name_Hebrew { get; set; }
        public string Name_French { get; set; }
        public string Picture { get; set; }
        public string Icon { get; set; }
        public bool HasPrice { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }

    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_Russian { get; set; }
        public string Name_Hebrew { get; set; }
        public string Name_French { get; set; }
        public double? Price { get; set; }
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

    public class SubSubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_Russian { get; set; }
        public string Name_Hebrew { get; set; }
        public string Name_French { get; set; }
        public double? Price { get; set; }
        public string Picture { get; set; }
        public string Icon { get; set; }
        [JsonIgnore]
        public bool IsSelected { get; set; }
    }

    public class ReferenceImagesModel
    {
        public ImageSource ReferenceImages { get; set; }
    }
}

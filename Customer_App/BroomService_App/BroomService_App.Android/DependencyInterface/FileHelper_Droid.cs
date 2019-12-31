using BroomService_App.DependencyInterface;
using BroomService_App.Droid.DependencyInterface;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper_Droid))]
namespace BroomService_App.Droid.DependencyInterface
{
    public class FileHelper_Droid : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

    }
}
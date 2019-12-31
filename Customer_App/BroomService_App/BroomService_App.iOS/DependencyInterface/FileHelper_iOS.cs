using BroomService_App.DependencyInterface;
using BroomService_App.iOS.DependencyInterface;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper_iOS))]
namespace BroomService_App.iOS.DependencyInterface
{
    public class FileHelper_iOS : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}

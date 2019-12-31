using Firebase.Database;
using BroomService_App.DependencyInterface;
using BroomService_App.Droid.DependencyInterface;
using Java.Lang;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetTimeStamp_Droid))]
namespace BroomService_App.Droid.DependencyInterface
{
    public class GetTimeStamp_Droid : IGetTimeStamp
    {
        public object TimeStamp()
        {
            return ServerValue.Timestamp as Object;

        }
    }
}
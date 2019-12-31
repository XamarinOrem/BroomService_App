using Firebase.Database;
using Foundation;
using BroomService_App.DependencyInterface;
using BroomService_App.iOS.DependencyInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetTimeStamp_iOS))]
namespace BroomService_App.iOS.DependencyInterface
{
    public class GetTimeStamp_iOS : IGetTimeStamp
    {
        public object TimeStamp()
        {
            var timeStamp = Convert(ServerValue.Timestamp); 
            return timeStamp as Object; 
        }

        private static IDictionary<string, string> Convert(NSDictionary nativeDict)
        {
            return nativeDict.ToDictionary<KeyValuePair<NSObject, NSObject>, string, string>(
                item => (NSString)item.Key, item => item.Value.ToString());
        }
    }
}
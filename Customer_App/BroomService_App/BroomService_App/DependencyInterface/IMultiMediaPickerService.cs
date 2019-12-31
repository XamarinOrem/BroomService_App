using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BroomService_App.Helpers.FileHelper;

namespace BroomService_App.DependencyInterface
{
    public interface IMultiMediaPickerService
    {
        event EventHandler<MediaFile> OnMediaPicked;
        event EventHandler<IList<MediaFile>> OnMediaPickedCompleted;
        Task<IList<MediaFile>> PickPhotosAsync();
        Task<IList<MediaFile>> PickVideosAsync();
        void Clean();
    }
}

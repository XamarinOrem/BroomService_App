using System;
using System.Collections.Generic;
using System.Text;

namespace BroomService_App.DependencyInterface
{
    public interface ILocale
    {
        string GetCurrent();

        void SetLocale();
        string SetLocale(string culturevalue);
    }
}

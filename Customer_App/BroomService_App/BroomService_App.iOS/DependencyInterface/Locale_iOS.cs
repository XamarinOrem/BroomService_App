using System;
using System.Threading;
using BroomService_App.DependencyInterface;
using BroomService_App.iOS.DependencyInterface;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(Locale_iOS))]
namespace BroomService_App.iOS.DependencyInterface
{
    public class Locale_iOS : ILocale
    {
        public void SetLocale()
        {
            try
            {
                var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
                var netLocale = iosLocaleAuto.Replace("_", "-");
                var ci = new System.Globalization.CultureInfo(netLocale);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
            catch
            {
            }
        }

        public string SetLocale(string culturevalue)
        {
            try
            {
                var netLocale = culturevalue.Replace("_", "-");
                var ci = new System.Globalization.CultureInfo(netLocale);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
                return netLocale;
            }
            catch (Exception)
            {
                return null;

            }
        }
        public string GetCurrent()
        {
            var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
            var iosLanguageAuto = NSLocale.AutoUpdatingCurrentLocale.LanguageCode;

            var netLocale = iosLocaleAuto.Replace("_", "-");
            var netLanguage = iosLanguageAuto.Replace("_", "-");

            try
            {
                if (Xamarin.Forms.Application.Current.Properties.ContainsKey("AppLocale") && !string.IsNullOrEmpty(Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString()))
                {
                    var languageculture = Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString();
                    netLanguage = languageculture;
                    netLocale = languageculture;
                }
                else
                {
                    netLanguage = "en-US";
                    netLocale = "en-US";
                }
            }
            catch (Exception)
            {
            }


            return netLanguage;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BroomService_App.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LargeTextLabel : ContentView
    {
        public LargeTextLabel()
        {
            InitializeComponent();
        }

        #region Expanded
        public static readonly BindableProperty ExpandedProperty = BindableProperty.Create(
                        nameof(LargeTextLabel),
            typeof(bool),
            typeof(ContentView),
            false,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (newValue != null && bindable is LargeTextLabel control)
                {
                    var actualNewValue = (bool)newValue;
                    control.SmallLabel.IsVisible = !actualNewValue;
                    control.FullLabel.IsVisible = actualNewValue;
                    control.ExpandContractButton.Text = actualNewValue ? "See Less" : "See More";
                }
            });

        public bool Expanded { get; set; }
        #endregion Expanded

        #region Text
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
                        nameof(LargeTextLabel),
            typeof(string),
            typeof(ContentView),
            default(string),
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (newValue != null && bindable is LargeTextLabel control)
                {
                    var actualNewValue = (string)newValue;
                    control.SmallLabel.Text = actualNewValue;
                    control.FullLabel.Text = actualNewValue;
                }
            });

        public string Text { get; set; }
        #endregion Text

        #region Command
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
                        nameof(LargeTextLabel),
            typeof(ICommand),
            typeof(ContentView),
            default(Command),
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (newValue != null && bindable is LargeTextLabel control)
                {
                    var actualNewValue = (ICommand)newValue;
                    control.ExpandContractButton.Command = actualNewValue;
                }
            });

        public ICommand Command { get; set; }
        #endregion Command
    }
}
using System;
using Xamarin.Forms;

namespace ASFormsControls.Client.Controls
{
    public class CheckBoxView : View
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            "Color", typeof(Color), typeof(CheckBoxView), Color.Default);

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
            "IsChecked", typeof(bool), typeof(CheckBoxView), true,
            propertyChanged: (s, o, n) => { (s as CheckBoxView).OnChecked(new EventArgs()); });

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public event EventHandler Checked;
        protected virtual void OnChecked(EventArgs e)
        {
            Checked?.Invoke(this, e);
        }
    }

    //For iOS checkbox custom renderer
    //http://www.goxuni.com/671974-creating-a-xamarin-bindings-library-for-a-custom-ios-control/
    //http://www.goxuni.com/672442-using-a-custom-native-control-with-xamarin-forms-via-a-custom-renderer/
    //https://github.com/Marxon13/M13Checkbox
}

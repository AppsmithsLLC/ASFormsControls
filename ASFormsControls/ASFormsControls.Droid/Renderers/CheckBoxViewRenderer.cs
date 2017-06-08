using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using System.ComponentModel;
using ASFormsControls.Client.Controls;
using ASFormsControls.Droid.Renderers;

[assembly: ExportRenderer(typeof(CheckBoxView), typeof(CheckBoxViewRenderer))]
namespace ASFormsControls.Droid.Renderers
{
    public class CheckBoxViewRenderer : ViewRenderer<CheckBoxView, CheckBox>
    {
        private CheckBox checkBox;

        protected override void OnElementChanged(ElementChangedEventArgs<CheckBoxView> e)
        {
            base.OnElementChanged(e);
            var model = e.NewElement;
            checkBox = new CheckBox(Context);
            checkBox.Tag = this;
            CheckboxPropertyChanged(model, null);
            checkBox.SetOnClickListener(new ClickListener(model));
            SetNativeControl(checkBox);
        }

        private void CheckboxPropertyChanged(CheckBoxView model, string propertyName)
        {
            if (propertyName == null || CheckBoxView.IsCheckedProperty.PropertyName == propertyName)
            {
                checkBox.Checked = model.IsChecked;
            }

            if (propertyName == null || CheckBoxView.ColorProperty.PropertyName == propertyName)
            {
                int[][] states = {
                    new int[] { Android.Resource.Attribute.StateEnabled}, // enabled
                    new int[] {Android.Resource.Attribute.StateEnabled}, // disabled
                    new int[] {Android.Resource.Attribute.StateChecked}, // unchecked
                    new int[] { Android.Resource.Attribute.StatePressed}  // pressed
                };
                var checkBoxColor = (int)model.Color.ToAndroid();
                int[] colors = {
                    checkBoxColor,
                    checkBoxColor,
                    checkBoxColor,
                    checkBoxColor
                };
                var myList = new Android.Content.Res.ColorStateList(states, colors);
                checkBox.ButtonTintList = myList;

            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (checkBox != null)
            {
                base.OnElementPropertyChanged(sender, e);
                CheckboxPropertyChanged((CheckBoxView)sender, e.PropertyName);
            }
        }

        public class ClickListener : Java.Lang.Object, IOnClickListener
        {
            private CheckBoxView _myCheckbox;
            public ClickListener(CheckBoxView myCheckbox)
            {
                _myCheckbox = myCheckbox;
            }
            public void OnClick(Android.Views.View v)
            {
                _myCheckbox.IsChecked = !_myCheckbox.IsChecked;
            }
        }
    }
}
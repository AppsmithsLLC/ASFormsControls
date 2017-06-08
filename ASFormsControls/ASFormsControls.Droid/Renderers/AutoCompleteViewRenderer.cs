using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using static Android.Widget.AdapterView;
using System.Collections.Generic;
using System;
using ASFormsControls.Client.Controls;
using ASFormsControls.Droid.Renderers;

[assembly: ExportRenderer(typeof(AutoCompleteView), typeof(AutoCompleteViewRenderer))]
namespace ASFormsControls.Droid.Renderers
{
    public class AutoCompleteViewRenderer : ViewRenderer<AutoCompleteView, AutoCompleteTextView>, IOnItemClickListener
    {
        private string _text;
        private ArrayAdapter _autoCompleteAdapter;

        protected override void OnElementChanged(ElementChangedEventArgs<AutoCompleteView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new AutoCompleteTextView(Context));
                Control.InputType = Android.Text.InputTypes.TextFlagAutoComplete;
                Control.ImeOptions = Android.Views.InputMethods.ImeAction.Next;
                Control.Hint = Element.PlaceholderText;
            }

            if (e.NewElement == null)
            {
                Control.SetOnClickListener(null);
                Control.OnItemClickListener = null;
                Control.TextChanged -= OnTextChanged;
            }
            else
            {
                Control.SetOnClickListener(this);
                Control.OnItemClickListener = this;
                Control.TextChanged += OnTextChanged;
            }

            Element.PropertyChanged += OnElementPropertyChanged;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element == null || Control == null)
                return;

            if (e.PropertyName == AutoCompleteView.TextProperty.PropertyName)
                Control.Text = Element.Text;

            if (e.PropertyName == AutoCompleteView.ItemsListProperty.PropertyName)
            {
                _autoCompleteAdapter = new ArrayAdapter(Control.Context, Android.Resource.Layout.SimpleDropDownItem1Line, Element.ItemsList);
                Control.Adapter = _autoCompleteAdapter;
            }
        }

        private void OnTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _text = e.Text.ToString();
            Element.Text = e.Text.ToString();

            //Move the cursor to the end of the line otherwise it moves to the front?!
            Control.SetSelection(_text.Length, _text.Length);
        }

        public void OnItemClick(AdapterView parent, Android.Views.View view, int position, long id)
        {
            int index = _autoCompleteAdapter.GetPosition(_text);
            Element.OnItemClick(index);
        }
    }
}
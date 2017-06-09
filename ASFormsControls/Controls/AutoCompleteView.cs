using ASFormsControls.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Input;
using Xamarin.Forms;

namespace ASFormsControls.Client.Controls
{
    public class AutoCompleteView : View
    {
        public event EventHandler<string> ItemClick;

        private BindingBase _itemDisplayBinding;
        public BindingBase ItemDisplayBinding
        {
            get => _itemDisplayBinding;
            set
            {
                if (_itemDisplayBinding == value)
                    return;

                OnPropertyChanging();
                _itemDisplayBinding = value;
                ItemsList = null;
                OnPropertyChanged();
            }
        }

        public string PlaceholderText { get; set; }

        #region BindableProperties

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            "Text", typeof(string), typeof(AutoCompleteView));


        public List<string> ItemsList
        {
            get => (List<string>)GetValue(ItemsListProperty);
            set => SetValue(ItemsListProperty, value);
        }

        public static readonly BindableProperty ItemsListProperty = BindableProperty.Create(
            "ItemsList", typeof(List<string>), typeof(AutoCompleteView));


        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            "ItemsSource", typeof(IList), typeof(AutoCompleteView), default(IList), propertyChanged: OnItemsSourceChanged);

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((AutoCompleteView)bindable).OnItemsSourceChanged((IList)oldValue, (IList)newValue);
        }

        private static readonly BindableProperty DisplayProperty =
            BindableProperty.Create("Display", typeof(string), typeof(Picker), default(string));

        #endregion

        #region Commands

        public ICommand ItemClickCommand
        {
            get => (ICommand)GetValue(ItemClickCommandProperty);
            set => SetValue(ItemClickCommandProperty, value);
        }

        public static readonly BindableProperty ItemClickCommandProperty = BindableProperty.Create(
            "ItemClickCommand", typeof(ICommand), typeof(AutoCompleteView));

        #endregion

        #region Event Handlers

        public void OnItemClick(string selectedString)
        {
            if (ItemClick != null)
                ItemClick(this, selectedString);
            else
                ItemClickCommand?.Execute(selectedString);
        }

        #endregion

        private void OnItemsSourceChanged(IList oldValue, IList newValue)
        {
            if (oldValue is INotifyCollectionChanged oldObservable)
                oldObservable.CollectionChanged -= CollectionChanged;

            if (newValue is INotifyCollectionChanged newObservable)
                newObservable.CollectionChanged += CollectionChanged;

            UpdateSuggestions();
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateSuggestions();
        }

        private string GetDisplayMember(object item)
        {
            if (ItemDisplayBinding == null)
                return item.ToString();

            ItemDisplayBinding.Apply(item, this, DisplayProperty);
            ItemDisplayBinding.Unapply();
            return (string)GetValue(DisplayProperty);
        }

        private void UpdateSuggestions()
        {
            if (ItemsSource == null)
                return;

            var suggestions = new List<string>();
            foreach (var item in ItemsSource)
            {
                suggestions.Add(GetDisplayMember(item));
            }
            ItemsList = suggestions;
        }
    }

}

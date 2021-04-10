namespace IFiscalV2.Common.Controls
{
    using Xamarin.Forms;

    //https://mindofai.github.io/Creating-Custom-Controls-with-Bindable-Properties-in-Xamarin.Forms/

    // https://forums.xamarin.com/discussion/145556/how-to-enable-only-numbers-keypad-without-dot-in-both-android-and-ios-device

    public class NumericEntry : Entry
    {

        #region SelectAllOnFocus
        public bool SelectAllOnFocus { get; set; }
        public static readonly BindableProperty SelectAllOnFocusProperty = BindableProperty.Create(
                                                         propertyName: "SelectAllOnFocus",
                                                         returnType: typeof(bool),
                                                         declaringType: typeof(NumericEntry),
                                                         defaultValue: false,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: SelectAllOnFocusPropertyChanged);

        private static void SelectAllOnFocusPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (NumericEntry)bindable;
            control.SelectAllOnFocus = (bool)newValue;

            //if (!(bool)newValue)
            //{
            //    control.SelectionLength = 0;
            //    control.CursorPosition = 0;
            //}

        }
        #endregion

        #region OutLineOff
        public bool OutLineOff { get; set; }
        public static readonly BindableProperty OutLineOffProperty = BindableProperty.Create(
                                                         propertyName: "OutLineOff",
                                                         returnType: typeof(bool),
                                                         declaringType: typeof(NumericEntry),
                                                         defaultValue: false,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: OutLineOffPropertyChanged);

        private static void OutLineOffPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (NumericEntry)bindable;
            control.OutLineOff = (bool)newValue;
        }
        #endregion


    }
}

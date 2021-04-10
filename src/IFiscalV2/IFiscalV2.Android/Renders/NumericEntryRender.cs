namespace IFiscalV2.Droid.Renders
{
    using Android.Content;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;
    using Android.Graphics.Drawables;
    using IFiscalV2.Common.Controls;
    using Android.Text;

    public class NumericEntryRender : EntryRenderer
    {
        public NumericEntryRender(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // https://forums.xamarin.com/discussion/145556/how-to-enable-only-numbers-keypad-without-dot-in-both-android-and-ios-device
                Control.InputType = InputTypes.ClassNumber;

                var el = e.NewElement != null ? e.NewElement : e.OldElement != null ? e.OldElement : null;

                if (el != null)
                {
                    var ctrl = el as NumericEntry;
                    var nativeEditText = (global::Android.Widget.EditText)Control;

                    if (ctrl.OutLineOff && e.OldElement == null)
                    {
                        Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);

                    }

                    if (ctrl.SelectAllOnFocus && e.NewElement != null)
                    {
                        nativeEditText.SetSelectAllOnFocus(true);
                    }
                }
            }

        } // OnElementChanged

    } // class NumericEntryRender
}
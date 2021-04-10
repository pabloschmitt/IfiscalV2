namespace IFiscalV2.Common.Controls
{
    using Xamarin.Forms;


    public class OnlyNumericDigitsBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(OnlyNumericDigitsBehavior), 0);
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public static readonly BindableProperty MaxValueProperty = BindableProperty.Create("MaxValue", typeof(int), typeof(OnlyNumericDigitsBehavior), 0);
        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        static string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
        static char[] specialCharactersArray = specialCharacters.ToCharArray();

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += bindable_TextChanged;
        }

        private void bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender != null))
                return;

            if (!(e != null))
                return;

            if (!(sender as NumericEntry).IsFocused)
                return;

            bool preserveOld = false;

            var ctrl = (sender as Entry);

            if (ctrl != null)
            {
                string val = ctrl.Text;
                if (string.IsNullOrEmpty(val))
                    return;

                //index == -1 no special characters
                int index = val.IndexOfAny(specialCharactersArray);
                if (index != -1)
                    preserveOld = true;

                double numValue;
                if (!double.TryParse(e.NewTextValue, out numValue))
                    preserveOld = true;


                if (MaxLength > 0)
                {
                    if (e.NewTextValue.Length > 0 && e.NewTextValue.Length > MaxLength)
                        preserveOld = true;
                }

                if (MaxValue > 0)
                {
                    if (numValue > MaxValue)
                        preserveOld = true;
                }


                if (preserveOld)
                    ctrl.Text = string.IsNullOrEmpty(e?.OldTextValue) ? "" : e?.OldTextValue??"";

            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= bindable_TextChanged;
            base.OnDetachingFrom(bindable);
        }

    } // NumericEntryValidationBehavior
}

using Xamarin.Forms;

namespace PModelo.Controls
{
    public class SfPickerBehavior : Behavior<Syncfusion.SfPicker.XForms.SfPicker>
    {
        protected override void OnAttachedTo(Syncfusion.SfPicker.XForms.SfPicker bindable)
        {
            base.OnAttachedTo(bindable);
        }
        protected override void OnDetachingFrom(Syncfusion.SfPicker.XForms.SfPicker bindable)
        {
            if (Device.OS == TargetPlatform.Windows)
            {
                (bindable as Syncfusion.SfPicker.XForms.SfPicker).Dispose();
            }
            base.OnDetachingFrom(bindable);
        }
    }
}

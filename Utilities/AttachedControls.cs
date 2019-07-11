using System.Windows;

namespace TFT_Overlay.Utilities
{
    public class AttachedControls
    {
        public static readonly DependencyProperty RelatedControlProperty = DependencyProperty.RegisterAttached("RelatedControl", typeof(UIElement), typeof(AttachedControls));

        #region Methods...

        #region SetRelatedControl
        public static void SetRelatedControl(DependencyObject element, UIElement value)
        {
            element.SetValue(RelatedControlProperty, value);
        }
        #endregion

        #region GetRelatedControl
        public static UIElement GetRelatedControl(DependencyObject element)
        {
            return (UIElement)element.GetValue(RelatedControlProperty);
        }
        #endregion

        #endregion
    }
}

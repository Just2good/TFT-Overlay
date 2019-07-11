using Microsoft.VisualStudio.PlatformUI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TFT_Overlay
{
    public partial class ItemsCounter : UserControl
    {
        private ICommand _upCommand;
        private ICommand _downCommand;

        #region Methods...

        private void UpPoints() => Value += Step;

        private void DownPoints() => Value -= Step;

        #endregion

        #region Commands...
        public ICommand UpCommand => _upCommand ?? (_upCommand = new DelegateCommand(() => UpPoints()));

        public ICommand DownCommand => _downCommand ?? (_downCommand = new DelegateCommand(() => DownPoints()));

        #endregion

        #region Properties...

        #region ValueProperty
        public static DependencyProperty ValueProperty =
           DependencyProperty.Register(
               "Value",
               typeof(int),
               typeof(ItemsCounter),
               new PropertyMetadata(0));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set
            {
                if (value < MinValue)
                    value = MinValue;
                if (value > MaxValue)
                    value = MaxValue;
                SetValue(ValueProperty, value);
            }
        }
        #endregion

        #region MinValueProperty
        public readonly static DependencyProperty MinValueProperty = DependencyProperty.Register(
            "MinValue",
            typeof(int),
            typeof(ItemsCounter),
            new PropertyMetadata(0));

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set
            {
                if (value > MaxValue)
                    MaxValue = value;
                SetValue(MinValueProperty, value);
            }
        }
        #endregion

        #region MaxValueProperty
        public readonly static DependencyProperty MaxValueProperty = DependencyProperty.Register(
            "MaxValue",
            typeof(int),
            typeof(ItemsCounter),
            new PropertyMetadata(int.MaxValue));

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set
            {
                if (value < MinValue)
                    value = MinValue;
                SetValue(MaxValueProperty, value);
            }
        }
        #endregion

        #region StepProperty
        public readonly static DependencyProperty StepProperty = DependencyProperty.Register(
            "Step",
            typeof(int),
            typeof(ItemsCounter),
            new PropertyMetadata(1));

        public int Step
        {
            get { return (int)GetValue(StepProperty); }
            set
            {
                SetValue(StepProperty, value);
            }
        }
        #endregion 

        #endregion
    }
}

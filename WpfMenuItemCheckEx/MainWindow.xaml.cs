using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfMenuItemCheckEx
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string menu1Value;

        public string Menu1Value
        {
            get => menu1Value;
            set
            {
                menu1Value = value;
                FirePropertyChanged(nameof(Menu1Value));
            }
        }

        public RoutedCommand Menu1SelectCommand { get; set; }

        public MainWindow()
        {
            Menu1SelectCommand = new RoutedCommand("Menu1Select", typeof(MainWindow));
            this.CommandBindings.Add(new CommandBinding(Menu1SelectCommand, Menu1Select));
            InitializeComponent();
        }

        private void Menu1Select(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is string paramString)
            {
                e.Handled = true;
                Menu1Value = paramString;
            }
        }

        private void FirePropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class Menu1CheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue && parameter is string paramString)
            {
                return (stringValue == paramString);
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}

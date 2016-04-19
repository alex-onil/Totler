using System.Windows;
using System.Windows.Input;
using DataTypes.ViewModels;

namespace WpfClient.Windows
    {
    /// <summary>
    /// Логика взаимодействия для Products_In.xaml
    /// </summary>
    public partial class ProductProperties
    {

        private bool _result;

        public static bool UpdateProductProperties(ShippedProductViewModel shippedProduct, string model)
         {

            var form = new ProductProperties
            {
                TB_Model = { Text = model ?? "Модель не определена"},
                GR_Main = { DataContext = shippedProduct }
            };

           form.ShowDialog();

           return form._result;
         }

        private ProductProperties()
        {
             _result = false;
             InitializeComponent();
         }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
            {
              e.Handled = !char.IsDigit(e.Text, 0);
            }


        private void Button_OK_Click(object sender, RoutedEventArgs e)
            {
               _result = true;
               Close();
            }

        private void Button_Click_1(object sender, RoutedEventArgs e)
            {
              Close();
            }
        }
    }

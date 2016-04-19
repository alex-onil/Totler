using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataTypes;
using System.Collections.ObjectModel;

namespace WpfClient.Windows
    {
    /// <summary>
    /// Логика взаимодействия для Products_In.xaml
    /// </summary>
    public partial class Products_In : Window
        {

        private ModelGroup _ModelGroup;
        private bool _result = false;

        public static IEnumerable<ShippedProduct> GetProducts(ModelGroup mg)
         {

           var form = new Products_In();

           form._ModelGroup = mg;

           form.GR_Main.DataContext = new ObservableCollection<ShippedProduct>();

           form.TB_Model.Text = mg.ModelGroupName;

           form.ShowDialog();

           if (!form._result) return null; 
           else 
              {
               return form.GR_Main.DataContext as ObservableCollection<ShippedProduct>;
              }
         } 

        private Products_In()
            {
             InitializeComponent();
            }

        private void Button_AddSerials_Click(object sender, RoutedEventArgs e)
            {
              var data = GR_Main.DataContext as ObservableCollection<ShippedProduct>;
              var _return = Serials.GetSerials();
              var items = (_return == null) ? null : _return.Select(m => {m.Model = _ModelGroup; return m;});
               if (data != null && items != null)
                {
                 foreach(var item in items)
                    data.Add(item);
                }

            }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
            {
              e.Handled = !Char.IsDigit(e.Text, 0);
            }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
            {
             var tb = sender as TextBox;
             if (tb != null && tb.Text !="")
              {
              
              LB_Items.Items.Filter = (_elm) => { return (_elm as ShippedProduct).SerialNumber.StartsWith(tb.Text) ||
                                                         (_elm as ShippedProduct).ProductName.StartsWith(tb.Text);  }; 

              } else LB_Items.Items.Filter = null;
            }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
            {
             if (LB_Items.SelectedItems.Count > 0)
              {
              var result = MessageBox.Show("Подтвердите удаление выделенных продуктов (" +
                   LB_Items.SelectedItems.Count + ")", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning);

              if (result == MessageBoxResult.Yes)
                  {
                  var data = (GR_Main.DataContext as ObservableCollection<ShippedProduct>);
                  do
                      {
                      data.Remove(LB_Items.SelectedItems[0] as ShippedProduct);
                      } while (LB_Items.SelectedItems.Count > 0);

                  } 

              } else MessageBox.Show("Нет выделенных позиций!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

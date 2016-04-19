using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using DataTypes.ViewModels;

namespace WpfClient.Windows
    {
    /// <summary>
    /// Логика взаимодействия для Products_In.xaml
    /// </summary>
    public partial class ProductsIn : Window
        {

        private ModelGroupViewModel _modelGroup;
        private bool _result;

        public static IList<ShippedProductViewModel> GetProducts(ModelGroupViewModel mg)
         {

            var form = new ProductsIn
                {
                    _modelGroup = mg,
                    GR_Main = {DataContext = new ObservableCollection<ShippedProductViewModel>()},
                    TB_Model = {Text = mg.ModelGroupName}
                };

            form.ShowDialog();

           if (!form._result) return null; 
           else 
              {
               return form.GR_Main.DataContext as ObservableCollection<ShippedProductViewModel>;
              }
         } 

        private ProductsIn()
            {
             InitializeComponent();
            }

        private void Button_AddSerials_Click(object sender, RoutedEventArgs e)
            {
              var data = GR_Main.DataContext as ObservableCollection<ShippedProductViewModel>;
              var _return = Serials.GetSerials();
              var items = (_return == null) ? null : _return.Select(m => {m.ModelRef = _modelGroup.ItemId; return m;});
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
              
              LB_Items.Items.Filter = (_elm) => (_elm as ShippedProductViewModel).SerialNumber.StartsWith(tb.Text) ||
                                                (_elm as ShippedProductViewModel).ProductName.StartsWith(tb.Text); 

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
                  var data = (GR_Main.DataContext as ObservableCollection<ShippedProductViewModel>);
                  do
                      {
                      data.Remove(LB_Items.SelectedItems[0] as ShippedProductViewModel);
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

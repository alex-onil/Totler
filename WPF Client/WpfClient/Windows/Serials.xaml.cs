using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using DataTypes.Models;
using DataTypes.ViewModels;
using WpfClient.Commands;
using WpfClient.Commands.Implementation;

namespace WpfClient.Windows
    {
    /// <summary>
    /// Логика взаимодействия для Serials.xaml
    /// </summary>
    /// 

    public partial class Serials : Window
        {

        private class _model
         {
          public ObservableCollection<string> Serials {get; set;}
          public string Model {get; set;}
          public string WarantyInMonth {get; set;}
          public Boolean Result = false;
         }

        public static IEnumerable<ShippedProductViewModel> GetSerials()
         {
           var form = new Serials();

           var items = new ObservableCollection<string>();

           form.GR_Main.DataContext = new _model() { Serials = items, Model="", WarantyInMonth="12"};

           form.ShowDialog();

           var data = (form.GR_Main.DataContext as _model);
            
            if (data.Result)
             {
             
             // var items = new ObservableCollection<ShippedProduct>();

             return data.Serials.Select( m => new ShippedProductViewModel() { ProductName = data.Model,
                                                              ProductWarrantyStartDate = DateTime.Now,
                                                              SerialNumber = m,
                                                              ProductWarrantyInMonth = int.Parse(data.WarantyInMonth)
                                                              });

             }
             else return null;

         }

        private Serials()
            {
             InitializeComponent();
             CommandBinding bind = new CommandBinding(ClientCommands.DeleteElement);
             bind.Executed += DeleteElement_Executed;
             bind.CanExecute += DeleteElement_CanExecute;
             this.CommandBindings.Add(bind);
             // CommandBinding bind = new CommandBinding(
            }

        void DeleteElement_CanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
               // throw new NotImplementedException();
               var lb = e.Source as ListBox;
               if (lb != null)
                 {
                   if (lb.SelectedItems.Count > 0) 
                    {
                     e.CanExecute = true;
                     return;
                    }
                 } 
                e.CanExecute = false;
            }

        void DeleteElement_Executed(object sender, ExecutedRoutedEventArgs e)
            {
            if (LB_Serials.SelectedItems.Count > 0)
                {
                var result = MessageBox.Show("Подтвердите удаление выделенных серийных номеров (" +
                   LB_Serials.SelectedItems.Count + ")", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                    {
                    var data = (GR_Main.DataContext as _model);
                    do
                        {
                        data.Serials.Remove(LB_Serials.SelectedItems[0] as string);
                        } while (LB_Serials.SelectedItems.Count > 0);

                    }
                }
            }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
            {
             var data = (GR_Main.DataContext as _model);
             data.Result = true;
             Close();
            }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
            {
             Close();
            }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
            {
              e.Handled = !Char.IsDigit(e.Text, 0);
            }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
            {
            if (e.Key == Key.Enter)
                {
                var data = (GR_Main.DataContext as _model);
                var tb = (sender as TextBox);

                if (data != null && tb != null)
                    {
                     data.Serials.Add(tb.Text);
                     tb.Clear();
                     e.Handled = true;
                    }

                }
            
            }

        //private void Button_Delete_Click(object sender, RoutedEventArgs e)
        //    {

        //    }

        //private void MI_Delete_Click(object sender, RoutedEventArgs e)
        //    {
        //     Button_Delete_Click(sender, e);
        //    }

        private void Delete_ContextMenuOpening(object sender, ContextMenuEventArgs e)
            {
              ContextMenu cm = GR_Main.Resources["LB_Menu"] as ContextMenu;
               if (cm != null)
                 {
                  //cm.Items.
                 
                 }
            }

        private void LB_Serials_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
              CommandManager.InvalidateRequerySuggested();
            }

        }
    }

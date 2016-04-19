using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataTypes.ViewModels;
using WpfClient.Commands.Abstract;

namespace WpfClient.Commands.Implementation
    {
        internal class ShippedProductsCommands : AbstractCommands
        {
        public ShippedProductsCommands(CommandContext currentContext) : base(currentContext) { }

        public async void Properties_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var currentDataGrid = e.Source as DataGrid;
            var currentTreeView = e.Parameter as TreeView;

            var selectedShippedProduct = currentDataGrid?.SelectedItem as ShippedProductViewModel;
            var selectedModelGroup = currentTreeView?.SelectedItem as ModelGroupViewModel;
            if (selectedShippedProduct == null || selectedModelGroup == null) return;

            var shippedProductBuffer = new ShippedProductViewModel();
            var mapper = DataTypes.Mapper.MapperConfig.GetMapper();
            mapper.Map(selectedShippedProduct, shippedProductBuffer);

            var updateResult = Windows.ProductProperties.UpdateProductProperties(shippedProductBuffer,
                         selectedModelGroup.ModelGroupName);

            if (!updateResult) return;
            try
                {
                await CurrentContext.ApiShippedProducts.UpdateAsync(shippedProductBuffer);
                mapper.Map(shippedProductBuffer, selectedShippedProduct);
                }
            catch (Exception)
                {
                MessageBox.Show("Во время обновления данных произошла ошибка!",
                  "Ошибка",
                  MessageBoxButton.OK,
                  MessageBoxImage.Error);
                }
            }

        public void Properties_CanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
            var grid = e.Source as DataGrid;
            if (grid != null)
                {
                e.CanExecute = (grid.SelectedItems.Count == 1);
                return;
                }
            e.CanExecute = false;
            }

        public async void ProductDelete_Executed(object sender, ExecutedRoutedEventArgs e)
            {

            var dg = e.Source as DataGrid;

            if (dg == null) return;

            var result = MessageBox.Show("Удалить выбранные продукты(" +
                               dg.SelectedItems.Count + ")? ", "Удаление",
                                     MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
                {
                try
                    {
                    var items = dg.SelectedItems.OfType<ShippedProductViewModel>().ToList();
                    await CurrentContext.ApiShippedProducts.DeleteAsync(items);// _apiClient.Products.DeleteAsync(_items);
                    ApplicationCommands.Delete.Execute(null, dg);
                    }
                catch (Exception ex)
                    {
                    MessageBox.Show("Ошибка при удалении продуктов: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    e.Handled = true;
                    }

                }
            }

        public void Products_CanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
            var dg = e.Source as DataGrid;

            if (CurrentContext.Client.IsConnected && dg != null && dg.SelectedItems.Count > 0)
                e.CanExecute = true;
            }

        }
    }

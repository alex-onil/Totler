using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DataTypes;
using ApiConnection;
using DataTypes.Extensions;
using System.Collections.Generic;
using ApiConnection.Infrastructure;
using DataTypes.Models;
using DataTypes.ViewModels;
using WpfClient.Adapters;
using WpfClient.Commands;
using WpfClient.Commands.Abstract;
using WpfClient.Commands.Implementation;
using WpfClient.Domain;

namespace WpfClient
    {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public CommandContext CurrentContext => 
                    new CommandContext(_apiClient, 
                                       _apiShippedProducts,
                                       _apiModelGroups, 
                                       _modelGroupCollection, 
                                       _shippedProductsCollection);

        private readonly ApiClient _apiClient;

        private readonly ApiShippedProductsObservableAdapter _apiShippedProducts;

        private readonly ApiModelGroupObservableAdapter _apiModelGroups;

        private readonly ObservableCollectionRange<ModelGroupViewModel> _modelGroupCollection = 
                                               new ObservableCollectionRange<ModelGroupViewModel>();

        private readonly ObservableCollectionRange<ShippedProductViewModel> _shippedProductsCollection =
                                               new ObservableCollectionRange<ShippedProductViewModel>();

        public MainWindow()
            {
             InitializeComponent();

            _apiClient = new ApiClient();
            _apiShippedProducts =
                    new ApiShippedProductsObservableAdapter(
                    new ApiCrud<ShippedProduct, List<ShippedProduct>>(_apiClient, "api/Products"));

            _apiModelGroups = new ApiModelGroupObservableAdapter(
                              new ApiModelGroups<List<ModelGroup>>(_apiClient, "api/ModelGroups"));

            ConnectionGrid.DataContext = new AuthModel() { Addr = "http://localhost:63039", Login = "root", PasswordValue = "123456" };
             _apiClient.ConnectionStatusChanged += ConnectionStatusChanged;
             BindCommandsTreeView();
             BindCommandsDataGrid();
             BindCommandSystem();

            // Load Collection to Context
            GR_TV.DataContext = _modelGroupCollection;
            GR_DG.DataContext = _shippedProductsCollection;

            }


        private async void ConnectionStatusChanged(object sender, ApiClient.ConnectionArgs e)
        {
            if (_apiClient.IsConnected)
            {
                try
                {
                    Title = "Авторизованный пользователь " + _apiClient.GetUserName;
                     var newModelGroupCollection = await _apiModelGroups.GetModelGroupsRootAsync();
                    _modelGroupCollection.ReplaceRange(newModelGroupCollection);

                }

                catch (Exception ex)
                {
                    var result = MessageBox.Show("Ошибка связи:" + ex.Message + " Повторить подключение?",
                         "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Error);
                    if (result == MessageBoxResult.Yes) ConnectionStatusChanged(sender, e);
                }
            }
            else
            {
                Title = "Не авторизованный пользователь";
                _modelGroupCollection?.Clear();
                _shippedProductsCollection?.Clear();
            }
        }

        #region Bind Command Connection
        private void BindCommandSystem()
            {
            var conectionManager = new ConnectionManagerCommands(CurrentContext);

            // Соединится
            var cm = new CommandBinding(ClientCommands.Connect);
            cm.CanExecute += conectionManager.Connect_CanExecute;
            cm.Executed += conectionManager.Connect_Executed;
            CommandBindings.Add(cm);

            // Разьединится
            cm = new CommandBinding(ClientCommands.Disconnect);
            cm.CanExecute += conectionManager.Disconnect_CanExecute;
            cm.Executed += conectionManager.Disconnect_Executed;
            CommandBindings.Add(cm);
            }

        #endregion

        #region Bind Commands DataGrid

        private void BindCommandsDataGrid()
            {
            var shippedProductsCommands = new ShippedProductsCommands(CurrentContext);

            // Удалить группу моделей
            var cm = new CommandBinding(ClientCommands.DeleteProducts);
            cm.CanExecute += shippedProductsCommands.Products_CanExecute;
            cm.Executed += shippedProductsCommands.ProductDelete_Executed;
            CommandBindings.Add(cm);

            // Свойства
            cm = new CommandBinding(ApplicationCommands.Properties);
            cm.CanExecute += shippedProductsCommands.Properties_CanExecute;
            cm.Executed += shippedProductsCommands.Properties_Executed;
            CommandBindings.Add(cm);
            }

        #endregion

        #region Bind Commands TreeView

        private void BindCommandsTreeView()
            {
            var modelGroupCommands =
                    new ModelGroupCommands(CurrentContext);

            // Создать Группу моделей
            var cm = new CommandBinding(ApplicationCommands.New);
            cm.CanExecute += modelGroupCommands.CheckMgItem_CanExecute;
            cm.Executed += modelGroupCommands.ModelGroupCreate_Executed;
            CommandBindings.Add(cm);

            // Удалить группу моделей
            cm = new CommandBinding(ApplicationCommands.Delete);
            cm.CanExecute += modelGroupCommands.CheckMgItem_CanExecute;
            cm.Executed += modelGroupCommands.ModelGroupDelete_Executed;
            CommandBindings.Add(cm);

            // Переместить в корень
            cm = new CommandBinding(ClientCommands.MoveToRoot);
            cm.CanExecute += modelGroupCommands.CheckMgItem_CanExecute;
            cm.Executed += modelGroupCommands.ModelGroupMoveToRoot_Executed;
            CommandBindings.Add(cm);

            //Создать корневую группу
            cm = new CommandBinding(ClientCommands.CreateRootElement);
            cm.CanExecute += modelGroupCommands.CheckConnected_CanExecute;
            cm.Executed += modelGroupCommands.ModelGroupCreateInRoot_Executed;
            CommandBindings.Add(cm);

            //Переименовать
            cm = new CommandBinding(ClientCommands.Rename);
            cm.CanExecute += modelGroupCommands.CheckMgItem_CanExecute;
            cm.Executed += modelGroupCommands.ModelGroupRename_Executed;
            CommandBindings.Add(cm);

            //Создать товары
            cm = new CommandBinding(ClientCommands.CreateProducts);
            cm.CanExecute += modelGroupCommands.CheckMgItem_CanExecute;
            cm.Executed += modelGroupCommands.ModelGroupCreateProducts_Executed;
            CommandBindings.Add(cm);

            //Обновить
            cm = new CommandBinding(NavigationCommands.Refresh);
            cm.CanExecute += modelGroupCommands.CheckConnected_CanExecute;
            cm.Executed += Refresh_Executed;
            CommandBindings.Add(cm);
            }

        void Refresh_Executed(object sender, ExecutedRoutedEventArgs e)
            {
             ConnectionStatusChanged(this, null);
            }

        #endregion

        private void BTN_Close_Click(object sender, RoutedEventArgs e)
            {
             Application.Current.Shutdown();
            }

        #region Tree View Drag`n Drop

        private Point _lastMouseDown;
        private object _draggedItem;
        private bool _datagridEdit; //= false;
        private object _sourceObj;


        private void DnD_MouseDown(object sender, MouseButtonEventArgs e)
            {

            if (e.ChangedButton == MouseButton.Left)
                {
                _lastMouseDown = e.GetPosition(null);
                _sourceObj = sender;
                }
            }

        private void DataGrid_Main_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
            if (e.ChangedButton == MouseButton.Left)
                {
                DataGrid_DnD_Reset(_sourceObj);
                }
            }

        private void DataGrid_Main_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
            var dg = sender as DataGrid;
            if (dg != null && dg.SelectedItems.Count > 0)
                {
                _draggedItem = dg.SelectedItems.Cast<ShippedProductViewModel>().ToList();
                }
            }

        private void DataGrid_Main_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
            {
            _datagridEdit = true;
            //in case we are in the middle of a drag/drop operation, cancel it...
            if (_draggedItem != null) DataGrid_DnD_Reset(sender);
            }

        private void DataGrid_DnD_Reset(object sender)
            {
            _draggedItem = null;

            var dg = sender as DataGrid;
            if (dg != null)
                {
                dg.IsReadOnly = false;
                }
            }

        private void DataGrid_Main_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
            {
            _datagridEdit = false;
            }

        private void DataGrid_MouseMove(object sender, MouseEventArgs e)
        {
            var dg = sender as DataGrid;
            if (_datagridEdit) DataGrid_DnD_Reset(sender);

            if (e.LeftButton == MouseButtonState.Pressed && dg != null && !_datagridEdit)
            {
                // Заблокируем Дата грид если перетаскивается
                if (!_datagridEdit)
                    dg.IsReadOnly = true;

                // Проверим - если сбилось выделение, то востановим
                var items = _draggedItem as IList<ShippedProductViewModel>;
                if (items != null && items.Count != dg.SelectedItems.Count)
                {
                    var notSelectedItems = items.Except(dg.SelectedItems.OfType<ShippedProductViewModel>());
                    foreach (var item in notSelectedItems)
                        dg.SelectedItems.Add(item);
                
                }

                // Point curentPosition = e.GetPosition(dg);
                var mousePos = e.GetPosition(null);
                var diff = _lastMouseDown - mousePos;
                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance
                      || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {

                    if (_draggedItem != null)
                    {
                        // DragDropEffects finalDropEffect =
                           DragDrop.DoDragDrop(dg,
                           _draggedItem,
                           DragDropEffects.Move);
                    }
                }
            }

        }

        private void TreeView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // var currentPosition = e.GetPosition(TV_Models);

                var mousePos = e.GetPosition(null);

                var diff = _lastMouseDown - mousePos;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance
                       || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    var treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                    _draggedItem = treeViewItem?.Header as ModelGroupViewModel;

                    if (_draggedItem != null)
                    {
                        //DragDropEffects finalDropEffect =
                                 DragDrop.DoDragDrop(TV_Models,
                                 TV_Models.SelectedValue,
                                 DragDropEffects.Move);
                    }
                }
            }
        }

        private async void TV_Models_Drop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;

            // Verify that this is a valid drop and then store the drop target
            var treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);

            var target = treeViewItem.Header as ModelGroupViewModel;

            if (target == null) return;

            #region Item is ModelGroup

            var modelGroupDraggedItem = _draggedItem as ModelGroupViewModel;

            if (modelGroupDraggedItem != null)
            {

                // Проверим на зацикливание (вложение в себя или в своих наследников)
                if (modelGroupDraggedItem.Childs.FindInChildren(mg => mg == target) != null ||
                    _draggedItem == target || modelGroupDraggedItem.ParentRef == target.ItemId) return;

                var result = MessageBox.Show("Перенести группу: " + modelGroupDraggedItem.ModelGroupName + " в группу: " +
                                                target.ModelGroupName, "Перемещение группы", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    _draggedItem = null;
                    return;
                }
                var buffer = modelGroupDraggedItem.ParentRef;
                modelGroupDraggedItem.ParentRef = target.ItemId;
                try
                {
                    var updatedGroupDraggedItem = await _apiModelGroups.UpdateAsync(modelGroupDraggedItem);

                    if (buffer.HasValue)
                    {
                        var parent = _modelGroupCollection.FindInChildren(mg => mg.ItemId == buffer);
                        parent?.Childs.Remove(modelGroupDraggedItem);

                        target.Childs.Add(updatedGroupDraggedItem);
                    }
                    else
                    {
                        _modelGroupCollection.Remove(modelGroupDraggedItem);
                        target.Childs.Add(updatedGroupDraggedItem);
                    }
                }
                catch (Exception ex)
                {
                    modelGroupDraggedItem.ParentRef = buffer;
                    MessageBox.Show("Перемещение группы завершилось ошибкой: " + ex.Message,
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }

            #endregion

            #region Item is List of ShippedProduct

            var shippedProductDraggedItem = _draggedItem as IList<ShippedProductViewModel>;


            if (shippedProductDraggedItem != null)
                {

                // Проверим на копирование в себя и если так то отменим
                if (target.ItemId == shippedProductDraggedItem.First().ModelRef)
                    {
                    e.Effects = DragDropEffects.None;
                    DataGrid_DnD_Reset(_sourceObj);
                    return;
                    }

                var result = MessageBox.Show("Перенести продукты(" + shippedProductDraggedItem.Count + " ) в группу: "
                    + target.ModelGroupName, "Перемещение продуктов", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var buffer =
                        shippedProductDraggedItem.ToDictionary(p => p.ItemId, v => v.ModelRef);

                    shippedProductDraggedItem.ToList().ForEach(p => p.ModelRef = target.ItemId);
                    try
                        {
                        var newItems = await _apiShippedProducts.UpdateAsync(shippedProductDraggedItem);

                            _shippedProductsCollection.AddRange(newItems);
                            //foreach (var shippedProduct in newItems)
                            //    _shippedProductsCollection.Add(shippedProduct);
                        }
                    catch (Exception ex)
                        {
                        shippedProductDraggedItem.ToList().ForEach(p => p.ModelRef = buffer[p.ItemId]);
                        MessageBox.Show("Перемещение продуктов завершилось ошибкой: " + ex.Message,
                                        "Ошибка",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                        }
                    }


                e.Effects = DragDropEffects.Move;
                DataGrid_DnD_Reset(_sourceObj);
                } else DataGrid_DnD_Reset(_sourceObj);

            #endregion
            }

        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
            {
            do
                {
                if (current is T)
                    {
                    return (T) current;
                    }
                current = VisualTreeHelper.GetParent(current);
                }
            while (current != null);
            return null;
            }

        private void TV_Models_DragOver(object sender, DragEventArgs e)
        {
            Point currentPosition = e.GetPosition(TV_Models);

            if ((Math.Abs(currentPosition.X - _lastMouseDown.X) > 10.0) ||
               (Math.Abs(currentPosition.Y - _lastMouseDown.Y) > 10.0))
            {
                // Verify that this is a valid drop and then store the drop target

                var treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                var target = treeViewItem.Header as ModelGroupViewModel;

                if (treeViewItem != null)
                {
                    // Выделяем элемент под курсором если он соответсвует
                    treeViewItem.IsSelected = true;

                    // Если свернут то разворачиваем
                    if (!treeViewItem.IsExpanded && treeViewItem.Items.Count > 0)
                        treeViewItem.ExpandSubtree();
                }

                if (target != null && _draggedItem != null)
                {

                    // Item is Model Group
                    var modelGroupDraggedItem = _draggedItem as ModelGroupViewModel;

                    if (modelGroupDraggedItem != null)
                    {
                        // Проверим на зацикливание (вложение в себя или в своих наследников)
                        if (modelGroupDraggedItem.Childs.FindInChildren(mg => mg == target) == null &&
                        modelGroupDraggedItem != target && modelGroupDraggedItem.ParentRef != target.ItemId)
                        {
                            e.Effects = DragDropEffects.Move;
                        }
                        else e.Effects = DragDropEffects.None;
                    }

                    // Item is List of ShippedProduct
                    var shipperProductsDraggedItem = _draggedItem as IList<ShippedProductViewModel>;
                    if (shipperProductsDraggedItem != null)
                    {

                        // Проверим на копирование в свою же группу
                        e.Effects = target.ItemId == shipperProductsDraggedItem.FirstOrDefault()?.ModelRef ?
                                        DragDropEffects.None : DragDropEffects.Move;
                    }
                }
            }
            e.Handled = true;
        }

        #endregion

        private async void TV_Models_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
            {
            // необходимо запросить данные в соответсвии с выделенной модель и связать их с датагридом
            var currentModelGroup = e.NewValue as ModelGroupViewModel;

            if (currentModelGroup == null) return;

            try
            {
                var newShippedCollection = await _apiModelGroups.ReadProductsByModelAsync(currentModelGroup);
                //if (newShippedCollection.Count == 0) _shippedProductsCollection.Clear(); 
                //    else 
                _shippedProductsCollection.ReplaceRange(newShippedCollection);
            }
            catch (Exception ex)
            {
                _shippedProductsCollection.Clear();
                MessageBox.Show("Запрос списка продуктов: " + ex.Message,
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
        }

        //private async void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //    {
        //     var item = sender as ShippedProductViewModel;
        //     if (item != null)
        //       {
        //        try
        //        {
        //            await _apiShippedProducts.UpdateAsync(item); //_apiClient.Products.UpdateAsync(item);
        //        }
        //         catch (Exception ex)
        //         {
        //         MessageBox.Show("Обновление данных закончилось с ошибкой: " + ex.Message,
        //           "Ошибка",
        //           MessageBoxButton.OK,
        //           MessageBoxImage.Error);

        //         }
        //       }
        //    }

        //private void items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //    {
        //       switch (e.Action)
        //        {
        //         case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
        //             break;
        //         case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
        //             break;
        //         case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
        //             break;
        //        }
        //    }

        private async void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
            {
            var dg = sender as DataGrid;
            if (e.Key == Key.Delete & dg != null && dg.SelectedItems.Count > 0)
                {
                var result = MessageBox.Show("Удалить выбранные продукты(" +
                                     dg.SelectedItems.Count + ")? ", "Удаление",
                                           MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                    {
                    e.Handled = true;
                    return;
                    }
                try
                    {
                    var selectedShippedProducts = dg.SelectedItems.OfType<ShippedProductViewModel>().ToList();
                    await _apiShippedProducts.DeleteAsync(selectedShippedProducts);
                        foreach (var shippedProduct in selectedShippedProducts)
                            _shippedProductsCollection.Remove(shippedProduct);
                    }
                catch (Exception ex)
                    {
                    MessageBox.Show("Ошибка при удалении продуктов: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    e.Handled = true;
                    }
                }
            }

        private void DataGrid_Main_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ApplicationCommands.Properties.Execute(TV_Models, GR_DG);
        }

        private void TB_Filter_TextChanged(object sender, TextChangedEventArgs e)
            {
             var tb = sender as TextBox;
             if (tb != null && tb.Text !="")
              {
              DataGrid_Main.Items.Filter = element => (element as ShippedProductViewModel).SerialNumber.StartsWith(TB_Filter.Text)
                                                         || (element as ShippedProductViewModel).ProductName.StartsWith(TB_Filter.Text)
                                                         || (element as ShippedProductViewModel).ProductName.Contains(TB_Filter.Text);
              }
             else DataGrid_Main.Items.Filter = null;
            }

        private void DataGrid_Main_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
            {
             TB_Filter_TextChanged(this,null);
            }

        }
    }

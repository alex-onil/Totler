using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataTypes.Extensions;
using DataTypes.ViewModels;
using WpfClient.Commands.Abstract;
using WpfClient.Utilities;

namespace WpfClient.Commands.Implementation
    {
    internal class ModelGroupCommands : AbstractCommands
        {
        public ModelGroupCommands(CommandContext currentContext) : base(currentContext) { }

        public async void ModelGroupCreateProducts_Executed(object sender, ExecutedRoutedEventArgs e)
            {
            var treeViewItem = e.OriginalSource as TreeViewItem;
            var currentModelGroup = treeViewItem?.Header as ModelGroupViewModel;

            if (currentModelGroup == null) return;

            var newShippedProducts = Windows.ProductsIn.GetProducts(currentModelGroup);
            if (newShippedProducts == null) return;

            newShippedProducts.ToList().ForEach(p => p.ModelRef = currentModelGroup.ItemId);

            try
                {
                var recivedShippedProducts = await CurrentContext.ApiShippedProducts.CreateAsync(newShippedProducts);
                    CurrentContext.ShippedproductsCollection.AddRange(recivedShippedProducts);
                }
            catch (Exception ex)
                {
                MessageBox.Show("Во время создания продуктов произошла ошибка: " + ex.Message);
                }
            }

        public async void ModelGroupRename_Executed(object sender, ExecutedRoutedEventArgs e)
            {
            var item = e.OriginalSource as TreeViewItem;
            var modelGroupToRename = item?.Header as ModelGroupViewModel;

            if (modelGroupToRename == null) return;
            var buf = modelGroupToRename.ModelGroupName;
            var result = buf.WPFEdit();

            if (!string.IsNullOrEmpty(result) && !buf.Equals(result))
                {
                try
                    {
                    modelGroupToRename.ModelGroupName = result;
                    modelGroupToRename = await CurrentContext.ApiModelGroups.UpdateAsync(modelGroupToRename);
                    }
                catch (Exception ex)
                    {
                    modelGroupToRename.ModelGroupName = buf;
                    MessageBox.Show("Во время обновления произошла ошибка: " + ex.Message);

                    }
                }
            }

        public async void ModelGroupCreateInRoot_Executed(object sender, ExecutedRoutedEventArgs e)
            {
            var result = "".WPFEdit();
            if (string.IsNullOrEmpty(result)) return;
            var newMg = new ModelGroupViewModel { ModelGroupName = result };
            try
                {
                newMg = await CurrentContext.ApiModelGroups.CreateAsync(newMg);
                CurrentContext.ModelGroupCollection?.Add(newMg);
                }
            catch (Exception ex)
                {
                MessageBox.Show("Во время создания группы произошла ошибка: " + ex.Message);
                }
            }

        public void CheckConnected_CanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
            e.CanExecute = CurrentContext.Client.IsConnected;
            }

        public async void ModelGroupMoveToRoot_Executed(object sender, ExecutedRoutedEventArgs e)
            {
            var mg = e.OriginalSource as TreeViewItem;
            var item = mg?.Header as ModelGroupViewModel;

            if (item?.ParentRef == null) return;

            var result = MessageBox.Show("Перенести группу: " + item.ModelGroupName + " в корень?",
                                  "Перемещение группы", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;

            var parentCode = item.ParentRef.Value;
            try
                {
                item.ParentRef = null;

                var newItem = await CurrentContext.ApiModelGroups.UpdateAsync(item);

                var parentModelGroup = CurrentContext.ModelGroupCollection?.FindInChildren(m => m.ItemId == parentCode);

                parentModelGroup?.Childs.Remove(item);

                CurrentContext.ModelGroupCollection?.Add(newItem);

                }
            catch (Exception ex)
                {
                item.ParentRef = parentCode;
                MessageBox.Show("Перемещение группы завершилось ошибкой: " + ex.Message,
                       "Ошибка",
                       MessageBoxButton.OK,
                       MessageBoxImage.Error);
                }
            }

        public async void ModelGroupDelete_Executed(object sender, ExecutedRoutedEventArgs e)
            {

            var item = e.OriginalSource as TreeViewItem;
            var selectedModelGroup = item?.Header as ModelGroupViewModel;
            if (selectedModelGroup == null) return;

            var result = MessageBox.Show("Удалить группу моделей: " + selectedModelGroup.ModelGroupName, "Удаление",
                                          MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) return;

            try
                {
                await CurrentContext.ApiModelGroups.DeleteAsync(selectedModelGroup);

                if (selectedModelGroup.ParentRef.HasValue)
                    {
                    var parent = CurrentContext.ModelGroupCollection?.FindInChildren(m => m.ItemId.Equals(selectedModelGroup.ParentRef));
                    parent?.Childs.Remove(selectedModelGroup);
                    } else
                    {
                    CurrentContext.ModelGroupCollection?.Remove(selectedModelGroup);
                    }

                }
            catch (Exception ex)
                {
                MessageBox.Show("Удаление группы завершилось ошибкой: " + ex.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                }
            }

        public async void ModelGroupCreate_Executed(object sender, ExecutedRoutedEventArgs e)
            {
            var item = e.OriginalSource as TreeViewItem;
            var mg = item?.Header as ModelGroupViewModel;

            var result = "".WPFEdit();
            if (string.IsNullOrEmpty(result) || mg == null) return;

            var newMg = new ModelGroupViewModel { ModelGroupName = result, ParentRef = mg.ItemId };
            try
                {
                var createdModelGroup = await CurrentContext.ApiModelGroups.CreateAsync(newMg);
                mg.Childs.Add(createdModelGroup);

                }
            catch (Exception ex)
                {
                MessageBox.Show("Во время создания группы произошла ошибка: " + ex.Message);
                }
            }

        public void CheckMgItem_CanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
            if (e.OriginalSource is TreeViewItem && CurrentContext.Client.IsConnected) e.CanExecute = true;
            else e.CanExecute = false;
            }
        }
    }

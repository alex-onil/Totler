using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ApiConnection;
using DataTypes;
using WpfClient.Commands.Abstract;

namespace WpfClient.Commands.Implementation
    {
    class ConnectionManagerCommands : AbstractCommands
        {
        private bool _connecting = false;

        public ConnectionManagerCommands(CommandContext currentContext) : base(currentContext) { }

        public void Disconnect_Executed(object sender, ExecutedRoutedEventArgs e)
            {
            var gb = e.Parameter as GroupBox;

            if (gb == null) return;

            CurrentContext.Client.Disconnect();

            gb.IsEnabled = !_connecting || !CurrentContext.Client.IsConnected;

            CommandManager.InvalidateRequerySuggested();
            }

        public void Disconnect_CanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
            e.CanExecute = CurrentContext.Client.IsConnected;
            }

        public async void Connect_Executed(object sender, ExecutedRoutedEventArgs e)
            {
            var gb = e.Parameter as GroupBox;

            var auth = (AuthModel) (gb?.FindName("ConnectionGrid") as Grid)?.DataContext;

            if (auth == null) return;

            gb.IsEnabled = false;

            auth.PasswordValue = (gb.FindName("CurrentPassword") as PasswordBox)?.Password;

            if (!await CurrentContext.Client.ConnectAsync(auth))
                {
                MessageBox.Show("Ошибка подключения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            gb.IsEnabled = !CurrentContext.Client.IsConnected;

            CommandManager.InvalidateRequerySuggested();
            }

        public void Connect_CanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
            e.CanExecute = !CurrentContext.Client.IsConnected && !_connecting;
            }
        }
    }

using System.Windows.Input;

namespace WpfClient.Commands
    {
    public static class ClientCommands
        {

         #region Декларативная часть комманд
        
        /// <summary>
        /// Комманда Удалить выделенные элементы
        /// </summary>
        public static RoutedUICommand DeleteElement
            { get; private set; }

        /// <summary>
        /// Комманда переименовать элемент
        /// </summary>
        public static RoutedUICommand Rename
            { get; private set; }
        
        /// <summary>
        /// Комманда переместить в корень
        /// </summary>
        public static RoutedUICommand MoveToRoot
        { get; private set; }

        /// <summary>
        /// Комманда создать корневой элемент
        /// </summary>
        public static RoutedUICommand CreateRootElement
        { get; private set; }

        public static RoutedUICommand CreateProducts
        { get; private set; }

        public static RoutedUICommand DeleteProducts
        { get; private set; }

        public static RoutedUICommand Connect
        { get; private set; }

        public static RoutedUICommand Disconnect
        { get; private set; }

         #endregion


         #region Часть инициализации

             // Инициализация комманд
             static ClientCommands()
              {
               DeleteElement = new RoutedUICommand("Удалить выделенные элементы","ElementsDelete", typeof(ClientCommands));

                 var inputs = new InputGestureCollection { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+K") };
                 Rename = new RoutedUICommand("Переименовать","Rename", typeof(ClientCommands), 
                    inputs);

               MoveToRoot = new RoutedUICommand("Переместить элемент в корень", "MoveToRoot", typeof(ClientCommands));

               CreateRootElement = new RoutedUICommand("Создать корневой элемент", "MoveToRoot", typeof(ClientCommands));

               CreateProducts = new RoutedUICommand("Добавить товары", "CreateProducts", typeof(ClientCommands));

               DeleteProducts = new RoutedUICommand("Удалить выбранные продукты", "DeleteProducts", typeof(ClientCommands));

               Connect = new RoutedUICommand("Соединить", "Connect", typeof(ClientCommands));

               Disconnect = new RoutedUICommand("Разьединить", "Disconect", typeof(ClientCommands));
              }

          #endregion


        }
    }

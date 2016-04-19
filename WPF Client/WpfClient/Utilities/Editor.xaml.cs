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

namespace WpfClient.Utilities
    {


    /// <summary>
    /// Реадактирование строк
    /// </summary>
    public partial class Editor : Window
        {
        /// <summary>
        /// Осуществляет редактирование строки
        /// </summary>
        /// <param name="Text">Строка для редактирование</param>
        /// <param name="EditText">Текст для редактирования</param>
        /// <returns>Измененная строка</returns>
        public static bool Edit(out string Text, string EditText = "")
          {
            var form = new Editor();
            form.TB_Text.Text = EditText;

            form.ShowDialog();
            Text = form.TB_Text.Text;
            if (form.TB_Text.Equals(EditText) || form.Cancel) return false;
                else return true;
          }
        
        private bool Cancel = false;

        private Editor()
            {
             InitializeComponent();
            }

        private void Button_Click(object sender, RoutedEventArgs e)
            {
              Cancel = true;
              Close();     
            }

        private void Button_Click_1(object sender, RoutedEventArgs e)
            {
             Close();
            }
        }
    }

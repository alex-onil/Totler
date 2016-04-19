using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.Utilities
    {
    public static class Edit
        {
        /// <summary>
        /// Редактирование строки
        /// </summary>
        /// <param name="Text">Строка для редактирования</param>
        /// <returns>Результат редактирования</returns>
         public static string WPFEdit(this string Text)
          {
           string buf;
           string _txt = Text ?? "";
           var result = Editor.Edit(out buf, _txt);
           if (!result)  return null;
            else 
            {
             return buf;
            }
          }
        }
    }

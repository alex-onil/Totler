using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace DataTypes.ViewModels.Abstract
    {
    /// <summary>
    /// Класс описывающий создателя и последнего изменившего
    /// </summary>
    [DataContract]
    public abstract class StoreIdViewModel : StoreId, INotifyPropertyChanged
        {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void ChangeProperty([CallerMemberName] string property = "")
            {
             LastModificationDate = DateTime.Now;
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            }

        }
    }

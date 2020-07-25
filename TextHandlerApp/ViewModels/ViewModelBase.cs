using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TextHandlerApp.ViewModels
{
    /// <summary>
    /// Базовая модель представление
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод вызывающий событие PropertyChanged для оповещения UI об изменениях
        /// </summary>
        /// <param name="propertyName">наименование свойства</param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

using System.ComponentModel;

namespace Surreily.TheWorstSoundboard.Views {
    public class ViewModelBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

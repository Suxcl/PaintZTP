using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PaintZtp
{
    public class INPCAdapter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

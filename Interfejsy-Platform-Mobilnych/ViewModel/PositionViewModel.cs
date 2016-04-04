using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.Modules;
using System.Collections.ObjectModel;

namespace Interfejsy_Platform_Mobilnych.ViewModel
{
    class PositionViewModel
    {
        internal ObservableCollection<Position> positions = new ObservableCollection<Position>();
        public ObservableCollection<Position> Positions { get { return positions; } }

        internal async void InitPositions(string code)
        {
            positions.Clear();
            (await Downloader.getPositionsFromCode(code)).ForEach(x => positions.Add(x));
        }
    }
}

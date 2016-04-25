using System.Collections.ObjectModel;
using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.Modules;

namespace Interfejsy_Platform_Mobilnych.ViewModel
{
    internal class PositionViewModel
    {
        public ObservableCollection<Position> Positions { get; } = new ObservableCollection<Position>();

        internal async void InitPositions(string code)
        {
            Positions.Clear();
            (await Downloader.GetPositionsFromCode(code)).ForEach(x => Positions.Add(x));
        }
    }
}

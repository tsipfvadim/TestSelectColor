using System;
using System.Collections.Generic;

namespace ViewModels
{
    public interface IGameViewModel
    {
        IReadOnlyList<IItemViewModel> Items { get; }
        event Action<bool> SelectionCompleted;
        void StartRound();
    }
}

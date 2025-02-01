using System;

namespace ViewModels
{
    public interface IUIViewModel
    {
        ReactiveProperty<string> ColorLabel { get; }
        event Action<bool> SelectionCompleted;
    }
}

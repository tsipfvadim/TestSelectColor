using System;
using UnityEngine;

namespace ViewModels
{
    public interface IItemViewModel
    {
        ReactiveProperty<Color> Color { get; }
        event Action<bool> SelectionCompleted;
        void OnSelect();
    }
}

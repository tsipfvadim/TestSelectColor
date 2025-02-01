using System;
using Models;
using UnityEngine;

namespace ViewModels
{
    public class ItemViewModel : IItemViewModel
    {
        public ReactiveProperty<Color> Color { get; } = new();
        public Item Item { get; private set; }

        public event Action<bool> SelectionCompleted;
        public event Action<ItemViewModel> ItemSelected;

        public ItemViewModel()
        {
            Color.Value = UnityEngine.Color.white;
        }

        public void SetItem(Item item)
        {
            Item = item;
            Color.Value = item.Color;
        }
        
        public void OnSelect()
        {
            ItemSelected?.Invoke(this);
        }

        public void CompleteSelection(bool isCorrect)
        {
            SelectionCompleted?.Invoke(isCorrect);
        }
    }
}

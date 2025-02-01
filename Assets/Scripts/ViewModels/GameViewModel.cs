using System;
using System.Collections.Generic;
using Models;
using UnityEngine.Assertions;

namespace ViewModels
{
    public class GameViewModel : IGameViewModel, IDisposable
    {
        private readonly List<ItemViewModel> items = new();
        private readonly Game model;
        private readonly RoundGenerator roundGenerator;
        private ItemViewModel selectedItemViewModel;
        
        public IReadOnlyList<IItemViewModel> Items => items;
        public event Action<bool> SelectionCompleted;
        
        public GameViewModel(Game game, RoundGenerator generator)
        {
            model = game;
            model.SelectionCompleted += ModelOnSelectionCompleted;
            roundGenerator = generator;
            

            for (var i = 0; i < game.ItemsCount; i++)
            {
                var item = new ItemViewModel();
                item.ItemSelected += OnItemSelected;
                items.Add(item);
            }
        }

        public void Dispose()
        {
            model.SelectionCompleted -= ModelOnSelectionCompleted;
            
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                item.ItemSelected -= OnItemSelected;
            }

            items.Clear();
        }

        public void StartRound()
        {
            var round = roundGenerator.Generate(model.ItemsCount);
            Assert.IsTrue(round.Items.Count == items.Count, "Items count not equals round Items count");
            model.StartRound(round);

            for (var i = 0; i < round.Items.Count; i++)
            {
                var item = round.Items[i];
                items[i].SetItem(item);
            }
        }

        private void OnItemSelected(ItemViewModel itemViewModel)
        {
            selectedItemViewModel = itemViewModel;
            model.SelectItem(itemViewModel.Item);
        }

        private void ModelOnSelectionCompleted(bool isCorrect)
        {
            selectedItemViewModel?.CompleteSelection(isCorrect);
            SelectionCompleted?.Invoke(isCorrect);
        }
    }
}
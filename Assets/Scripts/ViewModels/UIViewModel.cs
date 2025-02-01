using System;
using Models;

namespace ViewModels
{
    public class UIViewModel : IUIViewModel, IDisposable
    {
        private readonly Game model;
        public ReactiveProperty<string> ColorLabel { get; } = new();
        public event Action<bool> SelectionCompleted;

        public UIViewModel(Game game)
        {
            model = game;
            model.SelectionCompleted += ModelOnSelectionCompleted;
            model.Round.ValueChanged += RoundOnValueChanged;
        }

        public void Dispose()
        {
            model.SelectionCompleted -= ModelOnSelectionCompleted;
            model.Round.ValueChanged -= RoundOnValueChanged;
        }

        private void ModelOnSelectionCompleted(bool isCorrect)
        {
            SelectionCompleted?.Invoke(isCorrect);
        }

        private void RoundOnValueChanged(Round round)
        {
            ColorLabel.Value = round.CorrectItem.ColorName;
        }
    }
}

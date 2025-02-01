using System;
using UnityEngine.Assertions;
using ViewModels;

namespace Models
{
    public class Game
    {
        public readonly int ItemsCount;
        public ReactiveProperty<Round> Round { get; } = new();
        public event Action<bool> SelectionCompleted;

        public Game(int count)
        {
            ItemsCount = count;
        }

        public void StartRound(Round round)
        {
            Round.Value = round;
        }

        public void SelectItem(Item item)
        {
            Assert.IsNotNull(Round, "Round not started!");
            var result = item.Equals(Round.Value.CorrectItem);
            SelectionCompleted?.Invoke(result);
        }
    }
}

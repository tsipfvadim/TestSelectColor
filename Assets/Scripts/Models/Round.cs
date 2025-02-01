using System.Collections.Generic;

namespace Models
{
    public class Round
    {
        public readonly Item CorrectItem;
        public readonly IReadOnlyList<Item> Items;

        public Round(Item correctItem, IReadOnlyList<Item> items)
        {
            CorrectItem = correctItem;
            Items = items;
        }
    }
}

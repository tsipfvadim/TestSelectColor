using UnityEngine;

namespace Models
{
    public class Item
    {
        public readonly string ColorName;
        public readonly Color Color;

        public Item(string colorName, Color color)
        {
            ColorName = colorName;
            Color = color;
        }

        public override bool Equals(object obj)
        {
            if (obj is Item item)
                return Equals(item);
            
            return false;
        }
        
        public bool Equals(Item item)
        {
            return item != null && item.Color.Equals(Color);
        }

        public override int GetHashCode()
        {
            return Color.GetHashCode();
        }
    }
}

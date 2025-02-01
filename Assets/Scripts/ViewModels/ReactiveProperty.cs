using System;
using System.Collections.Generic;

namespace ViewModels
{
    public class ReactiveProperty<T>
    {
        private T viewValue;

        public T Value
        {
            get => viewValue;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(viewValue, value))
                {
                    viewValue = value;
                    ValueChanged?.Invoke(viewValue);
                }
            }
        }

        public event Action<T> ValueChanged;
        
        public static implicit operator T(ReactiveProperty<T> reactiveProperty)
        {
            return reactiveProperty.Value;
        }
    }
}

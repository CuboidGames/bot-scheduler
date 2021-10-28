using System;
using UnityEngine;

namespace BotScheduler.Systems.GridSystem
{
    public class Cell<T>
    {
        public readonly int x;
        public readonly int y;

        private readonly EventHandler<OnGridValueChangedEventArgs<T>> changeEventHandler;

        public T value { get; private set; }
        public Cell(int x, int y, EventHandler<OnGridValueChangedEventArgs<T>> changeEventHandler)
        {
            this.x = x;
            this.y = y;
            this.changeEventHandler = changeEventHandler;
        }

        public void SetValue(T newValue)
        {
            var oldValue = value;

            value = newValue;

            if (oldValue == null || !oldValue.Equals(newValue))
            {
                NotifyChange(x, y, oldValue, newValue);
            }
        }

        private void NotifyChange(int x, int y, T oldValue, T newVaue)
        {
            var eventData = new OnGridValueChangedEventArgs<T>
            {
                x = x,
                y = y,
                cell = this,
                oldValue = oldValue,
                newValue = newVaue,
            };

            changeEventHandler.Invoke(this, eventData);
        }
    }
}
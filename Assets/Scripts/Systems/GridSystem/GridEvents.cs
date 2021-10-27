namespace GridSystem
{
    public class OnGridValueChangedEventArgs<U>
    {
        public int x;
        public int y;

        public Cell<U> cell;

        public U oldValue;
        public U newValue;
    }
}
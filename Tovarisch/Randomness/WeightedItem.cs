namespace Wacton.Tovarisch.Randomness
{
    public struct WeightedItem<T>
    {
        public readonly T Item;
        public readonly double Weight;

        public WeightedItem(T item, double weight)
        {
            this.Item = item;
            this.Weight = weight;
        }

        public override string ToString() => $"{this.Item}: {this.Weight}";
    }
}

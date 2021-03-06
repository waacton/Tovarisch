﻿namespace Wacton.Tovarisch.Color
{
    using System.Windows.Media;

    public struct WeightedColor
    {
        public readonly Color Color;
        public readonly double Weight;

        public WeightedChannelValue WeightedR => new WeightedChannelValue(this.Color.R, this.Weight);
        public WeightedChannelValue WeightedG => new WeightedChannelValue(this.Color.G, this.Weight);
        public WeightedChannelValue WeightedB => new WeightedChannelValue(this.Color.B, this.Weight);

        public WeightedColor(Color color, double weight)
        {
            this.Color = color;
            this.Weight = weight;
        }

        public override string ToString() => $"{this.Color}: {this.Weight}";
    }

    public struct WeightedChannelValue
    {
        public readonly byte ChannelValue;
        public readonly double Weight;

        public WeightedChannelValue(byte channelValue, double weight)
        {
            this.ChannelValue = channelValue;
            this.Weight = weight;
        }

        public override string ToString() => $"{this.ChannelValue}: {this.Weight}";
    }
}

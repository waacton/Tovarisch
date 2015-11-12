﻿namespace Wacton.Tovarisch.Color
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Media;

    public static class ColorLogic
    {
        public static Color ModifyColor(Color baseColor, List<WeightedColor> modifyingWeightedColors)
        {
            // remove any modifiers that have no effect
            modifyingWeightedColors = modifyingWeightedColors.Where(weightedColor => weightedColor.Weight > 0.0).ToList();

            // if there are no colours to apply, return the base color
            var color = baseColor;
            if (modifyingWeightedColors.Count == 0)
            {
                return color;
            }

            // if there is only one colour to apply, do so and return
            if (modifyingWeightedColors.Count == 1)
            {
                var modifier = modifyingWeightedColors.Single();
                color = InterpolateColor(color, modifier.Color, modifier.Weight);
                return color;
            }

            // if there are two or more colours to apply 
            // calculate the colour of the modifiers and apply it using the max weighting of all modifiers
            var modifierOpacity = modifyingWeightedColors.Max(weightedColor => weightedColor.Weight);
            var modifierColor = InterpolateWeightedColors(modifyingWeightedColors);
            color = InterpolateColor(color, modifierColor, modifierOpacity);
            return color;
        }

        private static Color InterpolateColor(Color baseColor, Color targetColor, double targetFactor)
        {
            if (targetFactor < 0 || targetFactor > 1)
            {
                throw new ArgumentOutOfRangeException("targetFactor");
            }

            var alphaDifference = targetColor.A - baseColor.A;
            var redDifference = targetColor.R - baseColor.R;
            var greenDifference = targetColor.G - baseColor.G;
            var blueDifference = targetColor.B - baseColor.B;

            return new Color
            {
                A = (byte)(baseColor.A + (alphaDifference * targetFactor)),
                R = (byte)(baseColor.R + (redDifference * targetFactor)),
                G = (byte)(baseColor.G + (greenDifference * targetFactor)),
                B = (byte)(baseColor.B + (blueDifference * targetFactor))
            };
        }

        private static Color InterpolateWeightedColors(IEnumerable<WeightedColor> weightedColors)
        {
            var weightedRs = new List<WeightedChannelValue>();
            var weightedGs = new List<WeightedChannelValue>();
            var weightedBs = new List<WeightedChannelValue>();

            foreach (var weightedColor in weightedColors)
            {
                weightedRs.Add(weightedColor.WeightedR);
                weightedGs.Add(weightedColor.WeightedG);
                weightedBs.Add(weightedColor.WeightedB);
            }

            var interpolatedR = InterpolateWeightedChannelValues(weightedRs);
            var interpolatedG = InterpolateWeightedChannelValues(weightedGs);
            var interpolatedB = InterpolateWeightedChannelValues(weightedBs);

            return Color.FromRgb(interpolatedR, interpolatedG, interpolatedB);
        }

        private static byte InterpolateWeightedChannelValues(List<WeightedChannelValue> weightedChannelValues)
        {
            if (weightedChannelValues == null || weightedChannelValues.Count <= 1)
            {
                throw new ArgumentException("Cannot interpolate between one or fewer colour channels", "weightedChannelValues");
            }

            // order color channel values so greatest channel value is first
            weightedChannelValues = weightedChannelValues.OrderByDescending(weightedChannelValue => weightedChannelValue.ChannelValue).ToList();

            // base case: 2 color channel values
            // interpolate between the two values and return the result
            if (weightedChannelValues.Count == 2)
            {
                var currentWeightedChannelValue = weightedChannelValues[0];
                var nextWeightedChannelValue = weightedChannelValues[1];
                var interpolatedWeightedChannelValue = InterpolateWeightedChannelValues(currentWeightedChannelValue, nextWeightedChannelValue);
                return interpolatedWeightedChannelValue.ChannelValue;
            }

            // if > 2 color channel values
            // interpolate each channel value with the next one, as well as the resultant weight of the interpolation
            // then interpolate between these interpolations by recursively calling this function
            /* 
             * e.g. if 3 channel values A, B, C...
             * pass 1: interpolate A-B, B-C (now only have 2 weighted channel values)
             * pass 2: interpolate AB-BC    (2 weighted channel values is the base case, return the result of interpolation)
             */
            var interpolatedWeightedChannelValues = new List<WeightedChannelValue>();
            for (var i = 0; i < weightedChannelValues.Count - 1; i++)
            {
                var currentWeightedChannelValue = weightedChannelValues[i];
                var nextWeightedChannelValue = weightedChannelValues[i + 1];
                var interpolatedWeightedChannelValue = InterpolateWeightedChannelValues(currentWeightedChannelValue, nextWeightedChannelValue);
                interpolatedWeightedChannelValues.Add(interpolatedWeightedChannelValue);
            }

            return InterpolateWeightedChannelValues(interpolatedWeightedChannelValues);
        }

        private static WeightedChannelValue InterpolateWeightedChannelValues(WeightedChannelValue baseChannelValue, WeightedChannelValue targetChannelValue)
        {
            // how far towards the second channel value we should go according to the weights of the two channels
            var targetFactor = targetChannelValue.Weight / (baseChannelValue.Weight + targetChannelValue.Weight);

            // calculate the interpolated channel value based on the target factor
            var channelValueDifference = targetChannelValue.ChannelValue - baseChannelValue.ChannelValue;
            var interpolatedChannelValue = (byte)(baseChannelValue.ChannelValue + (channelValueDifference * targetFactor));

            // calculate the weight of the resultant interpolation based on the target factor
            var weightDifference = baseChannelValue.Weight - targetChannelValue.Weight;
            var interpolatedWeight = baseChannelValue.Weight - (weightDifference * targetFactor);

            var weightedColorChannel = new WeightedChannelValue(interpolatedChannelValue, interpolatedWeight);
            return weightedColorChannel;
        }
    }
}
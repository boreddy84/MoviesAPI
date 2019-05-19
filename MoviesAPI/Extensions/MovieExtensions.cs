using System;

namespace MoviesAPI.Extensions
{
    public static class MovieExtensions
    {
        public static decimal RoundToNearestHalf(this decimal? value)
        {          
            return Math.Round(value.HasValue ? value.Value * 2 : 0, MidpointRounding.AwayFromZero) / 2;
        }

    }
}

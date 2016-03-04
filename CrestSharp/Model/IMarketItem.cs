using System;

namespace CrestSharp.Model
{
    public interface IMarketItem : ICrestObject
    {
        long Id { get; }

        DateTime Issued { get; }

        double Price { get; }

        long VolumeEntered { get; }

        long MinVolume { get; }

        long Volume { get; }

        string Range { get; }

        ICrestLocation Location { get; }

        int Duration { get; }

        ICrestType Type { get; }

        bool IsSell { get; }

        bool IsBuy { get; }
    }
}
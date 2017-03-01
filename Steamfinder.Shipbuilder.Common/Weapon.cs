using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp.DiceNotation;

namespace Steamfinder.Shipbuilder.Common
{
    public abstract class Component
    {
        public string Name { get; set; }
        public double Weight { get; set; }
    }

    public class Accessory : Component
    {
        
    }

    public class Weapon : Accessory
    {
        
    }

    public class Defense : Accessory
    {
        
    }

    public class Container : Accessory
    {
        
    }

    public class Engine : Component
    {
        
    }

    public class Burner : Component
    {
        
    }

    public class Envelope : Component
    {
        
    }

    public abstract class Interlink<TSource, TConsumer, TLinkStatus> : Component where TSource : ILinkSource where TConsumer : ILinkConsumer where TLinkStatus : LinkStatus
    {
        public abstract void AddConsumer(TConsumer consumer);
        public abstract void AddSource(TSource source);
        public abstract TLinkStatus GetLinkStatus();
    }

    public interface ILinkSource
    {
        
    }

    public interface ILinkConsumer
    {
        
    }

    public abstract class LinkStatus
    {
        
    }
}

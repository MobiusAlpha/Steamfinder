namespace Steamfinder.Constructbuilder.Common
{
    public interface IPoweredComponent : IComponent
    {
        double WattageRequirement { get; }
        ActivationType Activation { get; }
    }
}
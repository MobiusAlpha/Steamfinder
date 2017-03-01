using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steamfinder.Constructbuilder.Common
{
    public class Construct
    {
        public string Name { get; set; }
        public Frame Frame { get; set; }
        public PowerSource PowerSource { get; set; }
        public IList<IPoweredComponent> Components { get; set; }
        public IEnumerable<ExteriorComponent> ExteriorComponents { get { return Components.OfType<ExteriorComponent>(); } }
        public IEnumerable<InteriorComponent> InteriorComponents { get { return Components.OfType<InteriorComponent>(); } }
        public IEnumerable<AccessoryComponent> AccessoryComponents { get { return Components.OfType<AccessoryComponent>(); } }
        public IEnumerable<HardpointComponent> HardpointComponents { get { return Components.OfType<HardpointComponent>(); } }
        public IDictionary<HardpointType, int> HardpointsExposed { get { return Components.OfType<HardpointProviderComponent>().SelectMany(x => x.Provided).GroupBy(y => y).ToDictionary(p => p.Key, q => q.Count()); } }
        public IDictionary<HardpointType, int> HardpointsRequired { get { return HardpointComponents.SelectMany(x => x.Requirements).GroupBy(y => y).ToDictionary(p => p.Key, q => q.Count()); } }
    }
}

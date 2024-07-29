using System.Collections.Generic;

namespace YellowDuckSoftware.TDI
{
    public class TinyDiConstructorInfo
    {
        public List<TinyDiParameterInfo> ParameterList { get; private set; }

        public TinyDiConstructorInfo()
        {
            ParameterList = new List<TinyDiParameterInfo>();
        }
    }
}

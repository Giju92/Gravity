using UnityEngine;

namespace Test
{
    public class StarRender : HeavenlyBodyRender
    {
        public StarRender(bool showOrbit = false, Color orbitColor = default ) : base(Color.yellow, orbitColor, showOrbit)
        {
            HasRays = true;
        }
    }
}
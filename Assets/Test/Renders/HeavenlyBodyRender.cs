using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class HeavenlyBodyRender : Render
    {
        protected bool HasRays = false;
        private float m_forceDimension;
        private float dimension => m_forceDimension > 0 ? m_forceDimension : Entity.Dimension;
       
        protected override void DrawSelectionRender()
        {
            DrawRectangle(Entity.Position, Vector3.right, dimension * 2.5f, dimension*2.5f);
        }

        protected override void DrawMesh()
        {
            DrawCircle(Entity.Position, dimension, HasRays);
        }

        public HeavenlyBodyRender(Color color, Color orbitColor = default, bool showOrbit = true, float forceDimension = -1) : base(color, orbitColor, showOrbit)
        {
            this.m_forceDimension = forceDimension;
        }
    }
}

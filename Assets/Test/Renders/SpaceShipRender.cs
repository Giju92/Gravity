using UnityEngine;

namespace Test
{
    public class SpaceShipRender : Render
    {
        private readonly float m_dimension = 0.5f;
        private Vector2 _velocityForRendering = Vector2.right;
        
        public SpaceShipRender(Color orbitColor = default) : base(Color.blue, orbitColor == default? Color.green : orbitColor, true)
        {
        }

        protected override void DrawSelectionRender()
        {
            _velocityForRendering = Entity.IsMoving ? Entity.Velocity : _velocityForRendering;
            
            DrawRectangle(Entity.Position, _velocityForRendering, m_dimension*2, m_dimension);
        }

        protected override void DrawMesh()
        {
            _velocityForRendering = Entity.IsMoving ? Entity.Velocity : _velocityForRendering;
            
            DrawTriangle(Entity.Position, _velocityForRendering, m_dimension);
        }
    }
}

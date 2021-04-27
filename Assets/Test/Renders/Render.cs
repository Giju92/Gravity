using UnityEngine;

namespace Test
{
    /// <summary>
    /// Render keeps basic info to draw a generic entity
    /// In the update are drawn all the essential components
    /// </summary>
    public abstract class Render
    {
        // Reference to the Entity which belong
        protected Entity Entity;

        // Render Colors
        private readonly Color m_color;
        private readonly Color m_selectedColor;
        private readonly Color m_orbitColor;

        private readonly bool m_showOrbit;

        protected Render(Color color, Color orbitColor = default, bool showOrbit = true)
        {
            m_color = color;
            m_orbitColor = orbitColor == default ? color : orbitColor;
            m_showOrbit = showOrbit;
            m_selectedColor = Constants.SelectionColor;
        }

        #region Public Methods

        public void UpdateRender()
        {
            DrawMesh();

            DrawOrbit();

            DrawSelection();

            DrawThrust();

            DrawExplosion();
        }

        #endregion

        #region Private Methods

        private void DrawOrbit()
        {
            if (!m_showOrbit || !Entity.IsMoving || Entity.IsPaused || !Constants.ShowOrbits)
                return;

            Debug.DrawLine(Entity.PrevPosition, Entity.Position, m_orbitColor, Constants.OrbitTrailDurationInSeconds);
        }

        private void DrawThrust()
        {
            if (Entity.IsBoosted)
                Debug.DrawRay(Entity.Position, -Entity.Velocity.normalized * 1.5f, Color.red);
        }

        private void DrawSelection()
        {
            if (Entity.IsSelected)
                DrawSelectionRender();
        }

        private void DrawExplosion()
        {
            if (!Entity.IsCollapsed)
                return;

            DrawCircle(Entity.Position, Entity.Dimension, true, Color.red, 1);
        }

        #endregion

        #region Virtual Methods

        public virtual void SetEntity(Entity entity)
        {
            Entity = entity;
        }

        #endregion

        #region Abstract Methods

        protected abstract void DrawSelectionRender();

        protected abstract void DrawMesh();

        #endregion

        #region Draw Functions

        protected void DrawTriangle(Vector2 pos, Vector2 velocity, float scale, Color color = default)
        {
            if (color == default)
                color = m_color;

            Vector2 up = new Vector2(velocity.y, -velocity.x).normalized * scale / 2;
            Vector2 right = velocity.normalized * (scale);
            Debug.DrawLine(pos + up, pos + right, color);
            Debug.DrawLine(pos - up, pos + right, color);
            Debug.DrawLine(pos - up, pos + up, color);
        }

        protected void DrawRectangle(Vector2 pos, Vector3 velocity, float length, float high, Color color = default)
        {
            if (color == default)
                color = m_selectedColor;

            Vector2 up = Utils.GetCrossedVector(velocity).normalized * high / 2;
            Vector2 right = velocity.normalized * length / 2;

            Debug.DrawLine(pos + up + right, pos - up + right, color);
            Debug.DrawLine(pos - up + right, pos - up - right, color);
            Debug.DrawLine(pos - up - right, pos + up - right, color);
            Debug.DrawLine(pos + up - right, pos + up + right, color);
        }

        protected void DrawCircle(Vector2 pos, float radius, bool withRays = false, Color color = default, float duration = default)
        {
            if (color == default)
                color = m_color;

            for (int i = 0; i < 360; i++)
            {
                float multiplier = withRays ? 1 / (1 + (i % 2)) : 1;
                Debug.DrawRay(pos, new Vector2(Mathf.Sin(i) * radius * multiplier, Mathf.Cos(i) * radius * multiplier),
                    color, duration);

                if (withRays)
                    Debug.DrawRay(pos, new Vector2(Mathf.Sin(i) * radius / 2, Mathf.Cos(i) * radius / 2), color, duration);
            }
        }

        #endregion
    }
}
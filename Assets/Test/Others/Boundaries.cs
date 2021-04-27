using UnityEngine;

namespace Test
{
    public class Boundaries
    {
        public readonly int High;
        public readonly int Length;

        public Utils.Directions? m_endSide;

        private Entity m_entity;

        #region Public Methods

        public Boundaries(int high, int length, Utils.Directions? endLineSide = null)
        {
            High = high;
            Length = length;
            m_endSide = endLineSide;

            m_entity = new Entity(Vector2.zero, 0, new BoundariesRender(high, length, endLineSide), isKinematic: true);
        }

        public void Render()
        {
            m_entity.Render();
        }

        public bool IsTheEntityInside(Entity itemSelected)
        {
            if (Mathf.Abs(itemSelected.Position.x) >= Length/2 || Mathf.Abs(itemSelected.Position.y) >= High/2)
                return false;

            return true;
        }

        public bool IsTheEntityOutInTheEndSide(Entity itemSelected)
        {
            switch (m_endSide)
            {
                case Utils.Directions.left:
                    return itemSelected.Position.x <= m_entity.Position.x - Length/2;
                case Utils.Directions.right:
                    return itemSelected.Position.x >= m_entity.Position.x + Length/2;
                case Utils.Directions.up:
                    return itemSelected.Position.y >= m_entity.Position.y + High/2;
                case Utils.Directions.down:
                    return itemSelected.Position.y <= m_entity.Position.y - High/2;
                default:
                    return false;
            }
        }

        public int GetMinSideDimension()
        {
            return Mathf.Min(High, Length);
        }

        public void SetEndSide(Utils.Directions endSide)
        {
            m_endSide = endSide;
            m_entity = new Entity(Vector2.zero, 0, new BoundariesRender(High, Length, endSide), isKinematic: true);
        }

        #endregion
    }
}
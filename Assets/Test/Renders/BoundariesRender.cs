using UnityEngine;

namespace Test
{
    public class BoundariesRender : Render
    {
        private int m_high;
        private int m_lenght;

        private Utils.Directions? sideEndLine;
        
        protected override void DrawSelectionRender()
        {
        }

        protected override void DrawMesh()
        {
            DrawRectangle(Entity.Position, Vector3.right, m_lenght, m_high, Color.white);
            
            if (sideEndLine == null)
                return;
            
            switch (sideEndLine)
            {
                case Utils.Directions.right:
                    Debug.DrawLine(new Vector2((float)m_lenght/2, (float)m_high/2), new Vector2((float)m_lenght/2, -(float)m_high/2), Color.blue);
                    break;
                case Utils.Directions.left:
                    Debug.DrawLine(new Vector2((float)-m_lenght/2, (float)m_high/2), new Vector2((float)-m_lenght/2, -(float)m_high/2), Color.blue);
                    break;
                case Utils.Directions.up:
                    Debug.DrawLine(new Vector2((float)-m_lenght/2, (float)m_high/2), new Vector2((float)m_lenght/2, (float)m_high/2), Color.blue);
                    break;
                case Utils.Directions.down:
                    Debug.DrawLine(new Vector2((float)-m_lenght/2, (float)-m_high/2), new Vector2((float)m_lenght/2, -(float)m_high/2), Color.blue);
                    break;
            }
            
            // I tried to draw chess board finish line but work is not tested

            // var isLeftOrRight = sideEndLine == Utils.Directions.left || sideEndLine == Utils.Directions.right;
            //
            // Vector2 startPos = new Vector2();
            // switch (sideEndLine)
            // {
            //     case Utils.Directions.right:
            //         startPos = new Vector2(m_lenght, m_high) + Vector2.right * cellSize + Vector2.down * cellSize;
            //         break;
            //     case Utils.Directions.left:
            //         startPos = new Vector2((float)-m_lenght/2, m_high - cellSize/2);// + Entity.Position + Vector2.left * (3 * cellSize) + Vector2.down * cellSize;
            //         break;
            //     case Utils.Directions.up:
            //         startPos = new Vector2(-m_lenght, m_high);// + Entity.Position + Vector2.up * (2 * cellSize) + Vector2.right * (cellSize);
            //         break;
            //     case Utils.Directions.down:
            //         startPos = new Vector2(-m_lenght, -m_high);// + Entity.Position + Vector2.down * (2 * cellSize) + Vector2.right * (cellSize);
            //         break;
            // }
            //
            // var count = isLeftOrRight ? m_lenght : m_high;
            //
            // for (int i = 0; i < count; i++)
            // {
            //     if (isLeftOrRight)
            //     {
            //         DrawRectangle(startPos + Vector2.down * (i * cellSize ), Vector3.down, cellSize, cellSize, i%2 == 0 ? Color.black : Color.white);
            //         DrawRectangle(startPos + Vector2.right * cellSize + Vector2.down * (i * cellSize ), Vector3.down , cellSize, cellSize, i%2 == 0 ? Color.white :Color.black);
            //     }
            //     else
            //     {
            //         DrawRectangle(startPos  + Vector2.right * (i * 2 * cellSize ), Vector3.down, cellSize, cellSize, i%2 == 0 ? Color.black : Color.white);
            //         DrawRectangle(startPos + Vector2.down * (2 * cellSize) + Vector2.right * (i * 2 * cellSize ), Vector3.down , cellSize, cellSize, i%2 == 0 ? Color.white :Color.black);
            //     }
            // }
        }

        public BoundariesRender(int high, int length, Utils.Directions? sideEndLine) : base(default, default, default)
        {
            Entity = new Entity(Vector2.zero, 0, this);
            m_high = high;
            m_lenght = length;
            this.sideEndLine = sideEndLine;
        }
    }
    
}

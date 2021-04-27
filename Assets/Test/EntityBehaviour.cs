using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class EntityBehaviour
    {
        private readonly Entity m_body;
        private readonly Entity m_pivot;

        public EntityBehaviour(Entity mBody, Entity mPivot = null)
        {
            m_body = mBody;
            m_pivot = mPivot;
        }

        public Vector2 CalculateAppliedVelocityPerFrame(List<Entity> entities)
        {
            Vector2 appliedForce = Vector2.zero;
            
            if (m_body.ManipulatedDirections != Utils.Directions.none || m_body.IsCollapsed || m_body.IsKinematic)
                return appliedForce;

            appliedForce += GetBoost();
            
            if (m_pivot != null && !m_pivot.IsCollapsed)
            {
                return (GetCircleOrbitalVelocity() - m_body.Velocity) * Constants.UpdateTime;
            }

            foreach (var item in entities)
            {
                if (m_body.Equals(item))
                    continue;

                var distanceVector = item.Position - m_body.Position;
                
                // Check collapse
                if (distanceVector.magnitude < Constants.MinDist)
                {
                    m_body.IsCollapsed = m_body.Mass <= item.Mass;
                    item.IsCollapsed = item.Mass <= m_body.Mass; 
                }

                // The applied force will be negative if the other planet is collapsed
                appliedForce +=
                    CalculateLawGravitationWithRestriction(distanceVector * (item.IsCollapsed ? -1 : 1), item.Mass);
            }

            return appliedForce * Constants.UpdateTime;
        }

        public Vector2 GetCircleOrbitalVelocity()
        {
            if(m_pivot == null)
                return Vector2.zero;
            
            return CalculateLawGravitationWithRestriction(Utils.GetCrossedVector(m_pivot.Position - m_body.Position),
                 m_pivot.Mass) + m_pivot.Velocity;
        }
        
        private Vector2 GetBoost()
        {
            if(!m_body.IsBoosted)
                return Vector2.zero;

            var acc = m_body.Velocity.normalized * (m_body.Velocity.magnitude * 1.5f) - m_body.Velocity;

            // If no velocity detected it will start moving toward the center 
            return  acc.magnitude < Constants.MinVelocity ? (Vector2.zero - m_body.Position).normalized * m_body.Mass/100 : acc; 
        }            

        private Vector2 CalculateLawGravitationWithRestriction(Vector2 distanceVector, float m1)
        {
            if (distanceVector.magnitude > Constants.MaxDist || distanceVector.magnitude < Constants.MinDist)
                return Vector2.zero;

            float distanceSquared = distanceVector.sqrMagnitude;
            Vector2 direction = distanceVector.normalized;

            return direction * (Constants.G * m1 / distanceSquared);
        }

        public Vector2 CalculatePosition()
        {
            Vector2 finalPosition = m_body.Position;
            switch (m_body.ManipulatedDirections)
            {
                case Utils.Directions.left:
                    finalPosition += Vector2.left * Constants.MinStep;
                    break;
                case Utils.Directions.right:
                    finalPosition += Vector2.right * Constants.MinStep;
                    break;
                case Utils.Directions.up:
                    finalPosition += Vector2.up * Constants.MinStep;
                    break;
                case Utils.Directions.down:
                    finalPosition += Vector2.down * Constants.MinStep;
                    break;
                case Utils.Directions.none:
                default:
                    finalPosition += m_body.Velocity;
                    break;
            }
            
            if (Mathf.Abs(finalPosition.x) > Constants.LengthBoundaryDimension/2)
                finalPosition.x = finalPosition.x > 0 ? Constants.LengthBoundaryDimension/2 : -Constants.LengthBoundaryDimension/2;
            
            if (Mathf.Abs(finalPosition.y) > Constants.HighBoundaryDimension/2)
                finalPosition.y = finalPosition.y > 0 ? Constants.HighBoundaryDimension/2 : -Constants.HighBoundaryDimension/2;
            
            return finalPosition;
            
        }
    }
}
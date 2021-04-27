using System;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    // The generic space entity.
    public class Entity
    {
        #region Public Members
        
        // Position and velocity in space.
        public Vector2 PrevPosition { get; private set; }
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        
        // Mass
        public readonly float Mass;
        
        // Dimension
        public float Dimension => Mathf.Min(Utils.GetDimensionPerMass(Mass), 10);

        // Control info

        public bool IsKinematic;
        public bool IsSelected = false;
        public bool IsCollapsed = false;
        public bool IsPaused
        {
            get; private set;
        }
        public bool IsBoosted
        {
            get => m_isBoosted;
            set => m_isBoosted = m_canBeBoosted && value; 
        }
        public Utils.Directions ManipulatedDirections
        {
            get => m_manipulatedDirections;
            set
            {
                if (!m_canBeManipulated)
                {
                    m_manipulatedDirections = Utils.Directions.none;
                    return;
                }

                if (value != Utils.Directions.none)
                    Velocity = Vector2.zero;

                m_manipulatedDirections = value;
            }
        }
        
        public bool IsMoving => !IsKinematic && Utils.IsTheVectorDescribingMotion(Velocity);

        #endregion

        #region Private Members
        
        // render
        private readonly Render m_render;
        
        // behaviour
        private readonly EntityBehaviour m_behaviour;
        
        private bool m_isBoosted;

        private bool m_canBeBoosted => !IsKinematic && IsSelected && !IsPaused && !IsCollapsed;

        private bool m_canBeManipulated;
        private Utils.Directions m_manipulatedDirections = Utils.Directions.none;
        #endregion
        
        public Entity(Vector2 position, float mass, Render render, Vector2 startVelocityDirection = default, Entity pivotPlanet = null, bool isKinematic = false, bool canBeManipulated = false)
        {
            Mass = mass;
            Position = position + (pivotPlanet?.Position ?? Vector2.zero);
            m_render = render;
            m_render.SetEntity(this);
            m_canBeManipulated = canBeManipulated;
            m_behaviour = new EntityBehaviour(this, pivotPlanet);
            IsKinematic = isKinematic;

            Velocity += startVelocityDirection;
            
            if(!Utils.IsTheVectorDescribingMotion(Velocity))
                Velocity += m_behaviour.GetCircleOrbitalVelocity();
        }

        #region Public Methods
        
        public void CalculateVelocity(List<Entity> planets)
        {
            Velocity += m_behaviour.CalculateAppliedVelocityPerFrame(planets);
        }

        public void CalculatePosition()
        {
            PrevPosition = Position;
            Position = m_behaviour.CalculatePosition();
        }

        public void Render(bool isPaused = false)
        {
            IsPaused = isPaused;
            m_render.UpdateRender();
        }

        #endregion
        
    }
}
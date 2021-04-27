using UnityEngine;

namespace Test
{
    /// <summary>
    /// Constant for various aspect, some of them can be modified for specific purposes
    /// </summary>
    
    public static class Constants 
    {
        // Update Fix Time Step
        public static float UpdateTime;
        
        // PHYSICS

        // Gravitational Constant
        public const float G = 0.02f;
        
        // Threshold for stale condition
        public const float MinVelocity = 0.001f;
        
        // Max distance between planet to consider attraction
        public const float MaxDist = 1000f;
        
        // Min distance between planet to consider collision
        public static float MinDist = 0.1f;
        
        // Min distance between planet to consider collision
        public static float MinStep;
        
        // High boundary dimension
        public static float HighBoundaryDimension;

        // Length boundary dimension
        public static float LengthBoundaryDimension;
        
        
        // RENDERING
        
        public static readonly Color SelectionColor = Color.magenta;
        
        public static readonly Color OrbitColor = Color.green;

        public static float OrbitTrailDurationInSeconds;

        public static float MaxMass;
        
        // GAME

        public static bool ShowOrbits = true;

        // Reset function, here some constant are defined
        public static void SetupDefault()
        {
            OrbitTrailDurationInSeconds = 1;
            MaxMass = 1f;
            MinStep = 1;
            HighBoundaryDimension = 1000;
            LengthBoundaryDimension = 1000;

        }

    }
    
}


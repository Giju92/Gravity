using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Test
{
    /// <summary>
    /// Here are defined function and enum shared in the namespace
    /// </summary>
    public static class Utils
    {
        public enum KeyState
        {
            down,
            up,
            pressed
        } 
        public enum Directions
        {
            up,
            down,
            left,
            right,
            none
        } 
        
        public static bool IsTheVectorDescribingMotion(Vector2 velocity)
        {
            return Math.Abs(velocity.magnitude) > Constants.MinVelocity;
        }

        public static Vector2 GetCrossedVector(Vector2 vector)
        {
           return new Vector2(vector.y, -vector.x);
        }

        public static void PrintLog(string log, Color c = default)
        {
            c = c == default ? Color.white : c;
            Debug.Log (string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(c.r * 255f), (byte)(c.g * 255f), (byte)(c.b * 255f), log));
        }

        public static float GetDimensionPerMass(float mass)
        {
            return mass / Constants.MaxMass;
        }
        
        public static Vector2 GetRandomStartVelocity(float maxVelocityValue)
        {
            var random = Random.Range(0, 3) % 3;
            switch (random)
            {
                case 0:
                    return Vector2.up * Random.Range(-maxVelocityValue,maxVelocityValue);
                case 1:
                    return Vector2.right * Random.Range(-maxVelocityValue,maxVelocityValue);
                default:
                    return Vector2.zero;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Test
{
    public class InfiniteLoopSystem : System
    {
        protected override void DisposeSystem()
        {
            var ball1 = new Entity(new Vector2(-0.97000436f, 0.24308753f), 1f,
                new HeavenlyBodyRender(Color.blue, Color.blue), new Vector2(-0.93240737f, -0.86473146f)* -0.01f);
            entities.Add(ball1);

            var ball2 = new Entity(new Vector2(0.97000436f, -0.24308753f), 1f,
                new HeavenlyBodyRender(Color.green, Color.green), new Vector2(-0.93240737f, -0.86473146f)* -0.01f);
            entities.Add(ball2);

            var ball3 = new Entity(new Vector2(0, 0), 1f, new HeavenlyBodyRender(Color.red, Color.red),
                new Vector2(-0.93240737f, -0.86473146f)* 0.02f);
            entities.Add(ball3);
        }

        protected override void DisplayInfo(bool isFirstTime)
        {
            base.DisplayInfo(isFirstTime);

            if (isFirstTime)
            {
                Utils.PrintLog("A simple periodic orbit for the newtonian problem of three equal masses in the plane.", logColor);
                Utils.PrintLog("The orbit has zero angular momentum and a very rich symmetry pattern.", logColor);
                Utils.PrintLog("Its most surprising feature is that the three bodies chase each other around a fixed eight-shaped curve.", logColor);
                Utils.PrintLog("Watch out, it is tiny. Be sure you have zoomed enough.", logColor);
                DisplayCommand();
            }
        }

        protected override void SetupConstants()
        {
            base.SetupConstants();
            Constants.MinDist = 0.001f;
            Constants.MaxMass = 10;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Test;
using UnityEngine;

namespace Test
{
    public class SolarSystem : System
    {
        protected override void DisposeSystem()
        {
            // Sun
            var sun = new Entity(Vector3.zero, 4000, new StarRender(), isKinematic:true);
            entities.Add(sun);
            
            // mercury
            var mercury = new Entity(Vector3.down * 20, 10f, new HeavenlyBodyRender(Color.magenta, Color.magenta, forceDimension: 0.8f),pivotPlanet:sun);
            entities.Add(mercury);
            
            // venus
            var venus = new Entity(Vector3.up * 30, 50f, new HeavenlyBodyRender(Color.green, Color.green, forceDimension: 1),pivotPlanet:sun);
            entities.Add(venus);
            
            // Earth
            var earth = new Entity(Vector3.right * 60, 50f, new HeavenlyBodyRender(Color.blue, Color.blue, forceDimension: 1), pivotPlanet:sun);
            entities.Add(earth);
            
            var moon = new Entity(Vector3.up * 5f, 0.001f, new HeavenlyBodyRender(Color.gray, Color.blue, forceDimension: 0.5f), pivotPlanet: earth);
            entities.Add(moon);
            
            //mars
            var mars = new Entity(Vector3.left * 80, 50f,
                new HeavenlyBodyRender(Color.red, Color.red, forceDimension: 1.5f),  pivotPlanet:sun);
            entities.Add(mars);
        }

        protected override void SetupConstants()
        {
            base.SetupConstants();
            Constants.MaxMass = 10;
            Constants.OrbitTrailDurationInSeconds = 30;
        }
        
        protected override void DisplayInfo(bool isFirstTime)
        {
            base.DisplayInfo(isFirstTime);

            if (isFirstTime)
            {
                Utils.PrintLog("The first planets in the solar system.", logColor);
                Utils.PrintLog("Mercury, Venus, Earth with the Moon and Mars.", logColor);
                DisplayCommand();
            }
        }
    }
}
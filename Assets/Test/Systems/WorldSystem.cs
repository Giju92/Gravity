using UnityEngine;

namespace Test
{
    public class WorldSystem : System
    {
        protected override void DisposeSystem()
        {
            var world = new Entity(Vector2.zero, 1250f, new HeavenlyBodyRender(Color.blue), isKinematic:true);
            entities.Add(world);
            
            var moon = new Entity(Vector3.right * 70, 0.01f, new HeavenlyBodyRender(Color.gray, Color.black, forceDimension: 5), pivotPlanet:world);
            entities.Add(moon);
           
            var spaceShip = new Entity(Vector2.right*25, 10f, new SpaceShipRender(Color.red), Vector2.up*0.15f);
            entities.Add(spaceShip);
            
            spaceShip = new Entity(Vector2.up*50, 10f, new SpaceShipRender(Color.blue), Vector2.right*0.1f);
            entities.Add(spaceShip);
            
            spaceShip = new Entity(Vector2.down*30, 10f, new SpaceShipRender(Color.green), Vector2.left*0.15f);
            entities.Add(spaceShip);
            
            spaceShip = new Entity(Vector2.left*30 + Vector2.up, 100f, new SpaceShipRender(Color.yellow), Vector2.down*0.1f);
            entities.Add(spaceShip);
            
        }
        
        protected override void DisplayInfo(bool isFirstTime)
        {
            base.DisplayInfo(isFirstTime);

            if (isFirstTime)
            {
                Utils.PrintLog("The planet heart with his moon and some space stations.", logColor);
                DisplayCommand();
            }
        }

        protected override void SetupConstants()
        {
            base.SetupConstants();
            Constants.MinDist = 0.001f;
            Constants.MaxMass = 10;
            Constants.OrbitTrailDurationInSeconds = 15;
        }
    }
}
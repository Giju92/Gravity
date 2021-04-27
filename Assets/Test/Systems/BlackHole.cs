using UnityEngine;

namespace Test
{
    public class BlackHole : System
    {
        protected override void DisposeSystem()
        {
            // Black Hole
            var blackHole = new Entity(Vector2.zero, 100000, new HeavenlyBodyRender(Color.black), isKinematic:true);
            entities.Add(blackHole);
            
            // All the other entities
            for(int i = 0; i < 100; i++)
            {
                var star = new Entity(Random.insideUnitCircle * 500, Random.Range(100, 250), GetRandomRender(),Utils.GetRandomStartVelocity(0.1f));
                
                entities.Add(star);
            }
        }
        
        private Render GetRandomRender()
        {
            var random = Random.Range(0, 12) % 5;
            switch (random)
            {
                case 0:
                    return new HeavenlyBodyRender(Color.blue);
                case 1:
                    return new HeavenlyBodyRender(Color.green);
                case 2:
                    return new HeavenlyBodyRender(Color.magenta);
                case 3:
                    return new StarRender();
                case 4:
                    return new SpaceShipRender();
                default:
                    return new SpaceShipRender();
            }
        }
        
        
        
        protected override void SetupConstants()
        {
            base.SetupConstants();
            Constants.OrbitTrailDurationInSeconds = 5;
            Constants.MinDist = 10f;
            Constants.MaxMass = 50;
        }
        
        protected override void DisplayInfo(bool isFirstTime)
        {
            base.DisplayInfo(isFirstTime);
            if (isFirstTime)
            {
                Utils.PrintLog("A simple simulation of a black hole.", logColor);
                Utils.PrintLog("The black hole mass is not big as it should be in the reality, so some planets could win his attraction!", logColor);
                Utils.PrintLog("Lets count how many will stand", logColor); 
                DisplayCommand();
            }
        }
    }
}

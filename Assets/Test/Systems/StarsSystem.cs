using UnityEngine;

namespace Test
{
    public class StarsSystem : GameSystem
    {
        protected override void DisposeSystem()
        {
            // Space Ship
            var spaceShip = new Entity(Vector2.zero, 5, new SpaceShipRender());
            entities.Add(spaceShip);
            
            // Stars
            for(int i = 0; i < 20; i++)
            {
                var star = new Entity(Random.insideUnitCircle * m_boundaries.GetMinSideDimension()/2, Random.Range(7.5f, 15), new StarRender(), isKinematic:true);
                entities.Add(star);
            }
            
        }

        public StarsSystem(int high, int length, Utils.Directions? endLineSide = Utils.Directions.none) : base(high, length, endLineSide)
        {
            m_canChangeEntities = false;
        }

        public override void SetupSystem(bool isFirstTime)
        {
            base.SetupSystem();
            SetRandomEndSide();
        }

        protected override void SetupConstants()
        {
            base.SetupConstants();
            Constants.MinDist = 0.15f;
        }
      
        
        protected override void DisplayInfo(bool isFirstTime)
        {
            base.DisplayInfo(isFirstTime);
            if (isFirstTime)
            {
                Utils.PrintLog("Try to hit the blue line.", logColor);
                Utils.PrintLog("You can boost your space ship along its current velocity vector.", logColor);
                DisplayCommand();
            }
            
        }

        protected override void DisplayCommand()
        {
            base.DisplayCommand();
            Utils.PrintLog("BACK SPACE => RESTART", Color.green);
        }
    }
}
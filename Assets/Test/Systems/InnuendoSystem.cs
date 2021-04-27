using UnityEngine;

namespace Test
{
    public class InnuendoSystem : GameSystem
    {
        private int initialEntities;
        
        public InnuendoSystem(int high, int length, Utils.Directions? endLineSide = null) : base(high, length, endLineSide)
        {
        }

        protected override void SetNextSelectedIndex(bool isNext)
        {
            base.SetNextSelectedIndex(isNext);
            if(SelectedIndex == 0)
                SetNextSelectedIndex(isNext);
        }

        public override void UpdateSystem()
        {
            base.UpdateSystem();
            if (entities != null && entities.Count != initialEntities)
                EndGame(false);
            
            foreach (var entity in entities)
            {
                if (!m_boundaries.IsTheEntityInside(entity) || entity.IsCollapsed)
                    EndGame(false);
            }
            
        }

        protected override void DisposeSystem()
        {
            // Space Ship
            var spaceShip = new Entity(Vector2.zero, 5, new SpaceShipRender(), canBeManipulated: false);
            entities.Add(spaceShip);
            
            // Stars
            for(int i = 0; i < 5; i++)
            {
                var star = new Entity(Random.insideUnitCircle * m_boundaries.GetMinSideDimension()/2, Random.Range(7.5f, 15), new StarRender(), canBeManipulated:true);
                entities.Add(star);
            }

            initialEntities = entities.Count;
        }
        
        public override void SetupSystem(bool isFirstTime)
        {
            base.SetupSystem(isFirstTime);
            SetRandomEndSide();
            SetNextSelectedIndex(true);
        }

        protected override void SetupConstants()
        {
            base.SetupConstants();
            Constants.MinDist = 0.2f;
        }

        protected override void DisplayInfo(bool isFirstTime)
        {
            base.DisplayInfo(isFirstTime);
            if (isFirstTime)
            {
                Utils.PrintLog("Try to hit the blue line with the space ship.", logColor);
                Utils.PrintLog("You can move all the entities around but not the space ship.", logColor);
                Utils.PrintLog("Be careful you cannot lose any planet.", logColor);
                DisplayCommand();
            }
            
        }

        protected override void DisplayCommand()
        {
            base.DisplayCommand();
            Utils.PrintLog("W,A,S,D => MOVE the controlled entity", Color.green);
            Utils.PrintLog("<= => => MOVE to previous o next entity", Color.green);
        }
    }

}

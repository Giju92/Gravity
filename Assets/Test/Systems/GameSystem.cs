using UnityEngine;

namespace Test
{
    /// <summary>
    /// An extension of the system, to play a game
    /// It manage the selection of an entity, end of the game and inputs
    /// </summary>
    public abstract class GameSystem : System
    {
        protected Boundaries m_boundaries;

        protected int m_currentSelectedIndex = -1;

        protected bool m_canChangeEntities = true;

        private Entity ItemSelected => entities[m_currentSelectedIndex];

        private Entity thePlayer;

        #region Public Methods

        public override void SetupSystem(bool isFirstTime = true)
        {
            base.SetupSystem(isFirstTime);
            SelectedIndex = 0;
            thePlayer = ItemSelected;
        }

        public override bool HandleReceivedKey(KeyCode? key, Utils.KeyState keyState)
        {
            if (base.HandleReceivedKey(key, keyState))
                return true;

            if (key == KeyCode.I && keyState == Utils.KeyState.down)
            {
                DisplayCommand();
                return true;
            }

            if (entities == null || entities.Count == 0 || ItemSelected == null)
                return false;

            if (key == KeyCode.A)
            {
                ItemSelected.ManipulatedDirections =
                    keyState == Utils.KeyState.up ? Utils.Directions.none : Utils.Directions.left;
                return true;
            }

            if (key == KeyCode.D)
            {
                ItemSelected.ManipulatedDirections =
                    keyState == Utils.KeyState.up ? Utils.Directions.none : Utils.Directions.right;
                return true;
            }

            if (key == KeyCode.W)
            {
                ItemSelected.ManipulatedDirections =
                    keyState == Utils.KeyState.up ? Utils.Directions.none : Utils.Directions.up;
                return true;
            }

            if (key == KeyCode.S)
            {
                ItemSelected.ManipulatedDirections =
                    keyState == Utils.KeyState.up ? Utils.Directions.none : Utils.Directions.down;
                return true;
            }

            if ((key == KeyCode.LeftArrow || key == KeyCode.RightArrow) && keyState == Utils.KeyState.down &&
                m_canChangeEntities)
            {
                SetNextSelectedIndex(key == KeyCode.RightArrow);
                return true;
            }

            if (key == KeyCode.Space && IsSystemRunning)
            {
                if (ItemSelected != null)
                {
                    ItemSelected.IsBoosted = keyState != Utils.KeyState.up;
                    return true;
                }
            }


            return false;
        }

        public override void UpdateSystem()
        {
            base.UpdateSystem();

            m_boundaries.Render();

            if (thePlayer != null && thePlayer.IsCollapsed)
                EndGame(false);

            if (thePlayer != null && !m_boundaries.IsTheEntityInside(thePlayer))
                EndGame(m_boundaries.IsTheEntityOutInTheEndSide(thePlayer));
        }

        #endregion

        #region Protected Methods

        protected GameSystem(int high, int length, Utils.Directions? endLineSide = null)
        {
            m_boundaries = new Boundaries(high, length, endLineSide);
        }

        protected override void SetupConstants()
        {
            base.SetupConstants();
            Constants.MinStep = (float) m_boundaries.GetMinSideDimension() / 50;
            Constants.HighBoundaryDimension = m_boundaries.High;
            Constants.LengthBoundaryDimension = m_boundaries.Length;
        }

        protected int SelectedIndex
        {
            get => m_currentSelectedIndex;
            private set
            {
                if (entities == null || entities.Count == 0)
                    m_currentSelectedIndex = -1;

                m_currentSelectedIndex = value;

                var planetCount = entities.Count;
                for (int i = 0; i < planetCount; i++)
                    entities[i].IsSelected = SelectedIndex == i;
            }
        }

        protected virtual void SetNextSelectedIndex(bool isNext)
        {
            var planetCount = entities?.Count ?? 0;

            int value = SelectedIndex;

            value += isNext ? +1 : -1;

            if (value < 0)
                value = planetCount - 1;

            value %= planetCount;

            SelectedIndex = value;
        }

        protected void EndGame(bool hasPlayerWin)
        {
            RestartSystem();

            if (hasPlayerWin)
                Utils.PrintLog("YOU WIN", Color.green);
            else
                Utils.PrintLog("YOU LOST", Color.red);
        }

        protected void SetRandomEndSide()
        {
            var random = Random.Range(0, 4) % 3;
            switch (random)
            {
                case 0:
                    m_boundaries.SetEndSide(Utils.Directions.right);
                    break;
                case 1:
                    m_boundaries.SetEndSide(Utils.Directions.up);
                    break;
                case 2:
                    m_boundaries.SetEndSide(Utils.Directions.down);
                    break;
                default:
                    m_boundaries.SetEndSide(Utils.Directions.left);
                    break;
            }
        }

        #endregion
    }
}
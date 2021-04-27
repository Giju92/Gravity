using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Test
{
    
    /// <summary>
    /// A system is the base class of each simulation.
    /// </summary>
    public abstract class System
    {
        // Enum to name the system states
        protected enum SystemState
        {
            Ready,
            Run,
            Pause,
            Ended
        }

        protected SystemState currentState;

        protected List<Entity> entities;
        protected bool IsSystemRunning => currentState == SystemState.Run;

        protected Color logColor = new Color(229,103,23);
        
        #region Abstract Methods

        protected abstract void DisposeSystem();

        #endregion

        #region Public Methods

        public void RestartSystem()
        {
            StopSystem();
            SetupSystem(false);
        }

        public void EndSystem()
        {
            currentState = SystemState.Ended;
        }

        #endregion

        #region Private Methods

        private void StartOrStopSystem()
        {
            if (IsSystemRunning)
                StopSystem();
            else
                StartSystem();
        }

        private void StopSystem()
        {
            currentState = SystemState.Pause;
        }

        private void StartSystem()
        {
            currentState = SystemState.Run;
        }

        #endregion

        #region Virtual Methods

        public virtual void SetupSystem(bool isFirstTime = true)
        {
            entities = new List<Entity>();
            DisposeSystem();
            SetupConstants();
            DisplayInfo(isFirstTime);
            currentState = SystemState.Ready;
        }

        public virtual void UpdateSystem()
        {
            if (currentState == SystemState.Ended)
                return;

            int planetCount = entities.Count;
            if (currentState == SystemState.Run)
            {
                // First Update Velocity for each Entity in the system
                for (int i = 0; i < planetCount; i++)
                    entities[i].CalculateVelocity(entities);

                // Secondly Update the positions 
                for (int i = 0; i < planetCount; i++)
                    entities[i].CalculatePosition();
            }

            // Render
            for (int i = 0; i < planetCount; i++)
                entities[i].Render(!IsSystemRunning);

            // Clean list
            entities = entities.Where(x => !x.IsCollapsed).ToList();
        }

        protected virtual void SetupConstants()
        {
            Constants.SetupDefault();

            // Set Max value for Mass
            foreach (var item in entities)
                Constants.MaxMass = (int) Mathf.Max(item.Mass, Constants.MaxMass);
        }

        public virtual bool HandleReceivedKey(KeyCode? key, Utils.KeyState keyState)
        {
            // Here we handle only key down event to trigger only one action at a time
            if (keyState != Utils.KeyState.down || key == null)
                return false;

            if (key == KeyCode.Return)
            {
                StartOrStopSystem();
                return true;
            }

            if (key == KeyCode.Backspace)
            {
                RestartSystem();
                return true;
            }

            if (key == KeyCode.LeftShift || key == KeyCode.LeftShift)
            {
                Constants.ShowOrbits = !Constants.ShowOrbits;
                return true;
            }
            

            return false;
        }

        protected virtual void DisplayInfo(bool IsFirstTime)
        {
            if (IsFirstTime)
            {
                var name = GetType().Name.Replace("System", "");
                string log = "----------- Welcome in the system called: " + name + " -----------";
                Utils.PrintLog(log, logColor);
            }
            else
            {
                DisplayRestart();
            }
        }

        protected virtual void DisplayRestart()
        {
            Utils.PrintLog("RESTART", logColor);
            Utils.PrintLog("L => LIST all the systems", logColor);
            Utils.PrintLog("I => LIST all the commands", logColor);
        }
        
        protected virtual void DisplayCommand()
        {
            Utils.PrintLog("RETURN => PLAY/STOP", Color.green);
            Utils.PrintLog("Shift => ENABLE/DISABLE the orbits", Color.green);
        }

        #endregion
    }
}
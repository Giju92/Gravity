using System;
using System.Collections.Generic;
using UnityEngine;
using Test;

public class TestMain : MonoBehaviour
{
    // List of available systems
    private readonly List<Test.System> m_systems = new List<Test.System>();

    // System currently selected
    private Test.System m_selected;

    #region Unity Methods

    private void Awake()
    {
        // Set the delta time accordingly to the environment in which runs 
        Constants.UpdateTime = Time.fixedDeltaTime;
    }

    private void Start()
    {
        // Add all the systems to show 
        m_systems.Add(new WorldSystem());
        m_systems.Add(new SolarSystem());
        m_systems.Add(new BlackHole());
        m_systems.Add(new InfiniteLoopSystem());
        m_systems.Add(new StarsSystem(50, 50));
        m_systems.Add(new InnuendoSystem(50, 50));

        // Print welcome message and the list of all the systems just added
        DisplayInfo();

        // Setup the first system by default
        SetupSystem();
    }

    // Last pressed key
    private KeyCode? lastKeyCode = null;

    private void Update()
    {
        if (Input.anyKey)
        {
            // Select the system by pressing key number (MAX 10 SYSTEMS ALLOWED IN THIS WAY!!!)
            for (int i = 0; i < 10 && i < m_systems.Count; i++)
            {
                var keyZero = (int) KeyCode.Alpha0;
                if (Input.GetKeyDown((KeyCode) keyZero + i))
                {
                    SetupSystem(i);
                    return;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.L))
            {
                DisplayInfo();
                return;
            }

            if (m_selected == null) return;

            // Forward the pressed key to the selected system
            foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keycode))
                {
                    m_selected.HandleReceivedKey(keycode, Utils.KeyState.down);
                    lastKeyCode = keycode;
                    return;
                }

                if (Input.GetKey(keycode))
                {
                    m_selected.HandleReceivedKey(keycode, Utils.KeyState.pressed);
                    return;
                }
            }
        }
        // Hack to send the key up event to the system
        else if (lastKeyCode != null)
        {
            m_selected.HandleReceivedKey(lastKeyCode, Utils.KeyState.up);
            lastKeyCode = null;
        }
    }

    private void FixedUpdate()
    {
        // Update the system with constant delta time
        m_selected?.UpdateSystem();
    }

    #endregion

    #region Private Methods

    private void SetupSystem(int i = 0)
    {
        if (m_systems == null || m_systems.Count == 0 || i >= m_systems.Count)
            return;

        var newSystem = m_systems[i];

        if (newSystem.Equals(m_selected))
        {
            m_selected.RestartSystem();
            return;
        }

        m_selected?.EndSystem();

        m_selected = newSystem;
        m_selected.SetupSystem();
    }

    private void DisplayInfo()
    {
        int i = 0;
        foreach (var item in m_systems)
        {
            var name = item.GetType().Name.Replace("System", "");
            Utils.PrintLog("Press " + i + " to start: " + name.ToUpper(), Color.green);
            i++;
        }
    }

    #endregion
}
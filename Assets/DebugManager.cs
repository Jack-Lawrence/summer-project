using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [Header("Variables")]
    public bool isDebugVisible = false;

    private float debugMenuYOffset = 10f;
    private PlayerMovement playerMovement;


    void Start()
    {
        isDebugVisible = false;
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            isDebugVisible = !isDebugVisible;
        }
    }

    void OnGUI()
    {
        if (isDebugVisible)
        {
            float yPosition = debugMenuYOffset;

            //Draw the debug menu entries with dynamic Y position
            GUI.color = Color.black;

            GUI.Label(new Rect(10, yPosition, 200, 20), "DEBUG MENU (F1 TOGGLE)");
            yPosition += 20;
            GUI.Label(new Rect(10, yPosition, 200, 20), "PLAYER STATE: " + playerMovement.currentState);
            yPosition += 20;

            GUI.color = Color.white;
        }
    }
}

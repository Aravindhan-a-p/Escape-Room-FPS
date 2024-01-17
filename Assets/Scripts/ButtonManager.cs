using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance { get; set; }

    private Button[] buttons;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        buttons = FindObjectsOfType<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        bool temp = true;
        foreach (Button button in buttons)
        {
            temp = temp && button.isActive;
        }
        if (temp)
        {
            FindObjectOfType<Exit>().openDoor();  
        }
    }
}

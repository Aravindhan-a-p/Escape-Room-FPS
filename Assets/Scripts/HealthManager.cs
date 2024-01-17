using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; set; }

    public TextMeshProUGUI healthDisplay;

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
    }
}

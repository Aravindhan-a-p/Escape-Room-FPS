using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Button hoveredButton = null;

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

    private void Update()
    {
        Ray ray= Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.transform.gameObject;

            if (objectHit.GetComponent<Button>())
            {
                hoveredButton = objectHit.GetComponent<Button>();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hoveredButton.isActive = true;
                }
            }
        }
    }
}

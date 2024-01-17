using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int HP = 100;

    public GameObject bloodyScreen;

    public GameObject GameOverUI;

    public GameObject canvas;

    private bool isDead=  false;

    void Start()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            PlayerDeath();
        }
        else
        {
            StartCoroutine(BloddyScreenEffect());
            SoundManager.Instance.playerHurt.Play();
        }
    }

    private void PlayerDeath()
    {
        if (!isDead)
        {
            SoundManager.Instance.playerDeath.Play();
        }
        

        isDead = true;

        GetComponent<MouseMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        GetComponentInChildren<Animator>().enabled = true;

        GetComponent<ScreenFader>().StartFade();

        StartCoroutine(ShowGameOverUI());

        canvas.SetActive(false);
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        GameOverUI.SetActive(true);
    }

    private IEnumerator BloddyScreenEffect()
    {
        if(bloodyScreen.activeInHierarchy == false)
        {
            bloodyScreen.SetActive(true);
        }

        var image = bloodyScreen.GetComponentInChildren<Image>();

        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        if (bloodyScreen.activeInHierarchy == true)
        {
            bloodyScreen.SetActive(false);
        }
    }

    void Update()
    {
        if (HP >= 0)
        {
            if (HealthManager.Instance.healthDisplay != null)
            {
                HealthManager.Instance.healthDisplay.text = $"HP : {HP}";
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percentage : MonoBehaviour
{
    ScoreManager scoreManager;
    GameObject[] brush;
    Brush brushScript;
    bool artýrýldý;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        brushScript = FindObjectOfType<Brush>();
    }
    void Update()
    {
        //BrushDeleter();

        if (scoreManager.initialCountNumber == 100)
        {
            brushScript.canPaint = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brush") && !artýrýldý)
        {
            artýrýldý = true;

            if (scoreManager.initialCountNumber < 80)
            {
                scoreManager.initialCountNumber += 1;
            }
            else
            {
                scoreManager.initialCountNumber += .5f;

            }
            StartCoroutine(IncreasePercentage());
            StartCoroutine(DestroyPercentage());
        }
    }
    void BrushDeleter()
    {
        brush = GameObject.FindGameObjectsWithTag("Brush");

        if (brush != null)
        {
            for (int i = 0; i < brush.Length - 1; i++) // I made this to ristrict amount of brushes
            {
                if (brush[i + 1].transform.position == brush[i].transform.position)
                {
                    Destroy(brush[i + 1]);
                }
                if (brushScript.canPaint == false)
                {
                    Destroy(brush[i]);
                }
            }
        }
        else
        {
            return;
        }
    }
    IEnumerator IncreasePercentage()
    {
        yield return new WaitForSeconds(.4f);
        scoreManager.percentageCounter.text = "%" + Mathf.RoundToInt(scoreManager.initialCountNumber);
    }
    IEnumerator DestroyPercentage()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
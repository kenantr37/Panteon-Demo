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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brush") && !artýrýldý)
        {
            artýrýldý = true;

            if (scoreManager.InitialCountNumber < 80)
            {
                scoreManager.InitialCountNumber += 1;
            }
            else
            {
                scoreManager.InitialCountNumber += .5f;

            }
            StartCoroutine(IncreasePercentage());
            StartCoroutine(DestroyPercentage());
        }
    }
    IEnumerator IncreasePercentage()
    {
        yield return new WaitForSeconds(.4f);
        scoreManager.percentageCounter.text = "%" + Mathf.RoundToInt(scoreManager.InitialCountNumber);
    }
    IEnumerator DestroyPercentage()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
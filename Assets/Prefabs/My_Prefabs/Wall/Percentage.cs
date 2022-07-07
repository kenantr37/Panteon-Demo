using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percentage : MonoBehaviour
{
    ScoreManager scoreManager;
    bool artýrýldý;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brush") && !artýrýldý)
        {
            scoreManager.initialCountNumber += 1;
            artýrýldý = true;
            StartCoroutine(IncreasePercentage());
            StartCoroutine(DestroyPercentage());
        }
    }
    IEnumerator IncreasePercentage()
    {
        yield return new WaitForSeconds(.4f);
        scoreManager.percentageCounter.text = "%" + scoreManager.initialCountNumber;
    }
    IEnumerator DestroyPercentage()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
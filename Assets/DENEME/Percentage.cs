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
            scoreManager.initialCountNumber += 5;
            artýrýldý = true;
            scoreManager.percentageCounter.text = "%" + scoreManager.initialCountNumber;
            Destroy(gameObject);
        }
    }
}

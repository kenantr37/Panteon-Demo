using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percentage : MonoBehaviour
{
    ScoreManager scoreManager;
    bool art�r�ld�;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brush") && !art�r�ld�)
        {
            scoreManager.initialCountNumber += 5;
            art�r�ld� = true;
            scoreManager.percentageCounter.text = "%" + scoreManager.initialCountNumber;
            Destroy(gameObject);
        }
    }
}

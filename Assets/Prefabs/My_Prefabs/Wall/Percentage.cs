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
            scoreManager.initialCountNumber += 1;
            art�r�ld� = true;
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
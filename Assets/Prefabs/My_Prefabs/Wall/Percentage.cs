using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percentage : MonoBehaviour
{
    ScoreManager scoreManager;
    bool artýrýldý;
    GameObject[] brush;
    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        BrushDeleter();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brush") && !artýrýldý)
        {
            artýrýldý = true;
            scoreManager.initialCountNumber += 1;
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
        scoreManager.percentageCounter.text = "%" + scoreManager.initialCountNumber;
    }
    IEnumerator DestroyPercentage()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
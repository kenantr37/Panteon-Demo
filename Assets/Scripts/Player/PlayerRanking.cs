using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanking : MonoBehaviour
{
    [SerializeField] List<GameObject> karakterler;
    GameObject birinci;
    float enYak�nMesafe;
    float ilkEnYak�nMesafe;

    void Update()
    {
        S�ray�Goster();
        Debug.Log("EN YAKIN K��� " + enYak�nMesafe + " ile : " + birinci.name);
    }
    void S�ray�Goster()
    {

        for (int i = 0; i < karakterler.Count - 1; i++)
        {
            if ((transform.position.z - karakterler[i].transform.position.z) <= transform.position.z - karakterler[i + 1].transform.position.z)
            {

                ilkEnYak�nMesafe = transform.position.z - karakterler[i].transform.position.z;
                enYak�nMesafe = ilkEnYak�nMesafe;
                birinci = karakterler[i].gameObject;
            }
            else if (transform.position.z - karakterler[i].transform.position.z > transform.position.z - karakterler[i + 1].transform.position.z)
            {
                enYak�nMesafe = transform.position.z - karakterler[i].transform.position.z;
                birinci = karakterler[i + 1].gameObject;
            }
        }
    }
}

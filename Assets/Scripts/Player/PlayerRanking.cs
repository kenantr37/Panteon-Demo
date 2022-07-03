using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanking : MonoBehaviour
{
    [SerializeField] List<GameObject> karakterler;
    GameObject birinci;
    float enYakınMesafe;
    float ilkEnYakınMesafe;

    void Update()
    {
        SırayıGoster();
        Debug.Log("EN YAKIN KİŞİ " + enYakınMesafe + " ile : " + birinci.name);
    }
    void SırayıGoster()
    {

        for (int i = 0; i < karakterler.Count - 1; i++)
        {
            if ((transform.position.z - karakterler[i].transform.position.z) <= transform.position.z - karakterler[i + 1].transform.position.z)
            {

                ilkEnYakınMesafe = transform.position.z - karakterler[i].transform.position.z;
                enYakınMesafe = ilkEnYakınMesafe;
                birinci = karakterler[i].gameObject;
            }
            else if (transform.position.z - karakterler[i].transform.position.z > transform.position.z - karakterler[i + 1].transform.position.z)
            {
                enYakınMesafe = transform.position.z - karakterler[i].transform.position.z;
                birinci = karakterler[i + 1].gameObject;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanking : MonoBehaviour
{
    [SerializeField] List<GameObject> karakterler;
    GameObject birinci;
    float enYakýnMesafe;
    float ilkEnYakýnMesafe;

    void Update()
    {
        SýrayýGoster();
        Debug.Log("EN YAKIN KÝÞÝ " + enYakýnMesafe + " ile : " + birinci.name);
    }
    void SýrayýGoster()
    {

        for (int i = 0; i < karakterler.Count - 1; i++)
        {
            if ((transform.position.z - karakterler[i].transform.position.z) <= transform.position.z - karakterler[i + 1].transform.position.z)
            {

                ilkEnYakýnMesafe = transform.position.z - karakterler[i].transform.position.z;
                enYakýnMesafe = ilkEnYakýnMesafe;
                birinci = karakterler[i].gameObject;
            }
            else if (transform.position.z - karakterler[i].transform.position.z > transform.position.z - karakterler[i + 1].transform.position.z)
            {
                enYakýnMesafe = transform.position.z - karakterler[i].transform.position.z;
                birinci = karakterler[i + 1].gameObject;
            }
        }
    }
}

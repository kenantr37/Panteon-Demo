using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingWayPoint : MonoBehaviour
{
    [SerializeField] List<GameObject> candidates;
    [SerializeField] int kackereDegdi = 1;
    [SerializeField] int playerSıra = 1;
    [SerializeField] TextMeshProUGUI playerRank;
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Opponent")
        {
            kackereDegdi++;
        }

        if (other.gameObject.tag == "Player")
        {
            playerSıra += kackereDegdi + 1;
            playerRank.text = "Player Rank : " + playerSıra;
        }
    }
}

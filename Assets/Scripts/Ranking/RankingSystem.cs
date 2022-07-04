using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingSystem : MonoBehaviour
{
    int counter = 1;
    PlayerMovement playerMovement;

    [SerializeField] TextMeshProUGUI playerRankText;
    void Awake()
    {
        playerMovement = GameObject.Find("Boy").GetComponent<PlayerMovement>();
    }
    void Start()
    {
        playerRankText.text = "Player Rank : " + playerMovement.PlayerCurrentRank;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "'in sýrasý : " + counter);

        if (other.gameObject.name == "Boy")
        {
            playerRankText.text = "Player Rank : " + counter.ToString();
        }
        counter++;
        if (counter == 12)
        {
            counter = 12;
        }

    }
}

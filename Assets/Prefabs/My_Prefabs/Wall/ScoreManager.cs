using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI percentageCounter;
    [SerializeField] float _initialCountNumber = 0;
    GameManager gameManager;

    public float InitialCountNumber { get { return _initialCountNumber; } set { _initialCountNumber = value; } }
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (_initialCountNumber >= 99.5f)
        {
            gameManager.RestartGame();
        }
    }

}

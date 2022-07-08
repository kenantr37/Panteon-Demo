using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public bool isGameStarted;
    public bool isGameRestarted;
    public bool isGameOver;
    [SerializeField] Button startButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;
    [SerializeField] TextMeshProUGUI rankText;

    public void StartGame()
    {
        isGameStarted = false;
        rankText.gameObject.SetActive(true);
        isGameStarted = !isGameStarted;
        startButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        StartCoroutine(IsGameOverTimer());
    }
    public void LoadGameAgain()
    {
        restartButton.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
    IEnumerator IsGameOverTimer()
    {
        yield return new WaitForSeconds(3);
        restartButton.gameObject.SetActive(true);
        isGameOver = true;
    }
}

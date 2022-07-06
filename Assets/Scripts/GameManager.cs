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
    [SerializeField] Button startButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;
    [SerializeField] TextMeshProUGUI rankText;

    public void StartGame()
    {
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
        StartCoroutine(ShowRestartButton());
    }
    public void LoadGameAgain()
    {
        restartButton.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
    IEnumerator ShowRestartButton()
    {
        yield return new WaitForSeconds(3);
        restartButton.gameObject.SetActive(true);
    }
}

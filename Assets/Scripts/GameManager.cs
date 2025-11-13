using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float gameDuration;
    public ScoreManager scoreManager;
    public MonsterSpawner spawner;

    [Header("UI Elements")] 
    public TMP_Text timerText; 
    public TMP_Text finalScoreText;
    public GameObject gameOverPanel;
    public GameObject collectionUIButton;

    private void Start()
    {
        if (spawner != null)
            spawner.canSpawn = true;
        
        if(gameOverPanel != null)
            gameOverPanel.SetActive(false);

        StartCoroutine(GameSession());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator GameSession()
    {
        float timer = gameDuration;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timerText != null)
                timerText.text = "Time: " + Mathf.Ceil(timer);
            yield return null;
        }

        EndGame();
    }
    

    void EndGame()
    {
        if(spawner != null)
            spawner.canSpawn = false;
        
        if(gameOverPanel != null)
            gameOverPanel.SetActive(true);
        collectionUIButton.SetActive(false);
        

        if (finalScoreText != null && scoreManager != null)
            finalScoreText.text = "Final Score:" + scoreManager.GetScore();

        if (scoreManager != null)
        {
            PlayerPrefs.SetInt("LastScore", scoreManager.GetScore());
            PlayerPrefs.Save();
        }
        
    }
}

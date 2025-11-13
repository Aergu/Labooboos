using UnityEngine;

public class PauseManager : MonoBehaviour
{
  public GameObject pauseMenuUI;
  private bool isPaused = false;


  public void TogglePause()
  {
    isPaused = !isPaused;
    pauseMenuUI.SetActive(isPaused);
    
    Time.timeScale = isPaused ? 0 : 1;
  }

  public void ResumeGame()
  {
    isPaused = false;
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1;
  }

  public void QuitGame()
  {
    Debug.Log("Quit Game");
    Application.Quit();
  }
}

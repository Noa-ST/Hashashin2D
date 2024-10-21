using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverDialog : Dialog
{
    public PlayerController playerController;
    public void Replay()
    {
        Show(false);

        if (playerController != null)
        {
            playerController.Setactive(true);
            playerController.CurHp = playerController.PlayerStats.hp;
        }
         
        ReloadCurrentScene();
    }

    public void BackHome()
    {
        Show(false);
        ReloadCurrentScene();
    }

    public void ExitGame()
    {
        Show(false);
        Application.Quit();
    }

    private void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

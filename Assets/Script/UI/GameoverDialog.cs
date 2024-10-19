using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverDialog : Dialog
{
    public void Replay()
    {
        Show(false);
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

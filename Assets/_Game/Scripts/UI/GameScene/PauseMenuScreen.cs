using UnityEngine;

public class PauseMenuScreen : GameScreen
{
    public void Resume()
    {
        Time.timeScale = 1;
        CloseScreen();
    }

    public void Restart()
    {
        SceneLoadManager.Instance.RestartGame();
    }

    public void GoToMenu()
    {
        SceneLoadManager.Instance.GoGameToMenu();
    }

    public void Exit()
    {
        Application.Quit();
    }
}

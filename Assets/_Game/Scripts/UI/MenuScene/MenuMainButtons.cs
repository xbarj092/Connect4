using UnityEngine;

public class MenuMainButtons : GameScreen
{
    public void PlayTheGame()
    {
        Close();
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.GameSettings);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

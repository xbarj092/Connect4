using UnityEngine;

public class MenuMainButtons : GameScreen
{
    public void PlayTheGame()
    {
        CloseScreen();
        // metoda ze tøidy ScreenEvents, která spouští otevøení screeny
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.GameSettings);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

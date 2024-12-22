using UnityEngine;

public class GameCanvasController : BaseCanvasController
{
    [SerializeField] private PauseMenuScreen _pauseMenuScreen;
    [SerializeField] private GameEndScreen _gameEndScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ScreenManager.Instance.ActiveGameScreen != null && ScreenManager.Instance.ActiveGameScreen.GameScreenType == GameScreenType.Pause)
            {
                ScreenEvents.OnGameScreenClosedInvoke(GameScreenType.Pause);
            }
            else
            {
                ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Pause);
            }
        }
    }

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.Pause => Instantiate(_pauseMenuScreen, transform),
            GameScreenType.GameEnd => Instantiate(_gameEndScreen, transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }
}

using UnityEngine;

public class MenuCanvasController : BaseCanvasController
{
    [SerializeField] private MenuMainButtons _menuMainButtonsPrefab;
    [SerializeField] private GameSettingsScreen _gameSettingsScreenPrefab;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.MenuMain => Instantiate(_menuMainButtonsPrefab, transform),
            GameScreenType.GameSettings => Instantiate(_gameSettingsScreenPrefab, transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }

    protected override GameScreenType GetActiveGameScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.GameSettings => GameScreenType.MenuMain,
            _ => base.GetActiveGameScreen(gameScreenType),
        };
    }
}

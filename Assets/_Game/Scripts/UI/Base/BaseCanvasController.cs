using System.Collections.Generic;
using UnityEngine;

public class BaseCanvasController : MonoBehaviour
{
    public GameScreen ActiveGameScreen;

    protected Dictionary<GameScreenType, GameScreen> _instantiatedScreens = new();

    private void OnEnable()
    {
        ScreenEvents.OnGameScreenOpened += ShowGameScreen;
        ScreenEvents.OnGameScreenClosed += CloseGameScreen;
    }

    private void OnDisable()
    {
        ScreenEvents.OnGameScreenOpened -= ShowGameScreen;
        ScreenEvents.OnGameScreenClosed -= CloseGameScreen;
    }

    protected void ShowGameScreen(GameScreenType gameScreenType)
    {
        if ((_instantiatedScreens.ContainsKey(gameScreenType) && _instantiatedScreens[gameScreenType] == null) ||
            !_instantiatedScreens.ContainsKey(gameScreenType))
        {
            InstantiateScreen(gameScreenType);
        }
    }

    protected void CloseGameScreen(GameScreenType gameScreenType)
    {
        if (_instantiatedScreens.ContainsKey(gameScreenType))
        {
            GameScreenType nextScreenType = GetActiveGameScreen(gameScreenType);
            if (nextScreenType != GameScreenType.None)
            {
                if (!_instantiatedScreens.TryGetValue(nextScreenType, out GameScreen existingScreen) || existingScreen == null)
                {
                    InstantiateScreen(nextScreenType);
                }
            }

            Destroy(_instantiatedScreens[gameScreenType].gameObject);
            _instantiatedScreens.Remove(gameScreenType);
        }
    }

    protected void DestroyGameScreen(GameScreenType gameScreenType)
    {
        if (_instantiatedScreens.ContainsKey(gameScreenType))
        {
            Destroy(_instantiatedScreens[gameScreenType].gameObject);
            _instantiatedScreens.Remove(gameScreenType);
        }
    }

    private void InstantiateScreen(GameScreenType gameScreenType)
    {
        GameScreen screenInstance = GetRelevantScreen(gameScreenType);
        InstantiateScreen(screenInstance);
    }

    private void InstantiateScreen(GameScreen screenInstance)
    {
        if (screenInstance != null)
        {
            _instantiatedScreens[screenInstance.GameScreenType] = screenInstance;
            ActiveGameScreen = screenInstance;
        }
    }

    protected virtual GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            _ => null
        };
    }

    protected virtual GameScreenType GetActiveGameScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            _ => GameScreenType.None
        };
    }
}

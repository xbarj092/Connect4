using UnityEngine;

// tøída GameScreen, která dìdí z MonoBehaviour -> mùže se používat jako komponenta na herních objektech
public class GameScreen : MonoBehaviour
{
    // typ obrazovky
    public GameScreenType GameScreenType;

    // metoda CloseScreen, která zavírá herní obrazovku pomocí zavolání statického eventu v tøídì ScreenEvents
    public void CloseScreen()
    {
        ScreenEvents.OnGameScreenClosedInvoke(GameScreenType);
    }
}

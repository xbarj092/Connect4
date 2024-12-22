using TMPro;
using UnityEngine;

public class GameEndScreen : GameScreen
{
    [SerializeField] private TMP_Text _playerWonText;

    private const string PLAYER_WON_TEXT_SUFFIX = " won!";
    private const string TIE_TEXT_SUFFIX = "Its a tie! No free fields left!";

    private void Awake()
    {
        if (GameManager.Instance.IsTie)
        {
            _playerWonText.text = TIE_TEXT_SUFFIX;
        }
        else
        {
            _playerWonText.text = (GameManager.Instance.PlayerOneWon ? GameManager.Instance.PlayerOneName : GameManager.Instance.PlayerTwoName) + PLAYER_WON_TEXT_SUFFIX;
        }
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

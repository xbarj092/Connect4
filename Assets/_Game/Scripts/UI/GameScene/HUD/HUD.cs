using TMPro;
using UnityEngine;

public class HUD : GameScreen
{
    [SerializeField] private TMP_Text _playerOneName;
    [SerializeField] private TMP_Text _playerTwoName;

    private void Start()
    {
        _playerOneName.text = GameManager.Instance.PlayerOneName;
        _playerTwoName.text = GameManager.Instance.PlayerTwoName;
    }
}

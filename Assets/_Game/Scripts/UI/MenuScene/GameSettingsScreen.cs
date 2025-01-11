using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class GameSettingsScreen : GameScreen
{
    [SerializeField] private int _shortPlayerNameThreshold;
    [SerializeField] private int _longPlayerNameThreshold;

    [SerializeField] private TMP_InputField _playerOneName;
    [SerializeField] private TMP_InputField _playerTwoName;

    [SerializeField] private TMP_Text _errorText;

    private const string ERROR_EMPTY_PLAYER_ONE_NAME = "Please fill out player one name!";
    private const string ERROR_EMPTY_PLAYER_TWO_NAME = "Please fill out player two name!";
    private const string ERROR_SHORT_PLAYER_ONE_NAME = "Player one name is too short!";
    private const string ERROR_SHORT_PLAYER_TWO_NAME = "Player two name is too short!";
    private const string ERROR_LONG_PLAYER_ONE_NAME = "Player one name is too long!";
    private const string ERROR_LONG_PLAYER_TWO_NAME = "Player two name is too long!";
    private const string ERROR_SAME_PLAYER_NAMES = "Players cannot have same names!";

    public void SaveAndPlay()
    {
        if (HandleErrorMessages())
        {
            return;
        }

        SaveGameSettings();
        SceneLoadManager.Instance.GoMenuToGame();
    }

    private void SaveGameSettings()
    {
        GameManager.Instance.PlayerOneName = _playerOneName.text;
        GameManager.Instance.PlayerTwoName = _playerTwoName.text;
    }

    private bool HandleErrorMessages()
    {
        Dictionary<Func<bool>, string> validations = new()
        {
            { () => string.IsNullOrEmpty(_playerOneName.text), ERROR_EMPTY_PLAYER_ONE_NAME },
            { () => string.IsNullOrEmpty(_playerTwoName.text), ERROR_EMPTY_PLAYER_TWO_NAME },
            { () => _playerOneName.text.Length < _shortPlayerNameThreshold, ERROR_SHORT_PLAYER_ONE_NAME },
            { () => _playerTwoName.text.Length < _shortPlayerNameThreshold, ERROR_SHORT_PLAYER_TWO_NAME },
            { () => _playerOneName.text.Length > _longPlayerNameThreshold, ERROR_LONG_PLAYER_ONE_NAME },
            { () => _playerTwoName.text.Length > _longPlayerNameThreshold, ERROR_LONG_PLAYER_TWO_NAME },
            { () =>  string.Equals(_playerOneName.text, _playerTwoName.text), ERROR_SAME_PLAYER_NAMES },
        };

        foreach (KeyValuePair<Func<bool>, string> validation in validations)
        {
            if (validation.Key())
            {
                ShowErrorText(validation.Value);
                return true;
            }
        }

        HideErrorText();
        return false;
    }

    private void ShowErrorText(string errorText)
    {
        _errorText.gameObject.SetActive(true);
        _errorText.text = errorText;
    }

    private void HideErrorText()
    {
        _errorText.gameObject.SetActive(false);
    }
}

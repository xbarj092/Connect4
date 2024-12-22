using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsScreen : GameScreen
{
    [Header("Variables")]
    [SerializeField] private int _shortPlayerNameThreshold;
    [SerializeField] private int _longPlayerNameThreshold;

    [Header("Properties")]
    [SerializeField] private TMP_Text _rowCountText;
    [SerializeField] private TMP_Text _columnCountText;

    [SerializeField] private Slider _rowCount;
    [SerializeField] private Slider _columnCount;

    [SerializeField] private TMP_InputField _playerOneName;
    [SerializeField] private TMP_InputField _playerTwoName;

    [SerializeField] private TMP_Text _errorText;

    private const string ROW_COUNT_TEXT_PREFIX = "Row count: ";
    private const string COLUMN_COUNT_TEXT_PREFIX = "Column count: ";

    private const string ERROR_EMPTY_PLAYER_ONE_NAME = "Please fill out player one name!";
    private const string ERROR_EMPTY_PLAYER_TWO_NAME = "Please fill out player two name!";
    private const string ERROR_SHORT_PLAYER_ONE_NAME = "Player one name is too short!";
    private const string ERROR_SHORT_PLAYER_TWO_NAME = "Player two name is too short!";
    private const string ERROR_LONG_PLAYER_ONE_NAME = "Player one name is too long!";
    private const string ERROR_LONG_PLAYER_TWO_NAME = "Player two name is too long!";
    private const string ERROR_SAME_PLAYER_NAMES = "Players cannot have same names!";

    public void OnRowCountChanged()
    {
        _rowCountText.text = ROW_COUNT_TEXT_PREFIX + _rowCount.value;
    }

    public void OnColumnCountChanged()
    {
        _columnCountText.text = COLUMN_COUNT_TEXT_PREFIX + _columnCount.value;
    }

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
        GameManager.Instance.RowCount = Mathf.RoundToInt(_rowCount.value);
        GameManager.Instance.ColumnCount = Mathf.RoundToInt(_columnCount.value);

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

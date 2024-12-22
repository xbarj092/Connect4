public class GameManager : MonoSingleton<GameManager>
{
    public int RowCount;
    public int ColumnCount;

    public string PlayerOneName;
    public string PlayerTwoName;

    public bool PlayerOneWon;
    public bool IsTie;

    public bool IsRedTurn { get; private set; } = true;

    public void SwitchTurn()
    {
        IsRedTurn = !IsRedTurn;
    }

    public void Restart()
    {
        PlayerOneWon = false;
        IsTie = false;
        IsRedTurn = true;
    }
}

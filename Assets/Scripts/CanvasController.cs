using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Canvas CanvasWinAndLose;
    [SerializeField] GameObject WinPlayer1;
    [SerializeField] GameObject WinPlayer2;
    [SerializeField] GameObject Draw;

    public void ShowWinPlayer1()
    {
        CanvasWinAndLose.gameObject.SetActive(true);
        WinPlayer1.SetActive(true);
        WinPlayer2.SetActive(false);
        Draw.SetActive(false);
    }

    public void ShowWinPlayer2()
    {
        CanvasWinAndLose.gameObject.SetActive(true);
        WinPlayer1.SetActive(false);
        WinPlayer2.SetActive(true);
        Draw.SetActive(false);
    }

    public void ShowDraw()
    {
        CanvasWinAndLose.gameObject.SetActive(true);
        WinPlayer1.SetActive(false);
        WinPlayer2.SetActive(false);
        Draw.SetActive(true);
    }
}

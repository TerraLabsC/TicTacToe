using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class Game : MonoBehaviour
{ 
   public GameObject[] crosses = new GameObject[9];
    public GameObject[] zeros = new GameObject[9];
    public GameObject redline, blueline, line = null;

    [Header("Подсветка игрока")]
    public ColorTween ImagePlayer1;
    public ColorTween ImagePlayer2;
    
    [Header("Иконка игрока")]
    public ColorTween IconPlayer1;
    public ColorTween IconPlayer2;

    [Header("Текст игроков")]     
    public ColorTween textPlayer1;
    public ColorTween textPlayer2;

    [Header("Ход игроков")]     
    public ColorTween ImageMovePlayer1;
    public ColorTween ImageMovePlayer2;
    
    [Header("Настройки цветов")]
    public Color inactiveColor = new Color(1, 1, 1, 0.5f);

    [Header("Таймер")]
    public float timeLimit = 120f;
    private float currentTime;
    public TextMeshProUGUI textTimer;

    public Color crossesColor, zerosColor, ObichniiColor;
    public int[] moves = new int[9];
    public int steps; 
    
    private bool isCrossTurn;

    [Header("Контроллер UI")]
    [SerializeField] private CanvasController canvasController;

    void Start()
    {
        currentTime = timeLimit;

        isCrossTurn = false;
        UpdateHighlight();
        RedactText("ИГРА НАЧАЛАСЬ");
    }

    void Update()
    {
        if (steps < 9 && !redline.activeSelf && !blueline.activeSelf)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                if (currentTime < 0) currentTime = 0;
                UpdateTimerText();
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = (int)currentTime / 60;
        int seconds = (int)currentTime % 60;
        textTimer.text = $"{minutes:00}:{seconds:00}";
    }

    void UpdateHighlight()
    {
        if (isCrossTurn)
        {
            // Игрок 1 (Крестики) активен
            ImagePlayer1.ChangeColor();
            textPlayer1.ChangeColor();
            IconPlayer1.ChangeColor();
            ImageMovePlayer1.ChangeColor();

            // Игрок 2 (Нолики) неактивен
            ImagePlayer2.ResetColor();
            textPlayer2.ResetColor();
            IconPlayer2.ResetColor();
            ImageMovePlayer2.ResetColor();
        }
        else
        {
            // Игрок 1 (Крестики) неактивен
            ImagePlayer1.ResetColor();
            textPlayer1.ResetColor();
            IconPlayer1.ResetColor();
            ImageMovePlayer1.ResetColor();

            // Игрок 2 (Нолики) активен
            ImagePlayer2.ChangeColor();
            textPlayer2.ChangeColor();
            IconPlayer2.ChangeColor();
            ImageMovePlayer2.ChangeColor();
        }
    }

    void SwitchTurn()
    {
        isCrossTurn = !isCrossTurn;
        UpdateHighlight();
        RedactText(isCrossTurn ? "ХОД КРЕСТИКОВ" : "ХОД НОЛИКОВ");
    }

    public void Move(int index)
    {
        if (moves[index] != 0 || redline.activeSelf || blueline.activeSelf) return;

        if (isCrossTurn)
        {
            moves[index] = 2; 
            crosses[index].SetActive(true);
        }
        else
        {
            moves[index] = 1; 
            zeros[index].SetActive(true);
        }

        steps++;
        Check();

        if (steps < 9 && !redline.activeSelf && !blueline.activeSelf)
        {
            SwitchTurn();
        }
        else if (steps == 9 && !redline.activeSelf)
        {
            RedactText("\nНИЧЬЯ!");
            canvasController.ShowDraw();
        }
    }

    void RedactText(string txt)
    {
        if (txt == "\nНИЧЬЯ!")
        {
            Debug.Log("Ничья!");
        }
        else if (isCrossTurn)
        {
            Debug.Log("Ход крестиков " + txt);
        }
        else
        {
            Debug.Log("Ход ноликов " + txt);
        }
    }

    void Check()
    {
        for (int i = 1; i < 3; i++)
        {
            if (moves[0] == i && moves[1] == i && moves[2] == i) End(0);
            else if (moves[3] == i && moves[4] == i && moves[5] == i) End(1);
            else if (moves[6] == i && moves[7] == i && moves[8] == i) End(2);
            else if (moves[0] == i && moves[3] == i && moves[6] == i) End(3);
            else if (moves[1] == i && moves[4] == i && moves[7] == i) End(4);
            else if (moves[2] == i && moves[5] == i && moves[8] == i) End(5);
            else if (moves[0] == i && moves[4] == i && moves[8] == i) End(6);
            else if (moves[2] == i && moves[4] == i && moves[6] == i) End(7);
        }
    }

    void End(int numVariant)
    {
        steps = 10;
        line = isCrossTurn ? redline : blueline;
        RedactText(isCrossTurn ? "КРЕСТИКИ ПОБЕДИЛИ!" : "НОЛИКИ ПОБЕДИЛИ!");

        if (isCrossTurn)
        {
           canvasController.ShowWinPlayer2();
        }
        else
        {
            canvasController.ShowWinPlayer1();
        }

        line.SetActive(true);
        switch (numVariant)
        {
            case 0: line.transform.rotation = Quaternion.Euler(0, 0, 90); line.transform.position = crosses[1].transform.position; break;
            case 1: line.transform.rotation = Quaternion.Euler(0, 0, 90); line.transform.position = crosses[4].transform.position; break;
            case 2: line.transform.rotation = Quaternion.Euler(0, 0, 90); line.transform.position = crosses[7].transform.position; break;
            case 3: line.transform.rotation = Quaternion.Euler(0, 0, 0);   line.transform.position = crosses[3].transform.position; break;
            case 4: line.transform.rotation = Quaternion.Euler(0, 0, 0);   line.transform.position = crosses[4].transform.position; break;
            case 5: line.transform.rotation = Quaternion.Euler(0, 0, 0);   line.transform.position = crosses[5].transform.position; break;
            case 6: line.transform.rotation = Quaternion.Euler(0, 0, 45);  line.transform.position = crosses[4].transform.position; break;
            case 7: line.transform.rotation = Quaternion.Euler(0, 0, -45); line.transform.position = crosses[4].transform.position; break;
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{ 
    public GameObject[] crosses = new GameObject[9];
    public GameObject[] zeros = new GameObject[9];

    public GameObject redline, blueline, line = null;

    public TextMeshProUGUI text;

    public Color crossesColor, zerosColor, ObichniiColor;

    public int[] moves = new int[9]; //0 - НИЧЕГО    1 - НОЛИКИ    2 - КРЕСТИКИ

    public int steps, a, b; //a - куда будет бить противник    b - куда будем бить мы

    public float time_for_reflection;

    bool _isFirst, _isYourRound;

    void Start()
    {
        time_for_reflection = Random.Range(1f, 3f);
        if (Random.Range(0f, 1f) <= 0.5f)
            _isFirst = _isYourRound = false;
        else
            _isFirst = _isYourRound = true;
    }

    void Update()
    {
        if (!_isYourRound && steps < 9 && !redline.activeSelf && !blueline.activeSelf)
        {
            RedactText("ХОД\nПРОТИВНИКА");
            if (time_for_reflection <= 0f)
            {
                do
                {
                    a = Random.Range(0, 9);
                } while (moves[a] != 0);
                Move();
            }
            else
                time_for_reflection -= Time.deltaTime;
        }
        else if (_isYourRound && steps < 9 && !redline.activeSelf && !blueline.activeSelf)
        {
            RedactText("ТВОЙ ХОД");
        }
        else if (steps == 9 && !redline.activeSelf && text.text != "\n\n\n")
        {
            RedactText("\n\n\n!!!");
        }
        else if ((redline.activeSelf && redline.GetComponent<SpriteRenderer>().sprite.name == "37") || (blueline.activeSelf && blueline.GetComponent<SpriteRenderer>().sprite.name == "36" || text.text == "\nНИЧЬЯ!"))
        {
            Restart();
        }
    }

    void RedactText(string txt)
    {
        text.text = txt;
        if (txt == "\nНИЧЬЯ!")
        {
            text.color = ObichniiColor;
        }
        else if (_isFirst && _isYourRound || !_isFirst && !_isYourRound)
        {
            text.color = crossesColor;
        }
        else
        {
            text.color = zerosColor;
        }
    }

    public void Move()
    {
        if (_isFirst && _isYourRound)
        {
            moves[b] = 2;
            crosses[b].SetActive(true);
        }
        else if (_isFirst && !_isYourRound)
        {
            moves[a] = 1;
            zeros[a].SetActive(true);
        }
        else if (!_isFirst && _isYourRound)
        {
            moves[b] = 1;
            zeros[b].SetActive(true);
        }
        else
        {
            moves[a] = 2;
            crosses[a].SetActive(true);
        }
        steps++;
        Check();
        _isYourRound = !_isYourRound;
    }

    void Check()
    {
        for (int i = 1; i < 3; i++)
        {
            if (moves[0] == i && moves[1] == i && moves[2] == i)
                End(0);
            else if (moves[3] == i && moves[4] == i && moves[5] == i)
                End(1);
            else if (moves[6] == i && moves[7] == i && moves[8] == i)
                End(2);
            else if (moves[0] == i && moves[3] == i && moves[6] == i)
                End(3);
            else if (moves[1] == i && moves[4] == i && moves[7] == i)
                End(4);
            else if (moves[2] == i && moves[5] == i && moves[8] == i)
                End(5);
            else if (moves[0] == i && moves[4] == i && moves[8] == i)
                End(6);
            else if (moves[2] == i && moves[4] == i && moves[6] == i)
                End(7);
        }
    }

    void End(int numVariant)
    {
        steps = 10;
        if (_isFirst && _isYourRound || !_isFirst && !_isYourRound)
            line = redline;
        else
            line = blueline;
        
        if (_isYourRound)
            RedactText("ТЫ ПОБЕДИЛ!");
        else
            RedactText("ТЫ ПРОИГРАЛ!");
        
        line.SetActive(true);

        switch (numVariant)
        {
            case 0:
                line.transform.rotation = Quaternion.Euler(0, 0, 90);
                line.transform.position = crosses[1].transform.position;
                break;
            case 1:
                line.transform.rotation = Quaternion.Euler(0, 0, 90);
                line.transform.position = crosses[4].transform.position;
                break;
            case 2:
                line.transform.rotation = Quaternion.Euler(0, 0, 90);
                line.transform.position = crosses[7].transform.position;
                break;
            case 3:
                line.transform.rotation = Quaternion.Euler(0, 0, 0);
                line.transform.position = crosses[3].transform.position;
                break;
            case 4:
                line.transform.rotation = Quaternion.Euler(0, 0, 0);
                line.transform.position = crosses[4].transform.position;
                break;

            case 5:
                line.transform.rotation = Quaternion.Euler(0, 0, 0);
                line.transform.position = crosses[5].transform.position;
                break;

            case 6:
                line.transform.rotation = Quaternion.Euler(0, 0, 45);
                line.transform.position = crosses[4].transform.position;
                break;

            case 7:
                line.transform.rotation = Quaternion.Euler(0, 0, -45);
                line.transform.position = crosses[4].transform.position;
                break;
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

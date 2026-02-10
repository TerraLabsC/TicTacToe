using UnityEngine;

public class Click : MonoBehaviour
{
    GameObject GameControl;

    void Start()
    {
        GameControl = GameObject.Find("GameControl");
    }

    private void OnMouseDown()
    {
        if (GameControl.GetComponent<Game>().moves[int.Parse(name)] == 0)
        {
            GameControl.GetComponent<Game>().b = int.Parse(name);
            GameControl.GetComponent<Game>().Move();
            GameControl.GetComponent<Game>().time_for_reflection = Random.Range(1f, 3f);
        }
    }
}

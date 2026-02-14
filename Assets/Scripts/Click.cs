using UnityEngine;

public class Click : MonoBehaviour
{
    private GameObject GameControl;
    private Game gameScript;

    void Start()
    {
        GameControl = GameObject.Find("GameControl");

        if (GameControl != null)
        {
            gameScript = GameControl.GetComponent<Game>();
        }
    }

    private void OnMouseDown()
    {
        if (gameScript != null)
        {
            int index = int.Parse(name);
            
            gameScript.Move(index);
        }
    }
}

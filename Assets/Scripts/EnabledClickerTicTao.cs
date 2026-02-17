using System.Collections.Generic;
using UnityEngine;

public class EnabledClickerTicTao : MonoBehaviour
{
    [SerializeField] private List<Click> clicks;

    public void ClicksEnabled()
    {
       Invoke("TimerEnabled", 0.5f);
    }

    public void TimerEnabled()
    {
        foreach (var click in clicks)
        {
            click.enabled = true;
        } 
    }

    public void ClicksOff()
    {
        foreach (var click in clicks)
        {
            click.enabled = false;
        }
    }
}

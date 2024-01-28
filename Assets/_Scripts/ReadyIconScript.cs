using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyIconScript : MonoBehaviour
{
    [SerializeField] GameObject objectToToggleOff;
    [SerializeField] GameObject objectToToggleOn;
    public bool ready = false;

    public void Ready()
    {

        objectToToggleOff.SetActive(false);
        objectToToggleOn.SetActive(true);

        ready = true;
        print("READYSCRIPT READY");
    }

    public void UnReady()
    {
        objectToToggleOff.SetActive(true);
        objectToToggleOn.SetActive(false);

        ready = false;
        print("READYSCRIPT UNREADY");
    }
}

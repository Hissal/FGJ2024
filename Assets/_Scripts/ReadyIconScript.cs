using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyIconScript : MonoBehaviour
{
    [SerializeField] List<GameObject> objectsToToggle;
    public bool ready = false;

    public void Ready()
    {
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(false);
        }
        ready = true;
        print("READYSCRIPT READY");
    }

    public void UnReady()
    {
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(true);
        }
        ready = false;
        print("READYSCRIPT UNREADY");
    }
}

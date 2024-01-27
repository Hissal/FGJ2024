using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{
    List<Transform> collidingWith = new List<Transform>();

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == this.gameObject) return;
        if (other == null) return;

        collidingWith.Add(other.transform);
    }

    public List<Transform> GetPunched()
    {
        return collidingWith;
    }

    public void ClearPunched()
    {
        collidingWith.Clear();
    }
}

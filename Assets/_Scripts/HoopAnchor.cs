using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopAnchor : MonoBehaviour
{
    private Transform tryingPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StorePlayer(Transform player)
    {
        Debug.Log("Storing player!");
        tryingPlayer = player;
    }
    public Transform GetTryingPlayer()
    {
        return tryingPlayer;
    }

    public void DetachHoop()
    {
        // Get a reference to the RingLeader script in the parent object
        Debug.Log(transform.parent);
        Ringleader ringLeader = transform.parent.GetComponent<Ringleader>();

        // If the RingLeader script exists
        if (ringLeader != null)
        {
            // Call the DetachHoops method
            ringLeader.DetachHoops();
        }
        FixedJoint joint = GetComponent<FixedJoint>();
    }
}
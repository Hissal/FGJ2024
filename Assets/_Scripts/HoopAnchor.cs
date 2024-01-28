using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopAnchor : MonoBehaviour
{
    private Transform tryingPlayer;
    // Start is called before the first frame update
    void Start()
    {
        ringleader = transform.parent.GetComponent<Ringleader>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void DetachHoop()
    {
        ringleader.hoopCount -= 1;
        Debug.Log("Detaching hoop!");
        // Assign the hoop to the "DetachedHoop" layer
        Transform hoopChild = transform.Find("Hoop");
        StartCoroutine(SetLayerAfterDelay(hoopChild.gameObject, "DetachedHoop", 0.15f));

        transform.parent = null;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        // Find the nested bone child
        Transform boneChild = transform.Find("Chain (1)/Armature/Bone.012/Bone.011");
        if (boneChild != null)
        {
            // Get the Rigidbody component of the bone child
            Rigidbody boneRb = boneChild.GetComponent<Rigidbody>();
            if (boneRb != null)
            {
                // Set the bone child to kinematic
                boneRb.isKinematic = true;
            }

            // Raise the Y height of the bone child by 30
            boneChild.position = new Vector3(boneChild.position.x, boneChild.position.y + 2, boneChild.position.z);
        }
        else
        {
            Debug.LogError("Bone child not found");
        }
        Destroy(gameObject, 1f);

    }
    private IEnumerator SetLayerAfterDelay(GameObject obj, string layerName, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Set the layer
        obj.layer = LayerMask.NameToLayer(layerName);
    }
}
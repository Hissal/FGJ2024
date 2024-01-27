using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    void Hit(Vector3 velocity, float forceMultiplier);
}

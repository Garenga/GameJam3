using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ConstantForce))]
public class GravityDirection : MonoBehaviour
{
    ConstantForce gravityDirection;
    Rigidbody objectRigibody;

    void Start()
    {
        gravityDirection = GetComponent<ConstantForce>();
        objectRigibody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        Events.OnGravityReverse+=GravityChange;
    }

    void OnDisable()
    {
        Events.OnGravityReverse-=GravityChange;
    }

    void GravityChange(Vector3 gDirection)
    {
        objectRigibody.useGravity = false;
        gravityDirection.force = gDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        objectRigibody.useGravity = true;
    }
}

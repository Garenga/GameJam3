using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewind : MonoBehaviour
{
    [SerializeField]List<Vector3> positions;
    [SerializeField] List<Quaternion> rotations;

    [SerializeField] bool isRewinding;

    Rigidbody objectRigidbody;

    private void OnEnable()
    {
        Events.OnTimeRewind += StartRewind;


    }
    private void OnDisable()
    {
        Events.OnTimeRewind -= StartRewind;

    }

    private void Start()
    {
        positions = new List<Vector3>();
        rotations = new List<Quaternion>();

        objectRigidbody = GetComponent<Rigidbody>();
    }


    void StartRewind(bool rewind)
    {
        isRewinding = rewind;
        objectRigidbody.isKinematic = true;
    }


    void StopRewind()
    {
        isRewinding = false;
        objectRigidbody.isKinematic = false;
    }

    private void FixedUpdate()
    {

        if (isRewinding)
        {
            Rewind();
        }

        else
        {
            Record();
        }
    }
     void Rewind()
    {
        if (positions.Count > 0 && isRewinding)
        {
            transform.position = positions[0];
            transform.rotation = rotations[0];

            rotations.RemoveAt(0);
            positions.RemoveAt(0);
        }
        else
        {
            StopRewind();
            //objectRigidbody.isKinematic = true;
        }
    }

    void Record()
    {
        objectRigidbody.isKinematic = false;
        if (!objectRigidbody.IsSleeping())
        {
            positions.Insert(0, transform.position);
            rotations.Insert(0, transform.rotation);
        }
    }


}

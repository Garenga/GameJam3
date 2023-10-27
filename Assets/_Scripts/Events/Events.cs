using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Events 
{
    public static UnityAction<Vector3> OnGravityReverse;
    public static UnityAction<bool> OnTimeRewind;

    public static UnityAction OnTimeStart;

    public static UnityAction<Vector3,bool> OnSpaceChange;


}

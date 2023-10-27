using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSpace : Ability
{
    [SerializeField] Vector3 objectScale;
    public override void ActivateAbility(bool active)
    {
        Debug.Log("Ability Used");
        Events.OnSpaceChange.Invoke(objectScale, active);
    }
}

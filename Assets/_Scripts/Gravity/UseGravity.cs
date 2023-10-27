using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UseGravity : Ability
{
    [SerializeField] Vector3 gravityDirection;

    public override void ActivateAbility(bool active)
    {
        Events.OnGravityReverse.Invoke(gravityDirection);
        gravityDirection *= -1;
    }
}

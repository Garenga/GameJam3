using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseTime : Ability
{
    public override void ActivateAbility(bool active)
    {
        Events.OnTimeRewind.Invoke(active);
    }
}

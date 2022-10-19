using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapone : MonoBehaviour
{
    public enum EType { NONE = -1, MELEE, RANGE, SUPPORT }
    public EType type = EType.NONE;
    public float damage = 0;
    public float attackspeed = 0;
    public float knockback = 0;

    public int ammo = 0;
    public int currentammo = 0;

}


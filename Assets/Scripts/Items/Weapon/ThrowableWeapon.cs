using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon/Throwable Weapon")]
public class ThrowableWeapon : Weapon
{
    [Space(15)]
    [Tooltip("The time after which the grenade will explode")]
    public float triggerTime;
    [Space]
    public float explosionRadius;
}

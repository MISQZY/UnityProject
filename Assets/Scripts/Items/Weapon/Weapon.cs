using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon/Base Weapon")]
public class Weapon : Item
{    
    public enum WeaponType
        {
            AR,
            Pistol,
            Mellee,
            Grenade,
            Utilities
        }


    public WeaponType weaponType;

    [Space]
    public int damage;

}

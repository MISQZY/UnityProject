using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon/Shooting Weapon")]
public class ShootingWeapon : Weapon
{

    [Space(15)]
    [Header("Base")]
    public float fireRate;

    [Header("Ammo")]
    public int magazineSize;
    public int magazineCount;

    [Header("Reload")]
    public float reloadTime;

    [Space(15)]
    public List<AllowedAttachments> allowedAttachments = new List<AllowedAttachments>();

}

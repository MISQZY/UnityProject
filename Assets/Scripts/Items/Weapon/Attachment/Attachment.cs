using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseAttachment", menuName = "Items/Weapon/Attachment/BaseAttachment")]
public class Attachment : Item
{
    public enum AttachmentType
    {
        Grip,
        Barrel,
        Muzzle,
        Stock,
        Scoope,
    }

    public AttachmentType attachmentType;

    float fireRateBonus;
}

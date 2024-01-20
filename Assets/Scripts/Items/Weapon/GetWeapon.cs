using System;
using System.Net.Mail;
using UnityEngine;

public class GetWeapon : MonoBehaviour
{

    public static Action<Weapon> onGunChange;


    public void SetWeapon(Weapon weapon)
    {

        if(transform.childCount == 0)
        {
            SpawnWeapon(weapon);
        }

        if (transform.GetChild(0) != weapon.prefab && transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            SpawnWeapon(weapon);
        }

        onGunChange?.Invoke(weapon);

    }

    private void SpawnWeapon(Weapon weapon)
    {
        Transform weaponTransform = Instantiate(weapon.prefab);

        weaponTransform.parent = transform;
        weaponTransform.localEulerAngles = Vector3.zero;
        weaponTransform.localPosition = Vector3.zero;
    }


    public void SpawnAttachment(AttachmentInfo attachment)
    {
        if (attachment != null)
        {
            Transform newParrentObj = transform.Find(attachment.attachment.attachmentType.ToString());

            if (newParrentObj == null)
            {
                newParrentObj = new GameObject(attachment.attachment.attachmentType.ToString()).transform;
                newParrentObj.transform.parent = transform;
                newParrentObj.transform.localEulerAngles = Vector3.zero;
                newParrentObj.transform.localPosition = attachment.spawnPosition;
            }
            else
            {
                for (int i = 0; i < newParrentObj.childCount; i++)
                {
                    Destroy(newParrentObj.GetChild(i).gameObject);
                }
            }


            GameObject attachmentInstance = Instantiate(attachment.attachment.prefab.gameObject, newParrentObj);

            attachmentInstance.transform.localEulerAngles = Vector3.zero;
            attachmentInstance.transform.localPosition = Vector3.zero;
            attachmentInstance.transform.SetParent(newParrentObj.transform);
        }

    }

    public void DestroyAttachment(Attachment.AttachmentType type)
    {
        Transform parrentObj = transform.Find(type.ToString());
        if (parrentObj != null)
        {
            foreach(Transform child in parrentObj.GetComponentsInChildren<Transform>())
            {
                Destroy(child.gameObject);
            }
        }
    }
}

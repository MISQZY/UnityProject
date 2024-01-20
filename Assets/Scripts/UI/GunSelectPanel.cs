using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AttachmentInfo
{
    public Attachment attachment;
    public Vector3 spawnPosition;
}

[System.Serializable]
public class AllowedAttachments
{
    public Attachment.AttachmentType type;
    public Vector3 buttonPosition;
    public List<AttachmentInfo> attachments;
}

[System.Serializable]
public class Weapons
{
    public Weapon.WeaponType type;
    public List<Weapon> weapons;
}

[System.Serializable]
public class WeaponsTypeList
{
    public List<Weapons> weaponsList;
}

public class GunSelectPanel : MonoBehaviour
{
    [SerializeField] GameObject gunTypeTab;
    [SerializeField] GameObject gunTypeTabContent;

    [SerializeField] GameObject gunListTab;
    [SerializeField] GameObject gunListTabContent;

    [SerializeField] Transform spawnPoint;

    [SerializeField] Button typeButtonPrefab;
    [SerializeField] Button weaponButtonPrefab;

    public WeaponsTypeList weaponTypeList = new WeaponsTypeList();

    private Button previousTypeButton;
    private Button previousListButton;


    private void Start()
    {
        AddGunTypePanel();
    }

    private void AddGunTypePanel()
    {
        gunListTab.SetActive(false);

        string[] gunNameList = Enum.GetNames(typeof(Weapon.WeaponType));

        foreach (string type in gunNameList)
        {
            Button _newButton = Instantiate(typeButtonPrefab, gunTypeTabContent.transform);
            _newButton.name = type + " Button";

            _newButton.GetComponentInChildren<TMP_Text>().text = type;

            AddGunTypeButtonListener(_newButton, type);
        }
    }

    private void AddGunTypeButtonListener(Button button, string type)
    {
        button.onClick.AddListener(() =>
        {
            if(previousTypeButton != null)
            {
                previousTypeButton.interactable = true;
            }

            previousTypeButton = button;
            button.interactable = false;

            ClearTab(gunListTabContent.transform);
            gunListTab.SetActive(false);

            foreach (Weapons weaponsList in weaponTypeList.weaponsList)
            {
                //TODO: Переделать проверку не по первому элементу в списке,а по общему типу всех элементов.

                if (weaponsList.type.ToString() == type)
                {
                    OpenGunListByType(weaponsList);
                }
            }
        });
    }

    private void OpenGunListByType(Weapons weaponsList)
    {
        foreach (Weapon weapon in weaponsList.weapons)
        {
            Button _newButton = Instantiate(weaponButtonPrefab, gunListTabContent.transform);
            _newButton.name = weapon.name + " Button";
            _newButton.GetComponentInChildren<TMP_Text>().text = weapon.name;
            for (int i = 0; i < _newButton.transform.childCount; i++)
            {
                if(_newButton.transform.GetChild(i).name == "Icon")
                {
                    _newButton.transform.GetChild(i).GetComponent<Image>().sprite = weapon.icon;
                }
            }

            AddGunListButtonListener(_newButton, weapon);
        }

        gunListTab.SetActive(true);
    }

    private void AddGunListButtonListener(Button button, Weapon weapon)
    {
        button.onClick.AddListener(() =>
        {
            if(previousListButton != null)
            {
                previousListButton.interactable = true;
            }
            previousListButton = button;
            button.interactable = false;

            spawnPoint.GetComponent<GetWeapon>().SetWeapon(weapon);

        });
    }

    private void ClearTab(Transform contentTab)
    {
        if (contentTab.childCount > 0)
        {
            foreach (Transform child in contentTab)
            {
                Destroy(child.gameObject);
            }
        }
    }



}

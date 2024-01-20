using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GunSelectPanel))]
public class WeaponsTypeListEditor : Editor
{

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        GunSelectPanel panel = (GunSelectPanel)target;



        EditorGUI.BeginChangeCheck();


        CheckWeaponTypeMatch(panel);
        CheckEmptyList(panel);
        CheckAlreadyExistCategory(panel);



        EditorGUI.EndChangeCheck();


/*        if (showNotification)
        {
            EditorGUILayout.HelpBox("Проверка условий выполнена.", MessageType.Info);
        }*/
    }





    private void CheckWeaponTypeMatch(GunSelectPanel panel)
    {
        List<Weapon> weaponsToRemove = new List<Weapon>();

        foreach (Weapons weaponsList in panel.weaponTypeList.weaponsList)
        {
            foreach (Weapon weapon in weaponsList.weapons)
            {
                if (weapon != null)
                {
                    if (weapon.weaponType != weaponsList.type)
                    {
                        weaponsToRemove.Add(weapon);
                        Debug.LogError("Объект не совпадает с типом категории, категория: " 
                            + weaponsList.type + " объект: " + weapon.name + " -  " + weapon.weaponType);
/*                        HelpBox("Объект не совпадает с типом категории, категория: " + weaponsList.type + " объект: " + weapon.name,
                            MessageType.Error);*/
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("Присутствуют None поля в категории: " + weaponsList.type, MessageType.Error);
                }
            }
            foreach (Weapon weaponToRemove in weaponsToRemove)
            {
                weaponsList.weapons.Remove(weaponToRemove);
            }
        }

    }

    private void CheckEmptyList(GunSelectPanel panel)
    {
        foreach (Weapons weaponsList in panel.weaponTypeList.weaponsList)
        {
            if (weaponsList.weapons.Count == 0)
            {
                EditorGUILayout.HelpBox("Пустая категория: " + weaponsList.type,
                MessageType.Error);
            }
        }
    }

    private void CheckAlreadyExistCategory(GunSelectPanel panel)
    {
        List<Weapon.WeaponType> existsCategory = new List<Weapon.WeaponType>();
        foreach (Weapons weaponsList in panel.weaponTypeList.weaponsList)
        {
            if(!existsCategory.Contains(weaponsList.type))
            {
                existsCategory.Add(weaponsList.type);
            }
            else
            {
                EditorGUILayout.HelpBox("Категория уже присутствует: " + weaponsList.type,
                MessageType.Warning);
            }
        }
    }

}
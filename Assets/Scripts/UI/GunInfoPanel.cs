using System;
using System.Collections;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class GunInfoPanel : MonoBehaviour
{
    [SerializeField] TMP_Text itemTitleText;
    [SerializeField] TMP_Text itemDescriptionText;
    [Space]

    [Header("Prefabs")]
    [SerializeField] Transform damageText;
    [Header("ShootingWeapon")]
    [SerializeField] TMP_Text magazineSizeText;
    [SerializeField] TMP_Text magazineCountText;
    [SerializeField] Transform fireRateText;
    [SerializeField] TMP_Text reloadTimeText;
    [Header("ThrowableWeapon")]
    [SerializeField] TMP_Text triggerTimeText;
    [SerializeField] TMP_Text explosionRadiusText;




    [Space(15)]
    [Header("Togglers")]
    [SerializeField] Toggle rotationToggle;
    [SerializeField] Toggle openPanelToggle;

    private Animator animator;

    public static Action<bool> OnToggleChanged;


    private bool isOpen = false;
    private bool isFirstOpen = true;


    private void Start()
    {
        openPanelToggle.gameObject.SetActive(false);

        animator = GetComponent<Animator>();
        rotationToggle.onValueChanged.AddListener(OnToggleChange);
        isOpen = openPanelToggle.isOn;

        openPanelToggle.onValueChanged.AddListener(delegate {
            OpenCloseWindow(openPanelToggle);
        });

    }



    private void OnEnable()
    {
        GetWeapon.onGunChange += OnWeaponChanged;
    }

    private void OnDisable()
    {
        GetWeapon.onGunChange -= OnWeaponChanged;
    }


    private void OnToggleChange(bool isOn)
    {
        OnToggleChanged?.Invoke(isOn);
    }

    //TODO: Решить проблему с обновлением текста до закрытия окна
    public void OnWeaponChanged(Weapon weapon)
    {
        openPanelToggle.gameObject.SetActive(true);
        StartCoroutine(UpdateWindow(weapon));
    }


    private void UpdateParameters(Weapon weapon)
    {
        itemTitleText.text = weapon.name;
        if (weapon.description != "")
        {
            itemDescriptionText.text = weapon.description.ToString() ;
        }
        else
        {
            itemDescriptionText.text = "Не указано";
        }


        damageText.GetChild(1).GetComponent<TMP_Text>().text = weapon.damage.ToString();
        //damageText.text = "Урон: " + weapon.damage;


        if (weapon is ShootingWeapon)
        {
            ShootingWeapon _shootingWeapon = weapon as ShootingWeapon;

            SetActiveStatus(true, fireRateText.gameObject,
                magazineCountText.gameObject,
                magazineSizeText.gameObject,
                reloadTimeText.gameObject);



            fireRateText.GetChild(1).GetComponent<TMP_Text>().text = _shootingWeapon.fireRate.ToString();
            //fireRateText.text = "Скорострельность: " + _shootingWeapon.fireRate + " в/cек";
            magazineSizeText.text = "Размер магазина: " + _shootingWeapon.magazineSize.ToString();
            magazineCountText.text = "Количество магазинов: " + _shootingWeapon.magazineCount.ToString();
            reloadTimeText.text = "Время перезарядки: " + _shootingWeapon.reloadTime + " сек";
        }
        else
        {
            SetActiveStatus(false, fireRateText.gameObject,
                magazineCountText.gameObject,
                magazineSizeText.gameObject,
                reloadTimeText.gameObject);
        }

        if (weapon is MeleeWeapon)
        {

            MeleeWeapon _meleeWeapon = weapon as MeleeWeapon;
        }

        if (weapon is ThrowableWeapon)
        {
            ThrowableWeapon _throwable = weapon as ThrowableWeapon;
            SetActiveStatus(true, triggerTimeText.gameObject,
                explosionRadiusText.gameObject);

            triggerTimeText.text = "Время до срабатывания: " + _throwable.triggerTime + " сек";
            explosionRadiusText.text = "Радиус взрыва: " + _throwable.explosionRadius + " м";

        }
        else
        {
            SetActiveStatus(false, triggerTimeText.gameObject,
                explosionRadiusText.gameObject);
        }

        if (weapon is UtilitiesWeapon)
        {
            UtilitiesWeapon _utilitiesWeapon = weapon as UtilitiesWeapon;
        }
    }

    public void OpenCloseWindow(Toggle toggle)
    {
        isOpen = toggle.isOn;
        animator.SetBool("IsOpen", toggle.isOn);
    }


    private IEnumerator UpdateWindow(Weapon weapon)
    {
        openPanelToggle.interactable = false;


        if(openPanelToggle.isOn || isFirstOpen)
        {
            isOpen = false;
            openPanelToggle.isOn = false;
            animator.SetBool("IsOpen", false);
        
            yield return new WaitForSeconds(3);
            UpdateParameters(weapon);

            animator.SetBool("IsOpen", true);
            openPanelToggle.isOn = true;
            isOpen = true;

            isFirstOpen = false;
        }
        else
        {
            yield return new WaitForSeconds(3);
            UpdateParameters(weapon);
        }

        openPanelToggle.interactable = true;
    }



    private void SetActiveStatus(bool status, params GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(status);
        }
    }

}

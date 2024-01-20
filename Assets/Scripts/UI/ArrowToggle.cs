using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowToggle : MonoBehaviour
{
    [SerializeField] Sprite spriteOn;
    [SerializeField] Sprite spriteOff;



    private Toggle toggle;
    private Image image;

    private void Start()
    {
        image = transform.GetComponent<Image>();
        toggle = transform.parent.parent.GetComponent<Toggle>();

        toggle.graphic = null;


        toggle.onValueChanged.AddListener(delegate {
            ChangedValue(toggle);
        });
        ChangedValue(toggle);

    }
/*    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(delegate
        {
            ChangedValue(toggle);
        });
    }*/

    private void ChangedValue(Toggle toggle)
    {
        if(toggle.isOn)
        {
            image.sprite = spriteOn;
        }
        else
        {
            image.sprite = spriteOff;
        }
    }

}

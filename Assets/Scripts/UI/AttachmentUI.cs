using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttachmentUI : MonoBehaviour
{

    [SerializeField] private Canvas _canvasPrefab;
    [SerializeField] private GameObject _attachmentWindow;
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private Sprite _noneSprite;


    private void OnEnable()
    {
        GetWeapon.onGunChange += GetAttachmets;
    }

    private void OnDisable()
    {
        GetWeapon.onGunChange -= GetAttachmets;
    }



    private void GetAttachmets(Weapon weapon)
    {
        DeleteAttachmentButtons();
        if (weapon is ShootingWeapon)
        {
            ShootingWeapon _shootingWeapon = weapon as ShootingWeapon;

            if (_shootingWeapon.allowedAttachments != null) 
            {
                foreach(AllowedAttachments attachmentsList in _shootingWeapon.allowedAttachments)
                {
                    CreateAttachmentButton(attachmentsList);
                }
            }
        }
    }



    private void CreateAttachmentButton(AllowedAttachments attachmentsList)
    {
        GameObject attachmentCanvas = Instantiate(_canvasPrefab.gameObject, transform);

        attachmentCanvas.name = "Canvas " + attachmentsList.attachments[0].attachment.attachmentType.ToString();

        Vector3 newPos = attachmentsList.buttonPosition;

        attachmentCanvas.transform.localPosition = newPos;

        Button newButton = Instantiate(_buttonPrefab, attachmentCanvas.transform.position,
            attachmentCanvas.transform.rotation, attachmentCanvas.transform);
        newButton.GetComponentInChildren<TMP_Text>().text = attachmentsList.type.ToString();


        newButton.onClick.AddListener(() =>
        {
            CreateWindow(newButton, attachmentsList);
        });
    }

    private void CreateWindow(Button newbutton, AllowedAttachments attachmentsList)
    {
        GameObject window = null;
        foreach (Transform child in newbutton.transform)
        {
            if (child.name.Contains(_attachmentWindow.name))
            {
                window = child.gameObject;
                Destroy(window);
            }
        }

        if(window == null)
        {
            CreateAttachmentWindow(newbutton, attachmentsList);
        }
    }


    private void CreateAttachmentWindow(Button button, AllowedAttachments attachmentsList)
    {

        GameObject window = Instantiate(_attachmentWindow, button.transform);

        CalculateWindowSize(window,button, attachmentsList);

        Button noneButton = Instantiate(_buttonPrefab, window.transform);
        noneButton.GetComponentInChildren<TMP_Text>().text = "None";
        noneButton.onClick.AddListener(() =>
        {
            UpdateAttachmentIcon(button, null);
            NoneButtonListener(attachmentsList);
        });

        foreach (AttachmentInfo attachment in attachmentsList.attachments)
        {
            Button newButton = Instantiate(_buttonPrefab, window.transform);

            newButton.GetComponentInChildren<TMP_Text>().text = attachment.attachment.name;

            UpdateAttachmentIcon(newButton, attachment);

            newButton.onClick.AddListener(() =>
            {
                UpdateAttachmentIcon(button, attachment);
                SpawnNewAttachmentListener(attachment);
            });
        }
    }


    private Vector2 CalculateWindowSize(GameObject window, Button button, AllowedAttachments attachmentsList)
    {
        RectTransform rect = window.GetComponent<RectTransform>();
        RectTransform buttonRect = button.GetComponent<RectTransform>();

        GridLayoutGroup gridGroup = window.GetComponent<GridLayoutGroup>();

        gridGroup.cellSize = new Vector2(buttonRect.sizeDelta.x, buttonRect.sizeDelta.y);


        int buttonsCount = attachmentsList.attachments.Count + 1;
        int rowsCount = buttonsCount / gridGroup.constraintCount;



        if (rowsCount == 0 && buttonsCount > 0)
        {
            rowsCount = 1;
        }

        float x = 0;

        if (buttonsCount > gridGroup.constraintCount)
        {
            x = ((gridGroup.spacing.x + gridGroup.cellSize.x) * gridGroup.constraintCount) - gridGroup.spacing.x + gridGroup.padding.horizontal;
        }
        else
        {
            x = ((gridGroup.spacing.x + gridGroup.cellSize.x) * buttonsCount) - gridGroup.spacing.x + gridGroup.padding.horizontal;
        }

        float y = (rowsCount * (gridGroup.spacing.y + gridGroup.cellSize.y)) - gridGroup.spacing.y + gridGroup.padding.vertical;
        rect.sizeDelta = new Vector2(x, y);


        rect.position = button.transform.position - new Vector3(
            -(rect.sizeDelta.x / 2 - (buttonRect.sizeDelta.x / 2)),
            rect.sizeDelta.y / 2 + (buttonRect.sizeDelta.y / 2) + gridGroup.spacing.y,
            0);


        return new Vector2(x, y);
    }

    private void SpawnNewAttachmentListener(AttachmentInfo attachment)
    {
        transform.GetChild(0).GetComponent<GetWeapon>().SpawnAttachment(attachment);
    }

    private void NoneButtonListener(AllowedAttachments attachmentsList)
    {
        transform.GetChild(0).GetComponent<GetWeapon>().DestroyAttachment(attachmentsList.type);
    }

    private void DeleteAttachmentButtons()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if(child.GetComponent<Canvas>() != null)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void DeleteAttachmentButton(AllowedAttachments attachmentsList)
    {
        GameObject buttonTodel = transform.Find("Canvas " + attachmentsList.attachments[0].attachment.attachmentType.ToString()).gameObject;
        if (buttonTodel != null)
        { 
            Destroy(buttonTodel);
        }
    }

    private void UpdateAttachmentIcon(Button button, AttachmentInfo attachment)
    {
        Transform icon = button.transform.Find("Icon");

        Image image = icon.GetComponent<Image>();
        Color color = image.color;


        if(attachment != null)
        {
            if (attachment.attachment.icon != null)
            {
                image.sprite = attachment.attachment.icon;
                color.a = 1f;
            }
            else
            {
                image.sprite = _noneSprite;
                color.a = 0f;
            }
        }
        else
        {
            image.sprite = _noneSprite;
            color.a = 0f;
        }
        image.color = color;

    }

}

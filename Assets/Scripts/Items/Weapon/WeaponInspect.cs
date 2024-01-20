using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponInspect : MonoBehaviour
{

    [SerializeField] private float rotateSpeed = .1f;

    [SerializeField] private bool gotRotationBounds;
    [SerializeField] private float rotationXMin;
    [SerializeField] private float rotationXMax;

    [SerializeField] private float zoomSpeed;
    [SerializeField] private float objectOffset = 0.1f;

    [Space]

    [SerializeField] private Animator animator;


    private Vector3 lastMousePosition;

    private bool rotating = false;

    private Vector3 startCamPosition;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
        startCamPosition = cam.transform.position;
    }


    private void Update()
    {
        
        if (!rotating)
        {
            GetComponent<Animator>().enabled = false;
            MouseInteract();
        }
        else
        {
            GetComponent<Animator>().enabled = true;
        }

         ZoomToObject();
         
    }

    private void OnEnable()
    {
        GunInfoPanel.OnToggleChanged += RotateObject;
    }

    private void OnDisable()
    {
        GunInfoPanel.OnToggleChanged -= RotateObject;
    }


    public void RotateObject(bool rotateState)
    {
        rotating = rotateState;
        animator.SetBool("Rotating", rotateState);
    }


    private void MouseInteract()
    {
        if (Input.GetButton("Fire1") && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mouseDelta = lastMousePosition - Input.mousePosition;

            transform.localEulerAngles += new Vector3(mouseDelta.y, mouseDelta.x, 0f) * rotateSpeed;


            float localEulerAnglesX = transform.localEulerAngles.x;

            if (localEulerAnglesX > 180)
            {
                localEulerAnglesX -= 360f;
            }

            //TODO: переписать логику вращения, избавиться от застревания при вращении без границ
            float rotationX = localEulerAnglesX;

            if(gotRotationBounds)
            {
               rotationX = Mathf.Clamp(localEulerAnglesX, rotationXMin, rotationXMax);
            }

            transform.localEulerAngles = new Vector3(rotationX,
                                                        transform.localEulerAngles.y,
                                                        transform.localEulerAngles.z);

        }



        lastMousePosition = Input.mousePosition;
    }


    private void ZoomToObject()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0.0f && !EventSystem.current.IsPointerOverGameObject())
        { 
            if (cam != null )
            {
                Vector3 newPosition = new Vector3(transform.position.x, 0, transform.position.z) * scroll * zoomSpeed;

                if (scroll > 0 && Vector3.Distance(transform.position, cam.transform.position) > objectOffset)
                {
                    if (cam.transform.position.x < transform.position.x && cam.transform.position.z < transform.position.z)
                        cam.transform.position += newPosition;
                    else
                        cam.transform.position = transform.position;
                }
                else if (scroll < 0)
                {
                    if (cam.transform.position.x > startCamPosition.x && cam.transform.position.z > startCamPosition.z)
                        cam.transform.position += newPosition;
                    else
                        cam.transform.position = startCamPosition;
                }
            }
        }
    }
}

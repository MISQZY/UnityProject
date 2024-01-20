using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Start()
    {
        LookOnCamera();
    }

    private void LateUpdate()
    {
        LookOnCamera();
    }

    private void LookOnCamera()
    {
        transform.forward = Camera.main.transform.forward;
    }
}

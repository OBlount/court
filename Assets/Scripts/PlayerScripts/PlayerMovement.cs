using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    private bool enableLooking;
    private float currentYaw;

    void Start()
    {
        enableLooking = false;
        currentYaw = transform.eulerAngles.y;
    }

    void Update()
    {
        if (enableLooking)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            currentYaw += mouseX;
            currentYaw = Mathf.Clamp(currentYaw, -150, -30);
            transform.localRotation = Quaternion.Euler(0f, currentYaw, 0f);
        }
    }

    public bool isLooking()
    {
        return enableLooking;
    }

    public void SetLookingMode(bool isLooking)
    {
        enableLooking = isLooking;
        if (isLooking)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        Cursor.visible = !isLooking;
    }
}

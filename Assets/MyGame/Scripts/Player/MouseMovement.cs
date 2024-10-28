using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("DangSon/MouseMovement")]
public class MouseMovement : MonoBehaviour
{
    [Header("Mouse")]
    public float mouseSensivity = 500f;
    private float xRotation;
    private float yRotation;
    [Header("Calmp")]
    public float topClamp = -90f;
    public float bottomClamp = 90f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")*mouseSensivity*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")*mouseSensivity * Time.deltaTime;
        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}

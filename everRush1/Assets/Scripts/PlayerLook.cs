using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam; 
    private float xRotation = 0f; //rotacija na x-osi

    public float xSensitivity = 30f; //hitrost premikanja kamere na x-osi
    public float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //izracuna rotacijo kamere za gledanje navzgor in navzdol
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        //to uporabimo za transformacijo kamere
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //rotacija player-ja za pogled na levo in desno
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}

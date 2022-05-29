using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GyroData : MonoBehaviour
{

    private bool gyroEnabled;
    float Gyromums;
    //public Text Data;
    Vector2 gyrovector;


    private void Start()
    {
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {

        InputSystem.EnableDevice(AttitudeSensor.current);

        return true;

    }

    private void Update()
    {

        //Debug.Log(AttitudeSensor.current.attitude.ReadValue());
        Quaternion value = AttitudeSensor.current.attitude.ReadValue();
        float x = (float)value.x;
        float y = (float)value.y;
        //gyrovector = new Vector2(x, y);

        Gyromums = (x + y) * 4;



        //Data.text = Gyromums.ToString();




    }

    public float GetGyro()

    {
        return Gyromums;
    }

}

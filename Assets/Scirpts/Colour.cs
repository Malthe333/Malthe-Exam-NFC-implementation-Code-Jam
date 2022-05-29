using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour : MonoBehaviour
{

    SpriteRenderer renderer;



    private void OnJointBreak(float breakForce)
    {
        
    }
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = Color.HSVToRGB(0.2f, 1, 1);


    }

    private void Update()
    {

        float Gyromums = GetComponent<GyroData>().GetGyro();
        Gyromums = Mathf.Abs(Gyromums);
        
            renderer.color = Color.HSVToRGB(Gyromums % 1, 1, 1);

        
            

       
    }


}

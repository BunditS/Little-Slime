using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Day_Night : MonoBehaviour
{

    public Gradient GradientColor;
    public float locate = 0;
    public Light2D Light;
     
    private void Start()
    {
       
    }

    private void Update()
    {   
       
        //กำหนดสีของแสงให้เป็นกลางวันและกลางคืน
        locate = Mathf.PingPong(GameManager.instance.minute / (1440f/2f), 1);   
        Light.color = GradientColor.Evaluate(locate);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float smooth ;
    public float tiltAngle ;
    public float angleZ;
    void Update()
    {
        angleZ = transform.localEulerAngles.z;
        
        Debug.Log(angleZ);
        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        // Rotate the cube by converting the angles into a quaternion.  //ตรงนี้คือกำหนดอง
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  smooth * Time.deltaTime);
    }
}

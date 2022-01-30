using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectToTarget {
    
    public static void RotateObject (GameObject go, Vector3 target) {

        var lookPos = target - go.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        go.transform.rotation = Quaternion.Slerp(go.transform.rotation, rotation, 1);


    }


}

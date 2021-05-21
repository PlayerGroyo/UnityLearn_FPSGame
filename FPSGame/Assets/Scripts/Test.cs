using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 vector3 = new Vector3(1, 1, 1);

        Vector3 res = transform.TransformVector(vector3);
        
        print(res);
    }
}

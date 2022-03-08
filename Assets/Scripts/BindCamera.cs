using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindCamera : MonoBehaviour
{
    public Transform target;

    private Vector3 offset;
    private const float kY = 7;

    // Start is called before the first frame update
    void Start()
    {
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, -offset.z);
        //transform.position.y = kY;
    }
}

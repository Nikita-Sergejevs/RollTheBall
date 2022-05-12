using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemRotate : MonoBehaviour
{
    public float speed;
    private Vector3 rotation;

    public void Start()
    {
        rotation = new Vector3(45, 15, 90);
    }

    public void Update()
    {
        this.transform.Rotate(rotation * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;
    private Vector3 rotation;

    public void Start()
    {
        rotation = new Vector3(0,90);
    }

    public void Update()
    {
        this.transform.Rotate(rotation * speed * Time.deltaTime);
    }
}

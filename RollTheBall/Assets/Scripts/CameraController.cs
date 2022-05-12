using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerObject;
    private Vector3 camOffset;

    public void Start()
    {
        camOffset = this.transform.position - playerObject.transform.position;
    }

    public void LateUpdate()
    {
        this.transform.position = playerObject.transform.position + camOffset;
    }
}

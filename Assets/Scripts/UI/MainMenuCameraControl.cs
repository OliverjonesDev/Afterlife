using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraControl : MonoBehaviour
{
    [SerializeField]
    private float smoothness;
    [SerializeField]
    private float horizontalMovementPos, horizontalMovementNeg, verticalMovementPos, verticalMovementNeg;
    private Camera _camera;
 
    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        var pos = transform.position;
        pos.x = Mathf.Clamp(Mathf.Lerp(transform.TransformDirection(Vector3.forward).x, transform.TransformDirection(Vector3.forward).x + Input.GetAxisRaw("Mouse X") * 1, smoothness * Time.deltaTime), horizontalMovementNeg, horizontalMovementPos);
        pos.y = Mathf.Clamp(Mathf.Lerp(transform.TransformDirection(Vector3.forward).y, transform.TransformDirection(Vector3.forward).y + Input.GetAxisRaw("Mouse Y") * 1, smoothness * Time.deltaTime), verticalMovementNeg, verticalMovementPos);

    }
}

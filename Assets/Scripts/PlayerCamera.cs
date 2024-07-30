using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    public Transform target;

    private Vector3 vel = Vector3.zero;
    private float yVal;

    private void Start()
    {
        yVal = target.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = -10;
        targetPosition.y = yVal + offset.y;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
    }
}

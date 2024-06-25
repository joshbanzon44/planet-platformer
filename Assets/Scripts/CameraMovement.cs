using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject cover;
    [SerializeField] private float smoothTime = 1f;

    public float camStartSize = 20f;
    public Vector3 backgroundStartScale = new Vector3(10f,10f,10f);

    private float camEndSize = 5f;
    private Vector3 backgroundEndScale = new Vector3(2f,2f,2f);

    private float zoom;
    private float velocity = 0;
    private Vector3 velo = Vector3.zero;

    private bool deleted = false;

    // Start is called before the first frame update
    void Start()
    {
        cover.GetComponent<SpriteRenderer>().enabled = true;
        cam.orthographicSize = camStartSize;
        background.transform.localScale = backgroundStartScale;
    }

    // Update is called once per frame
    void Update()
    {
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, camEndSize, ref velocity, smoothTime);
        background.transform.localScale = Vector3.SmoothDamp(background.transform.localScale, backgroundEndScale, ref velo, smoothTime);

        if (!deleted && (cam.orthographicSize<5.1f))
        {
            Destroy(cover);
            deleted = true;
        }
    }
}

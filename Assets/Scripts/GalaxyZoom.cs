using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GalaxyZoom : MonoBehaviour
{
    private bool play = false;
    [SerializeField] private Camera cam;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject cover;
    [SerializeField] private GameObject milkyway;
    [SerializeField] private float smoothTime = 1f;
    public Animator transitionAnim;

    private float velocity = 0;
    private Vector3 velo = Vector3.zero;

    public float camStartSize = 5f;

    public float camEndSize = 1f;

    // Start is called before the first frame update
    void Start()
    {
        cam.orthographicSize = camStartSize;
        cover.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void PlayButton()
    {
        StartCoroutine(enumer());
        canvas.enabled = false;
        cover.GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator enumer()
    {
        play = true;
        yield return new WaitForSeconds(smoothTime/2);
        cover.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(smoothTime/2);
        SceneManager.LoadScene("Level Select");
        transitionAnim.SetTrigger("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (!play)
        {
            return;
        }
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, new Vector3(-2f,-2.5f,-10f), ref velo, 0.5f);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, camEndSize, ref velocity, smoothTime);
    }
}

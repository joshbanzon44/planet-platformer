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

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        //Each number is a reference to a planet. 1 = Unlocked, 0 = Locked
        //The 9 planets are ordered as such:
        //Earth, Moon, Mars, Venus, Jupiter, Mercury, Saturn, Uranus, Neptune
        PlayerPrefs.SetString("PlanetsUnlocked", "100000000");

        //Each didgit of planet number determines if level is completed. 1 = Unlocked, 0 = Locked
        //E.g. if first level is complete, int will = 100.
        //        second level, int = 110
        //        third level, int = 111
        PlayerPrefs.SetInt("Earth", 111);
        PlayerPrefs.SetInt("Moon", 000);
        PlayerPrefs.SetInt("Mars", 000);
        PlayerPrefs.SetInt("Venus", 000);
        PlayerPrefs.SetInt("Jupiter", 000);
        PlayerPrefs.SetInt("Mercury", 000);
        PlayerPrefs.SetInt("Saturn", 000);
        PlayerPrefs.SetInt("Uranus", 000);
        PlayerPrefs.SetInt("Neptune", 000);
        PlayerPrefs.Save();

        StartCoroutine(enumer());
        canvas.enabled = false;
        cover.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void ContinueGame()
    {
        StartCoroutine(enumer());
        canvas.enabled = false;
        cover.GetComponent<SpriteRenderer>().enabled = true;
    }

    //Function to quit game on button click
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject outline;
    private Vector3 startPos;
    private Vector3 displayPos = new Vector3(-6f,0f,0f);

    private Vector3 startScale;
    private Vector3 displayScale = new Vector3(5.5f, 5.5f, 5.5f);


    //Information for HUD 
    public string planetName = "";
    public string planetType = "";
    public int radius = 0;
    public int surfaceTemp = 0;
    public float gravity = 0;
    public Vector3 rotationLength; //x is days, y is hours, z is minutes




    //Runs when game starts
    private void Start()
    {
        hideOutline();

        Camera.main.transform.GetChild(0).GetComponent<Canvas>().enabled = false;

        startPos = transform.position;
        startScale = transform.localScale;
    }

    //When mouse enters collider
    private void OnMouseEnter()
    {
        if (Camera.main.transform.GetChild(0).GetComponent<LevelHUD>().getPlanet() != null)
        {
            return;
        }

        transform.localScale *= 1.2f;
        showOutline();

    }

    //When mouse exits collider
    private void OnMouseExit()
    {
        if (Camera.main.transform.GetChild(0).GetComponent<LevelHUD>().getPlanet() != null)
        {
            return;
        }

        transform.localScale /= 1.2f;
        hideOutline();
    }

    //When mouse is pressed down and released
    private void OnMouseUpAsButton()
    {
        if (Camera.main.transform.GetChild(0).GetComponent<LevelHUD>().getPlanet() != null)
        {
            return;
        }
        
        hideOutline();
        transform.position = displayPos;
        transform.localScale = displayScale;
        GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("UI");
        GetComponent<Animator>().SetBool("display",true);
        Camera.main.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
        Camera.main.transform.GetChild(0).GetComponent<LevelHUD>().setActivePlanet(gameObject);
    }


    private void hideOutline()
    {
        outline.transform.GetComponent<SpriteRenderer>().enabled = false;
        outline.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    private void showOutline()
    {
        outline.transform.GetComponent<SpriteRenderer>().enabled = true;
        outline.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }

    
    public void exitDisplay()
    {
        transform.position = startPos;
        transform.localScale = startScale;
        GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Objects");
        GetComponent<Animator>().SetBool("display", false);
        Camera.main.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
    }

    public string getPlanetName()
    {
        return planetName; 
    }
}

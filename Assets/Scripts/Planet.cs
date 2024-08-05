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

    //Locked variable
    private bool locked = true;


    //Runs when game starts
    private void Start()
    {
        hideOutline();

        Camera.main.transform.GetChild(0).GetComponent<Canvas>().enabled = false;

        startPos = transform.position;
        startScale = transform.localScale;

        transform.GetChild(1).GetComponent<Canvas>().enabled = false;

        int i = 0;
        string prevPlanet = "";
        switch (planetName)
        {
            case "Earth":
                i = 0;
                prevPlanet = "";
                break;
            case "Moon":
                i = 1;
                prevPlanet = "Earth";
                break;
            case "Mars":
                i = 2;
                prevPlanet = "Moon";
                break;
            case "Venus":
                i = 3;
                prevPlanet = "Mars";
                break;
            case "Jupiter":
                i = 4;
                prevPlanet = "Venus";
                break;
            case "Mercury":
                i = 5;
                prevPlanet = "Jupiter";
                break;
            case "Saturn":
                i = 6;
                prevPlanet = "Mercury";
                break;
            case "Uranus":
                i = 7;
                prevPlanet = "Saturn";
                break;
            case "Neptune":
                i = 8;
                prevPlanet = "Uranus";
                break;
        }

        //Unlock next planet if all 3 previous levels complete
        if(PlayerPrefs.GetInt(prevPlanet) == 111)
        {
            string str = PlayerPrefs.GetString("PlanetsUnlocked");
            Debug.Log(planetName);
            Debug.Log(str);
            char[] ch = str.ToCharArray();
            ch[i] = '1';
            str = new string(ch);
            Debug.Log(str);
            PlayerPrefs.SetString("PlanetsUnlocked",str);
            PlayerPrefs.Save();
        }

        //Toggles whether planet is unlocked or not
        if (PlayerPrefs.GetString("PlanetsUnlocked")[i] == '1')
        {
            locked = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }


    //When mouse enters collider
    private void OnMouseEnter()
    {

        if (Camera.main.transform.GetChild(0).GetComponent<LevelHUD>().getPlanet() != null)
        {
            return;
        }

        if (locked && planetName != "Earth")
        {
            transform.GetChild(1).GetComponent<Canvas>().enabled = true;
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

        if (locked && planetName != "Earth")
        {
            transform.GetChild(1).GetComponent<Canvas>().enabled = false;
        }

        transform.localScale /= 1.2f;
        hideOutline();
    }

    //When mouse is pressed down and released
    private void OnMouseUpAsButton()
    {
        if (Camera.main.transform.GetChild(0).GetComponent<LevelHUD>().getPlanet() != null || locked)
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

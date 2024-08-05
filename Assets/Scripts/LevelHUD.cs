using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelHUD : MonoBehaviour
{
    private GameObject activePlanet= null;
    private Planet planetComponent;

    //References to TMP text boxes
    public TMP_Text projectorLabel;
    public TMP_Text planetTypeLabel;
    public TMP_Text radiusLabel;
    public TMP_Text gravityLabel;
    public TMP_Text surfTempLabel;
    public TMP_Text rotationLabel;

    //References to Lvl buttons
    public Button lvlBtn2;
    public Button lvlBtn3;


    public void setActivePlanet(GameObject planet)
    {
        activePlanet = planet;
        planetComponent = activePlanet.GetComponent<Planet>();

        //Set all planet information
        projectorLabel.SetText(planet.name);
        planetTypeLabel.SetText(planetComponent.planetType);
        radiusLabel.SetText(planetComponent.radius + " mi");
        gravityLabel.SetText(planetComponent.gravity + " m/s²");
        surfTempLabel.SetText(planetComponent.surfaceTemp + "°F");
        rotationLabel.SetText(planetComponent.rotationLength.x + "d," + planetComponent.rotationLength.y + "h," + planetComponent.rotationLength.z + "m");

        //Disable buttons
        lvlBtn2.interactable = false;
        lvlBtn3.interactable = false;

        //Enable buttons if levels are complete
        if (PlayerPrefs.GetInt(planetComponent.planetName) == 100)
        {
            lvlBtn2.interactable = true;
        }
        else if (PlayerPrefs.GetInt(planetComponent.planetName) == 110 || PlayerPrefs.GetInt(planetComponent.planetName) == 111)
        {
            lvlBtn2.interactable = true;
            lvlBtn3.interactable = true;
        }

    }

    public GameObject getPlanet()
    {
        return activePlanet;
    }

    public void returnToSelect()
    {
        activePlanet.GetComponent<Planet>().exitDisplay();
        activePlanet = null;
    }


}

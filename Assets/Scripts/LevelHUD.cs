using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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


    public void setActivePlanet(GameObject planet)
    {
        activePlanet = planet;
        planetComponent = activePlanet.GetComponent<Planet>();
        projectorLabel.SetText(planet.name);
        planetTypeLabel.SetText(planetComponent.planetType);
        radiusLabel.SetText(planetComponent.radius + " mi");
        gravityLabel.SetText(planetComponent.gravity + " m/s²");
        surfTempLabel.SetText(planetComponent.surfaceTemp + "°F");
        rotationLabel.SetText(planetComponent.rotationLength.x + "d," + planetComponent.rotationLength.y + "h," + planetComponent.rotationLength.z + "m");
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

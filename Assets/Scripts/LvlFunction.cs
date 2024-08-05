using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlFunction : MonoBehaviour
{
    //Toggle if scene is not a level
    public bool isLevel;
    
    //Player Reference
    public GameObject player;
    
    //Progress Bar References
    public Image greenBar;
    public Image astronaut;
    public float leftmostPlayerX;

    //End Object Reference;
    public GameObject endpoint;

    private float totalDistance = 0;
    private Vector3 barStartPos;
    private Vector3 barEndPos;

    private float percentComplete = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (!isLevel)
        {
            return;
        }

        barStartPos = astronaut.rectTransform.localPosition;
        greenBar.fillAmount = 0;

        totalDistance = endpoint.transform.position.x - leftmostPlayerX;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLevel)
        {
            return;
        }

        float playerPosX = player.transform.position.x;
        float endPosX = endpoint.transform.position.x;

        //Prevents astronaut from going off bar
        if (playerPosX < 0)
        {
            return;
        }

        percentComplete = 1f - ((endPosX - playerPosX) / totalDistance);

        float astroX = percentComplete*1250f;

        //Prevents astronaut from going off bar
        if (astroX > 1250)
        {
            return;
        }


        if (astroX == 625)
        {
            astroX = 0;
        }
        else if (astroX > 625)
        {
            astroX -= 625;
        }
        else
        {
            astroX = -625 + astroX;
        }

        
        greenBar.fillAmount = percentComplete;
        astronaut.rectTransform.localPosition = new Vector3(astroX,450,0);

    }
}

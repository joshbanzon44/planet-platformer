using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Script to controller scene changing throughout the game
    public Animator transitionAnim;
    public LevelHUD levelHUD;

    private string levelName;

    private void Start()
    {
        transitionAnim.SetTrigger("Awake");
    }

    public void NextScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    public void NextLevelButton(int levelNumber)
    {
        levelName = levelHUD.getPlanet().name + "L" + levelNumber;
        StartCoroutine(LoadLevel(levelName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
        transitionAnim.SetTrigger("Start");
    }
    
}

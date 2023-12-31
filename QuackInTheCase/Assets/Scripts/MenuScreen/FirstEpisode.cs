using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEpisode : MonoBehaviour
{
    private int levelsUnlocked = 1;
    private int cutscenesUnlocked = 0;
    public List<GameObject> Ep1Unlockables = new List<GameObject>();
    public List<GameObject> Ep1Cutscenes = new List<GameObject>();

    void Start()
    {
        
    }

    //Check each object in the list, and if the index of the object in the list is lower than the levelsUnlocked int, enable it. Otherwise, disable it.
    public void CheckEpisodeOneStatus()
    {
        levelsUnlocked = GetComponent<GameManager>().FirstEpisode(true);
        cutscenesUnlocked = GetComponent<GameManager>().FirstEpisode(false);
        Debug.Log("Levels unlocked: " + levelsUnlocked);

        for (int i = 0; i < Ep1Unlockables.Count; i++)
        {
            if (i < levelsUnlocked)
            {
                Ep1Unlockables[i].SetActive(true);
            }
            else
            {
                Ep1Unlockables[i].SetActive(false);
            }
        }

        for (int i = 0; i < Ep1Cutscenes.Count; i++)
        {
            if (i < cutscenesUnlocked)
            {
                Ep1Cutscenes[i].SetActive(true);
            }
            else
            {
                Ep1Cutscenes[i].SetActive(false);
            }
        }
    }

    public void CheckCutsceneOneStatus()
    {
        cutscenesUnlocked = GetComponent<GameManager>().FirstEpisode(false) - 1;
    }
}

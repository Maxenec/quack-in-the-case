using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeMode : MonoBehaviour
{
    private GameObject god;
    private List<string> levels = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        levels.Add("E1G1");
        levels.Add("E1G2");
        levels.Add("E1G3");
        levels.Add("E1G4");
        levels.Add("E1G5");
        levels.Add("E1G6");
    }

    // Update is called once per frame
    void Update()
    {
        if(god == null){
            god = GameObject.Find("God");
        }
    }

    public void ArcadeButton(){
        DontDestroyOnLoad(this.gameObject);
        god.GetComponent<GameManager>().SwitchScene(levels[Random.Range(0, god.GetComponent<GameManager>().FirstEpisode(true))]);
    }
}

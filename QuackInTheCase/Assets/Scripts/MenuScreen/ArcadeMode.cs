using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeMode : MonoBehaviour
{
    private GameObject god;
    private List<string> unlockedLevels = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        unlockedLevels.Add("E1G1");
        //unlockedLevels.Add("E1G2");
        //unlockedLevels.Add("E1G3");
        unlockedLevels.Add("E1G4");
        //unlockedLevels.Add("E1G5");
        unlockedLevels.Add("E1G6");
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
        god.GetComponent<GameManager>().SwitchScene(unlockedLevels[Random.Range(0,unlockedLevels.Count)]);
    }

    public void ArcadeFail(){
        god.GetComponent<GameManager>().QuitToMenu();
        Destroy(this.gameObject);
    }
}

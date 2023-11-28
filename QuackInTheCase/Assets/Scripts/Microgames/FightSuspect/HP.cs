using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public int maxHP = 100;
    public int minHP = 0;
    public int currentHP;
    public bool isPlayer;
    public Slider hpBar;
    public GameObject manager;

    // Start is called before the first frame update
    void Start() //sets the max and min values for the hp bar
    {
        hpBar.maxValue = maxHP;
        hpBar.minValue = minHP;
        SetHP(maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHP(int hp) //sets the hp to a valsue
    {
        currentHP = hp;
        if (currentHP > maxHP){ // if the hp increases past the max, it resets to the max
            currentHP = maxHP; 
        }
        if (currentHP < minHP){ // if the hp reduces below the min, it rests to the min
            currentHP = minHP;
        }
        hpBar.value = currentHP; //sets the bar value to the hp
        //CheckHP(); //runs the checking function
    }

    public void EffectHP(int hp) //either reduces or increases the hp
    {
        currentHP += hp;
        if (currentHP > maxHP){ // if the hp increases past the max, it resets to the max
            currentHP = maxHP; 
        }
        if (currentHP < minHP){ // if the hp reduces below the min, it rests to the min
            currentHP = minHP;
        }
        hpBar.value = currentHP; //sets the bar value to the hp
        //CheckHP(); //runs the checking function
    }

    public bool CheckHP(){
        if(currentHP <= minHP){
            return true;
        } else{
            return false;
        }
    }
}

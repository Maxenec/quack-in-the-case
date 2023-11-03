using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public int maxHP = 100;
    public int minHP = 0;
    public int currentHP;
    public Slider hpBar;

    // Start is called before the first frame update
    void Start()
    {
        hpBar.maxValue = maxHP;
        hpBar.minValue = minHP;
        SetHP(maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHP(int hp)
    {
        currentHP = hp;
        if (currentHP > maxHP)
        {
            currentHP = maxHP; 
        }
        if (currentHP < minHP)
        {
            currentHP = minHP;
        }
        hpBar.value = currentHP;
    }

    public void EffectHP(int hp)
    {
        currentHP += hp;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        if (currentHP < minHP)
        {
            currentHP = minHP;
        }
        hpBar.value = currentHP;
    }
}

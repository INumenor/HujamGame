using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarMan : MonoBehaviour
{
    public Image EnergyBar;
    public float EnergyBarAmount = 100f;

    void Start()
    {
        
    }

    public void Heal()
    {

    }
    
    public void UseEnergy()
    {
        if(EnergyBarAmount > 0f)
        {
            EnergyBarAmount -= 10f;
            EnergyBar.fillAmount = EnergyBarAmount / 100f;
        }
    }
    public void EarnEnergy()
    {
        if (EnergyBarAmount < 50f)
        {
            EnergyBarAmount += 10f;
            EnergyBar.fillAmount = EnergyBarAmount / 100f;
        } 
    }
    public float _EnergyBarAmount
    {
        get
        {
            return EnergyBarAmount;
        }
    }
}

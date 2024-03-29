using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Text energyText;
    public static int EnergyCurrent;
    public static int EnergyMax;
    public static int EnergyLargeCurrent;
    public static int EnergyLargeMax;
    private Image energyBar;
    // Start is called before the first frame update
    void Start()
    {
        energyBar = GetComponent<Image>();
        EnergyCurrent = EnergyMax;
    }

    // Update is called once per frame
    void Update()
    {
        energyBar.fillAmount = (float)EnergyCurrent / (float)EnergyMax;
        energyText.text = EnergyLargeCurrent.ToString() + "/" + EnergyLargeMax.ToString();
    }
}

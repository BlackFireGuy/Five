using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : BasePanel
{
    public void UpdateHealth(float currentHealth)
    {
        switch (currentHealth)
        {
            case 3:
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 2:
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 0:
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                break;
        }
    }
}

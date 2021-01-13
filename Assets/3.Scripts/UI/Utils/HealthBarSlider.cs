using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarSlider : MonoBehaviour
{

    public Image hpImage;
    public Image hpEffectImage;

    public float hp;
    [SerializeField] public float maxHp;
    [SerializeField] private float hurtSpeed = 0.005f;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        hpImage.fillAmount = hp / maxHp;
        if(hpEffectImage.fillAmount > hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= hurtSpeed;
        }
        else
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }
        text.text = hp.ToString() ;
    }
}

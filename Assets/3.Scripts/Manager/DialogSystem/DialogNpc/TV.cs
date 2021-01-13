using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : DialogButton
{
    public override void Show()
    {
        AdsManager.GetInstance().ShowRewardAds();
    }
}

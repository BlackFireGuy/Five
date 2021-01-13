using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBirdTwo : Shoot
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        bulletPath = "Prefabs/Bomb&TVAndSoOn/GUN&Bullet/Bullet002";
    }
}

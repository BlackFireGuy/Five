using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBirdOne : Shoot
{
    protected override void Start()
    {
        base.Start();
        bulletPath = "Prefabs/Bomb&TVAndSoOn/GUN&Bullet/Bullet001";
    }
}

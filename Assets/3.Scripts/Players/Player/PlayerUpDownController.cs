using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpDownController : MonoBehaviour//,IDamageable
{
    /*Rigidbody2D rig;
    Animator anim;
    
    Vector2 Movement;

    public float speed;
    public GameObject myBag;
    bool isOpen;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //myBag = InventoryManager.instance.bagUI;
    }

    // Update is called once per frame
    void Update()
    {
        UpDownController();

        SwitchAnim();

        OpenMyBag();
        *//*if (myBag.activeSelf == false)
            isOpen = false;*//*
    }

    private void FixedUpdate()
    {
        rig.MovePosition(rig.position + Movement * speed * Time.fixedDeltaTime);
        
    }

    void SwitchAnim()
    {
        anim.SetFloat("speed", Movement.magnitude);
    }

    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            myBag.SetActive(true);
            isOpen = !isOpen;
            myBag.SetActive(isOpen);
            InventoryManager.RefreshItem();
        }
    }



    public void UpDownController()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");
        if (Movement.x != 0)
            transform.localScale = new Vector3(Movement.x, 1, 1);
    }

    public void GetHit(float damage)
    {
        Debug.Log("被打了一下!");
    }*/
}

using UnityEngine;
[RequireComponent(typeof(BodyHit))]
public class PlayerAnimation : MonoBehaviour
{
    public bool isUpDown;
    private Animator ani;
    private Rigidbody2D rig;
    private PlayerController controller;
    private BodyHit bhit;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
        bhit = GetComponent<BodyHit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUpDown)
        {
            ani.SetFloat("speed", Mathf.Abs(rig.velocity.x));
            ani.SetFloat("velocityY", Mathf.Abs(rig.velocity.y));
            
            ani.SetBool("ground", bhit.isGround);
        }
        else
        {
            ani.SetFloat("speed", Mathf.Abs(rig.velocity.magnitude));
        }

    }
}

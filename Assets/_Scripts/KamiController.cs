using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Diagnostics;
using System.Collections;


public class KamiController : MonoBehaviour
{    

    #region Inspector fields

    [Header("Parameters")]
    [Range(0f, 25f)]
    public float MovementSpeed;

    [Range(0f, 1f)]
    public float GravityInfluence;

    [Range(0f, 100f)]
    public float JumpSpeed;
    [Range(0f, 10f)]
    public float GroundDist;

    [Range(0f, 5f)]
    public float AttackCooldown;
    [Range(0f, 5f)]
    public float AttackCooldown2;
    [Range(0f, 5f)]
    public float AttackCooldown3;

    [Header("References")]
    public GameObject Weapon;
    private BoxCollider WeaponCollider;
    public Animator Animator;
    //public CharacterController Character;
    public Transform groundCheck;
    private Rigidbody rb;

    [Header("Skills")]
    protected bool jump2 = false;
    protected bool jump3 = false;
    //public KamiJump sJumpl;
    //public kami02 skill02;
    // Change of the CharacterController for this RigidBody
    #endregion

    #region Properties
    public bool grounded = true;

    public bool IsJumping { get; private set; }
    public bool IsAttacking { get; private set; }
    //public bool IsSkill02 { get; private set; }

    #endregion

    #region Local variables

    protected bool IsSkill02;

    #endregion

    // Use this for initialization
    void Start () {
        IsJumping = false;
        rb.velocity = new Vector3(MovementSpeed, 0, 0);


    }

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        WeaponCollider = Weapon.GetComponent<BoxCollider>();
        
    }

    void FixedUpdate()
    {
        //grounded = Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, GroundDist);
        grounded = Physics.Linecast(transform.position, transform.position + Vector3.down * GroundDist);
        //rb.velocity= new Vector3(rb.velocity.x + MovementSpeed, rb.velocity.y, rb.velocity.z);
        //rb.velocity.Scale(transform.forward);
        //rb.velocity = new Vector3(transform.forward.x * (MovementSpeed + Mathf.Abs(Mathf.RoundToInt(rb.velocity.x) - MovementSpeed)), (transform.forward * MovementSpeed).y + rb.velocity.y, transform.forward.z * (MovementSpeed + Mathf.Abs(rb.velocity.z - MovementSpeed)));
        fixVelocity();
        //UnityEngine.Debug.Log(rb.velocity);
    }

    // Update is called once per frame
    void Update () {
        
        /*if (Input.GetMouseButtonDown(0)) //For hard test
        {
            performJump();
        }*/

        ProcessInputDesktop();
        ProcessInputMobile();

        //UpdateAnimationStatus();
        //CheckGroundStatus();
    }

    private void fixVelocity()
    {
        float _x, _y, _z;

        if (rb.velocity.x - MovementSpeed <= 0)
        {
            _x = MovementSpeed * transform.forward.x;
        }
        else
        {
            _x = transform.forward.x * (MovementSpeed + (Mathf.RoundToInt(rb.velocity.x) - MovementSpeed));
        }

        _y = rb.velocity.y;

        if (rb.velocity.z - MovementSpeed <= 0)
        {
            _z = MovementSpeed * transform.forward.z;
        }
        else
        {
            _z = transform.forward.z * (MovementSpeed + (Mathf.RoundToInt(rb.velocity.z) - MovementSpeed));
        }

        rb.velocity = new Vector3(_x,_y,_z);
    }


    [Conditional("UNITY_ANDROID"), Conditional("UNITY_IOS")]
    protected void ProcessInputMobile()
    {
        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject() == false)
        {
            performJump();
            //IsJumping = true;
        }
    }

    [Conditional("UNITY_STANDALONE")]
    protected void ProcessInputDesktop()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            performJump();
            //IsJumping = true;
        }
    }

    protected void UpdateAnimationStatus()
    {
        //Animator.SetBool("Jump", !grounded);
        Animator.SetBool("Attack02", IsSkill02);
        Animator.SetBool("Attack", IsAttacking);
        Animator.SetFloat("Speed", MovementSpeed);

    }

    public void performJump()
    {
        if (!IsJumping && grounded)
        {
            //grounded = false;
            UnityEngine.Debug.Log("flag Salto 1");
            
            
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(0, (JumpSpeed),0),ForceMode.Impulse);
            
            jump2 = true;
            IsJumping = true;
            
        }
        else if (jump2)
        {
            UnityEngine.Debug.Log("flag Salto 2");
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(0, (JumpSpeed),0), ForceMode.Impulse);
            jump2 = false;
            jump3 = true;
        }
        else if (jump3)
        {
            UnityEngine.Debug.Log("flag Salto 3");
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3((JumpSpeed/3),(JumpSpeed/3),0), ForceMode.Impulse);
            //rb.useGravity = false;
            
            jump3 = false;
        }

        if (rb.velocity.y < 1)
        {
            //rb.useGravity = true;
        }

        if (grounded)
        {
            rb.velocity = new Vector3(0, 0, 0);
            IsJumping = false;
            Animator.SetBool("Jump", false);
            Animator.SetFloat("Speed", MovementSpeed);
        }

        



    }



    public void PerformSkill01()
    {
        if (IsAttacking == false)
        {
            StartCoroutine(skill01Routine(AttackCooldown, finishedCallback: () =>
            {
                IsAttacking = false;
                WeaponCollider.isTrigger = false;
            })
            );
        }
    }


    public IEnumerator skill01Routine(float cooldown, System.Action finishedCallback = null)
    {
        if (!IsAttacking)
        {
            IsAttacking = true;
            WeaponCollider.enabled = true;
            Weapon.GetComponent<WeaponController>().setDamage(5);
            Animator.SetBool("Attack", IsAttacking);
        }
        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            WeaponCollider.enabled = false;
            IsAttacking = false;
        }

        yield return new WaitForSeconds(cooldown);

        if (finishedCallback != null)
        {
            finishedCallback();
        }



    }


}

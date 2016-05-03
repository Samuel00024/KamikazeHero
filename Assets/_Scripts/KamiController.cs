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
    protected Vector3 v_direction = new Vector3(1, 0, 0);

    #endregion

    #region Local variables

    protected bool IsSkill02;

    #endregion

    // Use this for initialization
    void Start () {
        IsJumping = false;
        
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
        
       rb.velocity = new Vector3((transform.forward * MovementSpeed).x, (transform.forward * MovementSpeed).y + rb.velocity.y , (transform.forward * MovementSpeed).z);

    }

    // Update is called once per frame
    void Update () {
        
        if (Input.GetMouseButtonDown(0))
        {
            performJump();
        }

        //UpdateAnimationStatus();
        //CheckGroundStatus();
    }



    [Conditional("UNITY_ANDROID"), Conditional("UNITY_IOS")]
    protected void ProcessInputMobile()
    {
        if (IsJumping == false && Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject() == false)
        {
            IsJumping = true;
        }
    }

    [Conditional("UNITY_STANDALONE")]
    protected void ProcessInputDesktop()
    {
        if (IsJumping == false && Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            performJump();
            IsJumping = true;
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
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(0, (JumpSpeed * 100)));
            jump2 = true;
            IsJumping = true;
            
        }
        else if (jump2)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(0, (JumpSpeed * 100)));
            jump2 = false;
            jump3 = true;
        }else if (jump3)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3((JumpSpeed * 100),(JumpSpeed * 30)));
            rb.useGravity = false;
            
            jump3 = false;
        }

        if (rb.velocity.y < 1)
        {
            rb.useGravity = true;
        }

        if (grounded)
        {
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

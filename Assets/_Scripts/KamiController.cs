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
    protected bool jump1 = true;
    protected bool jump2 = false;
    protected bool jump3 = false;
    protected bool s1_1 = true; //Can tou use this skill?
    protected bool s1_2 = false;
    protected bool s1_3 = false;
    protected bool s2_1 = true;
    protected bool s2_2 = false;
    protected bool s2_3 = false;


    // Change of the CharacterController for this RigidBody
    #endregion

    #region Properties
    public bool grounded = true;

    public bool IsSkill01; //You are using this skill
    public bool IsSkill02;
    public bool IsJumping2;
    public bool IsJumping3;


    #endregion

    #region Local variables


    #endregion

    // Use this for initialization
    void Start () {

        IsSkill01 = IsSkill02 = IsJumping2 = IsJumping3 = false;

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
        
        //UnityEngine.Debug.Log(rb.velocity);
        UpdateAnimationStatus();
        fixVelocity();
    }

    // Update is called once per frame
    void Update () {


        

        ProcessInputDesktop();
        ProcessInputMobile();
        //CheckGroundStatus();
    }

    private void fixVelocity()
    {
        float _x, _y, _z;

        if (rb.velocity.x < MovementSpeed )
        {
            if(rb.velocity.x >= 0) { //Velocidad por debajo de lo normal pero positiva
                _x = MovementSpeed * transform.forward.x;
            }
            else
            {
                _x = transform.forward.x * (MovementSpeed + (Mathf.RoundToInt(rb.velocity.x) - MovementSpeed));
            }
        }
        else
        {
            _x = transform.forward.x * (MovementSpeed + (Mathf.RoundToInt(rb.velocity.x) - MovementSpeed));
        }

        _y = rb.velocity.y;

        if (rb.velocity.z < MovementSpeed)
        {
            if (rb.velocity.z >= 0)
            {
                _z = MovementSpeed * transform.forward.z;
            }
            else
            {
                _z = transform.forward.z * (MovementSpeed + (Mathf.RoundToInt(rb.velocity.z) - MovementSpeed));
            }
        }
        else
        {
            _z = transform.forward.z * (MovementSpeed + (Mathf.RoundToInt(rb.velocity.z) - MovementSpeed));
        }

        rb.velocity = new Vector3(_x,_y,_z);
        //rb.velocity = new Vector3((rb.velocity.x*transform.forward.x)*MovementSpeed, _y, (rb.velocity.z*transform.forward.z)*MovementSpeed);
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

        Animator.SetBool("Skill01", IsSkill01);
        Animator.SetBool("Skill01-2", !s1_2);
        Animator.SetBool("Skill01-3", !s1_3);

        Animator.SetBool("Skill02", IsSkill02);
        Animator.SetBool("Skill02-2", !s2_2);
        Animator.SetBool("Skill02-3", !s2_3);

        Animator.SetFloat("Speed", MovementSpeed);
        Animator.SetBool("InGround", grounded);
        Animator.SetBool("Jump2", IsJumping2);
        Animator.SetBool("Jump3", IsJumping3);

        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Skill01"))
        {
            s1_1 = false;
        }
        else
        {
            IsSkill01 = false;
        }
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Skill01-2"))
        {
            s1_2 = false;
        }
        else
        {
            IsSkill01 = false;
        }
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Skill01-3"))
        {
            s1_3 = false;
        }
        else
        {
            IsSkill01 = false;
        }

        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Skill02"))
        {
            s2_1 = false;
        }
        else
        {
            IsSkill02 = false;
        }

        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Skill02-2"))
        {
            s2_2 = false;
        }
        else
        {
            IsSkill02 = false;
        }

        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Skill02-3"))
        {            
            s2_3 = false;
        }
        else
        {
            IsSkill02 = false;
        }

        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Running")||
            Animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping")||
            Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            WeaponCollider.enabled = false;
        }
        else
        {
            WeaponCollider.enabled = true;
        }



    }

    public void performJump()
    {
        if (jump1 && grounded)
        {
            //grounded = false;
            UnityEngine.Debug.Log("flag Salto 1");
            
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(0, (JumpSpeed),0),ForceMode.Impulse);
            
            jump2 = true;
            jump1 = false;
            
        }
        else if (jump2 && !grounded)
        {
            UnityEngine.Debug.Log("flag Salto 2");
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(0, (JumpSpeed),0), ForceMode.Impulse);
            jump2 = false;
            jump3 = true;
            IsJumping2 = true;
        }
        else if (jump3 && !grounded)
        {
            
            UnityEngine.Debug.Log("flag Salto 3");
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3((JumpSpeed/3),(JumpSpeed/3),0), ForceMode.Impulse);
                        
            jump3 = false;
            IsJumping2 = false;
            IsJumping3 = true;
        }

        if (grounded)
        {
            rb.velocity = new Vector3(0, 0, 0);
            jump1 = true;
            IsJumping2 = false;
            IsJumping3 = false;
        }


    }



    public void PerformSkill01()
    {
        if (s1_1)
        {
            WeaponCollider.enabled = true;
            Weapon.GetComponent<WeaponController>().setDamage(5);
            s1_2 = true;
            s1_1 = false;
            IsSkill01 = true;
            UnityEngine.Debug.Log("haciendo skill1");
            StartCoroutine(skill1_1Routine(AttackCooldown, finishedCallback: () =>
            {
                s1_1 = true;
                UnityEngine.Debug.Log("Skill1 Recargada");
            })
            );
        }else if (s1_2)
        {
            UnityEngine.Debug.Log("haciendo skill1-2");
            WeaponCollider.enabled = true;
            Weapon.GetComponent<WeaponController>().setDamage(5);
            s1_3 = true;
            s1_2 = false;
            IsSkill01 = true;
        }
        else if (s1_3)
        {
            WeaponCollider.enabled = true;
            Weapon.GetComponent<WeaponController>().setDamage(15);
            s1_3 = false;
            IsSkill01 = true;
            UnityEngine.Debug.Log("haciendo skill1-3");
        }
    }

    public void PerformSkill02()
    {
        if (s2_1)
        {
            IsSkill02 = true;
            UnityEngine.Debug.Log("haciendo skill2");

            WeaponCollider.enabled = true;
            Weapon.GetComponent<WeaponController>().setDamage(5);
            s2_2 = true;
            s2_1 = false;

            StartCoroutine(skill2_1Routine(AttackCooldown2, finishedCallback: () =>
            {
                s2_1 = true;
                UnityEngine.Debug.Log("Skill2 Recargada");
            })
            );
        }
        else if (s2_2)
        {
            IsSkill02 = true;
            UnityEngine.Debug.Log("haciendo skill2-2");
            rb.AddForce(new Vector3(5, 0, 0), ForceMode.Impulse);
            WeaponCollider.enabled = true;
            Weapon.GetComponent<WeaponController>().setDamage(5);
            s2_3 = true;
            s2_2 = false;

        }
        else if (s2_3)
        {
            IsSkill02 = true;
            UnityEngine.Debug.Log("haciendo skill2-3");
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            rb.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);
            s2_3 = false;
            WeaponCollider.enabled = true;
            Weapon.GetComponent<WeaponController>().setDamage(10);
        }

    }

    #region Skill 1 lv 1 to lv3

    //LV1
    public IEnumerator skill1_1Routine(float cooldown, System.Action finishedCallback = null)
    {
        yield return new WaitForSeconds(cooldown);

        if (finishedCallback != null)
        {
            finishedCallback();
        }

    }

    #endregion    

    #region Skill 2

    //LV1
    public IEnumerator skill2_1Routine(float cooldown, System.Action finishedCallback = null)
    {

        yield return new WaitForSeconds(cooldown);

        if (finishedCallback != null)
        {
            finishedCallback();
        }

    }
    #endregion


}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Diagnostics;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    #region Inspector fields

    [Header("Parameters")]
    [Range(0f, 25f)]
    public float MovementSpeed;

    [Range(0f, 1f)]
    public float GravityInfluence;

    [Range(0f, 100f)]
    public float JumpSpeed;

    [Range(0f, 5f)]
    public float AttackCooldown;
    public float AttackCooldown2;
    public float AttackCooldown3;

    [Header("References")]
    public GameObject Weapon;
    public BoxCollider WeaponCollider;
    public Animator Animator;
    public CharacterController Character;
    public kami02 skill02;

    #endregion

    #region Properties

    public bool IsJumping { get; private set; }
    public bool IsAttacking { get; private set; }
    //public bool IsSkill02 { get; private set; }

    #endregion

    #region Local variables

    protected Vector3 m_MovementDirection;
    protected bool IsSkill02;

    #endregion

    #region Public Methods

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

    public void PerformSkill02()
    {
        if (IsSkill02 == false)
        {
            skill02.lv1Ini(ref IsSkill02, ref WeaponCollider, ref MovementSpeed);
            StartCoroutine(skill02.skillRoutine(AttackCooldown, finishedCallback: () =>
            {
                skill02.lv1Fin(ref IsSkill02, ref WeaponCollider, ref MovementSpeed);
            })
            );
        }
    }


    public void PerformAbility(string text)
    {
        SceneManager.LoadScene("main");
    }

    #endregion

    #region Unity Methods

    void Update()
    {
        if (Character.isGrounded)
        {
            IsJumping = false;

            Animator.SetBool("Jump", false);

            m_MovementDirection = new Vector3(0, 0, MovementSpeed);
            m_MovementDirection = transform.TransformDirection(m_MovementDirection);
        }

        ProcessInputDesktop();
        ProcessInputMobile();

        UpdateAnimationStatus();
        UpdateMovement();
    }

    #endregion

    #region Local methods

    protected void UpdateAnimationStatus()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && IsAttacking)
        {
            IsAttacking = false;
            WeaponCollider.isTrigger = false;
        }

        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack02")  && IsSkill02)
        {
            IsSkill02 = false;
            WeaponCollider.isTrigger = false;
        }
        Animator.SetBool("Attack02", IsSkill02);
        Animator.SetBool("Attack", IsAttacking);
        Animator.SetFloat("Speed", MovementSpeed);

    }

    protected void UpdateMovement()
    {
        m_MovementDirection.y -= GravityInfluence;
        Character.Move(m_MovementDirection * Time.deltaTime);
    }

    [Conditional("UNITY_ANDROID"), Conditional("UNITY_IOS")]
    protected void ProcessInputMobile()
    {
        if (IsJumping == false && Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject() == false)
        {
            m_MovementDirection.y = JumpSpeed;
            IsJumping = true;

            Animator.SetBool("Jump", true);
        }
    }

    [Conditional("UNITY_STANDALONE")]
    protected void ProcessInputDesktop()
    {
        if (IsJumping == false && Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            m_MovementDirection.y = JumpSpeed;
            IsJumping = true;

            Animator.SetBool("Jump", true);
        }
    }

    #endregion

    #region Coroutines

    public IEnumerator skill01Routine(float cooldown, System.Action finishedCallback = null)
    {
        IsAttacking = true;
        WeaponCollider.isTrigger = true;

        yield return new WaitForSeconds(cooldown);

        if (finishedCallback != null)
        {
            finishedCallback();
        }
    }



    #endregion
}

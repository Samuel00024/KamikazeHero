using UnityEngine;
using System.Collections;

public class kami02 : MonoBehaviour {

    #region Inspector fields

    [Header("Parameters")]
    [Range(0f, 100f)]
    public float vInpulse;
    #endregion

    #region Local variables
    private float vIni;
    #endregion

    #region Unity Methods
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    #endregion

    public IEnumerator skillRoutine(float cooldown, System.Action finishedCallback = null)
    {
        yield return new WaitForSeconds(cooldown);

        if (finishedCallback != null)
        {
            finishedCallback();
        }
    }
    public void lv1Ini(ref bool IsSkill02, ref BoxCollider WeaponCollider, ref float MovementSpeed)
    {
        vIni = MovementSpeed;
        IsSkill02 = true;
        WeaponCollider.isTrigger = true;
        MovementSpeed = vIni + vInpulse;
    }
    public void lv1Fin(ref bool IsSkill02, ref BoxCollider WeaponCollider, ref float MovementSpeed)
    {
        MovementSpeed = vIni;
        IsSkill02 = false;
        WeaponCollider.isTrigger = false;
    }

}

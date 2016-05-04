using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    #region Inspector fields

    [Header("Parameters")]
    [Range(1f, 100f)]
    public int hp;
    [Range(0f, 100f)]
    public int exp;
    #endregion
    // Use this for initialization
    void Start () {
	
	}

    void LateUpdate()
    {

    }
	
    public int sub(int damage)
    {
        hp -= damage;
        if (hp < 1)
        {
            return exp;
        }
        else
        {
            return 0;
        }
    }
}

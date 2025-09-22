using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowAnimator : MonoBehaviour
{
    [SerializeField]
    public CrowScript crowScript;
    [SerializeField]
    public PlayerSwitch playerSwitchScript;
    [SerializeField]
    private Animator crowAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        crowAnimator.SetFloat("YVelocity", crowScript.CrowRb.velocity.y);
        crowAnimator.SetBool("isFlying", crowScript.isFlying);
        crowAnimator.SetBool("isAttached", playerSwitchScript.isAttached);
    }
}

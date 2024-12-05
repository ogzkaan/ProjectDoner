using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private Animator mAnimator;
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DolapController()
    {
        if (this.gameObject.transform.CompareTag("Dolap"))
        {
            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("DolapAc"))
            {
                mAnimator.SetBool("IsOpen", false);
            }
            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("DolapKapa"))
            {
                mAnimator.SetBool("IsOpen", true);
            }
        }
        if (this.gameObject.transform.CompareTag("Door"))
        {

            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorAc"))
            {
                mAnimator.SetBool("IsOpen", false);
            }
            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorKapa"))
            {
                mAnimator.SetBool("IsOpen", true);
            }
        }
        if (this.gameObject.transform.CompareTag("Fritoz"))
        {
            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("FritozAc"))
            {
                mAnimator.SetBool("IsOpen", false);
            }
            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("FritozKapa"))
            {
                mAnimator.SetBool("IsOpen", true);
            }
        }
    }

}

using UnityEngine;

public class FritozAnimation : MonoBehaviour
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
    public void FritozController()
    {
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

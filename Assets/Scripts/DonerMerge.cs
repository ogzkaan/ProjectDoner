using UnityEngine;

public class DonerMerge : MonoBehaviour
{
    public GameObject donerEkmek;
    public Transform donerEkmekPlace;

    DonerCheck donerCheck;
    currentObject currentObject;

    private bool is_Match=false;
    void Start()
    {
        currentObject = GetComponent<currentObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        is_Match = true;
        if (!currentObject.onHand)
        {
            if (collision.gameObject.tag == "DonerEkmek")
            {
                foreach (Transform child in collision.gameObject.transform)
                {
                    if(child.tag != gameObject.transform.tag)
                    {
                        is_Match= false;
                    }
                }
                if (!is_Match)
                {
                    donerCheck = collision.gameObject.GetComponent<DonerCheck>();

                   if(donerCheck.GetType().GetField(gameObject.tag).GetValue(donerCheck).ToString()=="False")
                   {
                        donerEkmekPlace = collision.gameObject.transform.Find("DonerPlace");
                        var inst = Instantiate(donerEkmek, donerEkmekPlace.position, donerEkmekPlace.rotation);
                        inst.transform.parent = collision.gameObject.transform;
                        donerCheck.donerCheckIngredient(gameObject);
                        Destroy(gameObject);
                    }              
                }
            }
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        is_Match = true;
        if (other.gameObject.tag == "DonerEkmek")
        {
            foreach (Transform child in other.gameObject.transform)
            {
                if (child.tag != gameObject.transform.tag)
                {
                    is_Match = false;
                }
            }
        }
        if (!is_Match)
        {
            donerCheck = other.gameObject.GetComponent<DonerCheck>();
            Debug.Log(donerCheck);

            if (donerCheck.GetType().GetField(gameObject.tag).GetValue(donerCheck).ToString() == "False")
            {
                donerEkmekPlace = other.gameObject.transform.Find("DonerPlace");
                var inst = Instantiate(donerEkmek, donerEkmekPlace.position, donerEkmekPlace.rotation);
                inst.transform.parent = other.gameObject.transform;
                donerCheck.donerCheckIngredient(gameObject);
            }
        }
    }
}

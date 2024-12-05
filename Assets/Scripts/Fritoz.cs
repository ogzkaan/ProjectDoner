using UnityEditor.Animations;
using UnityEngine;

public class Fritoz : MonoBehaviour
{
    private currentObject currentObject;
    private Transform FritozPlace;
    public GameObject Patates;
    public Animator Animator;
    private bool isEmpty;
    private void Start()
    {
        currentObject = GetComponent<currentObject>();
        isEmpty = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (!currentObject.onHand)
        {
            if (collision.gameObject.tag=="Fritoz" && isEmpty)
            {
                isEmpty = false;
                FritozPlace = collision.gameObject.transform.Find("FritozPlace");
                var inst = Instantiate(Patates, FritozPlace.transform.position, FritozPlace.transform.rotation);
                inst.transform.parent = collision.gameObject.transform;
                Destroy(gameObject);
                
            }
        }
    }

}

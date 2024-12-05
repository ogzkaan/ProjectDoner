using Unity.VisualScripting;
using UnityEngine;

public class currentObject : MonoBehaviour
{

    public enum ObjectType
    {
        Grab,
        Equip,
        Spawner,
        Animation,
        None
    };

    private Rigidbody objectRigidbody;
    private Collider objectColldier;
    private Transform objectGrabPointTransform;
    public GameObject parent;
    public ObjectType objectType;
    public ParticleSystem bottle;
    

    Vector3 newPosition;
    Quaternion newRotation;

    Outline outline;
    SpawnObject spawnObject;

    public bool onHand;
    [Header("UI")]
    public string UIname;

    [Header("Doner")]
    public bool donerClosed;
    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        objectColldier = GetComponent<Collider>();
        outline = GetComponent<Outline>();
        spawnObject = GetComponent<SpawnObject>();

        onHand = false;

    }
    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {

            if (objectType.ToString() == "Grab")
            {
                newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * 10f);
                newRotation = Quaternion.Lerp(transform.rotation, objectGrabPointTransform.rotation, Time.deltaTime * 10f);
                objectRigidbody.angularVelocity = Vector3.zero;
                objectRigidbody.linearVelocity = Vector3.zero;
                objectRigidbody.MovePosition(newPosition);
                objectRigidbody.MoveRotation(newRotation);
            }

        }

    }
    public void Grab(Transform objectGrabPointTransform, Transform objectEquipPointTransform, LayerMask layerMask)
    {
        
        if (objectType.ToString() == "Equip")
        {
            this.gameObject.transform.SetParent(parent.transform, false);
            this.gameObject.transform.localPosition = Vector3.zero;
            this.gameObject.transform.localRotation = Quaternion.identity;
            objectRigidbody.isKinematic = true;
            objectRigidbody.useGravity = false;
            objectColldier.isTrigger = true;
        }
        else
        {
            this.objectGrabPointTransform = objectGrabPointTransform;
            objectRigidbody.useGravity = false;
        }
        onHand = true;

    }
    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        objectRigidbody.isKinematic = false;
        if (gameObject.transform.childCount>0)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.transform.TryGetComponent(out Collider childCol);
                if (childCol)
                {
                    childCol.isTrigger = false;
                }
               
            }
        }
        else
        {
            objectColldier.isTrigger = false;
        }
        this.gameObject.transform.SetParent(null);
        onHand = false;
    }
    public void enableOutline()
    {
        if (!outline.enabled)
        {
            outline.enabled = true;
        }
        
    }
    public void disableOutline()
    {
        if (outline.enabled)
        {
            outline.enabled = false;
        }
        
    }
    public void DonerCloseEnable()
    {
        donerClosed = true;
    }
    public void DonerCloseDisable()
    {
        donerClosed=false;
    }
    public void ParticlePlay()
    {
        bottle.Play();
    }
    public void ParticleStop()
    {
        bottle.Stop();
    }
    public void Spawn(GameObject gameObject)
    {
        spawnObject.Spawn(gameObject);
    }
}

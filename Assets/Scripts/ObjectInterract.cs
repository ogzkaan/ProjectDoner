using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;
using static Models;
using static UnityEngine.Rendering.DebugUI.Table;
using static UnityEngine.Timeline.AnimationPlayableAsset;

public class ObjectInterract : MonoBehaviour
{

    private PlayerInput playerinput;
    private CharMovement charMovement;
    Outline outline;


    [Header("Ref")]
    public Transform cameraHolder;
    public Transform playerCamera;


    [Header("Grabbing")]
    [SerializeField] private LayerMask interractLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private Transform objectEquipPointTransform;
    [SerializeField] private bool objectPicked;
    [SerializeField] public float pickUpDistance;

    [Header("UI")]
    public TMP_Text itemName;

    RaycastHit hit;
    private currentObject currentObject;
    private currentObject NewcurrentObject;
    private currentObject currentObjectOutline;
    private currentObject newObjectOutline;
    private currentObject donerEkmek;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerinput = new PlayerInput();
        charMovement = new CharMovement();

        playerinput.Player.PickUp.performed += ctx => InterractObject();
        playerinput.Player.Interract.performed += ctx => Cut();
        playerinput.Player.Pour.performed += ctx => Pour();
        playerinput.Player.Pour.canceled += ctx => PourEnded();
        playerinput.Player.DonerClose.performed += ctx => DonerClose();

        playerinput.Enable();

    }

    // Update is called once per frame
    void Update()
    {
        OutlineControlEnable();
    }
    private void InterractObject()
    {
        if (objectPicked == false)
        {
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, pickUpDistance, interractLayerMask))
            {
                if (hit.transform.TryGetComponent(out currentObject))
                {
                    if (currentObject.objectType.ToString() == "Grab" || currentObject.objectType.ToString() == "Equip")
                    {
                        if (objectPicked == false)
                        {
                            currentObject.Grab(objectGrabPointTransform, objectEquipPointTransform, hit.transform.gameObject.layer);
                            objectPicked = true;
                        }
                        else if (objectPicked == true)
                        {
                            currentObject.Drop();
                            currentObject = null;
                            objectPicked = false;
                        }
                    }
                    else if (currentObject.objectType.ToString() == "Spawner")
                    {
                        if (hit.transform.TryGetComponent(out NewcurrentObject))
                        {
                            NewcurrentObject.Spawn(currentObject.gameObject);
                        }
                    }
                    else if (currentObject.objectType.ToString() == "Animation")
                    {
                        var animation=currentObject.GetComponent<DoorAnimation>();
                        animation.DolapController();
                    }
                }
            }
            
        }
        else if (objectPicked == true)
        {
            currentObject.Drop();
            currentObject = null;
            objectPicked = false;
            
        }
    }
    private void Pour()
    {
        
        if (currentObject.CompareTag("Bottle"))
        {
            Vector3 rot = objectGrabPointTransform.rotation.eulerAngles;
            rot = new Vector3(rot.x+180, rot.y, rot.z);
            objectGrabPointTransform.eulerAngles =rot;
            currentObject.ParticlePlay();
        }
    }
    private void PourEnded()
    {
        if (currentObject.CompareTag("Bottle"))
        {
            Vector3 rot = objectGrabPointTransform.rotation.eulerAngles;
            rot = new Vector3(rot.x - 180, rot.y, rot.z);
            objectGrabPointTransform.eulerAngles = rot;
            currentObject.ParticleStop();
        }
    }
    private void Cut()
    {

        if (currentObject.objectType.ToString() == "Equip")
        {
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, pickUpDistance, interractLayerMask))
            {
                if (hit.transform.TryGetComponent(out NewcurrentObject))
                {
                    NewcurrentObject.Spawn(currentObject.gameObject);
                }
            }
        }
    }
    private void DonerClose()
    {
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, pickUpDistance, interractLayerMask))
        {
            if (hit.transform.tag=="DonerEkmek")
            {
                donerEkmek = hit.transform.GetComponent<currentObject>();
                GameObject donerChild= donerEkmek.transform.GetChild(0).gameObject;
                if (!donerEkmek.donerClosed)
                {
                    donerChild.transform.localPosition = new Vector3(0, 0.0157f, 0f);
                    donerChild.transform.localRotation = Quaternion.Euler(new Vector3(180f, 0f, 0f));
                    donerEkmek.donerClosed = true;
                }
                else
                {
                    donerChild.transform.localPosition = new Vector3(0, 0, 0);
                    donerChild.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    donerEkmek.donerClosed = false;
                }
                
                Debug.Log("kapa");
            }
        }
    }
    private void OutlineControlEnable()
    {
        
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, pickUpDistance, interractLayerMask))
        {

            if (hit.transform.TryGetComponent(out newObjectOutline))
            {
                newObjectOutline.enableOutline();
                itemName.text = newObjectOutline.UIname;
            }
            if (currentObjectOutline != null & newObjectOutline != currentObjectOutline)
            {
                currentObjectOutline.disableOutline();
                itemName.text = null;
            }
        }
        else
        {
            if (currentObjectOutline)
            {
                newObjectOutline.disableOutline();
                newObjectOutline = null;
                itemName.text = null;
            }
        }
        currentObjectOutline = newObjectOutline;
    }

}


using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject spawnObject;
    public float amount;
    public bool despawn;

    private Transform spawnPoint;
    private Transform currentPosition;

    public float spawnTransfromOfset;
    public float spawnRotationOfset;
    public ParticleSystem particle;
    public float despawnTime;

    private float angleInRadians;
    Quaternion mirroredRotation;
    Vector2 direction;

    public string InterractTag;


    private void Awake()
    {
        
    }

    public void Spawn(GameObject Cutter)
    {
        Debug.Log(Cutter.gameObject.tag);
        if(Cutter.CompareTag(InterractTag))
        { 
            spawnPoint = gameObject.transform;
            angleInRadians = transform.eulerAngles.y * Mathf.Deg2Rad;
            direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
            if (particle)
            {
                particle.Play();
            }
            for (int i = 1; i <= amount; i++)
            {
                if (i % 2 == 0)
                {
                    Vector3 secondMirroredPosition = new Vector3(
                -direction.x * spawnTransfromOfset,
                0,
                direction.y * spawnTransfromOfset
            );
                    Instantiate(spawnObject, secondMirroredPosition + transform.position, spawnPoint.rotation);
                }
                else
                {
                    Vector3 firstMirroredPosition = new Vector3(
                direction.x * spawnTransfromOfset,
                0,
                -direction.y * spawnTransfromOfset
            );
                    Instantiate(spawnObject, transform.position + firstMirroredPosition, spawnPoint.rotation * Quaternion.Euler(0, 180f, 0));
                }



            }

            if (despawn)
            {
                Destroy(gameObject, despawnTime);
            }
        }

    }

}

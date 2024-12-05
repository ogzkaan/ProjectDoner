using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BroderCheck : MonoBehaviour

{
    [SerializeField] private List<GameObject> objectsInRange;
    public TMP_Text textMeshPro;

    private List<GameObject> ReturnObjectsInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f);
        foreach (Collider hitCol in hitColliders)
        {
            if (!objectsInRange.Contains(hitCol.gameObject))
            {
                if (hitCol.name == "Player")
                {
                    textMeshPro.text = "Amını Sikttim Nere Gidion";
                }
                else
                {
                    textMeshPro.text = null;
                }
            }
        }
        return objectsInRange;
    }
    private void FixedUpdate()
    {
        ReturnObjectsInRange();
    }
}

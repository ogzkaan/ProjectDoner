using UnityEngine;

public class SkyBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float skyBoxSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyBoxSpeed);
    }
}

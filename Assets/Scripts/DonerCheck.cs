using UnityEngine;

public class DonerCheck : MonoBehaviour
{

    public bool M_Tavuk = false;
    public bool M_Et = false;
    public bool M_Domates = false;
    public bool M_Marul = false;
    public bool M_Mayonez = false;
    public bool M_Ketchup = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void donerCheckIngredient(GameObject addedIngredient)
    {
        switch (addedIngredient.tag)
        {
            case "M_Tavuk":
                M_Tavuk = true;
                break;
            case "M_Et":
                M_Et = true;
                break;
            case "M_Domates":
                M_Domates = true;
                break;
            case "M_Marul":
                M_Marul = true;
                break;
            case "M_Mayonez":
                M_Mayonez = true;
                break;
            case "M_Ketchup":
                M_Ketchup = true;
                break;
        }
    }

}

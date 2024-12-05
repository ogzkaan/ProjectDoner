using UnityEngine;
using System;
public static class Models
{
    #region - Player -

    [Serializable]
    public class PlayerSettingsModel
    {
        [Header("ViewSettings")]

        public float xLookSens;
        public float yLookSens;

         

        public bool viewXInverted;
        public bool viewYInverted;

        [Header("Movement")]
        public float walkSpeed;

        
    }

    #endregion
}

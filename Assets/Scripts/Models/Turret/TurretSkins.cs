using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class TurretSkin
    {
        public string SkinId;
        public Material Material;
    }
    
    public class TurretSkins : MonoBehaviour
    {
        [SerializeField] private List<TurretSkin> m_Skins = new List<TurretSkin>();

        public Material GetMaterialForSkin(string skinId)
        {
            foreach (var skin in m_Skins)
            {
                if (skin.SkinId.Equals(skinId))
                    return skin.Material;
            }
            
            throw new ArgumentException($"No skin defined for {skinId}", nameof(skinId));
        }
    }
}

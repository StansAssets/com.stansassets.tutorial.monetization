using System;
using UnityEngine;

namespace Game
{
    public class GameData : IGameData
    {
        private const string k_AmmoKey = "Game_Ammo";
        private const string k_SkinPrefix = "Game_Skin_";
        
        public Action OnAmmoUpdated { get; set; }

        public int Ammo
        {
            get => PlayerPrefs.GetInt(k_AmmoKey, 10);
            set
            {
                PlayerPrefs.SetInt(k_AmmoKey, value);
                OnAmmoUpdated?.Invoke();
            }
        }

        public bool TryConsumeAmmo()
        {
            if (Ammo <= 0) return false;
            
            Ammo--;
            return true;
        }

        public void AddAmmo(int amount)
        {
            Ammo += amount;
        }

        public void UnlockSkin(string skinId)
        {
            Debug.Log("UnlockSkin: " + skinId);
            PlayerPrefs.SetInt(k_SkinPrefix + skinId, 1);
        }

        public bool IsSkinLocked(string skinId)
        {
            return !PlayerPrefs.HasKey(k_SkinPrefix + skinId);
        }
    }
}
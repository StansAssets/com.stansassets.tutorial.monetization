using System;

namespace Game
{
    public interface IGameData
    {
        Action OnAmmoUpdated { get; set; }
        int Ammo { get; }
        bool TryConsumeAmmo();
        void AddAmmo(int amount);

        void UnlockSkin(string skinId);
        bool IsSkinLocked(string skinId);
    }
}
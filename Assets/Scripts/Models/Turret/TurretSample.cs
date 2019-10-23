using Assets.Scripts.Config;
using SA.CrossPlatform.UI;
using UnityEngine;

namespace Game
{
    public class TurretSample : MonoBehaviour, IModelController
    {
        [Header("Partciles")]
        [SerializeField] private ParticleSystem m_ShootParticles = null;

        [Header("Skins")] 
        [SerializeField] private TurretSkins m_Skins = null;
        
        [Header("Turret Parts")] 
        [SerializeField] private GameObject m_Top  = null;
        [SerializeField] private GameObject m_Bottom  = null;
    
        public void Action()
        {
            if(Main.GameData.TryConsumeAmmo())
                m_ShootParticles.Play();
            else
            {
                var builder = new UM_NativeDialogBuilder("Out of Ammo", "Would you like to buy more ammo?");
                builder.SetPositiveButton("Yes!", () =>
                {
                    Main.PaymentService.AddPayment(GameConfig.k_10AmmoPack);
                });
                builder.SetNegativeButton("Maybe Next Time", () => {});

                var dialog = builder.Build();
                dialog.Show();
            }
        }

        public void SetSkin(string skinId)
        {
            var skinMaterial = m_Skins.GetMaterialForSkin(skinId);
            m_Top.GetComponent<Renderer>().material = skinMaterial;
            m_Bottom.GetComponent<Renderer>().material = skinMaterial;
        }
    }
}

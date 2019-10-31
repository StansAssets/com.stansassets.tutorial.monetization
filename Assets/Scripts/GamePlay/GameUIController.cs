using System.Collections.Generic;
using Assets.Scripts.Config;
using Game.UI;
using SA.CrossPlatform.InApp;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameUIController : MonoBehaviour, IGameUI
    {
        [Header("Ammo Panel")]
        [SerializeField] private Text m_AmmoCount = null;
        [SerializeField] private Button m_AddAmmoButton = null;
        
        [Header("Fire Panel")]
        [SerializeField] private Button m_FireButton = null;
        
        [Header("Skins")]
        [SerializeField] private Text m_SkinsTitle = null;
        [SerializeField] private SkinButton m_SkinButtonTemplate = null;
        
        private readonly List<SkinButton> m_SkinButtons = new List<SkinButton>();

        private void Start()
        {
            m_SkinsTitle.text = "Skins: (Loading.....)";
            m_SkinButtonTemplate.gameObject.SetActive(false);
            if (Main.PaymentService.IsConnected)
                RefreshSkinsOptions();
            else
                Main.PaymentService.OnServiceConnected += RefreshSkinsOptions;
            
            m_FireButton.onClick.AddListener(() =>
            {
                Main.Game.ActiveModel.Action();
            });

            UpdateAmmoCounter();
            Main.GameData.OnAmmoUpdated += UpdateAmmoCounter;
            
            m_AddAmmoButton.onClick.AddListener(() =>
            {
                Main.PaymentService.AddPayment(GameConfig.k_10AmmoPack);
            });
        }

        public void RefreshSkinsOptions()
        {
            foreach (var button in m_SkinButtons)
                Destroy(button.gameObject);
            
            m_SkinButtons.Clear();
            m_SkinsTitle.text = "Skins:";

            var offset = 150;
            //All our Non-Consumable products are skins so let's make a Button for each of the product:
            foreach (var product in Main.PaymentService.Products)
            {
                if(product.Type != UM_ProductType.NonConsumable)
                    continue;
                
                if(!product.IsActive)
                    continue;

                AddSkinOption(product, offset);
                offset += 120;
            }
        }
        
        private void UpdateAmmoCounter()
        {
            m_AmmoCount.text = $"Ammo: {Main.GameData.Ammo}";
        }

        private void AddSkinOption(UM_iProduct product, int offset)
        {
            var newSkin = Instantiate(m_SkinButtonTemplate.gameObject, Vector3.one, Quaternion.identity, m_SkinButtonTemplate.transform.parent );

            newSkin.transform.localScale = Vector3.one;
            newSkin.transform.localPosition = new Vector3(10, -offset, 0);
            newSkin.SetActive(true);

            var isLocked = Main.GameData.IsSkinLocked(product.Id);

            var newSkinButton = newSkin.GetComponent<SkinButton>();
            newSkinButton.SetUp(product.Icon, product.Title, isLocked);
            
            newSkinButton.onClick.AddListener(() =>
            {
                if(isLocked)
                    Main.PaymentService.AddPayment(product.Id);
                else
                    Main.Game.ActiveModel.SetSkin(product.Id);
            });
            
            m_SkinButtons.Add(newSkinButton);
        }
    }
}


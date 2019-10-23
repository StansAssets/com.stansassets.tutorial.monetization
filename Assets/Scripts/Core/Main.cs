using UnityEngine;

namespace Game
{
    public static class Main
    {
        private static IGame s_Game;
        private static IGameUI s_GameUI;
        private static IGameData s_GameData;
        private static IPaymentService s_PaymentService;
        public static IGame Game
        {
            get
            {
                if (s_Game != null)
                    return s_Game;
            
                //Bad example, should be replaced to something more adequate.
                s_Game = Object.FindObjectOfType<GameSceneController>();
                return s_Game;
            }
        }
        
        public static IGameUI GameUI
        {
            get
            {
                if (s_GameUI != null)
                    return s_GameUI;
            
                //Bad example, should be replaced to something more adequate.
                s_GameUI = Object.FindObjectOfType<GameUIController>();
                return s_GameUI;
            }
        }
        
        public static IGameData GameData
        {
            get
            {
                if (s_GameData != null)
                    return s_GameData;
                
                s_GameData = new GameData();
                return s_GameData;
            }
        }
        
        public static IPaymentService PaymentService
        {
            get
            {
                if (s_PaymentService != null)
                    return s_PaymentService;
                
                s_PaymentService = new PaymentService();
                return s_PaymentService;
            }
        }
    }
}


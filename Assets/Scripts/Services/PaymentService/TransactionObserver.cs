using Assets.Scripts.Config;
using SA.CrossPlatform.InApp;
using SA.CrossPlatform.UI;
using SA.Foundation.Templates;

namespace Game
{
    public class TransactionObserver : UM_iTransactionObserver
    {
        public void OnTransactionUpdated(UM_iTransaction transaction)
        {
            switch (transaction.State) {
                case UM_TransactionState.Restored:
                case UM_TransactionState.Purchased:
                    ProcessCompletedTransaction(transaction);
                    break;
                case UM_TransactionState.Deferred:
                    //Only fir iOS
                    //iOS 8 introduces Ask to Buy, which lets parents approve any 
                    //purchases initiated by children
                    //You should update your UI to reflect this deferred state, 
                    //and expect another Transaction Complete to be called again 
                    //with a new transaction state 
                    //reflecting the parent’s decision or after the transaction times out. 
                    //Avoid blocking your UI or game play while waiting 
                    //for the transaction to be updated.
                    UM_Preloader.UnlockScreen();
                    break;
                case UM_TransactionState.Failed:
                    //Our purchase flow is failed.
                    //We can unlock interface and tell user that the purchase is failed. 

                    UM_Preloader.UnlockScreen();
                    UM_InAppService.Client.FinishTransaction(transaction);
                    break;
            }
        }

        public void OnRestoreTransactionsComplete(SA_Result result) { }

        private static void ProcessCompletedTransaction(UM_iTransaction transaction)
        {
            //Our product has been successfully purchased or restored
            //So we need to provide content to our user depends on productIdentifier

            //In case you want to run your custom purchase validation, you might want to get full transaction info
            //provided by a platform. Lear how to do it here:
            //https://unionassets.com/ultimate-mobile-pro/advanced-use-cases-849


            switch (transaction.ProductId)
            {
                case GameConfig.k_10AmmoPack:
                    Main.GameData.AddAmmo(10);
                    break;
                default:
                    //All our other products are skins so, let's unlock it
                    Main.GameData.UnlockSkin(transaction.ProductId);
                    
                    //We also need to update skins UI, once skin is unlocked
                    Main.GameUI.RefreshSkinsOptions();
                    var product = UM_InAppService.Client.GetProductById(transaction.ProductId);
                    UM_DialogsUtility.ShowMessage("Thank you!", $"You can now try new {product.Title} skin!");
                break;
            }

            UM_Preloader.UnlockScreen();
            UM_InAppService.Client.FinishTransaction(transaction);
        }

    }
}
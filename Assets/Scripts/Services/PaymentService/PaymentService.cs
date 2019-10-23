using System;
using System.Collections.Generic;
using SA.CrossPlatform.InApp;
using SA.CrossPlatform.UI;
using UnityEngine;

namespace Game
{
    public class PaymentService : IPaymentService
    {
        private readonly UM_iInAppClient m_Client;

        public bool IsConnected { get; private set; }
        public Action OnServiceConnected { get; set; }
        public IEnumerable<UM_iProduct> Products => m_Client.Products;
        
        public PaymentService()
        {
            m_Client = UM_InAppService.Client;
            var observer = new TransactionObserver();
            m_Client.SetTransactionObserver(observer);
            
            m_Client.Connect(connectionResult => 
            {
                if(connectionResult.IsSucceeded)
                    OnServiceConnected?.Invoke();
                else
                    UM_DialogsUtility.ShowMessage("Payment Service Connection Failed", connectionResult.Error.FullMessage);
                
                IsConnected = connectionResult.IsSucceeded;
            });
        }

        public void AddPayment(string productId)
        {
            UM_Preloader.LockScreen();
            UM_InAppService.Client.AddPayment(productId);
        }
    }
}


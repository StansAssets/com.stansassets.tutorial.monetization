using System;
using System.Collections.Generic;
using SA.CrossPlatform.InApp;

namespace Game
{
    public interface IPaymentService
    {
        bool IsConnected { get; }
        Action OnServiceConnected { get; set; }
        IEnumerable<UM_iProduct> Products { get; }
        void AddPayment(string productId);
    }
}
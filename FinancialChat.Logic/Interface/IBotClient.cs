using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialChat.Logic.Interface
{
    public interface IBotClient
    {
        string Call(string message);

        void Close();
    }
}

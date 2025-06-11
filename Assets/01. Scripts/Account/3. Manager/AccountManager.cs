using UnityEngine;

public class AccountManager : Singleton<AccountManager>
{


    public bool TryRegister(string email, string nickname, string password)
    {
        return false;
    }

    public bool TryLogin(string email, string password)
    {
        return false;
    }
}

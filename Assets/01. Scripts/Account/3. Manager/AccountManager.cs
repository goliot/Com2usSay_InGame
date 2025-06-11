using UnityEngine;

public class AccountManager : Singleton<AccountManager>
{
    private readonly string SALT = "123456";

    private AccountRepository _repository;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        _repository = new AccountRepository();
    }

    public bool TryRegister(string email, string nickname, string password, out string message)
    {
        if (_repository.Find(email) != null)
        {
            message = "이미 사용 중인 이메일입니다.";
            return false;
        }

        if (Account.TryCreate(email, nickname, password, out Account account, out string error))
        {
            message = "회원가입에 성공했습니다.";
            _repository.Save(new AccountDTO(account.Email, nickname, SHA256Encryption.Encrypt(account.Password + SALT)));
            return true;
        }
        else
        {
            message = error;
            return false;
        }
    }

    public bool TryLogin(string email, string password, out string message)
    {
        AccountSaveData accountSaveData = _repository.Find(email);
        if (accountSaveData != null)
        {
            string encryptedInput = SHA256Encryption.Encrypt(password + SALT);
            if (accountSaveData.Password == encryptedInput)
            {
                message = "성공";
                return true;
            }
        }

        message = "이메일 또는 비밀번호가 일치하지 않거나 존재하지 않는 계정입니다.";
        return false;
    }

}

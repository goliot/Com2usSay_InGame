using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Account
{
    public string Email { get; }
    public string Nickname { get; }
    public string Password { get; }

    private Account(string email, string nickname, string password)
    {
        Email = email;
        Nickname = nickname;
        Password = password;
    }

    // 중복 이메일 체크용
    //private static readonly HashSet<string> _registeredEmails = new();

    public static bool TryCreate(string email, string nickname, string password, out Account account, out string errorMessage)
    {
        account = null;
        errorMessage = null;

        // 이메일 검증
        var emailSpecification = new AccountEmailSpecification();
        if(!emailSpecification.IsSatisfiedBy(email))
        {
            errorMessage = emailSpecification.ErrorMessage;
            return false;
        }

        //if (!_registeredEmails.Add(email.ToLowerInvariant()))
        //{
        //    errorMessage = "이미 사용 중인 이메일입니다.";
        //    return false;
        //}

        // 닉네임 검증
        var nicknameSpecification = new AccountNicknameSpecification();
        if(!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            errorMessage = nicknameSpecification.ErrorMessage;
            return false;
        }

        // 패스워드 검증
        var passwordSpecification = new AccountPasswordSpecification();
        if(!passwordSpecification.IsSatisfiedBy(password))
        {
            errorMessage = passwordSpecification.ErrorMessage;
            return false;
        }

        // 생성
        account = new Account(email, nickname, password);
        return true;
    }
}

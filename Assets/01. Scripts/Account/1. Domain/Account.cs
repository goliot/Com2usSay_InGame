using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Account
{
    // ────── 정규표현식 정의 ──────
    // 이메일: 일반적인 RFC‑5322 서브셋(간단 버전)
    private static readonly Regex _emailRegex =
        new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
                  RegexOptions.Compiled);

    // 닉네임: 2~7자, 한글 또는 영문만, 금칙어 미포함
    private static readonly Regex _nicknameRegex =
        new Regex(@"^(?!.*(바보|멍청이|운영자|김홍일))[가-힣A-Za-z]{2,7}$",
                  RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // 비밀번호: 임의 문자 6~12자
    private static readonly Regex _passwordRegex =
        new Regex(@"^.{6,12}$", RegexOptions.Compiled);

    // ────── 중복 이메일 체크용 (예시) ──────
    private static readonly HashSet<string> _registeredEmails = new HashSet<string>();

    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    public Account(string email, string nickname, string password)
    {
        /* ────── 이메일 검증 ────── */
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new Exception("이메일은 비어있을 수 없습니다.");
        }
        if (!_emailRegex.IsMatch(email))
        {
            throw new Exception("올바른 이메일 형식이 아닙니다.");
        }
        if (!_registeredEmails.Add(email.ToLowerInvariant()))
        {
            throw new Exception("이미 사용 중인 이메일입니다.");
        }

        /* ────── 닉네임 검증 ────── */
        if (string.IsNullOrWhiteSpace(nickname))
        {
            throw new Exception("닉네임은 비어있을 수 없습니다.");
        }
        if (!_nicknameRegex.IsMatch(nickname))
        {
            throw new Exception("닉네임은 2~7자의 한글·영문만 가능하며, 부적절한 단어를 포함할 수 없습니다.");
        }

        /* ────── 비밀번호 검증 ────── */
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new Exception("패스워드는 비어있을 수 없습니다.");
        }
        if (!_passwordRegex.IsMatch(password))
        {
            throw new Exception("패스워드는 6자 이상 12자 이하여야 합니다.");
        }

        /* ────── 최종 할당 ────── */
        Email = email;
        Nickname = nickname;
        Password = password;
    }
}
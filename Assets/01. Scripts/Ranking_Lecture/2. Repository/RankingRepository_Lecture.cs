using System.Collections.Generic;
using UnityEngine;

public class RankingRepository_Lecture
{
    public List<RankingSaveData_Lecture> Load()
    {
        // 데이터가 아직 없지만 ..
        // 개발 단계에서 데이터가 필요하다면.. Mocking 기법을 쓴다.
        // PlayerPrefs대신 '가짜 데이터 반환'
        return new List<RankingSaveData_Lecture>()
        {
            new RankingSaveData_Lecture(1813, "test1@test.com", "냉철한토끼65"),
            new RankingSaveData_Lecture(2721, "test2@test.com", "빛나는호랑이922"),
            new RankingSaveData_Lecture(2960, "test3@test.com", "달콤한햄스터489"),
            new RankingSaveData_Lecture(2263, "test4@test.com", "따뜻한토끼754"),
            new RankingSaveData_Lecture(1552, "test5@test.com", "귀여운여우451"),
            new RankingSaveData_Lecture(2086, "test6@test.com", "행복한고양이621"),
            new RankingSaveData_Lecture(2065, "test7@test.com", "따뜻한햄스터558"),
            new RankingSaveData_Lecture(1211, "test8@test.com", "우주고양이980"),
            new RankingSaveData_Lecture(2139, "test9@test.com", "무서운사자570"),
            new RankingSaveData_Lecture(1469, "test10@test.com", "우주토끼732"),
            new RankingSaveData_Lecture(2786, "test11@test.com", "행복한여우85"),
            new RankingSaveData_Lecture(2850, "test12@test.com", "귀여운토끼586"),
            new RankingSaveData_Lecture(3100, "test13@test.com", "냉철한늑대739"),
            new RankingSaveData_Lecture(3034, "test14@test.com", "냉철한여우560"),
            new RankingSaveData_Lecture(2446, "test15@test.com", "빛나는고양이474"),
            new RankingSaveData_Lecture(3175, "test16@test.com", "따뜻한여우884"),
            new RankingSaveData_Lecture(1056, "test17@test.com", "귀여운곰674"),
            new RankingSaveData_Lecture(2492, "test18@test.com", "행복한햄스터538"),
            new RankingSaveData_Lecture(1527, "test19@test.com", "미친여우342"),
            new RankingSaveData_Lecture(2710, "test20@test.com", "달콤한토끼618"),
            new RankingSaveData_Lecture(2869, "test21@test.com", "행복한호랑이271"),
            new RankingSaveData_Lecture(2825, "test22@test.com", "행복한곰35"),
            new RankingSaveData_Lecture(1045, "test23@test.com", "고요한고양이417"),
            new RankingSaveData_Lecture(2503, "test24@test.com", "행복한고양이796"),
            new RankingSaveData_Lecture(2992, "test25@test.com", "무서운곰112"),
            new RankingSaveData_Lecture(2265, "test26@test.com", "달콤한고양이669"),
            new RankingSaveData_Lecture(2060, "test27@test.com", "우주햄스터18"),
            new RankingSaveData_Lecture(1108, "test28@test.com", "따뜻한사자117"),
            new RankingSaveData_Lecture(1762, "test29@test.com", "미친판다565"),
            new RankingSaveData_Lecture(1589, "test30@test.com", "우주고양이491"),
            new RankingSaveData_Lecture(2546, "test31@test.com", "귀여운늑대579"),
            new RankingSaveData_Lecture(2895, "test32@test.com", "행복한여우38"),
            new RankingSaveData_Lecture(3134, "test33@test.com", "미친햄스터637"),
            new RankingSaveData_Lecture(1165, "test34@test.com", "미친햄스터526"),
            new RankingSaveData_Lecture(3341, "test35@test.com", "따뜻한사자758"),
            new RankingSaveData_Lecture(3130, "test36@test.com", "무서운늑대159"),
            new RankingSaveData_Lecture(2464, "test37@test.com", "냉철한호랑이995"),
            new RankingSaveData_Lecture(1229, "test38@test.com", "달콤한사자524"),
            new RankingSaveData_Lecture(1641, "test39@test.com", "귀여운판다495"),
            new RankingSaveData_Lecture(2241, "test40@test.com", "고요한여우971"),
            new RankingSaveData_Lecture(1936, "test41@test.com", "우주곰490"),
            new RankingSaveData_Lecture(1521, "test42@test.com", "무서운여우785"),
            new RankingSaveData_Lecture(1584, "test43@test.com", "따뜻한늑대857"),
            new RankingSaveData_Lecture(1996, "test44@test.com", "귀여운곰189"),
            new RankingSaveData_Lecture(2161, "test45@test.com", "우주독수리75"),
            new RankingSaveData_Lecture(1729, "test46@test.com", "귀여운곰394"),
            new RankingSaveData_Lecture(2398, "test47@test.com", "미친호랑이739"),
            new RankingSaveData_Lecture(2360, "test48@test.com", "따뜻한곰721"),
            new RankingSaveData_Lecture(1865, "test49@test.com", "빛나는햄스터343"),
            new RankingSaveData_Lecture(3020, "test50@test.com", "냉철한호랑이494"),
            new RankingSaveData_Lecture(1697, "test100@test.com", "행복한토끼588"),
        };
    }
}

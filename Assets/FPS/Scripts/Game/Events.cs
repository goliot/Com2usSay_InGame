using UnityEngine;

namespace Unity.FPS.Game
{
    // The Game Events used across the Game.
    // Anytime there is a need for a new event, it should be added here.

    public static class Events
    {
        public static ObjectiveUpdateEvent ObjectiveUpdateEvent = new ObjectiveUpdateEvent();
        public static AllObjectivesCompletedEvent AllObjectivesCompletedEvent = new AllObjectivesCompletedEvent();
        public static GameOverEvent GameOverEvent = new GameOverEvent();
        public static PlayerDeathEvent PlayerDeathEvent = new PlayerDeathEvent();
        public static EnemyKillEvent EnemyKillEvent = new EnemyKillEvent();
        public static PickupEvent PickupEvent = new PickupEvent();
        public static AmmoPickupEvent AmmoPickupEvent = new AmmoPickupEvent();
        public static DamageEvent DamageEvent = new DamageEvent();
        public static DisplayMessageEvent DisplayMessageEvent = new DisplayMessageEvent();
        public static AchievementDataChangedEvent AchievementDataChangedEvent = new AchievementDataChangedEvent();
        public static CurrencyChangedEvent CurrencyChangedEvent = new CurrencyChangedEvent();
        public static CurrencyIncreaseEvent CurrencyIncreaseEvent = new CurrencyIncreaseEvent();
        public static MonsterKillEvent MonsterKillEvent = new MonsterKillEvent();
        public static NewAchievementNotificationEvent NewAchievementRewarded = new NewAchievementNotificationEvent();
        public static NotificationPopupEndEvent NotificationPopupEndEvent = new NotificationPopupEndEvent();
    }

    public class ObjectiveUpdateEvent : GameEvent
    {
        public Objective Objective;
        public string DescriptionText;
        public string CounterText;
        public bool IsComplete;
        public string NotificationText;
    }

    public class AllObjectivesCompletedEvent : GameEvent { }

    public class GameOverEvent : GameEvent
    {
        public bool Win;
    }

    public class PlayerDeathEvent : GameEvent { }

    public class EnemyKillEvent : GameEvent
    {
        public GameObject Enemy;
        public int RemainingEnemyCount;
    }

    public class PickupEvent : GameEvent
    {
        public GameObject Pickup;
    }

    public class AmmoPickupEvent : GameEvent
    {
        public WeaponController Weapon;
    }

    public class DamageEvent : GameEvent
    {
        public GameObject Sender;
        public float DamageValue;
    }

    public class DisplayMessageEvent : GameEvent
    {
        public string Message;
        public float DelayBeforeDisplay;
    }

    public class AchievementDataChangedEvent : GameEvent { }

    public class CurrencyChangedEvent : GameEvent { }

    public class CurrencyIncreaseEvent : GameEvent
    {
        public ECurrencyType Type { get; set; }
        public int Value { get; set; }

        public CurrencyIncreaseEvent() { }
        public CurrencyIncreaseEvent(ECurrencyType type, int value)
        {
            Type = type;
            Value = value;
        }
    }

    public class MonsterKillEvent : GameEvent
    {
        public EEnemyType Type;
    }

    public class NewAchievementNotificationEvent : GameEvent
    {
        public AchievementDTO AchievementDto;

        public NewAchievementNotificationEvent() { }

        public NewAchievementNotificationEvent(AchievementDTO achievementDto)
        {
            AchievementDto = achievementDto;
        }
    }

    public class NotificationPopupEndEvent : GameEvent { }
}

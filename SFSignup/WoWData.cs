using System.Windows.Media;

namespace SFSignup.WoW
{
    public class ClassColours
    {
        public static Color Warrior => Color.FromRgb(199, 156, 110);
        public static Color Paladin => Color.FromRgb(245, 140, 186);
        public static Color Hunter => Color.FromRgb(171, 212, 115);
        public static Color Rogue => Color.FromRgb(255, 245, 105);
        public static Color Priest => Color.FromRgb(255, 255, 255);
        public static Color DeathKnight => Color.FromRgb(196, 30, 59);
        public static Color Shaman => Color.FromRgb(0, 112, 222);
        public static Color Mage => Color.FromRgb(105, 204, 240);
        public static Color Warlock => Color.FromRgb(148, 130, 201);
        public static Color Monk => Color.FromRgb(0, 255, 150);
        public static Color Druid => Color.FromRgb(255, 125, 10);
        public static Color DemonHunter => Color.FromRgb(163, 48, 201);

        public static Color GetClassColour(PlayerClass c)
        {
            switch (c)
            {
                case PlayerClass.DeathKnight:
                    return DeathKnight;

                case PlayerClass.DemonHunter:
                    return DemonHunter;

                case PlayerClass.Druid:
                    return Druid;

                case PlayerClass.Hunter:
                    return Hunter;

                case PlayerClass.Mage:
                    return Mage;

                case PlayerClass.Monk:
                    return Monk;

                case PlayerClass.Paladin:
                    return Paladin;

                case PlayerClass.Priest:
                    return Priest;

                case PlayerClass.Rogue:
                    return Rogue;

                case PlayerClass.Shaman:
                    return Shaman;

                case PlayerClass.Warlock:
                    return Warlock;

                case PlayerClass.Warrior:
                    return Warrior;
            }
            return DeathKnight;
        }
    }

    public enum PlayerClass
    {
        Unknown,
        Warrior,
        Paladin,
        Hunter,
        Rogue,
        Priest,
        DeathKnight,
        Shaman,
        Mage,
        Warlock,
        Monk,
        Druid,
        DemonHunter
       
    }
}
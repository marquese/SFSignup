using System.Windows.Media;

namespace SFSignup.WoW
{
    public class ClassColours
    {
        public static Color Warrior { get { return Color.FromRgb(199, 156, 110); } }
        public static Color Paladin { get { return Color.FromRgb(245, 140, 186); } }
        public static Color Hunter { get { return Color.FromRgb(171, 212, 115); } }
        public static Color Rogue { get { return Color.FromRgb(255, 245, 105); } }
        public static Color Priest { get { return Color.FromRgb(255, 255, 255); } }
        public static Color DeathKnight { get { return Color.FromRgb(196, 30, 59); } }
        public static Color Shaman { get { return Color.FromRgb(0, 112, 222); } }
        public static Color Mage { get { return Color.FromRgb(105, 204, 240); } }
        public static Color Warlock { get { return Color.FromRgb(148, 130, 201); } }
        public static Color Monk { get { return Color.FromRgb(0, 255, 150); } }
        public static Color Druid { get { return Color.FromRgb(255, 125, 10); } }
        public static Color DemonHunter { get { return Color.FromRgb(163, 48, 201); } }

        public static Color GetClassColour(PlayerClass c)
        {
            switch (c)
            {
                case PlayerClass.DeathKnight:
                    return ClassColours.DeathKnight;

                case PlayerClass.DemonHunter:
                    return ClassColours.DemonHunter;

                case PlayerClass.Druid:
                    return ClassColours.Druid;

                case PlayerClass.Hunter:
                    return ClassColours.Hunter;

                case PlayerClass.Mage:
                    return ClassColours.Mage;

                case PlayerClass.Monk:
                    return ClassColours.Monk;

                case PlayerClass.Paladin:
                    return ClassColours.Paladin;

                case PlayerClass.Priest:
                    return ClassColours.Priest;

                case PlayerClass.Rogue:
                    return ClassColours.Rogue;

                case PlayerClass.Shaman:
                    return ClassColours.Shaman;

                case PlayerClass.Warlock:
                    return ClassColours.Warlock;

                case PlayerClass.Warrior:
                    return ClassColours.Warrior;
            }
            return DeathKnight;
        }
    }

    public enum PlayerClass
    {
        DeathKnight,
        DemonHunter,
        Druid,
        Hunter,
        Mage,
        Monk,
        Paladin,
        Priest,
        Rogue,
        Shaman,
        Warlock,
        Warrior
    }
}

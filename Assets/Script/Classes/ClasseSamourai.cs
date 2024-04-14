using Assets.Script.Entities;

namespace Assets.Script.Classes
{
    public class ClasseSamourai : ClasseDistance<Projectile>
    {
        public override string ClassePrefabName => "EnemySamourai";

        public override int BaseHealth => 2;

        public override float BaseSpeed => 1;

        public override int AttackDamage => 2;

        public override float AttackDelay => 0.4f;

        public override string ProjectilePrefabName => "ProjectileShuriken";
    }
}

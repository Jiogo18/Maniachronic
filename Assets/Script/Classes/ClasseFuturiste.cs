using Assets.Script.Entities;

namespace Assets.Script.Classes
{
    public class ClasseFuturiste : ClasseDistance<Projectile>
    {
        public override string ClassePrefabName => "EnemyFuturiste";

        public override int BaseHealth => 4;

        public override float BaseSpeed => 1;

        public override int AttackDamage => 1;

        public override float AttackDelay => 0.25f;

        public override string ProjectilePrefabName => "ProjectileLaser";
    }
}

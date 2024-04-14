namespace Assets.Script.Classes
{
    public class ClasseCromagnon : ClasseMelee
    {
        public override string ClassePrefabName => "EnemyCromagnon";

        public override int BaseHealth => 1;

        public override float BaseSpeed => 1;

        public override int AttackDamage => 1;

        public override float AttackDelay => 0.3f;

        public override float AttackRange => 2;
    }
}

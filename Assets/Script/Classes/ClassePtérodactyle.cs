namespace Assets.Script.Classes
{
    public class ClassePtérodactyle : ClasseMelee
    {
        public override string ClassePrefabName => "EnemyPtérodactyle";

        public override int BaseHealth => 1;

        public override float BaseSpeed => 2;

        public override int AttackDamage => 2;

        public override float AttackDelay => 0.6f;

        public override float AttackRange => 3;
    }
}

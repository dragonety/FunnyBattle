namespace Reconstitution {
    public class HealthAttribute : IAttribute {

        public float maxHealth;

        public float curHealth;

        public HealthAttribute (float health) {
            maxHealth = health;
            curHealth = health;
        }

        public void Hurt(float damage) {
            curHealth -= damage;
            if (curHealth < 0) {
                curHealth = 0;
            }
        }

    }
}
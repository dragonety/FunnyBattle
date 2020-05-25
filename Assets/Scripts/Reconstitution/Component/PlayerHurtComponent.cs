using UnityEngine;

namespace Reconstitution {
    public class PlayerHurtComponent : AbsComponet {

        private AnimatorFeature animatorFeature;
        private HealthAttribute healthAttribute;
        private PlayerAttack player;

        private bool debug = true;

        public override void OnInit() {
            animatorFeature = Entity.GetFeature<AnimatorFeature>();
            healthAttribute = Entity.GetAttribute<HealthAttribute>();
            player = Entity.GetFeature<ObjectFeature>().GetComponent<PlayerAttack>();

            RegisterMessage(MessageID.HealthUpdate, (IBody body) => {
                if (debug) Debug.Log("message healthupdate");
                float damage = (body as FloatBody).value;
                healthAttribute.curHealth -= damage;
                //player.health = healthAttribute.curHealth;
                //player.health -= damage;
                if (debug) Debug.Log(Entity.Id + " health is " + healthAttribute.curHealth);
                //player.HealthChange(healthAttribute.curHealth);
                GetStab();
            });
            
            RegisterMessage(MessageID.PlayerHurt, (IBody body) => {
                uint targetId = (body as UintBody).value;
                if (debug) Debug.Log("message player hurt " + targetId);
                player.GetStab(targetId);
            });
        }

        private void GetHurt() {
            animatorFeature.Hurt();
		}

        private void GetStab() {
            animatorFeature.Stab();
		}

    }
}
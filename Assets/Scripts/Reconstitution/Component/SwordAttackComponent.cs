using UnityEngine;

namespace Reconstitution {
    public class SwordAttackComponent : AbsComponet {

        private ObjectFeature objectFeature;
        private SwordCollision swordCollision;

        private bool debug = true;

        public override void OnInit() {

            objectFeature = Entity.GetFeature<ObjectFeature>();
            swordCollision = objectFeature.GetComponent<SwordCollision>(); 

            RegisterMessage(MessageID.PlayerAttack, (IBody body) => {
                if (debug) Debug.Log("message start attack");
                AttackStart(body as UintBody);
            });
        }

        public void AttackStart(UintBody body) {
            swordCollision.canAttack = true;
            swordCollision.playerId = body.value;
            TimerManager.Register(Consts.attackTime, AttackOver);
        }

        private void AttackOver() {
            swordCollision.canAttack = false;
        }

    }
}
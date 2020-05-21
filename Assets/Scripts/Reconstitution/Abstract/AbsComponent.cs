namespace Reconstitution {

    public abstract class AbsComponet : IComponent {
        public IEntity Entity { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void OnInit() {
            throw new System.NotImplementedException();
        }

        public void OnRemove() {
            throw new System.NotImplementedException();
        }
    }

}
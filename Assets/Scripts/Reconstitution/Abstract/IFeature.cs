namespace Reconstitution {

    public interface IFeature {
        void OnInit(IEntity entity);
        void OnRemove();
    }

}
public class Singleton<T> where T : class, new() {

    static T instance;
    public static T Instance {
        get {
            return instance ?? (instance = new T());
        }
    }

}
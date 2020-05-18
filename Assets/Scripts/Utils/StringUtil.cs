using System.Text;

public class StringUtil {

    static StringBuilder stringBuilder = new StringBuilder();
    static object lockObject = new object();

    public static string Contact(params object[] objects) {
        if (objects == null) {
            return string.Empty;
        }

        lock (lockObject) {
            stringBuilder.Length = 0;
            foreach (var item in objects) {
                if (item != null) {
                    stringBuilder.Append(item);
                }
            }
            return stringBuilder.ToString();
        }
    }

}

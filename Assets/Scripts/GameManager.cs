public class GameManager {
    private static GameManager? gm = null;

    public float GameVolume = .125f;

    public static GameManager getInstance() {
        if (gm == null) {
             gm = new GameManager(); 
        }

        return gm;
    }
}

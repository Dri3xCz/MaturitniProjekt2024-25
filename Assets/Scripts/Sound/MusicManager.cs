using System.Collections.Generic;

public class MusicManager {
    private static MusicManager? mm = null;

    public float GameVolume = .125f;

    public Dictionary<string, float> volumeConstants = new Dictionary<string, float>() {
        {"MenuBGM", 1},
        {"StageOne", .5f},
        {"Fire1", .8f},
    };

    public static MusicManager getInstance() {
        if (mm == null) {
             mm = new MusicManager(); 
        }

        return mm;
    }
}

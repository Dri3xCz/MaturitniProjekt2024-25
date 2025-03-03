using System.Collections.Generic;

public class MusicManager {
#nullable enable
    private static MusicManager? mm = null;
#nullable disable
    public Dictionary<string, float> volumeConstants = new Dictionary<string, float>() {
        {"MenuBGM", 1},
        {"StageOne", .5f},
        {"Fire1", .8f},
        {"Fire2", .1f},
        {"PlayerHit", 1}
    };

    public static MusicManager getInstance() {
        return mm ??= new MusicManager();
    }
}

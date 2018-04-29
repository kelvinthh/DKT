using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneToLoad {

	private static int sceneIndexToLoad;

    public static int SceneIndexToLoad
    {
        get
        {
            return sceneIndexToLoad;
        }
        set
        {
            sceneIndexToLoad = value;
        }
    }
}

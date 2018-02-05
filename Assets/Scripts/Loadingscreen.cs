using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loadingscreen : MonoBehaviour {

    private bool loadScene = false;

    [SerializeField]
    static public int scene;

    [SerializeField]
    static public int scene2;



    // Updates once per frame
    void Update() {

        // If the player has pressed the space bar and a new scene is not loading yet...
        if (loadScene == false) {

            // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            loadScene = true;

         

            // ...and start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene());

        }



    }


    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene() {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(1);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        //AsyncOperation async = Application.LoadLevelAsync(scene);

        //// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        //while (!async.isDone) {
        //    yield return null;
        //}
        if (scene == 1)
        {
            GameObject.Find("Sphere1").GetComponent<UnityEngine.Video.VideoPlayer>().frame = 1;
            GameObject.Find("Sphere1").GetComponent<UnloadVideos>().create = true;
            GameObject.Find("Sphere1").GetComponent<MeshRenderer>().enabled = true;            
            GameObject.Find("Sphere1").GetComponent<UnityEngine.Video.VideoPlayer>().Play();
            SceneManager.UnloadSceneAsync(7);
        }
        else if (scene == 10)
        {
            GameObject.Find("Sphere2").GetComponent<UnityEngine.Video.VideoPlayer>().frame = 1;
            GameObject.Find("Sphere2").GetComponent<UnloadVideos>().create = true;
            GameObject.Find("Sphere2").GetComponent<MeshRenderer>().enabled = true;            
            GameObject.Find("Sphere2").GetComponent<UnityEngine.Video.VideoPlayer>().Play();
            SceneManager.UnloadSceneAsync(7);
        }
        else if (scene == 11)
        {
            GameObject.Find("Sphere3").GetComponent<UnityEngine.Video.VideoPlayer>().frame = 1;
            GameObject.Find("Sphere3").GetComponent<UnloadVideos>().create = true;
            GameObject.Find("Sphere3").GetComponent<MeshRenderer>().enabled = true;            
            GameObject.Find("Sphere3").GetComponent<UnityEngine.Video.VideoPlayer>().Play();
            SceneManager.UnloadSceneAsync(7);
        }
        else
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(7);
            
        }
        }

}
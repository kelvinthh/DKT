using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractiveObject {

    void GazeTimer();

    void GazeEnter();

    void GazeExit();
 
    void Action();
}

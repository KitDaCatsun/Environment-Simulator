using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
    public State state;

    // Start is called before the first frame update
    void Start() {
        state.tick = 0;
    }

    // Update is called once per frame
    void Update() {
        state.tick += state.tickSpeed * Time.deltaTime;
        state.decimalTick = state.tick % 1;
    }
}

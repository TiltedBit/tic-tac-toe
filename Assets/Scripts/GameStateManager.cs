using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts {

    public class GameStateManager : MonoBehaviour {

        /// <summary>
        /// The game's current state
        /// </summary>
        public GameState gameState;

        public GameObject TicTacTileObject;
        
        /// <summary>
        /// Contains the shapes placed by the player
        /// </summary>
        public TicTacShape[][] board = new TicTacShape[3][];

        /// <summary>
        /// Holds a reference to the tictacttoe object
        /// </summary>
        public GameObject[][] boardObjects = new GameObject[3][];

        void Start() {

            GameObject parent = new GameObject("Board");

            // Populate the board
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {

                    if (j == 0) {
                        board[i] = new TicTacShape[3];
                        boardObjects[i] = new GameObject[3];
                    }

                    // Set up the board
                    board[i][j] = TicTacShape.Empty;
                    boardObjects[i][j] = Instantiate(TicTacTileObject, new Vector3(i, 0, j), Quaternion.Euler(90, 0, 0));
                    boardObjects[i][j].transform.parent = parent.transform;
                    boardObjects[i][j].gameObject.name = $"{i}-{j}";
                    
                    // Offset this by 1 to make sure the center tile is in the middle of the camera
                    boardObjects[i][j].transform.position = new Vector3(i - 1, 0, j - 1);
                }
            }
        }
        
        /// <summary>
        /// Called every frame
        /// </summary>
        void Update () {

            // Get whether the left mouse button is clicked during this frame
            if (Input.GetMouseButtonDown(0)) {

                // Cast a ray form this object's location using the forward transform of the object (the direction it's """looking""" in) with a maximum range of 20
                // Keep in mind that this literally casts a ray from the object this script is attached to, if you want to cast a ray from the clicked location you need to
                // use some ScreenToWorld function to calculate the clicked position in the world.
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 20.0f)) {

                    // Log the name of the hit gameobject
                    Debug.Log("GameObject: " + hit.collider.gameObject.name);
                    
                    // Check if the hit gameobject's name is "Circle"
                    if (hit.collider.gameObject.name == "Circle") {

                        // Get the mesh renderer of the clicked object
                        MeshRenderer _quadMeshRenderer = hit.collider.gameObject.GetComponent<MeshRenderer>();
                        
                        // Get the materials array
                        Material[] mats = _quadMeshRenderer.materials;
                        
                        // Set the first material to the circle material
                        //mats[0] = circleMat;

                        // Update the mesh renderer's material array
                        // We can't modify the material array directly, at least that's what Trigary said
                        _quadMeshRenderer.materials = mats;

                    }
                    
                }

            }
        
        }
    }
    
    [System.Serializable]
    public enum GameState {
        MainMenu,
        Player1,
        Player2,
        EndScreen
    }

    [System.Serializable]
    public enum TicTacShape {
        Empty,
        Cross,
        Circle
    }
}




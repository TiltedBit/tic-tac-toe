using UnityEngine;

namespace Assets.Scripts {

    public class GameStateManager : MonoBehaviour {

        /// <summary>
        /// The game's current state
        /// </summary>
        public GameState gameState;

        public Material circleMat;
        
        
        
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
                        mats[0] = circleMat;

                        // Update the mesh renderer's material array
                        // We can't modify the material array directly, at least that's what Trigary said
                        _quadMeshRenderer.materials = mats;

                    }
                    
                }

            }
        
        }
    }
}




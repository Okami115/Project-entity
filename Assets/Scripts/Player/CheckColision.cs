using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

namespace player
{
    public class CheckColision : MonoBehaviour
    {
        [SerializeField] private Transform pivot;

        [SerializeField] private GameObject[] UI;
        [SerializeField] private LayerMask ground;

        private string obstacleTag = "Obstacle"; 
        private string emptyTag = "Empty"; 
        private float raycastDistanceObjetc = 1f;
        private float raycastDistanceEmpty = 2f;
        Ray objetcRay;
        Ray groundRay;

        void Update()
        {

            RaycastHit hitInfo;
            objetcRay = new Ray(pivot.position, pivot.forward);
            groundRay = new Ray(pivot.position, -pivot.up);

            
            if (Physics.Raycast(objetcRay, out hitInfo, raycastDistanceObjetc))
            {

                if (hitInfo.collider.CompareTag(obstacleTag))
                {
                    Debug.Log("PARED");
                    UI[2].SetActive(false);
                    UI[1].SetActive(true);
                    Time.timeScale = 0.0f;
                }
            }

        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(objetcRay);
            Gizmos.DrawRay(groundRay);
        }

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log("VOID");

            if (other.gameObject.CompareTag(emptyTag))
            {
                UI[2].SetActive(false);
                UI[1].SetActive(true);
                Time.timeScale = 0.0f;
            }
        }


    }
}
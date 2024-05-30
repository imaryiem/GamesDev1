// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.AI; //important

// //if you use this code you are contractually obligated to like the YT video
// public class move : MonoBehaviour //don't forget to change the script name if you haven't
// {
//     public NavMeshAgent agent;

//     void Update()
//     {
//         if (Input.GetMouseButtonDown(1))
//         {
//             Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
//             if(Physics.Raycast(movePosition, out var hitInfo))
//             {
//                 agent.SetDestination(hitInfo.point);
//             }
//         }
//     }
// }
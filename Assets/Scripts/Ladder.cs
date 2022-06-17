using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class Ladder : MonoBehaviour
{
    public PlayerMovement Player;
    public Transform PlayerView;
    public LayerMask LadderMask;
    public float InteractDistance = 2f;

    private void OnTriggerEnter(Collider other)
    {
        Player.InLadderRange = true;    
    }

    private void OnTriggerStay(Collider other)
    {
        Ray rey = new Ray(PlayerView.position, PlayerView.forward);
        if (Physics.Raycast(rey, InteractDistance, LadderMask))
        {
            Player.LookingAtLadder = true;
        }
        else
        {
            Player.LookingAtLadder = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player.InLadderRange = false;
        Player.IsClimbing = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(PlayerView.position, PlayerView.position + PlayerView.forward * InteractDistance);
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Spectator Mode", priority = 5000)]
    public static void SpecMode()
    {
    }
#endif
}

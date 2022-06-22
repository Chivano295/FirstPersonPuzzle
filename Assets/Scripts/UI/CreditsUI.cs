using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DentedPixel;
using Cinemachine;

public class CreditsUI : MonoBehaviour
{
    public float IntervalMoves = 0.3f;
    public float MoveTime = 0.1f;

    [SerializeField]
    private GameObject ronaldThing;
    [SerializeField]
    private GameObject julianThing;
    [SerializeField]
    private GameObject chivanoThing;
    [SerializeField]
    private GameObject nigelThing;

    [SerializeField] 
    private CinemachinePath path;
    [SerializeField]
    private float spinAmount = 360f;
    [SerializeField]
    private float spinTime = 1f;

    private void Awake()
    {
        //Fade Nigel
        LeanTween.alphaCanvas(nigelThing.GetComponent<CanvasGroup>(), 0, 2.25f);
        
        //Move other team members so they are more centered
        LeanTween.moveX(ronaldThing, ronaldThing.GetComponent<ABTrack>().End.x, MoveTime).setEaseOutExpo().setDelay(2.25f + IntervalMoves * 2);
        LeanTween.moveX(julianThing, julianThing.GetComponent<ABTrack>().End.x, MoveTime).setEaseOutExpo().setDelay(2.25f);
        LeanTween.moveX(chivanoThing, chivanoThing.GetComponent<ABTrack>().End.x, MoveTime).setEaseOutExpo().setDelay(2.25f + IntervalMoves);

        LTSpline spline = new LTSpline(path.m_Waypoints.Select<CinemachinePath.Waypoint, Vector3>(current =>
        {
            return path.transform.position + current.position;
        }).ToArray()) ;

        //LeanTween.move(ronaldThing, spline, 3f).setDelay(2.25f + IntervalMoves * 3).setEaseOutExpo();
    }

    public void Spin(GameObject go)
    {
        LeanTween.rotateAround(go, Vector3.forward, spinAmount, spinTime);
    }
}

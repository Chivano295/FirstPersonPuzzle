using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public GameObject[] MainButtons;
    public GameObject[] SaveButtons;
    public Vector3 SaveButtonsLocation;
    [Tooltip("Where to place objects without being on the screeen")]
    public float XOutOfScreen = 1920;
    [Range(0f, 2f)]
    [Tooltip("Change how long it takes for the buttons to disappear from screen")]
    public float TravelTime = .4f;
    [Min(0.01f)]
    [Tooltip("Changes delay bnetween buttons starting to disappear")]
    public float ButtonTransitionInterval = 0.85f;

    [SerializeField] private ABTrack track;
    [SerializeField] private GameObject cart;
    [SerializeField] private MenuDriver driver;

    private ABTrack[] tracks;
    private ABTrack[] saveSectionTracks;

    private void Awake()
    {
        //Get the ABTrack component for each buttan that needs to move
        tracks = new ABTrack[MainButtons.Length];
        for (int i = 0; i < MainButtons.Length; i++)
        {
            tracks[i] = MainButtons[i].GetComponent<ABTrack>();
        }

        saveSectionTracks = new ABTrack[SaveButtons.Length];
        for (int i = 0; i < SaveButtons.Length; i++)
        {
            saveSectionTracks[i] = SaveButtons[i].GetComponent<ABTrack>();
        }
    }

    public void TransitionToPlaySelection()
    {
        
        for (int i = 0; i < MainButtons.Length; i++)
        {
            //LeanTween.moveX(MainButtons[i], XOutOfScreen, TravelTime).setDelay(i * ButtonTransitionInterval);
            LeanTween.moveX(MainButtons[i], tracks[i].End.x, TravelTime).setDelay(i * ButtonTransitionInterval);
        }
        
        //Move save selection buttons into view
        for (int i = 0; i < SaveButtons.Length; i++)
        {
            //LeanTween.moveX(SaveButtons[i], SaveButtonsLocation.x, TravelTime).setDelay(i * ButtonTransitionInterval + 1);
            if (i == SaveButtons.Length - 1)
                LeanTween.moveX(SaveButtons[i], saveSectionTracks[i].End.x, TravelTime).setDelay(i * ButtonTransitionInterval + 1).setOnComplete(driver.PlayFileSelect);
            else
                LeanTween.moveX(SaveButtons[i], saveSectionTracks[i].End.x, TravelTime).setDelay(i * ButtonTransitionInterval + 1);

        }
        //cart.transform.position = track.Begin;
        //LeanTween.moveX(cart, track.End.x, TravelTime).setDelay(ButtonTransitionInterval + 1);
        
    }
    public void TransitionToMainSelection()
    {
        //Move save selection buttons out of view
        for (int i = 0; i < SaveButtons.Length; i++)
        {
            LeanTween.moveX(SaveButtons[i], saveSectionTracks[i].Begin.x, TravelTime).setDelay(i * ButtonTransitionInterval);
        }

        //Move main menu buttons into view
        for (int i = 0; i < MainButtons.Length; i++)
        {
            if (i == MainButtons.Length - 1)
                LeanTween.moveX(MainButtons[i], tracks[i].Begin.x, TravelTime).setDelay(i * ButtonTransitionInterval + 1).setOnComplete(driver.PlayTitle);
            else
                LeanTween.moveX(MainButtons[i], tracks[i].Begin.x, TravelTime).setDelay(i * ButtonTransitionInterval + 1);
        }
        //LeanTween.moveX(cart, track.Begin.x, TravelTime).setDelay(ButtonTransitionInterval + 1);
    }
}

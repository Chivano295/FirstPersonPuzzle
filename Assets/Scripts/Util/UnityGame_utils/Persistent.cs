using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UnityUtils
{
    public enum HideMethod
    {
        automatic = 0,
        manual,
        m_lock
    }

    public enum FinishAction
    {
        disable = 0,
        keep,
        delete
    }

    public enum DuplicateAction
    {
        keepOld = 0,
        replaceOld
    }

    [Guid("96706864-81BF-472C-A4FD-72E1349E8A9F")]
    public class Persistent : MonoBehaviour
    {

        [Tooltip("Prevent duplicate objects of this type")]
        public bool preventDuplicates = true;
        [DrawIf("preventDuplicates", true, ComparisonType.Equals, DisablingType.ReadOnly)]
        public DuplicateAction duplicateAction = DuplicateAction.keepOld;
        [Tooltip("The method of hiding the object. \n automatic = after fixed seconds. \n manual = by function or trigger")]
        public HideMethod hideMethod = HideMethod.automatic;
        [Range(1.0f, 100f)]
        [Tooltip("The delay before the object becomes hidden (ignored if manual/lock method)")]
        public float hideDelay = 3.0f;
        [Tooltip("The action that is done after the delay (ignored if manual/lock method) \n keep = keep the object as is. \n disable = keep obj but invisible (setActive) \n del = destroy the object")]
        public FinishAction doFinish = FinishAction.disable;
        public bool DebugLogging = false;

        //static instance of this class, used to prevent more than 1 of the same object
        [HideInInspector]
        public static Persistent instance;
        [HideInInspector]
        public bool IsLocked = false;

        private static bool prevDupe
        {
            get
            {
                return instance.preventDuplicates;
            }
        }
        void Awake()
        {
            if (preventDuplicates)
            {
                switch (instance)
                {
                    case null:
                        if (DebugLogging)
                            Debug.Log("No instance found, creating 1");
                        instance = this;
                        //objLink 
                        //currentScene = SceneManager.GetActiveScene();
                        break;
                    default:
                        if (DebugLogging)
                            Debug.Log("Instance found");
                        switch (duplicateAction)
                        {
                            case DuplicateAction.keepOld:
                                Destroy(gameObject);
                                break;
                            case DuplicateAction.replaceOld:
                                Destroy(instance.gameObject);
                                instance = this;
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }
            DontDestroyOnLoad(this);
            if (DebugLogging) Debug.LogWarning("Object made persistent");

            switch (hideMethod)
            {
                case HideMethod.automatic:
                    Invoke(nameof(AutoObj), hideDelay);
                    break;
                case HideMethod.manual:
                    break;
                case HideMethod.m_lock:
                    IsLocked = true;
                    break;
            }
        }
        void AutoObj()
        {
            switch (doFinish)
            {
                case FinishAction.keep:
                    break;
                case FinishAction.disable:
                    gameObject.SetActive(false);
                    break;
                case FinishAction.delete:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}

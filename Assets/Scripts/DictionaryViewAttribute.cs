using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class DictionaryViewAttribute : PropertyAttribute
{
    
}

#if UNITY_EDITOR
public class DictionaryViewDrawer : PropertyDrawer
{
    struct SendHelp { int a; int b; }
    //unsafe void a()
    //{
    //    int a = 567;
    //    int* b = &a;
    //    void* c = (void*)b;
    //    SendHelp* f = (SendHelp*)c;

    //    int d = 2357;
    //    void* e = (void*)&d;

    //    UnsafeUtility.MemMove(c, e, sizeof(int));
    //}
}
#endif
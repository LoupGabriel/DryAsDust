using System;
using UnityEngine;

public class TransitionManager : Singleton<TransitionManager>
{
  public void StartTransition(Action intro, Action outro = null)
    {
        GetComponent<Transition>().StartTransition(intro, outro);
    }
}

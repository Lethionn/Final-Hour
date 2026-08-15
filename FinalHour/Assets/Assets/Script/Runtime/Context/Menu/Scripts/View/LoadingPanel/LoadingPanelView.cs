using DG.Tweening;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Script.Runtime.Context.Menu.Scripts.View.LoadingPanel
{
  public class LoadingPanelView : EventView
  {
    public RectTransform loadingTransform;
    
    public RectTransform hourglassTransform;

    public void Show()
    {
      loadingTransform.gameObject.SetActive(true);
      
      hourglassTransform.DORotate(
          new Vector3(0, 0, -360),  
          1f,                       
          RotateMode.FastBeyond360 
        )
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);    
    }

    public void Hide()
    {
      hourglassTransform.DOKill();
      hourglassTransform.rotation = Quaternion.Euler(0, 0, 0);
      loadingTransform.gameObject.SetActive(false);
    }
  }
}
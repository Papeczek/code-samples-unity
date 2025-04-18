using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ActionBarVisuals : MonoBehaviour
{
    [SerializeField] private RectTransform[] icons;
    [SerializeField] private GameObject bigTile;
    [SerializeField] private Vector2 selectedIconSize;

    [Space,Header("Animation Parameters")]
    [SerializeField] private Ease movementEase;
    [SerializeField] private float movementDuration = 0.1f;
    [Space]
    [SerializeField] private Ease resizeEase;
    [SerializeField] private float resizeDuration = 0.1f;

    private Vector2 normalIconSize;
    [SerializeField] private Vector2 selectedIconPositonOffset = new Vector2(0f, 11.5f);

    private int currentIconIndex = -1;
    [Space]
//    public View startView = View.World;

    public static ActionBarVisuals i;
    private void Awake()
    {
        i = this;
        normalIconSize = new Vector2(icons[0].rect.width, icons[0].rect.height);
        bigTile.SetActive(true);
       // Animate(startView);    
    }

    public void Animate(ActionBarNavigation.View view)
    {
        int iconIndex = (int)view;
        if(currentIconIndex >= 0)
        {
          UiSoundPlayer.i.PlayBarClick();
          icons[currentIconIndex].DOSizeDelta(normalIconSize, resizeDuration).SetEase(resizeEase);
          icons[currentIconIndex].DOBlendableMoveBy(-selectedIconPositonOffset,0.1f);
        //  bigTile.transform.DOMoveX(icons[iconIndex].transform.position.x, 0);
        } 
          bigTile.transform.DOMoveX(icons[iconIndex].transform.position.x,movementDuration).SetEase(movementEase);
        icons[iconIndex].DOSizeDelta(selectedIconSize, resizeDuration).SetEase(resizeEase);
        icons[iconIndex].DOBlendableMoveBy(selectedIconPositonOffset, 0.1f);
        currentIconIndex = iconIndex;
    }
    private void OnEnable()
    {
        ActionBarNavigation.OnViewOpen += Animate;
    }
    private void OnDisable()
    {
        ActionBarNavigation.OnViewOpen -= Animate;
    }

    //public enum View
    //{
    //    Shop,
    //    Inventory,
    //    World,
    //    Skills,
    //    Tavern
    //}
}

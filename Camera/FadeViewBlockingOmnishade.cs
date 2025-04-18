using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FadeViewBlockingOmnishade : MonoBehaviour
{
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private Transform Target;
    [SerializeField] private Camera Camera;
    [SerializeField] private float sphereCastInterval = 0.5f;
    [SerializeField] private float sphereCastRadius = 1f;

    [Range(0, 1f)]
    [SerializeField] private float FadedAlpha = 0.33f;
    [SerializeField] private bool RetainShadows = true;
    [SerializeField] private Vector3 TargetPositionOffset = Vector3.up;
    [SerializeField] private float FadeSpeed = 1;

    [Header("Read Only Data")]
    [SerializeField] private List<FadingObject> ObjectsBlockingView = new List<FadingObject>();

    private Dictionary<FadingObject, Coroutine> RunningCoroutines = new Dictionary<FadingObject, Coroutine>();

    private RaycastHit[] Hits = new RaycastHit[10];

    private void OnEnable()
    {
        StartCoroutine(CheckForObjects());
    }

    private IEnumerator CheckForObjects()
    {
        while (true)
        {
            int hits = Physics.SphereCastNonAlloc(
                Camera.transform.position,
                sphereCastRadius,
                (Target.transform.position + TargetPositionOffset - Camera.transform.position).normalized,
                Hits,
                Vector3.Distance(Camera.transform.position, Target.transform.position + TargetPositionOffset),
                LayerMask
            );

            if (hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    FadingObject fadingObject = GetFadingObjectFromHit(Hits[i]);

                    if (fadingObject != null && !ObjectsBlockingView.Contains(fadingObject))
                    {
                        if (RunningCoroutines.ContainsKey(fadingObject))
                        {
                            if (RunningCoroutines[fadingObject] != null)
                            {
                                StopCoroutine(RunningCoroutines[fadingObject]);
                            }

                            RunningCoroutines.Remove(fadingObject);
                        }

                        RunningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectOut(fadingObject)));
                        ObjectsBlockingView.Add(fadingObject);
                    }
                }
            }

            FadeObjectsNoLongerBeingHit();

            ClearHits();

            yield return new WaitForSeconds(sphereCastInterval);
        }
    }


    private void FadeObjectsNoLongerBeingHit()
    {
        List<FadingObject> objectsToRemove = new List<FadingObject>(ObjectsBlockingView.Count);

        foreach (FadingObject fadingObject in ObjectsBlockingView)
        {
            bool objectIsBeingHit = false;
            for (int i = 0; i < Hits.Length; i++)
            {
                FadingObject hitFadingObject = GetFadingObjectFromHit(Hits[i]);
                if (hitFadingObject != null && fadingObject == hitFadingObject)
                {
                    objectIsBeingHit = true;
                    break;
                }
            }

            if (!objectIsBeingHit)
            {
                if (RunningCoroutines.ContainsKey(fadingObject))
                {
                    if (RunningCoroutines[fadingObject] != null)
                    {
                        StopCoroutine(RunningCoroutines[fadingObject]);
                    }
                    RunningCoroutines.Remove(fadingObject);
                }

                RunningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectIn(fadingObject)));
                objectsToRemove.Add(fadingObject);
            }
        }

        foreach (FadingObject removeObject in objectsToRemove)
        {
            ObjectsBlockingView.Remove(removeObject);
        }
    }

    private IEnumerator FadeObjectOut(FadingObject FadingObject)
    {
        foreach (Material material in FadingObject.Materials)
        {
            material.SetFloat("_SourceBlend", 5);    // SrcAlpha
            material.SetFloat("_DestBlend", 10);     // OneMinusSrcAlpha
            if (material.renderQueue < 3000)
                material.renderQueue = 3000;
            material.SetFloat("_Cutout", 0);         // Cutout
            material.DisableKeyword("CUTOUT");
        }

        float time = 0;

        while (FadingObject.Materials[0].color.a > FadedAlpha)
        {
            foreach (Material material in FadingObject.Materials)
            {
                if (material.HasProperty("_Color"))
                {
                    material.color = new Color(
                        material.color.r,
                        material.color.g,
                        material.color.b,
                        Mathf.Lerp(FadingObject.InitialAlpha, FadedAlpha, time * FadeSpeed)
                    );
                }
            }

            time += Time.deltaTime;
            yield return null;
        }

        if (RunningCoroutines.ContainsKey(FadingObject))
        {
            StopCoroutine(RunningCoroutines[FadingObject]);
            RunningCoroutines.Remove(FadingObject);
        }
    }

    private IEnumerator FadeObjectIn(FadingObject FadingObject)
    {
        float time = 0;

        while (FadingObject.Materials[0].color.a < FadingObject.InitialAlpha)
        {
            foreach (Material material in FadingObject.Materials)
            {
                if (material.HasProperty("_Color"))
                {
                    material.color = new Color(
                        material.color.r,
                        material.color.g,
                        material.color.b,
                        Mathf.Lerp(FadedAlpha, FadingObject.InitialAlpha, time * FadeSpeed)
                    );
                }
            }

            time += Time.deltaTime;
            yield return null;
        }

        foreach (Material material in FadingObject.Materials)
        {
            material.SetFloat("_ZWrite", 1);
            material.SetFloat("_SourceBlend", 1);    // One
            material.SetFloat("_DestBlend", 0);      // Zero
            if (material.renderQueue >= 2450)
                material.renderQueue = 2000;
            if (FadingObject.SetCutoutBack)
            {
                material.EnableKeyword("CUTOUT");
                material.SetFloat("_Cutout", 1);
            }
            //else
            //{

            //    material.SetFloat("_Cutout", 0);         // Cutout
            //    material.DisableKeyword("CUTOUT");
            //}
        }

        if (RunningCoroutines.ContainsKey(FadingObject))
        {
            StopCoroutine(RunningCoroutines[FadingObject]);
            RunningCoroutines.Remove(FadingObject);
        }
    }

    private void ClearHits()
    {
        System.Array.Clear(Hits, 0, Hits.Length);
    }

    private FadingObject GetFadingObjectFromHit(RaycastHit Hit)
    {
        return Hit.collider != null ? Hit.collider.GetComponentInParent<FadingObject>() : null;
    }
}

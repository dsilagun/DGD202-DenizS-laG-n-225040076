using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerController))]
public class BasicInventorySystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> _collectibleObjects;
    [SerializeField] private float _collectibleDistance = .5f;
    [SerializeField] private List<GameObject> _collectedObjects;

    [SerializeField] private GameObject cuffImage;
    [SerializeField] private GameObject daggerImage;

    private void Start()
    {
        cuffImage?.SetActive(false);
        daggerImage?.SetActive(false);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        foreach (var collectedObject in _collectibleObjects.ToList())
        {
            var t = collectedObject.transform;

            if (Vector3.Distance(t.position, transform.position) > _collectibleDistance)
            {
#if UNITY_EDITOR
                Debug.DrawLine(transform.position + Vector3.up * .5f, t.position, Color.red, .3f);
#endif
                continue;
            }

#if UNITY_EDITOR
            Debug.DrawLine(transform.position + Vector3.up * .5f, t.position, Color.green, .3f);
#endif

            _collectibleObjects.Remove(collectedObject);
            _collectedObjects.Add(collectedObject);
            collectedObject.SetActive(false);
            
            CheckUI(collectedObject.name);
        }
    }

    private void CheckUI(string collectedObjectName)
    {
        switch (collectedObjectName.ToLower())
        {
            case "dagger":
                daggerImage?.SetActive(true);
                break;
            case "cuff":
                cuffImage?.SetActive(true);
                break;
        }
    }
}

using UnityEngine;
using TMPro;

public class PlanetNameplate : MonoBehaviour 
{

    [SerializeField]
    private string planetName;
    [SerializeField]
    private TextMeshProUGUI planetNameText;
    [SerializeField]
    private Highlightable highlightable;
    [SerializeField]
    private GameObject infoPanel;

    private void Awake()
    {
        planetNameText.text = planetName;
    }

    private void Update()
    {
        if (highlightable.isHighlighted)
        {
            if (!infoPanel.activeInHierarchy)
            {
                infoPanel.SetActive(true);
            }

            transform.position = highlightable.transform.position;

            //Vector3 target = VRPlayerMovementManager.instance.headTransform.position;
            //target.y = transform.position.y;
            transform.LookAt(VRPlayerMovementManager.instance.headTransform);
        }
        else
        {
            if (infoPanel.activeInHierarchy)
            {
                infoPanel.SetActive(false);
            }
        }
    }
}

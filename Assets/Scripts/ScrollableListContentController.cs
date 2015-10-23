using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class ScrollableListContentController : MonoBehaviour
{
    [SerializeField]
    private GameObject listElementPrefab;
    [SerializeField]
    private GameObject objectsHolder;
    [SerializeField]
    private GameObject contentPanel;
    [SerializeField]
    private Sprite[] icons;
    [SerializeField]
    private Text selectedObjectNameText;


    private Toggle[] itemsTogglesArray;
    private GameObject[] animatedObjects;

    void Awake()
    {
        PopulateList();
        ConfigureTogglesSystem();
    }

    /// <summary>
    /// Creates and inflates all the scrollable list elements.
    /// </summary>
    void PopulateList()
    {
        animatedObjects = (objectsHolder.GetComponentsInChildren<Transform>(true).Select(item => item.gameObject).Where(item => item != objectsHolder).ToArray());
        // Populate the scrollable list
        for (int i = 0; i < animatedObjects.Length; i++)
        {
            GameObject newListItem = Instantiate(listElementPrefab);
            newListItem.transform.parent = contentPanel.transform;
            // GetComponentInChildren function returns the parent component for some reason.
            newListItem.GetComponentsInChildren<Image>()[1].sprite = icons[i];
            newListItem.GetComponentInChildren<Text>().text = animatedObjects[i].name;
        }
    }

    /// <summary>
    /// Basically adds OnClick callback function for every list element. Also sets 
    /// every list element to a Toggle Group (So there can only be one element chosen at a time.
    /// </summary>
    void ConfigureTogglesSystem()
    {
        itemsTogglesArray = contentPanel.GetComponentsInChildren<Toggle>();

        for (int i = 0; i < itemsTogglesArray.Length; i++)
        {
            itemsTogglesArray[i].onValueChanged.AddListener(OnListItemSelected);
            itemsTogglesArray[i].group = contentPanel.GetComponent<ToggleGroup>();
        }
    }

    public void OnListItemSelected(bool boolValue)
    {
        Debug.Log(boolValue);
        for (int i = 0; i < itemsTogglesArray.Length; i++)
        {
            if (itemsTogglesArray[i].isOn)
            {
                int selectedObjectIndex = GameManager.Instance.SelectedObjectIndex;
                // If there's a previously selected item - unmark it, so the freshly selected one will be marked.
                if (selectedObjectIndex >= 0)
                    itemsTogglesArray[selectedObjectIndex].interactable = true;

                itemsTogglesArray[i].interactable = false;

                selectedObjectNameText.text = animatedObjects[i].name;

                GameManager.Instance.ShowSelectedObject(i);
                return;
            }
        }
    }
}

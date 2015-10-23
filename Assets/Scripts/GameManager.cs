using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject objectsHolder;

    private Sprite[] objectsIconArray;

    private Animator[] objectsAnimArray;
    private int selectedObjectIndex;
    public int SelectedObjectIndex { get { return selectedObjectIndex; } }
    

    public void ToggleAnimation()
    {
        objectsAnimArray[selectedObjectIndex].SetTrigger("Animate");
    }

    public void ShowSelectedObject(int index)
    {
        objectsAnimArray[selectedObjectIndex].gameObject.SetActive(false);

        selectedObjectIndex = index;
        objectsAnimArray[selectedObjectIndex].gameObject.SetActive(true);
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        objectsAnimArray = objectsHolder.GetComponentsInChildren<Animator>(true);

        foreach(var item in objectsAnimArray)
            item.gameObject.SetActive(false);
    }
}
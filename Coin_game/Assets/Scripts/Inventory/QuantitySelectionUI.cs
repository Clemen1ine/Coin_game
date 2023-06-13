using UnityEngine;
using UnityEngine.UI;

public class QuantitySelectionUI : MonoBehaviour
{
    public InputField quantityInputField;
    private int selectedQuantity;

    public void OpenQuantitySelection()
    {
        gameObject.SetActive(true);
    }

    public void CloseQuantitySelection()
    {
        gameObject.SetActive(false);
    }

    public void OnConfirmButton()
    {
        int.TryParse(quantityInputField.text, out selectedQuantity);
        CloseQuantitySelection();
    }

    public int GetSelectedQuantity()
    {
        return selectedQuantity;
    }
}
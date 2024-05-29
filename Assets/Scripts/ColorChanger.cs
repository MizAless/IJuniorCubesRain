using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void SetRandomColor(Cube cube)
    {
        float randomRedValue = UnityEngine.Random.value;
        float randomGreenValue = UnityEngine.Random.value;
        float randomBlueValue = UnityEngine.Random.value;

        Color newColor = new Color(randomRedValue, randomGreenValue, randomBlueValue);

        cube.SetColor(newColor);
    }
}

using UnityEngine;

public class BlockShader : MonoBehaviour
{
    [SerializeField] private GameObject topShade, rightShade, bottomShade, leftShade;

    private void Start()
    {
        BlockCollider.OnBlockCollision += DisableShade;
    }

    public void UpdateShade(float angle)
    {
        switch (angle)
        {
            case 0:
                topShade.SetActive(false);
                rightShade.SetActive(false);
                bottomShade.SetActive(true);
                leftShade.SetActive(false);
                break;
            case 90:
                topShade.SetActive(false);
                rightShade.SetActive(false);
                bottomShade.SetActive(false);
                leftShade.SetActive(true);
                break;
            case 180:
                topShade.SetActive(true);
                rightShade.SetActive(false);
                bottomShade.SetActive(false);
                leftShade.SetActive(false);
                break;
            case 270:
                topShade.SetActive(false);
                rightShade.SetActive(true);
                bottomShade.SetActive(false);
                leftShade.SetActive(false);
                break;
        }
    }

    private void DisableShade(GameObject block)
    {
        if(this.gameObject == block && this != null)
        {
            topShade.SetActive(false);
            rightShade.SetActive(false);
            bottomShade.SetActive(false);
            leftShade.SetActive(false);

            BlockCollider.OnBlockCollision -= DisableShade;
            Destroy(this);
        }
    }
}
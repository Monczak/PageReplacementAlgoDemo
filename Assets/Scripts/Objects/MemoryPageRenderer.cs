using UnityEngine;

public class MemoryPageRenderer : MonoBehaviour
{
    public MeshRenderer pagesRenderer;
    private Texture2D pagesTexture;

    private Material pagesMaterial;

    public Color nullPageColor;

    public int maxTextureSize = 2048;

    private void Awake()
    {
        pagesMaterial = pagesRenderer.material;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RenderPages(MemoryPage[] pages)
    {
        if (pagesTexture == null)
            CreateTexture(pages.Length);

        for (int i = 0; i < pages.Length; i++)
        {
            if (pages.Length > maxTextureSize)
            {
                pagesTexture.SetPixel(0, (int)Mathf.Lerp(0, maxTextureSize, (float)i / pages.Length), GetPageColor(pages[i]));
            }
            else
            {
                pagesTexture.SetPixel(0, i, GetPageColor(pages[i]));
            }
        }
        pagesTexture.Apply();

        pagesMaterial.mainTexture = pagesTexture;
    }

    public void CreateTexture(int size)
    {
        pagesTexture = null;
        pagesTexture = new Texture2D(1, Mathf.Min(size, maxTextureSize), TextureFormat.RGBA32, false)
        {
            filterMode = FilterMode.Point
        };
    }

    private Color GetPageColor(MemoryPage page) => page == MemoryPage.NullPage ? nullPageColor : Color.HSVToRGB((float)page.index / SimulationManager.Instance.simulationSettings.sequenceLength, 0.85f, 0.95f);
}

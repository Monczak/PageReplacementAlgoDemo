using UnityEngine;

public class MemoryPageRenderer : MonoBehaviour
{
    public MeshRenderer pagesRenderer;
    private Texture2D pagesTexture;

    private Material pagesMaterial;

    public Color nullPageColor;

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
            pagesTexture.SetPixel(0, i, GetPageColor(pages[i]));
        pagesTexture.Apply();

        pagesMaterial.mainTexture = pagesTexture;
    }

    public void CreateTexture(int size)
    {
        pagesTexture = null;
        pagesTexture = new Texture2D(1, size, TextureFormat.RGBA32, false)
        {
            filterMode = FilterMode.Point
        };
    }

    private Color GetPageColor(MemoryPage page) => page == MemoryPage.NullPage ? nullPageColor : Color.HSVToRGB((float)page.index / SimulationManager.Instance.simulationSettings.sequenceLength, 0.85f, 0.95f);
}

using UnityEngine;
using System.Collections;

public class Wave: MonoBehaviour {

    private Vector3 position;
    private float radius;
    private float travelSpeed;
    private Material mat;

    private float trailWidth;

    private Color edgeColor;
    private Color midColor;
    private Color trailColor;

    private Color StartEdgeColor;
    private Color StartMidColor;
    private Color StartTrailColor;

    private float life;
    private float fadeSpeed;
    private Camera _camera;
    private float trailModifer;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.depthTextureMode = DepthTextureMode.Depth;
        this.radius = 0;

    }

    public void SetPosition(Vector3 pos)
    {
        position = pos;
    }

    public void setTravelSpeed(float speed)
    {
        travelSpeed = speed;
    }

    public void SetMaterial(Material mat)
    {
        this.mat = new Material(mat);
        StartEdgeColor = mat.GetColor("_LeadColor");
        StartMidColor = mat.GetColor("_MidColor");
        StartTrailColor = mat.GetColor("_TrailColor");
    }

    public void SetColor(Color col)
    {
        StartEdgeColor *= col;
        StartMidColor *= col;
        StartTrailColor *= col;
    }

    public void SetFadeSpeed(float fadeSpeed)
    {
        this.fadeSpeed = fadeSpeed;
    }

    public void SetTrailModifier(float modifier)
    {
        trailModifer = modifier;
    }

    void Update()
    {
        life += Time.deltaTime/fadeSpeed;
        radius += travelSpeed * Time.deltaTime;
        trailWidth = radius * 0.8f;

        edgeColor = Color.Lerp(StartEdgeColor, Color.black, life);
        midColor = Color.Lerp(StartMidColor, Color.black, life);
        trailColor = Color.Lerp(StartTrailColor, Color.black, life);

        if (life > 1) // Yep this is a bit weird :P
            Destroy(this);
        
    }


    [ImageEffectOpaque]
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        UpdateMaterial();
        RaycastCornerBlit(src, dst, mat);
        
     }

    void RaycastCornerBlit(RenderTexture source, RenderTexture dest, Material mat)
    {
        // Compute Frustum Corners
        float camFar = _camera.farClipPlane;
        float camFov = _camera.fieldOfView;
        float camAspect = _camera.aspect;

        float fovWHalf = camFov * 0.5f;

        Vector3 toRight = _camera.transform.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * camAspect;
        Vector3 toTop = _camera.transform.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

        Vector3 topLeft = (_camera.transform.forward - toRight + toTop);
        float camScale = topLeft.magnitude * camFar;

        topLeft.Normalize();
        topLeft *= camScale;

        Vector3 topRight = (_camera.transform.forward + toRight + toTop);
        topRight.Normalize();
        topRight *= camScale;

        Vector3 bottomRight = (_camera.transform.forward + toRight - toTop);
        bottomRight.Normalize();
        bottomRight *= camScale;

        Vector3 bottomLeft = (_camera.transform.forward - toRight - toTop);
        bottomLeft.Normalize();
        bottomLeft *= camScale;

        // Custom Blit, encoding Frustum Corners as additional Texture Coordinates
        RenderTexture.active = dest;

        mat.SetTexture("_MainTex", source);

        GL.PushMatrix();
        GL.LoadOrtho();

        mat.SetPass(0);

        GL.Begin(GL.QUADS);

        GL.MultiTexCoord2(0, 0.0f, 0.0f);
        GL.MultiTexCoord(1, bottomLeft);
        GL.Vertex3(0.0f, 0.0f, 0.0f);

        GL.MultiTexCoord2(0, 1.0f, 0.0f);
        GL.MultiTexCoord(1, bottomRight);
        GL.Vertex3(1.0f, 0.0f, 0.0f);

        GL.MultiTexCoord2(0, 1.0f, 1.0f);
        GL.MultiTexCoord(1, topRight);
        GL.Vertex3(1.0f, 1.0f, 0.0f);

        GL.MultiTexCoord2(0, 0.0f, 1.0f);
        GL.MultiTexCoord(1, topLeft);
        GL.Vertex3(0.0f, 1.0f, 0.0f);

        GL.End();
        GL.PopMatrix();
    }


    public void UpdateMaterial()
    {
        if(mat)
        {
            mat.SetVector("_WorldSpaceScannerPos", position);
            mat.SetFloat("_ScanDistance", radius);
            mat.SetFloat("_ScanWidth", trailWidth);
            mat.SetColor("_LeadColor", edgeColor);
            mat.SetColor("_MidColor", midColor);
            mat.SetColor("_TrailColor", trailColor);
            
        }
        
    }
}

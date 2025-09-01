using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000217 RID: 535
[RequireComponent(typeof(CanvasRenderer))]
[ExecuteAlways]
public class UIMeshRenderer : MonoBehaviour
{
	// Token: 0x17000171 RID: 369
	// (get) Token: 0x06001694 RID: 5780 RVA: 0x0008EE48 File Offset: 0x0008D048
	private float Opacity
	{
		get
		{
			float num = 1f;
			if (this.Groups == null)
			{
				return num;
			}
			foreach (CanvasGroup canvasGroup in this.Groups)
			{
				num *= canvasGroup.alpha;
			}
			return num;
		}
	}

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x06001695 RID: 5781 RVA: 0x0008EEB0 File Offset: 0x0008D0B0
	public Material InstanceMaterial
	{
		get
		{
			return this.meshMat;
		}
	}

	// Token: 0x06001696 RID: 5782 RVA: 0x0008EEB8 File Offset: 0x0008D0B8
	private void Start()
	{
		this.Groups = new List<CanvasGroup>();
		foreach (CanvasGroup item in base.GetComponentsInParent<CanvasGroup>())
		{
			this.Groups.Add(item);
		}
		CanvasGroup component = base.GetComponent<CanvasGroup>();
		if (component != null)
		{
			this.Groups.Add(component);
		}
		this.SetupMesh();
	}

	// Token: 0x06001697 RID: 5783 RVA: 0x0008EF18 File Offset: 0x0008D118
	private void SetupMesh()
	{
		if (this.canvasRenderer == null)
		{
			this.canvasRenderer = base.GetComponent<CanvasRenderer>();
		}
		if (this.rect == null)
		{
			this.rect = base.GetComponent<RectTransform>();
		}
		if (Application.isPlaying)
		{
			this.meshMat = new Material(this.SourceMaterial);
		}
		else
		{
			this.meshMat = this.SourceMaterial;
		}
		this.canvasRenderer.SetMaterial(this.meshMat, null);
		this.needsOpacity = false;
		this.lastAlpha = -1f;
		this.meshMat.SetFloat(UIMeshRenderer.OpacityID, 1f);
		this.canvasRenderer.SetMesh(this.CreateNewMesh());
		if (this.mask)
		{
			this.SetStencilSelf();
			this.childImage = base.GetComponentsInChildren<Image>();
			if (this.childImage.Length != 0)
			{
				this.SetStencilChildren(this.childImage);
				return;
			}
		}
		else if (this.maskable)
		{
			this.SetMaskableSelf();
		}
	}

	// Token: 0x06001698 RID: 5784 RVA: 0x0008F008 File Offset: 0x0008D208
	private void Update()
	{
		if (this.cachedWidth != this.rect.rect.width || this.cachedHeight != this.rect.rect.height)
		{
			this.canvasRenderer.SetMesh(this.CreateNewMesh());
			this.cachedWidth = this.rect.rect.width;
			this.cachedHeight = this.rect.rect.height;
		}
		float opacity = this.Opacity;
		if (opacity < 1f && !this.needsOpacity)
		{
			this.SetupMaterialForOpacity();
		}
		if (this.needsOpacity && opacity != this.lastAlpha)
		{
			this.lastAlpha = opacity;
			this.meshMat.SetFloat(UIMeshRenderer.OpacityID, opacity);
		}
	}

	// Token: 0x06001699 RID: 5785 RVA: 0x0008F0D4 File Offset: 0x0008D2D4
	private void SetupMaterialForOpacity()
	{
		this.needsOpacity = true;
	}

	// Token: 0x0600169A RID: 5786 RVA: 0x0008F0DD File Offset: 0x0008D2DD
	public void ChangeMaterial(Material newMat)
	{
		this.SourceMaterial = newMat;
		this.SetupMesh();
	}

	// Token: 0x0600169B RID: 5787 RVA: 0x0008F0EC File Offset: 0x0008D2EC
	private void OnEnable()
	{
		this.SetupMesh();
		this.canvasRenderer.cull = false;
	}

	// Token: 0x0600169C RID: 5788 RVA: 0x0008F100 File Offset: 0x0008D300
	private void OnDisable()
	{
		this.canvasRenderer.Clear();
		this.canvasRenderer.cull = true;
	}

	// Token: 0x0600169D RID: 5789 RVA: 0x0008F11C File Offset: 0x0008D31C
	private Mesh CreateNewMesh()
	{
		Mesh mesh = UnityEngine.Object.Instantiate<Mesh>(this.mesh);
		this.baseVertices = mesh.vertices;
		Vector3[] array = new Vector3[this.baseVertices.Length];
		Vector2 vector = new Vector2(mesh.bounds.extents.x, mesh.bounds.extents.y);
		Rect rect = this.rect.rect;
		if (this.preserveAspect && vector.sqrMagnitude > 0f)
		{
			float num = vector.x / vector.y;
			float num2 = rect.width / rect.height;
			if (num > num2)
			{
				float height = rect.height;
				rect.height = rect.width * (1f / num);
				rect.y += (height - rect.height) * this.rect.pivot.y;
			}
			else
			{
				float width = rect.width;
				rect.width = rect.height * num;
				rect.x += (width - rect.width) * this.rect.pivot.x;
			}
		}
		float num3 = rect.height / mesh.bounds.max.y * 0.5f;
		float num4 = rect.width / mesh.bounds.max.x * 0.5f;
		for (int i = 0; i < array.Length; i++)
		{
			Vector3 vector2 = this.baseVertices[i];
			vector2.x *= num4;
			vector2.y *= num3;
			vector2.z *= num4;
			array[i] = vector2;
		}
		mesh.vertices = array;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		return mesh;
	}

	// Token: 0x0600169E RID: 5790 RVA: 0x0008F314 File Offset: 0x0008D514
	private void SetStencilSelf()
	{
		this.canvasRenderer.SetMaterial(this.meshMat, null);
		this.canvasRenderer.GetMaterial().SetInt("_Stencil", 1);
		this.canvasRenderer.GetMaterial().SetInt("_StencilComp", 8);
		this.canvasRenderer.GetMaterial().SetInt("_StencilOp", 2);
		if (this.showMaskGraphic)
		{
			this.canvasRenderer.GetMaterial().SetInt("_ColorMask", 15);
			return;
		}
		this.canvasRenderer.GetMaterial().SetInt("_ColorMask", 0);
	}

	// Token: 0x0600169F RID: 5791 RVA: 0x0008F3AC File Offset: 0x0008D5AC
	private void SetMaskableSelf()
	{
		this.canvasRenderer.SetMaterial(this.meshMat, null);
		this.canvasRenderer.GetMaterial().SetInt("_Stencil", 1);
		this.canvasRenderer.GetMaterial().SetInt("_StencilComp", 3);
		this.canvasRenderer.GetMaterial().SetInt("_StencilOp", 0);
		this.canvasRenderer.GetMaterial().SetInt("_StencilReadMask", 1);
		this.canvasRenderer.GetMaterial().SetInt("_StencilWriteMask", 0);
	}

	// Token: 0x060016A0 RID: 5792 RVA: 0x0008F43C File Offset: 0x0008D63C
	private void SetStencilChildren(Image[] images)
	{
		for (int i = 0; i < images.Length; i++)
		{
			if (images[i].maskable)
			{
				Material material = new Material(Shader.Find("UI/Default"));
				images[i].material = material;
				images[i].material.SetInt("_Stencil", 1);
				images[i].material.SetInt("_StencilComp", 3);
				images[i].material.SetInt("_StencilOp", 0);
				images[i].material.SetInt("_StencilReadMask", 1);
				images[i].material.SetInt("_StencilWriteMask", 0);
			}
		}
	}

	// Token: 0x060016A1 RID: 5793 RVA: 0x0008F4DF File Offset: 0x0008D6DF
	private void OnValidate()
	{
		if (!Application.isPlaying)
		{
			this.SetupMesh();
		}
	}

	// Token: 0x060016A2 RID: 5794 RVA: 0x0008F4EE File Offset: 0x0008D6EE
	public UIMeshRenderer()
	{
	}

	// Token: 0x060016A3 RID: 5795 RVA: 0x0008F501 File Offset: 0x0008D701
	// Note: this type is marked as 'beforefieldinit'.
	static UIMeshRenderer()
	{
	}

	// Token: 0x04001626 RID: 5670
	[SerializeField]
	private Material SourceMaterial;

	// Token: 0x04001627 RID: 5671
	[SerializeField]
	private Mesh mesh;

	// Token: 0x04001628 RID: 5672
	[SerializeField]
	private bool mask;

	// Token: 0x04001629 RID: 5673
	[SerializeField]
	private bool showMaskGraphic;

	// Token: 0x0400162A RID: 5674
	[SerializeField]
	private bool maskable;

	// Token: 0x0400162B RID: 5675
	[SerializeField]
	private bool preserveAspect;

	// Token: 0x0400162C RID: 5676
	private CanvasRenderer canvasRenderer;

	// Token: 0x0400162D RID: 5677
	private List<CanvasGroup> Groups;

	// Token: 0x0400162E RID: 5678
	private bool needsOpacity;

	// Token: 0x0400162F RID: 5679
	private float lastAlpha = -1f;

	// Token: 0x04001630 RID: 5680
	private Material meshMat;

	// Token: 0x04001631 RID: 5681
	private Image[] childImage;

	// Token: 0x04001632 RID: 5682
	private Vector3[] baseVertices;

	// Token: 0x04001633 RID: 5683
	private RectTransform rect;

	// Token: 0x04001634 RID: 5684
	private float cachedHeight;

	// Token: 0x04001635 RID: 5685
	private float cachedWidth;

	// Token: 0x04001636 RID: 5686
	private static readonly int OpacityID = Shader.PropertyToID("_Opacity");
}

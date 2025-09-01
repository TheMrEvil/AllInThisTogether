using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace cakeslice
{
	// Token: 0x02000004 RID: 4
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Camera))]
	public class OutlineEffect : MonoBehaviour
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002253 File Offset: 0x00000453
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000225A File Offset: 0x0000045A
		public static OutlineEffect Instance
		{
			[CompilerGenerated]
			get
			{
				return OutlineEffect.<Instance>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				OutlineEffect.<Instance>k__BackingField = value;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002262 File Offset: 0x00000462
		private Material GetMaterialFromID(int ID)
		{
			if (ID == 0)
			{
				return this.outline1Material;
			}
			if (ID == 1)
			{
				return this.outline2Material;
			}
			if (ID == 2)
			{
				return this.outline3Material;
			}
			return this.outline1Material;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000228C File Offset: 0x0000048C
		private Material CreateMaterial(Color emissionColor)
		{
			Material material = new Material(this.outlineBufferShader);
			material.SetColor("_Color", emissionColor);
			material.SetInt("_SrcBlend", 5);
			material.SetInt("_DstBlend", 10);
			material.SetInt("_ZWrite", 0);
			material.DisableKeyword("_ALPHATEST_ON");
			material.EnableKeyword("_ALPHABLEND_ON");
			material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			material.renderQueue = 3000;
			return material;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002301 File Offset: 0x00000501
		private void Awake()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002304 File Offset: 0x00000504
		private void Start()
		{
			if (OutlineEffect.Instance != null)
			{
				UnityEngine.Object.Destroy(this);
				return;
			}
			OutlineEffect.Instance = this;
			this.CreateMaterialsIfNeeded();
			this.UpdateMaterialsPublicProperties();
			if (this.sourceCamera == null)
			{
				this.sourceCamera = base.GetComponent<Camera>();
				if (this.sourceCamera == null)
				{
					this.sourceCamera = Camera.main;
				}
			}
			if (this.outlineCamera == null)
			{
				foreach (Camera camera in base.GetComponentsInChildren<Camera>())
				{
					if (camera.name == "Outline Camera")
					{
						this.outlineCamera = camera;
						camera.enabled = false;
						break;
					}
				}
				if (this.outlineCamera == null)
				{
					this.outlineCamera = new GameObject("Outline Camera")
					{
						transform = 
						{
							parent = this.sourceCamera.transform
						}
					}.AddComponent<Camera>();
					this.outlineCamera.rect = new Rect(0f, 0f, 1.0001f, 1f);
					this.outlineCamera.enabled = false;
				}
			}
			if (this.renderTexture != null)
			{
				this.renderTexture.Release();
			}
			if (this.extraRenderTexture != null)
			{
				this.renderTexture.Release();
			}
			this.renderTexture = new RenderTexture(this.sourceCamera.pixelWidth, this.sourceCamera.pixelHeight, 16, RenderTextureFormat.Default);
			this.extraRenderTexture = new RenderTexture(this.sourceCamera.pixelWidth, this.sourceCamera.pixelHeight, 16, RenderTextureFormat.Default);
			this.UpdateOutlineCameraFromSource();
			this.commandBuffer = new CommandBuffer();
			this.outlineCamera.AddCommandBuffer(CameraEvent.BeforeImageEffects, this.commandBuffer);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024C0 File Offset: 0x000006C0
		public void OnPreRender()
		{
			if (this.commandBuffer == null)
			{
				return;
			}
			if (this.outlines.Count == 0)
			{
				if (!this.RenderTheNextFrame)
				{
					return;
				}
				this.RenderTheNextFrame = false;
			}
			else
			{
				this.RenderTheNextFrame = true;
			}
			this.CreateMaterialsIfNeeded();
			if (this.renderTexture == null || this.renderTexture.width != this.sourceCamera.pixelWidth || this.renderTexture.height != this.sourceCamera.pixelHeight)
			{
				if (this.renderTexture != null)
				{
					this.renderTexture.Release();
				}
				if (this.extraRenderTexture != null)
				{
					this.renderTexture.Release();
				}
				this.renderTexture = new RenderTexture(this.sourceCamera.pixelWidth, this.sourceCamera.pixelHeight, 16, RenderTextureFormat.Default);
				this.extraRenderTexture = new RenderTexture(this.sourceCamera.pixelWidth, this.sourceCamera.pixelHeight, 16, RenderTextureFormat.Default);
				this.outlineCamera.targetTexture = this.renderTexture;
			}
			this.UpdateMaterialsPublicProperties();
			this.UpdateOutlineCameraFromSource();
			this.outlineCamera.targetTexture = this.renderTexture;
			this.commandBuffer.SetRenderTarget(this.renderTexture);
			this.commandBuffer.Clear();
			foreach (Outline outline in this.outlines)
			{
				LayerMask mask = this.sourceCamera.cullingMask;
				if (outline != null && mask == (mask | 1 << outline.gameObject.layer))
				{
					for (int i = 0; i < outline.SharedMaterials.Length; i++)
					{
						Material material = null;
						if (outline.SharedMaterials[i].HasProperty("_MainTex") && outline.SharedMaterials[i].mainTexture != null && outline.SharedMaterials[i])
						{
							foreach (Material material2 in this.materialBuffer)
							{
								if (material2.mainTexture == outline.SharedMaterials[i].mainTexture)
								{
									if (outline.eraseRenderer && material2.color == this.outlineEraseMaterial.color)
									{
										material = material2;
									}
									else if (!outline.eraseRenderer && material2.color == this.GetMaterialFromID(outline.color).color)
									{
										material = material2;
									}
								}
							}
							if (material == null)
							{
								if (outline.eraseRenderer)
								{
									material = new Material(this.outlineEraseMaterial);
								}
								else
								{
									material = new Material(this.GetMaterialFromID(outline.color));
								}
								material.mainTexture = outline.SharedMaterials[i].mainTexture;
								this.materialBuffer.Add(material);
							}
						}
						else if (outline.eraseRenderer)
						{
							material = this.outlineEraseMaterial;
						}
						else
						{
							material = this.GetMaterialFromID(outline.color);
						}
						if (this.backfaceCulling)
						{
							material.SetInt("_Culling", 2);
						}
						else
						{
							material.SetInt("_Culling", 0);
						}
						MeshFilter meshFilter = outline.MeshFilter;
						SkinnedMeshRenderer skinnedMeshRenderer = outline.SkinnedMeshRenderer;
						SpriteRenderer spriteRenderer = outline.SpriteRenderer;
						if (meshFilter)
						{
							if (meshFilter.sharedMesh != null && i < meshFilter.sharedMesh.subMeshCount)
							{
								this.commandBuffer.DrawRenderer(outline.Renderer, material, i, 0);
							}
						}
						else if (skinnedMeshRenderer)
						{
							if (skinnedMeshRenderer.sharedMesh != null && i < skinnedMeshRenderer.sharedMesh.subMeshCount)
							{
								this.commandBuffer.DrawRenderer(outline.Renderer, material, i, 0);
							}
						}
						else if (spriteRenderer)
						{
							this.commandBuffer.DrawRenderer(outline.Renderer, material, i, 0);
						}
					}
				}
			}
			this.outlineCamera.Render();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002904 File Offset: 0x00000B04
		private void OnEnable()
		{
			Outline[] array = UnityEngine.Object.FindObjectsOfType<Outline>();
			if (this.autoEnableOutlines)
			{
				foreach (Outline outline in array)
				{
					outline.enabled = false;
					outline.enabled = true;
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000293F File Offset: 0x00000B3F
		private void OnDestroy()
		{
			if (this.renderTexture != null)
			{
				this.renderTexture.Release();
			}
			if (this.extraRenderTexture != null)
			{
				this.extraRenderTexture.Release();
			}
			this.DestroyMaterials();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000297C File Offset: 0x00000B7C
		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.outlineShaderMaterial != null)
			{
				this.outlineShaderMaterial.SetTexture("_OutlineSource", this.renderTexture);
				if (this.addLinesBetweenColors)
				{
					Graphics.Blit(source, this.extraRenderTexture, this.outlineShaderMaterial, 0);
					this.outlineShaderMaterial.SetTexture("_OutlineSource", this.extraRenderTexture);
				}
				Graphics.Blit(source, destination, this.outlineShaderMaterial, 1);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000029EC File Offset: 0x00000BEC
		private void CreateMaterialsIfNeeded()
		{
			if (this.outlineShader == null)
			{
				this.outlineShader = Resources.Load<Shader>("OutlineShader");
			}
			if (this.outlineBufferShader == null)
			{
				this.outlineBufferShader = Resources.Load<Shader>("OutlineBufferShader");
			}
			if (this.outlineShaderMaterial == null)
			{
				this.outlineShaderMaterial = new Material(this.outlineShader);
				this.outlineShaderMaterial.hideFlags = HideFlags.HideAndDontSave;
				this.UpdateMaterialsPublicProperties();
			}
			if (this.outlineEraseMaterial == null)
			{
				this.outlineEraseMaterial = this.CreateMaterial(new Color(0f, 0f, 0f, 0f));
			}
			if (this.outline1Material == null)
			{
				this.outline1Material = this.CreateMaterial(new Color(1f, 0f, 0f, 0f));
			}
			if (this.outline2Material == null)
			{
				this.outline2Material = this.CreateMaterial(new Color(0f, 1f, 0f, 0f));
			}
			if (this.outline3Material == null)
			{
				this.outline3Material = this.CreateMaterial(new Color(0f, 0f, 1f, 0f));
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002B34 File Offset: 0x00000D34
		private void DestroyMaterials()
		{
			foreach (Material obj in this.materialBuffer)
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
			this.materialBuffer.Clear();
			UnityEngine.Object.DestroyImmediate(this.outlineShaderMaterial);
			UnityEngine.Object.DestroyImmediate(this.outlineEraseMaterial);
			UnityEngine.Object.DestroyImmediate(this.outline1Material);
			UnityEngine.Object.DestroyImmediate(this.outline2Material);
			UnityEngine.Object.DestroyImmediate(this.outline3Material);
			this.outlineShader = null;
			this.outlineBufferShader = null;
			this.outlineShaderMaterial = null;
			this.outlineEraseMaterial = null;
			this.outline1Material = null;
			this.outline2Material = null;
			this.outline3Material = null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public void UpdateMaterialsPublicProperties()
		{
			if (this.outlineShaderMaterial)
			{
				float num = 1f;
				if (this.scaleWithScreenSize)
				{
					num = (float)Screen.height / 360f;
				}
				if (this.scaleWithScreenSize && num < 1f)
				{
					this.outlineShaderMaterial.SetFloat("_LineThicknessX", 0.001f * (1f / (float)Screen.width) * 1000f);
					this.outlineShaderMaterial.SetFloat("_LineThicknessY", 0.001f * (1f / (float)Screen.height) * 1000f);
				}
				else
				{
					this.outlineShaderMaterial.SetFloat("_LineThicknessX", num * (this.lineThickness / 1000f) * (1f / (float)Screen.width) * 1000f);
					this.outlineShaderMaterial.SetFloat("_LineThicknessY", num * (this.lineThickness / 1000f) * (1f / (float)Screen.height) * 1000f);
				}
				this.outlineShaderMaterial.SetFloat("_LineIntensity", this.lineIntensity);
				this.outlineShaderMaterial.SetFloat("_FillAmount", this.fillAmount);
				this.outlineShaderMaterial.SetColor("_FillColor", this.fillColor);
				this.outlineShaderMaterial.SetFloat("_UseFillColor", (float)(this.useFillColor ? 1 : 0));
				this.outlineShaderMaterial.SetColor("_LineColor1", this.lineColor0 * this.lineColor0);
				this.outlineShaderMaterial.SetColor("_LineColor2", this.lineColor1 * this.lineColor1);
				this.outlineShaderMaterial.SetColor("_LineColor3", this.lineColor2 * this.lineColor2);
				if (this.flipY)
				{
					this.outlineShaderMaterial.SetInt("_FlipY", 1);
				}
				else
				{
					this.outlineShaderMaterial.SetInt("_FlipY", 0);
				}
				if (!this.additiveRendering)
				{
					this.outlineShaderMaterial.SetInt("_Dark", 1);
				}
				else
				{
					this.outlineShaderMaterial.SetInt("_Dark", 0);
				}
				if (this.cornerOutlines)
				{
					this.outlineShaderMaterial.SetInt("_CornerOutlines", 1);
				}
				else
				{
					this.outlineShaderMaterial.SetInt("_CornerOutlines", 0);
				}
				Shader.SetGlobalFloat("_OutlineAlphaCutoff", this.alphaCutoff);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002E4C File Offset: 0x0000104C
		private void UpdateOutlineCameraFromSource()
		{
			this.outlineCamera.CopyFrom(this.sourceCamera);
			this.outlineCamera.renderingPath = RenderingPath.Forward;
			this.outlineCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
			this.outlineCamera.clearFlags = CameraClearFlags.Color;
			this.outlineCamera.rect = new Rect(0f, 0f, 1f, 1f);
			this.outlineCamera.cullingMask = 0;
			this.outlineCamera.targetTexture = this.renderTexture;
			this.outlineCamera.enabled = false;
			this.outlineCamera.allowHDR = false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002EFF File Offset: 0x000010FF
		public void AddOutline(Outline outline)
		{
			this.outlines.Add(outline);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002F0E File Offset: 0x0000110E
		public void RemoveOutline(Outline outline)
		{
			this.outlines.Remove(outline);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002F20 File Offset: 0x00001120
		public OutlineEffect()
		{
		}

		// Token: 0x0400000B RID: 11
		[CompilerGenerated]
		private static OutlineEffect <Instance>k__BackingField;

		// Token: 0x0400000C RID: 12
		private readonly LinkedSet<Outline> outlines = new LinkedSet<Outline>();

		// Token: 0x0400000D RID: 13
		[Range(1f, 6f)]
		public float lineThickness = 1.25f;

		// Token: 0x0400000E RID: 14
		[Range(0f, 10f)]
		public float lineIntensity = 0.5f;

		// Token: 0x0400000F RID: 15
		[Range(0f, 1f)]
		public float fillAmount = 0.2f;

		// Token: 0x04000010 RID: 16
		public Color lineColor0 = Color.red;

		// Token: 0x04000011 RID: 17
		public Color lineColor1 = Color.green;

		// Token: 0x04000012 RID: 18
		public Color lineColor2 = Color.blue;

		// Token: 0x04000013 RID: 19
		public bool additiveRendering;

		// Token: 0x04000014 RID: 20
		public bool backfaceCulling = true;

		// Token: 0x04000015 RID: 21
		public Color fillColor = Color.blue;

		// Token: 0x04000016 RID: 22
		public bool useFillColor;

		// Token: 0x04000017 RID: 23
		[Header("These settings can affect performance!")]
		public bool cornerOutlines;

		// Token: 0x04000018 RID: 24
		public bool addLinesBetweenColors;

		// Token: 0x04000019 RID: 25
		[Header("Advanced settings")]
		public bool scaleWithScreenSize = true;

		// Token: 0x0400001A RID: 26
		[Range(0f, 1f)]
		public float alphaCutoff = 0.5f;

		// Token: 0x0400001B RID: 27
		public bool flipY;

		// Token: 0x0400001C RID: 28
		public Camera sourceCamera;

		// Token: 0x0400001D RID: 29
		public bool autoEnableOutlines;

		// Token: 0x0400001E RID: 30
		[HideInInspector]
		public Camera outlineCamera;

		// Token: 0x0400001F RID: 31
		private Material outline1Material;

		// Token: 0x04000020 RID: 32
		private Material outline2Material;

		// Token: 0x04000021 RID: 33
		private Material outline3Material;

		// Token: 0x04000022 RID: 34
		private Material outlineEraseMaterial;

		// Token: 0x04000023 RID: 35
		private Shader outlineShader;

		// Token: 0x04000024 RID: 36
		private Shader outlineBufferShader;

		// Token: 0x04000025 RID: 37
		[HideInInspector]
		public Material outlineShaderMaterial;

		// Token: 0x04000026 RID: 38
		[HideInInspector]
		public RenderTexture renderTexture;

		// Token: 0x04000027 RID: 39
		[HideInInspector]
		public RenderTexture extraRenderTexture;

		// Token: 0x04000028 RID: 40
		private CommandBuffer commandBuffer;

		// Token: 0x04000029 RID: 41
		private List<Material> materialBuffer = new List<Material>();

		// Token: 0x0400002A RID: 42
		private bool RenderTheNextFrame;
	}
}

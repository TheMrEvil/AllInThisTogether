using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	// Token: 0x0200008B RID: 139
	[ExecuteInEditMode]
	public abstract class BaseMeshEffect : UIBehaviour, IMeshModifier
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x000266FA File Offset: 0x000248FA
		public Graphic graphic
		{
			get
			{
				this.Initialize();
				return this._graphic;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00026708 File Offset: 0x00024908
		public CanvasRenderer canvasRenderer
		{
			get
			{
				this.Initialize();
				return this._canvasRenderer;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00026716 File Offset: 0x00024916
		public TMP_Text textMeshPro
		{
			get
			{
				this.Initialize();
				return this._textMeshPro;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00026724 File Offset: 0x00024924
		public RectTransform rectTransform
		{
			get
			{
				this.Initialize();
				return this._rectTransform;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00026732 File Offset: 0x00024932
		public virtual AdditionalCanvasShaderChannels requiredChannels
		{
			get
			{
				return AdditionalCanvasShaderChannels.TexCoord1;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00026735 File Offset: 0x00024935
		public bool isTMPro
		{
			get
			{
				return this.textMeshPro != null;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00026743 File Offset: 0x00024943
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x00026778 File Offset: 0x00024978
		public virtual Material material
		{
			get
			{
				if (this.textMeshPro)
				{
					return this.textMeshPro.fontSharedMaterial;
				}
				if (this.graphic)
				{
					return this.graphic.material;
				}
				return null;
			}
			set
			{
				if (this.textMeshPro)
				{
					this.textMeshPro.fontSharedMaterial = value;
					return;
				}
				if (this.graphic)
				{
					this.graphic.material = value;
				}
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000267B0 File Offset: 0x000249B0
		public virtual Material[] materials
		{
			get
			{
				if (this.textMeshPro)
				{
					return this.textMeshPro.fontSharedMaterials ?? BaseMeshEffect.s_EmptyMaterials;
				}
				if (this.graphic)
				{
					this._materials[0] = this.graphic.material;
					return this._materials;
				}
				return BaseMeshEffect.s_EmptyMaterials;
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0002680B File Offset: 0x00024A0B
		public virtual void ModifyMesh(Mesh mesh)
		{
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0002680D File Offset: 0x00024A0D
		public virtual void ModifyMesh(VertexHelper vh)
		{
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00026810 File Offset: 0x00024A10
		public virtual void SetVerticesDirty()
		{
			if (this.textMeshPro != null && this.textMeshPro.textInfo != null && this.textMeshPro.textInfo.meshInfo != null)
			{
				foreach (TMP_MeshInfo tmp_MeshInfo in this.textMeshPro.textInfo.meshInfo)
				{
					Mesh mesh = tmp_MeshInfo.mesh;
					if (mesh)
					{
						mesh.Clear();
						mesh.vertices = tmp_MeshInfo.vertices;
						mesh.uv = tmp_MeshInfo.uvs0;
						mesh.uv2 = tmp_MeshInfo.uvs2;
						mesh.colors32 = tmp_MeshInfo.colors32;
						mesh.normals = tmp_MeshInfo.normals;
						mesh.tangents = tmp_MeshInfo.tangents;
						mesh.triangles = tmp_MeshInfo.triangles;
					}
				}
				if (this.canvasRenderer)
				{
					this.canvasRenderer.SetMesh(this.textMeshPro.mesh);
					base.GetComponentsInChildren<TMP_SubMeshUI>(false, BaseMeshEffect.s_SubMeshUIs);
					foreach (TMP_SubMeshUI tmp_SubMeshUI in BaseMeshEffect.s_SubMeshUIs)
					{
						tmp_SubMeshUI.canvasRenderer.SetMesh(tmp_SubMeshUI.mesh);
					}
					BaseMeshEffect.s_SubMeshUIs.Clear();
				}
				this.textMeshPro.havePropertiesChanged = true;
				return;
			}
			if (this.graphic)
			{
				this.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00026994 File Offset: 0x00024B94
		public void ShowTMProWarning(Shader shader, Shader mobileShader, Shader spriteShader, Action<Material> onCreatedMaterial)
		{
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00026996 File Offset: 0x00024B96
		protected virtual bool isLegacyMeshModifier
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0002699C File Offset: 0x00024B9C
		protected virtual void Initialize()
		{
			if (!this._initialized)
			{
				this._initialized = true;
				this._graphic = (this._graphic ?? base.GetComponent<Graphic>());
				this._canvasRenderer = (this._canvasRenderer ?? base.GetComponent<CanvasRenderer>());
				this._rectTransform = (this._rectTransform ?? base.GetComponent<RectTransform>());
				this._textMeshPro = (this._textMeshPro ?? base.GetComponent<TMP_Text>());
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00026A10 File Offset: 0x00024C10
		protected override void OnEnable()
		{
			this._initialized = false;
			this.SetVerticesDirty();
			if (this.textMeshPro)
			{
				TMPro_EventManager.TEXT_CHANGED_EVENT.Add(new Action<UnityEngine.Object>(this.OnTextChanged));
			}
			if (this.graphic)
			{
				AdditionalCanvasShaderChannels requiredChannels = this.requiredChannels;
				Canvas canvas = this.graphic.canvas;
				if (canvas && (canvas.additionalShaderChannels & requiredChannels) != requiredChannels)
				{
					Debug.LogWarningFormat(this, "Enable {1} of Canvas.additionalShaderChannels to use {0}.", new object[]
					{
						base.GetType().Name,
						requiredChannels
					});
				}
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00026AA8 File Offset: 0x00024CA8
		protected override void OnDisable()
		{
			TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(new Action<UnityEngine.Object>(this.OnTextChanged));
			this.SetVerticesDirty();
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00026AC8 File Offset: 0x00024CC8
		protected virtual void LateUpdate()
		{
			if (this.textMeshPro)
			{
				if (this.textMeshPro.havePropertiesChanged || this._isTextMeshProActive != this.textMeshPro.isActiveAndEnabled)
				{
					this.SetVerticesDirty();
				}
				this._isTextMeshProActive = this.textMeshPro.isActiveAndEnabled;
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00026B19 File Offset: 0x00024D19
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetVerticesDirty();
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00026B24 File Offset: 0x00024D24
		private void OnTextChanged(UnityEngine.Object obj)
		{
			TMP_TextInfo textInfo = this.textMeshPro.textInfo;
			if (this.textMeshPro != obj || textInfo.characterCount - textInfo.spaceCount <= 0)
			{
				return;
			}
			BaseMeshEffect.s_Meshes.Clear();
			foreach (TMP_MeshInfo tmp_MeshInfo in textInfo.meshInfo)
			{
				BaseMeshEffect.s_Meshes.Add(tmp_MeshInfo.mesh);
			}
			if (this.isLegacyMeshModifier)
			{
				using (List<Mesh>.Enumerator enumerator = BaseMeshEffect.s_Meshes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Mesh mesh = enumerator.Current;
						if (mesh)
						{
							this.ModifyMesh(mesh);
						}
					}
					goto IL_108;
				}
			}
			foreach (Mesh mesh2 in BaseMeshEffect.s_Meshes)
			{
				if (mesh2)
				{
					this.FillVertexHelper(BaseMeshEffect.s_VertexHelper, mesh2);
					this.ModifyMesh(BaseMeshEffect.s_VertexHelper);
					BaseMeshEffect.s_VertexHelper.FillMesh(mesh2);
				}
			}
			IL_108:
			if (this.canvasRenderer)
			{
				this.canvasRenderer.SetMesh(this.textMeshPro.mesh);
				base.GetComponentsInChildren<TMP_SubMeshUI>(false, BaseMeshEffect.s_SubMeshUIs);
				foreach (TMP_SubMeshUI tmp_SubMeshUI in BaseMeshEffect.s_SubMeshUIs)
				{
					tmp_SubMeshUI.canvasRenderer.SetMesh(tmp_SubMeshUI.mesh);
				}
				BaseMeshEffect.s_SubMeshUIs.Clear();
			}
			BaseMeshEffect.s_Meshes.Clear();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00026CE8 File Offset: 0x00024EE8
		private void FillVertexHelper(VertexHelper vh, Mesh mesh)
		{
			vh.Clear();
			mesh.GetVertices(BaseMeshEffect.s_Vertices);
			mesh.GetColors(BaseMeshEffect.s_Colors);
			mesh.GetUVs(0, BaseMeshEffect.s_Uv0);
			mesh.GetUVs(1, BaseMeshEffect.s_Uv1);
			mesh.GetNormals(BaseMeshEffect.s_Normals);
			mesh.GetTangents(BaseMeshEffect.s_Tangents);
			mesh.GetIndices(BaseMeshEffect.s_Indices, 0);
			for (int i = 0; i < BaseMeshEffect.s_Vertices.Count; i++)
			{
				BaseMeshEffect.s_VertexHelper.AddVert(BaseMeshEffect.s_Vertices[i], BaseMeshEffect.s_Colors[i], BaseMeshEffect.s_Uv0[i], BaseMeshEffect.s_Uv1[i], BaseMeshEffect.s_Normals[i], BaseMeshEffect.s_Tangents[i]);
			}
			for (int j = 0; j < BaseMeshEffect.s_Indices.Count; j += 3)
			{
				vh.AddTriangle(BaseMeshEffect.s_Indices[j], BaseMeshEffect.s_Indices[j + 1], BaseMeshEffect.s_Indices[j + 2]);
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00026DF6 File Offset: 0x00024FF6
		protected BaseMeshEffect()
		{
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00026E0C File Offset: 0x0002500C
		// Note: this type is marked as 'beforefieldinit'.
		static BaseMeshEffect()
		{
		}

		// Token: 0x040004C4 RID: 1220
		private static readonly List<Vector2> s_Uv0 = new List<Vector2>();

		// Token: 0x040004C5 RID: 1221
		private static readonly List<Vector2> s_Uv1 = new List<Vector2>();

		// Token: 0x040004C6 RID: 1222
		private static readonly List<Vector3> s_Vertices = new List<Vector3>();

		// Token: 0x040004C7 RID: 1223
		private static readonly List<int> s_Indices = new List<int>();

		// Token: 0x040004C8 RID: 1224
		private static readonly List<Vector3> s_Normals = new List<Vector3>();

		// Token: 0x040004C9 RID: 1225
		private static readonly List<Vector4> s_Tangents = new List<Vector4>();

		// Token: 0x040004CA RID: 1226
		private static readonly List<Color32> s_Colors = new List<Color32>();

		// Token: 0x040004CB RID: 1227
		private static readonly VertexHelper s_VertexHelper = new VertexHelper();

		// Token: 0x040004CC RID: 1228
		private static readonly List<TMP_SubMeshUI> s_SubMeshUIs = new List<TMP_SubMeshUI>();

		// Token: 0x040004CD RID: 1229
		private static readonly List<Mesh> s_Meshes = new List<Mesh>();

		// Token: 0x040004CE RID: 1230
		private static readonly Material[] s_EmptyMaterials = new Material[0];

		// Token: 0x040004CF RID: 1231
		private bool _initialized;

		// Token: 0x040004D0 RID: 1232
		private CanvasRenderer _canvasRenderer;

		// Token: 0x040004D1 RID: 1233
		private RectTransform _rectTransform;

		// Token: 0x040004D2 RID: 1234
		private Graphic _graphic;

		// Token: 0x040004D3 RID: 1235
		private Material[] _materials = new Material[1];

		// Token: 0x040004D4 RID: 1236
		private bool _isTextMeshProActive;

		// Token: 0x040004D5 RID: 1237
		private TMP_Text _textMeshPro;
	}
}

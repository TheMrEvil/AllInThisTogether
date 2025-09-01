using System;
using UnityEngine;

namespace Onager.FXMesh
{
	// Token: 0x02000164 RID: 356
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	[ExecuteInEditMode]
	public class FXMesh : MonoBehaviour
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0005C9AD File Offset: 0x0005ABAD
		public FXMeshSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0005C9B8 File Offset: 0x0005ABB8
		private MeshFilter MFilter
		{
			get
			{
				if (!this._meshFilter)
				{
					return this._meshFilter = base.GetComponent<MeshFilter>();
				}
				return this._meshFilter;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0005C9E8 File Offset: 0x0005ABE8
		private MeshRenderer MRenderer
		{
			get
			{
				if (!this._meshRenderer)
				{
					return this._meshRenderer = base.GetComponent<MeshRenderer>();
				}
				return this._meshRenderer;
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0005CA18 File Offset: 0x0005AC18
		private void Awake()
		{
			this.ApplyMesh();
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0005CA20 File Offset: 0x0005AC20
		public void ApplyMesh()
		{
			if (this.settings == null)
			{
				return;
			}
			this.MFilter.sharedMesh = this.settings.GetMesh(false);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0005CA48 File Offset: 0x0005AC48
		public FXMesh()
		{
		}

		// Token: 0x04000BA5 RID: 2981
		[SerializeField]
		private FXMeshSettings settings;

		// Token: 0x04000BA6 RID: 2982
		private MeshFilter _meshFilter;

		// Token: 0x04000BA7 RID: 2983
		private MeshRenderer _meshRenderer;
	}
}

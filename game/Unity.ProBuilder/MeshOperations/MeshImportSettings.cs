using System;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x02000082 RID: 130
	[Serializable]
	public sealed class MeshImportSettings
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00031FF8 File Offset: 0x000301F8
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00032000 File Offset: 0x00030200
		public bool quads
		{
			get
			{
				return this.m_Quads;
			}
			set
			{
				this.m_Quads = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00032009 File Offset: 0x00030209
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00032011 File Offset: 0x00030211
		public bool smoothing
		{
			get
			{
				return this.m_Smoothing;
			}
			set
			{
				this.m_Smoothing = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0003201A File Offset: 0x0003021A
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00032022 File Offset: 0x00030222
		public float smoothingAngle
		{
			get
			{
				return this.m_SmoothingThreshold;
			}
			set
			{
				this.m_SmoothingThreshold = value;
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0003202B File Offset: 0x0003022B
		public override string ToString()
		{
			return string.Format("quads: {0}\nsmoothing: {1}\nthreshold: {2}", this.quads, this.smoothing, this.smoothingAngle);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00032058 File Offset: 0x00030258
		public MeshImportSettings()
		{
		}

		// Token: 0x04000267 RID: 615
		[SerializeField]
		private bool m_Quads = true;

		// Token: 0x04000268 RID: 616
		[SerializeField]
		private bool m_Smoothing = true;

		// Token: 0x04000269 RID: 617
		[SerializeField]
		private float m_SmoothingThreshold = 1f;
	}
}

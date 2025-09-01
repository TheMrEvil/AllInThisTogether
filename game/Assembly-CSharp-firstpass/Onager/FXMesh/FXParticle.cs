using System;
using UnityEngine;

namespace Onager.FXMesh
{
	// Token: 0x02000166 RID: 358
	[RequireComponent(typeof(ParticleSystem))]
	[ExecuteInEditMode]
	public class FXParticle : MonoBehaviour
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x0005D16B File Offset: 0x0005B36B
		public FXMeshSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0005D174 File Offset: 0x0005B374
		private ParticleSystemRenderer psr
		{
			get
			{
				if (!this._psr)
				{
					return this._psr = base.GetComponent<ParticleSystemRenderer>();
				}
				return this._psr;
			}
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0005D1A4 File Offset: 0x0005B3A4
		private void Awake()
		{
			this.ApplyMesh();
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0005D1AC File Offset: 0x0005B3AC
		public void ApplyMesh()
		{
			if (this.settings == null)
			{
				return;
			}
			this.psr.mesh = this.Settings.GetMesh(false);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0005D1D4 File Offset: 0x0005B3D4
		public FXParticle()
		{
		}

		// Token: 0x04000BC2 RID: 3010
		[SerializeField]
		private FXMeshSettings settings;

		// Token: 0x04000BC3 RID: 3011
		private ParticleSystemRenderer _psr;
	}
}

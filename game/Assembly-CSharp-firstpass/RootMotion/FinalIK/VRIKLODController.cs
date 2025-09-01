using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000121 RID: 289
	public class VRIKLODController : MonoBehaviour
	{
		// Token: 0x06000C7F RID: 3199 RVA: 0x0005495B File Offset: 0x00052B5B
		private void Start()
		{
			this.ik = base.GetComponent<VRIK>();
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00054969 File Offset: 0x00052B69
		private void Update()
		{
			this.ik.solver.LOD = this.GetLODLevel();
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00054984 File Offset: 0x00052B84
		private int GetLODLevel()
		{
			if (this.allowCulled)
			{
				if (this.LODRenderer == null)
				{
					return 0;
				}
				if (!this.LODRenderer.isVisible)
				{
					return 2;
				}
			}
			if ((this.ik.transform.position - Camera.main.transform.position).sqrMagnitude > this.LODDistance * this.LODDistance)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x000549F6 File Offset: 0x00052BF6
		public VRIKLODController()
		{
		}

		// Token: 0x040009C8 RID: 2504
		public Renderer LODRenderer;

		// Token: 0x040009C9 RID: 2505
		public float LODDistance = 15f;

		// Token: 0x040009CA RID: 2506
		public bool allowCulled = true;

		// Token: 0x040009CB RID: 2507
		private VRIK ik;
	}
}

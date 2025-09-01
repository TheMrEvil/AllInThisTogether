using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fluxy
{
	// Token: 0x02000013 RID: 19
	[AddComponentMenu("Physics/FluXY/TargetProviders/Target Detector", 800)]
	public class FluxyTargetDetector : FluxyTargetProvider
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00005E83 File Offset: 0x00004083
		public void OnValidate()
		{
			Array.Resize<Collider>(ref this.colliders, this.maxColliders);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005E96 File Offset: 0x00004096
		public void Awake()
		{
			Array.Resize<Collider>(ref this.colliders, this.maxColliders);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005EAC File Offset: 0x000040AC
		public override List<FluxyTarget> GetTargets()
		{
			this.targets.Clear();
			int num = Physics.OverlapBoxNonAlloc(base.transform.position, this.size * 0.5f, this.colliders, Quaternion.identity, this.layers);
			for (int i = 0; i < num; i++)
			{
				FluxyTarget item;
				if (this.colliders[i].TryGetComponent<FluxyTarget>(out item))
				{
					this.targets.Add(item);
				}
			}
			return this.targets;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005F2A File Offset: 0x0000412A
		public void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(0.5f, 0.8f, 1f, 0.5f);
			Gizmos.DrawWireCube(base.transform.position, this.size);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005F60 File Offset: 0x00004160
		public FluxyTargetDetector()
		{
		}

		// Token: 0x0400009F RID: 159
		public Vector3 size = new Vector3(0.5f, 0.5f, 0.5f);

		// Token: 0x040000A0 RID: 160
		public int maxColliders = 32;

		// Token: 0x040000A1 RID: 161
		public LayerMask layers = -1;

		// Token: 0x040000A2 RID: 162
		private Collider[] colliders = new Collider[0];

		// Token: 0x040000A3 RID: 163
		private List<FluxyTarget> targets = new List<FluxyTarget>();
	}
}

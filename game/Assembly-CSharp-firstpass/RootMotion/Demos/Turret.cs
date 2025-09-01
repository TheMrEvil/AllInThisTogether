using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000153 RID: 339
	public class Turret : MonoBehaviour
	{
		// Token: 0x06000D4E RID: 3406 RVA: 0x00059EC8 File Offset: 0x000580C8
		private void Update()
		{
			Turret.Part[] array = this.parts;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].AimAt(this.target);
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00059EF8 File Offset: 0x000580F8
		public Turret()
		{
		}

		// Token: 0x04000AFD RID: 2813
		public Transform target;

		// Token: 0x04000AFE RID: 2814
		public Turret.Part[] parts;

		// Token: 0x02000238 RID: 568
		[Serializable]
		public class Part
		{
			// Token: 0x060011CA RID: 4554 RVA: 0x0006E5BC File Offset: 0x0006C7BC
			public void AimAt(Transform target)
			{
				this.transform.LookAt(target.position, this.transform.up);
				if (this.rotationLimit == null)
				{
					this.rotationLimit = this.transform.GetComponent<RotationLimit>();
					this.rotationLimit.Disable();
				}
				this.rotationLimit.Apply();
			}

			// Token: 0x060011CB RID: 4555 RVA: 0x0006E61B File Offset: 0x0006C81B
			public Part()
			{
			}

			// Token: 0x040010BA RID: 4282
			public Transform transform;

			// Token: 0x040010BB RID: 4283
			private RotationLimit rotationLimit;
		}
	}
}

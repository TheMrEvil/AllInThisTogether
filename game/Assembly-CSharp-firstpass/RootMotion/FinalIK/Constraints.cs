using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000CD RID: 205
	[Serializable]
	public class Constraints
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x0003B09A File Offset: 0x0003929A
		public bool IsValid()
		{
			return this.transform != null;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0003B0A8 File Offset: 0x000392A8
		public void Initiate(Transform transform)
		{
			this.transform = transform;
			this.position = transform.position;
			this.rotation = transform.eulerAngles;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0003B0CC File Offset: 0x000392CC
		public void Update()
		{
			if (!this.IsValid())
			{
				return;
			}
			if (this.target != null)
			{
				this.position = this.target.position;
			}
			this.transform.position += this.positionOffset;
			if (this.positionWeight > 0f)
			{
				this.transform.position = Vector3.Lerp(this.transform.position, this.position, this.positionWeight);
			}
			if (this.target != null)
			{
				this.rotation = this.target.eulerAngles;
			}
			this.transform.rotation = Quaternion.Euler(this.rotationOffset) * this.transform.rotation;
			if (this.rotationWeight > 0f)
			{
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(this.rotation), this.rotationWeight);
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0003B1CF File Offset: 0x000393CF
		public Constraints()
		{
		}

		// Token: 0x040006EB RID: 1771
		public Transform transform;

		// Token: 0x040006EC RID: 1772
		public Transform target;

		// Token: 0x040006ED RID: 1773
		public Vector3 positionOffset;

		// Token: 0x040006EE RID: 1774
		public Vector3 position;

		// Token: 0x040006EF RID: 1775
		[Range(0f, 1f)]
		public float positionWeight;

		// Token: 0x040006F0 RID: 1776
		public Vector3 rotationOffset;

		// Token: 0x040006F1 RID: 1777
		public Vector3 rotation;

		// Token: 0x040006F2 RID: 1778
		[Range(0f, 1f)]
		public float rotationWeight;
	}
}

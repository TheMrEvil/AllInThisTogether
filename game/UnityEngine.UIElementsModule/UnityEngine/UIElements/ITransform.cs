using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000045 RID: 69
	public interface ITransform
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001B2 RID: 434
		// (set) Token: 0x060001B3 RID: 435
		Vector3 position { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001B4 RID: 436
		// (set) Token: 0x060001B5 RID: 437
		Quaternion rotation { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001B6 RID: 438
		// (set) Token: 0x060001B7 RID: 439
		Vector3 scale { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001B8 RID: 440
		Matrix4x4 matrix { get; }
	}
}

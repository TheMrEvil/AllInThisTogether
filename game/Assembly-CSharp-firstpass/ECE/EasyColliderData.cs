using System;
using UnityEngine;

namespace ECE
{
	// Token: 0x0200006A RID: 106
	public class EasyColliderData
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x00022248 File Offset: 0x00020448
		public void Clone(EasyColliderData data)
		{
			this.ColliderType = data.ColliderType;
			this.IsValid = data.IsValid;
			this.Matrix = data.Matrix;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0002226E File Offset: 0x0002046E
		public EasyColliderData()
		{
		}

		// Token: 0x04000416 RID: 1046
		public CREATE_COLLIDER_TYPE ColliderType;

		// Token: 0x04000417 RID: 1047
		public bool IsValid;

		// Token: 0x04000418 RID: 1048
		public Matrix4x4 Matrix;
	}
}

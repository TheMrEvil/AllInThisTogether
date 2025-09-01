using System;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x0200001D RID: 29
	public class MagicaSphereCollider : ColliderComponent
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00005302 File Offset: 0x00003502
		public override ColliderManager.ColliderType GetColliderType()
		{
			return ColliderManager.ColliderType.Sphere;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000560C File Offset: 0x0000380C
		public override void DataValidate()
		{
			this.size.x = Mathf.Max(this.size.x, 0.001f);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000562E File Offset: 0x0000382E
		public void SetSize(float radius)
		{
			base.SetSize(new Vector3(radius, 0f, 0f));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005601 File Offset: 0x00003801
		public MagicaSphereCollider()
		{
		}
	}
}

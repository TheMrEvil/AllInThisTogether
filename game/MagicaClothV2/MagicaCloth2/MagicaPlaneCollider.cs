using System;

namespace MagicaCloth2
{
	// Token: 0x0200001C RID: 28
	public class MagicaPlaneCollider : ColliderComponent
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00005609 File Offset: 0x00003809
		public override ColliderManager.ColliderType GetColliderType()
		{
			return ColliderManager.ColliderType.Plane;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005305 File Offset: 0x00003505
		public override void DataValidate()
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005601 File Offset: 0x00003801
		public MagicaPlaneCollider()
		{
		}
	}
}

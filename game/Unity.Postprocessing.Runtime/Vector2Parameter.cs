using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200004D RID: 77
	[Serializable]
	public sealed class Vector2Parameter : ParameterOverride<Vector2>
	{
		// Token: 0x06000106 RID: 262 RVA: 0x0000B120 File Offset: 0x00009320
		public override void Interp(Vector2 from, Vector2 to, float t)
		{
			this.value.x = from.x + (to.x - from.x) * t;
			this.value.y = from.y + (to.y - from.y) * t;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000B16F File Offset: 0x0000936F
		public static implicit operator Vector3(Vector2Parameter prop)
		{
			return prop.value;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000B17C File Offset: 0x0000937C
		public static implicit operator Vector4(Vector2Parameter prop)
		{
			return prop.value;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000B189 File Offset: 0x00009389
		public Vector2Parameter()
		{
		}
	}
}

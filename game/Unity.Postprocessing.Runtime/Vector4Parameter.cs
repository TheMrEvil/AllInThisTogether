using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	public sealed class Vector4Parameter : ParameterOverride<Vector4>
	{
		// Token: 0x0600010E RID: 270 RVA: 0x0000B228 File Offset: 0x00009428
		public override void Interp(Vector4 from, Vector4 to, float t)
		{
			this.value.x = from.x + (to.x - from.x) * t;
			this.value.y = from.y + (to.y - from.y) * t;
			this.value.z = from.z + (to.z - from.z) * t;
			this.value.w = from.w + (to.w - from.w) * t;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000B2B9 File Offset: 0x000094B9
		public static implicit operator Vector2(Vector4Parameter prop)
		{
			return prop.value;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000B2C6 File Offset: 0x000094C6
		public static implicit operator Vector3(Vector4Parameter prop)
		{
			return prop.value;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000B2D3 File Offset: 0x000094D3
		public Vector4Parameter()
		{
		}
	}
}

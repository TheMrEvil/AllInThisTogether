using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200004E RID: 78
	[Serializable]
	public sealed class Vector3Parameter : ParameterOverride<Vector3>
	{
		// Token: 0x0600010A RID: 266 RVA: 0x0000B194 File Offset: 0x00009394
		public override void Interp(Vector3 from, Vector3 to, float t)
		{
			this.value.x = from.x + (to.x - from.x) * t;
			this.value.y = from.y + (to.y - from.y) * t;
			this.value.z = from.z + (to.z - from.z) * t;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000B204 File Offset: 0x00009404
		public static implicit operator Vector2(Vector3Parameter prop)
		{
			return prop.value;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000B211 File Offset: 0x00009411
		public static implicit operator Vector4(Vector3Parameter prop)
		{
			return prop.value;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000B21E File Offset: 0x0000941E
		public Vector3Parameter()
		{
		}
	}
}

using System;
using UnityEngine;

namespace QFSW.QC.Serializers
{
	// Token: 0x0200000F RID: 15
	public class Vector4Serializer : BasicQcSerializer<Vector4>
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000023E4 File Offset: 0x000005E4
		public override string SerializeFormatted(Vector4 value, QuantumTheme theme)
		{
			return string.Format("({0}, {1}, {2}, {3})", new object[]
			{
				value.x,
				value.y,
				value.z,
				value.w
			});
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002439 File Offset: 0x00000639
		public Vector4Serializer()
		{
		}
	}
}

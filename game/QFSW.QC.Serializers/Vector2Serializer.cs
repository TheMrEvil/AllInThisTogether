using System;
using UnityEngine;

namespace QFSW.QC.Serializers
{
	// Token: 0x0200000C RID: 12
	public class Vector2Serializer : BasicQcSerializer<Vector2>
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000234B File Offset: 0x0000054B
		public override string SerializeFormatted(Vector2 value, QuantumTheme theme)
		{
			return string.Format("({0}, {1})", value.x, value.y);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000236D File Offset: 0x0000056D
		public Vector2Serializer()
		{
		}
	}
}

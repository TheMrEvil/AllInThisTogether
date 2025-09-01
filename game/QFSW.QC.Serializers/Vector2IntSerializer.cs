using System;
using UnityEngine;

namespace QFSW.QC.Serializers
{
	// Token: 0x0200000B RID: 11
	public class Vector2IntSerializer : BasicQcSerializer<Vector2Int>
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000231F File Offset: 0x0000051F
		public override string SerializeFormatted(Vector2Int value, QuantumTheme theme)
		{
			return string.Format("({0}, {1})", value.x, value.y);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002343 File Offset: 0x00000543
		public Vector2IntSerializer()
		{
		}
	}
}

using System;
using UnityEngine;

namespace QFSW.QC.Serializers
{
	// Token: 0x0200000E RID: 14
	public class Vector3Serializer : BasicQcSerializer<Vector3>
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000023AD File Offset: 0x000005AD
		public override string SerializeFormatted(Vector3 value, QuantumTheme theme)
		{
			return string.Format("({0}, {1}, {2})", value.x, value.y, value.z);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023DA File Offset: 0x000005DA
		public Vector3Serializer()
		{
		}
	}
}

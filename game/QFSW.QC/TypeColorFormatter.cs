using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.Serialization;

namespace QFSW.QC
{
	// Token: 0x0200004E RID: 78
	[Serializable]
	public class TypeColorFormatter : TypeFormatter
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00008270 File Offset: 0x00006470
		[Preserve]
		public TypeColorFormatter(Type type) : base(type)
		{
		}

		// Token: 0x0400012C RID: 300
		[FormerlySerializedAs("color")]
		public Color Color = Color.white;
	}
}

using System;
using UnityEngine.Scripting;
using UnityEngine.Serialization;

namespace QFSW.QC
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	public class CollectionFormatter : TypeFormatter
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00008284 File Offset: 0x00006484
		[Preserve]
		public CollectionFormatter(Type type) : base(type)
		{
		}

		// Token: 0x0400012D RID: 301
		[FormerlySerializedAs("seperatorString")]
		public string SeperatorString = ",";

		// Token: 0x0400012E RID: 302
		[FormerlySerializedAs("leftScoper")]
		public string LeftScoper = "[";

		// Token: 0x0400012F RID: 303
		[FormerlySerializedAs("rightScoper")]
		public string RightScoper = "]";
	}
}

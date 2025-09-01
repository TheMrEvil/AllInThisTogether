using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000207 RID: 519
	internal class EnumInfo
	{
		// Token: 0x060016C7 RID: 5831 RVA: 0x00024CC4 File Offset: 0x00022EC4
		[UsedByNativeCode]
		internal static EnumInfo CreateEnumInfoFromNativeEnum(string[] names, int[] values, string[] annotations, bool isFlags)
		{
			return new EnumInfo
			{
				names = names,
				values = values,
				annotations = annotations,
				isFlags = isFlags
			};
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00002072 File Offset: 0x00000272
		public EnumInfo()
		{
		}

		// Token: 0x040007F5 RID: 2037
		public string[] names;

		// Token: 0x040007F6 RID: 2038
		public int[] values;

		// Token: 0x040007F7 RID: 2039
		public string[] annotations;

		// Token: 0x040007F8 RID: 2040
		public bool isFlags;
	}
}

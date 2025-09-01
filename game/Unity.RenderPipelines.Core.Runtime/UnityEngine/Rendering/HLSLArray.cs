using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000088 RID: 136
	[AttributeUsage(AttributeTargets.Field)]
	public class HLSLArray : Attribute
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x00014423 File Offset: 0x00012623
		public HLSLArray(int arraySize, Type elementType)
		{
			this.arraySize = arraySize;
			this.elementType = elementType;
		}

		// Token: 0x040002D1 RID: 721
		public int arraySize;

		// Token: 0x040002D2 RID: 722
		public Type elementType;
	}
}

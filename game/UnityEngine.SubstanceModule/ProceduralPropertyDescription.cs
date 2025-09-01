using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[Obsolete("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ProceduralPropertyDescription
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002077 File Offset: 0x00000277
		public ProceduralPropertyDescription()
		{
		}

		// Token: 0x0400002B RID: 43
		public string name;

		// Token: 0x0400002C RID: 44
		public string label;

		// Token: 0x0400002D RID: 45
		public string group;

		// Token: 0x0400002E RID: 46
		public ProceduralPropertyType type;

		// Token: 0x0400002F RID: 47
		public bool hasRange;

		// Token: 0x04000030 RID: 48
		public float minimum;

		// Token: 0x04000031 RID: 49
		public float maximum;

		// Token: 0x04000032 RID: 50
		public float step;

		// Token: 0x04000033 RID: 51
		public string[] enumOptions;

		// Token: 0x04000034 RID: 52
		public string[] componentLabels;
	}
}

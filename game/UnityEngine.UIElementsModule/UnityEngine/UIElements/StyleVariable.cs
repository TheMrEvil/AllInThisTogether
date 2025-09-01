using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B4 RID: 692
	internal struct StyleVariable
	{
		// Token: 0x06001781 RID: 6017 RVA: 0x000624CD File Offset: 0x000606CD
		public StyleVariable(string name, StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.name = name;
			this.sheet = sheet;
			this.handles = handles;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000624E8 File Offset: 0x000606E8
		public override int GetHashCode()
		{
			int num = this.name.GetHashCode();
			num = (num * 397 ^ this.sheet.GetHashCode());
			return num * 397 ^ this.handles.GetHashCode();
		}

		// Token: 0x04000A21 RID: 2593
		public readonly string name;

		// Token: 0x04000A22 RID: 2594
		public readonly StyleSheet sheet;

		// Token: 0x04000A23 RID: 2595
		public readonly StyleValueHandle[] handles;
	}
}

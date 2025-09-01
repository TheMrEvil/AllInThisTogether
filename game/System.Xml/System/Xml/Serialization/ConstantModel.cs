using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x0200029D RID: 669
	internal class ConstantModel
	{
		// Token: 0x06001924 RID: 6436 RVA: 0x0009069F File Offset: 0x0008E89F
		internal ConstantModel(FieldInfo fieldInfo, long value)
		{
			this.fieldInfo = fieldInfo;
			this.value = value;
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x000906B5 File Offset: 0x0008E8B5
		internal string Name
		{
			get
			{
				return this.fieldInfo.Name;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x000906C2 File Offset: 0x0008E8C2
		internal long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x000906CA File Offset: 0x0008E8CA
		internal FieldInfo FieldInfo
		{
			get
			{
				return this.fieldInfo;
			}
		}

		// Token: 0x04001917 RID: 6423
		private FieldInfo fieldInfo;

		// Token: 0x04001918 RID: 6424
		private long value;
	}
}

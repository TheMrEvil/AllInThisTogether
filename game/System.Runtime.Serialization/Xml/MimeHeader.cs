using System;
using System.Runtime.Serialization;

namespace System.Xml
{
	// Token: 0x02000078 RID: 120
	internal class MimeHeader
	{
		// Token: 0x060006AE RID: 1710 RVA: 0x0001CA70 File Offset: 0x0001AC70
		public MimeHeader(string name, string value)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			this.name = name;
			this.value = value;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001CA94 File Offset: 0x0001AC94
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001CA9C File Offset: 0x0001AC9C
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040002F4 RID: 756
		private string name;

		// Token: 0x040002F5 RID: 757
		private string value;
	}
}

using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002DC RID: 732
	internal class ImportStructWorkItem
	{
		// Token: 0x06001C72 RID: 7282 RVA: 0x000A3954 File Offset: 0x000A1B54
		internal ImportStructWorkItem(StructModel model, StructMapping mapping)
		{
			this.model = model;
			this.mapping = mapping;
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x000A396A File Offset: 0x000A1B6A
		internal StructModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x000A3972 File Offset: 0x000A1B72
		internal StructMapping Mapping
		{
			get
			{
				return this.mapping;
			}
		}

		// Token: 0x04001A17 RID: 6679
		private StructModel model;

		// Token: 0x04001A18 RID: 6680
		private StructMapping mapping;
	}
}

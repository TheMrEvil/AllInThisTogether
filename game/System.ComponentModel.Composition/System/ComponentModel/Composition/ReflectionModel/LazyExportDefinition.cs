using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000083 RID: 131
	internal class LazyExportDefinition : ExportDefinition
	{
		// Token: 0x0600037D RID: 893 RVA: 0x0000A8DF File Offset: 0x00008ADF
		public LazyExportDefinition(string contractName, Lazy<IDictionary<string, object>> metadata) : base(contractName, null)
		{
			this._metadata = metadata;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public override IDictionary<string, object> Metadata
		{
			get
			{
				return this._metadata.Value ?? MetadataServices.EmptyMetadata;
			}
		}

		// Token: 0x0400016C RID: 364
		private readonly Lazy<IDictionary<string, object>> _metadata;
	}
}

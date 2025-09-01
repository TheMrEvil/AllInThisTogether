using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x0200006D RID: 109
	internal class PartCreatorExportDefinition : ExportDefinition
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00008AB3 File Offset: 0x00006CB3
		public PartCreatorExportDefinition(ExportDefinition productDefinition)
		{
			this._productDefinition = productDefinition;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00008AC2 File Offset: 0x00006CC2
		public override string ContractName
		{
			get
			{
				return "System.ComponentModel.Composition.Contracts.ExportFactory";
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00008ACC File Offset: 0x00006CCC
		public override IDictionary<string, object> Metadata
		{
			get
			{
				if (this._metadata == null)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>(this._productDefinition.Metadata);
					dictionary["ExportTypeIdentity"] = CompositionConstants.PartCreatorTypeIdentity;
					dictionary["ProductDefinition"] = this._productDefinition;
					this._metadata = dictionary.AsReadOnly();
				}
				return this._metadata;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008B28 File Offset: 0x00006D28
		internal static bool IsProductConstraintSatisfiedBy(ImportDefinition productImportDefinition, ExportDefinition exportDefinition)
		{
			object obj = null;
			if (exportDefinition.Metadata.TryGetValue("ProductDefinition", out obj))
			{
				ExportDefinition exportDefinition2 = obj as ExportDefinition;
				if (exportDefinition2 != null)
				{
					return productImportDefinition.IsConstraintSatisfiedBy(exportDefinition2);
				}
			}
			return false;
		}

		// Token: 0x0400012B RID: 299
		private readonly ExportDefinition _productDefinition;

		// Token: 0x0400012C RID: 300
		private IDictionary<string, object> _metadata;
	}
}

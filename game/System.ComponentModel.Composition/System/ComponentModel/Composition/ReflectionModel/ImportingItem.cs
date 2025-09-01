using System;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000068 RID: 104
	internal abstract class ImportingItem
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0000811D File Offset: 0x0000631D
		protected ImportingItem(ContractBasedImportDefinition definition, ImportType importType)
		{
			Assumes.NotNull<ContractBasedImportDefinition>(definition);
			this._definition = definition;
			this._importType = importType;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00008139 File Offset: 0x00006339
		public ContractBasedImportDefinition Definition
		{
			get
			{
				return this._definition;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00008141 File Offset: 0x00006341
		public ImportType ImportType
		{
			get
			{
				return this._importType;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00008149 File Offset: 0x00006349
		public object CastExportsToImportType(Export[] exports)
		{
			if (this.Definition.Cardinality == ImportCardinality.ZeroOrMore)
			{
				return this.CastExportsToCollectionImportType(exports);
			}
			return this.CastExportsToSingleImportType(exports);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00008168 File Offset: 0x00006368
		private object CastExportsToCollectionImportType(Export[] exports)
		{
			Assumes.NotNull<Export[]>(exports);
			Type type = this.ImportType.ElementType ?? typeof(object);
			Array array = Array.CreateInstance(type, exports.Length);
			for (int i = 0; i < array.Length; i++)
			{
				object value = this.CastSingleExportToImportType(type, exports[i]);
				array.SetValue(value, i);
			}
			return array;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000081C4 File Offset: 0x000063C4
		private object CastExportsToSingleImportType(Export[] exports)
		{
			Assumes.NotNull<Export[]>(exports);
			Assumes.IsTrue(exports.Length < 2);
			if (exports.Length == 0)
			{
				return null;
			}
			return this.CastSingleExportToImportType(this.ImportType.ActualType, exports[0]);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000081F1 File Offset: 0x000063F1
		private object CastSingleExportToImportType(Type type, Export export)
		{
			if (this.ImportType.CastExport != null)
			{
				return this.ImportType.CastExport(export);
			}
			return this.Cast(type, export);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000821C File Offset: 0x0000641C
		private object Cast(Type type, Export export)
		{
			object value = export.Value;
			object result;
			if (!ContractServices.TryCast(type, value, out result))
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportNotAssignableFromExport, export.ToElement().DisplayName, type.FullName), this.Definition.ToElement());
			}
			return result;
		}

		// Token: 0x04000121 RID: 289
		private readonly ContractBasedImportDefinition _definition;

		// Token: 0x04000122 RID: 290
		private readonly ImportType _importType;
	}
}

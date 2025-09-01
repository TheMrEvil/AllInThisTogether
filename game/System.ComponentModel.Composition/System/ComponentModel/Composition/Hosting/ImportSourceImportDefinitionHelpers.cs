using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq.Expressions;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Hosting
{
	// Token: 0x020000F6 RID: 246
	internal static class ImportSourceImportDefinitionHelpers
	{
		// Token: 0x06000679 RID: 1657 RVA: 0x00014134 File Offset: 0x00012334
		public static ImportDefinition RemoveImportSource(this ImportDefinition definition)
		{
			ContractBasedImportDefinition contractBasedImportDefinition = definition as ContractBasedImportDefinition;
			if (contractBasedImportDefinition == null)
			{
				return definition;
			}
			return new ImportSourceImportDefinitionHelpers.NonImportSourceImportDefinition(contractBasedImportDefinition);
		}

		// Token: 0x020000F7 RID: 247
		internal class NonImportSourceImportDefinition : ContractBasedImportDefinition
		{
			// Token: 0x0600067A RID: 1658 RVA: 0x00014153 File Offset: 0x00012353
			public NonImportSourceImportDefinition(ContractBasedImportDefinition sourceDefinition)
			{
				Assumes.NotNull<ContractBasedImportDefinition>(sourceDefinition);
				this._sourceDefinition = sourceDefinition;
				this._metadata = null;
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001416F File Offset: 0x0001236F
			public override string ContractName
			{
				get
				{
					return this._sourceDefinition.ContractName;
				}
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001417C File Offset: 0x0001237C
			public override IDictionary<string, object> Metadata
			{
				get
				{
					IDictionary<string, object> dictionary = this._metadata;
					if (dictionary == null)
					{
						dictionary = new Dictionary<string, object>(this._sourceDefinition.Metadata);
						dictionary.Remove("System.ComponentModel.Composition.ImportSource");
						this._metadata = dictionary;
					}
					return dictionary;
				}
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x0600067D RID: 1661 RVA: 0x000141B8 File Offset: 0x000123B8
			public override ImportCardinality Cardinality
			{
				get
				{
					return this._sourceDefinition.Cardinality;
				}
			}

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x0600067E RID: 1662 RVA: 0x000141C5 File Offset: 0x000123C5
			public override Expression<Func<ExportDefinition, bool>> Constraint
			{
				get
				{
					return this._sourceDefinition.Constraint;
				}
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x0600067F RID: 1663 RVA: 0x000141D2 File Offset: 0x000123D2
			public override bool IsPrerequisite
			{
				get
				{
					return this._sourceDefinition.IsPrerequisite;
				}
			}

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x06000680 RID: 1664 RVA: 0x000141DF File Offset: 0x000123DF
			public override bool IsRecomposable
			{
				get
				{
					return this._sourceDefinition.IsRecomposable;
				}
			}

			// Token: 0x06000681 RID: 1665 RVA: 0x000141EC File Offset: 0x000123EC
			public override bool IsConstraintSatisfiedBy(ExportDefinition exportDefinition)
			{
				Requires.NotNull<ExportDefinition>(exportDefinition, "exportDefinition");
				return this._sourceDefinition.IsConstraintSatisfiedBy(exportDefinition);
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x00014205 File Offset: 0x00012405
			public override string ToString()
			{
				return this._sourceDefinition.ToString();
			}

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x06000683 RID: 1667 RVA: 0x00014212 File Offset: 0x00012412
			public override string RequiredTypeIdentity
			{
				get
				{
					return this._sourceDefinition.RequiredTypeIdentity;
				}
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001421F File Offset: 0x0001241F
			public override IEnumerable<KeyValuePair<string, Type>> RequiredMetadata
			{
				get
				{
					return this._sourceDefinition.RequiredMetadata;
				}
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001422C File Offset: 0x0001242C
			public override CreationPolicy RequiredCreationPolicy
			{
				get
				{
					return this._sourceDefinition.RequiredCreationPolicy;
				}
			}

			// Token: 0x040002CD RID: 717
			private ContractBasedImportDefinition _sourceDefinition;

			// Token: 0x040002CE RID: 718
			private IDictionary<string, object> _metadata;
		}
	}
}

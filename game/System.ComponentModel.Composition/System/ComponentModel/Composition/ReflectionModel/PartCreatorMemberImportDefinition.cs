using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq.Expressions;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x0200006E RID: 110
	internal class PartCreatorMemberImportDefinition : ReflectionMemberImportDefinition, IPartCreatorImportDefinition
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x00008B60 File Offset: 0x00006D60
		public PartCreatorMemberImportDefinition(LazyMemberInfo importingLazyMember, ICompositionElement origin, ContractBasedImportDefinition productImportDefinition) : base(importingLazyMember, "System.ComponentModel.Composition.Contracts.ExportFactory", CompositionConstants.PartCreatorTypeIdentity, productImportDefinition.RequiredMetadata, productImportDefinition.Cardinality, productImportDefinition.IsRecomposable, false, productImportDefinition.RequiredCreationPolicy, MetadataServices.EmptyMetadata, origin)
		{
			Assumes.NotNull<ContractBasedImportDefinition>(productImportDefinition);
			this._productImportDefinition = productImportDefinition;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00008BAA File Offset: 0x00006DAA
		public ContractBasedImportDefinition ProductImportDefinition
		{
			get
			{
				return this._productImportDefinition;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008BB2 File Offset: 0x00006DB2
		public override bool IsConstraintSatisfiedBy(ExportDefinition exportDefinition)
		{
			return base.IsConstraintSatisfiedBy(exportDefinition) && PartCreatorExportDefinition.IsProductConstraintSatisfiedBy(this._productImportDefinition, exportDefinition);
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00008BCB File Offset: 0x00006DCB
		public override Expression<Func<ExportDefinition, bool>> Constraint
		{
			get
			{
				return ConstraintServices.CreatePartCreatorConstraint(base.Constraint, this._productImportDefinition);
			}
		}

		// Token: 0x0400012D RID: 301
		private readonly ContractBasedImportDefinition _productImportDefinition;
	}
}

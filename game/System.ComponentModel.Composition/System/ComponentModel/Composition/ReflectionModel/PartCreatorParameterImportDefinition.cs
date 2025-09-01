using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x0200006F RID: 111
	internal class PartCreatorParameterImportDefinition : ReflectionParameterImportDefinition, IPartCreatorImportDefinition
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00008BE0 File Offset: 0x00006DE0
		public PartCreatorParameterImportDefinition(Lazy<ParameterInfo> importingLazyParameter, ICompositionElement origin, ContractBasedImportDefinition productImportDefinition) : base(importingLazyParameter, "System.ComponentModel.Composition.Contracts.ExportFactory", CompositionConstants.PartCreatorTypeIdentity, productImportDefinition.RequiredMetadata, productImportDefinition.Cardinality, CreationPolicy.Any, MetadataServices.EmptyMetadata, origin)
		{
			Assumes.NotNull<ContractBasedImportDefinition>(productImportDefinition);
			this._productImportDefinition = productImportDefinition;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00008C1E File Offset: 0x00006E1E
		public ContractBasedImportDefinition ProductImportDefinition
		{
			get
			{
				return this._productImportDefinition;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00008C26 File Offset: 0x00006E26
		public override bool IsConstraintSatisfiedBy(ExportDefinition exportDefinition)
		{
			return base.IsConstraintSatisfiedBy(exportDefinition) && PartCreatorExportDefinition.IsProductConstraintSatisfiedBy(this._productImportDefinition, exportDefinition);
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00008C3F File Offset: 0x00006E3F
		public override Expression<Func<ExportDefinition, bool>> Constraint
		{
			get
			{
				return ConstraintServices.CreatePartCreatorConstraint(base.Constraint, this._productImportDefinition);
			}
		}

		// Token: 0x0400012E RID: 302
		private readonly ContractBasedImportDefinition _productImportDefinition;
	}
}

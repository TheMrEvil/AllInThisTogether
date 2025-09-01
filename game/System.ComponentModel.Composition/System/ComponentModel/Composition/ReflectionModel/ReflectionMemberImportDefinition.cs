using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x0200007C RID: 124
	internal class ReflectionMemberImportDefinition : ReflectionImportDefinition
	{
		// Token: 0x06000341 RID: 833 RVA: 0x00009FE0 File Offset: 0x000081E0
		public ReflectionMemberImportDefinition(LazyMemberInfo importingLazyMember, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata, ICompositionElement origin) : base(contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, isPrerequisite, requiredCreationPolicy, metadata, origin)
		{
			Assumes.NotNull<string>(contractName);
			this._importingLazyMember = importingLazyMember;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000A010 File Offset: 0x00008210
		public override ImportingItem ToImportingItem()
		{
			ReflectionWritableMember reflectionWritableMember = this.ImportingLazyMember.ToReflectionWriteableMember();
			return new ImportingMember(this, reflectionWritableMember, new ImportType(reflectionWritableMember.ReturnType, this.Cardinality));
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000A041 File Offset: 0x00008241
		public LazyMemberInfo ImportingLazyMember
		{
			get
			{
				return this._importingLazyMember;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000A049 File Offset: 0x00008249
		protected override string GetDisplayName()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} (ContractName=\"{1}\")", this.ImportingLazyMember.ToReflectionMember().GetDisplayName(), this.ContractName);
		}

		// Token: 0x04000157 RID: 343
		private LazyMemberInfo _importingLazyMember;
	}
}

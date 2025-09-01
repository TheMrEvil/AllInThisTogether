using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000077 RID: 119
	internal abstract class ReflectionImportDefinition : ContractBasedImportDefinition, ICompositionElement
	{
		// Token: 0x06000324 RID: 804 RVA: 0x00009EB4 File Offset: 0x000080B4
		public ReflectionImportDefinition(string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata, ICompositionElement origin) : base(contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, isPrerequisite, requiredCreationPolicy, metadata)
		{
			this._origin = origin;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00009EDC File Offset: 0x000080DC
		string ICompositionElement.DisplayName
		{
			get
			{
				return this.GetDisplayName();
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00009EE4 File Offset: 0x000080E4
		ICompositionElement ICompositionElement.Origin
		{
			get
			{
				return this._origin;
			}
		}

		// Token: 0x06000327 RID: 807
		public abstract ImportingItem ToImportingItem();

		// Token: 0x06000328 RID: 808
		protected abstract string GetDisplayName();

		// Token: 0x0400014C RID: 332
		private readonly ICompositionElement _origin;
	}
}

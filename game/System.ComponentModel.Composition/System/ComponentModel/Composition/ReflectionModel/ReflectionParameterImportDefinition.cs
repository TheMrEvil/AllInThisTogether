using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000085 RID: 133
	internal class ReflectionParameterImportDefinition : ReflectionImportDefinition
	{
		// Token: 0x06000385 RID: 901 RVA: 0x0000A96C File Offset: 0x00008B6C
		public ReflectionParameterImportDefinition(Lazy<ParameterInfo> importingLazyParameter, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata, ICompositionElement origin) : base(contractName, requiredTypeIdentity, requiredMetadata, cardinality, false, true, requiredCreationPolicy, metadata, origin)
		{
			Assumes.NotNull<Lazy<ParameterInfo>>(importingLazyParameter);
			this._importingLazyParameter = importingLazyParameter;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000A99A File Offset: 0x00008B9A
		public override ImportingItem ToImportingItem()
		{
			return new ImportingParameter(this, new ImportType(this.ImportingLazyParameter.GetNotNullValue("parameter").ParameterType, this.Cardinality));
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000A9C2 File Offset: 0x00008BC2
		public Lazy<ParameterInfo> ImportingLazyParameter
		{
			get
			{
				return this._importingLazyParameter;
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000A9CC File Offset: 0x00008BCC
		protected override string GetDisplayName()
		{
			ParameterInfo notNullValue = this.ImportingLazyParameter.GetNotNullValue("parameter");
			return string.Format(CultureInfo.CurrentCulture, "{0} (Parameter=\"{1}\", ContractName=\"{2}\")", notNullValue.Member.GetDisplayName(), notNullValue.Name, this.ContractName);
		}

		// Token: 0x0400016E RID: 366
		private Lazy<ParameterInfo> _importingLazyParameter;
	}
}

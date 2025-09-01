using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.AttributedModel
{
	// Token: 0x02000101 RID: 257
	internal class AttributedExportDefinition : ExportDefinition
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x00014D00 File Offset: 0x00012F00
		public AttributedExportDefinition(AttributedPartCreationInfo partCreationInfo, MemberInfo member, ExportAttribute exportAttribute, Type typeIdentityType, string contractName) : base(contractName, null)
		{
			Assumes.NotNull<AttributedPartCreationInfo>(partCreationInfo);
			Assumes.NotNull<MemberInfo>(member);
			Assumes.NotNull<ExportAttribute>(exportAttribute);
			this._partCreationInfo = partCreationInfo;
			this._member = member;
			this._exportAttribute = exportAttribute;
			this._typeIdentityType = typeIdentityType;
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00014D3C File Offset: 0x00012F3C
		public override IDictionary<string, object> Metadata
		{
			get
			{
				if (this._metadata == null)
				{
					IDictionary<string, object> dictionary;
					this._member.TryExportMetadataForMember(out dictionary);
					string value = this._exportAttribute.IsContractNameSameAsTypeIdentity() ? this.ContractName : this._member.GetTypeIdentityFromExport(this._typeIdentityType);
					dictionary.Add("ExportTypeIdentity", value);
					IDictionary<string, object> metadata = this._partCreationInfo.GetMetadata();
					if (metadata != null && metadata.ContainsKey("System.ComponentModel.Composition.CreationPolicy"))
					{
						dictionary.Add("System.ComponentModel.Composition.CreationPolicy", metadata["System.ComponentModel.Composition.CreationPolicy"]);
					}
					if (this._typeIdentityType != null && this._member.MemberType != MemberTypes.Method && this._typeIdentityType.ContainsGenericParameters)
					{
						dictionary.Add("System.ComponentModel.Composition.GenericExportParametersOrderMetadataName", GenericServices.GetGenericParametersOrder(this._typeIdentityType));
					}
					this._metadata = dictionary;
				}
				return this._metadata;
			}
		}

		// Token: 0x040002E5 RID: 741
		private readonly AttributedPartCreationInfo _partCreationInfo;

		// Token: 0x040002E6 RID: 742
		private readonly MemberInfo _member;

		// Token: 0x040002E7 RID: 743
		private readonly ExportAttribute _exportAttribute;

		// Token: 0x040002E8 RID: 744
		private readonly Type _typeIdentityType;

		// Token: 0x040002E9 RID: 745
		private IDictionary<string, object> _metadata;
	}
}

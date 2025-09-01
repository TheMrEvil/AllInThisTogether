using System;
using System.ComponentModel.Composition.Diagnostics;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.AttributedModel
{
	// Token: 0x02000102 RID: 258
	internal static class AttributedModelDiscovery
	{
		// Token: 0x060006C4 RID: 1732 RVA: 0x00014E14 File Offset: 0x00013014
		public static ComposablePartDefinition CreatePartDefinitionIfDiscoverable(Type type, ICompositionElement origin)
		{
			AttributedPartCreationInfo attributedPartCreationInfo = new AttributedPartCreationInfo(type, null, false, origin);
			if (!attributedPartCreationInfo.IsPartDiscoverable())
			{
				return null;
			}
			return new ReflectionComposablePartDefinition(attributedPartCreationInfo);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00014E3B File Offset: 0x0001303B
		public static ReflectionComposablePartDefinition CreatePartDefinition(Type type, PartCreationPolicyAttribute partCreationPolicy, bool ignoreConstructorImports, ICompositionElement origin)
		{
			Assumes.NotNull<Type>(type);
			return new ReflectionComposablePartDefinition(new AttributedPartCreationInfo(type, partCreationPolicy, ignoreConstructorImports, origin));
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00014E51 File Offset: 0x00013051
		public static ReflectionComposablePart CreatePart(object attributedPart)
		{
			Assumes.NotNull<object>(attributedPart);
			return new ReflectionComposablePart(AttributedModelDiscovery.CreatePartDefinition(attributedPart.GetType(), PartCreationPolicyAttribute.Shared, true, null), attributedPart);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00014E74 File Offset: 0x00013074
		public static ReflectionComposablePart CreatePart(object attributedPart, ReflectionContext reflectionContext)
		{
			Assumes.NotNull<object>(attributedPart);
			Assumes.NotNull<ReflectionContext>(reflectionContext);
			TypeInfo typeInfo = reflectionContext.MapType(attributedPart.GetType().GetTypeInfo());
			if (typeInfo.Assembly.ReflectionOnly)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.Argument_ReflectionContextReturnsReflectionOnlyType, "reflectionContext"), "reflectionContext");
			}
			return AttributedModelDiscovery.CreatePart(AttributedModelDiscovery.CreatePartDefinition(typeInfo, PartCreationPolicyAttribute.Shared, true, null), attributedPart);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00014EDC File Offset: 0x000130DC
		public static ReflectionComposablePart CreatePart(ComposablePartDefinition partDefinition, object attributedPart)
		{
			Assumes.NotNull<ComposablePartDefinition>(partDefinition);
			Assumes.NotNull<object>(attributedPart);
			return new ReflectionComposablePart((ReflectionComposablePartDefinition)partDefinition, attributedPart);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00014EF8 File Offset: 0x000130F8
		public static ReflectionParameterImportDefinition CreateParameterImportDefinition(ParameterInfo parameter, ICompositionElement origin)
		{
			Requires.NotNull<ParameterInfo>(parameter, "parameter");
			ReflectionParameter reflectionParameter = parameter.ToReflectionParameter();
			IAttributedImport attributedImport = AttributedModelDiscovery.GetAttributedImport(reflectionParameter, parameter);
			ImportType importType = new ImportType(reflectionParameter.ReturnType, attributedImport.Cardinality);
			if (importType.IsPartCreator)
			{
				return new PartCreatorParameterImportDefinition(new Lazy<ParameterInfo>(() => parameter), origin, new ContractBasedImportDefinition(attributedImport.GetContractNameFromImport(importType), attributedImport.GetTypeIdentityFromImport(importType), CompositionServices.GetRequiredMetadata(importType.MetadataViewType), attributedImport.Cardinality, false, true, (attributedImport.RequiredCreationPolicy != CreationPolicy.NewScope) ? CreationPolicy.NonShared : CreationPolicy.NewScope, CompositionServices.GetImportMetadata(importType, attributedImport)));
			}
			if (attributedImport.RequiredCreationPolicy == CreationPolicy.NewScope)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidPartCreationPolicyOnImport, attributedImport.RequiredCreationPolicy), origin);
			}
			return new ReflectionParameterImportDefinition(new Lazy<ParameterInfo>(() => parameter), attributedImport.GetContractNameFromImport(importType), attributedImport.GetTypeIdentityFromImport(importType), CompositionServices.GetRequiredMetadata(importType.MetadataViewType), attributedImport.Cardinality, attributedImport.RequiredCreationPolicy, CompositionServices.GetImportMetadata(importType, attributedImport), origin);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00015014 File Offset: 0x00013214
		public static ReflectionMemberImportDefinition CreateMemberImportDefinition(MemberInfo member, ICompositionElement origin)
		{
			Requires.NotNull<MemberInfo>(member, "member");
			ReflectionWritableMember reflectionWritableMember = member.ToReflectionWritableMember();
			IAttributedImport attributedImport = AttributedModelDiscovery.GetAttributedImport(reflectionWritableMember, member);
			ImportType importType = new ImportType(reflectionWritableMember.ReturnType, attributedImport.Cardinality);
			if (importType.IsPartCreator)
			{
				return new PartCreatorMemberImportDefinition(new LazyMemberInfo(member), origin, new ContractBasedImportDefinition(attributedImport.GetContractNameFromImport(importType), attributedImport.GetTypeIdentityFromImport(importType), CompositionServices.GetRequiredMetadata(importType.MetadataViewType), attributedImport.Cardinality, attributedImport.AllowRecomposition, false, (attributedImport.RequiredCreationPolicy != CreationPolicy.NewScope) ? CreationPolicy.NonShared : CreationPolicy.NewScope, CompositionServices.GetImportMetadata(importType, attributedImport)));
			}
			if (attributedImport.RequiredCreationPolicy == CreationPolicy.NewScope)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidPartCreationPolicyOnImport, attributedImport.RequiredCreationPolicy), origin);
			}
			bool isPrerequisite = member.GetAttributes<ExportAttribute>().Length != 0;
			return new ReflectionMemberImportDefinition(new LazyMemberInfo(member), attributedImport.GetContractNameFromImport(importType), attributedImport.GetTypeIdentityFromImport(importType), CompositionServices.GetRequiredMetadata(importType.MetadataViewType), attributedImport.Cardinality, attributedImport.AllowRecomposition, isPrerequisite, attributedImport.RequiredCreationPolicy, CompositionServices.GetImportMetadata(importType, attributedImport), origin);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00015114 File Offset: 0x00013314
		private static IAttributedImport GetAttributedImport(ReflectionItem item, ICustomAttributeProvider attributeProvider)
		{
			IAttributedImport[] attributes = attributeProvider.GetAttributes(false);
			if (attributes.Length == 0)
			{
				return new ImportAttribute();
			}
			if (attributes.Length > 1)
			{
				CompositionTrace.MemberMarkedWithMultipleImportAndImportMany(item);
			}
			return attributes[0];
		}

		// Token: 0x02000103 RID: 259
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x060006CC RID: 1740 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x060006CD RID: 1741 RVA: 0x00015142 File Offset: 0x00013342
			internal ParameterInfo <CreateParameterImportDefinition>b__0()
			{
				return this.parameter;
			}

			// Token: 0x060006CE RID: 1742 RVA: 0x00015142 File Offset: 0x00013342
			internal ParameterInfo <CreateParameterImportDefinition>b__1()
			{
				return this.parameter;
			}

			// Token: 0x040002EA RID: 746
			public ParameterInfo parameter;
		}
	}
}

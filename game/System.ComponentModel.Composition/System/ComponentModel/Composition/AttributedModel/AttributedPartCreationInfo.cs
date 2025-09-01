using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition.Diagnostics;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.AttributedModel
{
	// Token: 0x02000104 RID: 260
	internal class AttributedPartCreationInfo : IReflectionPartCreationInfo, ICompositionElement
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x0001514A File Offset: 0x0001334A
		public AttributedPartCreationInfo(Type type, PartCreationPolicyAttribute partCreationPolicy, bool ignoreConstructorImports, ICompositionElement origin)
		{
			Assumes.NotNull<Type>(type);
			this._type = type;
			this._ignoreConstructorImports = ignoreConstructorImports;
			this._partCreationPolicy = partCreationPolicy;
			this._origin = origin;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00015175 File Offset: 0x00013375
		public Type GetPartType()
		{
			return this._type;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001517D File Offset: 0x0001337D
		public Lazy<Type> GetLazyPartType()
		{
			return new Lazy<Type>(new Func<Type>(this.GetPartType), LazyThreadSafetyMode.PublicationOnly);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00015192 File Offset: 0x00013392
		public ConstructorInfo GetConstructor()
		{
			if (this._constructor == null && !this._ignoreConstructorImports)
			{
				this._constructor = AttributedPartCreationInfo.SelectPartConstructor(this._type);
			}
			return this._constructor;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000151C1 File Offset: 0x000133C1
		public IDictionary<string, object> GetMetadata()
		{
			return this._type.GetPartMetadataForType(this.CreationPolicy);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000151D4 File Offset: 0x000133D4
		public IEnumerable<ExportDefinition> GetExports()
		{
			this.DiscoverExportsAndImports();
			return this._exports;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000151E2 File Offset: 0x000133E2
		public IEnumerable<ImportDefinition> GetImports()
		{
			this.DiscoverExportsAndImports();
			return this._imports;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x000151F0 File Offset: 0x000133F0
		public bool IsDisposalRequired
		{
			get
			{
				return typeof(IDisposable).IsAssignableFrom(this.GetPartType());
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00015207 File Offset: 0x00013407
		public bool IsPartDiscoverable()
		{
			if (this._type.IsAttributeDefined<PartNotDiscoverableAttribute>())
			{
				CompositionTrace.DefinitionMarkedWithPartNotDiscoverableAttribute(this._type);
				return false;
			}
			if (!this.HasExports())
			{
				CompositionTrace.DefinitionContainsNoExports(this._type);
				return false;
			}
			return this.AllExportsHaveMatchingArity();
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00015243 File Offset: 0x00013443
		private bool HasExports()
		{
			return this.GetExportMembers(this._type).Any<MemberInfo>() || this.GetInheritedExports(this._type).Any<Type>();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001526C File Offset: 0x0001346C
		private bool AllExportsHaveMatchingArity()
		{
			bool result = true;
			if (this._type.ContainsGenericParameters)
			{
				int pureGenericArity = this._type.GetPureGenericArity();
				foreach (MemberInfo memberInfo in this.GetExportMembers(this._type).Concat(this.GetInheritedExports(this._type)))
				{
					if (memberInfo.MemberType == MemberTypes.Method && ((MethodInfo)memberInfo).ContainsGenericParameters)
					{
						result = false;
						CompositionTrace.DefinitionMismatchedExportArity(this._type, memberInfo);
					}
					else if (memberInfo.GetDefaultTypeFromMember().GetPureGenericArity() != pureGenericArity)
					{
						result = false;
						CompositionTrace.DefinitionMismatchedExportArity(this._type, memberInfo);
					}
				}
			}
			return result;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001532C File Offset: 0x0001352C
		string ICompositionElement.DisplayName
		{
			get
			{
				return this.GetDisplayName();
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00015334 File Offset: 0x00013534
		ICompositionElement ICompositionElement.Origin
		{
			get
			{
				return this._origin;
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001532C File Offset: 0x0001352C
		public override string ToString()
		{
			return this.GetDisplayName();
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001533C File Offset: 0x0001353C
		private string GetDisplayName()
		{
			return this.GetPartType().GetDisplayName();
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001534C File Offset: 0x0001354C
		private CreationPolicy CreationPolicy
		{
			get
			{
				if (this._partCreationPolicy == null)
				{
					this._partCreationPolicy = (this._type.GetFirstAttribute<PartCreationPolicyAttribute>() ?? PartCreationPolicyAttribute.Default);
				}
				if (this._partCreationPolicy.CreationPolicy == CreationPolicy.NewScope)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidPartCreationPolicyOnPart, this._partCreationPolicy.CreationPolicy), this._origin);
				}
				return this._partCreationPolicy.CreationPolicy;
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000153C0 File Offset: 0x000135C0
		private static ConstructorInfo SelectPartConstructor(Type type)
		{
			Assumes.NotNull<Type>(type);
			if (type.IsAbstract)
			{
				return null;
			}
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			ConstructorInfo[] constructors = type.GetConstructors(bindingAttr);
			if (constructors.Length == 0)
			{
				return null;
			}
			if (constructors.Length == 1 && constructors[0].GetParameters().Length == 0)
			{
				return constructors[0];
			}
			ConstructorInfo constructorInfo = null;
			ConstructorInfo constructorInfo2 = null;
			foreach (ConstructorInfo constructorInfo3 in constructors)
			{
				if (constructorInfo3.IsAttributeDefined<ImportingConstructorAttribute>())
				{
					if (constructorInfo != null)
					{
						return null;
					}
					constructorInfo = constructorInfo3;
				}
				else if (constructorInfo2 == null && constructorInfo3.GetParameters().Length == 0)
				{
					constructorInfo2 = constructorInfo3;
				}
			}
			return constructorInfo ?? constructorInfo2;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00015459 File Offset: 0x00013659
		private void DiscoverExportsAndImports()
		{
			if (this._exports != null && this._imports != null)
			{
				return;
			}
			this._exports = this.GetExportDefinitions();
			this._imports = this.GetImportDefinitions();
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00015484 File Offset: 0x00013684
		private IEnumerable<ExportDefinition> GetExportDefinitions()
		{
			List<ExportDefinition> list = new List<ExportDefinition>();
			this._contractNamesOnNonInterfaces = new HashSet<string>();
			foreach (MemberInfo memberInfo in this.GetExportMembers(this._type))
			{
				foreach (ExportAttribute exportAttribute in memberInfo.GetAttributes<ExportAttribute>())
				{
					AttributedExportDefinition attributedExportDefinition = this.CreateExportDefinition(memberInfo, exportAttribute);
					if (exportAttribute.GetType() == CompositionServices.InheritedExportAttributeType)
					{
						if (!this._contractNamesOnNonInterfaces.Contains(attributedExportDefinition.ContractName))
						{
							list.Add(new ReflectionMemberExportDefinition(memberInfo.ToLazyMember(), attributedExportDefinition, this));
							this._contractNamesOnNonInterfaces.Add(attributedExportDefinition.ContractName);
						}
					}
					else
					{
						list.Add(new ReflectionMemberExportDefinition(memberInfo.ToLazyMember(), attributedExportDefinition, this));
					}
				}
			}
			foreach (Type type in this.GetInheritedExports(this._type))
			{
				foreach (InheritedExportAttribute exportAttribute2 in type.GetAttributes<InheritedExportAttribute>())
				{
					AttributedExportDefinition attributedExportDefinition2 = this.CreateExportDefinition(type, exportAttribute2);
					if (!this._contractNamesOnNonInterfaces.Contains(attributedExportDefinition2.ContractName))
					{
						list.Add(new ReflectionMemberExportDefinition(type.ToLazyMember(), attributedExportDefinition2, this));
						if (!type.IsInterface)
						{
							this._contractNamesOnNonInterfaces.Add(attributedExportDefinition2.ContractName);
						}
					}
				}
			}
			this._contractNamesOnNonInterfaces = null;
			return list;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00015638 File Offset: 0x00013838
		private AttributedExportDefinition CreateExportDefinition(MemberInfo member, ExportAttribute exportAttribute)
		{
			string contractName = null;
			Type typeIdentityType = null;
			member.GetContractInfoFromExport(exportAttribute, out typeIdentityType, out contractName);
			return new AttributedExportDefinition(this, member, exportAttribute, typeIdentityType, contractName);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001565E File Offset: 0x0001385E
		private IEnumerable<MemberInfo> GetExportMembers(Type type)
		{
			BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			if (type.IsAbstract)
			{
				flags &= ~BindingFlags.Instance;
			}
			else if (AttributedPartCreationInfo.IsExport(type))
			{
				yield return type;
			}
			foreach (FieldInfo fieldInfo in type.GetFields(flags))
			{
				if (AttributedPartCreationInfo.IsExport(fieldInfo))
				{
					yield return fieldInfo;
				}
			}
			FieldInfo[] array = null;
			foreach (PropertyInfo propertyInfo in type.GetProperties(flags))
			{
				if (AttributedPartCreationInfo.IsExport(propertyInfo))
				{
					yield return propertyInfo;
				}
			}
			PropertyInfo[] array2 = null;
			foreach (MethodInfo methodInfo in type.GetMethods(flags))
			{
				if (AttributedPartCreationInfo.IsExport(methodInfo))
				{
					yield return methodInfo;
				}
			}
			MethodInfo[] array3 = null;
			yield break;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001566E File Offset: 0x0001386E
		private IEnumerable<Type> GetInheritedExports(Type type)
		{
			if (type.IsAbstract)
			{
				yield break;
			}
			Type currentType = type.BaseType;
			if (currentType == null)
			{
				yield break;
			}
			while (currentType != null && currentType.UnderlyingSystemType != CompositionServices.ObjectType)
			{
				if (AttributedPartCreationInfo.IsInheritedExport(currentType))
				{
					yield return currentType;
				}
				currentType = currentType.BaseType;
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				if (AttributedPartCreationInfo.IsInheritedExport(type2))
				{
					yield return type2;
				}
			}
			Type[] array = null;
			yield break;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001567E File Offset: 0x0001387E
		private static bool IsExport(ICustomAttributeProvider attributeProvider)
		{
			return attributeProvider.IsAttributeDefined(false);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00015687 File Offset: 0x00013887
		private static bool IsInheritedExport(ICustomAttributeProvider attributedProvider)
		{
			return attributedProvider.IsAttributeDefined(false);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00015690 File Offset: 0x00013890
		private IEnumerable<ImportDefinition> GetImportDefinitions()
		{
			List<ImportDefinition> list = new List<ImportDefinition>();
			foreach (MemberInfo member in this.GetImportMembers(this._type))
			{
				ReflectionMemberImportDefinition item = AttributedModelDiscovery.CreateMemberImportDefinition(member, this);
				list.Add(item);
			}
			ConstructorInfo constructor = this.GetConstructor();
			if (constructor != null)
			{
				ParameterInfo[] parameters = constructor.GetParameters();
				for (int i = 0; i < parameters.Length; i++)
				{
					ReflectionParameterImportDefinition item2 = AttributedModelDiscovery.CreateParameterImportDefinition(parameters[i], this);
					list.Add(item2);
				}
			}
			return list;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00015730 File Offset: 0x00013930
		private IEnumerable<MemberInfo> GetImportMembers(Type type)
		{
			if (type.IsAbstract)
			{
				yield break;
			}
			foreach (MemberInfo memberInfo in this.GetDeclaredOnlyImportMembers(type))
			{
				yield return memberInfo;
			}
			IEnumerator<MemberInfo> enumerator = null;
			if (type.BaseType != null)
			{
				Type baseType = type.BaseType;
				while (baseType != null && baseType.UnderlyingSystemType != CompositionServices.ObjectType)
				{
					foreach (MemberInfo memberInfo2 in this.GetDeclaredOnlyImportMembers(baseType))
					{
						yield return memberInfo2;
					}
					enumerator = null;
					baseType = baseType.BaseType;
				}
				baseType = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00015747 File Offset: 0x00013947
		private IEnumerable<MemberInfo> GetDeclaredOnlyImportMembers(Type type)
		{
			BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			foreach (FieldInfo fieldInfo in type.GetFields(flags))
			{
				if (AttributedPartCreationInfo.IsImport(fieldInfo))
				{
					yield return fieldInfo;
				}
			}
			FieldInfo[] array = null;
			foreach (PropertyInfo propertyInfo in type.GetProperties(flags))
			{
				if (AttributedPartCreationInfo.IsImport(propertyInfo))
				{
					yield return propertyInfo;
				}
			}
			PropertyInfo[] array2 = null;
			yield break;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00015757 File Offset: 0x00013957
		private static bool IsImport(ICustomAttributeProvider attributeProvider)
		{
			return attributeProvider.IsAttributeDefined(false);
		}

		// Token: 0x040002EB RID: 747
		private readonly Type _type;

		// Token: 0x040002EC RID: 748
		private readonly bool _ignoreConstructorImports;

		// Token: 0x040002ED RID: 749
		private readonly ICompositionElement _origin;

		// Token: 0x040002EE RID: 750
		private PartCreationPolicyAttribute _partCreationPolicy;

		// Token: 0x040002EF RID: 751
		private ConstructorInfo _constructor;

		// Token: 0x040002F0 RID: 752
		private IEnumerable<ExportDefinition> _exports;

		// Token: 0x040002F1 RID: 753
		private IEnumerable<ImportDefinition> _imports;

		// Token: 0x040002F2 RID: 754
		private HashSet<string> _contractNamesOnNonInterfaces;

		// Token: 0x02000105 RID: 261
		[CompilerGenerated]
		private sealed class <GetExportMembers>d__32 : IEnumerable<MemberInfo>, IEnumerable, IEnumerator<MemberInfo>, IDisposable, IEnumerator
		{
			// Token: 0x060006EB RID: 1771 RVA: 0x00015760 File Offset: 0x00013960
			[DebuggerHidden]
			public <GetExportMembers>d__32(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060006EC RID: 1772 RVA: 0x000028FF File Offset: 0x00000AFF
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x0001577C File Offset: 0x0001397C
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					flags = (BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					if (type.IsAbstract)
					{
						flags &= ~BindingFlags.Instance;
					}
					else if (AttributedPartCreationInfo.IsExport(type))
					{
						this.<>2__current = type;
						this.<>1__state = 1;
						return true;
					}
					break;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_C6;
				case 3:
					this.<>1__state = -1;
					goto IL_138;
				case 4:
					this.<>1__state = -1;
					goto IL_1AA;
				default:
					return false;
				}
				array = type.GetFields(flags);
				i = 0;
				goto IL_D4;
				IL_C6:
				i++;
				IL_D4:
				if (i >= array.Length)
				{
					array = null;
					array2 = type.GetProperties(flags);
					i = 0;
					goto IL_146;
				}
				FieldInfo attributeProvider = array[i];
				if (AttributedPartCreationInfo.IsExport(attributeProvider))
				{
					this.<>2__current = attributeProvider;
					this.<>1__state = 2;
					return true;
				}
				goto IL_C6;
				IL_138:
				i++;
				IL_146:
				if (i >= array2.Length)
				{
					array2 = null;
					array3 = type.GetMethods(flags);
					i = 0;
					goto IL_1B8;
				}
				PropertyInfo attributeProvider2 = array2[i];
				if (AttributedPartCreationInfo.IsExport(attributeProvider2))
				{
					this.<>2__current = attributeProvider2;
					this.<>1__state = 3;
					return true;
				}
				goto IL_138;
				IL_1AA:
				i++;
				IL_1B8:
				if (i >= array3.Length)
				{
					array3 = null;
					return false;
				}
				MethodInfo attributeProvider3 = array3[i];
				if (AttributedPartCreationInfo.IsExport(attributeProvider3))
				{
					this.<>2__current = attributeProvider3;
					this.<>1__state = 4;
					return true;
				}
				goto IL_1AA;
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x060006EE RID: 1774 RVA: 0x00015959 File Offset: 0x00013B59
			MemberInfo IEnumerator<MemberInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060006EF RID: 1775 RVA: 0x00002C6B File Offset: 0x00000E6B
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00015959 File Offset: 0x00013B59
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060006F1 RID: 1777 RVA: 0x00015964 File Offset: 0x00013B64
			[DebuggerHidden]
			IEnumerator<MemberInfo> IEnumerable<MemberInfo>.GetEnumerator()
			{
				AttributedPartCreationInfo.<GetExportMembers>d__32 <GetExportMembers>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetExportMembers>d__ = this;
				}
				else
				{
					<GetExportMembers>d__ = new AttributedPartCreationInfo.<GetExportMembers>d__32(0);
				}
				<GetExportMembers>d__.type = type;
				return <GetExportMembers>d__;
			}

			// Token: 0x060006F2 RID: 1778 RVA: 0x000159A7 File Offset: 0x00013BA7
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo>.GetEnumerator();
			}

			// Token: 0x040002F3 RID: 755
			private int <>1__state;

			// Token: 0x040002F4 RID: 756
			private MemberInfo <>2__current;

			// Token: 0x040002F5 RID: 757
			private int <>l__initialThreadId;

			// Token: 0x040002F6 RID: 758
			private Type type;

			// Token: 0x040002F7 RID: 759
			public Type <>3__type;

			// Token: 0x040002F8 RID: 760
			private BindingFlags <flags>5__2;

			// Token: 0x040002F9 RID: 761
			private FieldInfo[] <>7__wrap2;

			// Token: 0x040002FA RID: 762
			private int <>7__wrap3;

			// Token: 0x040002FB RID: 763
			private PropertyInfo[] <>7__wrap4;

			// Token: 0x040002FC RID: 764
			private MethodInfo[] <>7__wrap5;
		}

		// Token: 0x02000106 RID: 262
		[CompilerGenerated]
		private sealed class <GetInheritedExports>d__33 : IEnumerable<Type>, IEnumerable, IEnumerator<Type>, IDisposable, IEnumerator
		{
			// Token: 0x060006F3 RID: 1779 RVA: 0x000159AF File Offset: 0x00013BAF
			[DebuggerHidden]
			public <GetInheritedExports>d__33(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060006F4 RID: 1780 RVA: 0x000028FF File Offset: 0x00000AFF
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060006F5 RID: 1781 RVA: 0x000159CC File Offset: 0x00013BCC
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					if (type.IsAbstract)
					{
						return false;
					}
					currentType = type.BaseType;
					if (currentType == null)
					{
						return false;
					}
					goto IL_8C;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_F8;
				default:
					return false;
				}
				IL_7B:
				currentType = currentType.BaseType;
				IL_8C:
				if (!(currentType != null) || !(currentType.UnderlyingSystemType != CompositionServices.ObjectType))
				{
					array = type.GetInterfaces();
					i = 0;
					goto IL_106;
				}
				if (AttributedPartCreationInfo.IsInheritedExport(currentType))
				{
					this.<>2__current = currentType;
					this.<>1__state = 1;
					return true;
				}
				goto IL_7B;
				IL_F8:
				i++;
				IL_106:
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				Type attributedProvider = array[i];
				if (AttributedPartCreationInfo.IsInheritedExport(attributedProvider))
				{
					this.<>2__current = attributedProvider;
					this.<>1__state = 2;
					return true;
				}
				goto IL_F8;
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00015AF7 File Offset: 0x00013CF7
			Type IEnumerator<Type>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060006F7 RID: 1783 RVA: 0x00002C6B File Offset: 0x00000E6B
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00015AF7 File Offset: 0x00013CF7
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060006F9 RID: 1785 RVA: 0x00015B00 File Offset: 0x00013D00
			[DebuggerHidden]
			IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
			{
				AttributedPartCreationInfo.<GetInheritedExports>d__33 <GetInheritedExports>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetInheritedExports>d__ = this;
				}
				else
				{
					<GetInheritedExports>d__ = new AttributedPartCreationInfo.<GetInheritedExports>d__33(0);
				}
				<GetInheritedExports>d__.type = type;
				return <GetInheritedExports>d__;
			}

			// Token: 0x060006FA RID: 1786 RVA: 0x00015B43 File Offset: 0x00013D43
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Type>.GetEnumerator();
			}

			// Token: 0x040002FD RID: 765
			private int <>1__state;

			// Token: 0x040002FE RID: 766
			private Type <>2__current;

			// Token: 0x040002FF RID: 767
			private int <>l__initialThreadId;

			// Token: 0x04000300 RID: 768
			private Type type;

			// Token: 0x04000301 RID: 769
			public Type <>3__type;

			// Token: 0x04000302 RID: 770
			private Type <currentType>5__2;

			// Token: 0x04000303 RID: 771
			private Type[] <>7__wrap2;

			// Token: 0x04000304 RID: 772
			private int <>7__wrap3;
		}

		// Token: 0x02000107 RID: 263
		[CompilerGenerated]
		private sealed class <GetImportMembers>d__37 : IEnumerable<MemberInfo>, IEnumerable, IEnumerator<MemberInfo>, IDisposable, IEnumerator
		{
			// Token: 0x060006FB RID: 1787 RVA: 0x00015B4B File Offset: 0x00013D4B
			[DebuggerHidden]
			public <GetImportMembers>d__37(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060006FC RID: 1788 RVA: 0x00015B68 File Offset: 0x00013D68
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				switch (this.<>1__state)
				{
				case -4:
				case 2:
					break;
				case -3:
				case 1:
					try
					{
						return;
					}
					finally
					{
						this.<>m__Finally1();
					}
					break;
				case -2:
				case -1:
				case 0:
					return;
				default:
					return;
				}
				try
				{
				}
				finally
				{
					this.<>m__Finally2();
				}
			}

			// Token: 0x060006FD RID: 1789 RVA: 0x00015BD4 File Offset: 0x00013DD4
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					AttributedPartCreationInfo attributedPartCreationInfo = this;
					switch (num)
					{
					case 0:
						this.<>1__state = -1;
						if (type.IsAbstract)
						{
							return false;
						}
						enumerator = attributedPartCreationInfo.GetDeclaredOnlyImportMembers(type).GetEnumerator();
						this.<>1__state = -3;
						break;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -4;
						goto IL_118;
					default:
						return false;
					}
					if (enumerator.MoveNext())
					{
						MemberInfo memberInfo = enumerator.Current;
						this.<>2__current = memberInfo;
						this.<>1__state = 1;
						return true;
					}
					this.<>m__Finally1();
					enumerator = null;
					if (type.BaseType != null)
					{
						baseType = type.BaseType;
						goto IL_143;
					}
					goto IL_172;
					IL_118:
					if (enumerator.MoveNext())
					{
						MemberInfo memberInfo2 = enumerator.Current;
						this.<>2__current = memberInfo2;
						this.<>1__state = 2;
						return true;
					}
					this.<>m__Finally2();
					enumerator = null;
					baseType = baseType.BaseType;
					IL_143:
					if (baseType != null && baseType.UnderlyingSystemType != CompositionServices.ObjectType)
					{
						enumerator = attributedPartCreationInfo.GetDeclaredOnlyImportMembers(baseType).GetEnumerator();
						this.<>1__state = -4;
						goto IL_118;
					}
					baseType = null;
					IL_172:
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060006FE RID: 1790 RVA: 0x00015D7C File Offset: 0x00013F7C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x060006FF RID: 1791 RVA: 0x00015D7C File Offset: 0x00013F7C
			private void <>m__Finally2()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000700 RID: 1792 RVA: 0x00015D98 File Offset: 0x00013F98
			MemberInfo IEnumerator<MemberInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000701 RID: 1793 RVA: 0x00002C6B File Offset: 0x00000E6B
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x06000702 RID: 1794 RVA: 0x00015D98 File Offset: 0x00013F98
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000703 RID: 1795 RVA: 0x00015DA0 File Offset: 0x00013FA0
			[DebuggerHidden]
			IEnumerator<MemberInfo> IEnumerable<MemberInfo>.GetEnumerator()
			{
				AttributedPartCreationInfo.<GetImportMembers>d__37 <GetImportMembers>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetImportMembers>d__ = this;
				}
				else
				{
					<GetImportMembers>d__ = new AttributedPartCreationInfo.<GetImportMembers>d__37(0);
					<GetImportMembers>d__.<>4__this = this;
				}
				<GetImportMembers>d__.type = type;
				return <GetImportMembers>d__;
			}

			// Token: 0x06000704 RID: 1796 RVA: 0x00015DEF File Offset: 0x00013FEF
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo>.GetEnumerator();
			}

			// Token: 0x04000305 RID: 773
			private int <>1__state;

			// Token: 0x04000306 RID: 774
			private MemberInfo <>2__current;

			// Token: 0x04000307 RID: 775
			private int <>l__initialThreadId;

			// Token: 0x04000308 RID: 776
			private Type type;

			// Token: 0x04000309 RID: 777
			public Type <>3__type;

			// Token: 0x0400030A RID: 778
			public AttributedPartCreationInfo <>4__this;

			// Token: 0x0400030B RID: 779
			private IEnumerator<MemberInfo> <>7__wrap1;

			// Token: 0x0400030C RID: 780
			private Type <baseType>5__3;
		}

		// Token: 0x02000108 RID: 264
		[CompilerGenerated]
		private sealed class <GetDeclaredOnlyImportMembers>d__38 : IEnumerable<MemberInfo>, IEnumerable, IEnumerator<MemberInfo>, IDisposable, IEnumerator
		{
			// Token: 0x06000705 RID: 1797 RVA: 0x00015DF7 File Offset: 0x00013FF7
			[DebuggerHidden]
			public <GetDeclaredOnlyImportMembers>d__38(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000706 RID: 1798 RVA: 0x000028FF File Offset: 0x00000AFF
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000707 RID: 1799 RVA: 0x00015E14 File Offset: 0x00014014
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					flags = (BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					array = type.GetFields(flags);
					i = 0;
					goto IL_85;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_E9;
				default:
					return false;
				}
				IL_77:
				i++;
				IL_85:
				if (i >= array.Length)
				{
					array = null;
					array2 = type.GetProperties(flags);
					i = 0;
					goto IL_F7;
				}
				FieldInfo attributeProvider = array[i];
				if (AttributedPartCreationInfo.IsImport(attributeProvider))
				{
					this.<>2__current = attributeProvider;
					this.<>1__state = 1;
					return true;
				}
				goto IL_77;
				IL_E9:
				i++;
				IL_F7:
				if (i >= array2.Length)
				{
					array2 = null;
					return false;
				}
				PropertyInfo attributeProvider2 = array2[i];
				if (AttributedPartCreationInfo.IsImport(attributeProvider2))
				{
					this.<>2__current = attributeProvider2;
					this.<>1__state = 2;
					return true;
				}
				goto IL_E9;
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x06000708 RID: 1800 RVA: 0x00015F30 File Offset: 0x00014130
			MemberInfo IEnumerator<MemberInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000709 RID: 1801 RVA: 0x00002C6B File Offset: 0x00000E6B
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x0600070A RID: 1802 RVA: 0x00015F30 File Offset: 0x00014130
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600070B RID: 1803 RVA: 0x00015F38 File Offset: 0x00014138
			[DebuggerHidden]
			IEnumerator<MemberInfo> IEnumerable<MemberInfo>.GetEnumerator()
			{
				AttributedPartCreationInfo.<GetDeclaredOnlyImportMembers>d__38 <GetDeclaredOnlyImportMembers>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetDeclaredOnlyImportMembers>d__ = this;
				}
				else
				{
					<GetDeclaredOnlyImportMembers>d__ = new AttributedPartCreationInfo.<GetDeclaredOnlyImportMembers>d__38(0);
				}
				<GetDeclaredOnlyImportMembers>d__.type = type;
				return <GetDeclaredOnlyImportMembers>d__;
			}

			// Token: 0x0600070C RID: 1804 RVA: 0x00015F7B File Offset: 0x0001417B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo>.GetEnumerator();
			}

			// Token: 0x0400030D RID: 781
			private int <>1__state;

			// Token: 0x0400030E RID: 782
			private MemberInfo <>2__current;

			// Token: 0x0400030F RID: 783
			private int <>l__initialThreadId;

			// Token: 0x04000310 RID: 784
			private Type type;

			// Token: 0x04000311 RID: 785
			public Type <>3__type;

			// Token: 0x04000312 RID: 786
			private BindingFlags <flags>5__2;

			// Token: 0x04000313 RID: 787
			private FieldInfo[] <>7__wrap2;

			// Token: 0x04000314 RID: 788
			private int <>7__wrap3;

			// Token: 0x04000315 RID: 789
			private PropertyInfo[] <>7__wrap4;
		}
	}
}

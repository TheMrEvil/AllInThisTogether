using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x02000181 RID: 385
	internal class ImportedTypeDefinition : ImportedDefinition, ITypeDefinition, IMemberDefinition
	{
		// Token: 0x06001240 RID: 4672 RVA: 0x0004C7D9 File Offset: 0x0004A9D9
		public ImportedTypeDefinition(Type type, MetadataImporter importer) : base(type, importer)
		{
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x0004C7E3 File Offset: 0x0004A9E3
		public IAssemblyDefinition DeclaringAssembly
		{
			get
			{
				return this.importer.GetAssemblyDefinition(this.provider.Module.Assembly);
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0004C800 File Offset: 0x0004AA00
		bool ITypeDefinition.IsComImport
		{
			get
			{
				return ((Type)this.provider).IsImport;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsPartial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsCyclicTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0004C814 File Offset: 0x0004AA14
		public override string Name
		{
			get
			{
				if (this.name == null)
				{
					this.name = base.Name;
					if (this.tparams != null)
					{
						int num = this.name.IndexOf('`');
						if (num > 0)
						{
							this.name = this.name.Substring(0, num);
						}
					}
				}
				return this.name;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x0004C868 File Offset: 0x0004AA68
		public string Namespace
		{
			get
			{
				return ((Type)this.provider).Namespace;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x0004C87A File Offset: 0x0004AA7A
		public int TypeParametersCount
		{
			get
			{
				if (this.tparams != null)
				{
					return this.tparams.Length;
				}
				return 0;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x0004C88E File Offset: 0x0004AA8E
		// (set) Token: 0x0600124A RID: 4682 RVA: 0x0004C896 File Offset: 0x0004AA96
		public TypeParameterSpec[] TypeParameters
		{
			get
			{
				return this.tparams;
			}
			set
			{
				this.tparams = value;
			}
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0004C8A0 File Offset: 0x0004AAA0
		public void DefineInterfaces(TypeSpec spec)
		{
			Type[] interfaces = ((Type)this.provider).GetInterfaces();
			if (interfaces.Length != 0)
			{
				foreach (Type type in interfaces)
				{
					spec.AddInterface(this.importer.CreateType(type));
				}
			}
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0004C8EC File Offset: 0x0004AAEC
		public static void Error_MissingDependency(IMemberContext ctx, List<MissingTypeSpecReference> missing, Location loc)
		{
			Report report = ctx.Module.Compiler.Report;
			for (int i = 0; i < missing.Count; i++)
			{
				TypeSpec type = missing[i].Type;
				if (!report.Printer.MissingTypeReported(type.MemberDefinition))
				{
					string signatureForError = type.GetSignatureForError();
					MemberSpec caller = missing[i].Caller;
					if (caller.Kind != MemberKind.MissingType)
					{
						report.SymbolRelatedToPreviousError(caller);
					}
					ITypeDefinition memberDefinition = type.MemberDefinition;
					if (memberDefinition.DeclaringAssembly == ctx.Module.DeclaringAssembly)
					{
						report.Error(1683, loc, "Reference to type `{0}' claims it is defined in this assembly, but it is not defined in source or any added modules", signatureForError);
					}
					else if (memberDefinition.DeclaringAssembly.IsMissing)
					{
						if (memberDefinition.IsTypeForwarder)
						{
							report.Error(1070, loc, "The type `{0}' has been forwarded to an assembly that is not referenced. Consider adding a reference to assembly `{1}'", signatureForError, memberDefinition.DeclaringAssembly.FullName);
						}
						else
						{
							report.Error(12, loc, "The type `{0}' is defined in an assembly that is not referenced. Consider adding a reference to assembly `{1}'", signatureForError, memberDefinition.DeclaringAssembly.FullName);
						}
					}
					else if (memberDefinition.IsTypeForwarder)
					{
						report.Error(731, loc, "The type forwarder for type `{0}' in assembly `{1}' has circular dependency", signatureForError, memberDefinition.DeclaringAssembly.FullName);
					}
					else
					{
						report.Error(7069, loc, "Reference to type `{0}' claims it is defined assembly `{1}', but it could not be found", signatureForError, type.MemberDefinition.DeclaringAssembly.FullName);
					}
				}
			}
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0004CA41 File Offset: 0x0004AC41
		public TypeSpec GetAttributeCoClass()
		{
			if (this.cattrs == null)
			{
				base.ReadAttributes();
			}
			return this.cattrs.CoClass;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0004CA5C File Offset: 0x0004AC5C
		public string GetAttributeDefaultMember()
		{
			if (this.cattrs == null)
			{
				base.ReadAttributes();
			}
			return this.cattrs.DefaultIndexerName;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004CA77 File Offset: 0x0004AC77
		public AttributeUsageAttribute GetAttributeUsage(PredefinedAttribute pa)
		{
			if (this.cattrs == null)
			{
				base.ReadAttributes();
			}
			return this.cattrs.AttributeUsage;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0004CA94 File Offset: 0x0004AC94
		bool ITypeDefinition.IsInternalAsPublic(IAssemblyDefinition assembly)
		{
			IAssemblyDefinition assemblyDefinition = this.importer.GetAssemblyDefinition(this.provider.Module.Assembly);
			return assemblyDefinition == assembly || assemblyDefinition.IsFriendAssemblyTo(assembly);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0004CACC File Offset: 0x0004ACCC
		public void LoadMembers(TypeSpec declaringType, bool onlyTypes, ref MemberCache cache)
		{
			if (declaringType.IsPrivate && this.importer.IgnorePrivateMembers)
			{
				cache = MemberCache.Empty;
				return;
			}
			Type type = (Type)this.provider;
			Dictionary<MethodBase, MethodSpec> dictionary = null;
			List<EventSpec> list = null;
			MemberInfo[] members;
			try
			{
				members = type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
			catch (Exception exception)
			{
				throw new InternalErrorException(exception, "Could not import type `{0}' from `{1}'", new object[]
				{
					declaringType.GetSignatureForError(),
					declaringType.MemberDefinition.DeclaringAssembly.FullName
				});
			}
			if (cache == null)
			{
				cache = new MemberCache(members.Length);
				foreach (MemberInfo memberInfo in members)
				{
					if (memberInfo.MemberType == MemberTypes.NestedType)
					{
						Type type2 = (Type)memberInfo;
						if ((type2.Attributes & TypeAttributes.VisibilityMask) != TypeAttributes.NestedPrivate || !this.importer.IgnorePrivateMembers)
						{
							MemberSpec memberSpec;
							try
							{
								memberSpec = this.importer.CreateNestedType(type2, declaringType);
							}
							catch (Exception exception2)
							{
								throw new InternalErrorException(exception2, "Could not import nested type `{0}' from `{1}'", new object[]
								{
									type2.FullName,
									declaringType.MemberDefinition.DeclaringAssembly.FullName
								});
							}
							cache.AddMemberImported(memberSpec);
						}
					}
				}
				foreach (MemberInfo memberInfo2 in members)
				{
					if (memberInfo2.MemberType == MemberTypes.NestedType)
					{
						Type type3 = (Type)memberInfo2;
						if ((type3.Attributes & TypeAttributes.VisibilityMask) != TypeAttributes.NestedPrivate || !this.importer.IgnorePrivateMembers)
						{
							this.importer.ImportTypeBase(type3);
						}
					}
				}
			}
			if (declaringType.IsInterface && declaringType.Interfaces != null)
			{
				foreach (TypeSpec iface in declaringType.Interfaces)
				{
					cache.AddInterface(iface);
				}
			}
			if (!onlyTypes)
			{
				MemberInfo[] array = members;
				int i = 0;
				while (i < array.Length)
				{
					MemberInfo memberInfo3 = array[i];
					MemberTypes memberType = memberInfo3.MemberType;
					MemberSpec memberSpec;
					if (memberType <= MemberTypes.Method)
					{
						switch (memberType)
						{
						case MemberTypes.Constructor:
							if (declaringType.IsInterface)
							{
								goto IL_493;
							}
							break;
						case MemberTypes.Event:
						{
							if (dictionary == null)
							{
								goto IL_493;
							}
							EventInfo eventInfo = (EventInfo)memberInfo3;
							MethodInfo methodInfo = eventInfo.GetAddMethod(true);
							MethodSpec methodSpec;
							if (methodInfo == null || !dictionary.TryGetValue(methodInfo, out methodSpec))
							{
								methodSpec = null;
							}
							methodInfo = eventInfo.GetRemoveMethod(true);
							MethodSpec methodSpec2;
							if (methodInfo == null || !dictionary.TryGetValue(methodInfo, out methodSpec2))
							{
								methodSpec2 = null;
							}
							if (methodSpec != null && methodSpec2 != null)
							{
								EventSpec eventSpec = this.importer.CreateEvent(eventInfo, declaringType, methodSpec, methodSpec2);
								if (!this.importer.IgnorePrivateMembers)
								{
									if (list == null)
									{
										list = new List<EventSpec>();
									}
									list.Add(eventSpec);
								}
								memberSpec = eventSpec;
								goto IL_479;
							}
							goto IL_493;
						}
						case MemberTypes.Constructor | MemberTypes.Event:
							goto IL_46C;
						case MemberTypes.Field:
						{
							FieldInfo fieldInfo = (FieldInfo)memberInfo3;
							memberSpec = this.importer.CreateField(fieldInfo, declaringType);
							if (memberSpec == null)
							{
								goto IL_493;
							}
							if (list == null)
							{
								goto IL_479;
							}
							int j;
							for (j = 0; j < list.Count; j++)
							{
								EventSpec eventSpec2 = list[j];
								if (eventSpec2.Name == fieldInfo.Name)
								{
									eventSpec2.BackingField = (FieldSpec)memberSpec;
									list.RemoveAt(j);
									j = -1;
									break;
								}
							}
							if (j < 0)
							{
								goto IL_493;
							}
							goto IL_479;
						}
						default:
							if (memberType != MemberTypes.Method)
							{
								goto IL_46C;
							}
							break;
						}
						MethodBase methodBase = (MethodBase)memberInfo3;
						MethodAttributes attributes = methodBase.Attributes;
						if ((attributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Private || (!this.importer.IgnorePrivateMembers && (attributes & (MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask)) != (MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask) && !MetadataImporter.HasAttribute(CustomAttributeData.GetCustomAttributes(methodBase), "CompilerGeneratedAttribute", MetadataImporter.CompilerServicesNamespace)))
						{
							memberSpec = this.importer.CreateMethod(methodBase, declaringType);
							if (memberSpec.Kind == MemberKind.Method && !memberSpec.IsGeneric)
							{
								if (dictionary == null)
								{
									dictionary = new Dictionary<MethodBase, MethodSpec>(ReferenceEquality<MethodBase>.Default);
								}
								dictionary.Add(methodBase, (MethodSpec)memberSpec);
								goto IL_479;
							}
							goto IL_479;
						}
					}
					else if (memberType != MemberTypes.Property)
					{
						if (memberType != MemberTypes.NestedType)
						{
							goto IL_46C;
						}
					}
					else if (dictionary != null)
					{
						PropertyInfo propertyInfo = (PropertyInfo)memberInfo3;
						MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
						MethodSpec methodSpec3;
						if (methodInfo == null || !dictionary.TryGetValue(methodInfo, out methodSpec3))
						{
							methodSpec3 = null;
						}
						methodInfo = propertyInfo.GetSetMethod(true);
						MethodSpec methodSpec4;
						if (methodInfo == null || !dictionary.TryGetValue(methodInfo, out methodSpec4))
						{
							methodSpec4 = null;
						}
						if (methodSpec3 != null || methodSpec4 != null)
						{
							try
							{
								memberSpec = this.importer.CreateProperty(propertyInfo, declaringType, methodSpec3, methodSpec4);
							}
							catch (Exception exception3)
							{
								throw new InternalErrorException(exception3, "Could not import property `{0}' inside `{1}'", new object[]
								{
									propertyInfo.Name,
									declaringType.GetSignatureForError()
								});
							}
							if (memberSpec != null)
							{
								goto IL_479;
							}
						}
					}
					IL_493:
					i++;
					continue;
					IL_479:
					if (!memberSpec.IsStatic || !declaringType.IsInterface)
					{
						cache.AddMemberImported(memberSpec);
						goto IL_493;
					}
					goto IL_493;
					IL_46C:
					throw new NotImplementedException(memberInfo3.ToString());
				}
			}
		}

		// Token: 0x040007BC RID: 1980
		private TypeParameterSpec[] tparams;

		// Token: 0x040007BD RID: 1981
		private string name;
	}
}

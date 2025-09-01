using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000247 RID: 583
	public class MemberCache
	{
		// Token: 0x06001CD8 RID: 7384 RVA: 0x0008B1D0 File Offset: 0x000893D0
		public MemberCache() : this(16)
		{
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0008B1DA File Offset: 0x000893DA
		public MemberCache(int capacity)
		{
			this.member_hash = new Dictionary<string, IList<MemberSpec>>(capacity);
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0008B1EE File Offset: 0x000893EE
		public MemberCache(MemberCache cache) : this(cache.member_hash.Count)
		{
			this.state = cache.state;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0008B20D File Offset: 0x0008940D
		public MemberCache(TypeContainer container) : this()
		{
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0008B218 File Offset: 0x00089418
		public void AddBaseType(TypeSpec baseType)
		{
			foreach (KeyValuePair<string, IList<MemberSpec>> keyValuePair in baseType.MemberCache.member_hash)
			{
				IList<MemberSpec> list;
				if (!this.member_hash.TryGetValue(keyValuePair.Key, out list))
				{
					if (keyValuePair.Value.Count == 1)
					{
						list = keyValuePair.Value;
					}
					else
					{
						list = new List<MemberSpec>(keyValuePair.Value);
					}
					this.member_hash.Add(keyValuePair.Key, list);
				}
				else
				{
					foreach (MemberSpec item in keyValuePair.Value)
					{
						if (!list.Contains(item))
						{
							if (list is MemberSpec[])
							{
								list = new List<MemberSpec>
								{
									list[0]
								};
								this.member_hash[keyValuePair.Key] = list;
							}
							list.Add(item);
						}
					}
				}
			}
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0008B338 File Offset: 0x00089538
		public void AddInterface(TypeSpec iface)
		{
			foreach (KeyValuePair<string, IList<MemberSpec>> keyValuePair in iface.MemberCache.member_hash)
			{
				IList<MemberSpec> list;
				if (!this.member_hash.TryGetValue(keyValuePair.Key, out list))
				{
					if (keyValuePair.Value.Count == 1)
					{
						list = keyValuePair.Value;
					}
					else
					{
						list = new List<MemberSpec>(keyValuePair.Value);
					}
					this.member_hash.Add(keyValuePair.Key, list);
				}
				else
				{
					foreach (MemberSpec memberSpec in keyValuePair.Value)
					{
						if (!list.Contains(memberSpec) && MemberCache.AddInterfaceMember(memberSpec, ref list))
						{
							this.member_hash[keyValuePair.Key] = list;
						}
					}
				}
			}
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0008B440 File Offset: 0x00089640
		public void AddMember(InterfaceMemberBase imb, string exlicitName, MemberSpec ms)
		{
			if (imb.IsExplicitImpl)
			{
				this.AddMember(exlicitName, ms, false);
				return;
			}
			this.AddMember(ms);
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0008B45B File Offset: 0x0008965B
		public void AddMember(MemberSpec ms)
		{
			this.AddMember(MemberCache.GetLookupName(ms), ms, false);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0008B46C File Offset: 0x0008966C
		private void AddMember(string name, MemberSpec member, bool removeHiddenMembers)
		{
			if (member.Kind == MemberKind.Operator)
			{
				TypeSpec declaringType = member.DeclaringType;
				if (!BuiltinTypeSpec.IsPrimitiveType(declaringType) || declaringType.BuiltinType == BuiltinTypeSpec.Type.Char)
				{
					BuiltinTypeSpec.Type builtinType = declaringType.BuiltinType;
					if (builtinType != BuiltinTypeSpec.Type.String && builtinType != BuiltinTypeSpec.Type.Delegate && builtinType != BuiltinTypeSpec.Type.MulticastDelegate)
					{
						if (name == Operator.GetMetadataName(Operator.OpType.Implicit) || name == Operator.GetMetadataName(Operator.OpType.Explicit))
						{
							this.state |= MemberCache.StateFlags.HasConversionOperator;
						}
						else
						{
							this.state |= MemberCache.StateFlags.HasUserOperator;
						}
					}
				}
			}
			IList<MemberSpec> list;
			if (!this.member_hash.TryGetValue(name, out list))
			{
				this.member_hash.Add(name, new MemberSpec[]
				{
					member
				});
				return;
			}
			if (removeHiddenMembers && member.DeclaringType.IsInterface)
			{
				if (MemberCache.AddInterfaceMember(member, ref list))
				{
					this.member_hash[name] = list;
					return;
				}
			}
			else
			{
				if (list.Count == 1)
				{
					list = new List<MemberSpec>
					{
						list[0]
					};
					this.member_hash[name] = list;
				}
				list.Add(member);
			}
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0008B56C File Offset: 0x0008976C
		public void AddMemberImported(MemberSpec ms)
		{
			this.AddMember(MemberCache.GetLookupName(ms), ms, true);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0008B57C File Offset: 0x0008977C
		private static bool AddInterfaceMember(MemberSpec member, ref IList<MemberSpec> existing)
		{
			AParametersCollection aparametersCollection = (member is IParametersMember) ? ((IParametersMember)member).Parameters : null;
			for (int i = 0; i < existing.Count; i++)
			{
				MemberSpec memberSpec = existing[i];
				if (memberSpec.Arity == member.Arity)
				{
					AParametersCollection a = null;
					if (aparametersCollection != null)
					{
						IParametersMember parametersMember = memberSpec as IParametersMember;
						if (parametersMember != null)
						{
							a = parametersMember.Parameters;
							if (!TypeSpecComparer.Override.IsEqual(a, aparametersCollection))
							{
								goto IL_D1;
							}
						}
					}
					if (member.DeclaringType.ImplementsInterface(memberSpec.DeclaringType, false))
					{
						if (existing.Count == 1)
						{
							existing = new MemberSpec[]
							{
								member
							};
							return true;
						}
						existing.RemoveAt(i--);
					}
					else
					{
						if (memberSpec.DeclaringType == member.DeclaringType && memberSpec.IsAccessor == member.IsAccessor)
						{
							return false;
						}
						if (memberSpec.DeclaringType.ImplementsInterface(member.DeclaringType, false) && AParametersCollection.HasSameParameterDefaults(a, aparametersCollection))
						{
							return false;
						}
					}
				}
				IL_D1:;
			}
			if (existing.Count == 1)
			{
				existing = new List<MemberSpec>
				{
					existing[0],
					member
				};
				return true;
			}
			existing.Add(member);
			return false;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0008B69C File Offset: 0x0008989C
		public static MemberSpec FindMember(TypeSpec container, MemberFilter filter, BindingRestriction restrictions)
		{
			MemberSpec memberSpec;
			for (;;)
			{
				IList<MemberSpec> list;
				if (container.MemberCache.member_hash.TryGetValue(filter.Name, out list))
				{
					for (int i = list.Count - 1; i >= 0; i--)
					{
						memberSpec = list[i];
						if (((restrictions & BindingRestriction.InstanceOnly) == BindingRestriction.None || !memberSpec.IsStatic) && ((restrictions & BindingRestriction.NoAccessors) == BindingRestriction.None || !memberSpec.IsAccessor) && ((restrictions & BindingRestriction.OverrideOnly) == BindingRestriction.None || (memberSpec.Modifiers & Modifiers.OVERRIDE) != (Modifiers)0) && filter.Equals(memberSpec) && ((restrictions & BindingRestriction.DeclaredOnly) == BindingRestriction.None || !container.IsInterface || memberSpec.DeclaringType == container))
						{
							return memberSpec;
						}
					}
				}
				if ((restrictions & BindingRestriction.DeclaredOnly) != BindingRestriction.None)
				{
					goto IL_98;
				}
				container = container.BaseType;
				if (container == null)
				{
					goto IL_98;
				}
			}
			return memberSpec;
			IL_98:
			return null;
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0008B744 File Offset: 0x00089944
		public static IList<MemberSpec> FindMembers(TypeSpec container, string name, bool declaredOnlyClass)
		{
			IList<MemberSpec> result;
			while (!container.MemberCache.member_hash.TryGetValue(name, out result) && !declaredOnlyClass)
			{
				container = container.BaseType;
				if (container == null)
				{
					return null;
				}
			}
			return result;
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0008B778 File Offset: 0x00089978
		public static TypeSpec FindNestedType(TypeSpec container, string name, int arity)
		{
			TypeSpec typeSpec = null;
			TypeSpec typeSpec2;
			for (;;)
			{
				TypeContainer typeContainer = container.MemberDefinition as TypeContainer;
				if (typeContainer != null)
				{
					typeContainer.DefineContainer();
				}
				IList<MemberSpec> list;
				if (container.MemberCacheTypes.member_hash.TryGetValue(name, out list))
				{
					for (int i = list.Count - 1; i >= 0; i--)
					{
						MemberSpec memberSpec = list[i];
						if ((memberSpec.Kind & MemberKind.NestedMask) != (MemberKind)0)
						{
							typeSpec2 = (TypeSpec)memberSpec;
							if (arity == typeSpec2.Arity)
							{
								return typeSpec2;
							}
							if (arity < 0)
							{
								if (typeSpec == null)
								{
									typeSpec = typeSpec2;
								}
								else if (Math.Abs(typeSpec2.Arity + arity) < Math.Abs(typeSpec2.Arity + arity))
								{
									typeSpec = typeSpec2;
								}
							}
						}
					}
				}
				container = container.BaseType;
				if (container == null)
				{
					return typeSpec;
				}
			}
			return typeSpec2;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0008B830 File Offset: 0x00089A30
		public List<MethodSpec> FindExtensionMethods(IMemberContext invocationContext, string name, int arity)
		{
			IList<MemberSpec> list;
			if (!this.member_hash.TryGetValue(name, out list))
			{
				return null;
			}
			List<MethodSpec> list2 = null;
			foreach (MemberSpec memberSpec in list)
			{
				if (memberSpec.Kind == MemberKind.Method && (arity <= 0 || memberSpec.Arity == arity))
				{
					MethodSpec methodSpec = (MethodSpec)memberSpec;
					if (methodSpec.IsExtensionMethod && methodSpec.IsAccessible(invocationContext) && ((methodSpec.DeclaringType.Modifiers & Modifiers.INTERNAL) == (Modifiers)0 || methodSpec.DeclaringType.MemberDefinition.IsInternalAsPublic(invocationContext.Module.DeclaringAssembly)))
					{
						if (list2 == null)
						{
							list2 = new List<MethodSpec>();
						}
						list2.Add(methodSpec);
					}
				}
			}
			return list2;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0008B8F8 File Offset: 0x00089AF8
		public static MemberSpec FindBaseMember(MemberCore member, out MemberSpec bestCandidate, ref bool overrides)
		{
			bestCandidate = null;
			TypeSpec typeSpec = member.Parent.PartialContainer.Definition;
			if (!typeSpec.IsInterface)
			{
				typeSpec = typeSpec.BaseType;
				if (typeSpec == null)
				{
					return null;
				}
			}
			string lookupName = MemberCache.GetLookupName(member);
			AParametersCollection b = (member is IParametersMember) ? ((IParametersMember)member).Parameters : null;
			MemberKind memberCoreKind = MemberCache.GetMemberCoreKind(member);
			bool flag = memberCoreKind == MemberKind.Indexer || memberCoreKind == MemberKind.Property;
			MemberSpec memberSpec = null;
			MemberSpec memberSpec2;
			for (;;)
			{
				IList<MemberSpec> list;
				if (typeSpec.MemberCache.member_hash.TryGetValue(lookupName, out list))
				{
					for (int i = 0; i < list.Count; i++)
					{
						memberSpec2 = list[i];
						if ((memberSpec2.Modifiers & Modifiers.PUBLIC) != (Modifiers)0 || memberSpec2.IsAccessible(member))
						{
							if ((memberSpec2.Kind & ~MemberKind.Destructor & memberCoreKind & MemberKind.MaskType) == (MemberKind)0)
							{
								if ((memberSpec2.Kind & MemberKind.Destructor) == (MemberKind)0 && (memberCoreKind == MemberKind.Method || member.MemberName.Arity == memberSpec2.Arity))
								{
									goto IL_E6;
								}
							}
							else if (member.MemberName.Arity == memberSpec2.Arity && ((memberSpec2.Kind & memberCoreKind & (MemberKind.Method | MemberKind.Indexer)) == (MemberKind)0 || (memberSpec2.IsAccessor == member is AbstractPropertyEventMethod && TypeSpecComparer.Override.IsEqual((memberSpec2 as IParametersMember).Parameters, b))))
							{
								if (flag && (memberSpec2.Modifiers & (Modifiers.SEALED | Modifiers.OVERRIDE)) == Modifiers.OVERRIDE)
								{
									overrides = true;
								}
								else
								{
									if (memberSpec != null || (memberSpec2.Kind & memberCoreKind & (MemberKind.Method | MemberKind.Indexer)) == (MemberKind)0)
									{
										goto IL_16C;
									}
									bestCandidate = null;
									memberSpec = memberSpec2;
								}
							}
						}
					}
				}
				if (typeSpec.IsInterface || memberSpec != null)
				{
					return memberSpec;
				}
				typeSpec = typeSpec.BaseType;
				if (typeSpec == null)
				{
					return memberSpec;
				}
			}
			IL_E6:
			bestCandidate = memberSpec2;
			return null;
			IL_16C:
			bestCandidate = memberSpec;
			return memberSpec2;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0008BAA8 File Offset: 0x00089CA8
		public static T GetMember<T>(TypeSpec container, T spec) where T : MemberSpec
		{
			IList<MemberSpec> list;
			if (container.MemberCache.member_hash.TryGetValue(MemberCache.GetLookupName(spec), out list))
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					MemberSpec memberSpec = list[i];
					if (memberSpec.MemberDefinition == spec.MemberDefinition)
					{
						return (T)((object)memberSpec);
					}
				}
			}
			throw new InternalErrorException("Missing member `{0}' on inflated type `{1}'", new object[]
			{
				spec.GetSignatureForError(),
				container.GetSignatureForError()
			});
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0008BB30 File Offset: 0x00089D30
		private static MemberKind GetMemberCoreKind(MemberCore member)
		{
			if (member is FieldBase)
			{
				return MemberKind.Field;
			}
			if (member is Indexer)
			{
				return MemberKind.Indexer;
			}
			if (member is Class)
			{
				return MemberKind.Class;
			}
			if (member is Struct)
			{
				return MemberKind.Struct;
			}
			if (member is Destructor)
			{
				return MemberKind.Destructor;
			}
			if (member is Method)
			{
				return MemberKind.Method;
			}
			if (member is Property)
			{
				return MemberKind.Property;
			}
			if (member is EventField)
			{
				return MemberKind.Event;
			}
			if (member is Interface)
			{
				return MemberKind.Interface;
			}
			if (member is EventProperty)
			{
				return MemberKind.Event;
			}
			if (member is Delegate)
			{
				return MemberKind.Delegate;
			}
			if (member is Enum)
			{
				return MemberKind.Enum;
			}
			throw new NotImplementedException(member.GetType().ToString());
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0008BBE0 File Offset: 0x00089DE0
		public static List<FieldSpec> GetAllFieldsForDefiniteAssignment(TypeSpec container, IMemberContext context)
		{
			List<FieldSpec> list = null;
			bool isImported = container.MemberDefinition.IsImported;
			foreach (KeyValuePair<string, IList<MemberSpec>> keyValuePair in container.MemberCache.member_hash)
			{
				foreach (MemberSpec memberSpec in keyValuePair.Value)
				{
					if (memberSpec.Kind == MemberKind.Field && (memberSpec.Modifiers & Modifiers.STATIC) == (Modifiers)0 && !(memberSpec is FixedFieldSpec) && !(memberSpec is ConstSpec))
					{
						FieldSpec fieldSpec = (FieldSpec)memberSpec;
						if (!isImported || !MemberCache.ShouldIgnoreFieldForDefiniteAssignment(fieldSpec, context))
						{
							if (list == null)
							{
								list = new List<FieldSpec>();
							}
							list.Add(fieldSpec);
							break;
						}
					}
				}
			}
			return list ?? new List<FieldSpec>(0);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0008BCE0 File Offset: 0x00089EE0
		private static bool ShouldIgnoreFieldForDefiniteAssignment(FieldSpec fs, IMemberContext context)
		{
			Modifiers modifiers = fs.Modifiers;
			if ((modifiers & Modifiers.PRIVATE) == (Modifiers)0 && (modifiers & Modifiers.INTERNAL) != (Modifiers)0 && fs.DeclaringType.MemberDefinition.IsInternalAsPublic(context.Module.DeclaringAssembly))
			{
				return false;
			}
			TypeSpec memberType = fs.MemberType;
			MemberKind kind = memberType.Kind;
			return kind != MemberKind.TypeParameter && kind != MemberKind.ArrayType && TypeSpec.IsReferenceType(memberType);
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0008BD44 File Offset: 0x00089F44
		public static IList<MemberSpec> GetCompletitionMembers(IMemberContext ctx, TypeSpec container, string name)
		{
			List<MemberSpec> list = new List<MemberSpec>();
			foreach (KeyValuePair<string, IList<MemberSpec>> keyValuePair in container.MemberCache.member_hash)
			{
				foreach (MemberSpec memberSpec in keyValuePair.Value)
				{
					if (!memberSpec.IsAccessor && (memberSpec.Kind & (MemberKind.Constructor | MemberKind.Operator | MemberKind.Destructor)) == (MemberKind)0 && memberSpec.IsAccessible(ctx) && (name == null || memberSpec.Name.StartsWith(name)))
					{
						list.Add(memberSpec);
					}
				}
			}
			return list;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0008BE10 File Offset: 0x0008A010
		public static List<MethodSpec> GetInterfaceMethods(TypeSpec iface)
		{
			List<MethodSpec> list = new List<MethodSpec>();
			foreach (IList<MemberSpec> list2 in iface.MemberCache.member_hash.Values)
			{
				foreach (MemberSpec memberSpec in list2)
				{
					if (iface == memberSpec.DeclaringType && memberSpec.Kind == MemberKind.Method)
					{
						list.Add((MethodSpec)memberSpec);
					}
				}
			}
			return list;
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0008BEBC File Offset: 0x0008A0BC
		public static IList<MethodSpec> GetNotImplementedAbstractMethods(TypeSpec type)
		{
			if (type.MemberCache.missing_abstract != null)
			{
				return type.MemberCache.missing_abstract;
			}
			List<MethodSpec> list = new List<MethodSpec>();
			List<TypeSpec> list2 = null;
			TypeSpec typeSpec = type;
			for (;;)
			{
				foreach (KeyValuePair<string, IList<MemberSpec>> keyValuePair in typeSpec.MemberCache.member_hash)
				{
					foreach (MemberSpec memberSpec in keyValuePair.Value)
					{
						if ((memberSpec.Modifiers & Modifiers.ABSTRACT) != (Modifiers)0)
						{
							MethodSpec methodSpec = memberSpec as MethodSpec;
							if (methodSpec != null)
							{
								list.Add(methodSpec);
							}
						}
					}
				}
				TypeSpec baseType = typeSpec.BaseType;
				if (!baseType.IsAbstract)
				{
					break;
				}
				if (list2 == null)
				{
					list2 = new List<TypeSpec>();
				}
				list2.Add(typeSpec);
				typeSpec = baseType;
			}
			int num = list.Count;
			if (num == 0 || list2 == null)
			{
				type.MemberCache.missing_abstract = list;
				return type.MemberCache.missing_abstract;
			}
			foreach (TypeSpec typeSpec2 in list2)
			{
				Dictionary<string, IList<MemberSpec>> dictionary = typeSpec2.MemberCache.member_hash;
				if (dictionary.Count != 0)
				{
					for (int i = 0; i < list.Count; i++)
					{
						MethodSpec methodSpec2 = list[i];
						IList<MemberSpec> list3;
						if (methodSpec2 != null && dictionary.TryGetValue(methodSpec2.Name, out list3))
						{
							MemberFilter memberFilter = new MemberFilter(methodSpec2);
							foreach (MemberSpec memberSpec2 in list3)
							{
								if ((memberSpec2.Modifiers & (Modifiers.VIRTUAL | Modifiers.OVERRIDE)) != (Modifiers)0 && (memberSpec2.Modifiers & Modifiers.ABSTRACT) == (Modifiers)0 && memberFilter.Equals(memberSpec2))
								{
									num--;
									list[i] = null;
									break;
								}
							}
						}
					}
				}
			}
			if (num == list.Count)
			{
				type.MemberCache.missing_abstract = list;
				return type.MemberCache.missing_abstract;
			}
			MethodSpec[] array = new MethodSpec[num];
			int num2 = 0;
			foreach (MethodSpec methodSpec3 in list)
			{
				if (methodSpec3 != null)
				{
					array[num2++] = methodSpec3;
				}
			}
			type.MemberCache.missing_abstract = array;
			return type.MemberCache.missing_abstract;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0008C170 File Offset: 0x0008A370
		private static string GetLookupName(MemberSpec ms)
		{
			if (ms.Kind == MemberKind.Indexer)
			{
				return MemberCache.IndexerNameAlias;
			}
			if (ms.Kind != MemberKind.Constructor)
			{
				return ms.Name;
			}
			if (ms.IsStatic)
			{
				return Constructor.TypeConstructorName;
			}
			return Constructor.ConstructorName;
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0008C1A5 File Offset: 0x0008A3A5
		private static string GetLookupName(MemberCore mc)
		{
			if (mc is Indexer)
			{
				return MemberCache.IndexerNameAlias;
			}
			if (!(mc is Constructor))
			{
				return mc.MemberName.Name;
			}
			if (!mc.IsStatic)
			{
				return Constructor.ConstructorName;
			}
			return Constructor.TypeConstructorName;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0008C1DC File Offset: 0x0008A3DC
		public static IList<MemberSpec> GetUserOperator(TypeSpec container, Operator.OpType op, bool declaredOnly)
		{
			IList<MemberSpec> list = null;
			bool flag = true;
			do
			{
				MemberCache memberCache = container.MemberCache;
				IList<MemberSpec> list2;
				if ((((op == Operator.OpType.Implicit || op == Operator.OpType.Explicit) && (memberCache.state & MemberCache.StateFlags.HasConversionOperator) != (MemberCache.StateFlags)0) || (memberCache.state & MemberCache.StateFlags.HasUserOperator) != (MemberCache.StateFlags)0) && memberCache.member_hash.TryGetValue(Operator.GetMetadataName(op), out list2))
				{
					int i = 0;
					while (i < list2.Count && list2[i].Kind == MemberKind.Operator)
					{
						i++;
					}
					if (i != list2.Count)
					{
						for (i = 0; i < list2.Count; i++)
						{
							if (list2[i].Kind == MemberKind.Operator)
							{
								if (list == null)
								{
									list = new List<MemberSpec>();
									list.Add(list2[i]);
								}
								else
								{
									List<MemberSpec> list3;
									if (flag)
									{
										flag = false;
										list3 = new List<MemberSpec>(list.Count + 1);
										list3.AddRange(list);
									}
									else
									{
										list3 = (List<MemberSpec>)list;
									}
									list3.Add(list2[i]);
								}
							}
						}
					}
					else if (list == null)
					{
						list = list2;
						flag = true;
					}
					else
					{
						List<MemberSpec> list4;
						if (flag)
						{
							flag = false;
							list4 = new List<MemberSpec>(list.Count + list2.Count);
							list4.AddRange(list);
							list = list4;
						}
						else
						{
							list4 = (List<MemberSpec>)list;
						}
						list4.AddRange(list2);
					}
				}
				if (declaredOnly)
				{
					break;
				}
				container = container.BaseType;
			}
			while (container != null);
			return list;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0008C324 File Offset: 0x0008A524
		public void InflateTypes(MemberCache inflated_cache, TypeParameterInflator inflator)
		{
			foreach (KeyValuePair<string, IList<MemberSpec>> keyValuePair in this.member_hash)
			{
				IList<MemberSpec> list = null;
				for (int i = 0; i < keyValuePair.Value.Count; i++)
				{
					MemberSpec memberSpec = keyValuePair.Value[i];
					if (memberSpec != null && (memberSpec.Kind & MemberKind.NestedMask) != (MemberKind)0 && (memberSpec.Modifiers & Modifiers.COMPILER_GENERATED) == (Modifiers)0)
					{
						if (list == null)
						{
							list = new MemberSpec[keyValuePair.Value.Count];
							inflated_cache.member_hash.Add(keyValuePair.Key, list);
						}
						list[i] = memberSpec.InflateMember(inflator);
					}
				}
			}
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x0008C3F8 File Offset: 0x0008A5F8
		public void InflateMembers(MemberCache cacheToInflate, TypeSpec inflatedType, TypeParameterInflator inflator)
		{
			Dictionary<string, IList<MemberSpec>> dictionary = cacheToInflate.member_hash;
			Dictionary<MemberSpec, MethodSpec> dictionary2 = null;
			List<MemberSpec> list = null;
			cacheToInflate.state = this.state;
			foreach (KeyValuePair<string, IList<MemberSpec>> keyValuePair in this.member_hash)
			{
				IList<MemberSpec> value = keyValuePair.Value;
				IList<MemberSpec> list2 = null;
				for (int i = 0; i < value.Count; i++)
				{
					MemberSpec memberSpec = value[i];
					if ((memberSpec.Kind & MemberKind.NestedMask) != (MemberKind)0 && (memberSpec.Modifiers & Modifiers.COMPILER_GENERATED) == (Modifiers)0)
					{
						if (list2 == null)
						{
							list2 = dictionary[keyValuePair.Key];
						}
					}
					else
					{
						if (list2 == null)
						{
							list2 = new MemberSpec[keyValuePair.Value.Count];
							dictionary.Add(keyValuePair.Key, list2);
						}
						TypeParameterInflator inflator2 = inflator;
						if (memberSpec.DeclaringType != inflatedType)
						{
							if (!memberSpec.DeclaringType.IsGeneric && !memberSpec.DeclaringType.IsNested)
							{
								list2[i] = memberSpec;
								goto IL_169;
							}
							TypeSpec typeSpec = inflator.Inflate(memberSpec.DeclaringType);
							if (typeSpec != inflator.TypeInstance)
							{
								inflator2 = new TypeParameterInflator(inflator, typeSpec);
							}
						}
						MemberSpec memberSpec2 = memberSpec.InflateMember(inflator2);
						list2[i] = memberSpec2;
						if (memberSpec is PropertySpec || memberSpec is EventSpec)
						{
							if (list == null)
							{
								list = new List<MemberSpec>();
							}
							list.Add(memberSpec2);
						}
						else if (memberSpec.IsAccessor)
						{
							if (dictionary2 == null)
							{
								dictionary2 = new Dictionary<MemberSpec, MethodSpec>();
							}
							dictionary2.Add(memberSpec, (MethodSpec)memberSpec2);
						}
					}
					IL_169:;
				}
			}
			if (list != null)
			{
				foreach (MemberSpec memberSpec3 in list)
				{
					PropertySpec propertySpec = memberSpec3 as PropertySpec;
					if (propertySpec != null)
					{
						if (propertySpec.Get != null)
						{
							propertySpec.Get = dictionary2[propertySpec.Get];
						}
						if (propertySpec.Set != null)
						{
							propertySpec.Set = dictionary2[propertySpec.Set];
						}
					}
					else
					{
						EventSpec eventSpec = (EventSpec)memberSpec3;
						eventSpec.AccessorAdd = dictionary2[eventSpec.AccessorAdd];
						eventSpec.AccessorRemove = dictionary2[eventSpec.AccessorRemove];
					}
				}
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0008C684 File Offset: 0x0008A884
		public void RemoveHiddenMembers(TypeSpec container)
		{
			foreach (KeyValuePair<string, IList<MemberSpec>> keyValuePair in this.member_hash)
			{
				IList<MemberSpec> value = keyValuePair.Value;
				int num = 0;
				while (value[num].DeclaringType != container && ++num < keyValuePair.Value.Count)
				{
				}
				if (num != 0 && num != value.Count)
				{
					for (int i = 0; i < num; i++)
					{
						MemberSpec memberSpec = value[i];
						if (container.ImplementsInterface(memberSpec.DeclaringType, false))
						{
							AParametersCollection b = (memberSpec is IParametersMember) ? ((IParametersMember)memberSpec).Parameters : ParametersCompiled.EmptyReadOnlyParameters;
							for (int j = num; j < value.Count; j++)
							{
								MemberSpec memberSpec2 = value[j];
								if (memberSpec2.Arity == memberSpec.Arity && (!(memberSpec2 is IParametersMember) || TypeSpecComparer.Override.IsEqual(((IParametersMember)memberSpec2).Parameters, b)))
								{
									value.RemoveAt(i);
									num--;
									j--;
									i--;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0008C7D8 File Offset: 0x0008A9D8
		public void VerifyClsCompliance(TypeSpec container, Report report)
		{
			if (this.locase_members != null)
			{
				return;
			}
			if (container.BaseType == null)
			{
				this.locase_members = new Dictionary<string, MemberSpec[]>(this.member_hash.Count);
			}
			else
			{
				TypeSpec definition = container.BaseType.GetDefinition();
				definition.MemberCache.VerifyClsCompliance(definition, report);
				this.locase_members = new Dictionary<string, MemberSpec[]>(definition.MemberCache.locase_members);
			}
			bool isImported = container.MemberDefinition.IsImported;
			foreach (KeyValuePair<string, IList<MemberSpec>> keyValuePair in container.MemberCache.member_hash)
			{
				for (int i = 0; i < keyValuePair.Value.Count; i++)
				{
					MemberSpec memberSpec = keyValuePair.Value[i];
					if ((memberSpec.Modifiers & (Modifiers.PROTECTED | Modifiers.PUBLIC)) != (Modifiers)0 && (memberSpec.Modifiers & (Modifiers.OVERRIDE | Modifiers.COMPILER_GENERATED)) == (Modifiers)0 && (memberSpec.Kind & MemberKind.MaskType) != (MemberKind)0 && !(memberSpec.MemberDefinition.CLSAttributeValue == false))
					{
						IParametersMember parametersMember = null;
						if (!isImported)
						{
							parametersMember = (memberSpec as IParametersMember);
							if (parametersMember != null && !memberSpec.IsAccessor)
							{
								AParametersCollection parameters = parametersMember.Parameters;
								for (int j = i + 1; j < keyValuePair.Value.Count; j++)
								{
									MemberSpec memberSpec2 = keyValuePair.Value[j];
									IParametersMember parametersMember2 = memberSpec2 as IParametersMember;
									if (parametersMember2 != null && parameters.Count == parametersMember2.Parameters.Count && !memberSpec2.IsAccessor)
									{
										int num = ParametersCompiled.IsSameClsSignature(parametersMember.Parameters, parametersMember2.Parameters);
										if (num != 0)
										{
											MemberCache.ReportOverloadedMethodClsDifference(memberSpec, memberSpec2, num, report);
										}
									}
								}
							}
						}
						if (i <= 0 && memberSpec.Kind != MemberKind.Constructor && memberSpec.Kind != MemberKind.Indexer)
						{
							string key = memberSpec.Name.ToLowerInvariant();
							MemberSpec[] array;
							if (!this.locase_members.TryGetValue(key, out array))
							{
								array = new MemberSpec[]
								{
									memberSpec
								};
								this.locase_members.Add(key, array);
							}
							else
							{
								bool flag = true;
								foreach (MemberSpec memberSpec3 in array)
								{
									if (memberSpec3.Name == memberSpec.Name)
									{
										if (parametersMember != null)
										{
											IParametersMember parametersMember3 = memberSpec3 as IParametersMember;
											if (parametersMember3 != null && parametersMember.Parameters.Count == parametersMember3.Parameters.Count && !memberSpec3.IsAccessor)
											{
												int num2 = ParametersCompiled.IsSameClsSignature(parametersMember.Parameters, parametersMember3.Parameters);
												if (num2 != 0)
												{
													MemberCache.ReportOverloadedMethodClsDifference(memberSpec3, memberSpec, num2, report);
												}
											}
										}
									}
									else
									{
										flag = false;
										if (!isImported)
										{
											MemberCore laterDefinedMember = MemberCache.GetLaterDefinedMember(memberSpec3, memberSpec);
											if (laterDefinedMember == memberSpec3.MemberDefinition)
											{
												report.SymbolRelatedToPreviousError(memberSpec);
											}
											else
											{
												report.SymbolRelatedToPreviousError(memberSpec3);
											}
											report.Warning(3005, 1, laterDefinedMember.Location, "Identifier `{0}' differing only in case is not CLS-compliant", laterDefinedMember.GetSignatureForError());
										}
									}
								}
								if (!flag)
								{
									Array.Resize<MemberSpec>(ref array, array.Length + 1);
									MemberSpec[] array3 = array;
									array3[array3.Length - 1] = memberSpec;
									this.locase_members[key] = array;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0008CB50 File Offset: 0x0008AD50
		private static MemberCore GetLaterDefinedMember(MemberSpec a, MemberSpec b)
		{
			MemberCore memberCore = a.MemberDefinition as MemberCore;
			MemberCore memberCore2 = b.MemberDefinition as MemberCore;
			if (memberCore == null)
			{
				return memberCore2;
			}
			if (memberCore2 == null)
			{
				return memberCore;
			}
			if (a.DeclaringType.MemberDefinition != b.DeclaringType.MemberDefinition)
			{
				return memberCore2;
			}
			if (memberCore.Location.File != memberCore.Location.File)
			{
				return memberCore2;
			}
			if (memberCore2.Location.Row <= memberCore.Location.Row)
			{
				return memberCore;
			}
			return memberCore2;
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0008CBDC File Offset: 0x0008ADDC
		private static void ReportOverloadedMethodClsDifference(MemberSpec a, MemberSpec b, int res, Report report)
		{
			MemberCore laterDefinedMember = MemberCache.GetLaterDefinedMember(a, b);
			if (laterDefinedMember == a.MemberDefinition)
			{
				report.SymbolRelatedToPreviousError(b);
			}
			else
			{
				report.SymbolRelatedToPreviousError(a);
			}
			if ((res & 1) != 0)
			{
				report.Warning(3006, 1, laterDefinedMember.Location, "Overloaded method `{0}' differing only in ref or out, or in array rank, is not CLS-compliant", laterDefinedMember.GetSignatureForError());
			}
			if ((res & 2) != 0)
			{
				report.Warning(3007, 1, laterDefinedMember.Location, "Overloaded method `{0}' differing only by unnamed array types is not CLS-compliant", laterDefinedMember.GetSignatureForError());
			}
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0008CC50 File Offset: 0x0008AE50
		public bool CheckExistingMembersOverloads(MemberCore member, AParametersCollection parameters)
		{
			string name = MemberCache.GetLookupName(member);
			InterfaceMemberBase interfaceMemberBase = member as InterfaceMemberBase;
			if (interfaceMemberBase != null && interfaceMemberBase.IsExplicitImpl)
			{
				name = interfaceMemberBase.GetFullName(name);
			}
			return this.CheckExistingMembersOverloads(member, name, parameters);
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0008CC88 File Offset: 0x0008AE88
		public bool CheckExistingMembersOverloads(MemberCore member, string name, AParametersCollection parameters)
		{
			IList<MemberSpec> list;
			if (!this.member_hash.TryGetValue(name, out list))
			{
				return false;
			}
			Report report = member.Compiler.Report;
			int count = parameters.Count;
			for (int i = list.Count - 1; i >= 0; i--)
			{
				MemberSpec memberSpec = list[i];
				IParametersMember parametersMember = memberSpec as IParametersMember;
				AParametersCollection aparametersCollection = (parametersMember == null) ? ParametersCompiled.EmptyReadOnlyParameters : parametersMember.Parameters;
				if (aparametersCollection.Count == count && memberSpec.Arity == member.MemberName.Arity && member.Parent.PartialContainer == memberSpec.DeclaringType.MemberDefinition)
				{
					TypeSpec[] types = aparametersCollection.Types;
					if (count > 0)
					{
						int num = count - 1;
						TypeSpec a;
						TypeSpec b;
						bool flag;
						bool flag2;
						do
						{
							a = parameters.Types[num];
							b = types[num];
							flag = ((aparametersCollection.FixedParameters[num].ModFlags & Parameter.Modifier.RefOutMask) > Parameter.Modifier.NONE);
							flag2 = ((parameters.FixedParameters[num].ModFlags & Parameter.Modifier.RefOutMask) > Parameter.Modifier.NONE);
						}
						while (flag == flag2 && TypeSpecComparer.Override.IsEqual(a, b) && num-- != 0);
						if (num >= 0 || (member is Operator && memberSpec.Kind == MemberKind.Operator && ((MethodSpec)memberSpec).ReturnType != ((Operator)member).ReturnType))
						{
							goto IL_4A9;
						}
						if (aparametersCollection != null && member is MethodCore)
						{
							num = count;
							while (num-- != 0 && (parameters.FixedParameters[num].ModFlags & Parameter.Modifier.ModifierMask) == (aparametersCollection.FixedParameters[num].ModFlags & Parameter.Modifier.ModifierMask) && parameters.ExtensionMethodType == aparametersCollection.ExtensionMethodType)
							{
							}
							if (num >= 0)
							{
								MethodSpec methodSpec = memberSpec as MethodSpec;
								member.Compiler.Report.SymbolRelatedToPreviousError(memberSpec);
								if ((member.ModFlags & Modifiers.PARTIAL) != (Modifiers)0 && (methodSpec.Modifiers & Modifiers.PARTIAL) != (Modifiers)0)
								{
									if (parameters.HasParams || aparametersCollection.HasParams)
									{
										report.Error(758, member.Location, "A partial method declaration and partial method implementation cannot differ on use of `params' modifier");
									}
									else
									{
										report.Error(755, member.Location, "A partial method declaration and partial method implementation must be both an extension method or neither");
									}
								}
								else if (member is Constructor)
								{
									report.Error(851, member.Location, "Overloaded contructor `{0}' cannot differ on use of parameter modifiers only", member.GetSignatureForError());
								}
								else
								{
									report.Error(663, member.Location, "Overloaded method `{0}' cannot differ on use of parameter modifiers only", member.GetSignatureForError());
								}
								return false;
							}
						}
					}
					if ((memberSpec.Kind & MemberKind.Method) != (MemberKind)0)
					{
						Method method = member as Method;
						Method method2 = memberSpec.MemberDefinition as Method;
						if (method != null && method2 != null && (method.ModFlags & method2.ModFlags & Modifiers.PARTIAL) != (Modifiers)0)
						{
							if (method.IsPartialDefinition != method2.IsPartialImplementation)
							{
								report.SymbolRelatedToPreviousError(memberSpec);
								if (method.IsPartialDefinition)
								{
									report.Error(756, member.Location, "A partial method `{0}' declaration is already defined", member.GetSignatureForError());
								}
								report.Error(757, member.Location, "A partial method `{0}' implementation is already defined", member.GetSignatureForError());
								return false;
							}
							if ((method.ModFlags & (Modifiers.STATIC | Modifiers.UNSAFE)) != (method2.ModFlags & (Modifiers.STATIC | Modifiers.UNSAFE)) && (!method.Parent.IsUnsafe || !method2.Parent.IsUnsafe))
							{
								if (method.IsStatic != method2.IsStatic)
								{
									report.SymbolRelatedToPreviousError(memberSpec);
									report.Error(763, member.Location, "A partial method declaration and partial method implementation must be both `static' or neither");
								}
								if ((method.ModFlags & Modifiers.UNSAFE) != (method2.ModFlags & Modifiers.UNSAFE))
								{
									report.SymbolRelatedToPreviousError(memberSpec);
									report.Error(764, member.Location, "A partial method declaration and partial method implementation must be both `unsafe' or neither");
								}
								return false;
							}
							if (!method.IsPartialImplementation)
							{
								method2.SetPartialDefinition(method);
								method.caching_flags |= MemberCore.Flags.PartialDefinitionExists;
								goto IL_4A9;
							}
							method.SetPartialDefinition(method2);
							if (list.Count == 1)
							{
								this.member_hash.Remove(name);
								goto IL_4A9;
							}
							list.RemoveAt(i);
							goto IL_4A9;
						}
						else
						{
							report.SymbolRelatedToPreviousError(memberSpec);
							bool flag3 = member is AbstractPropertyEventMethod || member is Operator;
							bool isReservedMethod = ((MethodSpec)memberSpec).IsReservedMethod;
							if (flag3 || isReservedMethod)
							{
								report.Error(82, member.Location, "A member `{0}' is already reserved", flag3 ? memberSpec.GetSignatureForError() : member.GetSignatureForError());
								return false;
							}
						}
					}
					else
					{
						report.SymbolRelatedToPreviousError(memberSpec);
					}
					if (member is Operator && memberSpec.Kind == MemberKind.Operator)
					{
						report.Error(557, member.Location, "Duplicate user-defined conversion in type `{0}'", member.Parent.GetSignatureForError());
						return false;
					}
					report.Error(111, member.Location, "A member `{0}' is already defined. Rename this member or use different parameter types", member.GetSignatureForError());
					return false;
				}
				IL_4A9:;
			}
			return true;
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0008D14A File Offset: 0x0008B34A
		// Note: this type is marked as 'beforefieldinit'.
		static MemberCache()
		{
		}

		// Token: 0x04000AC2 RID: 2754
		private readonly Dictionary<string, IList<MemberSpec>> member_hash;

		// Token: 0x04000AC3 RID: 2755
		private Dictionary<string, MemberSpec[]> locase_members;

		// Token: 0x04000AC4 RID: 2756
		private IList<MethodSpec> missing_abstract;

		// Token: 0x04000AC5 RID: 2757
		private MemberCache.StateFlags state;

		// Token: 0x04000AC6 RID: 2758
		public static readonly string IndexerNameAlias = "<this>";

		// Token: 0x04000AC7 RID: 2759
		public static readonly MemberCache Empty = new MemberCache(0);

		// Token: 0x020003D0 RID: 976
		[Flags]
		private enum StateFlags
		{
			// Token: 0x040010CC RID: 4300
			HasConversionOperator = 2,
			// Token: 0x040010CD RID: 4301
			HasUserOperator = 4
		}
	}
}

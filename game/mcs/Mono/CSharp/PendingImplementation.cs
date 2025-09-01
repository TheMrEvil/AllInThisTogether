using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000274 RID: 628
	public class PendingImplementation
	{
		// Token: 0x06001ECB RID: 7883 RVA: 0x00097480 File Offset: 0x00095680
		private PendingImplementation(TypeDefinition container, PendingImplementation.MissingInterfacesInfo[] missing_ifaces, MethodSpec[] abstract_methods, int total)
		{
			TypeSpec definition = container.Definition;
			this.container = container;
			this.pending_implementations = new TypeAndMethods[total];
			int num = 0;
			if (abstract_methods != null)
			{
				int num2 = abstract_methods.Length;
				this.pending_implementations[num].methods = new MethodSpec[num2];
				this.pending_implementations[num].need_proxy = new MethodSpec[num2];
				this.pending_implementations[num].methods = abstract_methods;
				this.pending_implementations[num].found = new MethodData[num2];
				this.pending_implementations[num].type = definition;
				num++;
			}
			foreach (PendingImplementation.MissingInterfacesInfo missingInterfacesInfo in missing_ifaces)
			{
				TypeSpec type = missingInterfacesInfo.Type;
				List<MethodSpec> interfaceMethods = MemberCache.GetInterfaceMethods(type);
				int count = interfaceMethods.Count;
				this.pending_implementations[num].type = type;
				this.pending_implementations[num].optional = missingInterfacesInfo.Optional;
				this.pending_implementations[num].methods = interfaceMethods;
				this.pending_implementations[num].found = new MethodData[count];
				this.pending_implementations[num].need_proxy = new MethodSpec[count];
				num++;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x000975D5 File Offset: 0x000957D5
		private Report Report
		{
			get
			{
				return this.container.Module.Compiler.Report;
			}
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x000975EC File Offset: 0x000957EC
		private static PendingImplementation.MissingInterfacesInfo[] GetMissingInterfaces(TypeDefinition container)
		{
			IList<TypeSpec> interfaces = container.Definition.Interfaces;
			if (interfaces == null || interfaces.Count == 0)
			{
				return PendingImplementation.EmptyMissingInterfacesInfo;
			}
			PendingImplementation.MissingInterfacesInfo[] array = new PendingImplementation.MissingInterfacesInfo[interfaces.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new PendingImplementation.MissingInterfacesInfo(interfaces[i]);
			}
			if (container.BaseType == null)
			{
				return array;
			}
			IList<TypeSpec> interfaces2 = container.BaseType.Interfaces;
			if (interfaces2 != null)
			{
				foreach (TypeSpec typeSpec in interfaces2)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (typeSpec == array[j].Type)
						{
							array[j].Optional = true;
							break;
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x000976CC File Offset: 0x000958CC
		public static PendingImplementation GetPendingImplementations(TypeDefinition container)
		{
			TypeSpec baseType = container.BaseType;
			PendingImplementation.MissingInterfacesInfo[] missingInterfaces = PendingImplementation.GetMissingInterfaces(container);
			bool flag = baseType != null && baseType.IsAbstract && (container.ModFlags & Modifiers.ABSTRACT) == (Modifiers)0;
			MethodSpec[] array = null;
			if (flag)
			{
				IList<MethodSpec> notImplementedAbstractMethods = MemberCache.GetNotImplementedAbstractMethods(baseType);
				if (notImplementedAbstractMethods == null)
				{
					flag = false;
				}
				else
				{
					array = new MethodSpec[notImplementedAbstractMethods.Count];
					notImplementedAbstractMethods.CopyTo(array, 0);
				}
			}
			int num = missingInterfaces.Length + (flag ? 1 : 0);
			if (num == 0)
			{
				return null;
			}
			PendingImplementation pendingImplementation = new PendingImplementation(container, missingInterfaces, array, num);
			foreach (TypeAndMethods typeAndMethods in pendingImplementation.pending_implementations)
			{
				if (typeAndMethods.type.IsGeneric)
				{
					for (int j = 0; j < typeAndMethods.methods.Count; j++)
					{
						MethodSpec methodSpec = typeAndMethods.methods[j];
						if (!methodSpec.Parameters.IsEmpty)
						{
							for (int k = j + 1; k < typeAndMethods.methods.Count; k++)
							{
								MethodSpec methodSpec2 = typeAndMethods.methods[k];
								if (!(methodSpec.Name != methodSpec2.Name) && typeAndMethods.type == methodSpec2.DeclaringType && TypeSpecComparer.Override.IsSame(methodSpec.Parameters.Types, methodSpec2.Parameters.Types))
								{
									bool flag2 = true;
									bool flag3 = false;
									IParameterData[] fixedParameters = methodSpec.Parameters.FixedParameters;
									IParameterData[] fixedParameters2 = methodSpec2.Parameters.FixedParameters;
									for (int l = 0; l < fixedParameters.Length; l++)
									{
										if ((fixedParameters[l].ModFlags & Parameter.Modifier.RefOutMask) != (fixedParameters2[l].ModFlags & Parameter.Modifier.RefOutMask))
										{
											if (((fixedParameters[l].ModFlags | fixedParameters2[l].ModFlags) & Parameter.Modifier.RefOutMask) != Parameter.Modifier.RefOutMask)
											{
												flag2 = false;
												break;
											}
											flag3 = true;
										}
									}
									if (flag2 && flag3)
									{
										pendingImplementation.Report.SymbolRelatedToPreviousError(methodSpec);
										pendingImplementation.Report.SymbolRelatedToPreviousError(methodSpec2);
										pendingImplementation.Report.Error(767, container.Location, "Cannot implement interface `{0}' with the specified type parameters because it causes method `{1}' to differ on parameter modifiers only", typeAndMethods.type.GetDefinition().GetSignatureForError(), methodSpec.GetSignatureForError());
										break;
									}
								}
							}
						}
					}
				}
			}
			return pendingImplementation;
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x0009791C File Offset: 0x00095B1C
		public MethodSpec IsInterfaceMethod(MemberName name, TypeSpec ifaceType, MethodData method, out MethodSpec ambiguousCandidate, ref bool optional)
		{
			return this.InterfaceMethod(name, ifaceType, method, PendingImplementation.Operation.Lookup, out ambiguousCandidate, ref optional);
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x0009792C File Offset: 0x00095B2C
		public void ImplementMethod(MemberName name, TypeSpec ifaceType, MethodData method, bool clear_one, out MethodSpec ambiguousCandidate, ref bool optional)
		{
			this.InterfaceMethod(name, ifaceType, method, clear_one ? PendingImplementation.Operation.ClearOne : PendingImplementation.Operation.ClearAll, out ambiguousCandidate, ref optional);
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x00097944 File Offset: 0x00095B44
		public MethodSpec InterfaceMethod(MemberName name, TypeSpec iType, MethodData method, PendingImplementation.Operation op, out MethodSpec ambiguousCandidate, ref bool optional)
		{
			ambiguousCandidate = null;
			if (this.pending_implementations == null)
			{
				return null;
			}
			TypeSpec returnType = method.method.ReturnType;
			ParametersCompiled parameterInfo = method.method.ParameterInfo;
			bool flag = method.method is Indexer.SetIndexerMethod || method.method is Indexer.GetIndexerMethod;
			MethodSpec methodSpec;
			foreach (TypeAndMethods typeAndMethods in this.pending_implementations)
			{
				if (iType == null || typeAndMethods.type == iType)
				{
					int count = typeAndMethods.methods.Count;
					for (int j = 0; j < count; j++)
					{
						methodSpec = typeAndMethods.methods[j];
						if (methodSpec != null)
						{
							if (flag)
							{
								if (!methodSpec.IsAccessor)
								{
									goto IL_1A6;
								}
								if (methodSpec.Parameters.IsEmpty)
								{
									goto IL_1A6;
								}
							}
							else if (name.Name != methodSpec.Name || methodSpec.Arity != name.Arity)
							{
								goto IL_1A6;
							}
							if (TypeSpecComparer.Override.IsEqual(methodSpec.Parameters, parameterInfo))
							{
								if (!TypeSpecComparer.Override.IsEqual(methodSpec.ReturnType, returnType))
								{
									typeAndMethods.found[j] = method;
								}
								else
								{
									if (op != PendingImplementation.Operation.Lookup)
									{
										if (methodSpec.IsAccessor != method.method.IsAccessor)
										{
											goto IL_1A6;
										}
										if (methodSpec.DeclaringType.IsInterface && iType == null && name.Name != methodSpec.Name)
										{
											typeAndMethods.need_proxy[j] = method.method.Spec;
										}
										else
										{
											typeAndMethods.methods[j] = null;
										}
									}
									else
									{
										typeAndMethods.found[j] = method;
										optional = typeAndMethods.optional;
									}
									if (op == PendingImplementation.Operation.Lookup && name.ExplicitInterface != null && ambiguousCandidate == null)
									{
										ambiguousCandidate = methodSpec;
									}
									else if (op != PendingImplementation.Operation.ClearAll)
									{
										return methodSpec;
									}
								}
							}
						}
						IL_1A6:;
					}
					if (typeAndMethods.type == iType)
					{
						break;
					}
				}
			}
			methodSpec = ambiguousCandidate;
			ambiguousCandidate = null;
			return methodSpec;
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00097B2C File Offset: 0x00095D2C
		private void DefineProxy(TypeSpec iface, MethodSpec base_method, MethodSpec iface_method)
		{
			string @namespace = iface.MemberDefinition.Namespace;
			string name;
			if (string.IsNullOrEmpty(@namespace))
			{
				name = iface.MemberDefinition.Name + "." + iface_method.Name;
			}
			else
			{
				name = string.Concat(new string[]
				{
					@namespace,
					".",
					iface.MemberDefinition.Name,
					".",
					iface_method.Name
				});
			}
			AParametersCollection parameters = iface_method.Parameters;
			MethodBuilder methodBuilder = this.container.TypeBuilder.DefineMethod(name, MethodAttributes.Private | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.CheckAccessOnOverride | MethodAttributes.VtableLayoutMask, CallingConventions.Standard | CallingConventions.HasThis, base_method.ReturnType.GetMetaInfo(), parameters.GetMetaInfo());
			if (iface_method.IsGeneric)
			{
				string[] names = (from l in iface_method.GenericDefinition.TypeParameters
				select l.Name).ToArray<string>();
				methodBuilder.DefineGenericParameters(names);
			}
			for (int i = 0; i < parameters.Count; i++)
			{
				string name2 = parameters.FixedParameters[i].Name;
				ParameterAttributes parameterAttribute = AParametersCollection.GetParameterAttribute(parameters.FixedParameters[i].ModFlags);
				methodBuilder.DefineParameter(i + 1, parameterAttribute, name2);
			}
			int count = parameters.Count;
			EmitContext emitContext = new EmitContext(new ProxyMethodContext(this.container), methodBuilder.GetILGenerator(), null, null);
			emitContext.EmitThis();
			for (int j = 0; j < count; j++)
			{
				emitContext.EmitArgumentLoad(j);
			}
			emitContext.Emit(OpCodes.Call, base_method);
			emitContext.Emit(OpCodes.Ret);
			this.container.TypeBuilder.DefineMethodOverride(methodBuilder, (MethodInfo)iface_method.GetMetaInfo());
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00097CE0 File Offset: 0x00095EE0
		private bool BaseImplements(TypeSpec iface_type, MethodSpec mi, out MethodSpec base_method)
		{
			base_method = null;
			TypeSpec baseType = this.container.BaseType;
			AParametersCollection parameters = mi.Parameters;
			MethodSpec methodSpec = null;
			MethodSpec methodSpec2;
			for (;;)
			{
				IList<MemberSpec> list = MemberCache.FindMembers(baseType, mi.Name, false);
				if (list == null)
				{
					break;
				}
				methodSpec2 = null;
				foreach (MemberSpec memberSpec in list)
				{
					if (memberSpec.Kind == MemberKind.Method && memberSpec.Arity == mi.Arity)
					{
						AParametersCollection parameters2 = ((MethodSpec)memberSpec).Parameters;
						if (TypeSpecComparer.Override.IsEqual(parameters.Types, parameters2.Types))
						{
							bool flag = true;
							for (int i = 0; i < parameters.Count; i++)
							{
								if ((parameters.FixedParameters[i].ModFlags & Parameter.Modifier.RefOutMask) != (parameters2.FixedParameters[i].ModFlags & Parameter.Modifier.RefOutMask))
								{
									flag = false;
									if ((parameters.FixedParameters[i].ModFlags & Parameter.Modifier.RefOutMask) == (parameters2.FixedParameters[i].ModFlags & Parameter.Modifier.RefOutMask))
									{
										methodSpec2 = null;
										break;
									}
									if (methodSpec2 == null)
									{
										if (!memberSpec.IsPublic || !TypeSpecComparer.Override.IsEqual(mi.ReturnType, ((MethodSpec)memberSpec).ReturnType))
										{
											break;
										}
										methodSpec2 = (MethodSpec)memberSpec;
									}
								}
							}
							if (flag)
							{
								MethodSpec methodSpec3 = (MethodSpec)memberSpec;
								if (!methodSpec3.IsPublic)
								{
									if (methodSpec == null)
									{
										methodSpec = methodSpec3;
									}
								}
								else if (!TypeSpecComparer.Override.IsEqual(mi.ReturnType, methodSpec3.ReturnType))
								{
									if (methodSpec == null)
									{
										methodSpec = methodSpec3;
									}
								}
								else
								{
									base_method = methodSpec3;
									if (mi.IsGeneric && !Method.CheckImplementingMethodConstraints(this.container, methodSpec3, mi))
									{
										return true;
									}
								}
							}
						}
					}
				}
				if (base_method != null)
				{
					goto Block_3;
				}
				baseType = list[0].DeclaringType.BaseType;
				if (baseType == null)
				{
					goto Block_5;
				}
			}
			base_method = methodSpec;
			return false;
			Block_3:
			if (methodSpec2 != null)
			{
				this.Report.SymbolRelatedToPreviousError(methodSpec2);
				this.Report.SymbolRelatedToPreviousError(mi);
				this.Report.SymbolRelatedToPreviousError(this.container);
				this.Report.Warning(1956, 1, ((MemberCore)base_method.MemberDefinition).Location, "The interface method `{0}' implementation is ambiguous between following methods: `{1}' and `{2}' in type `{3}'", new object[]
				{
					mi.GetSignatureForError(),
					base_method.GetSignatureForError(),
					methodSpec2.GetSignatureForError(),
					this.container.GetSignatureForError()
				});
			}
			if (!base_method.IsVirtual)
			{
				this.DefineProxy(iface_type, base_method, mi);
			}
			return true;
			Block_5:
			base_method = methodSpec;
			return false;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00097F70 File Offset: 0x00096170
		public bool VerifyPendingMethods()
		{
			int num = this.pending_implementations.Length;
			bool result = false;
			for (int i = 0; i < num; i++)
			{
				TypeSpec type = this.pending_implementations[i].type;
				bool flag = type.IsInterface && this.container.BaseType != null && this.container.BaseType.ImplementsInterface(type, false);
				for (int j = 0; j < this.pending_implementations[i].methods.Count; j++)
				{
					MethodSpec methodSpec = this.pending_implementations[i].methods[j];
					if (methodSpec != null)
					{
						if (type.IsInterface)
						{
							MethodSpec methodSpec2 = this.pending_implementations[i].need_proxy[j];
							if (methodSpec2 != null)
							{
								this.DefineProxy(type, methodSpec2, methodSpec);
								goto IL_2B3;
							}
							MethodSpec spec;
							if (this.pending_implementations[i].optional || flag || this.BaseImplements(type, methodSpec, out spec))
							{
								goto IL_2B3;
							}
							if (spec == null)
							{
								MethodData methodData = this.pending_implementations[i].found[j];
								if (methodData != null)
								{
									spec = methodData.method.Spec;
								}
							}
							this.Report.SymbolRelatedToPreviousError(methodSpec);
							if (spec != null)
							{
								this.Report.SymbolRelatedToPreviousError(spec);
								if (spec.IsStatic)
								{
									this.Report.Error(736, this.container.Location, "`{0}' does not implement interface member `{1}' and the best implementing candidate `{2}' is static", new string[]
									{
										this.container.GetSignatureForError(),
										methodSpec.GetSignatureForError(),
										spec.GetSignatureForError()
									});
								}
								else if ((spec.Modifiers & Modifiers.PUBLIC) == (Modifiers)0)
								{
									this.Report.Error(737, this.container.Location, "`{0}' does not implement interface member `{1}' and the best implementing candidate `{2}' is not public", new string[]
									{
										this.container.GetSignatureForError(),
										methodSpec.GetSignatureForError(),
										spec.GetSignatureForError()
									});
								}
								else
								{
									this.Report.Error(738, this.container.Location, "`{0}' does not implement interface member `{1}' and the best implementing candidate `{2}' return type `{3}' does not match interface member return type `{4}'", new string[]
									{
										this.container.GetSignatureForError(),
										methodSpec.GetSignatureForError(),
										spec.GetSignatureForError(),
										spec.ReturnType.GetSignatureForError(),
										methodSpec.ReturnType.GetSignatureForError()
									});
								}
							}
							else
							{
								this.Report.Error(535, this.container.Location, "`{0}' does not implement interface member `{1}'", this.container.GetSignatureForError(), methodSpec.GetSignatureForError());
							}
						}
						else
						{
							this.Report.SymbolRelatedToPreviousError(methodSpec);
							this.Report.Error(534, this.container.Location, "`{0}' does not implement inherited abstract member `{1}'", this.container.GetSignatureForError(), methodSpec.GetSignatureForError());
						}
						result = true;
					}
					IL_2B3:;
				}
			}
			return result;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x0009825F File Offset: 0x0009645F
		// Note: this type is marked as 'beforefieldinit'.
		static PendingImplementation()
		{
		}

		// Token: 0x04000B5A RID: 2906
		private readonly TypeDefinition container;

		// Token: 0x04000B5B RID: 2907
		private TypeAndMethods[] pending_implementations;

		// Token: 0x04000B5C RID: 2908
		private static readonly PendingImplementation.MissingInterfacesInfo[] EmptyMissingInterfacesInfo = new PendingImplementation.MissingInterfacesInfo[0];

		// Token: 0x020003D7 RID: 983
		private struct MissingInterfacesInfo
		{
			// Token: 0x0600278B RID: 10123 RVA: 0x000BC4D0 File Offset: 0x000BA6D0
			public MissingInterfacesInfo(TypeSpec t)
			{
				this.Type = t;
				this.Optional = false;
			}

			// Token: 0x040010FD RID: 4349
			public TypeSpec Type;

			// Token: 0x040010FE RID: 4350
			public bool Optional;
		}

		// Token: 0x020003D8 RID: 984
		public enum Operation
		{
			// Token: 0x04001100 RID: 4352
			Lookup,
			// Token: 0x04001101 RID: 4353
			ClearOne,
			// Token: 0x04001102 RID: 4354
			ClearAll
		}

		// Token: 0x020003D9 RID: 985
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600278C RID: 10124 RVA: 0x000BC4E0 File Offset: 0x000BA6E0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600278D RID: 10125 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c()
			{
			}

			// Token: 0x0600278E RID: 10126 RVA: 0x000B6E73 File Offset: 0x000B5073
			internal string <DefineProxy>b__13_0(TypeParameterSpec l)
			{
				return l.Name;
			}

			// Token: 0x04001103 RID: 4355
			public static readonly PendingImplementation.<>c <>9 = new PendingImplementation.<>c();

			// Token: 0x04001104 RID: 4356
			public static Func<TypeParameterSpec, string> <>9__13_0;
		}
	}
}

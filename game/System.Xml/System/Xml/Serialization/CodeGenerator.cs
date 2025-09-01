using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x02000268 RID: 616
	internal class CodeGenerator
	{
		// Token: 0x06001723 RID: 5923 RVA: 0x000893AA File Offset: 0x000875AA
		internal static bool IsValidLanguageIndependentIdentifier(string ident)
		{
			return CodeGenerator.IsValidLanguageIndependentIdentifier(ident);
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x000893B2 File Offset: 0x000875B2
		internal static void ValidateIdentifiers(CodeObject e)
		{
			CodeGenerator.ValidateIdentifiers(e);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x000893BA File Offset: 0x000875BA
		internal CodeGenerator(TypeBuilder typeBuilder)
		{
			this.typeBuilder = typeBuilder;
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x000893ED File Offset: 0x000875ED
		internal static bool IsNullableGenericType(Type type)
		{
			return type.Name == "Nullable`1";
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0000B528 File Offset: 0x00009728
		internal static void AssertHasInterface(Type type, Type iType)
		{
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00089400 File Offset: 0x00087600
		internal void BeginMethod(Type returnType, string methodName, Type[] argTypes, string[] argNames, MethodAttributes methodAttributes)
		{
			this.methodBuilder = this.typeBuilder.DefineMethod(methodName, methodAttributes, returnType, argTypes);
			this.ilGen = this.methodBuilder.GetILGenerator();
			this.InitILGeneration(argTypes, argNames, (this.methodBuilder.Attributes & MethodAttributes.Static) == MethodAttributes.Static);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0008944F File Offset: 0x0008764F
		internal void BeginMethod(Type returnType, MethodBuilderInfo methodBuilderInfo, Type[] argTypes, string[] argNames, MethodAttributes methodAttributes)
		{
			this.methodBuilder = methodBuilderInfo.MethodBuilder;
			this.ilGen = this.methodBuilder.GetILGenerator();
			this.InitILGeneration(argTypes, argNames, (this.methodBuilder.Attributes & MethodAttributes.Static) == MethodAttributes.Static);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0008948C File Offset: 0x0008768C
		private void InitILGeneration(Type[] argTypes, string[] argNames, bool isStatic)
		{
			this.methodEndLabel = this.ilGen.DefineLabel();
			this.retLabel = this.ilGen.DefineLabel();
			this.blockStack = new Stack();
			this.whileStack = new Stack();
			this.currentScope = new LocalScope();
			this.freeLocals = new Dictionary<Tuple<Type, string>, Queue<LocalBuilder>>();
			this.argList = new Dictionary<string, ArgBuilder>();
			if (!isStatic)
			{
				this.argList.Add("this", new ArgBuilder("this", 0, this.typeBuilder.BaseType));
			}
			for (int i = 0; i < argTypes.Length; i++)
			{
				ArgBuilder argBuilder = new ArgBuilder(argNames[i], this.argList.Count, argTypes[i]);
				this.argList.Add(argBuilder.Name, argBuilder);
				this.methodBuilder.DefineParameter(argBuilder.Index, ParameterAttributes.None, argBuilder.Name);
			}
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0008956C File Offset: 0x0008776C
		internal MethodBuilder EndMethod()
		{
			this.MarkLabel(this.methodEndLabel);
			this.Ret();
			MethodBuilder result = this.methodBuilder;
			this.methodBuilder = null;
			this.ilGen = null;
			this.freeLocals = null;
			this.blockStack = null;
			this.whileStack = null;
			this.argList = null;
			this.currentScope = null;
			this.retLocal = null;
			return result;
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x000895C9 File Offset: 0x000877C9
		internal MethodBuilder MethodBuilder
		{
			get
			{
				return this.methodBuilder;
			}
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x000895D1 File Offset: 0x000877D1
		internal static Exception NotSupported(string msg)
		{
			return new NotSupportedException(msg);
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x000895D9 File Offset: 0x000877D9
		internal ArgBuilder GetArg(string name)
		{
			return this.argList[name];
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x000895E7 File Offset: 0x000877E7
		internal LocalBuilder GetLocal(string name)
		{
			return this.currentScope[name];
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x000895F5 File Offset: 0x000877F5
		internal LocalBuilder ReturnLocal
		{
			get
			{
				if (this.retLocal == null)
				{
					this.retLocal = this.DeclareLocal(this.methodBuilder.ReturnType, "_ret");
				}
				return this.retLocal;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00089621 File Offset: 0x00087821
		internal Label ReturnLabel
		{
			get
			{
				return this.retLabel;
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0008962C File Offset: 0x0008782C
		internal LocalBuilder GetTempLocal(Type type)
		{
			LocalBuilder localBuilder;
			if (!this.TmpLocals.TryGetValue(type, out localBuilder))
			{
				localBuilder = this.DeclareLocal(type, "_tmp" + this.TmpLocals.Count.ToString());
				this.TmpLocals.Add(type, localBuilder);
			}
			return localBuilder;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0008967C File Offset: 0x0008787C
		internal Type GetVariableType(object var)
		{
			if (var is ArgBuilder)
			{
				return ((ArgBuilder)var).ArgType;
			}
			if (var is LocalBuilder)
			{
				return ((LocalBuilder)var).LocalType;
			}
			return var.GetType();
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x000896AC File Offset: 0x000878AC
		internal object GetVariable(string name)
		{
			object result;
			if (this.TryGetVariable(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000896C8 File Offset: 0x000878C8
		internal bool TryGetVariable(string name, out object variable)
		{
			LocalBuilder localBuilder;
			if (this.currentScope != null && this.currentScope.TryGetValue(name, out localBuilder))
			{
				variable = localBuilder;
				return true;
			}
			ArgBuilder argBuilder;
			if (this.argList != null && this.argList.TryGetValue(name, out argBuilder))
			{
				variable = argBuilder;
				return true;
			}
			int num;
			if (int.TryParse(name, out num))
			{
				variable = num;
				return true;
			}
			variable = null;
			return false;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00089728 File Offset: 0x00087928
		internal void EnterScope()
		{
			LocalScope localScope = new LocalScope(this.currentScope);
			this.currentScope = localScope;
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00089748 File Offset: 0x00087948
		internal void ExitScope()
		{
			this.currentScope.AddToFreeLocals(this.freeLocals);
			this.currentScope = this.currentScope.parent;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0008976C File Offset: 0x0008796C
		private bool TryDequeueLocal(Type type, string name, out LocalBuilder local)
		{
			Tuple<Type, string> key = new Tuple<Type, string>(type, name);
			Queue<LocalBuilder> queue;
			if (this.freeLocals.TryGetValue(key, out queue))
			{
				local = queue.Dequeue();
				if (queue.Count == 0)
				{
					this.freeLocals.Remove(key);
				}
				return true;
			}
			local = null;
			return false;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000897B4 File Offset: 0x000879B4
		internal LocalBuilder DeclareLocal(Type type, string name)
		{
			LocalBuilder localBuilder;
			if (!this.TryDequeueLocal(type, name, out localBuilder))
			{
				localBuilder = this.ilGen.DeclareLocal(type, false);
				if (DiagnosticsSwitches.KeepTempFiles.Enabled)
				{
					localBuilder.SetLocalSymInfo(name);
				}
			}
			this.currentScope[name] = localBuilder;
			return localBuilder;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000897FC File Offset: 0x000879FC
		internal LocalBuilder DeclareOrGetLocal(Type type, string name)
		{
			LocalBuilder result;
			if (!this.currentScope.TryGetValue(name, out result))
			{
				result = this.DeclareLocal(type, name);
			}
			return result;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00089824 File Offset: 0x00087A24
		internal object For(LocalBuilder local, object start, object end)
		{
			ForState forState = new ForState(local, this.DefineLabel(), this.DefineLabel(), end);
			if (forState.Index != null)
			{
				this.Load(start);
				this.Stloc(forState.Index);
				this.Br(forState.TestLabel);
			}
			this.MarkLabel(forState.BeginLabel);
			this.blockStack.Push(forState);
			return forState;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00089888 File Offset: 0x00087A88
		internal void EndFor()
		{
			ForState forState = this.blockStack.Pop() as ForState;
			if (forState.Index != null)
			{
				this.Ldloc(forState.Index);
				this.Ldc(1);
				this.Add();
				this.Stloc(forState.Index);
				this.MarkLabel(forState.TestLabel);
				this.Ldloc(forState.Index);
				this.Load(forState.End);
				if (this.GetVariableType(forState.End).IsArray)
				{
					this.Ldlen();
				}
				else
				{
					MethodInfo method = typeof(ICollection).GetMethod("get_Count", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.Call(method);
				}
				this.Blt(forState.BeginLabel);
				return;
			}
			this.Br(forState.BeginLabel);
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00089956 File Offset: 0x00087B56
		internal void If()
		{
			this.InternalIf(false);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0008995F File Offset: 0x00087B5F
		internal void IfNot()
		{
			this.InternalIf(true);
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00089968 File Offset: 0x00087B68
		private OpCode GetBranchCode(Cmp cmp)
		{
			return CodeGenerator.BranchCodes[(int)cmp];
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00089978 File Offset: 0x00087B78
		internal void If(Cmp cmpOp)
		{
			IfState ifState = new IfState();
			ifState.EndIf = this.DefineLabel();
			ifState.ElseBegin = this.DefineLabel();
			this.ilGen.Emit(this.GetBranchCode(cmpOp), ifState.ElseBegin);
			this.blockStack.Push(ifState);
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x000899C7 File Offset: 0x00087BC7
		internal void If(object value1, Cmp cmpOp, object value2)
		{
			this.Load(value1);
			this.Load(value2);
			this.If(cmpOp);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x000899E0 File Offset: 0x00087BE0
		internal void Else()
		{
			IfState ifState = this.PopIfState();
			this.Br(ifState.EndIf);
			this.MarkLabel(ifState.ElseBegin);
			ifState.ElseBegin = ifState.EndIf;
			this.blockStack.Push(ifState);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00089A24 File Offset: 0x00087C24
		internal void EndIf()
		{
			IfState ifState = this.PopIfState();
			if (!ifState.ElseBegin.Equals(ifState.EndIf))
			{
				this.MarkLabel(ifState.ElseBegin);
			}
			this.MarkLabel(ifState.EndIf);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00089A66 File Offset: 0x00087C66
		internal void BeginExceptionBlock()
		{
			this.leaveLabels.Push(this.DefineLabel());
			this.ilGen.BeginExceptionBlock();
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00089A8A File Offset: 0x00087C8A
		internal void BeginCatchBlock(Type exception)
		{
			this.ilGen.BeginCatchBlock(exception);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00089A98 File Offset: 0x00087C98
		internal void EndExceptionBlock()
		{
			this.ilGen.EndExceptionBlock();
			this.ilGen.MarkLabel((Label)this.leaveLabels.Pop());
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00089AC0 File Offset: 0x00087CC0
		internal void Leave()
		{
			this.ilGen.Emit(OpCodes.Leave, (Label)this.leaveLabels.Peek());
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00089AE2 File Offset: 0x00087CE2
		internal void Call(MethodInfo methodInfo)
		{
			if (methodInfo.IsVirtual && !methodInfo.DeclaringType.IsValueType)
			{
				this.ilGen.Emit(OpCodes.Callvirt, methodInfo);
				return;
			}
			this.ilGen.Emit(OpCodes.Call, methodInfo);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00089B1C File Offset: 0x00087D1C
		internal void Call(ConstructorInfo ctor)
		{
			this.ilGen.Emit(OpCodes.Call, ctor);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00089B2F File Offset: 0x00087D2F
		internal void New(ConstructorInfo constructorInfo)
		{
			this.ilGen.Emit(OpCodes.Newobj, constructorInfo);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00089B42 File Offset: 0x00087D42
		internal void InitObj(Type valueType)
		{
			this.ilGen.Emit(OpCodes.Initobj, valueType);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00089B55 File Offset: 0x00087D55
		internal void NewArray(Type elementType, object len)
		{
			this.Load(len);
			this.ilGen.Emit(OpCodes.Newarr, elementType);
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00089B70 File Offset: 0x00087D70
		internal void LoadArrayElement(object obj, object arrayIndex)
		{
			Type elementType = this.GetVariableType(obj).GetElementType();
			this.Load(obj);
			this.Load(arrayIndex);
			if (CodeGenerator.IsStruct(elementType))
			{
				this.Ldelema(elementType);
				this.Ldobj(elementType);
				return;
			}
			this.Ldelem(elementType);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00089BB8 File Offset: 0x00087DB8
		internal void StoreArrayElement(object obj, object arrayIndex, object value)
		{
			Type variableType = this.GetVariableType(obj);
			if (variableType == typeof(Array))
			{
				this.Load(obj);
				this.Call(typeof(Array).GetMethod("SetValue", new Type[]
				{
					typeof(object),
					typeof(int)
				}));
				return;
			}
			Type elementType = variableType.GetElementType();
			this.Load(obj);
			this.Load(arrayIndex);
			if (CodeGenerator.IsStruct(elementType))
			{
				this.Ldelema(elementType);
			}
			this.Load(value);
			this.ConvertValue(this.GetVariableType(value), elementType);
			if (CodeGenerator.IsStruct(elementType))
			{
				this.Stobj(elementType);
				return;
			}
			this.Stelem(elementType);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00089C71 File Offset: 0x00087E71
		private static bool IsStruct(Type objType)
		{
			return objType.IsValueType && !objType.IsPrimitive;
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00089C86 File Offset: 0x00087E86
		internal Type LoadMember(object obj, MemberInfo memberInfo)
		{
			if (this.GetVariableType(obj).IsValueType)
			{
				this.LoadAddress(obj);
			}
			else
			{
				this.Load(obj);
			}
			return this.LoadMember(memberInfo);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x00089CB0 File Offset: 0x00087EB0
		private static MethodInfo GetPropertyMethodFromBaseType(PropertyInfo propertyInfo, bool isGetter)
		{
			Type baseType = propertyInfo.DeclaringType.BaseType;
			string name = propertyInfo.Name;
			MethodInfo methodInfo = null;
			while (baseType != null)
			{
				PropertyInfo property = baseType.GetProperty(name);
				if (property != null)
				{
					if (isGetter)
					{
						methodInfo = property.GetGetMethod(true);
					}
					else
					{
						methodInfo = property.GetSetMethod(true);
					}
					if (methodInfo != null)
					{
						break;
					}
				}
				baseType = baseType.BaseType;
			}
			return methodInfo;
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00089D14 File Offset: 0x00087F14
		internal Type LoadMember(MemberInfo memberInfo)
		{
			Type result;
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				result = fieldInfo.FieldType;
				if (fieldInfo.IsStatic)
				{
					this.ilGen.Emit(OpCodes.Ldsfld, fieldInfo);
				}
				else
				{
					this.ilGen.Emit(OpCodes.Ldfld, fieldInfo);
				}
			}
			else
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				result = propertyInfo.PropertyType;
				if (propertyInfo != null)
				{
					MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = CodeGenerator.GetPropertyMethodFromBaseType(propertyInfo, true);
					}
					this.Call(methodInfo);
				}
			}
			return result;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00089DA0 File Offset: 0x00087FA0
		internal Type LoadMemberAddress(MemberInfo memberInfo)
		{
			Type type;
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				type = fieldInfo.FieldType;
				if (fieldInfo.IsStatic)
				{
					this.ilGen.Emit(OpCodes.Ldsflda, fieldInfo);
				}
				else
				{
					this.ilGen.Emit(OpCodes.Ldflda, fieldInfo);
				}
			}
			else
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				type = propertyInfo.PropertyType;
				if (propertyInfo != null)
				{
					MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = CodeGenerator.GetPropertyMethodFromBaseType(propertyInfo, true);
					}
					this.Call(methodInfo);
					LocalBuilder tempLocal = this.GetTempLocal(type);
					this.Stloc(tempLocal);
					this.Ldloca(tempLocal);
				}
			}
			return type;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00089E48 File Offset: 0x00088048
		internal void StoreMember(MemberInfo memberInfo)
		{
			if (memberInfo.MemberType != MemberTypes.Field)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				if (propertyInfo != null)
				{
					MethodInfo methodInfo = propertyInfo.GetSetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = CodeGenerator.GetPropertyMethodFromBaseType(propertyInfo, false);
					}
					this.Call(methodInfo);
				}
				return;
			}
			FieldInfo fieldInfo = (FieldInfo)memberInfo;
			if (fieldInfo.IsStatic)
			{
				this.ilGen.Emit(OpCodes.Stsfld, fieldInfo);
				return;
			}
			this.ilGen.Emit(OpCodes.Stfld, fieldInfo);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00089EC4 File Offset: 0x000880C4
		internal void Load(object obj)
		{
			if (obj == null)
			{
				this.ilGen.Emit(OpCodes.Ldnull);
				return;
			}
			if (obj is ArgBuilder)
			{
				this.Ldarg((ArgBuilder)obj);
				return;
			}
			if (obj is LocalBuilder)
			{
				this.Ldloc((LocalBuilder)obj);
				return;
			}
			this.Ldc(obj);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00089F16 File Offset: 0x00088116
		internal void LoadAddress(object obj)
		{
			if (obj is ArgBuilder)
			{
				this.LdargAddress((ArgBuilder)obj);
				return;
			}
			if (obj is LocalBuilder)
			{
				this.LdlocAddress((LocalBuilder)obj);
				return;
			}
			this.Load(obj);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00089F49 File Offset: 0x00088149
		internal void ConvertAddress(Type source, Type target)
		{
			this.InternalConvert(source, target, true);
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00089F54 File Offset: 0x00088154
		internal void ConvertValue(Type source, Type target)
		{
			this.InternalConvert(source, target, false);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00089F5F File Offset: 0x0008815F
		internal void Castclass(Type target)
		{
			this.ilGen.Emit(OpCodes.Castclass, target);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x00089F72 File Offset: 0x00088172
		internal void Box(Type type)
		{
			this.ilGen.Emit(OpCodes.Box, type);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00089F85 File Offset: 0x00088185
		internal void Unbox(Type type)
		{
			this.ilGen.Emit(OpCodes.Unbox, type);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00089F98 File Offset: 0x00088198
		private OpCode GetLdindOpCode(TypeCode typeCode)
		{
			return CodeGenerator.LdindOpCodes[(int)typeCode];
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00089FA8 File Offset: 0x000881A8
		internal void Ldobj(Type type)
		{
			OpCode ldindOpCode = this.GetLdindOpCode(Type.GetTypeCode(type));
			if (!ldindOpCode.Equals(OpCodes.Nop))
			{
				this.ilGen.Emit(ldindOpCode);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldobj, type);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00089FEE File Offset: 0x000881EE
		internal void Stobj(Type type)
		{
			this.ilGen.Emit(OpCodes.Stobj, type);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0008A001 File Offset: 0x00088201
		internal void Ceq()
		{
			this.ilGen.Emit(OpCodes.Ceq);
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0008A013 File Offset: 0x00088213
		internal void Clt()
		{
			this.ilGen.Emit(OpCodes.Clt);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0008A025 File Offset: 0x00088225
		internal void Cne()
		{
			this.Ceq();
			this.Ldc(0);
			this.Ceq();
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0008A03A File Offset: 0x0008823A
		internal void Ble(Label label)
		{
			this.ilGen.Emit(OpCodes.Ble, label);
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0008A04D File Offset: 0x0008824D
		internal void Throw()
		{
			this.ilGen.Emit(OpCodes.Throw);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0008A05F File Offset: 0x0008825F
		internal void Ldtoken(Type t)
		{
			this.ilGen.Emit(OpCodes.Ldtoken, t);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0008A074 File Offset: 0x00088274
		internal void Ldc(object o)
		{
			Type type = o.GetType();
			if (o is Type)
			{
				this.Ldtoken((Type)o);
				this.Call(typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					typeof(RuntimeTypeHandle)
				}, null));
				return;
			}
			if (type.IsEnum)
			{
				this.Ldc(((IConvertible)o).ToType(Enum.GetUnderlyingType(type), null));
				return;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				this.Ldc((bool)o);
				return;
			case TypeCode.Char:
				throw new NotSupportedException("Char is not a valid schema primitive and should be treated as int in DataContract");
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
				this.Ldc(((IConvertible)o).ToInt32(CultureInfo.InvariantCulture));
				return;
			case TypeCode.Int32:
				this.Ldc((int)o);
				return;
			case TypeCode.UInt32:
				this.Ldc((int)((uint)o));
				return;
			case TypeCode.Int64:
				this.Ldc((long)o);
				return;
			case TypeCode.UInt64:
				this.Ldc((long)((ulong)o));
				return;
			case TypeCode.Single:
				this.Ldc((float)o);
				return;
			case TypeCode.Double:
				this.Ldc((double)o);
				return;
			case TypeCode.Decimal:
			{
				ConstructorInfo constructor = typeof(decimal).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(int),
					typeof(int),
					typeof(int),
					typeof(bool),
					typeof(byte)
				}, null);
				int[] bits = decimal.GetBits((decimal)o);
				this.Ldc(bits[0]);
				this.Ldc(bits[1]);
				this.Ldc(bits[2]);
				this.Ldc(((long)bits[3] & (long)((ulong)int.MinValue)) == (long)((ulong)int.MinValue));
				this.Ldc((int)((byte)(bits[3] >> 16 & 255)));
				this.New(constructor);
				return;
			}
			case TypeCode.DateTime:
			{
				ConstructorInfo constructor2 = typeof(DateTime).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(long)
				}, null);
				this.Ldc(((DateTime)o).Ticks);
				this.New(constructor2);
				return;
			}
			case TypeCode.String:
				this.Ldstr((string)o);
				return;
			}
			if (type == typeof(TimeSpan) && LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				ConstructorInfo constructor3 = typeof(TimeSpan).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(long)
				}, null);
				this.Ldc(((TimeSpan)o).Ticks);
				this.New(constructor3);
				return;
			}
			throw new NotSupportedException("UnknownConstantType");
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0008A345 File Offset: 0x00088545
		internal void Ldc(bool boolVar)
		{
			if (boolVar)
			{
				this.ilGen.Emit(OpCodes.Ldc_I4_1);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldc_I4_0);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0008A36C File Offset: 0x0008856C
		internal void Ldc(int intVar)
		{
			switch (intVar)
			{
			case -1:
				this.ilGen.Emit(OpCodes.Ldc_I4_M1);
				return;
			case 0:
				this.ilGen.Emit(OpCodes.Ldc_I4_0);
				return;
			case 1:
				this.ilGen.Emit(OpCodes.Ldc_I4_1);
				return;
			case 2:
				this.ilGen.Emit(OpCodes.Ldc_I4_2);
				return;
			case 3:
				this.ilGen.Emit(OpCodes.Ldc_I4_3);
				return;
			case 4:
				this.ilGen.Emit(OpCodes.Ldc_I4_4);
				return;
			case 5:
				this.ilGen.Emit(OpCodes.Ldc_I4_5);
				return;
			case 6:
				this.ilGen.Emit(OpCodes.Ldc_I4_6);
				return;
			case 7:
				this.ilGen.Emit(OpCodes.Ldc_I4_7);
				return;
			case 8:
				this.ilGen.Emit(OpCodes.Ldc_I4_8);
				return;
			default:
				this.ilGen.Emit(OpCodes.Ldc_I4, intVar);
				return;
			}
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0008A469 File Offset: 0x00088669
		internal void Ldc(long l)
		{
			this.ilGen.Emit(OpCodes.Ldc_I8, l);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0008A47C File Offset: 0x0008867C
		internal void Ldc(float f)
		{
			this.ilGen.Emit(OpCodes.Ldc_R4, f);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0008A48F File Offset: 0x0008868F
		internal void Ldc(double d)
		{
			this.ilGen.Emit(OpCodes.Ldc_R8, d);
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0008A4A2 File Offset: 0x000886A2
		internal void Ldstr(string strVar)
		{
			if (strVar == null)
			{
				this.ilGen.Emit(OpCodes.Ldnull);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldstr, strVar);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0008A4C9 File Offset: 0x000886C9
		internal void LdlocAddress(LocalBuilder localBuilder)
		{
			if (localBuilder.LocalType.IsValueType)
			{
				this.Ldloca(localBuilder);
				return;
			}
			this.Ldloc(localBuilder);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0008A4E7 File Offset: 0x000886E7
		internal void Ldloc(LocalBuilder localBuilder)
		{
			this.ilGen.Emit(OpCodes.Ldloc, localBuilder);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0008A4FC File Offset: 0x000886FC
		internal void Ldloc(string name)
		{
			LocalBuilder localBuilder = this.currentScope[name];
			this.Ldloc(localBuilder);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0008A520 File Offset: 0x00088720
		internal void Stloc(Type type, string name)
		{
			LocalBuilder local = null;
			if (!this.currentScope.TryGetValue(name, out local))
			{
				local = this.DeclareLocal(type, name);
			}
			this.Stloc(local);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0008A54F File Offset: 0x0008874F
		internal void Stloc(LocalBuilder local)
		{
			this.ilGen.Emit(OpCodes.Stloc, local);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0008A564 File Offset: 0x00088764
		internal void Ldloc(Type type, string name)
		{
			LocalBuilder localBuilder = this.currentScope[name];
			this.Ldloc(localBuilder);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0008A585 File Offset: 0x00088785
		internal void Ldloca(LocalBuilder localBuilder)
		{
			this.ilGen.Emit(OpCodes.Ldloca, localBuilder);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0008A598 File Offset: 0x00088798
		internal void LdargAddress(ArgBuilder argBuilder)
		{
			if (argBuilder.ArgType.IsValueType)
			{
				this.Ldarga(argBuilder);
				return;
			}
			this.Ldarg(argBuilder);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0008A5B6 File Offset: 0x000887B6
		internal void Ldarg(string arg)
		{
			this.Ldarg(this.GetArg(arg));
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0008A5C5 File Offset: 0x000887C5
		internal void Ldarg(ArgBuilder arg)
		{
			this.Ldarg(arg.Index);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0008A5D4 File Offset: 0x000887D4
		internal void Ldarg(int slot)
		{
			switch (slot)
			{
			case 0:
				this.ilGen.Emit(OpCodes.Ldarg_0);
				return;
			case 1:
				this.ilGen.Emit(OpCodes.Ldarg_1);
				return;
			case 2:
				this.ilGen.Emit(OpCodes.Ldarg_2);
				return;
			case 3:
				this.ilGen.Emit(OpCodes.Ldarg_3);
				return;
			default:
				if (slot <= 255)
				{
					this.ilGen.Emit(OpCodes.Ldarg_S, slot);
					return;
				}
				this.ilGen.Emit(OpCodes.Ldarg, slot);
				return;
			}
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0008A668 File Offset: 0x00088868
		internal void Ldarga(ArgBuilder argBuilder)
		{
			this.Ldarga(argBuilder.Index);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0008A676 File Offset: 0x00088876
		internal void Ldarga(int slot)
		{
			if (slot <= 255)
			{
				this.ilGen.Emit(OpCodes.Ldarga_S, slot);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldarga, slot);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0008A6A3 File Offset: 0x000888A3
		internal void Ldlen()
		{
			this.ilGen.Emit(OpCodes.Ldlen);
			this.ilGen.Emit(OpCodes.Conv_I4);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0008A6C5 File Offset: 0x000888C5
		private OpCode GetLdelemOpCode(TypeCode typeCode)
		{
			return CodeGenerator.LdelemOpCodes[(int)typeCode];
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0008A6D4 File Offset: 0x000888D4
		internal void Ldelem(Type arrayElementType)
		{
			if (arrayElementType.IsEnum)
			{
				this.Ldelem(Enum.GetUnderlyingType(arrayElementType));
				return;
			}
			OpCode ldelemOpCode = this.GetLdelemOpCode(Type.GetTypeCode(arrayElementType));
			if (ldelemOpCode.Equals(OpCodes.Nop))
			{
				throw new InvalidOperationException("ArrayTypeIsNotSupported");
			}
			this.ilGen.Emit(ldelemOpCode);
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0008A728 File Offset: 0x00088928
		internal void Ldelema(Type arrayElementType)
		{
			OpCode ldelema = OpCodes.Ldelema;
			this.ilGen.Emit(ldelema, arrayElementType);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0008A748 File Offset: 0x00088948
		private OpCode GetStelemOpCode(TypeCode typeCode)
		{
			return CodeGenerator.StelemOpCodes[(int)typeCode];
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0008A758 File Offset: 0x00088958
		internal void Stelem(Type arrayElementType)
		{
			if (arrayElementType.IsEnum)
			{
				this.Stelem(Enum.GetUnderlyingType(arrayElementType));
				return;
			}
			OpCode stelemOpCode = this.GetStelemOpCode(Type.GetTypeCode(arrayElementType));
			if (stelemOpCode.Equals(OpCodes.Nop))
			{
				throw new InvalidOperationException("ArrayTypeIsNotSupported");
			}
			this.ilGen.Emit(stelemOpCode);
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0008A7AC File Offset: 0x000889AC
		internal Label DefineLabel()
		{
			return this.ilGen.DefineLabel();
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0008A7B9 File Offset: 0x000889B9
		internal void MarkLabel(Label label)
		{
			this.ilGen.MarkLabel(label);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0008A7C7 File Offset: 0x000889C7
		internal void Nop()
		{
			this.ilGen.Emit(OpCodes.Nop);
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0008A7D9 File Offset: 0x000889D9
		internal void Add()
		{
			this.ilGen.Emit(OpCodes.Add);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0008A7EB File Offset: 0x000889EB
		internal void Ret()
		{
			this.ilGen.Emit(OpCodes.Ret);
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0008A7FD File Offset: 0x000889FD
		internal void Br(Label label)
		{
			this.ilGen.Emit(OpCodes.Br, label);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0008A810 File Offset: 0x00088A10
		internal void Br_S(Label label)
		{
			this.ilGen.Emit(OpCodes.Br_S, label);
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0008A823 File Offset: 0x00088A23
		internal void Blt(Label label)
		{
			this.ilGen.Emit(OpCodes.Blt, label);
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0008A836 File Offset: 0x00088A36
		internal void Brfalse(Label label)
		{
			this.ilGen.Emit(OpCodes.Brfalse, label);
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0008A849 File Offset: 0x00088A49
		internal void Brtrue(Label label)
		{
			this.ilGen.Emit(OpCodes.Brtrue, label);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0008A85C File Offset: 0x00088A5C
		internal void Pop()
		{
			this.ilGen.Emit(OpCodes.Pop);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0008A86E File Offset: 0x00088A6E
		internal void Dup()
		{
			this.ilGen.Emit(OpCodes.Dup);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0008A880 File Offset: 0x00088A80
		internal void Ldftn(MethodInfo methodInfo)
		{
			this.ilGen.Emit(OpCodes.Ldftn, methodInfo);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0008A894 File Offset: 0x00088A94
		private void InternalIf(bool negate)
		{
			IfState ifState = new IfState();
			ifState.EndIf = this.DefineLabel();
			ifState.ElseBegin = this.DefineLabel();
			if (negate)
			{
				this.Brtrue(ifState.ElseBegin);
			}
			else
			{
				this.Brfalse(ifState.ElseBegin);
			}
			this.blockStack.Push(ifState);
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0008A8E8 File Offset: 0x00088AE8
		private OpCode GetConvOpCode(TypeCode typeCode)
		{
			return CodeGenerator.ConvOpCodes[(int)typeCode];
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0008A8F8 File Offset: 0x00088AF8
		private void InternalConvert(Type source, Type target, bool isAddress)
		{
			if (target == source)
			{
				return;
			}
			if (target.IsValueType)
			{
				if (source.IsValueType)
				{
					OpCode convOpCode = this.GetConvOpCode(Type.GetTypeCode(target));
					if (convOpCode.Equals(OpCodes.Nop))
					{
						throw new CodeGeneratorConversionException(source, target, isAddress, "NoConversionPossibleTo");
					}
					this.ilGen.Emit(convOpCode);
					return;
				}
				else
				{
					if (!source.IsAssignableFrom(target))
					{
						throw new CodeGeneratorConversionException(source, target, isAddress, "IsNotAssignableFrom");
					}
					this.Unbox(target);
					if (!isAddress)
					{
						this.Ldobj(target);
						return;
					}
				}
			}
			else if (target.IsAssignableFrom(source))
			{
				if (source.IsValueType)
				{
					if (isAddress)
					{
						this.Ldobj(source);
					}
					this.Box(source);
					return;
				}
			}
			else
			{
				if (source.IsAssignableFrom(target))
				{
					this.Castclass(target);
					return;
				}
				if (target.IsInterface || source.IsInterface)
				{
					this.Castclass(target);
					return;
				}
				throw new CodeGeneratorConversionException(source, target, isAddress, "IsNotAssignableFrom");
			}
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0008A9D8 File Offset: 0x00088BD8
		private IfState PopIfState()
		{
			return this.blockStack.Pop() as IfState;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0008A9EC File Offset: 0x00088BEC
		internal static AssemblyBuilder CreateAssemblyBuilder(AppDomain appDomain, string name)
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = name;
			assemblyName.Version = new Version(1, 0, 0, 0);
			if (DiagnosticsSwitches.KeepTempFiles.Enabled)
			{
				return appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave, CodeGenerator.TempFilesLocation);
			}
			return appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x0008AA38 File Offset: 0x00088C38
		// (set) Token: 0x06001792 RID: 6034 RVA: 0x0008AA8C File Offset: 0x00088C8C
		internal static string TempFilesLocation
		{
			get
			{
				if (CodeGenerator.tempFilesLocation == null)
				{
					object section = ConfigurationManager.GetSection(ConfigurationStrings.XmlSerializerSectionPath);
					string text = null;
					if (section != null)
					{
						XmlSerializerSection xmlSerializerSection = section as XmlSerializerSection;
						if (xmlSerializerSection != null)
						{
							text = xmlSerializerSection.TempFilesLocation;
						}
					}
					if (text != null)
					{
						CodeGenerator.tempFilesLocation = text.Trim();
					}
					else
					{
						CodeGenerator.tempFilesLocation = Path.GetTempPath();
					}
				}
				return CodeGenerator.tempFilesLocation;
			}
			set
			{
				CodeGenerator.tempFilesLocation = value;
			}
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0008AA94 File Offset: 0x00088C94
		internal static ModuleBuilder CreateModuleBuilder(AssemblyBuilder assemblyBuilder, string name)
		{
			if (DiagnosticsSwitches.KeepTempFiles.Enabled)
			{
				return assemblyBuilder.DefineDynamicModule(name, name + ".dll", true);
			}
			return assemblyBuilder.DefineDynamicModule(name);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0008AABD File Offset: 0x00088CBD
		internal static TypeBuilder CreateTypeBuilder(ModuleBuilder moduleBuilder, string name, TypeAttributes attributes, Type parent, Type[] interfaces)
		{
			return moduleBuilder.DefineType("Microsoft.Xml.Serialization.GeneratedAssembly." + name, attributes, parent, interfaces);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0008AAD4 File Offset: 0x00088CD4
		internal void InitElseIf()
		{
			this.elseIfState = (IfState)this.blockStack.Pop();
			this.initElseIfStack = this.blockStack.Count;
			this.Br(this.elseIfState.EndIf);
			this.MarkLabel(this.elseIfState.ElseBegin);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0008AB2A File Offset: 0x00088D2A
		internal void InitIf()
		{
			this.initIfStack = this.blockStack.Count;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0008AB40 File Offset: 0x00088D40
		internal void AndIf(Cmp cmpOp)
		{
			if (this.initIfStack == this.blockStack.Count)
			{
				this.initIfStack = -1;
				this.If(cmpOp);
				return;
			}
			if (this.initElseIfStack == this.blockStack.Count)
			{
				this.initElseIfStack = -1;
				this.elseIfState.ElseBegin = this.DefineLabel();
				this.ilGen.Emit(this.GetBranchCode(cmpOp), this.elseIfState.ElseBegin);
				this.blockStack.Push(this.elseIfState);
				return;
			}
			IfState ifState = (IfState)this.blockStack.Peek();
			this.ilGen.Emit(this.GetBranchCode(cmpOp), ifState.ElseBegin);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0008ABF4 File Offset: 0x00088DF4
		internal void AndIf()
		{
			if (this.initIfStack == this.blockStack.Count)
			{
				this.initIfStack = -1;
				this.If();
				return;
			}
			if (this.initElseIfStack == this.blockStack.Count)
			{
				this.initElseIfStack = -1;
				this.elseIfState.ElseBegin = this.DefineLabel();
				this.Brfalse(this.elseIfState.ElseBegin);
				this.blockStack.Push(this.elseIfState);
				return;
			}
			IfState ifState = (IfState)this.blockStack.Peek();
			this.Brfalse(ifState.ElseBegin);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0008AC8D File Offset: 0x00088E8D
		internal void IsInst(Type type)
		{
			this.ilGen.Emit(OpCodes.Isinst, type);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0008ACA0 File Offset: 0x00088EA0
		internal void Beq(Label label)
		{
			this.ilGen.Emit(OpCodes.Beq, label);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0008ACB3 File Offset: 0x00088EB3
		internal void Bne(Label label)
		{
			this.ilGen.Emit(OpCodes.Bne_Un, label);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0008ACC6 File Offset: 0x00088EC6
		internal void GotoMethodEnd()
		{
			this.Br(this.methodEndLabel);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0008ACD4 File Offset: 0x00088ED4
		internal void WhileBegin()
		{
			CodeGenerator.WhileState whileState = new CodeGenerator.WhileState(this);
			this.Br(whileState.CondLabel);
			this.MarkLabel(whileState.StartLabel);
			this.whileStack.Push(whileState);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0008AD0C File Offset: 0x00088F0C
		internal void WhileEnd()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Pop();
			this.MarkLabel(whileState.EndLabel);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0008AD38 File Offset: 0x00088F38
		internal void WhileBreak()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Br(whileState.EndLabel);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0008AD64 File Offset: 0x00088F64
		internal void WhileContinue()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Br(whileState.CondLabel);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0008AD90 File Offset: 0x00088F90
		internal void WhileBeginCondition()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Nop();
			this.MarkLabel(whileState.CondLabel);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0008ADC0 File Offset: 0x00088FC0
		internal void WhileEndCondition()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Brtrue(whileState.StartLabel);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0008ADEC File Offset: 0x00088FEC
		// Note: this type is marked as 'beforefieldinit'.
		static CodeGenerator()
		{
		}

		// Token: 0x04001849 RID: 6217
		internal static BindingFlags InstancePublicBindingFlags = BindingFlags.Instance | BindingFlags.Public;

		// Token: 0x0400184A RID: 6218
		internal static BindingFlags InstanceBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x0400184B RID: 6219
		internal static BindingFlags StaticBindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x0400184C RID: 6220
		internal static MethodAttributes PublicMethodAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig;

		// Token: 0x0400184D RID: 6221
		internal static MethodAttributes PublicOverrideMethodAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig;

		// Token: 0x0400184E RID: 6222
		internal static MethodAttributes ProtectedOverrideMethodAttributes = MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig;

		// Token: 0x0400184F RID: 6223
		internal static MethodAttributes PrivateMethodAttributes = MethodAttributes.Private | MethodAttributes.HideBySig;

		// Token: 0x04001850 RID: 6224
		internal static Type[] EmptyTypeArray = new Type[0];

		// Token: 0x04001851 RID: 6225
		internal static string[] EmptyStringArray = new string[0];

		// Token: 0x04001852 RID: 6226
		private TypeBuilder typeBuilder;

		// Token: 0x04001853 RID: 6227
		private MethodBuilder methodBuilder;

		// Token: 0x04001854 RID: 6228
		private ILGenerator ilGen;

		// Token: 0x04001855 RID: 6229
		private Dictionary<string, ArgBuilder> argList;

		// Token: 0x04001856 RID: 6230
		private LocalScope currentScope;

		// Token: 0x04001857 RID: 6231
		private Dictionary<Tuple<Type, string>, Queue<LocalBuilder>> freeLocals;

		// Token: 0x04001858 RID: 6232
		private Stack blockStack;

		// Token: 0x04001859 RID: 6233
		private Label methodEndLabel;

		// Token: 0x0400185A RID: 6234
		internal LocalBuilder retLocal;

		// Token: 0x0400185B RID: 6235
		internal Label retLabel;

		// Token: 0x0400185C RID: 6236
		private Dictionary<Type, LocalBuilder> TmpLocals = new Dictionary<Type, LocalBuilder>();

		// Token: 0x0400185D RID: 6237
		private static OpCode[] BranchCodes = new OpCode[]
		{
			OpCodes.Bge,
			OpCodes.Bne_Un,
			OpCodes.Bgt,
			OpCodes.Ble,
			OpCodes.Beq,
			OpCodes.Blt
		};

		// Token: 0x0400185E RID: 6238
		private Stack leaveLabels = new Stack();

		// Token: 0x0400185F RID: 6239
		private static OpCode[] LdindOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Ldind_I1,
			OpCodes.Ldind_I2,
			OpCodes.Ldind_I1,
			OpCodes.Ldind_U1,
			OpCodes.Ldind_I2,
			OpCodes.Ldind_U2,
			OpCodes.Ldind_I4,
			OpCodes.Ldind_U4,
			OpCodes.Ldind_I8,
			OpCodes.Ldind_I8,
			OpCodes.Ldind_R4,
			OpCodes.Ldind_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Ldind_Ref
		};

		// Token: 0x04001860 RID: 6240
		private static OpCode[] LdelemOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Ldelem_Ref,
			OpCodes.Ldelem_Ref,
			OpCodes.Ldelem_I1,
			OpCodes.Ldelem_I2,
			OpCodes.Ldelem_I1,
			OpCodes.Ldelem_U1,
			OpCodes.Ldelem_I2,
			OpCodes.Ldelem_U2,
			OpCodes.Ldelem_I4,
			OpCodes.Ldelem_U4,
			OpCodes.Ldelem_I8,
			OpCodes.Ldelem_I8,
			OpCodes.Ldelem_R4,
			OpCodes.Ldelem_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Ldelem_Ref
		};

		// Token: 0x04001861 RID: 6241
		private static OpCode[] StelemOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Stelem_Ref,
			OpCodes.Stelem_Ref,
			OpCodes.Stelem_I1,
			OpCodes.Stelem_I2,
			OpCodes.Stelem_I1,
			OpCodes.Stelem_I1,
			OpCodes.Stelem_I2,
			OpCodes.Stelem_I2,
			OpCodes.Stelem_I4,
			OpCodes.Stelem_I4,
			OpCodes.Stelem_I8,
			OpCodes.Stelem_I8,
			OpCodes.Stelem_R4,
			OpCodes.Stelem_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Stelem_Ref
		};

		// Token: 0x04001862 RID: 6242
		private static OpCode[] ConvOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Conv_I1,
			OpCodes.Conv_I2,
			OpCodes.Conv_I1,
			OpCodes.Conv_U1,
			OpCodes.Conv_I2,
			OpCodes.Conv_U2,
			OpCodes.Conv_I4,
			OpCodes.Conv_U4,
			OpCodes.Conv_I8,
			OpCodes.Conv_U8,
			OpCodes.Conv_R4,
			OpCodes.Conv_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop
		};

		// Token: 0x04001863 RID: 6243
		private static string tempFilesLocation = null;

		// Token: 0x04001864 RID: 6244
		private int initElseIfStack = -1;

		// Token: 0x04001865 RID: 6245
		private IfState elseIfState;

		// Token: 0x04001866 RID: 6246
		private int initIfStack = -1;

		// Token: 0x04001867 RID: 6247
		private Stack whileStack;

		// Token: 0x02000269 RID: 617
		internal class WhileState
		{
			// Token: 0x060017A4 RID: 6052 RVA: 0x0008B28D File Offset: 0x0008948D
			public WhileState(CodeGenerator ilg)
			{
				this.StartLabel = ilg.DefineLabel();
				this.CondLabel = ilg.DefineLabel();
				this.EndLabel = ilg.DefineLabel();
			}

			// Token: 0x04001868 RID: 6248
			public Label StartLabel;

			// Token: 0x04001869 RID: 6249
			public Label CondLabel;

			// Token: 0x0400186A RID: 6250
			public Label EndLabel;
		}
	}
}

using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x020002FF RID: 767
	internal class ReflectionAwareILGen
	{
		// Token: 0x06001FC6 RID: 8134 RVA: 0x0000216B File Offset: 0x0000036B
		internal ReflectionAwareILGen()
		{
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x000CEDE8 File Offset: 0x000CCFE8
		internal void WriteReflectionInit(TypeScope scope)
		{
			foreach (object obj in scope.Types)
			{
				Type type = (Type)obj;
				scope.GetTypeDesc(type);
			}
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x000CEE44 File Offset: 0x000CD044
		internal void ILGenForEnumLongValue(CodeGenerator ilg, string variable)
		{
			ArgBuilder arg = ilg.GetArg(variable);
			ilg.Ldarg(arg);
			ilg.ConvertValue(arg.ArgType, typeof(long));
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x000CEE76 File Offset: 0x000CD076
		internal string GetStringForTypeof(string typeFullName)
		{
			return "typeof(" + typeFullName + ")";
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x000CEE88 File Offset: 0x000CD088
		internal string GetStringForMember(string obj, string memberName, TypeDesc typeDesc)
		{
			return obj + ".@" + memberName;
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x000CEE96 File Offset: 0x000CD096
		internal SourceInfo GetSourceForMember(string obj, MemberMapping member, TypeDesc typeDesc, CodeGenerator ilg)
		{
			return this.GetSourceForMember(obj, member, member.MemberInfo, typeDesc, ilg);
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x000CEEA9 File Offset: 0x000CD0A9
		internal SourceInfo GetSourceForMember(string obj, MemberMapping member, MemberInfo memberInfo, TypeDesc typeDesc, CodeGenerator ilg)
		{
			return new SourceInfo(this.GetStringForMember(obj, member.Name, typeDesc), obj, memberInfo, member.TypeDesc.Type, ilg);
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x000CEECE File Offset: 0x000CD0CE
		internal void ILGenForEnumMember(CodeGenerator ilg, Type type, string memberName)
		{
			ilg.Ldc(Enum.Parse(type, memberName, false));
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x000CEEDE File Offset: 0x000CD0DE
		internal string GetStringForArrayMember(string arrayName, string subscript, TypeDesc arrayTypeDesc)
		{
			return arrayName + "[" + subscript + "]";
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x000CEEF1 File Offset: 0x000CD0F1
		internal string GetStringForMethod(string obj, string typeFullName, string memberName)
		{
			return obj + "." + memberName + "(";
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x000CEF04 File Offset: 0x000CD104
		internal void ILGenForCreateInstance(CodeGenerator ilg, Type type, bool ctorInaccessible, bool cast)
		{
			if (ctorInaccessible)
			{
				this.ILGenForCreateInstance(ilg, type, cast ? type : null, ctorInaccessible);
				return;
			}
			ConstructorInfo constructor = type.GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			if (constructor != null)
			{
				ilg.New(constructor);
				return;
			}
			LocalBuilder tempLocal = ilg.GetTempLocal(type);
			ilg.Ldloca(tempLocal);
			ilg.InitObj(type);
			ilg.Ldloc(tempLocal);
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x000CEF68 File Offset: 0x000CD168
		internal void ILGenForCreateInstance(CodeGenerator ilg, Type type, Type cast, bool nonPublic)
		{
			if (type == typeof(DBNull))
			{
				FieldInfo field = typeof(DBNull).GetField("Value", CodeGenerator.StaticBindingFlags);
				ilg.LoadMember(field);
				return;
			}
			if (type.FullName == "System.Xml.Linq.XElement")
			{
				Type type2 = type.Assembly.GetType("System.Xml.Linq.XName");
				if (type2 != null)
				{
					MethodInfo method = type2.GetMethod("op_Implicit", CodeGenerator.StaticBindingFlags, null, new Type[]
					{
						typeof(string)
					}, null);
					ConstructorInfo constructor = type.GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
					{
						type2
					}, null);
					if (method != null && constructor != null)
					{
						ilg.Ldstr("default");
						ilg.Call(method);
						ilg.New(constructor);
						return;
					}
				}
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
			if (nonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			MethodInfo method2 = typeof(Activator).GetMethod("CreateInstance", CodeGenerator.StaticBindingFlags, null, new Type[]
			{
				typeof(Type),
				typeof(BindingFlags),
				typeof(Binder),
				typeof(object[]),
				typeof(CultureInfo)
			}, null);
			ilg.Ldc(type);
			ilg.Load((int)bindingFlags);
			ilg.Load(null);
			ilg.NewArray(typeof(object), 0);
			ilg.Load(null);
			ilg.Call(method2);
			if (cast != null)
			{
				ilg.ConvertValue(method2.ReturnType, cast);
			}
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x000CF110 File Offset: 0x000CD310
		internal void WriteLocalDecl(string variableName, SourceInfo initValue)
		{
			Type type = initValue.Type;
			LocalBuilder localBuilder = initValue.ILG.DeclareOrGetLocal(type, variableName);
			if (initValue.Source != null)
			{
				if (initValue == "null")
				{
					initValue.ILG.Load(null);
				}
				else if (initValue.Arg.StartsWith("o.@", StringComparison.Ordinal))
				{
					initValue.ILG.LoadMember(initValue.ILG.GetLocal("o"), initValue.MemberInfo);
				}
				else if (initValue.Source.EndsWith("]", StringComparison.Ordinal))
				{
					initValue.Load(initValue.Type);
				}
				else if (initValue.Source == "fixup.Source" || initValue.Source == "e.Current")
				{
					string[] array = initValue.Source.Split('.', StringSplitOptions.None);
					object variable = initValue.ILG.GetVariable(array[0]);
					PropertyInfo property = initValue.ILG.GetVariableType(variable).GetProperty(array[1]);
					initValue.ILG.LoadMember(variable, property);
					initValue.ILG.ConvertValue(property.PropertyType, localBuilder.LocalType);
				}
				else
				{
					object variable2 = initValue.ILG.GetVariable(initValue.Arg);
					initValue.ILG.Load(variable2);
					initValue.ILG.ConvertValue(initValue.ILG.GetVariableType(variable2), localBuilder.LocalType);
				}
				initValue.ILG.Stloc(localBuilder);
			}
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x000CF28C File Offset: 0x000CD48C
		internal void WriteCreateInstance(string source, bool ctorInaccessible, Type type, CodeGenerator ilg)
		{
			LocalBuilder local = ilg.DeclareOrGetLocal(type, source);
			this.ILGenForCreateInstance(ilg, type, ctorInaccessible, ctorInaccessible);
			ilg.Stloc(local);
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x000CF2B6 File Offset: 0x000CD4B6
		internal void WriteInstanceOf(SourceInfo source, Type type, CodeGenerator ilg)
		{
			source.Load(typeof(object));
			ilg.IsInst(type);
			ilg.Load(null);
			ilg.Cne();
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000CF2DC File Offset: 0x000CD4DC
		internal void WriteArrayLocalDecl(string typeName, string variableName, SourceInfo initValue, TypeDesc arrayTypeDesc)
		{
			Type type = (typeName == arrayTypeDesc.CSharpName) ? arrayTypeDesc.Type : arrayTypeDesc.Type.MakeArrayType();
			LocalBuilder localBuilder = initValue.ILG.DeclareOrGetLocal(type, variableName);
			if (initValue != null)
			{
				initValue.Load(localBuilder.LocalType);
				initValue.ILG.Stloc(localBuilder);
			}
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x000CF33D File Offset: 0x000CD53D
		internal void WriteTypeCompare(string variable, Type type, CodeGenerator ilg)
		{
			ilg.Ldloc(typeof(Type), variable);
			ilg.Ldc(type);
			ilg.Ceq();
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x000CF33D File Offset: 0x000CD53D
		internal void WriteArrayTypeCompare(string variable, Type arrayType, CodeGenerator ilg)
		{
			ilg.Ldloc(typeof(Type), variable);
			ilg.Ldc(arrayType);
			ilg.Ceq();
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x000CF35D File Offset: 0x000CD55D
		internal static string GetQuotedCSharpString(IndentedWriter notUsed, string value)
		{
			if (value == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("@\"");
			stringBuilder.Append(ReflectionAwareILGen.GetCSharpString(value));
			stringBuilder.Append("\"");
			return stringBuilder.ToString();
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x000CF394 File Offset: 0x000CD594
		internal static string GetCSharpString(string value)
		{
			if (value == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (c < ' ')
				{
					if (c == '\r')
					{
						stringBuilder.Append("\\r");
					}
					else if (c == '\n')
					{
						stringBuilder.Append("\\n");
					}
					else if (c == '\t')
					{
						stringBuilder.Append("\\t");
					}
					else
					{
						byte b = (byte)c;
						stringBuilder.Append("\\x");
						stringBuilder.Append("0123456789ABCDEF"[b >> 4]);
						stringBuilder.Append("0123456789ABCDEF"[(int)(b & 15)]);
					}
				}
				else if (c == '"')
				{
					stringBuilder.Append("\"\"");
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001B0D RID: 6925
		private const string hexDigits = "0123456789ABCDEF";

		// Token: 0x04001B0E RID: 6926
		private const string arrayMemberKey = "0";
	}
}

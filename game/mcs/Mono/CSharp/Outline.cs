using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x02000264 RID: 612
	public class Outline
	{
		// Token: 0x06001E21 RID: 7713 RVA: 0x0009370C File Offset: 0x0009190C
		public Outline(Type t, TextWriter output, bool declared_only, bool show_private, bool filter_obsolete)
		{
			this.t = t;
			this.o = new IndentedTextWriter(output, "\t");
			this.declared_only = declared_only;
			this.show_private = show_private;
			this.filter_obsolete = filter_obsolete;
			this.type_multicast_delegate = typeof(MulticastDelegate);
			this.type_object = typeof(object);
			this.type_value_type = typeof(ValueType);
			this.type_int = typeof(int);
			this.type_flags_attribute = typeof(FlagsAttribute);
			this.type_obsolete_attribute = typeof(ObsoleteAttribute);
			this.type_param_array_attribute = typeof(ParamArrayAttribute);
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000937C0 File Offset: 0x000919C0
		public void OutlineType()
		{
			this.OutlineAttributes();
			this.o.Write(Outline.GetTypeVisibility(this.t));
			if (this.t.IsClass && !this.t.IsSubclassOf(this.type_multicast_delegate))
			{
				if (this.t.IsSealed)
				{
					this.o.Write(this.t.IsAbstract ? " static" : " sealed");
				}
				else if (this.t.IsAbstract)
				{
					this.o.Write(" abstract");
				}
			}
			this.o.Write(" ");
			this.o.Write(this.GetTypeKind(this.t));
			this.o.Write(" ");
			Type[] array = (Type[])Comparer.Sort(Outline.TypeGetInterfaces(this.t, this.declared_only));
			Type baseType = this.t.BaseType;
			if (this.t.IsSubclassOf(this.type_multicast_delegate))
			{
				MethodInfo method = this.t.GetMethod("Invoke");
				this.o.Write(this.FormatType(method.ReturnType));
				this.o.Write(" ");
				this.o.Write(this.GetTypeName(this.t));
				this.o.Write(" (");
				this.OutlineParams(method.GetParameters());
				this.o.Write(")");
				this.WriteGenericConstraints(this.t.GetGenericArguments());
				this.o.WriteLine(";");
				return;
			}
			this.o.Write(this.GetTypeName(this.t));
			bool flag;
			if (((baseType != null && baseType != this.type_object && baseType != this.type_value_type) || array.Length != 0) && !this.t.IsEnum)
			{
				flag = true;
				this.o.Write(" : ");
				if (baseType != null && baseType != this.type_object && baseType != this.type_value_type)
				{
					this.o.Write(this.FormatType(baseType));
					flag = false;
				}
				foreach (Type type in array)
				{
					if (!flag)
					{
						this.o.Write(", ");
					}
					flag = false;
					this.o.Write(this.FormatType(type));
				}
			}
			bool isEnum = this.t.IsEnum;
			this.WriteGenericConstraints(this.t.GetGenericArguments());
			this.o.WriteLine(" {");
			IndentedTextWriter indentedTextWriter = this.o;
			int i = indentedTextWriter.Indent;
			indentedTextWriter.Indent = i + 1;
			if (this.t.IsEnum)
			{
				bool flag2 = true;
				foreach (FieldInfo fieldInfo in this.t.GetFields(BindingFlags.Static | BindingFlags.Public))
				{
					if (!flag2)
					{
						this.o.WriteLine(",");
					}
					flag2 = false;
					this.o.Write(fieldInfo.Name);
				}
				this.o.WriteLine();
				IndentedTextWriter indentedTextWriter2 = this.o;
				i = indentedTextWriter2.Indent;
				indentedTextWriter2.Indent = i - 1;
				this.o.WriteLine("}");
				return;
			}
			flag = true;
			foreach (ConstructorInfo constructorInfo in this.t.GetConstructors(this.DefaultFlags))
			{
				if (this.ShowMember(constructorInfo))
				{
					if (flag)
					{
						this.o.WriteLine();
					}
					flag = false;
					this.OutlineMemberAttribute(constructorInfo);
					this.OutlineConstructor(constructorInfo);
					this.o.WriteLine();
				}
			}
			flag = true;
			foreach (MethodInfo methodInfo in Comparer.Sort(this.t.GetMethods(this.DefaultFlags)))
			{
				if (this.ShowMember(methodInfo) && (methodInfo.Attributes & MethodAttributes.SpecialName) == MethodAttributes.PrivateScope)
				{
					if (flag)
					{
						this.o.WriteLine();
					}
					flag = false;
					this.OutlineMemberAttribute(methodInfo);
					this.OutlineMethod(methodInfo);
					this.o.WriteLine();
				}
			}
			flag = true;
			foreach (MethodInfo methodInfo2 in this.t.GetMethods(this.DefaultFlags))
			{
				if (this.ShowMember(methodInfo2) && (methodInfo2.Attributes & MethodAttributes.SpecialName) != MethodAttributes.PrivateScope && methodInfo2.Name.StartsWith("op_"))
				{
					if (flag)
					{
						this.o.WriteLine();
					}
					flag = false;
					this.OutlineMemberAttribute(methodInfo2);
					this.OutlineOperator(methodInfo2);
					this.o.WriteLine();
				}
			}
			flag = true;
			foreach (PropertyInfo propertyInfo in Comparer.Sort(this.t.GetProperties(this.DefaultFlags)))
			{
				if ((propertyInfo.CanRead && this.ShowMember(propertyInfo.GetGetMethod(true))) || (propertyInfo.CanWrite && this.ShowMember(propertyInfo.GetSetMethod(true))))
				{
					if (flag)
					{
						this.o.WriteLine();
					}
					flag = false;
					this.OutlineMemberAttribute(propertyInfo);
					this.OutlineProperty(propertyInfo);
					this.o.WriteLine();
				}
			}
			flag = true;
			foreach (FieldInfo fieldInfo2 in this.t.GetFields(this.DefaultFlags))
			{
				if (this.ShowMember(fieldInfo2))
				{
					if (flag)
					{
						this.o.WriteLine();
					}
					flag = false;
					this.OutlineMemberAttribute(fieldInfo2);
					this.OutlineField(fieldInfo2);
					this.o.WriteLine();
				}
			}
			flag = true;
			foreach (EventInfo eventInfo in Comparer.Sort(this.t.GetEvents(this.DefaultFlags)))
			{
				if (this.ShowMember(eventInfo.GetAddMethod(true)))
				{
					if (flag)
					{
						this.o.WriteLine();
					}
					flag = false;
					this.OutlineMemberAttribute(eventInfo);
					this.OutlineEvent(eventInfo);
					this.o.WriteLine();
				}
			}
			flag = true;
			foreach (Type mi in Comparer.Sort(this.t.GetNestedTypes(this.DefaultFlags)))
			{
				if (this.ShowMember(mi))
				{
					if (flag)
					{
						this.o.WriteLine();
					}
					flag = false;
					new Outline(mi, this.o, this.declared_only, this.show_private, this.filter_obsolete).OutlineType();
				}
			}
			IndentedTextWriter indentedTextWriter3 = this.o;
			i = indentedTextWriter3.Indent;
			indentedTextWriter3.Indent = i - 1;
			this.o.WriteLine("}");
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x00093E74 File Offset: 0x00092074
		private BindingFlags DefaultFlags
		{
			get
			{
				BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
				if (this.declared_only)
				{
					bindingFlags |= BindingFlags.DeclaredOnly;
				}
				return bindingFlags;
			}
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x00093E94 File Offset: 0x00092094
		private void OutlineAttributes()
		{
			if (this.t.IsSerializable)
			{
				this.o.WriteLine("[Serializable]");
			}
			if (this.t.IsDefined(this.type_flags_attribute, true))
			{
				this.o.WriteLine("[Flags]");
			}
			if (this.t.IsDefined(this.type_obsolete_attribute, true))
			{
				this.o.WriteLine("[Obsolete]");
			}
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0000AF70 File Offset: 0x00009170
		private void OutlineMemberAttribute(MemberInfo mi)
		{
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00093F08 File Offset: 0x00092108
		private void OutlineEvent(EventInfo ei)
		{
			MethodBase addMethod = ei.GetAddMethod(true);
			this.o.Write(Outline.GetMethodVisibility(addMethod));
			this.o.Write("event ");
			this.o.Write(this.FormatType(ei.EventHandlerType));
			this.o.Write(" ");
			this.o.Write(ei.Name);
			this.o.Write(";");
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00093F88 File Offset: 0x00092188
		private void OutlineConstructor(ConstructorInfo ci)
		{
			this.o.Write(Outline.GetMethodVisibility(ci));
			this.o.Write(Outline.RemoveGenericArity(this.t.Name));
			this.o.Write(" (");
			this.OutlineParams(ci.GetParameters());
			this.o.Write(");");
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00093FF0 File Offset: 0x000921F0
		private void OutlineProperty(PropertyInfo pi)
		{
			ParameterInfo[] indexParameters = pi.GetIndexParameters();
			MethodBase getMethod = pi.GetGetMethod(true);
			MethodBase setMethod = pi.GetSetMethod(true);
			MethodBase methodBase = (getMethod != null) ? getMethod : setMethod;
			if (pi.CanRead && pi.CanWrite && (getMethod.Attributes & MethodAttributes.MemberAccessMask) != (setMethod.Attributes & MethodAttributes.MemberAccessMask))
			{
				if (getMethod.IsPublic)
				{
					methodBase = getMethod;
				}
				else if (setMethod.IsPublic)
				{
					methodBase = setMethod;
				}
				else if (getMethod.IsFamilyOrAssembly)
				{
					methodBase = getMethod;
				}
				else if (setMethod.IsFamilyOrAssembly)
				{
					methodBase = setMethod;
				}
				else if (getMethod.IsAssembly || getMethod.IsFamily)
				{
					methodBase = getMethod;
				}
				else if (setMethod.IsAssembly || setMethod.IsFamily)
				{
					methodBase = setMethod;
				}
			}
			this.o.Write(Outline.GetMethodVisibility(methodBase));
			this.o.Write(Outline.GetMethodModifiers(methodBase));
			this.o.Write(this.FormatType(pi.PropertyType));
			this.o.Write(" ");
			if (indexParameters.Length == 0)
			{
				this.o.Write(pi.Name);
			}
			else
			{
				this.o.Write("this [");
				this.OutlineParams(indexParameters);
				this.o.Write("]");
			}
			this.o.WriteLine(" {");
			IndentedTextWriter indentedTextWriter = this.o;
			int indent = indentedTextWriter.Indent;
			indentedTextWriter.Indent = indent + 1;
			if (getMethod != null && this.ShowMember(getMethod))
			{
				if ((getMethod.Attributes & MethodAttributes.MemberAccessMask) != (methodBase.Attributes & MethodAttributes.MemberAccessMask))
				{
					this.o.Write(Outline.GetMethodVisibility(getMethod));
				}
				this.o.WriteLine("get;");
			}
			if (setMethod != null && this.ShowMember(setMethod))
			{
				if ((setMethod.Attributes & MethodAttributes.MemberAccessMask) != (methodBase.Attributes & MethodAttributes.MemberAccessMask))
				{
					this.o.Write(Outline.GetMethodVisibility(setMethod));
				}
				this.o.WriteLine("set;");
			}
			IndentedTextWriter indentedTextWriter2 = this.o;
			indent = indentedTextWriter2.Indent;
			indentedTextWriter2.Indent = indent - 1;
			this.o.Write("}");
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000941E8 File Offset: 0x000923E8
		private void OutlineMethod(MethodInfo mi)
		{
			if (this.MethodIsExplicitIfaceImpl(mi))
			{
				this.o.Write(this.FormatType(mi.ReturnType));
				this.o.Write(" ");
			}
			else
			{
				this.o.Write(Outline.GetMethodVisibility(mi));
				this.o.Write(Outline.GetMethodModifiers(mi));
				this.o.Write(this.FormatType(mi.ReturnType));
				this.o.Write(" ");
			}
			this.o.Write(mi.Name);
			this.o.Write(this.FormatGenericParams(mi.GetGenericArguments()));
			this.o.Write(" (");
			this.OutlineParams(mi.GetParameters());
			this.o.Write(")");
			this.WriteGenericConstraints(mi.GetGenericArguments());
			this.o.Write(";");
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x000942E0 File Offset: 0x000924E0
		private void OutlineOperator(MethodInfo mi)
		{
			this.o.Write(Outline.GetMethodVisibility(mi));
			this.o.Write(Outline.GetMethodModifiers(mi));
			if (mi.Name == "op_Explicit" || mi.Name == "op_Implicit")
			{
				this.o.Write(mi.Name.Substring(3).ToLower());
				this.o.Write(" operator ");
				this.o.Write(this.FormatType(mi.ReturnType));
			}
			else
			{
				this.o.Write(this.FormatType(mi.ReturnType));
				this.o.Write(" operator ");
				this.o.Write(this.OperatorFromName(mi.Name));
			}
			this.o.Write(" (");
			this.OutlineParams(mi.GetParameters());
			this.o.Write(");");
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x000943E4 File Offset: 0x000925E4
		private void OutlineParams(ParameterInfo[] pi)
		{
			int num = 0;
			foreach (ParameterInfo parameterInfo in pi)
			{
				if (parameterInfo.ParameterType.IsByRef)
				{
					this.o.Write(parameterInfo.IsOut ? "out " : "ref ");
					this.o.Write(this.FormatType(parameterInfo.ParameterType.GetElementType()));
				}
				else if (parameterInfo.IsDefined(this.type_param_array_attribute, false))
				{
					this.o.Write("params ");
					this.o.Write(this.FormatType(parameterInfo.ParameterType));
				}
				else
				{
					this.o.Write(this.FormatType(parameterInfo.ParameterType));
				}
				this.o.Write(" ");
				this.o.Write(parameterInfo.Name);
				if (num + 1 < pi.Length)
				{
					this.o.Write(", ");
				}
				num++;
			}
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x000944E4 File Offset: 0x000926E4
		private void OutlineField(FieldInfo fi)
		{
			if (fi.IsPublic)
			{
				this.o.Write("public ");
			}
			if (fi.IsFamily)
			{
				this.o.Write("protected ");
			}
			if (fi.IsPrivate)
			{
				this.o.Write("private ");
			}
			if (fi.IsAssembly)
			{
				this.o.Write("public ");
			}
			if (fi.IsLiteral)
			{
				this.o.Write("const ");
			}
			else if (fi.IsStatic)
			{
				this.o.Write("static ");
			}
			if (fi.IsInitOnly)
			{
				this.o.Write("readonly ");
			}
			this.o.Write(this.FormatType(fi.FieldType));
			this.o.Write(" ");
			this.o.Write(fi.Name);
			if (fi.IsLiteral)
			{
				object rawConstantValue = fi.GetRawConstantValue();
				this.o.Write(" = ");
				if (rawConstantValue is char)
				{
					this.o.Write("'{0}'", rawConstantValue);
				}
				else if (rawConstantValue is string)
				{
					this.o.Write("\"{0}\"", rawConstantValue);
				}
				else
				{
					this.o.Write(fi.GetRawConstantValue());
				}
			}
			this.o.Write(";");
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0009464C File Offset: 0x0009284C
		private static string GetMethodVisibility(MethodBase m)
		{
			if (m.DeclaringType.IsInterface)
			{
				return "";
			}
			if (m.IsPublic)
			{
				return "public ";
			}
			if (m.IsFamily)
			{
				return "protected ";
			}
			if (m.IsPrivate)
			{
				return "private ";
			}
			if (m.IsAssembly)
			{
				return "public ";
			}
			return null;
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x000946A8 File Offset: 0x000928A8
		private static string GetMethodModifiers(MethodBase method)
		{
			if (method.IsStatic)
			{
				return "static ";
			}
			if (method.IsFinal)
			{
				if (method.IsVirtual)
				{
					return null;
				}
				return "sealed ";
			}
			else
			{
				if (!method.IsVirtual || method.DeclaringType.IsInterface)
				{
					return null;
				}
				if (method.IsAbstract)
				{
					return "abstract ";
				}
				if ((method.Attributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.PrivateScope)
				{
					return "override ";
				}
				return "virtual ";
			}
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x0009471C File Offset: 0x0009291C
		private string GetTypeKind(Type t)
		{
			if (t.IsEnum)
			{
				return "enum";
			}
			if (t.IsClass)
			{
				if (t.IsSubclassOf(this.type_multicast_delegate))
				{
					return "delegate";
				}
				return "class";
			}
			else
			{
				if (t.IsInterface)
				{
					return "interface";
				}
				if (t.IsValueType)
				{
					return "struct";
				}
				return "class";
			}
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0009477C File Offset: 0x0009297C
		private static string GetTypeVisibility(Type t)
		{
			switch (t.Attributes & TypeAttributes.VisibilityMask)
			{
			case TypeAttributes.Public:
			case TypeAttributes.NestedPublic:
				return "public";
			case TypeAttributes.NestedFamily:
			case TypeAttributes.NestedFamANDAssem:
			case TypeAttributes.VisibilityMask:
				return "protected";
			}
			return "internal";
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x000947CC File Offset: 0x000929CC
		private string FormatGenericParams(Type[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (args.Length == 0)
			{
				return "";
			}
			stringBuilder.Append("<");
			for (int i = 0; i < args.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(this.FormatType(args[i]));
			}
			stringBuilder.Append(">");
			return stringBuilder.ToString();
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x00094838 File Offset: 0x00092A38
		private string FormatType(Type t)
		{
			if (t == null)
			{
				return "";
			}
			string fullName = this.GetFullName(t);
			if (fullName == null)
			{
				return t.ToString();
			}
			if (!fullName.StartsWith("System."))
			{
				if (fullName.IndexOf(".") == -1)
				{
					return fullName;
				}
				if (t.GetNamespace() == this.t.GetNamespace())
				{
					return t.Name;
				}
				return fullName;
			}
			else
			{
				if (t.HasElementType)
				{
					Type elementType = t.GetElementType();
					if (t.IsArray)
					{
						return this.FormatType(elementType) + " []";
					}
					if (t.IsPointer)
					{
						return this.FormatType(elementType) + " *";
					}
					if (t.IsByRef)
					{
						return "ref " + this.FormatType(elementType);
					}
				}
				uint num = <PrivateImplementationDetails>.ComputeStringHash(fullName);
				if (num <= 1741144581U)
				{
					if (num <= 875577056U)
					{
						if (num <= 347085918U)
						{
							if (num != 320746120U)
							{
								if (num == 347085918U)
								{
									if (fullName == "System.Boolean")
									{
										return "bool";
									}
								}
							}
							else if (fullName == "System.Void")
							{
								return "void";
							}
						}
						else if (num != 848225627U)
						{
							if (num == 875577056U)
							{
								if (fullName == "System.UInt64")
								{
									return "ulong";
								}
							}
						}
						else if (fullName == "System.Double")
						{
							return "double";
						}
					}
					else if (num <= 1625787317U)
					{
						if (num != 942540437U)
						{
							if (num == 1625787317U)
							{
								if (fullName == "System.Object")
								{
									return "object";
								}
							}
						}
						else if (fullName == "System.UInt16")
						{
							return "ushort";
						}
					}
					else if (num != 1697786220U)
					{
						if (num == 1741144581U)
						{
							if (fullName == "System.Decimal")
							{
								return "decimal";
							}
						}
					}
					else if (fullName == "System.Int16")
					{
						return "short";
					}
				}
				else if (num <= 2747029693U)
				{
					if (num <= 2185383742U)
					{
						if (num != 1764058053U)
						{
							if (num == 2185383742U)
							{
								if (fullName == "System.Single")
								{
									return "float";
								}
							}
						}
						else if (fullName == "System.Int64")
						{
							return "long";
						}
					}
					else if (num != 2249825754U)
					{
						if (num == 2747029693U)
						{
							if (fullName == "System.SByte")
							{
								return "sbyte";
							}
						}
					}
					else if (fullName == "System.Char")
					{
						return "char";
					}
				}
				else if (num <= 3291009739U)
				{
					if (num != 3079944380U)
					{
						if (num == 3291009739U)
						{
							if (fullName == "System.UInt32")
							{
								return "uint";
							}
						}
					}
					else if (fullName == "System.Byte")
					{
						return "byte";
					}
				}
				else if (num != 4180476474U)
				{
					if (num == 4201364391U)
					{
						if (fullName == "System.String")
						{
							return "string";
						}
					}
				}
				else if (fullName == "System.Int32")
				{
					return "int";
				}
				if (fullName.LastIndexOf(".") == 6)
				{
					return fullName.Substring(7);
				}
				if (this.t.Namespace.StartsWith(t.Namespace + ".") || t.Namespace == this.t.Namespace)
				{
					return fullName.Substring(t.Namespace.Length + 1);
				}
				return fullName;
			}
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00094C0C File Offset: 0x00092E0C
		public static string RemoveGenericArity(string name)
		{
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder();
			while (i < name.Length)
			{
				int num = name.IndexOf('`', i);
				if (num < 0)
				{
					stringBuilder.Append(name.Substring(i));
					break;
				}
				stringBuilder.Append(name.Substring(i, num - i));
				num++;
				while (num < name.Length && char.IsNumber(name[num]))
				{
					num++;
				}
				i = num;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00094C84 File Offset: 0x00092E84
		private string GetTypeName(Type t)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.GetTypeName(stringBuilder, t);
			return stringBuilder.ToString();
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x00094CA5 File Offset: 0x00092EA5
		private void GetTypeName(StringBuilder sb, Type t)
		{
			sb.Append(Outline.RemoveGenericArity(t.Name));
			sb.Append(this.FormatGenericParams(t.GetGenericArguments()));
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00094CCC File Offset: 0x00092ECC
		private string GetFullName(Type t)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.GetFullName_recursed(stringBuilder, t, false);
			return stringBuilder.ToString();
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00094CF0 File Offset: 0x00092EF0
		private void GetFullName_recursed(StringBuilder sb, Type t, bool recursed)
		{
			if (t.IsGenericParameter)
			{
				sb.Append(t.Name);
				return;
			}
			if (t.DeclaringType != null)
			{
				this.GetFullName_recursed(sb, t.DeclaringType, true);
				sb.Append(".");
			}
			if (!recursed)
			{
				string @namespace = t.GetNamespace();
				if (@namespace != null && @namespace != "")
				{
					sb.Append(@namespace);
					sb.Append(".");
				}
			}
			this.GetTypeName(sb, t);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00094D6C File Offset: 0x00092F6C
		private void WriteGenericConstraints(Type[] args)
		{
			foreach (Type type in args)
			{
				bool flag = true;
				Type[] array = Outline.TypeGetInterfaces(type, true);
				GenericParameterAttributes genericParameterAttributes = type.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask;
				GenericParameterAttributes[] array2 = new GenericParameterAttributes[]
				{
					GenericParameterAttributes.ReferenceTypeConstraint,
					GenericParameterAttributes.NotNullableValueTypeConstraint,
					GenericParameterAttributes.DefaultConstructorConstraint
				};
				if (type.BaseType != this.type_object || array.Length != 0 || genericParameterAttributes != GenericParameterAttributes.None)
				{
					this.o.Write(" where ");
					this.o.Write(this.FormatType(type));
					this.o.Write(" : ");
				}
				if (type.BaseType != this.type_object)
				{
					this.o.Write(this.FormatType(type.BaseType));
					flag = false;
				}
				foreach (Type type2 in array)
				{
					if (!flag)
					{
						this.o.Write(", ");
					}
					flag = false;
					this.o.Write(this.FormatType(type2));
				}
				foreach (GenericParameterAttributes genericParameterAttributes2 in array2)
				{
					if ((genericParameterAttributes & genericParameterAttributes2) != GenericParameterAttributes.None)
					{
						if (!flag)
						{
							this.o.Write(", ");
						}
						flag = false;
						if (genericParameterAttributes2 != GenericParameterAttributes.ReferenceTypeConstraint)
						{
							if (genericParameterAttributes2 != GenericParameterAttributes.NotNullableValueTypeConstraint)
							{
								if (genericParameterAttributes2 == GenericParameterAttributes.DefaultConstructorConstraint)
								{
									this.o.Write("new ()");
								}
							}
							else
							{
								this.o.Write("struct");
							}
						}
						else
						{
							this.o.Write("class");
						}
					}
				}
			}
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00094EF4 File Offset: 0x000930F4
		private string OperatorFromName(string name)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 2242295702U)
			{
				if (num <= 1234170120U)
				{
					if (num <= 906583475U)
					{
						if (num != 90588446U)
						{
							if (num != 835846267U)
							{
								if (num == 906583475U)
								{
									if (name == "op_Addition")
									{
										return "+";
									}
								}
							}
							else if (name == "op_BitwiseAnd")
							{
								return "&";
							}
						}
						else if (name == "op_OnesComplement")
						{
							return "~";
						}
					}
					else if (num != 1034931220U)
					{
						if (num != 1195761148U)
						{
							if (num == 1234170120U)
							{
								if (name == "op_LessThanOrEqual")
								{
									return "<=";
								}
							}
						}
						else if (name == "op_GreaterThan")
						{
							return ">";
						}
					}
					else if (name == "op_Increment")
					{
						return "++";
					}
				}
				else if (num <= 1548478473U)
				{
					if (num != 1258540185U)
					{
						if (num != 1516143579U)
						{
							if (num == 1548478473U)
							{
								if (name == "op_RightShift")
								{
									return ">>";
								}
							}
						}
						else if (name == "op_Equality")
						{
							return "==";
						}
					}
					else if (name == "op_LessThan")
					{
						return "<";
					}
				}
				else if (num != 1850069070U)
				{
					if (num != 1915672496U)
					{
						if (num == 2242295702U)
						{
							if (name == "op_LeftShift")
							{
								return "<<";
							}
						}
					}
					else if (name == "op_Division")
					{
						return "/";
					}
				}
				else if (name == "op_False")
				{
					return "false";
				}
			}
			else if (num <= 2958252495U)
			{
				if (num <= 2459852411U)
				{
					if (num != 2366795836U)
					{
						if (num != 2429678952U)
						{
							if (num == 2459852411U)
							{
								if (name == "op_GreaterThanOrEqual")
								{
									return ">=";
								}
							}
						}
						else if (name == "op_Modulus")
						{
							return "%";
						}
					}
					else if (name == "op_ExclusiveOr")
					{
						return "^";
					}
				}
				else if (num != 2536726348U)
				{
					if (num != 2574677899U)
					{
						if (num == 2958252495U)
						{
							if (name == "op_Multiply")
							{
								return "*";
							}
						}
					}
					else if (name == "op_LogicalNot")
					{
						return "!";
					}
				}
				else if (name == "op_Decrement")
				{
					return "--";
				}
			}
			else if (num <= 3492550567U)
			{
				if (num != 3075696130U)
				{
					if (num != 3279419199U)
					{
						if (num == 3492550567U)
						{
							if (name == "op_BitwiseOr")
							{
								return "|";
							}
						}
					}
					else if (name == "op_Subtraction")
					{
						return "-";
					}
				}
				else if (name == "op_UnaryPlus")
				{
					return "+";
				}
			}
			else if (num != 3568900899U)
			{
				if (num != 3716665893U)
				{
					if (num == 3794317784U)
					{
						if (name == "op_Inequality")
						{
							return "!=";
						}
					}
				}
				else if (name == "op_UnaryNegation")
				{
					return "-";
				}
			}
			else if (name == "op_True")
			{
				return "true";
			}
			return name;
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x000952F6 File Offset: 0x000934F6
		private bool MethodIsExplicitIfaceImpl(MethodBase mb)
		{
			return mb.IsFinal && mb.IsVirtual && mb.IsPrivate;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x00095314 File Offset: 0x00093514
		private bool ShowMember(MemberInfo mi)
		{
			if (mi.MemberType == MemberTypes.Constructor && ((MethodBase)mi).IsStatic)
			{
				return false;
			}
			if (this.show_private)
			{
				return true;
			}
			if (this.filter_obsolete && mi.IsDefined(this.type_obsolete_attribute, false))
			{
				return false;
			}
			MemberTypes memberType = mi.MemberType;
			if (memberType <= MemberTypes.Field)
			{
				if (memberType != MemberTypes.Constructor)
				{
					if (memberType != MemberTypes.Field)
					{
						return true;
					}
					FieldInfo fieldInfo = mi as FieldInfo;
					return fieldInfo.IsFamily || fieldInfo.IsPublic || fieldInfo.IsFamilyOrAssembly;
				}
			}
			else if (memberType != MemberTypes.Method)
			{
				if (memberType != MemberTypes.TypeInfo && memberType != MemberTypes.NestedType)
				{
					return true;
				}
				switch ((mi as Type).Attributes & TypeAttributes.VisibilityMask)
				{
				case TypeAttributes.Public:
				case TypeAttributes.NestedPublic:
				case TypeAttributes.NestedFamily:
				case TypeAttributes.VisibilityMask:
					return true;
				}
				return false;
			}
			MethodBase methodBase = mi as MethodBase;
			return methodBase.IsFamily || methodBase.IsPublic || methodBase.IsFamilyOrAssembly || this.MethodIsExplicitIfaceImpl(methodBase);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x00095414 File Offset: 0x00093614
		private static Type[] TypeGetInterfaces(Type t, bool declonly)
		{
			if (t.IsGenericParameter)
			{
				return new Type[0];
			}
			Type[] interfaces = t.GetInterfaces();
			if (!declonly)
			{
				return interfaces;
			}
			if (t.BaseType == null || interfaces.Length == 0)
			{
				return interfaces;
			}
			ArrayList arrayList = new ArrayList();
			foreach (Type type in interfaces)
			{
				if (!type.IsAssignableFrom(t.BaseType))
				{
					arrayList.Add(type);
				}
			}
			return (Type[])arrayList.ToArray(typeof(Type));
		}

		// Token: 0x04000B2D RID: 2861
		private bool declared_only;

		// Token: 0x04000B2E RID: 2862
		private bool show_private;

		// Token: 0x04000B2F RID: 2863
		private bool filter_obsolete;

		// Token: 0x04000B30 RID: 2864
		private IndentedTextWriter o;

		// Token: 0x04000B31 RID: 2865
		private Type t;

		// Token: 0x04000B32 RID: 2866
		private Type type_multicast_delegate;

		// Token: 0x04000B33 RID: 2867
		private Type type_object;

		// Token: 0x04000B34 RID: 2868
		private Type type_value_type;

		// Token: 0x04000B35 RID: 2869
		private Type type_int;

		// Token: 0x04000B36 RID: 2870
		private Type type_flags_attribute;

		// Token: 0x04000B37 RID: 2871
		private Type type_obsolete_attribute;

		// Token: 0x04000B38 RID: 2872
		private Type type_param_array_attribute;
	}
}

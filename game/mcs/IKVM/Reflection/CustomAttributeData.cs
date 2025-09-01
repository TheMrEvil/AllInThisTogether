using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x0200000D RID: 13
	public sealed class CustomAttributeData
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00003476 File Offset: 0x00001676
		internal CustomAttributeData(Module module, int index)
		{
			this.module = module;
			this.customAttributeIndex = index;
			this.declSecurityIndex = -1;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003493 File Offset: 0x00001693
		internal CustomAttributeData(Module module, ConstructorInfo constructor, object[] args, List<CustomAttributeNamedArgument> namedArguments) : this(module, constructor, CustomAttributeData.WrapConstructorArgs(args, constructor.MethodSignature), namedArguments)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000034AC File Offset: 0x000016AC
		private static List<CustomAttributeTypedArgument> WrapConstructorArgs(object[] args, MethodSignature sig)
		{
			List<CustomAttributeTypedArgument> list = new List<CustomAttributeTypedArgument>();
			for (int i = 0; i < args.Length; i++)
			{
				list.Add(new CustomAttributeTypedArgument(sig.GetParameterType(i), args[i]));
			}
			return list;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000034E4 File Offset: 0x000016E4
		internal CustomAttributeData(Module module, ConstructorInfo constructor, List<CustomAttributeTypedArgument> constructorArgs, List<CustomAttributeNamedArgument> namedArguments)
		{
			this.module = module;
			this.customAttributeIndex = -1;
			this.declSecurityIndex = -1;
			this.lazyConstructor = constructor;
			this.lazyConstructorArguments = constructorArgs.AsReadOnly();
			if (namedArguments == null)
			{
				this.lazyNamedArguments = Empty<CustomAttributeNamedArgument>.Array;
				return;
			}
			this.lazyNamedArguments = namedArguments.AsReadOnly();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000353C File Offset: 0x0000173C
		internal CustomAttributeData(Assembly asm, ConstructorInfo constructor, ByteReader br)
		{
			this.module = asm.ManifestModule;
			this.customAttributeIndex = -1;
			this.declSecurityIndex = -1;
			this.lazyConstructor = constructor;
			if (br.Length == 0)
			{
				this.lazyConstructorArguments = Empty<CustomAttributeTypedArgument>.Array;
				this.lazyNamedArguments = Empty<CustomAttributeNamedArgument>.Array;
				return;
			}
			if (br.ReadUInt16() != 1)
			{
				throw new BadImageFormatException();
			}
			this.lazyConstructorArguments = CustomAttributeData.ReadConstructorArguments(this.module, br, constructor);
			this.lazyNamedArguments = CustomAttributeData.ReadNamedArguments(this.module, br, (int)br.ReadUInt16(), constructor.DeclaringType, true);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000035D0 File Offset: 0x000017D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.Append(this.Constructor.DeclaringType.FullName);
			stringBuilder.Append('(');
			string value = "";
			ParameterInfo[] parameters = this.Constructor.GetParameters();
			IList<CustomAttributeTypedArgument> constructorArguments = this.ConstructorArguments;
			for (int i = 0; i < parameters.Length; i++)
			{
				stringBuilder.Append(value);
				value = ", ";
				CustomAttributeData.AppendValue(stringBuilder, parameters[i].ParameterType, constructorArguments[i]);
			}
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in this.NamedArguments)
			{
				stringBuilder.Append(value);
				value = ", ";
				stringBuilder.Append(customAttributeNamedArgument.MemberInfo.Name);
				stringBuilder.Append(" = ");
				FieldInfo fieldInfo = customAttributeNamedArgument.MemberInfo as FieldInfo;
				Type type = (fieldInfo != null) ? fieldInfo.FieldType : ((PropertyInfo)customAttributeNamedArgument.MemberInfo).PropertyType;
				CustomAttributeData.AppendValue(stringBuilder, type, customAttributeNamedArgument.TypedValue);
			}
			stringBuilder.Append(')');
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003728 File Offset: 0x00001928
		private static void AppendValue(StringBuilder sb, Type type, CustomAttributeTypedArgument arg)
		{
			if (arg.ArgumentType == arg.ArgumentType.Module.universe.System_String)
			{
				sb.Append('"').Append(arg.Value).Append('"');
				return;
			}
			if (arg.ArgumentType.IsArray)
			{
				Type elementType = arg.ArgumentType.GetElementType();
				string value;
				if (elementType.IsPrimitive || elementType == type.Module.universe.System_Object || elementType == type.Module.universe.System_String || elementType == type.Module.universe.System_Type)
				{
					value = elementType.Name;
				}
				else
				{
					value = elementType.FullName;
				}
				sb.Append("new ").Append(value).Append("[").Append(((Array)arg.Value).Length).Append("] { ");
				string value2 = "";
				foreach (CustomAttributeTypedArgument arg2 in (CustomAttributeTypedArgument[])arg.Value)
				{
					sb.Append(value2);
					value2 = ", ";
					CustomAttributeData.AppendValue(sb, elementType, arg2);
				}
				sb.Append(" }");
				return;
			}
			if (arg.ArgumentType != type || (type.IsEnum && !arg.Value.Equals(0)))
			{
				sb.Append('(');
				sb.Append(arg.ArgumentType.FullName);
				sb.Append(')');
			}
			sb.Append(arg.Value);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000038E4 File Offset: 0x00001AE4
		internal static void ReadDeclarativeSecurity(Module module, int index, List<CustomAttributeData> list)
		{
			Universe universe = module.universe;
			Assembly assembly = module.Assembly;
			int action = (int)module.DeclSecurity.records[index].Action;
			ByteReader blob = module.GetBlob(module.DeclSecurity.records[index].PermissionSet);
			if (blob.PeekByte() == 46)
			{
				blob.ReadByte();
				int num = blob.ReadCompressedUInt();
				for (int i = 0; i < num; i++)
				{
					ConstructorInfo pseudoCustomAttributeConstructor = CustomAttributeData.ReadType(module, blob).GetPseudoCustomAttributeConstructor(new Type[]
					{
						universe.System_Security_Permissions_SecurityAction
					});
					ByteReader byteReader = blob;
					byte[] blob2 = byteReader.ReadBytes(byteReader.ReadCompressedUInt());
					list.Add(new CustomAttributeData(assembly, pseudoCustomAttributeConstructor, action, blob2, index));
				}
				return;
			}
			char[] array = new char[blob.Length / 2];
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = blob.ReadChar();
			}
			string value = new string(array);
			ConstructorInfo pseudoCustomAttributeConstructor2 = universe.System_Security_Permissions_PermissionSetAttribute.GetPseudoCustomAttributeConstructor(new Type[]
			{
				universe.System_Security_Permissions_SecurityAction
			});
			List<CustomAttributeNamedArgument> list2 = new List<CustomAttributeNamedArgument>();
			list2.Add(new CustomAttributeNamedArgument(CustomAttributeData.GetProperty(null, universe.System_Security_Permissions_PermissionSetAttribute, "XML", universe.System_String), new CustomAttributeTypedArgument(universe.System_String, value)));
			list.Add(new CustomAttributeData(assembly.ManifestModule, pseudoCustomAttributeConstructor2, new object[]
			{
				action
			}, list2));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003A48 File Offset: 0x00001C48
		internal CustomAttributeData(Assembly asm, ConstructorInfo constructor, int securityAction, byte[] blob, int index)
		{
			this.module = asm.ManifestModule;
			this.customAttributeIndex = -1;
			this.declSecurityIndex = index;
			Universe universe = constructor.Module.universe;
			this.lazyConstructor = constructor;
			this.lazyConstructorArguments = new List<CustomAttributeTypedArgument>
			{
				new CustomAttributeTypedArgument(universe.System_Security_Permissions_SecurityAction, securityAction)
			}.AsReadOnly();
			this.declSecurityBlob = blob;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003ABC File Offset: 0x00001CBC
		private static Type ReadFieldOrPropType(Module context, ByteReader br)
		{
			Universe universe = context.universe;
			byte b = br.ReadByte();
			if (b <= 29)
			{
				switch (b)
				{
				case 2:
					return universe.System_Boolean;
				case 3:
					return universe.System_Char;
				case 4:
					return universe.System_SByte;
				case 5:
					return universe.System_Byte;
				case 6:
					return universe.System_Int16;
				case 7:
					return universe.System_UInt16;
				case 8:
					return universe.System_Int32;
				case 9:
					return universe.System_UInt32;
				case 10:
					return universe.System_Int64;
				case 11:
					return universe.System_UInt64;
				case 12:
					return universe.System_Single;
				case 13:
					return universe.System_Double;
				case 14:
					return universe.System_String;
				default:
					if (b == 29)
					{
						return CustomAttributeData.ReadFieldOrPropType(context, br).MakeArrayType();
					}
					break;
				}
			}
			else
			{
				if (b == 80)
				{
					return universe.System_Type;
				}
				if (b == 81)
				{
					return universe.System_Object;
				}
				if (b == 85)
				{
					return CustomAttributeData.ReadType(context, br);
				}
			}
			throw new BadImageFormatException();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003BB8 File Offset: 0x00001DB8
		private static CustomAttributeTypedArgument ReadFixedArg(Module context, ByteReader br, Type type)
		{
			Universe universe = context.universe;
			if (type == universe.System_String)
			{
				return new CustomAttributeTypedArgument(type, br.ReadString());
			}
			if (type == universe.System_Boolean)
			{
				return new CustomAttributeTypedArgument(type, br.ReadByte() > 0);
			}
			if (type == universe.System_Char)
			{
				return new CustomAttributeTypedArgument(type, br.ReadChar());
			}
			if (type == universe.System_Single)
			{
				return new CustomAttributeTypedArgument(type, br.ReadSingle());
			}
			if (type == universe.System_Double)
			{
				return new CustomAttributeTypedArgument(type, br.ReadDouble());
			}
			if (type == universe.System_SByte)
			{
				return new CustomAttributeTypedArgument(type, br.ReadSByte());
			}
			if (type == universe.System_Int16)
			{
				return new CustomAttributeTypedArgument(type, br.ReadInt16());
			}
			if (type == universe.System_Int32)
			{
				return new CustomAttributeTypedArgument(type, br.ReadInt32());
			}
			if (type == universe.System_Int64)
			{
				return new CustomAttributeTypedArgument(type, br.ReadInt64());
			}
			if (type == universe.System_Byte)
			{
				return new CustomAttributeTypedArgument(type, br.ReadByte());
			}
			if (type == universe.System_UInt16)
			{
				return new CustomAttributeTypedArgument(type, br.ReadUInt16());
			}
			if (type == universe.System_UInt32)
			{
				return new CustomAttributeTypedArgument(type, br.ReadUInt32());
			}
			if (type == universe.System_UInt64)
			{
				return new CustomAttributeTypedArgument(type, br.ReadUInt64());
			}
			if (type == universe.System_Type)
			{
				return new CustomAttributeTypedArgument(type, CustomAttributeData.ReadType(context, br));
			}
			if (type == universe.System_Object)
			{
				return CustomAttributeData.ReadFixedArg(context, br, CustomAttributeData.ReadFieldOrPropType(context, br));
			}
			if (type.IsArray)
			{
				int num = br.ReadInt32();
				if (num == -1)
				{
					return new CustomAttributeTypedArgument(type, null);
				}
				Type elementType = type.GetElementType();
				CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = CustomAttributeData.ReadFixedArg(context, br, elementType);
				}
				return new CustomAttributeTypedArgument(type, array);
			}
			else
			{
				if (type.IsEnum)
				{
					return new CustomAttributeTypedArgument(type, CustomAttributeData.ReadFixedArg(context, br, type.GetEnumUnderlyingTypeImpl()).Value);
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003E20 File Offset: 0x00002020
		private static Type ReadType(Module context, ByteReader br)
		{
			string text = br.ReadString();
			if (text == null)
			{
				return null;
			}
			if (text.Length > 0)
			{
				string text2 = text;
				if (text2[text2.Length - 1] == '\0')
				{
					text = text.Substring(0, text.Length - 1);
				}
			}
			return TypeNameParser.Parse(text, true).GetType(context.universe, context, true, text, true, false);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003E7C File Offset: 0x0000207C
		private static IList<CustomAttributeTypedArgument> ReadConstructorArguments(Module context, ByteReader br, ConstructorInfo constructor)
		{
			MethodSignature methodSignature = constructor.MethodSignature;
			int parameterCount = methodSignature.GetParameterCount();
			List<CustomAttributeTypedArgument> list = new List<CustomAttributeTypedArgument>(parameterCount);
			for (int i = 0; i < parameterCount; i++)
			{
				list.Add(CustomAttributeData.ReadFixedArg(context, br, methodSignature.GetParameterType(i)));
			}
			return list.AsReadOnly();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003EC4 File Offset: 0x000020C4
		private static IList<CustomAttributeNamedArgument> ReadNamedArguments(Module context, ByteReader br, int named, Type type, bool required)
		{
			List<CustomAttributeNamedArgument> list = new List<CustomAttributeNamedArgument>(named);
			for (int i = 0; i < named; i++)
			{
				byte b = br.ReadByte();
				Type type2 = CustomAttributeData.ReadFieldOrPropType(context, br);
				if (type2.__IsMissing && !required)
				{
					return null;
				}
				string name = br.ReadString();
				CustomAttributeTypedArgument value = CustomAttributeData.ReadFixedArg(context, br, type2);
				MemberInfo member;
				if (b != 83)
				{
					if (b != 84)
					{
						throw new BadImageFormatException();
					}
					member = CustomAttributeData.GetProperty(context, type, name, type2);
				}
				else
				{
					member = CustomAttributeData.GetField(context, type, name, type2);
				}
				list.Add(new CustomAttributeNamedArgument(member, value));
			}
			return list.AsReadOnly();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003F58 File Offset: 0x00002158
		private static FieldInfo GetField(Module context, Type type, string name, Type fieldType)
		{
			Type type2 = type;
			while (type != null && !type.__IsMissing)
			{
				foreach (FieldInfo fieldInfo in type.__GetDeclaredFields())
				{
					if (fieldInfo.IsPublic && !fieldInfo.IsStatic && fieldInfo.Name == name)
					{
						return fieldInfo;
					}
				}
				type = type.BaseType;
			}
			if (type == null)
			{
				type = type2;
			}
			FieldSignature signature = FieldSignature.Create(fieldType, default(CustomModifiers));
			return type.FindField(name, signature) ?? type.Module.universe.GetMissingFieldOrThrow(context, type, name, signature);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004000 File Offset: 0x00002200
		private static PropertyInfo GetProperty(Module context, Type type, string name, Type propertyType)
		{
			Type type2 = type;
			while (type != null && !type.__IsMissing)
			{
				foreach (PropertyInfo propertyInfo in type.__GetDeclaredProperties())
				{
					if (propertyInfo.IsPublic && !propertyInfo.IsStatic && propertyInfo.Name == name)
					{
						return propertyInfo;
					}
				}
				type = type.BaseType;
			}
			if (type == null)
			{
				type = type2;
			}
			return type.Module.universe.GetMissingPropertyOrThrow(context, type, name, PropertySignature.Create(CallingConventions.Standard | CallingConventions.HasThis, propertyType, null, default(PackedCustomModifiers)));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004098 File Offset: 0x00002298
		[Obsolete("Use AttributeType property instead.")]
		internal bool __TryReadTypeName(out string ns, out string name)
		{
			if (this.Constructor.DeclaringType.IsNested)
			{
				ns = null;
				name = null;
				return false;
			}
			TypeName typeName = this.AttributeType.TypeName;
			ns = typeName.Namespace;
			name = typeName.Name;
			return true;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000040E0 File Offset: 0x000022E0
		public byte[] __GetBlob()
		{
			if (this.declSecurityBlob != null)
			{
				return (byte[])this.declSecurityBlob.Clone();
			}
			if (this.customAttributeIndex == -1)
			{
				return this.__ToBuilder().GetBlob(this.module.Assembly);
			}
			return ((ModuleReader)this.module).GetBlobCopy(this.module.CustomAttribute.records[this.customAttributeIndex].Value);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004158 File Offset: 0x00002358
		public int __Parent
		{
			get
			{
				if (this.customAttributeIndex >= 0)
				{
					return this.module.CustomAttribute.records[this.customAttributeIndex].Parent;
				}
				if (this.declSecurityIndex < 0)
				{
					return 0;
				}
				return this.module.DeclSecurity.records[this.declSecurityIndex].Parent;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000041BA File Offset: 0x000023BA
		public Type AttributeType
		{
			get
			{
				return this.Constructor.DeclaringType;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000041C8 File Offset: 0x000023C8
		public ConstructorInfo Constructor
		{
			get
			{
				if (this.lazyConstructor == null)
				{
					this.lazyConstructor = (ConstructorInfo)this.module.ResolveMethod(this.module.CustomAttribute.records[this.customAttributeIndex].Type);
				}
				return this.lazyConstructor;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000421F File Offset: 0x0000241F
		public IList<CustomAttributeTypedArgument> ConstructorArguments
		{
			get
			{
				if (this.lazyConstructorArguments == null)
				{
					this.LazyParseArguments(false);
				}
				return this.lazyConstructorArguments;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004238 File Offset: 0x00002438
		public IList<CustomAttributeNamedArgument> NamedArguments
		{
			get
			{
				if (this.lazyNamedArguments == null)
				{
					if (this.customAttributeIndex >= 0)
					{
						this.LazyParseArguments(true);
					}
					else
					{
						ByteReader byteReader = new ByteReader(this.declSecurityBlob, 0, this.declSecurityBlob.Length);
						Module context = this.module;
						ByteReader byteReader2 = byteReader;
						this.lazyNamedArguments = CustomAttributeData.ReadNamedArguments(context, byteReader2, byteReader2.ReadCompressedUInt(), this.Constructor.DeclaringType, true);
					}
				}
				return this.lazyNamedArguments;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000042A0 File Offset: 0x000024A0
		private void LazyParseArguments(bool requireNameArguments)
		{
			ByteReader blob = this.module.GetBlob(this.module.CustomAttribute.records[this.customAttributeIndex].Value);
			if (blob.Length == 0)
			{
				this.lazyConstructorArguments = Empty<CustomAttributeTypedArgument>.Array;
				this.lazyNamedArguments = Empty<CustomAttributeNamedArgument>.Array;
				return;
			}
			if (blob.ReadUInt16() != 1)
			{
				throw new BadImageFormatException();
			}
			this.lazyConstructorArguments = CustomAttributeData.ReadConstructorArguments(this.module, blob, this.Constructor);
			Module context = this.module;
			ByteReader byteReader = blob;
			this.lazyNamedArguments = CustomAttributeData.ReadNamedArguments(context, byteReader, (int)byteReader.ReadUInt16(), this.Constructor.DeclaringType, requireNameArguments);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004344 File Offset: 0x00002544
		public CustomAttributeBuilder __ToBuilder()
		{
			ParameterInfo[] parameters = this.Constructor.GetParameters();
			object[] array = new object[this.ConstructorArguments.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = CustomAttributeData.RewrapArray(parameters[i].ParameterType, this.ConstructorArguments[i]);
			}
			List<PropertyInfo> list = new List<PropertyInfo>();
			List<object> list2 = new List<object>();
			List<FieldInfo> list3 = new List<FieldInfo>();
			List<object> list4 = new List<object>();
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in this.NamedArguments)
			{
				PropertyInfo propertyInfo = customAttributeNamedArgument.MemberInfo as PropertyInfo;
				if (propertyInfo != null)
				{
					list.Add(propertyInfo);
					list2.Add(CustomAttributeData.RewrapArray(propertyInfo.PropertyType, customAttributeNamedArgument.TypedValue));
				}
				else
				{
					FieldInfo fieldInfo = (FieldInfo)customAttributeNamedArgument.MemberInfo;
					list3.Add(fieldInfo);
					list4.Add(CustomAttributeData.RewrapArray(fieldInfo.FieldType, customAttributeNamedArgument.TypedValue));
				}
			}
			return new CustomAttributeBuilder(this.Constructor, array, list.ToArray(), list2.ToArray(), list3.ToArray(), list4.ToArray());
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000448C File Offset: 0x0000268C
		private static object RewrapArray(Type type, CustomAttributeTypedArgument arg)
		{
			IList<CustomAttributeTypedArgument> list = arg.Value as IList<CustomAttributeTypedArgument>;
			if (list == null)
			{
				return arg.Value;
			}
			Type elementType = arg.ArgumentType.GetElementType();
			object[] array = new object[list.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = CustomAttributeData.RewrapArray(elementType, list[i]);
			}
			if (type == type.Module.universe.System_Object)
			{
				return CustomAttributeBuilder.__MakeTypedArgument(arg.ArgumentType, array);
			}
			return array;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004514 File Offset: 0x00002714
		public static IList<CustomAttributeData> GetCustomAttributes(MemberInfo member)
		{
			return CustomAttributeData.__GetCustomAttributes(member, null, false);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000451E File Offset: 0x0000271E
		public static IList<CustomAttributeData> GetCustomAttributes(Assembly assembly)
		{
			return assembly.GetCustomAttributesData(null);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004527 File Offset: 0x00002727
		public static IList<CustomAttributeData> GetCustomAttributes(Module module)
		{
			return CustomAttributeData.__GetCustomAttributes(module, null, false);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004531 File Offset: 0x00002731
		public static IList<CustomAttributeData> GetCustomAttributes(ParameterInfo parameter)
		{
			return CustomAttributeData.__GetCustomAttributes(parameter, null, false);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000453B File Offset: 0x0000273B
		public static IList<CustomAttributeData> __GetCustomAttributes(Assembly assembly, Type attributeType, bool inherit)
		{
			return assembly.GetCustomAttributesData(attributeType);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004544 File Offset: 0x00002744
		public static IList<CustomAttributeData> __GetCustomAttributes(Module module, Type attributeType, bool inherit)
		{
			if (module.__IsMissing)
			{
				throw new MissingModuleException((MissingModule)module);
			}
			IList<CustomAttributeData> customAttributesImpl = CustomAttributeData.GetCustomAttributesImpl(null, module, 1, attributeType);
			return customAttributesImpl ?? CustomAttributeData.EmptyList;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000457C File Offset: 0x0000277C
		public static IList<CustomAttributeData> __GetCustomAttributes(ParameterInfo parameter, Type attributeType, bool inherit)
		{
			Module module = parameter.Module;
			List<CustomAttributeData> list = null;
			FieldMarshal fm;
			if (module.universe.ReturnPseudoCustomAttributes && (attributeType == null || attributeType.IsAssignableFrom(parameter.Module.universe.System_Runtime_InteropServices_MarshalAsAttribute)) && parameter.__TryGetFieldMarshal(out fm))
			{
				if (list == null)
				{
					list = new List<CustomAttributeData>();
				}
				list.Add(CustomAttributeData.CreateMarshalAsPseudoCustomAttribute(parameter.Module, fm));
			}
			ModuleBuilder moduleBuilder = module as ModuleBuilder;
			int num = parameter.MetadataToken;
			if (moduleBuilder != null && moduleBuilder.IsSaved && ModuleBuilder.IsPseudoToken(num))
			{
				num = moduleBuilder.ResolvePseudoToken(num);
			}
			IList<CustomAttributeData> customAttributesImpl = CustomAttributeData.GetCustomAttributesImpl(list, module, num, attributeType);
			return customAttributesImpl ?? CustomAttributeData.EmptyList;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004628 File Offset: 0x00002828
		public static IList<CustomAttributeData> __GetCustomAttributes(MemberInfo member, Type attributeType, bool inherit)
		{
			if (!member.IsBaked)
			{
				throw new NotImplementedException();
			}
			if (!inherit || !CustomAttributeData.IsInheritableAttribute(attributeType))
			{
				IList<CustomAttributeData> customAttributesImpl = CustomAttributeData.GetCustomAttributesImpl(null, member, attributeType);
				return customAttributesImpl ?? CustomAttributeData.EmptyList;
			}
			List<CustomAttributeData> list = new List<CustomAttributeData>();
			for (;;)
			{
				CustomAttributeData.GetCustomAttributesImpl(list, member, attributeType);
				Type type = member as Type;
				if (type != null)
				{
					type = type.BaseType;
					if (type == null)
					{
						break;
					}
					member = type;
				}
				else
				{
					MethodInfo methodInfo = member as MethodInfo;
					if (!(methodInfo != null))
					{
						return list;
					}
					MemberInfo m = member;
					methodInfo = methodInfo.GetBaseDefinition();
					if (methodInfo == null || methodInfo == m)
					{
						return list;
					}
					member = methodInfo;
				}
			}
			return list;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000046D0 File Offset: 0x000028D0
		private static List<CustomAttributeData> GetCustomAttributesImpl(List<CustomAttributeData> list, MemberInfo member, Type attributeType)
		{
			if (member.Module.universe.ReturnPseudoCustomAttributes)
			{
				List<CustomAttributeData> pseudoCustomAttributes = member.GetPseudoCustomAttributes(attributeType);
				if (list == null)
				{
					list = pseudoCustomAttributes;
				}
				else if (pseudoCustomAttributes != null)
				{
					list.AddRange(pseudoCustomAttributes);
				}
			}
			return CustomAttributeData.GetCustomAttributesImpl(list, member.Module, member.GetCurrentToken(), attributeType);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000471C File Offset: 0x0000291C
		internal static List<CustomAttributeData> GetCustomAttributesImpl(List<CustomAttributeData> list, Module module, int token, Type attributeType)
		{
			foreach (int num in module.CustomAttribute.Filter(token))
			{
				if (attributeType == null)
				{
					if (list == null)
					{
						list = new List<CustomAttributeData>();
					}
					list.Add(new CustomAttributeData(module, num));
				}
				else if (attributeType.IsAssignableFrom(module.ResolveMethod(module.CustomAttribute.records[num].Type).DeclaringType))
				{
					if (list == null)
					{
						list = new List<CustomAttributeData>();
					}
					list.Add(new CustomAttributeData(module, num));
				}
			}
			return list;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000047B4 File Offset: 0x000029B4
		public static IList<CustomAttributeData> __GetCustomAttributes(Type type, Type interfaceType, Type attributeType, bool inherit)
		{
			Module module = type.Module;
			foreach (int num in module.InterfaceImpl.Filter(type.MetadataToken))
			{
				Module module2 = module;
				if (module2.ResolveType(module2.InterfaceImpl.records[num].Interface, type) == interfaceType)
				{
					IList<CustomAttributeData> customAttributesImpl = CustomAttributeData.GetCustomAttributesImpl(null, module, 9 << 24 | num + 1, attributeType);
					return customAttributesImpl ?? CustomAttributeData.EmptyList;
				}
			}
			return CustomAttributeData.EmptyList;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000483E File Offset: 0x00002A3E
		public static IList<CustomAttributeData> __GetDeclarativeSecurity(Assembly assembly)
		{
			if (assembly.__IsMissing)
			{
				throw new MissingAssemblyException((MissingAssembly)assembly);
			}
			return assembly.ManifestModule.GetDeclarativeSecurity(536870913);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004864 File Offset: 0x00002A64
		public static IList<CustomAttributeData> __GetDeclarativeSecurity(Type type)
		{
			if ((type.Attributes & TypeAttributes.HasSecurity) != TypeAttributes.AnsiClass)
			{
				return type.Module.GetDeclarativeSecurity(type.MetadataToken);
			}
			return CustomAttributeData.EmptyList;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000488B File Offset: 0x00002A8B
		public static IList<CustomAttributeData> __GetDeclarativeSecurity(MethodBase method)
		{
			if ((method.Attributes & MethodAttributes.HasSecurity) != MethodAttributes.PrivateScope)
			{
				return method.Module.GetDeclarativeSecurity(method.MetadataToken);
			}
			return CustomAttributeData.EmptyList;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000048B4 File Offset: 0x00002AB4
		private static bool IsInheritableAttribute(Type attribute)
		{
			Type system_AttributeUsageAttribute = attribute.Module.universe.System_AttributeUsageAttribute;
			IList<CustomAttributeData> list = CustomAttributeData.__GetCustomAttributes(attribute, system_AttributeUsageAttribute, false);
			if (list.Count != 0)
			{
				foreach (CustomAttributeNamedArgument customAttributeNamedArgument in list[0].NamedArguments)
				{
					if (customAttributeNamedArgument.MemberInfo.Name == "Inherited")
					{
						return (bool)customAttributeNamedArgument.TypedValue.Value;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004958 File Offset: 0x00002B58
		internal static CustomAttributeData CreateDllImportPseudoCustomAttribute(Module module, ImplMapFlags flags, string entryPoint, string dllName, MethodImplAttributes attr)
		{
			Type system_Runtime_InteropServices_DllImportAttribute = module.universe.System_Runtime_InteropServices_DllImportAttribute;
			ConstructorInfo pseudoCustomAttributeConstructor = system_Runtime_InteropServices_DllImportAttribute.GetPseudoCustomAttributeConstructor(new Type[]
			{
				module.universe.System_String
			});
			List<CustomAttributeNamedArgument> list = new List<CustomAttributeNamedArgument>();
			CharSet charSet;
			switch (flags & ImplMapFlags.CharSetMask)
			{
			case ImplMapFlags.CharSetAnsi:
				charSet = CharSet.Ansi;
				goto IL_65;
			case ImplMapFlags.CharSetUnicode:
				charSet = CharSet.Unicode;
				goto IL_65;
			case ImplMapFlags.CharSetMask:
				charSet = CharSet.Auto;
				goto IL_65;
			}
			charSet = CharSet.None;
			IL_65:
			ImplMapFlags implMapFlags = flags & ImplMapFlags.CallConvMask;
			CallingConvention callingConvention;
			if (implMapFlags <= ImplMapFlags.CallConvCdecl)
			{
				if (implMapFlags == ImplMapFlags.CallConvWinapi)
				{
					callingConvention = CallingConvention.Winapi;
					goto IL_C4;
				}
				if (implMapFlags == ImplMapFlags.CallConvCdecl)
				{
					callingConvention = CallingConvention.Cdecl;
					goto IL_C4;
				}
			}
			else
			{
				if (implMapFlags == ImplMapFlags.CallConvStdcall)
				{
					callingConvention = CallingConvention.StdCall;
					goto IL_C4;
				}
				if (implMapFlags == ImplMapFlags.CallConvThiscall)
				{
					callingConvention = CallingConvention.ThisCall;
					goto IL_C4;
				}
				if (implMapFlags == ImplMapFlags.CallConvFastcall)
				{
					callingConvention = CallingConvention.FastCall;
					goto IL_C4;
				}
			}
			callingConvention = (CallingConvention)0;
			IL_C4:
			CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_DllImportAttribute, "EntryPoint", entryPoint);
			CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_DllImportAttribute, "CharSet", module.universe.System_Runtime_InteropServices_CharSet, (int)charSet);
			CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_DllImportAttribute, "ExactSpelling", (int)flags, 1);
			CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_DllImportAttribute, "SetLastError", (int)flags, 64);
			CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_DllImportAttribute, "PreserveSig", (int)attr, 128);
			CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_DllImportAttribute, "CallingConvention", module.universe.System_Runtime_InteropServices_CallingConvention, (int)callingConvention);
			CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_DllImportAttribute, "BestFitMapping", (int)flags, 16);
			CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_DllImportAttribute, "ThrowOnUnmappableChar", (int)flags, 4096);
			return new CustomAttributeData(module, pseudoCustomAttributeConstructor, new object[]
			{
				dllName
			}, list);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004AD4 File Offset: 0x00002CD4
		internal static CustomAttributeData CreateMarshalAsPseudoCustomAttribute(Module module, FieldMarshal fm)
		{
			Type system_Runtime_InteropServices_MarshalAsAttribute = module.universe.System_Runtime_InteropServices_MarshalAsAttribute;
			Type system_Runtime_InteropServices_UnmanagedType = module.universe.System_Runtime_InteropServices_UnmanagedType;
			Type system_Runtime_InteropServices_VarEnum = module.universe.System_Runtime_InteropServices_VarEnum;
			Type system_Type = module.universe.System_Type;
			List<CustomAttributeNamedArgument> list = new List<CustomAttributeNamedArgument>();
			CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_MarshalAsAttribute, "ArraySubType", system_Runtime_InteropServices_UnmanagedType, (int)(fm.ArraySubType ?? ((UnmanagedType)0)));
			CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_MarshalAsAttribute, "SizeParamIndex", module.universe.System_Int16, fm.SizeParamIndex ?? 0);
			CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_MarshalAsAttribute, "SizeConst", module.universe.System_Int32, fm.SizeConst ?? 0);
			CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_MarshalAsAttribute, "IidParameterIndex", module.universe.System_Int32, fm.IidParameterIndex ?? 0);
			CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_MarshalAsAttribute, "SafeArraySubType", system_Runtime_InteropServices_VarEnum, (int)(fm.SafeArraySubType ?? VarEnum.VT_EMPTY));
			if (fm.SafeArrayUserDefinedSubType != null)
			{
				CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_MarshalAsAttribute, "SafeArrayUserDefinedSubType", system_Type, fm.SafeArrayUserDefinedSubType);
			}
			if (fm.MarshalType != null)
			{
				CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_MarshalAsAttribute, "MarshalType", module.universe.System_String, fm.MarshalType);
			}
			if (fm.MarshalTypeRef != null)
			{
				CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_MarshalAsAttribute, "MarshalTypeRef", module.universe.System_Type, fm.MarshalTypeRef);
			}
			if (fm.MarshalCookie != null)
			{
				CustomAttributeData.AddNamedArgument(list, system_Runtime_InteropServices_MarshalAsAttribute, "MarshalCookie", module.universe.System_String, fm.MarshalCookie);
			}
			ConstructorInfo pseudoCustomAttributeConstructor = system_Runtime_InteropServices_MarshalAsAttribute.GetPseudoCustomAttributeConstructor(new Type[]
			{
				system_Runtime_InteropServices_UnmanagedType
			});
			return new CustomAttributeData(module, pseudoCustomAttributeConstructor, new object[]
			{
				(int)fm.UnmanagedType
			}, list);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004CE6 File Offset: 0x00002EE6
		private static void AddNamedArgument(List<CustomAttributeNamedArgument> list, Type type, string fieldName, string value)
		{
			CustomAttributeData.AddNamedArgument(list, type, fieldName, type.Module.universe.System_String, value);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004D01 File Offset: 0x00002F01
		private static void AddNamedArgument(List<CustomAttributeNamedArgument> list, Type type, string fieldName, int flags, int flagMask)
		{
			CustomAttributeData.AddNamedArgument(list, type, fieldName, type.Module.universe.System_Boolean, (flags & flagMask) != 0);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004D28 File Offset: 0x00002F28
		private static void AddNamedArgument(List<CustomAttributeNamedArgument> list, Type attributeType, string fieldName, Type valueType, object value)
		{
			FieldInfo fieldInfo = attributeType.FindField(fieldName, FieldSignature.Create(valueType, default(CustomModifiers)));
			if (fieldInfo != null)
			{
				list.Add(new CustomAttributeNamedArgument(fieldInfo, new CustomAttributeTypedArgument(valueType, value)));
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004D6C File Offset: 0x00002F6C
		internal static CustomAttributeData CreateFieldOffsetPseudoCustomAttribute(Module module, int offset)
		{
			ConstructorInfo pseudoCustomAttributeConstructor = module.universe.System_Runtime_InteropServices_FieldOffsetAttribute.GetPseudoCustomAttributeConstructor(new Type[]
			{
				module.universe.System_Int32
			});
			return new CustomAttributeData(module, pseudoCustomAttributeConstructor, new object[]
			{
				offset
			}, null);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004DB8 File Offset: 0x00002FB8
		internal static CustomAttributeData CreatePreserveSigPseudoCustomAttribute(Module module)
		{
			ConstructorInfo pseudoCustomAttributeConstructor = module.universe.System_Runtime_InteropServices_PreserveSigAttribute.GetPseudoCustomAttributeConstructor(new Type[0]);
			return new CustomAttributeData(module, pseudoCustomAttributeConstructor, Empty<object>.Array, null);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004DE9 File Offset: 0x00002FE9
		// Note: this type is marked as 'beforefieldinit'.
		static CustomAttributeData()
		{
		}

		// Token: 0x04000033 RID: 51
		internal static readonly IList<CustomAttributeData> EmptyList = new List<CustomAttributeData>(0).AsReadOnly();

		// Token: 0x04000034 RID: 52
		private readonly Module module;

		// Token: 0x04000035 RID: 53
		private readonly int customAttributeIndex;

		// Token: 0x04000036 RID: 54
		private readonly int declSecurityIndex;

		// Token: 0x04000037 RID: 55
		private readonly byte[] declSecurityBlob;

		// Token: 0x04000038 RID: 56
		private ConstructorInfo lazyConstructor;

		// Token: 0x04000039 RID: 57
		private IList<CustomAttributeTypedArgument> lazyConstructorArguments;

		// Token: 0x0400003A RID: 58
		private IList<CustomAttributeNamedArgument> lazyNamedArguments;
	}
}

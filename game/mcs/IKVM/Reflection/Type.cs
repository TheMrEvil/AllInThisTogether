using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IKVM.Reflection
{
	// Token: 0x0200005C RID: 92
	public abstract class Type : MemberInfo, IGenericContext, IGenericBinder
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x0000DE9F File Offset: 0x0000C09F
		internal Type()
		{
			this.underlyingType = this;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000DEAE File Offset: 0x0000C0AE
		internal Type(Type underlyingType)
		{
			this.underlyingType = underlyingType;
			this.typeFlags = underlyingType.typeFlags;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000DEC9 File Offset: 0x0000C0C9
		internal Type(byte sigElementType) : this()
		{
			this.sigElementType = sigElementType;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		public static Binder DefaultBinder
		{
			get
			{
				return new DefaultBinder();
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000DEDF File Offset: 0x0000C0DF
		public sealed override MemberTypes MemberType
		{
			get
			{
				if (!this.IsNested)
				{
					return MemberTypes.TypeInfo;
				}
				return MemberTypes.NestedType;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000DEF1 File Offset: 0x0000C0F1
		public virtual string AssemblyQualifiedName
		{
			get
			{
				return this.FullName + ", " + this.Assembly.FullName;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000478 RID: 1144
		public abstract Type BaseType { get; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000479 RID: 1145
		public abstract TypeAttributes Attributes { get; }

		// Token: 0x0600047A RID: 1146 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual Type GetElementType()
		{
			return null;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000AF70 File Offset: 0x00009170
		internal virtual void CheckBaked()
		{
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00008BC5 File Offset: 0x00006DC5
		public virtual Type[] __GetDeclaredTypes()
		{
			return Type.EmptyTypes;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00008BC5 File Offset: 0x00006DC5
		public virtual Type[] __GetDeclaredInterfaces()
		{
			return Type.EmptyTypes;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000DF0E File Offset: 0x0000C10E
		public virtual MethodBase[] __GetDeclaredMethods()
		{
			return Empty<MethodBase>.Array;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual __MethodImplMap __GetMethodImplMap()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000DF15 File Offset: 0x0000C115
		public virtual FieldInfo[] __GetDeclaredFields()
		{
			return Empty<FieldInfo>.Array;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000DF1C File Offset: 0x0000C11C
		public virtual EventInfo[] __GetDeclaredEvents()
		{
			return Empty<EventInfo>.Array;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000DF23 File Offset: 0x0000C123
		public virtual PropertyInfo[] __GetDeclaredProperties()
		{
			return Empty<PropertyInfo>.Array;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000DF2C File Offset: 0x0000C12C
		public virtual CustomModifiers __GetCustomModifiers()
		{
			return default(CustomModifiers);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000DF44 File Offset: 0x0000C144
		[Obsolete("Please use __GetCustomModifiers() instead.")]
		public Type[] __GetRequiredCustomModifiers()
		{
			return this.__GetCustomModifiers().GetRequired();
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000DF60 File Offset: 0x0000C160
		[Obsolete("Please use __GetCustomModifiers() instead.")]
		public Type[] __GetOptionalCustomModifiers()
		{
			return this.__GetCustomModifiers().GetOptional();
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public virtual __StandAloneMethodSig __MethodSignature
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000DF7B File Offset: 0x0000C17B
		public bool HasElementType
		{
			get
			{
				return this.IsArray || this.IsByRef || this.IsPointer;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000DF95 File Offset: 0x0000C195
		public bool IsArray
		{
			get
			{
				return this.sigElementType == 20 || this.sigElementType == 29;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000DFAD File Offset: 0x0000C1AD
		public bool __IsVector
		{
			get
			{
				return this.sigElementType == 29;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000DFB9 File Offset: 0x0000C1B9
		public bool IsByRef
		{
			get
			{
				return this.sigElementType == 16;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000DFC5 File Offset: 0x0000C1C5
		public bool IsPointer
		{
			get
			{
				return this.sigElementType == 15;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000DFD1 File Offset: 0x0000C1D1
		public bool __IsFunctionPointer
		{
			get
			{
				return this.sigElementType == 27;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
		public virtual bool IsValueType
		{
			get
			{
				Type baseType = this.BaseType;
				return baseType != null && baseType.IsEnumOrValueType && !this.IsEnumOrValueType;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000E010 File Offset: 0x0000C210
		public bool IsGenericParameter
		{
			get
			{
				return this.sigElementType == 19 || this.sigElementType == 30;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual int GenericParameterPosition
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000E028 File Offset: 0x0000C228
		public Type UnderlyingSystemType
		{
			get
			{
				return this.underlyingType;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000055E7 File Offset: 0x000037E7
		public override Type DeclaringType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal virtual TypeName TypeName
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000E030 File Offset: 0x0000C230
		public string __Name
		{
			get
			{
				return this.TypeName.Name;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000E04C File Offset: 0x0000C24C
		public string __Namespace
		{
			get
			{
				return this.TypeName.Namespace;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000496 RID: 1174
		public abstract override string Name { get; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000E067 File Offset: 0x0000C267
		public virtual string Namespace
		{
			get
			{
				if (this.IsNested)
				{
					return this.DeclaringType.Namespace;
				}
				return this.__Namespace;
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal virtual int GetModuleBuilderToken()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000E083 File Offset: 0x0000C283
		public static bool operator ==(Type t1, Type t2)
		{
			return t1 == t2 || (t1 != null && t2 != null && t1.underlyingType == t2.underlyingType);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000E0A1 File Offset: 0x0000C2A1
		public static bool operator !=(Type t1, Type t2)
		{
			return !(t1 == t2);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000E0AD File Offset: 0x0000C2AD
		public bool Equals(Type type)
		{
			return this == type;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000E0B6 File Offset: 0x0000C2B6
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Type);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
		public override int GetHashCode()
		{
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType != this)
			{
				return underlyingSystemType.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000E0E9 File Offset: 0x0000C2E9
		public Type[] GenericTypeArguments
		{
			get
			{
				if (!this.IsConstructedGenericType)
				{
					return Type.EmptyTypes;
				}
				return this.GetGenericArguments();
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00008BC5 File Offset: 0x00006DC5
		public virtual Type[] GetGenericArguments()
		{
			return Type.EmptyTypes;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000E0FF File Offset: 0x0000C2FF
		public virtual CustomModifiers[] __GetGenericArgumentsCustomModifiers()
		{
			return Empty<CustomModifiers>.Array;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000E108 File Offset: 0x0000C308
		[Obsolete("Please use __GetGenericArgumentsCustomModifiers() instead")]
		public Type[][] __GetGenericArgumentsRequiredCustomModifiers()
		{
			CustomModifiers[] array = this.__GetGenericArgumentsCustomModifiers();
			Type[][] array2 = new Type[array.Length][];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = array[i].GetRequired();
			}
			return array2;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000E144 File Offset: 0x0000C344
		[Obsolete("Please use __GetGenericArgumentsCustomModifiers() instead")]
		public Type[][] __GetGenericArgumentsOptionalCustomModifiers()
		{
			CustomModifiers[] array = this.__GetGenericArgumentsCustomModifiers();
			Type[][] array2 = new Type[array.Length][];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = array[i].GetOptional();
			}
			return array2;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public virtual Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000E180 File Offset: 0x0000C380
		public StructLayoutAttribute StructLayoutAttribute
		{
			get
			{
				TypeAttributes typeAttributes = this.Attributes & TypeAttributes.LayoutMask;
				StructLayoutAttribute structLayoutAttribute;
				if (typeAttributes != TypeAttributes.AnsiClass)
				{
					if (typeAttributes != TypeAttributes.SequentialLayout)
					{
						if (typeAttributes != TypeAttributes.ExplicitLayout)
						{
							throw new BadImageFormatException();
						}
						structLayoutAttribute = new StructLayoutAttribute(LayoutKind.Explicit);
					}
					else
					{
						structLayoutAttribute = new StructLayoutAttribute(LayoutKind.Sequential);
					}
				}
				else
				{
					structLayoutAttribute = new StructLayoutAttribute(LayoutKind.Auto);
				}
				typeAttributes = (this.Attributes & TypeAttributes.CustomFormatClass);
				if (typeAttributes != TypeAttributes.AnsiClass)
				{
					if (typeAttributes != TypeAttributes.UnicodeClass)
					{
						if (typeAttributes != TypeAttributes.AutoClass)
						{
							structLayoutAttribute.CharSet = CharSet.None;
						}
						else
						{
							structLayoutAttribute.CharSet = CharSet.Auto;
						}
					}
					else
					{
						structLayoutAttribute.CharSet = CharSet.Unicode;
					}
				}
				else
				{
					structLayoutAttribute.CharSet = CharSet.Ansi;
				}
				if (!this.__GetLayout(out structLayoutAttribute.Pack, out structLayoutAttribute.Size))
				{
					structLayoutAttribute.Pack = 8;
				}
				return structLayoutAttribute;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000E226 File Offset: 0x0000C426
		public virtual bool __GetLayout(out int packingSize, out int typeSize)
		{
			packingSize = 0;
			typeSize = 0;
			return false;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000E230 File Offset: 0x0000C430
		public virtual bool ContainsGenericParameters
		{
			get
			{
				if (this.IsGenericParameter)
				{
					return true;
				}
				Type[] genericArguments = this.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (genericArguments[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public virtual Type[] GetGenericParameterConstraints()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public virtual CustomModifiers[] __GetGenericParameterConstraintCustomModifiers()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public virtual GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual int GetArrayRank()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual int[] __GetArraySizes()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual int[] __GetArrayLowerBounds()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000E269 File Offset: 0x0000C469
		public virtual Type GetEnumUnderlyingType()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException();
			}
			this.CheckBaked();
			return this.GetEnumUnderlyingTypeImpl();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000E288 File Offset: 0x0000C488
		internal Type GetEnumUnderlyingTypeImpl()
		{
			foreach (FieldInfo fieldInfo in this.__GetDeclaredFields())
			{
				if (!fieldInfo.IsStatic)
				{
					return fieldInfo.FieldType;
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		public string[] GetEnumNames()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException();
			}
			List<string> list = new List<string>();
			foreach (FieldInfo fieldInfo in this.__GetDeclaredFields())
			{
				if (fieldInfo.IsLiteral)
				{
					list.Add(fieldInfo.Name);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000E318 File Offset: 0x0000C518
		public string GetEnumName(object value)
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException();
			}
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			try
			{
				value = Convert.ChangeType(value, Type.GetTypeCode(this.GetEnumUnderlyingType()));
			}
			catch (FormatException)
			{
				throw new ArgumentException();
			}
			catch (OverflowException)
			{
				return null;
			}
			catch (InvalidCastException)
			{
				return null;
			}
			foreach (FieldInfo fieldInfo in this.__GetDeclaredFields())
			{
				if (fieldInfo.IsLiteral && fieldInfo.GetRawConstantValue().Equals(value))
				{
					return fieldInfo.Name;
				}
			}
			return null;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		public bool IsEnumDefined(object value)
		{
			if (value is string)
			{
				return Array.IndexOf<object>(this.GetEnumNames(), value) != -1;
			}
			if (!this.IsEnum)
			{
				throw new ArgumentException();
			}
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (Type.GetTypeCode(value.GetType()) != Type.GetTypeCode(this.GetEnumUnderlyingType()))
			{
				throw new ArgumentException();
			}
			foreach (FieldInfo fieldInfo in this.__GetDeclaredFields())
			{
				if (fieldInfo.IsLiteral && fieldInfo.GetRawConstantValue().Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000E453 File Offset: 0x0000C653
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004B6 RID: 1206
		public abstract string FullName { get; }

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000E45C File Offset: 0x0000C65C
		protected string GetFullName()
		{
			string text = TypeNameParser.Escape(this.__Namespace);
			Type declaringType = this.DeclaringType;
			if (declaringType == null)
			{
				if (text == null)
				{
					return this.Name;
				}
				return text + "." + this.Name;
			}
			else
			{
				if (text == null)
				{
					return declaringType.FullName + "+" + this.Name;
				}
				return string.Concat(new string[]
				{
					declaringType.FullName,
					"+",
					text,
					".",
					this.Name
				});
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000022F4 File Offset: 0x000004F4
		internal virtual bool IsModulePseudoType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal virtual Type GetGenericTypeArgument(int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		public MemberInfo[] GetDefaultMembers()
		{
			Type type = this.Module.universe.Import(typeof(DefaultMemberAttribute));
			foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(this))
			{
				if (customAttributeData.Constructor.DeclaringType.Equals(type))
				{
					return this.GetMember((string)customAttributeData.ConstructorArguments[0].Value);
				}
			}
			return Empty<MemberInfo>.Array;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000E58C File Offset: 0x0000C78C
		public MemberInfo[] GetMember(string name)
		{
			return this.GetMember(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000E597 File Offset: 0x0000C797
		public MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
		{
			return this.GetMember(name, MemberTypes.All, bindingAttr);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000E5A6 File Offset: 0x0000C7A6
		public MemberInfo[] GetMembers()
		{
			return this.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		public MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.AddRange(this.GetConstructors(bindingAttr));
			list.AddRange(this.GetMethods(bindingAttr));
			list.AddRange(this.GetFields(bindingAttr));
			list.AddRange(this.GetProperties(bindingAttr));
			list.AddRange(this.GetEvents(bindingAttr));
			list.AddRange(this.GetNestedTypes(bindingAttr));
			return list.ToArray();
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000E618 File Offset: 0x0000C818
		public MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			MemberFilter filter;
			if ((bindingAttr & BindingFlags.IgnoreCase) != BindingFlags.Default)
			{
				name = name.ToLowerInvariant();
				filter = ((MemberInfo member, object filterCriteria) => member.Name.ToLowerInvariant().Equals(filterCriteria));
			}
			else
			{
				filter = ((MemberInfo member, object filterCriteria) => member.Name.Equals(filterCriteria));
			}
			return this.FindMembers(type, bindingAttr, filter, name);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000E680 File Offset: 0x0000C880
		private static void AddMembers(List<MemberInfo> list, MemberFilter filter, object filterCriteria, MemberInfo[] members)
		{
			foreach (MemberInfo memberInfo in members)
			{
				if (filter == null || filter(memberInfo, filterCriteria))
				{
					list.Add(memberInfo);
				}
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
		public MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
		{
			List<MemberInfo> list = new List<MemberInfo>();
			if ((memberType & MemberTypes.Constructor) != (MemberTypes)0)
			{
				Type.AddMembers(list, filter, filterCriteria, this.GetConstructors(bindingAttr));
			}
			if ((memberType & MemberTypes.Method) != (MemberTypes)0)
			{
				Type.AddMembers(list, filter, filterCriteria, this.GetMethods(bindingAttr));
			}
			if ((memberType & MemberTypes.Field) != (MemberTypes)0)
			{
				Type.AddMembers(list, filter, filterCriteria, this.GetFields(bindingAttr));
			}
			if ((memberType & MemberTypes.Property) != (MemberTypes)0)
			{
				Type.AddMembers(list, filter, filterCriteria, this.GetProperties(bindingAttr));
			}
			if ((memberType & MemberTypes.Event) != (MemberTypes)0)
			{
				Type.AddMembers(list, filter, filterCriteria, this.GetEvents(bindingAttr));
			}
			if ((memberType & MemberTypes.NestedType) != (MemberTypes)0)
			{
				Type.AddMembers(list, filter, filterCriteria, this.GetNestedTypes(bindingAttr));
			}
			return list.ToArray();
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000E754 File Offset: 0x0000C954
		private MemberInfo[] GetMembers<T>()
		{
			if (typeof(T) == typeof(ConstructorInfo) || typeof(T) == typeof(MethodInfo))
			{
				return this.__GetDeclaredMethods();
			}
			if (typeof(T) == typeof(FieldInfo))
			{
				return this.__GetDeclaredFields();
			}
			if (typeof(T) == typeof(PropertyInfo))
			{
				return this.__GetDeclaredProperties();
			}
			if (typeof(T) == typeof(EventInfo))
			{
				return this.__GetDeclaredEvents();
			}
			if (typeof(T) == typeof(Type))
			{
				return this.__GetDeclaredTypes();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000E810 File Offset: 0x0000CA10
		private T[] GetMembers<T>(BindingFlags flags) where T : MemberInfo
		{
			this.CheckBaked();
			List<T> list = new List<T>();
			foreach (MemberInfo memberInfo in this.GetMembers<T>())
			{
				if (memberInfo is T && memberInfo.BindingFlagsMatch(flags))
				{
					list.Add((T)((object)memberInfo));
				}
			}
			if ((flags & BindingFlags.DeclaredOnly) == BindingFlags.Default)
			{
				Type baseType = this.BaseType;
				while (baseType != null)
				{
					baseType.CheckBaked();
					foreach (MemberInfo memberInfo2 in baseType.GetMembers<T>())
					{
						if (memberInfo2 is T && memberInfo2.BindingFlagsMatchInherited(flags))
						{
							list.Add((T)((object)memberInfo2.SetReflectedType(this)));
						}
					}
					baseType = baseType.BaseType;
				}
			}
			return list.ToArray();
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000E8D0 File Offset: 0x0000CAD0
		private T GetMemberByName<T>(string name, BindingFlags flags, Predicate<T> filter) where T : MemberInfo
		{
			this.CheckBaked();
			if ((flags & BindingFlags.IgnoreCase) != BindingFlags.Default)
			{
				name = name.ToLowerInvariant();
			}
			T t = default(T);
			foreach (MemberInfo memberInfo in this.GetMembers<T>())
			{
				if (memberInfo is T && memberInfo.BindingFlagsMatch(flags))
				{
					string text = memberInfo.Name;
					if ((flags & BindingFlags.IgnoreCase) != BindingFlags.Default)
					{
						text = text.ToLowerInvariant();
					}
					if (text == name && (filter == null || filter((T)((object)memberInfo))))
					{
						if (t != null)
						{
							throw new AmbiguousMatchException();
						}
						t = (T)((object)memberInfo);
					}
				}
			}
			if ((flags & BindingFlags.DeclaredOnly) == BindingFlags.Default)
			{
				Type baseType = this.BaseType;
				while ((t == null || typeof(T) == typeof(MethodInfo)) && baseType != null)
				{
					baseType.CheckBaked();
					foreach (MemberInfo memberInfo2 in baseType.GetMembers<T>())
					{
						if (memberInfo2 is T && memberInfo2.BindingFlagsMatchInherited(flags))
						{
							string text2 = memberInfo2.Name;
							if ((flags & BindingFlags.IgnoreCase) != BindingFlags.Default)
							{
								text2 = text2.ToLowerInvariant();
							}
							if (text2 == name && (filter == null || filter((T)((object)memberInfo2))))
							{
								if (t != null)
								{
									MethodInfo methodInfo;
									if (!((methodInfo = (t as MethodInfo)) != null) || !methodInfo.MethodSignature.MatchParameterTypes(((MethodBase)memberInfo2).MethodSignature))
									{
										throw new AmbiguousMatchException();
									}
								}
								else
								{
									t = (T)((object)memberInfo2.SetReflectedType(this));
								}
							}
						}
					}
					baseType = baseType.BaseType;
				}
			}
			return t;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000EA84 File Offset: 0x0000CC84
		private T GetMemberByName<T>(string name, BindingFlags flags) where T : MemberInfo
		{
			return this.GetMemberByName<T>(name, flags, null);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000EA8F File Offset: 0x0000CC8F
		public EventInfo GetEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000EA9A File Offset: 0x0000CC9A
		public EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.GetMemberByName<EventInfo>(name, bindingAttr);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		public EventInfo[] GetEvents()
		{
			return this.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000EAAE File Offset: 0x0000CCAE
		public EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.GetMembers<EventInfo>(bindingAttr);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000EAB7 File Offset: 0x0000CCB7
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000EAC2 File Offset: 0x0000CCC2
		public FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.GetMemberByName<FieldInfo>(name, bindingAttr);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000EACC File Offset: 0x0000CCCC
		public FieldInfo[] GetFields()
		{
			return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000EAD6 File Offset: 0x0000CCD6
		public FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.GetMembers<FieldInfo>(bindingAttr);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		public Type[] GetInterfaces()
		{
			List<Type> list = new List<Type>();
			Type type = this;
			while (type != null)
			{
				Type.AddInterfaces(list, type);
				type = type.BaseType;
			}
			return list.ToArray();
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000EB14 File Offset: 0x0000CD14
		private static void AddInterfaces(List<Type> list, Type type)
		{
			foreach (Type type2 in type.__GetDeclaredInterfaces())
			{
				if (!list.Contains(type2))
				{
					list.Add(type2);
					Type.AddInterfaces(list, type2);
				}
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000EB54 File Offset: 0x0000CD54
		public MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			this.CheckBaked();
			List<MethodInfo> list = new List<MethodInfo>();
			MethodBase[] array = this.__GetDeclaredMethods();
			for (int i = 0; i < array.Length; i++)
			{
				MethodInfo methodInfo = array[i] as MethodInfo;
				if (methodInfo != null && methodInfo.BindingFlagsMatch(bindingAttr))
				{
					list.Add(methodInfo);
				}
			}
			if ((bindingAttr & BindingFlags.DeclaredOnly) == BindingFlags.Default)
			{
				List<MethodInfo> list2 = new List<MethodInfo>();
				foreach (MethodInfo methodInfo2 in list)
				{
					if (methodInfo2.IsVirtual)
					{
						list2.Add(methodInfo2.GetBaseDefinition());
					}
				}
				Type baseType = this.BaseType;
				while (baseType != null)
				{
					baseType.CheckBaked();
					array = baseType.__GetDeclaredMethods();
					for (int i = 0; i < array.Length; i++)
					{
						MethodInfo methodInfo3 = array[i] as MethodInfo;
						if (methodInfo3 != null && methodInfo3.BindingFlagsMatchInherited(bindingAttr))
						{
							if (methodInfo3.IsVirtual)
							{
								if (list2 == null)
								{
									list2 = new List<MethodInfo>();
								}
								else if (list2.Contains(methodInfo3.GetBaseDefinition()))
								{
									goto IL_11A;
								}
								list2.Add(methodInfo3.GetBaseDefinition());
							}
							list.Add((MethodInfo)methodInfo3.SetReflectedType(this));
						}
						IL_11A:;
					}
					baseType = baseType.BaseType;
				}
			}
			return list.ToArray();
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		public MethodInfo[] GetMethods()
		{
			return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000ECBE File Offset: 0x0000CEBE
		public MethodInfo GetMethod(string name)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000ECC9 File Offset: 0x0000CEC9
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
		{
			return this.GetMemberByName<MethodInfo>(name, bindingAttr);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000ECD3 File Offset: 0x0000CED3
		public MethodInfo GetMethod(string name, Type[] types)
		{
			return this.GetMethod(name, types, null);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000ECDE File Offset: 0x0000CEDE
		public MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, types, modifiers);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			MethodInfo result;
			if ((result = this.GetMemberByName<MethodInfo>(name, bindingAttr, (MethodInfo method) => method.MethodSignature.MatchParameterTypes(types))) == null)
			{
				result = this.GetMethodWithBinder<MethodInfo>(name, bindingAttr, binder ?? Type.DefaultBinder, types, modifiers);
			}
			return result;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		private T GetMethodWithBinder<T>(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers) where T : MethodBase
		{
			List<MethodBase> list = new List<MethodBase>();
			this.GetMemberByName<T>(name, bindingAttr, delegate(T method)
			{
				list.Add(method);
				return false;
			});
			return (T)((object)binder.SelectMethod(bindingAttr, list.ToArray(), types, modifiers));
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000ED8A File Offset: 0x0000CF8A
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, bindingAttr, binder, types, modifiers);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000ED99 File Offset: 0x0000CF99
		public ConstructorInfo[] GetConstructors()
		{
			return this.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000EDA3 File Offset: 0x0000CFA3
		public ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.GetMembers<ConstructorInfo>(bindingAttr | BindingFlags.DeclaredOnly);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000EDAE File Offset: 0x0000CFAE
		public ConstructorInfo GetConstructor(Type[] types)
		{
			return this.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, CallingConventions.Standard, types, null);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000EDBC File Offset: 0x0000CFBC
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			ConstructorInfo constructorInfo = null;
			if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
			{
				constructorInfo = this.GetConstructorImpl(ConstructorInfo.ConstructorName, bindingAttr, binder, types, modifiers);
			}
			if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
			{
				ConstructorInfo constructorImpl = this.GetConstructorImpl(ConstructorInfo.TypeConstructorName, bindingAttr, binder, types, modifiers);
				if (constructorImpl != null)
				{
					if (constructorInfo != null)
					{
						throw new AmbiguousMatchException();
					}
					return constructorImpl;
				}
			}
			return constructorInfo;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000EE14 File Offset: 0x0000D014
		private ConstructorInfo GetConstructorImpl(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			ConstructorInfo result;
			if ((result = this.GetMemberByName<ConstructorInfo>(name, bindingAttr | BindingFlags.DeclaredOnly, (ConstructorInfo ctor) => ctor.MethodSignature.MatchParameterTypes(types))) == null)
			{
				result = this.GetMethodWithBinder<ConstructorInfo>(name, bindingAttr, binder ?? Type.DefaultBinder, types, modifiers);
			}
			return result;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000EE63 File Offset: 0x0000D063
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callingConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetConstructor(bindingAttr, binder, types, modifiers);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000EE71 File Offset: 0x0000D071
		internal Type ResolveNestedType(Module requester, TypeName typeName)
		{
			return this.FindNestedType(typeName) ?? this.Module.universe.GetMissingTypeOrThrow(requester, this.Module, this, typeName);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000EE98 File Offset: 0x0000D098
		internal virtual Type FindNestedType(TypeName name)
		{
			foreach (Type type in this.__GetDeclaredTypes())
			{
				if (type.TypeName == name)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		internal virtual Type FindNestedTypeIgnoreCase(TypeName lowerCaseName)
		{
			foreach (Type type in this.__GetDeclaredTypes())
			{
				if (type.TypeName.ToLowerInvariant() == lowerCaseName)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000EF0F File Offset: 0x0000D10F
		public Type GetNestedType(string name)
		{
			return this.GetNestedType(name, BindingFlags.Public);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000EF1A File Offset: 0x0000D11A
		public Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.GetMemberByName<Type>(name, bindingAttr | BindingFlags.DeclaredOnly);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000EF26 File Offset: 0x0000D126
		public Type[] GetNestedTypes()
		{
			return this.GetNestedTypes(BindingFlags.Public);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000EF30 File Offset: 0x0000D130
		public Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.GetMembers<Type>(bindingAttr | BindingFlags.DeclaredOnly);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000EF3B File Offset: 0x0000D13B
		public PropertyInfo[] GetProperties()
		{
			return this.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000EF45 File Offset: 0x0000D145
		public PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.GetMembers<PropertyInfo>(bindingAttr);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000EF4E File Offset: 0x0000D14E
		public PropertyInfo GetProperty(string name)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000EF59 File Offset: 0x0000D159
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
		{
			return this.GetMemberByName<PropertyInfo>(name, bindingAttr);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000EF64 File Offset: 0x0000D164
		public PropertyInfo GetProperty(string name, Type returnType)
		{
			return this.GetMemberByName<PropertyInfo>(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (PropertyInfo prop) => prop.PropertyType.Equals(returnType)) ?? this.GetPropertyWithBinder(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, Type.DefaultBinder, returnType, null, null);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
		public PropertyInfo GetProperty(string name, Type[] types)
		{
			return this.GetMemberByName<PropertyInfo>(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (PropertyInfo prop) => prop.PropertySignature.MatchParameterTypes(types)) ?? this.GetPropertyWithBinder(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, Type.DefaultBinder, null, types, null);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000EFF9 File Offset: 0x0000D1F9
		public PropertyInfo GetProperty(string name, Type returnType, Type[] types)
		{
			return this.GetProperty(name, returnType, types, null);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000F005 File Offset: 0x0000D205
		public PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, types, modifiers);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000F018 File Offset: 0x0000D218
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			PropertyInfo result;
			if ((result = this.GetMemberByName<PropertyInfo>(name, bindingAttr, (PropertyInfo prop) => prop.PropertyType.Equals(returnType) && prop.PropertySignature.MatchParameterTypes(types))) == null)
			{
				result = this.GetPropertyWithBinder(name, bindingAttr, binder ?? Type.DefaultBinder, returnType, types, modifiers);
			}
			return result;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000F074 File Offset: 0x0000D274
		private PropertyInfo GetPropertyWithBinder(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			List<PropertyInfo> list = new List<PropertyInfo>();
			this.GetMemberByName<PropertyInfo>(name, bindingAttr, delegate(PropertyInfo property)
			{
				list.Add(property);
				return false;
			});
			return binder.SelectProperty(bindingAttr, list.ToArray(), returnType, types, modifiers);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000F0BF File Offset: 0x0000D2BF
		public Type GetInterface(string name)
		{
			return this.GetInterface(name, false);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		public Type GetInterface(string name, bool ignoreCase)
		{
			if (ignoreCase)
			{
				name = name.ToLowerInvariant();
			}
			Type type = null;
			foreach (Type type2 in this.GetInterfaces())
			{
				string text = type2.FullName;
				if (ignoreCase)
				{
					text = text.ToLowerInvariant();
				}
				if (text == name)
				{
					if (type != null)
					{
						throw new AmbiguousMatchException();
					}
					type = type2;
				}
			}
			return type;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000F130 File Offset: 0x0000D330
		public Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
		{
			List<Type> list = new List<Type>();
			foreach (Type type in this.GetInterfaces())
			{
				if (filter(type, filterCriteria))
				{
					list.Add(type);
				}
			}
			return list.ToArray();
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000F173 File Offset: 0x0000D373
		public ConstructorInfo TypeInitializer
		{
			get
			{
				return this.GetConstructor(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000F184 File Offset: 0x0000D384
		public bool IsPrimitive
		{
			get
			{
				return this.__IsBuiltIn && ((this.sigElementType >= 2 && this.sigElementType <= 13) || this.sigElementType == 24 || this.sigElementType == 25);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000F1B9 File Offset: 0x0000D3B9
		public bool __IsBuiltIn
		{
			get
			{
				return (this.typeFlags & (Type.TypeFlags.PotentialBuiltIn | Type.TypeFlags.BuiltIn)) != Type.TypeFlags.ContainsMissingType_Unknown && ((this.typeFlags & Type.TypeFlags.BuiltIn) != Type.TypeFlags.ContainsMissingType_Unknown || this.ResolvePotentialBuiltInType());
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000F1E1 File Offset: 0x0000D3E1
		internal byte SigElementType
		{
			get
			{
				return this.sigElementType;
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000F1EC File Offset: 0x0000D3EC
		private bool ResolvePotentialBuiltInType()
		{
			this.typeFlags &= ~Type.TypeFlags.PotentialBuiltIn;
			Universe universe = this.Universe;
			string _Name = this.__Name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(_Name);
			if (num <= 2187444805U)
			{
				if (num <= 765439473U)
				{
					if (num <= 679076413U)
					{
						if (num != 423635464U)
						{
							if (num == 679076413U)
							{
								if (_Name == "Char")
								{
									return this.ResolvePotentialBuiltInType(universe.System_Char, 3);
								}
							}
						}
						else if (_Name == "SByte")
						{
							return this.ResolvePotentialBuiltInType(universe.System_SByte, 4);
						}
					}
					else if (num != 697196164U)
					{
						if (num == 765439473U)
						{
							if (_Name == "Int16")
							{
								return this.ResolvePotentialBuiltInType(universe.System_Int16, 6);
							}
						}
					}
					else if (_Name == "Int64")
					{
						return this.ResolvePotentialBuiltInType(universe.System_Int64, 10);
					}
				}
				else if (num <= 1324880019U)
				{
					if (num != 1323747186U)
					{
						if (num == 1324880019U)
						{
							if (_Name == "UInt64")
							{
								return this.ResolvePotentialBuiltInType(universe.System_UInt64, 11);
							}
						}
					}
					else if (_Name == "UInt16")
					{
						return this.ResolvePotentialBuiltInType(universe.System_UInt16, 7);
					}
				}
				else if (num != 1489158872U)
				{
					if (num != 1615808600U)
					{
						if (num == 2187444805U)
						{
							if (_Name == "UIntPtr")
							{
								return this.ResolvePotentialBuiltInType(universe.System_UIntPtr, 25);
							}
						}
					}
					else if (_Name == "String")
					{
						return this.ResolvePotentialBuiltInType(universe.System_String, 14);
					}
				}
				else if (_Name == "IntPtr")
				{
					return this.ResolvePotentialBuiltInType(universe.System_IntPtr, 24);
				}
			}
			else if (num <= 3370340735U)
			{
				if (num <= 2711245919U)
				{
					if (num != 2386971688U)
					{
						if (num == 2711245919U)
						{
							if (_Name == "Int32")
							{
								return this.ResolvePotentialBuiltInType(universe.System_Int32, 8);
							}
						}
					}
					else if (_Name == "Double")
					{
						return this.ResolvePotentialBuiltInType(universe.System_Double, 13);
					}
				}
				else if (num != 3145356080U)
				{
					if (num == 3370340735U)
					{
						if (_Name == "Void")
						{
							return this.ResolvePotentialBuiltInType(universe.System_Void, 1);
						}
					}
				}
				else if (_Name == "TypedReference")
				{
					return this.ResolvePotentialBuiltInType(universe.System_TypedReference, 22);
				}
			}
			else if (num <= 3538687084U)
			{
				if (num != 3409549631U)
				{
					if (num == 3538687084U)
					{
						if (_Name == "UInt32")
						{
							return this.ResolvePotentialBuiltInType(universe.System_UInt32, 9);
						}
					}
				}
				else if (_Name == "Byte")
				{
					return this.ResolvePotentialBuiltInType(universe.System_Byte, 5);
				}
			}
			else if (num != 3851314394U)
			{
				if (num != 3969205087U)
				{
					if (num == 4051133705U)
					{
						if (_Name == "Single")
						{
							return this.ResolvePotentialBuiltInType(universe.System_Single, 12);
						}
					}
				}
				else if (_Name == "Boolean")
				{
					return this.ResolvePotentialBuiltInType(universe.System_Boolean, 2);
				}
			}
			else if (_Name == "Object")
			{
				return this.ResolvePotentialBuiltInType(universe.System_Object, 28);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000F5C5 File Offset: 0x0000D7C5
		private bool ResolvePotentialBuiltInType(Type builtIn, byte elementType)
		{
			if (this == builtIn)
			{
				this.typeFlags |= Type.TypeFlags.BuiltIn;
				this.sigElementType = elementType;
				return true;
			}
			return false;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000F5EC File Offset: 0x0000D7EC
		public bool IsEnum
		{
			get
			{
				Type baseType = this.BaseType;
				return baseType != null && baseType.IsEnumOrValueType && baseType.__Name[0] == 'E';
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000F623 File Offset: 0x0000D823
		public bool IsSealed
		{
			get
			{
				return (this.Attributes & TypeAttributes.Sealed) > TypeAttributes.AnsiClass;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000F634 File Offset: 0x0000D834
		public bool IsAbstract
		{
			get
			{
				return (this.Attributes & TypeAttributes.Abstract) > TypeAttributes.AnsiClass;
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000F645 File Offset: 0x0000D845
		private bool CheckVisibility(TypeAttributes access)
		{
			return (this.Attributes & TypeAttributes.VisibilityMask) == access;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000F652 File Offset: 0x0000D852
		public bool IsPublic
		{
			get
			{
				return this.CheckVisibility(TypeAttributes.Public);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0000F65B File Offset: 0x0000D85B
		public bool IsNestedPublic
		{
			get
			{
				return this.CheckVisibility(TypeAttributes.NestedPublic);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000F664 File Offset: 0x0000D864
		public bool IsNestedPrivate
		{
			get
			{
				return this.CheckVisibility(TypeAttributes.NestedPrivate);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000F66D File Offset: 0x0000D86D
		public bool IsNestedFamily
		{
			get
			{
				return this.CheckVisibility(TypeAttributes.NestedFamily);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000F676 File Offset: 0x0000D876
		public bool IsNestedAssembly
		{
			get
			{
				return this.CheckVisibility(TypeAttributes.NestedAssembly);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000F67F File Offset: 0x0000D87F
		public bool IsNestedFamANDAssem
		{
			get
			{
				return this.CheckVisibility(TypeAttributes.NestedFamANDAssem);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000F688 File Offset: 0x0000D888
		public bool IsNestedFamORAssem
		{
			get
			{
				return this.CheckVisibility(TypeAttributes.VisibilityMask);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000F691 File Offset: 0x0000D891
		public bool IsNotPublic
		{
			get
			{
				return this.CheckVisibility(TypeAttributes.AnsiClass);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000F69A File Offset: 0x0000D89A
		public bool IsImport
		{
			get
			{
				return (this.Attributes & TypeAttributes.Import) > TypeAttributes.AnsiClass;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0000F6AB File Offset: 0x0000D8AB
		public bool IsCOMObject
		{
			get
			{
				return this.IsClass && this.IsImport;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0000F6BD File Offset: 0x0000D8BD
		public bool IsContextful
		{
			get
			{
				return this.IsSubclassOf(this.Module.universe.Import(typeof(ContextBoundObject)));
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000F6DF File Offset: 0x0000D8DF
		public bool IsMarshalByRef
		{
			get
			{
				return this.IsSubclassOf(this.Module.universe.Import(typeof(MarshalByRefObject)));
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000F701 File Offset: 0x0000D901
		public virtual bool IsVisible
		{
			get
			{
				return this.IsPublic || (this.IsNestedPublic && this.DeclaringType.IsVisible);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000F722 File Offset: 0x0000D922
		public bool IsAnsiClass
		{
			get
			{
				return (this.Attributes & TypeAttributes.CustomFormatClass) == TypeAttributes.AnsiClass;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000F733 File Offset: 0x0000D933
		public bool IsUnicodeClass
		{
			get
			{
				return (this.Attributes & TypeAttributes.CustomFormatClass) == TypeAttributes.UnicodeClass;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000F748 File Offset: 0x0000D948
		public bool IsAutoClass
		{
			get
			{
				return (this.Attributes & TypeAttributes.CustomFormatClass) == TypeAttributes.AutoClass;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000F75D File Offset: 0x0000D95D
		public bool IsAutoLayout
		{
			get
			{
				return (this.Attributes & TypeAttributes.LayoutMask) == TypeAttributes.AnsiClass;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000F76B File Offset: 0x0000D96B
		public bool IsLayoutSequential
		{
			get
			{
				return (this.Attributes & TypeAttributes.LayoutMask) == TypeAttributes.SequentialLayout;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000F779 File Offset: 0x0000D979
		public bool IsExplicitLayout
		{
			get
			{
				return (this.Attributes & TypeAttributes.LayoutMask) == TypeAttributes.ExplicitLayout;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000F788 File Offset: 0x0000D988
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & TypeAttributes.SpecialName) > TypeAttributes.AnsiClass;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000F799 File Offset: 0x0000D999
		public bool IsSerializable
		{
			get
			{
				return (this.Attributes & TypeAttributes.Serializable) > TypeAttributes.AnsiClass;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000F7AA File Offset: 0x0000D9AA
		public bool IsClass
		{
			get
			{
				return !this.IsInterface && !this.IsValueType;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0000F7BF File Offset: 0x0000D9BF
		public bool IsInterface
		{
			get
			{
				return (this.Attributes & TypeAttributes.ClassSemanticsMask) > TypeAttributes.AnsiClass;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000F7CD File Offset: 0x0000D9CD
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000F7DC File Offset: 0x0000D9DC
		public bool __ContainsMissingType
		{
			get
			{
				if ((this.typeFlags & Type.TypeFlags.ContainsMissingType_No) == Type.TypeFlags.ContainsMissingType_Unknown)
				{
					this.typeFlags |= Type.TypeFlags.ContainsMissingType_Pending;
					this.typeFlags = ((this.typeFlags & ~(Type.TypeFlags.ContainsMissingType_Pending | Type.TypeFlags.ContainsMissingType_Yes)) | (this.ContainsMissingTypeImpl ? Type.TypeFlags.ContainsMissingType_Yes : Type.TypeFlags.ContainsMissingType_No));
				}
				return (this.typeFlags & Type.TypeFlags.ContainsMissingType_No) == Type.TypeFlags.ContainsMissingType_Yes;
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000F844 File Offset: 0x0000DA44
		internal static bool ContainsMissingType(Type[] types)
		{
			if (types == null)
			{
				return false;
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i].__ContainsMissingType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000F874 File Offset: 0x0000DA74
		protected virtual bool ContainsMissingTypeImpl
		{
			get
			{
				return this.__IsMissing || Type.ContainsMissingType(this.GetGenericArguments()) || this.__GetCustomModifiers().ContainsMissingType;
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		public Type MakeArrayType()
		{
			return ArrayType.Make(this, default(CustomModifiers));
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
		public Type __MakeArrayType(CustomModifiers customModifiers)
		{
			return ArrayType.Make(this, customModifiers);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000F8CD File Offset: 0x0000DACD
		[Obsolete("Please use __MakeArrayType(CustomModifiers) instead.")]
		public Type __MakeArrayType(Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			return this.__MakeArrayType(CustomModifiers.FromReqOpt(requiredCustomModifiers, optionalCustomModifiers));
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000F8DC File Offset: 0x0000DADC
		public Type MakeArrayType(int rank)
		{
			return this.__MakeArrayType(rank, default(CustomModifiers));
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000F8F9 File Offset: 0x0000DAF9
		public Type __MakeArrayType(int rank, CustomModifiers customModifiers)
		{
			return MultiArrayType.Make(this, rank, Empty<int>.Array, new int[rank], customModifiers);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000F90E File Offset: 0x0000DB0E
		[Obsolete("Please use __MakeArrayType(int, CustomModifiers) instead.")]
		public Type __MakeArrayType(int rank, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			return this.__MakeArrayType(rank, CustomModifiers.FromReqOpt(requiredCustomModifiers, optionalCustomModifiers));
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000F91E File Offset: 0x0000DB1E
		public Type __MakeArrayType(int rank, int[] sizes, int[] lobounds, CustomModifiers customModifiers)
		{
			return MultiArrayType.Make(this, rank, sizes ?? Empty<int>.Array, lobounds ?? Empty<int>.Array, customModifiers);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000F93D File Offset: 0x0000DB3D
		[Obsolete("Please use __MakeArrayType(int, int[], int[], CustomModifiers) instead.")]
		public Type __MakeArrayType(int rank, int[] sizes, int[] lobounds, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			return this.__MakeArrayType(rank, sizes, lobounds, CustomModifiers.FromReqOpt(requiredCustomModifiers, optionalCustomModifiers));
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000F954 File Offset: 0x0000DB54
		public Type MakeByRefType()
		{
			return ByRefType.Make(this, default(CustomModifiers));
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000F970 File Offset: 0x0000DB70
		public Type __MakeByRefType(CustomModifiers customModifiers)
		{
			return ByRefType.Make(this, customModifiers);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000F979 File Offset: 0x0000DB79
		[Obsolete("Please use __MakeByRefType(CustomModifiers) instead.")]
		public Type __MakeByRefType(Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			return this.__MakeByRefType(CustomModifiers.FromReqOpt(requiredCustomModifiers, optionalCustomModifiers));
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000F988 File Offset: 0x0000DB88
		public Type MakePointerType()
		{
			return PointerType.Make(this, default(CustomModifiers));
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000F9A4 File Offset: 0x0000DBA4
		public Type __MakePointerType(CustomModifiers customModifiers)
		{
			return PointerType.Make(this, customModifiers);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000F9AD File Offset: 0x0000DBAD
		[Obsolete("Please use __MakeByRefType(CustomModifiers) instead.")]
		public Type __MakePointerType(Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			return this.__MakePointerType(CustomModifiers.FromReqOpt(requiredCustomModifiers, optionalCustomModifiers));
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000F9BC File Offset: 0x0000DBBC
		public Type MakeGenericType(params Type[] typeArguments)
		{
			return this.__MakeGenericType(typeArguments, null);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000F9C6 File Offset: 0x0000DBC6
		public Type __MakeGenericType(Type[] typeArguments, CustomModifiers[] customModifiers)
		{
			if (!this.__IsMissing && !this.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException();
			}
			return GenericTypeInstance.Make(this, Util.Copy(typeArguments), (customModifiers == null) ? null : ((CustomModifiers[])customModifiers.Clone()));
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000F9FC File Offset: 0x0000DBFC
		[Obsolete("Please use __MakeGenericType(Type[], CustomModifiers[]) instead.")]
		public Type __MakeGenericType(Type[] typeArguments, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			if (!this.__IsMissing && !this.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException();
			}
			CustomModifiers[] array = null;
			if (requiredCustomModifiers != null || optionalCustomModifiers != null)
			{
				array = new CustomModifiers[typeArguments.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = CustomModifiers.FromReqOpt(Util.NullSafeElementAt<Type[]>(requiredCustomModifiers, i), Util.NullSafeElementAt<Type[]>(optionalCustomModifiers, i));
				}
			}
			return GenericTypeInstance.Make(this, Util.Copy(typeArguments), array);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000FA68 File Offset: 0x0000DC68
		public static Type __GetSystemType(TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.Empty:
				return null;
			case TypeCode.Object:
				return typeof(object);
			case TypeCode.DBNull:
				return typeof(DBNull);
			case TypeCode.Boolean:
				return typeof(bool);
			case TypeCode.Char:
				return typeof(char);
			case TypeCode.SByte:
				return typeof(sbyte);
			case TypeCode.Byte:
				return typeof(byte);
			case TypeCode.Int16:
				return typeof(short);
			case TypeCode.UInt16:
				return typeof(ushort);
			case TypeCode.Int32:
				return typeof(int);
			case TypeCode.UInt32:
				return typeof(uint);
			case TypeCode.Int64:
				return typeof(long);
			case TypeCode.UInt64:
				return typeof(ulong);
			case TypeCode.Single:
				return typeof(float);
			case TypeCode.Double:
				return typeof(double);
			case TypeCode.Decimal:
				return typeof(decimal);
			case TypeCode.DateTime:
				return typeof(DateTime);
			case TypeCode.String:
				return typeof(string);
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000FB90 File Offset: 0x0000DD90
		public static TypeCode GetTypeCode(Type type)
		{
			if (type == null)
			{
				return TypeCode.Empty;
			}
			if (!type.__IsMissing && type.IsEnum)
			{
				type = type.GetEnumUnderlyingType();
			}
			Universe universe = type.Module.universe;
			if (type == universe.System_Boolean)
			{
				return TypeCode.Boolean;
			}
			if (type == universe.System_Char)
			{
				return TypeCode.Char;
			}
			if (type == universe.System_SByte)
			{
				return TypeCode.SByte;
			}
			if (type == universe.System_Byte)
			{
				return TypeCode.Byte;
			}
			if (type == universe.System_Int16)
			{
				return TypeCode.Int16;
			}
			if (type == universe.System_UInt16)
			{
				return TypeCode.UInt16;
			}
			if (type == universe.System_Int32)
			{
				return TypeCode.Int32;
			}
			if (type == universe.System_UInt32)
			{
				return TypeCode.UInt32;
			}
			if (type == universe.System_Int64)
			{
				return TypeCode.Int64;
			}
			if (type == universe.System_UInt64)
			{
				return TypeCode.UInt64;
			}
			if (type == universe.System_Single)
			{
				return TypeCode.Single;
			}
			if (type == universe.System_Double)
			{
				return TypeCode.Double;
			}
			if (type == universe.System_DateTime)
			{
				return TypeCode.DateTime;
			}
			if (type == universe.System_DBNull)
			{
				return TypeCode.DBNull;
			}
			if (type == universe.System_Decimal)
			{
				return TypeCode.Decimal;
			}
			if (type == universe.System_String)
			{
				return TypeCode.String;
			}
			if (type.__IsMissing)
			{
				throw new MissingMemberException(type);
			}
			return TypeCode.Object;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000FCE5 File Offset: 0x0000DEE5
		public Assembly Assembly
		{
			get
			{
				return this.Module.Assembly;
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000FCF4 File Offset: 0x0000DEF4
		public bool IsAssignableFrom(Type type)
		{
			if (this.Equals(type))
			{
				return true;
			}
			if (type == null)
			{
				return false;
			}
			if (this.IsArray && type.IsArray)
			{
				if (this.GetArrayRank() != type.GetArrayRank())
				{
					return false;
				}
				if (this.__IsVector && !type.__IsVector)
				{
					return false;
				}
				Type elementType = this.GetElementType();
				Type elementType2 = type.GetElementType();
				return elementType.IsValueType == elementType2.IsValueType && elementType.IsAssignableFrom(elementType2);
			}
			else
			{
				if (this.IsCovariant(type))
				{
					return true;
				}
				if (this.IsSealed)
				{
					return false;
				}
				if (this.IsInterface)
				{
					foreach (Type type2 in type.GetInterfaces())
					{
						if (this.Equals(type2) || this.IsCovariant(type2))
						{
							return true;
						}
					}
					return false;
				}
				if (type.IsInterface)
				{
					return this == this.Module.universe.System_Object;
				}
				if (type.IsPointer)
				{
					return this == this.Module.universe.System_Object || this == this.Module.universe.System_ValueType;
				}
				return type.IsSubclassOf(this);
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000FE20 File Offset: 0x0000E020
		private bool IsCovariant(Type other)
		{
			if (this.IsConstructedGenericType && other.IsConstructedGenericType && this.GetGenericTypeDefinition() == other.GetGenericTypeDefinition())
			{
				Type[] genericArguments = this.GetGenericTypeDefinition().GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					Type genericTypeArgument = this.GetGenericTypeArgument(i);
					Type genericTypeArgument2 = other.GetGenericTypeArgument(i);
					if (genericTypeArgument.IsValueType != genericTypeArgument2.IsValueType)
					{
						return false;
					}
					switch (genericArguments[i].GenericParameterAttributes & GenericParameterAttributes.VarianceMask)
					{
					case GenericParameterAttributes.None:
						if (genericTypeArgument != genericTypeArgument2)
						{
							return false;
						}
						break;
					case GenericParameterAttributes.Covariant:
						if (!genericTypeArgument.IsAssignableFrom(genericTypeArgument2))
						{
							return false;
						}
						break;
					case GenericParameterAttributes.Contravariant:
						if (!genericTypeArgument2.IsAssignableFrom(genericTypeArgument))
						{
							return false;
						}
						break;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000FED8 File Offset: 0x0000E0D8
		public bool IsSubclassOf(Type type)
		{
			Type baseType = this.BaseType;
			while (baseType != null)
			{
				if (baseType.Equals(type))
				{
					return true;
				}
				baseType = baseType.BaseType;
			}
			return false;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000FF0C File Offset: 0x0000E10C
		private bool IsDirectlyImplementedInterface(Type interfaceType)
		{
			foreach (Type type in this.__GetDeclaredInterfaces())
			{
				if (interfaceType.IsAssignableFrom(type))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000FF40 File Offset: 0x0000E140
		public InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			this.CheckBaked();
			InterfaceMapping interfaceMapping;
			interfaceMapping.InterfaceMethods = interfaceType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			interfaceMapping.InterfaceType = interfaceType;
			interfaceMapping.TargetMethods = new MethodInfo[interfaceMapping.InterfaceMethods.Length];
			interfaceMapping.TargetType = this;
			this.FillInInterfaceMethods(interfaceType, interfaceMapping.InterfaceMethods, interfaceMapping.TargetMethods);
			return interfaceMapping;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000FF9C File Offset: 0x0000E19C
		private void FillInInterfaceMethods(Type interfaceType, MethodInfo[] interfaceMethods, MethodInfo[] targetMethods)
		{
			this.FillInExplicitInterfaceMethods(interfaceMethods, targetMethods);
			bool flag = this.IsDirectlyImplementedInterface(interfaceType);
			if (flag)
			{
				this.FillInImplicitInterfaceMethods(interfaceMethods, targetMethods);
			}
			Type baseType = this.BaseType;
			if (baseType != null)
			{
				baseType.FillInInterfaceMethods(interfaceType, interfaceMethods, targetMethods);
				this.ReplaceOverriddenMethods(targetMethods);
			}
			if (flag)
			{
				Type baseType2 = this.BaseType;
				while (baseType2 != null && baseType2.Module == this.Module)
				{
					baseType2.FillInImplicitInterfaceMethods(interfaceMethods, targetMethods);
					baseType2 = baseType2.BaseType;
				}
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00010014 File Offset: 0x0000E214
		private void FillInImplicitInterfaceMethods(MethodInfo[] interfaceMethods, MethodInfo[] targetMethods)
		{
			MethodBase[] array = null;
			for (int i = 0; i < targetMethods.Length; i++)
			{
				if (targetMethods[i] == null)
				{
					if (array == null)
					{
						array = this.__GetDeclaredMethods();
					}
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].IsVirtual && array[j].Name == interfaceMethods[i].Name && array[j].MethodSignature.Equals(interfaceMethods[i].MethodSignature))
						{
							targetMethods[i] = (MethodInfo)array[j];
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001009C File Offset: 0x0000E29C
		private void ReplaceOverriddenMethods(MethodInfo[] baseMethods)
		{
			__MethodImplMap _MethodImplMap = this.__GetMethodImplMap();
			for (int i = 0; i < baseMethods.Length; i++)
			{
				if (baseMethods[i] != null && !baseMethods[i].IsFinal)
				{
					MethodInfo baseDefinition = baseMethods[i].GetBaseDefinition();
					for (int j = 0; j < _MethodImplMap.MethodDeclarations.Length; j++)
					{
						for (int k = 0; k < _MethodImplMap.MethodDeclarations[j].Length; k++)
						{
							if (_MethodImplMap.MethodDeclarations[j][k].GetBaseDefinition() == baseDefinition)
							{
								baseMethods[i] = _MethodImplMap.MethodBodies[j];
								goto IL_BF;
							}
						}
					}
					MethodInfo methodInfo = this.FindMethod(baseDefinition.Name, baseDefinition.MethodSignature) as MethodInfo;
					if (methodInfo != null && methodInfo.IsVirtual && !methodInfo.IsNewSlot)
					{
						baseMethods[i] = methodInfo;
					}
				}
				IL_BF:;
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00010178 File Offset: 0x0000E378
		internal void FillInExplicitInterfaceMethods(MethodInfo[] interfaceMethods, MethodInfo[] targetMethods)
		{
			__MethodImplMap _MethodImplMap = this.__GetMethodImplMap();
			for (int i = 0; i < _MethodImplMap.MethodDeclarations.Length; i++)
			{
				for (int j = 0; j < _MethodImplMap.MethodDeclarations[i].Length; j++)
				{
					int num = Array.IndexOf<MethodInfo>(interfaceMethods, _MethodImplMap.MethodDeclarations[i][j]);
					if (num != -1 && targetMethods[num] == null)
					{
						targetMethods[num] = _MethodImplMap.MethodBodies[i];
					}
				}
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000101DF File Offset: 0x0000E3DF
		Type IGenericContext.GetGenericTypeArgument(int index)
		{
			return this.GetGenericTypeArgument(index);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x000101E8 File Offset: 0x0000E3E8
		Type IGenericContext.GetGenericMethodArgument(int index)
		{
			throw new BadImageFormatException();
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000101EF File Offset: 0x0000E3EF
		Type IGenericBinder.BindTypeParameter(Type type)
		{
			return this.GetGenericTypeArgument(type.GenericParameterPosition);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000101E8 File Offset: 0x0000E3E8
		Type IGenericBinder.BindMethodParameter(Type type)
		{
			throw new BadImageFormatException();
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00010200 File Offset: 0x0000E400
		internal virtual Type BindTypeParameters(IGenericBinder binder)
		{
			if (this.IsGenericTypeDefinition)
			{
				Type[] genericArguments = this.GetGenericArguments();
				Type.InplaceBindTypeParameters(binder, genericArguments);
				return GenericTypeInstance.Make(this, genericArguments, null);
			}
			return this;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00010230 File Offset: 0x0000E430
		private static void InplaceBindTypeParameters(IGenericBinder binder, Type[] types)
		{
			for (int i = 0; i < types.Length; i++)
			{
				types[i] = types[i].BindTypeParameters(binder);
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00010258 File Offset: 0x0000E458
		internal virtual MethodBase FindMethod(string name, MethodSignature signature)
		{
			foreach (MethodBase methodBase in this.__GetDeclaredMethods())
			{
				if (methodBase.Name == name && methodBase.MethodSignature.Equals(signature))
				{
					return methodBase;
				}
			}
			return null;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000102A0 File Offset: 0x0000E4A0
		internal virtual FieldInfo FindField(string name, FieldSignature signature)
		{
			foreach (FieldInfo fieldInfo in this.__GetDeclaredFields())
			{
				if (fieldInfo.Name == name && fieldInfo.FieldSignature.Equals(signature))
				{
					return fieldInfo;
				}
			}
			return null;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x000102E8 File Offset: 0x0000E4E8
		internal bool IsAllowMultipleCustomAttribute
		{
			get
			{
				IList<CustomAttributeData> list = CustomAttributeData.__GetCustomAttributes(this, this.Module.universe.System_AttributeUsageAttribute, false);
				if (list.Count == 1)
				{
					foreach (CustomAttributeNamedArgument customAttributeNamedArgument in list[0].NamedArguments)
					{
						if (customAttributeNamedArgument.MemberInfo.Name == "AllowMultiple")
						{
							return (bool)customAttributeNamedArgument.TypedValue.Value;
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001038C File Offset: 0x0000E58C
		internal Type MarkNotValueType()
		{
			this.typeFlags |= Type.TypeFlags.NotValueType;
			return this;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001039E File Offset: 0x0000E59E
		internal Type MarkValueType()
		{
			this.typeFlags |= Type.TypeFlags.ValueType;
			return this;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000103B0 File Offset: 0x0000E5B0
		internal ConstructorInfo GetPseudoCustomAttributeConstructor(params Type[] parameterTypes)
		{
			Universe universe = this.Module.universe;
			MethodSignature signature = MethodSignature.MakeFromBuilder(universe.System_Void, parameterTypes, default(PackedCustomModifiers), CallingConventions.Standard | CallingConventions.HasThis, 0);
			return (ConstructorInfo)(this.FindMethod(".ctor", signature) ?? universe.GetMissingMethodOrThrow(null, this, ".ctor", signature));
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00010405 File Offset: 0x0000E605
		public MethodBase __CreateMissingMethod(string name, CallingConventions callingConvention, Type returnType, CustomModifiers returnTypeCustomModifiers, Type[] parameterTypes, CustomModifiers[] parameterTypeCustomModifiers)
		{
			return this.CreateMissingMethod(name, callingConvention, returnType, parameterTypes, PackedCustomModifiers.CreateFromExternal(returnTypeCustomModifiers, parameterTypeCustomModifiers, parameterTypes.Length));
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00010420 File Offset: 0x0000E620
		private MethodBase CreateMissingMethod(string name, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, PackedCustomModifiers customModifiers)
		{
			MethodSignature signature = new MethodSignature(returnType ?? this.Module.universe.System_Void, Util.Copy(parameterTypes), customModifiers, callingConvention, 0);
			MethodInfo methodInfo = new MissingMethod(this, name, signature);
			if (name == ".ctor" || name == ".cctor")
			{
				return new ConstructorInfoImpl(methodInfo);
			}
			return methodInfo;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00010480 File Offset: 0x0000E680
		[Obsolete("Please use __CreateMissingMethod(string, CallingConventions, Type, CustomModifiers, Type[], CustomModifiers[]) instead")]
		public MethodBase __CreateMissingMethod(string name, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			return this.CreateMissingMethod(name, callingConvention, returnType, parameterTypes, PackedCustomModifiers.CreateFromExternal(returnTypeOptionalCustomModifiers, returnTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, parameterTypeRequiredCustomModifiers, parameterTypes.Length));
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000104A9 File Offset: 0x0000E6A9
		public FieldInfo __CreateMissingField(string name, Type fieldType, CustomModifiers customModifiers)
		{
			return new MissingField(this, name, FieldSignature.Create(fieldType, customModifiers));
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000104B9 File Offset: 0x0000E6B9
		[Obsolete("Please use __CreateMissingField(string, Type, CustomModifiers) instead")]
		public FieldInfo __CreateMissingField(string name, Type fieldType, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			return this.__CreateMissingField(name, fieldType, CustomModifiers.FromReqOpt(requiredCustomModifiers, optionalCustomModifiers));
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000104CC File Offset: 0x0000E6CC
		public PropertyInfo __CreateMissingProperty(string name, CallingConventions callingConvention, Type propertyType, CustomModifiers propertyTypeCustomModifiers, Type[] parameterTypes, CustomModifiers[] parameterTypeCustomModifiers)
		{
			PropertySignature signature = PropertySignature.Create(callingConvention, propertyType, parameterTypes, PackedCustomModifiers.CreateFromExternal(propertyTypeCustomModifiers, parameterTypeCustomModifiers, Util.NullSafeLength<Type>(parameterTypes)));
			return new MissingProperty(this, name, signature);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00005936 File Offset: 0x00003B36
		internal virtual Type SetMetadataTokenForMissing(int token, int flags)
		{
			return this;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00005936 File Offset: 0x00003B36
		internal virtual Type SetCyclicTypeForwarder()
		{
			return this;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000104FC File Offset: 0x0000E6FC
		protected void MarkKnownType(string typeNamespace, string typeName)
		{
			if (typeNamespace == "System")
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(typeName);
				if (num <= 2187444805U)
				{
					if (num <= 765439473U)
					{
						if (num <= 423635464U)
						{
							if (num != 85497770U)
							{
								if (num != 423635464U)
								{
									return;
								}
								if (!(typeName == "SByte"))
								{
									return;
								}
							}
							else
							{
								if (!(typeName == "ValueType"))
								{
									return;
								}
								goto IL_276;
							}
						}
						else if (num != 679076413U)
						{
							if (num != 697196164U)
							{
								if (num != 765439473U)
								{
									return;
								}
								if (!(typeName == "Int16"))
								{
									return;
								}
							}
							else if (!(typeName == "Int64"))
							{
								return;
							}
						}
						else if (!(typeName == "Char"))
						{
							return;
						}
					}
					else if (num <= 1324880019U)
					{
						if (num != 1323747186U)
						{
							if (num != 1324880019U)
							{
								return;
							}
							if (!(typeName == "UInt64"))
							{
								return;
							}
						}
						else if (!(typeName == "UInt16"))
						{
							return;
						}
					}
					else if (num != 1489158872U)
					{
						if (num != 1615808600U)
						{
							if (num != 2187444805U)
							{
								return;
							}
							if (!(typeName == "UIntPtr"))
							{
								return;
							}
						}
						else if (!(typeName == "String"))
						{
							return;
						}
					}
					else if (!(typeName == "IntPtr"))
					{
						return;
					}
				}
				else if (num <= 3409549631U)
				{
					if (num <= 2711245919U)
					{
						if (num != 2386971688U)
						{
							if (num != 2711245919U)
							{
								return;
							}
							if (!(typeName == "Int32"))
							{
								return;
							}
						}
						else if (!(typeName == "Double"))
						{
							return;
						}
					}
					else if (num != 3145356080U)
					{
						if (num != 3370340735U)
						{
							if (num != 3409549631U)
							{
								return;
							}
							if (!(typeName == "Byte"))
							{
								return;
							}
						}
						else if (!(typeName == "Void"))
						{
							return;
						}
					}
					else if (!(typeName == "TypedReference"))
					{
						return;
					}
				}
				else if (num <= 3851314394U)
				{
					if (num != 3538687084U)
					{
						if (num != 3851314394U)
						{
							return;
						}
						if (!(typeName == "Object"))
						{
							return;
						}
					}
					else if (!(typeName == "UInt32"))
					{
						return;
					}
				}
				else if (num != 3897416224U)
				{
					if (num != 3969205087U)
					{
						if (num != 4051133705U)
						{
							return;
						}
						if (!(typeName == "Single"))
						{
							return;
						}
					}
					else if (!(typeName == "Boolean"))
					{
						return;
					}
				}
				else
				{
					if (!(typeName == "Enum"))
					{
						return;
					}
					goto IL_276;
				}
				this.typeFlags |= Type.TypeFlags.PotentialBuiltIn;
				return;
				IL_276:
				this.typeFlags |= Type.TypeFlags.PotentialEnumOrValueType;
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00010790 File Offset: 0x0000E990
		private bool ResolvePotentialEnumOrValueType()
		{
			if (this.Assembly == this.Universe.Mscorlib || this.Assembly.GetName().Name.Equals("mscorlib", StringComparison.OrdinalIgnoreCase) || this.Universe.Mscorlib.FindType(this.TypeName) == this)
			{
				this.typeFlags = ((this.typeFlags & ~Type.TypeFlags.PotentialEnumOrValueType) | Type.TypeFlags.EnumOrValueType);
				return true;
			}
			this.typeFlags &= ~Type.TypeFlags.PotentialEnumOrValueType;
			return false;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00010815 File Offset: 0x0000EA15
		private bool IsEnumOrValueType
		{
			get
			{
				return (this.typeFlags & (Type.TypeFlags.PotentialEnumOrValueType | Type.TypeFlags.EnumOrValueType)) != Type.TypeFlags.ContainsMissingType_Unknown && ((this.typeFlags & Type.TypeFlags.EnumOrValueType) != Type.TypeFlags.ContainsMissingType_Unknown || this.ResolvePotentialEnumOrValueType());
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00010837 File Offset: 0x0000EA37
		internal virtual Universe Universe
		{
			get
			{
				return this.Module.universe;
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00010844 File Offset: 0x0000EA44
		internal sealed override bool BindingFlagsMatch(BindingFlags flags)
		{
			return MemberInfo.BindingFlagsMatch(this.IsNestedPublic, flags, BindingFlags.Public, BindingFlags.NonPublic);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal sealed override MemberInfo SetReflectedType(Type type)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00010856 File Offset: 0x0000EA56
		internal override int GetCurrentToken()
		{
			return this.MetadataToken;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000055E7 File Offset: 0x000037E7
		internal sealed override List<CustomAttributeData> GetPseudoCustomAttributes(Type attributeType)
		{
			return null;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001085E File Offset: 0x0000EA5E
		public TypeInfo GetTypeInfo()
		{
			TypeInfo typeInfo = this as TypeInfo;
			if (typeInfo == null)
			{
				throw new MissingMemberException(this);
			}
			return typeInfo;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool __IsTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool __IsCyclicTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00010876 File Offset: 0x0000EA76
		// Note: this type is marked as 'beforefieldinit'.
		static Type()
		{
		}

		// Token: 0x040001FB RID: 507
		public static readonly Type[] EmptyTypes = Empty<Type>.Array;

		// Token: 0x040001FC RID: 508
		protected readonly Type underlyingType;

		// Token: 0x040001FD RID: 509
		protected Type.TypeFlags typeFlags;

		// Token: 0x040001FE RID: 510
		private byte sigElementType;

		// Token: 0x0200032C RID: 812
		[Flags]
		protected enum TypeFlags : ushort
		{
			// Token: 0x04000E55 RID: 3669
			IsGenericTypeDefinition = 1,
			// Token: 0x04000E56 RID: 3670
			HasNestedTypes = 2,
			// Token: 0x04000E57 RID: 3671
			Baked = 4,
			// Token: 0x04000E58 RID: 3672
			ValueType = 8,
			// Token: 0x04000E59 RID: 3673
			NotValueType = 16,
			// Token: 0x04000E5A RID: 3674
			PotentialEnumOrValueType = 32,
			// Token: 0x04000E5B RID: 3675
			EnumOrValueType = 64,
			// Token: 0x04000E5C RID: 3676
			NotGenericTypeDefinition = 128,
			// Token: 0x04000E5D RID: 3677
			ContainsMissingType_Unknown = 0,
			// Token: 0x04000E5E RID: 3678
			ContainsMissingType_Pending = 256,
			// Token: 0x04000E5F RID: 3679
			ContainsMissingType_Yes = 512,
			// Token: 0x04000E60 RID: 3680
			ContainsMissingType_No = 768,
			// Token: 0x04000E61 RID: 3681
			ContainsMissingType_Mask = 768,
			// Token: 0x04000E62 RID: 3682
			PotentialBuiltIn = 1024,
			// Token: 0x04000E63 RID: 3683
			BuiltIn = 2048
		}

		// Token: 0x0200032D RID: 813
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060025AA RID: 9642 RVA: 0x000B3FA7 File Offset: 0x000B21A7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060025AB RID: 9643 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c()
			{
			}

			// Token: 0x060025AC RID: 9644 RVA: 0x000B3FB3 File Offset: 0x000B21B3
			internal bool <GetMember>b__114_0(MemberInfo member, object filterCriteria)
			{
				return member.Name.ToLowerInvariant().Equals(filterCriteria);
			}

			// Token: 0x060025AD RID: 9645 RVA: 0x000B3FC6 File Offset: 0x000B21C6
			internal bool <GetMember>b__114_1(MemberInfo member, object filterCriteria)
			{
				return member.Name.Equals(filterCriteria);
			}

			// Token: 0x04000E64 RID: 3684
			public static readonly Type.<>c <>9 = new Type.<>c();

			// Token: 0x04000E65 RID: 3685
			public static MemberFilter <>9__114_0;

			// Token: 0x04000E66 RID: 3686
			public static MemberFilter <>9__114_1;
		}

		// Token: 0x0200032E RID: 814
		[CompilerGenerated]
		private sealed class <>c__DisplayClass137_0
		{
			// Token: 0x060025AE RID: 9646 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass137_0()
			{
			}

			// Token: 0x060025AF RID: 9647 RVA: 0x000B3FD4 File Offset: 0x000B21D4
			internal bool <GetMethod>b__0(MethodInfo method)
			{
				return method.MethodSignature.MatchParameterTypes(this.types);
			}

			// Token: 0x04000E67 RID: 3687
			public Type[] types;
		}

		// Token: 0x0200032F RID: 815
		[CompilerGenerated]
		private sealed class <>c__DisplayClass138_0<T> where T : MethodBase
		{
			// Token: 0x060025B0 RID: 9648 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass138_0()
			{
			}

			// Token: 0x060025B1 RID: 9649 RVA: 0x000B3FE7 File Offset: 0x000B21E7
			internal bool <GetMethodWithBinder>b__0(T method)
			{
				this.list.Add(method);
				return false;
			}

			// Token: 0x04000E68 RID: 3688
			public List<MethodBase> list;
		}

		// Token: 0x02000330 RID: 816
		[CompilerGenerated]
		private sealed class <>c__DisplayClass144_0
		{
			// Token: 0x060025B2 RID: 9650 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass144_0()
			{
			}

			// Token: 0x060025B3 RID: 9651 RVA: 0x000B3FFB File Offset: 0x000B21FB
			internal bool <GetConstructorImpl>b__0(ConstructorInfo ctor)
			{
				return ctor.MethodSignature.MatchParameterTypes(this.types);
			}

			// Token: 0x04000E69 RID: 3689
			public Type[] types;
		}

		// Token: 0x02000331 RID: 817
		[CompilerGenerated]
		private sealed class <>c__DisplayClass157_0
		{
			// Token: 0x060025B4 RID: 9652 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass157_0()
			{
			}

			// Token: 0x060025B5 RID: 9653 RVA: 0x000B400E File Offset: 0x000B220E
			internal bool <GetProperty>b__0(PropertyInfo prop)
			{
				return prop.PropertyType.Equals(this.returnType);
			}

			// Token: 0x04000E6A RID: 3690
			public Type returnType;
		}

		// Token: 0x02000332 RID: 818
		[CompilerGenerated]
		private sealed class <>c__DisplayClass158_0
		{
			// Token: 0x060025B6 RID: 9654 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass158_0()
			{
			}

			// Token: 0x060025B7 RID: 9655 RVA: 0x000B4021 File Offset: 0x000B2221
			internal bool <GetProperty>b__0(PropertyInfo prop)
			{
				return prop.PropertySignature.MatchParameterTypes(this.types);
			}

			// Token: 0x04000E6B RID: 3691
			public Type[] types;
		}

		// Token: 0x02000333 RID: 819
		[CompilerGenerated]
		private sealed class <>c__DisplayClass161_0
		{
			// Token: 0x060025B8 RID: 9656 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass161_0()
			{
			}

			// Token: 0x060025B9 RID: 9657 RVA: 0x000B4034 File Offset: 0x000B2234
			internal bool <GetProperty>b__0(PropertyInfo prop)
			{
				return prop.PropertyType.Equals(this.returnType) && prop.PropertySignature.MatchParameterTypes(this.types);
			}

			// Token: 0x04000E6C RID: 3692
			public Type returnType;

			// Token: 0x04000E6D RID: 3693
			public Type[] types;
		}

		// Token: 0x02000334 RID: 820
		[CompilerGenerated]
		private sealed class <>c__DisplayClass162_0
		{
			// Token: 0x060025BA RID: 9658 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass162_0()
			{
			}

			// Token: 0x060025BB RID: 9659 RVA: 0x000B405C File Offset: 0x000B225C
			internal bool <GetPropertyWithBinder>b__0(PropertyInfo property)
			{
				this.list.Add(property);
				return false;
			}

			// Token: 0x04000E6E RID: 3694
			public List<PropertyInfo> list;
		}
	}
}

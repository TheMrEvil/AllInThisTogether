using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection
{
	// Token: 0x020008FC RID: 2300
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ParameterInfo))]
	[ComVisible(true)]
	[Serializable]
	internal class RuntimeParameterInfo : ParameterInfo
	{
		// Token: 0x06004DE9 RID: 19945 RVA: 0x000F50DE File Offset: 0x000F32DE
		internal RuntimeParameterInfo(string name, Type type, int position, int attrs, object defaultValue, MemberInfo member, MarshalAsAttribute marshalAs)
		{
			this.NameImpl = name;
			this.ClassImpl = type;
			this.PositionImpl = position;
			this.AttrsImpl = (ParameterAttributes)attrs;
			this.DefaultValueImpl = defaultValue;
			this.MemberImpl = member;
			this.marshalAs = marshalAs;
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x000F511C File Offset: 0x000F331C
		internal static void FormatParameters(StringBuilder sb, ParameterInfo[] p, CallingConventions callingConvention, bool serialization)
		{
			for (int i = 0; i < p.Length; i++)
			{
				if (i > 0)
				{
					sb.Append(", ");
				}
				Type parameterType = p[i].ParameterType;
				string text = parameterType.FormatTypeName(serialization);
				if (parameterType.IsByRef && !serialization)
				{
					sb.Append(text.TrimEnd(new char[]
					{
						'&'
					}));
					sb.Append(" ByRef");
				}
				else
				{
					sb.Append(text);
				}
			}
			if ((callingConvention & CallingConventions.VarArgs) != (CallingConventions)0)
			{
				if (p.Length != 0)
				{
					sb.Append(", ");
				}
				sb.Append("...");
			}
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x000F51B0 File Offset: 0x000F33B0
		internal RuntimeParameterInfo(ParameterBuilder pb, Type type, MemberInfo member, int position)
		{
			this.ClassImpl = type;
			this.MemberImpl = member;
			if (pb != null)
			{
				this.NameImpl = pb.Name;
				this.PositionImpl = pb.Position - 1;
				this.AttrsImpl = (ParameterAttributes)pb.Attributes;
				return;
			}
			this.NameImpl = null;
			this.PositionImpl = position - 1;
			this.AttrsImpl = ParameterAttributes.None;
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x000F5213 File Offset: 0x000F3413
		internal static ParameterInfo New(ParameterBuilder pb, Type type, MemberInfo member, int position)
		{
			return new RuntimeParameterInfo(pb, type, member, position);
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x000F5220 File Offset: 0x000F3420
		internal RuntimeParameterInfo(ParameterInfo pinfo, Type type, MemberInfo member, int position)
		{
			this.ClassImpl = type;
			this.MemberImpl = member;
			if (pinfo != null)
			{
				this.NameImpl = pinfo.Name;
				this.PositionImpl = pinfo.Position - 1;
				this.AttrsImpl = pinfo.Attributes;
				return;
			}
			this.NameImpl = null;
			this.PositionImpl = position - 1;
			this.AttrsImpl = ParameterAttributes.None;
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x000F5284 File Offset: 0x000F3484
		internal RuntimeParameterInfo(ParameterInfo pinfo, MemberInfo member)
		{
			this.ClassImpl = pinfo.ParameterType;
			this.MemberImpl = member;
			this.NameImpl = pinfo.Name;
			this.PositionImpl = pinfo.Position;
			this.AttrsImpl = pinfo.Attributes;
			this.DefaultValueImpl = this.GetDefaultValueImpl(pinfo);
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x000F52DB File Offset: 0x000F34DB
		internal RuntimeParameterInfo(Type type, MemberInfo member, MarshalAsAttribute marshalAs)
		{
			this.ClassImpl = type;
			this.MemberImpl = member;
			this.NameImpl = null;
			this.PositionImpl = -1;
			this.AttrsImpl = ParameterAttributes.Retval;
			this.marshalAs = marshalAs;
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06004DF0 RID: 19952 RVA: 0x000F5310 File Offset: 0x000F3510
		public override object DefaultValue
		{
			get
			{
				if (this.ClassImpl == typeof(decimal) || this.ClassImpl == typeof(decimal?))
				{
					DecimalConstantAttribute[] array = (DecimalConstantAttribute[])this.GetCustomAttributes(typeof(DecimalConstantAttribute), false);
					if (array.Length != 0)
					{
						return array[0].Value;
					}
				}
				else if (this.ClassImpl == typeof(DateTime) || this.ClassImpl == typeof(DateTime?))
				{
					DateTimeConstantAttribute[] array2 = (DateTimeConstantAttribute[])this.GetCustomAttributes(typeof(DateTimeConstantAttribute), false);
					if (array2.Length != 0)
					{
						return array2[0].Value;
					}
				}
				return this.DefaultValueImpl;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06004DF1 RID: 19953 RVA: 0x000F53CC File Offset: 0x000F35CC
		public override object RawDefaultValue
		{
			get
			{
				if (this.DefaultValue != null && this.DefaultValue.GetType().IsEnum)
				{
					return ((Enum)this.DefaultValue).GetValue();
				}
				return this.DefaultValue;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06004DF2 RID: 19954 RVA: 0x000F5400 File Offset: 0x000F3600
		public override int MetadataToken
		{
			get
			{
				if (this.MemberImpl is PropertyInfo)
				{
					PropertyInfo propertyInfo = (PropertyInfo)this.MemberImpl;
					MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = propertyInfo.GetSetMethod(true);
					}
					return methodInfo.GetParametersInternal()[this.PositionImpl].MetadataToken;
				}
				if (this.MemberImpl is MethodBase)
				{
					return this.GetMetadataToken();
				}
				string str = "Can't produce MetadataToken for member of type ";
				Type type = this.MemberImpl.GetType();
				throw new ArgumentException(str + ((type != null) ? type.ToString() : null));
			}
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x000F548C File Offset: 0x000F368C
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, false);
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x000F5495 File Offset: 0x000F3695
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, false);
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x000F549F File Offset: 0x000F369F
		internal object GetDefaultValueImpl(ParameterInfo pinfo)
		{
			return typeof(ParameterInfo).GetField("DefaultValueImpl", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(pinfo);
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x00052A66 File Offset: 0x00050C66
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004DF7 RID: 19959 RVA: 0x000F54BD File Offset: 0x000F36BD
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06004DF8 RID: 19960
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetMetadataToken();

		// Token: 0x06004DF9 RID: 19961 RVA: 0x000F54C5 File Offset: 0x000F36C5
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.GetCustomModifiers(true);
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x000F54D0 File Offset: 0x000F36D0
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			if (base.IsIn)
			{
				num++;
			}
			if (base.IsOut)
			{
				num++;
			}
			if (base.IsOptional)
			{
				num++;
			}
			if (this.marshalAs != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if (base.IsIn)
			{
				array[num++] = new InAttribute();
			}
			if (base.IsOut)
			{
				array[num++] = new OutAttribute();
			}
			if (base.IsOptional)
			{
				array[num++] = new OptionalAttribute();
			}
			if (this.marshalAs != null)
			{
				array[num++] = this.marshalAs.Copy();
			}
			return array;
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x000F5574 File Offset: 0x000F3774
		internal CustomAttributeData[] GetPseudoCustomAttributesData()
		{
			int num = 0;
			if (base.IsIn)
			{
				num++;
			}
			if (base.IsOut)
			{
				num++;
			}
			if (base.IsOptional)
			{
				num++;
			}
			if (this.marshalAs != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			CustomAttributeData[] array = new CustomAttributeData[num];
			num = 0;
			if (base.IsIn)
			{
				array[num++] = new CustomAttributeData(typeof(InAttribute).GetConstructor(Type.EmptyTypes));
			}
			if (base.IsOut)
			{
				array[num++] = new CustomAttributeData(typeof(OutAttribute).GetConstructor(Type.EmptyTypes));
			}
			if (base.IsOptional)
			{
				array[num++] = new CustomAttributeData(typeof(OptionalAttribute).GetConstructor(Type.EmptyTypes));
			}
			if (this.marshalAs != null)
			{
				CustomAttributeTypedArgument[] ctorArgs = new CustomAttributeTypedArgument[]
				{
					new CustomAttributeTypedArgument(typeof(UnmanagedType), this.marshalAs.Value)
				};
				array[num++] = new CustomAttributeData(typeof(MarshalAsAttribute).GetConstructor(new Type[]
				{
					typeof(UnmanagedType)
				}), ctorArgs, EmptyArray<CustomAttributeNamedArgument>.Value);
			}
			return array;
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x000F56A3 File Offset: 0x000F38A3
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.GetCustomModifiers(false);
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06004DFD RID: 19965 RVA: 0x000F56AC File Offset: 0x000F38AC
		public override bool HasDefaultValue
		{
			get
			{
				object defaultValue = this.DefaultValue;
				return defaultValue == null || (!(defaultValue.GetType() == typeof(DBNull)) && !(defaultValue.GetType() == typeof(Missing)));
			}
		}

		// Token: 0x06004DFE RID: 19966
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type[] GetTypeModifiers(Type type, MemberInfo member, int position, bool optional);

		// Token: 0x06004DFF RID: 19967 RVA: 0x000F56F6 File Offset: 0x000F38F6
		internal static ParameterInfo New(ParameterInfo pinfo, Type type, MemberInfo member, int position)
		{
			return new RuntimeParameterInfo(pinfo, type, member, position);
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x000F5701 File Offset: 0x000F3901
		internal static ParameterInfo New(ParameterInfo pinfo, MemberInfo member)
		{
			return new RuntimeParameterInfo(pinfo, member);
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x000F570A File Offset: 0x000F390A
		internal static ParameterInfo New(Type type, MemberInfo member, MarshalAsAttribute marshalAs)
		{
			return new RuntimeParameterInfo(type, member, marshalAs);
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x000F5714 File Offset: 0x000F3914
		private Type[] GetCustomModifiers(bool optional)
		{
			return RuntimeParameterInfo.GetTypeModifiers(this.ParameterType, this.Member, this.Position, optional) ?? Type.EmptyTypes;
		}

		// Token: 0x0400306B RID: 12395
		internal MarshalAsAttribute marshalAs;
	}
}

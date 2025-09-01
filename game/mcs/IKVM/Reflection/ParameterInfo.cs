using System;
using System.Collections.Generic;

namespace IKVM.Reflection
{
	// Token: 0x02000050 RID: 80
	public abstract class ParameterInfo : ICustomAttributeProvider
	{
		// Token: 0x060003BD RID: 957 RVA: 0x00002CCC File Offset: 0x00000ECC
		internal ParameterInfo()
		{
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000AFA8 File Offset: 0x000091A8
		public sealed override bool Equals(object obj)
		{
			ParameterInfo parameterInfo = obj as ParameterInfo;
			return parameterInfo != null && parameterInfo.Member == this.Member && parameterInfo.Position == this.Position;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000AFE8 File Offset: 0x000091E8
		public sealed override int GetHashCode()
		{
			return this.Member.GetHashCode() * 1777 + this.Position;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00008A31 File Offset: 0x00006C31
		public static bool operator ==(ParameterInfo p1, ParameterInfo p2)
		{
			return p1 == p2 || (p1 != null && p1.Equals(p2));
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000B002 File Offset: 0x00009202
		public static bool operator !=(ParameterInfo p1, ParameterInfo p2)
		{
			return !(p1 == p2);
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003C2 RID: 962
		public abstract string Name { get; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003C3 RID: 963
		public abstract Type ParameterType { get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003C4 RID: 964
		public abstract ParameterAttributes Attributes { get; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003C5 RID: 965
		public abstract int Position { get; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003C6 RID: 966
		public abstract object RawDefaultValue { get; }

		// Token: 0x060003C7 RID: 967
		public abstract CustomModifiers __GetCustomModifiers();

		// Token: 0x060003C8 RID: 968
		public abstract bool __TryGetFieldMarshal(out FieldMarshal fieldMarshal);

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003C9 RID: 969
		public abstract MemberInfo Member { get; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003CA RID: 970
		public abstract int MetadataToken { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003CB RID: 971
		internal abstract Module Module { get; }

		// Token: 0x060003CC RID: 972 RVA: 0x0000B010 File Offset: 0x00009210
		public Type[] GetOptionalCustomModifiers()
		{
			return this.__GetCustomModifiers().GetOptional();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000B02C File Offset: 0x0000922C
		public Type[] GetRequiredCustomModifiers()
		{
			return this.__GetCustomModifiers().GetRequired();
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000B047 File Offset: 0x00009247
		public bool IsIn
		{
			get
			{
				return (this.Attributes & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000B054 File Offset: 0x00009254
		public bool IsOut
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000B061 File Offset: 0x00009261
		public bool IsLcid
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Lcid) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000B06E File Offset: 0x0000926E
		public bool IsRetval
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Retval) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000B07B File Offset: 0x0000927B
		public bool IsOptional
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000B089 File Offset: 0x00009289
		public bool HasDefaultValue
		{
			get
			{
				return (this.Attributes & ParameterAttributes.HasDefault) > ParameterAttributes.None;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000B09A File Offset: 0x0000929A
		public bool IsDefined(Type attributeType, bool inherit)
		{
			return CustomAttributeData.__GetCustomAttributes(this, attributeType, inherit).Count != 0;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000B0AC File Offset: 0x000092AC
		public IList<CustomAttributeData> __GetCustomAttributes(Type attributeType, bool inherit)
		{
			return CustomAttributeData.__GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000B0B6 File Offset: 0x000092B6
		public IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000B0BE File Offset: 0x000092BE
		public IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}
	}
}

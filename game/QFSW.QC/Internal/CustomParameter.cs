using System;
using System.Collections.Generic;
using System.Reflection;

namespace QFSW.QC.Internal
{
	// Token: 0x02000064 RID: 100
	internal class CustomParameter : ParameterInfo
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00009DAB File Offset: 0x00007FAB
		public CustomParameter(ParameterInfo internalParameter, Type typeOverride, string nameOverride)
		{
			this._typeOverride = typeOverride;
			this._nameOverride = nameOverride;
			this._internalParameter = internalParameter;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009DC8 File Offset: 0x00007FC8
		public CustomParameter(ParameterInfo internalParameter, string nameOverride) : this(internalParameter, internalParameter.ParameterType, nameOverride)
		{
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00009DD8 File Offset: 0x00007FD8
		public override Type ParameterType
		{
			get
			{
				return this._typeOverride;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00009DE0 File Offset: 0x00007FE0
		public override string Name
		{
			get
			{
				return this._nameOverride;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00009DE8 File Offset: 0x00007FE8
		public override ParameterAttributes Attributes
		{
			get
			{
				return this._internalParameter.Attributes;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override object DefaultValue
		{
			get
			{
				return this._internalParameter.DefaultValue;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00009E02 File Offset: 0x00008002
		public override bool Equals(object obj)
		{
			return this._internalParameter.Equals(obj);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00009E10 File Offset: 0x00008010
		public override IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this._internalParameter.CustomAttributes;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00009E1D File Offset: 0x0000801D
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._internalParameter.GetCustomAttributes(inherit);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00009E2B File Offset: 0x0000802B
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._internalParameter.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00009E3A File Offset: 0x0000803A
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return this._internalParameter.GetCustomAttributesData();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00009E47 File Offset: 0x00008047
		public override int GetHashCode()
		{
			return this._internalParameter.GetHashCode();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00009E54 File Offset: 0x00008054
		public override Type[] GetOptionalCustomModifiers()
		{
			return this._internalParameter.GetOptionalCustomModifiers();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00009E61 File Offset: 0x00008061
		public override Type[] GetRequiredCustomModifiers()
		{
			return this._internalParameter.GetRequiredCustomModifiers();
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00009E6E File Offset: 0x0000806E
		public override bool HasDefaultValue
		{
			get
			{
				return this._internalParameter.HasDefaultValue;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009E7B File Offset: 0x0000807B
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._internalParameter.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00009E8A File Offset: 0x0000808A
		public override MemberInfo Member
		{
			get
			{
				return this._internalParameter.Member;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00009E97 File Offset: 0x00008097
		public override int MetadataToken
		{
			get
			{
				return this._internalParameter.MetadataToken;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00009EA4 File Offset: 0x000080A4
		public override int Position
		{
			get
			{
				return this._internalParameter.Position;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00009EB1 File Offset: 0x000080B1
		public override object RawDefaultValue
		{
			get
			{
				return this._internalParameter.RawDefaultValue;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009EBE File Offset: 0x000080BE
		public override string ToString()
		{
			return this._internalParameter.ToString();
		}

		// Token: 0x04000142 RID: 322
		private readonly ParameterInfo _internalParameter;

		// Token: 0x04000143 RID: 323
		private readonly Type _typeOverride;

		// Token: 0x04000144 RID: 324
		private readonly string _nameOverride;
	}
}

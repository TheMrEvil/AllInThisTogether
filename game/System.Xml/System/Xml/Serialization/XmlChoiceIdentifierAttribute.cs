using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the member can be further detected by using an enumeration.</summary>
	// Token: 0x020002CD RID: 717
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false)]
	public class XmlChoiceIdentifierAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlChoiceIdentifierAttribute" /> class.</summary>
		// Token: 0x06001B4A RID: 6986 RVA: 0x000021EA File Offset: 0x000003EA
		public XmlChoiceIdentifierAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlChoiceIdentifierAttribute" /> class.</summary>
		/// <param name="name">The member name that returns the enumeration used to detect a choice. </param>
		// Token: 0x06001B4B RID: 6987 RVA: 0x0009BD6D File Offset: 0x00099F6D
		public XmlChoiceIdentifierAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets or sets the name of the field that returns the enumeration to use when detecting types.</summary>
		/// <returns>The name of a field that returns an enumeration.</returns>
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0009BD7C File Offset: 0x00099F7C
		// (set) Token: 0x06001B4D RID: 6989 RVA: 0x0009BD92 File Offset: 0x00099F92
		public string MemberName
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x0009BD9B File Offset: 0x00099F9B
		// (set) Token: 0x06001B4F RID: 6991 RVA: 0x0009BDA3 File Offset: 0x00099FA3
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
			set
			{
				this.memberInfo = value;
			}
		}

		// Token: 0x040019E7 RID: 6631
		private string name;

		// Token: 0x040019E8 RID: 6632
		private MemberInfo memberInfo;
	}
}

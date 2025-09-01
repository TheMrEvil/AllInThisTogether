using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	public class SerializableEnum
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000EF68 File Offset: 0x0000D168
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000EF92 File Offset: 0x0000D192
		public Enum value
		{
			get
			{
				object obj;
				if (Enum.TryParse(this.m_EnumType, this.m_EnumValueAsString, out obj))
				{
					return (Enum)obj;
				}
				return null;
			}
			set
			{
				this.m_EnumValueAsString = value.ToString();
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
		public SerializableEnum(Type enumType)
		{
			this.m_EnumType = enumType;
			this.m_EnumValueAsString = Enum.GetNames(enumType)[0];
		}

		// Token: 0x04000209 RID: 521
		[SerializeField]
		private string m_EnumValueAsString;

		// Token: 0x0400020A RID: 522
		[SerializeField]
		private Type m_EnumType;
	}
}

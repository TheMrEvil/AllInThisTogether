using System;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x0200039A RID: 922
	public class JSONNull : JSONNode
	{
		// Token: 0x06001E64 RID: 7780 RVA: 0x000B6B53 File Offset: 0x000B4D53
		public static JSONNull CreateOrGet()
		{
			if (JSONNull.reuseSameInstance)
			{
				return JSONNull.m_StaticInstance;
			}
			return new JSONNull();
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000B6B67 File Offset: 0x000B4D67
		private JSONNull()
		{
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x000B6B6F File Offset: 0x000B4D6F
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.NullValue;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x000B6B72 File Offset: 0x000B4D72
		public override bool IsNull
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x000B6B78 File Offset: 0x000B4D78
		public override JSONNode.Enumerator GetEnumerator()
		{
			return default(JSONNode.Enumerator);
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x000B6B8E File Offset: 0x000B4D8E
		// (set) Token: 0x06001E6A RID: 7786 RVA: 0x000B6B95 File Offset: 0x000B4D95
		public override string Value
		{
			get
			{
				return "null";
			}
			set
			{
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x000B6B97 File Offset: 0x000B4D97
		// (set) Token: 0x06001E6C RID: 7788 RVA: 0x000B6B9A File Offset: 0x000B4D9A
		public override bool AsBool
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x000B6B9C File Offset: 0x000B4D9C
		public override JSONNode Clone()
		{
			return JSONNull.CreateOrGet();
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x000B6BA3 File Offset: 0x000B4DA3
		public override bool Equals(object obj)
		{
			return this == obj || obj is JSONNull;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x000B6BB4 File Offset: 0x000B4DB4
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x000B6BB7 File Offset: 0x000B4DB7
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append("null");
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x000B6BC5 File Offset: 0x000B4DC5
		// Note: this type is marked as 'beforefieldinit'.
		static JSONNull()
		{
		}

		// Token: 0x04001ECA RID: 7882
		private static JSONNull m_StaticInstance = new JSONNull();

		// Token: 0x04001ECB RID: 7883
		public static bool reuseSameInstance = true;
	}
}

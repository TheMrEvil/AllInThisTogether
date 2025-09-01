using System;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000399 RID: 921
	public class JSONBool : JSONNode
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x000B6A85 File Offset: 0x000B4C85
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.Boolean;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x000B6A88 File Offset: 0x000B4C88
		public override bool IsBoolean
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x000B6A8C File Offset: 0x000B4C8C
		public override JSONNode.Enumerator GetEnumerator()
		{
			return default(JSONNode.Enumerator);
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x000B6AA2 File Offset: 0x000B4CA2
		// (set) Token: 0x06001E5B RID: 7771 RVA: 0x000B6AB0 File Offset: 0x000B4CB0
		public override string Value
		{
			get
			{
				return this.m_Data.ToString();
			}
			set
			{
				bool data;
				if (bool.TryParse(value, out data))
				{
					this.m_Data = data;
				}
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x000B6ACE File Offset: 0x000B4CCE
		// (set) Token: 0x06001E5D RID: 7773 RVA: 0x000B6AD6 File Offset: 0x000B4CD6
		public override bool AsBool
		{
			get
			{
				return this.m_Data;
			}
			set
			{
				this.m_Data = value;
			}
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x000B6ADF File Offset: 0x000B4CDF
		public JSONBool(bool aData)
		{
			this.m_Data = aData;
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x000B6AEE File Offset: 0x000B4CEE
		public JSONBool(string aData)
		{
			this.Value = aData;
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x000B6AFD File Offset: 0x000B4CFD
		public override JSONNode Clone()
		{
			return new JSONBool(this.m_Data);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x000B6B0A File Offset: 0x000B4D0A
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append(this.m_Data ? "true" : "false");
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x000B6B27 File Offset: 0x000B4D27
		public override bool Equals(object obj)
		{
			return obj != null && obj is bool && this.m_Data == (bool)obj;
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x000B6B46 File Offset: 0x000B4D46
		public override int GetHashCode()
		{
			return this.m_Data.GetHashCode();
		}

		// Token: 0x04001EC9 RID: 7881
		private bool m_Data;
	}
}

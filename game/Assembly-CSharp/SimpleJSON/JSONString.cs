using System;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000397 RID: 919
	public class JSONString : JSONNode
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x000B6833 File Offset: 0x000B4A33
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.String;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x000B6836 File Offset: 0x000B4A36
		public override bool IsString
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000B683C File Offset: 0x000B4A3C
		public override JSONNode.Enumerator GetEnumerator()
		{
			return default(JSONNode.Enumerator);
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x000B6852 File Offset: 0x000B4A52
		// (set) Token: 0x06001E41 RID: 7745 RVA: 0x000B685A File Offset: 0x000B4A5A
		public override string Value
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

		// Token: 0x06001E42 RID: 7746 RVA: 0x000B6863 File Offset: 0x000B4A63
		public JSONString(string aData)
		{
			this.m_Data = aData;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000B6872 File Offset: 0x000B4A72
		public override JSONNode Clone()
		{
			return new JSONString(this.m_Data);
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000B687F File Offset: 0x000B4A7F
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append('"').Append(JSONNode.Escape(this.m_Data)).Append('"');
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x000B68A4 File Offset: 0x000B4AA4
		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				return true;
			}
			string text = obj as string;
			if (text != null)
			{
				return this.m_Data == text;
			}
			JSONString jsonstring = obj as JSONString;
			return jsonstring != null && this.m_Data == jsonstring.m_Data;
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x000B68F6 File Offset: 0x000B4AF6
		public override int GetHashCode()
		{
			return this.m_Data.GetHashCode();
		}

		// Token: 0x04001EC7 RID: 7879
		private string m_Data;
	}
}

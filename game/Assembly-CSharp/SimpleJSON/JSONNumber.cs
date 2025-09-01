using System;
using System.Globalization;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000398 RID: 920
	public class JSONNumber : JSONNode
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x000B6903 File Offset: 0x000B4B03
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.Number;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x000B6906 File Offset: 0x000B4B06
		public override bool IsNumber
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000B690C File Offset: 0x000B4B0C
		public override JSONNode.Enumerator GetEnumerator()
		{
			return default(JSONNode.Enumerator);
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x000B6922 File Offset: 0x000B4B22
		// (set) Token: 0x06001E4B RID: 7755 RVA: 0x000B6934 File Offset: 0x000B4B34
		public override string Value
		{
			get
			{
				return this.m_Data.ToString(CultureInfo.InvariantCulture);
			}
			set
			{
				double data;
				if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out data))
				{
					this.m_Data = data;
				}
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x000B695C File Offset: 0x000B4B5C
		// (set) Token: 0x06001E4D RID: 7757 RVA: 0x000B6964 File Offset: 0x000B4B64
		public override double AsDouble
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

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06001E4E RID: 7758 RVA: 0x000B696D File Offset: 0x000B4B6D
		// (set) Token: 0x06001E4F RID: 7759 RVA: 0x000B6976 File Offset: 0x000B4B76
		public override long AsLong
		{
			get
			{
				return (long)this.m_Data;
			}
			set
			{
				this.m_Data = (double)value;
			}
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x000B6980 File Offset: 0x000B4B80
		public JSONNumber(double aData)
		{
			this.m_Data = aData;
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x000B698F File Offset: 0x000B4B8F
		public JSONNumber(string aData)
		{
			this.Value = aData;
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x000B699E File Offset: 0x000B4B9E
		public override JSONNode Clone()
		{
			return new JSONNumber(this.m_Data);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x000B69AB File Offset: 0x000B4BAB
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append(this.Value);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x000B69BC File Offset: 0x000B4BBC
		private static bool IsNumeric(object value)
		{
			return value is int || value is uint || value is float || value is double || value is decimal || value is long || value is ulong || value is short || value is ushort || value is sbyte || value is byte;
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x000B6A24 File Offset: 0x000B4C24
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (base.Equals(obj))
			{
				return true;
			}
			JSONNumber jsonnumber = obj as JSONNumber;
			if (jsonnumber != null)
			{
				return this.m_Data == jsonnumber.m_Data;
			}
			return JSONNumber.IsNumeric(obj) && Convert.ToDouble(obj) == this.m_Data;
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x000B6A78 File Offset: 0x000B4C78
		public override int GetHashCode()
		{
			return this.m_Data.GetHashCode();
		}

		// Token: 0x04001EC8 RID: 7880
		private double m_Data;
	}
}

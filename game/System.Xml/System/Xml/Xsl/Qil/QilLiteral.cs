using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004C2 RID: 1218
	internal class QilLiteral : QilNode
	{
		// Token: 0x060030D5 RID: 12501 RVA: 0x00121919 File Offset: 0x0011FB19
		public QilLiteral(QilNodeType nodeType, object value) : base(nodeType)
		{
			this.Value = value;
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x00121929 File Offset: 0x0011FB29
		// (set) Token: 0x060030D7 RID: 12503 RVA: 0x00121931 File Offset: 0x0011FB31
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x0012193A File Offset: 0x0011FB3A
		public static implicit operator string(QilLiteral literal)
		{
			return (string)literal._value;
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x00121947 File Offset: 0x0011FB47
		public static implicit operator int(QilLiteral literal)
		{
			return (int)literal._value;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x00121954 File Offset: 0x0011FB54
		public static implicit operator long(QilLiteral literal)
		{
			return (long)literal._value;
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x00121961 File Offset: 0x0011FB61
		public static implicit operator double(QilLiteral literal)
		{
			return (double)literal._value;
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x0012196E File Offset: 0x0011FB6E
		public static implicit operator decimal(QilLiteral literal)
		{
			return (decimal)literal._value;
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x0012197B File Offset: 0x0011FB7B
		public static implicit operator XmlQueryType(QilLiteral literal)
		{
			return (XmlQueryType)literal._value;
		}

		// Token: 0x040025CD RID: 9677
		private object _value;
	}
}

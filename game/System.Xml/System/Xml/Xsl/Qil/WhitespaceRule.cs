using System;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004DF RID: 1247
	internal class WhitespaceRule
	{
		// Token: 0x06003344 RID: 13124 RVA: 0x0000216B File Offset: 0x0000036B
		protected WhitespaceRule()
		{
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x00124B55 File Offset: 0x00122D55
		public WhitespaceRule(string localName, string namespaceName, bool preserveSpace)
		{
			this.Init(localName, namespaceName, preserveSpace);
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x00124B66 File Offset: 0x00122D66
		protected void Init(string localName, string namespaceName, bool preserveSpace)
		{
			this._localName = localName;
			this._namespaceName = namespaceName;
			this._preserveSpace = preserveSpace;
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x00124B7D File Offset: 0x00122D7D
		// (set) Token: 0x06003348 RID: 13128 RVA: 0x00124B85 File Offset: 0x00122D85
		public string LocalName
		{
			get
			{
				return this._localName;
			}
			set
			{
				this._localName = value;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06003349 RID: 13129 RVA: 0x00124B8E File Offset: 0x00122D8E
		// (set) Token: 0x0600334A RID: 13130 RVA: 0x00124B96 File Offset: 0x00122D96
		public string NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
			set
			{
				this._namespaceName = value;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x0600334B RID: 13131 RVA: 0x00124B9F File Offset: 0x00122D9F
		public bool PreserveSpace
		{
			get
			{
				return this._preserveSpace;
			}
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x00124BA7 File Offset: 0x00122DA7
		public void GetObjectData(XmlQueryDataWriter writer)
		{
			writer.WriteStringQ(this._localName);
			writer.WriteStringQ(this._namespaceName);
			writer.Write(this._preserveSpace);
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x00124BCD File Offset: 0x00122DCD
		public WhitespaceRule(XmlQueryDataReader reader)
		{
			this._localName = reader.ReadStringQ();
			this._namespaceName = reader.ReadStringQ();
			this._preserveSpace = reader.ReadBoolean();
		}

		// Token: 0x0400266D RID: 9837
		private string _localName;

		// Token: 0x0400266E RID: 9838
		private string _namespaceName;

		// Token: 0x0400266F RID: 9839
		private bool _preserveSpace;
	}
}

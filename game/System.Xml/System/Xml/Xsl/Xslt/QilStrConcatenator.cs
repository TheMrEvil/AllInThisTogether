using System;
using System.Text;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003F4 RID: 1012
	internal class QilStrConcatenator
	{
		// Token: 0x06002867 RID: 10343 RVA: 0x000F31C0 File Offset: 0x000F13C0
		public QilStrConcatenator(XPathQilFactory f)
		{
			this.f = f;
			this.builder = new StringBuilder();
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000F31DA File Offset: 0x000F13DA
		public void Reset()
		{
			this.inUse = true;
			this.builder.Length = 0;
			this.concat = null;
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000F31F8 File Offset: 0x000F13F8
		private void FlushBuilder()
		{
			if (this.concat == null)
			{
				this.concat = this.f.BaseFactory.Sequence();
			}
			if (this.builder.Length != 0)
			{
				this.concat.Add(this.f.String(this.builder.ToString()));
				this.builder.Length = 0;
			}
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000F325D File Offset: 0x000F145D
		public void Append(string value)
		{
			this.builder.Append(value);
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000F326C File Offset: 0x000F146C
		public void Append(char value)
		{
			this.builder.Append(value);
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x000F327B File Offset: 0x000F147B
		public void Append(QilNode value)
		{
			if (value != null)
			{
				if (value.NodeType == QilNodeType.LiteralString)
				{
					this.builder.Append((QilLiteral)value);
					return;
				}
				this.FlushBuilder();
				this.concat.Add(value);
			}
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000F32B4 File Offset: 0x000F14B4
		public QilNode ToQil()
		{
			this.inUse = false;
			if (this.concat == null)
			{
				return this.f.String(this.builder.ToString());
			}
			this.FlushBuilder();
			return this.f.StrConcat(this.concat);
		}

		// Token: 0x04001FE8 RID: 8168
		private XPathQilFactory f;

		// Token: 0x04001FE9 RID: 8169
		private StringBuilder builder;

		// Token: 0x04001FEA RID: 8170
		private QilList concat;

		// Token: 0x04001FEB RID: 8171
		private bool inUse;
	}
}

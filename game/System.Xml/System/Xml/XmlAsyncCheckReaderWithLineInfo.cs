using System;

namespace System.Xml
{
	// Token: 0x02000063 RID: 99
	internal class XmlAsyncCheckReaderWithLineInfo : XmlAsyncCheckReader, IXmlLineInfo
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00010D66 File Offset: 0x0000EF66
		public XmlAsyncCheckReaderWithLineInfo(XmlReader reader) : base(reader)
		{
			this.readerAsIXmlLineInfo = (IXmlLineInfo)reader;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00010D7B File Offset: 0x0000EF7B
		public virtual bool HasLineInfo()
		{
			return this.readerAsIXmlLineInfo.HasLineInfo();
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00010D88 File Offset: 0x0000EF88
		public virtual int LineNumber
		{
			get
			{
				return this.readerAsIXmlLineInfo.LineNumber;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00010D95 File Offset: 0x0000EF95
		public virtual int LinePosition
		{
			get
			{
				return this.readerAsIXmlLineInfo.LinePosition;
			}
		}

		// Token: 0x040006B0 RID: 1712
		private readonly IXmlLineInfo readerAsIXmlLineInfo;
	}
}

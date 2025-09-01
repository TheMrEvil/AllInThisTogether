using System;
using System.Configuration.Internal;
using System.IO;
using System.Xml;

// Token: 0x02000004 RID: 4
internal class ConfigXmlTextReader : XmlTextReader, IConfigErrorInfo
{
	// Token: 0x06000004 RID: 4 RVA: 0x00002064 File Offset: 0x00000264
	public ConfigXmlTextReader(Stream s, string fileName) : base(s)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		this.fileName = fileName;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002082 File Offset: 0x00000282
	public ConfigXmlTextReader(TextReader input, string fileName) : base(input)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		this.fileName = fileName;
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000006 RID: 6 RVA: 0x000020A0 File Offset: 0x000002A0
	public string Filename
	{
		get
		{
			return this.fileName;
		}
	}

	// Token: 0x0400002A RID: 42
	private readonly string fileName;
}

using System;
using System.Collections;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl
{
	// Token: 0x02000332 RID: 818
	internal class XmlILCommand
	{
		// Token: 0x060021A4 RID: 8612 RVA: 0x000D5FC9 File Offset: 0x000D41C9
		public XmlILCommand(ExecuteDelegate delExec, XmlQueryStaticData staticData)
		{
			this.delExec = delExec;
			this.staticData = staticData;
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x000D5FDF File Offset: 0x000D41DF
		public ExecuteDelegate ExecuteDelegate
		{
			get
			{
				return this.delExec;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x000D5FE7 File Offset: 0x000D41E7
		public XmlQueryStaticData StaticData
		{
			get
			{
				return this.staticData;
			}
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000D5FF0 File Offset: 0x000D41F0
		public IList Evaluate(string contextDocumentUri, XmlResolver dataSources, XsltArgumentList argumentList)
		{
			XmlCachedSequenceWriter xmlCachedSequenceWriter = new XmlCachedSequenceWriter();
			this.Execute(contextDocumentUri, dataSources, argumentList, xmlCachedSequenceWriter);
			return xmlCachedSequenceWriter.ResultSequence;
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x000D6014 File Offset: 0x000D4214
		public void Execute(object defaultDocument, XmlResolver dataSources, XsltArgumentList argumentList, XmlWriter writer)
		{
			try
			{
				if (writer is XmlAsyncCheckWriter)
				{
					writer = ((XmlAsyncCheckWriter)writer).CoreWriter;
				}
				XmlWellFormedWriter xmlWellFormedWriter = writer as XmlWellFormedWriter;
				if (xmlWellFormedWriter != null && xmlWellFormedWriter.RawWriter != null && xmlWellFormedWriter.WriteState == WriteState.Start && xmlWellFormedWriter.Settings.ConformanceLevel != ConformanceLevel.Document)
				{
					this.Execute(defaultDocument, dataSources, argumentList, new XmlMergeSequenceWriter(xmlWellFormedWriter.RawWriter));
				}
				else
				{
					this.Execute(defaultDocument, dataSources, argumentList, new XmlMergeSequenceWriter(new XmlRawWriterWrapper(writer)));
				}
			}
			finally
			{
				writer.Flush();
			}
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x000D60A8 File Offset: 0x000D42A8
		private void Execute(object defaultDocument, XmlResolver dataSources, XsltArgumentList argumentList, XmlSequenceWriter results)
		{
			if (dataSources == null)
			{
				dataSources = XmlNullResolver.Singleton;
			}
			this.delExec(new XmlQueryRuntime(this.staticData, defaultDocument, dataSources, argumentList, results));
		}

		// Token: 0x04001BB1 RID: 7089
		private ExecuteDelegate delExec;

		// Token: 0x04001BB2 RID: 7090
		private XmlQueryStaticData staticData;
	}
}

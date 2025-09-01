using System;
using System.Collections;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004A8 RID: 1192
	internal class XmlILConstructInfo : IQilAnnotation
	{
		// Token: 0x06002E92 RID: 11922 RVA: 0x00110328 File Offset: 0x0010E528
		public static XmlILConstructInfo Read(QilNode nd)
		{
			XmlILAnnotation xmlILAnnotation = nd.Annotation as XmlILAnnotation;
			XmlILConstructInfo xmlILConstructInfo = (xmlILAnnotation != null) ? xmlILAnnotation.ConstructInfo : null;
			if (xmlILConstructInfo == null)
			{
				if (XmlILConstructInfo.Default == null)
				{
					xmlILConstructInfo = new XmlILConstructInfo(QilNodeType.Unknown);
					xmlILConstructInfo.isReadOnly = true;
					XmlILConstructInfo.Default = xmlILConstructInfo;
				}
				else
				{
					xmlILConstructInfo = XmlILConstructInfo.Default;
				}
			}
			return xmlILConstructInfo;
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x0011037C File Offset: 0x0010E57C
		public static XmlILConstructInfo Write(QilNode nd)
		{
			XmlILAnnotation xmlILAnnotation = XmlILAnnotation.Write(nd);
			XmlILConstructInfo xmlILConstructInfo = xmlILAnnotation.ConstructInfo;
			if (xmlILConstructInfo == null || xmlILConstructInfo.isReadOnly)
			{
				xmlILConstructInfo = new XmlILConstructInfo(nd.NodeType);
				xmlILAnnotation.ConstructInfo = xmlILConstructInfo;
			}
			return xmlILConstructInfo;
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x001103B8 File Offset: 0x0010E5B8
		private XmlILConstructInfo(QilNodeType nodeType)
		{
			this.nodeType = nodeType;
			this.xstatesInitial = (this.xstatesFinal = PossibleXmlStates.Any);
			this.xstatesBeginLoop = (this.xstatesEndLoop = PossibleXmlStates.None);
			this.isNmspInScope = false;
			this.mightHaveNmsp = true;
			this.mightHaveAttrs = true;
			this.mightHaveDupAttrs = true;
			this.mightHaveNmspAfterAttrs = true;
			this.constrMeth = XmlILConstructMethod.Iterator;
			this.parentInfo = null;
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002E95 RID: 11925 RVA: 0x00110423 File Offset: 0x0010E623
		// (set) Token: 0x06002E96 RID: 11926 RVA: 0x0011042B File Offset: 0x0010E62B
		public PossibleXmlStates InitialStates
		{
			get
			{
				return this.xstatesInitial;
			}
			set
			{
				this.xstatesInitial = value;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002E97 RID: 11927 RVA: 0x00110434 File Offset: 0x0010E634
		// (set) Token: 0x06002E98 RID: 11928 RVA: 0x0011043C File Offset: 0x0010E63C
		public PossibleXmlStates FinalStates
		{
			get
			{
				return this.xstatesFinal;
			}
			set
			{
				this.xstatesFinal = value;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (set) Token: 0x06002E99 RID: 11929 RVA: 0x00110445 File Offset: 0x0010E645
		public PossibleXmlStates BeginLoopStates
		{
			set
			{
				this.xstatesBeginLoop = value;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (set) Token: 0x06002E9A RID: 11930 RVA: 0x0011044E File Offset: 0x0010E64E
		public PossibleXmlStates EndLoopStates
		{
			set
			{
				this.xstatesEndLoop = value;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002E9B RID: 11931 RVA: 0x00110457 File Offset: 0x0010E657
		// (set) Token: 0x06002E9C RID: 11932 RVA: 0x0011045F File Offset: 0x0010E65F
		public XmlILConstructMethod ConstructMethod
		{
			get
			{
				return this.constrMeth;
			}
			set
			{
				this.constrMeth = value;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x00110468 File Offset: 0x0010E668
		// (set) Token: 0x06002E9E RID: 11934 RVA: 0x00110480 File Offset: 0x0010E680
		public bool PushToWriterFirst
		{
			get
			{
				return this.constrMeth == XmlILConstructMethod.Writer || this.constrMeth == XmlILConstructMethod.WriterThenIterator;
			}
			set
			{
				XmlILConstructMethod xmlILConstructMethod = this.constrMeth;
				if (xmlILConstructMethod == XmlILConstructMethod.Iterator)
				{
					this.constrMeth = XmlILConstructMethod.WriterThenIterator;
					return;
				}
				if (xmlILConstructMethod != XmlILConstructMethod.IteratorThenWriter)
				{
					return;
				}
				this.constrMeth = XmlILConstructMethod.Writer;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x001104AB File Offset: 0x0010E6AB
		// (set) Token: 0x06002EA0 RID: 11936 RVA: 0x001104C4 File Offset: 0x0010E6C4
		public bool PushToWriterLast
		{
			get
			{
				return this.constrMeth == XmlILConstructMethod.Writer || this.constrMeth == XmlILConstructMethod.IteratorThenWriter;
			}
			set
			{
				XmlILConstructMethod xmlILConstructMethod = this.constrMeth;
				if (xmlILConstructMethod == XmlILConstructMethod.Iterator)
				{
					this.constrMeth = XmlILConstructMethod.IteratorThenWriter;
					return;
				}
				if (xmlILConstructMethod != XmlILConstructMethod.WriterThenIterator)
				{
					return;
				}
				this.constrMeth = XmlILConstructMethod.Writer;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x001104EF File Offset: 0x0010E6EF
		// (set) Token: 0x06002EA2 RID: 11938 RVA: 0x00110508 File Offset: 0x0010E708
		public bool PullFromIteratorFirst
		{
			get
			{
				return this.constrMeth == XmlILConstructMethod.IteratorThenWriter || this.constrMeth == XmlILConstructMethod.Iterator;
			}
			set
			{
				XmlILConstructMethod xmlILConstructMethod = this.constrMeth;
				if (xmlILConstructMethod == XmlILConstructMethod.Writer)
				{
					this.constrMeth = XmlILConstructMethod.IteratorThenWriter;
					return;
				}
				if (xmlILConstructMethod != XmlILConstructMethod.WriterThenIterator)
				{
					return;
				}
				this.constrMeth = XmlILConstructMethod.Iterator;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (set) Token: 0x06002EA3 RID: 11939 RVA: 0x00110534 File Offset: 0x0010E734
		public XmlILConstructInfo ParentInfo
		{
			set
			{
				this.parentInfo = value;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x0011053D File Offset: 0x0010E73D
		public XmlILConstructInfo ParentElementInfo
		{
			get
			{
				if (this.parentInfo != null && this.parentInfo.nodeType == QilNodeType.ElementCtor)
				{
					return this.parentInfo;
				}
				return null;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x0011055E File Offset: 0x0010E75E
		// (set) Token: 0x06002EA6 RID: 11942 RVA: 0x00110566 File Offset: 0x0010E766
		public bool IsNamespaceInScope
		{
			get
			{
				return this.isNmspInScope;
			}
			set
			{
				this.isNmspInScope = value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x0011056F File Offset: 0x0010E76F
		// (set) Token: 0x06002EA8 RID: 11944 RVA: 0x00110577 File Offset: 0x0010E777
		public bool MightHaveNamespaces
		{
			get
			{
				return this.mightHaveNmsp;
			}
			set
			{
				this.mightHaveNmsp = value;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x00110580 File Offset: 0x0010E780
		// (set) Token: 0x06002EAA RID: 11946 RVA: 0x00110588 File Offset: 0x0010E788
		public bool MightHaveNamespacesAfterAttributes
		{
			get
			{
				return this.mightHaveNmspAfterAttrs;
			}
			set
			{
				this.mightHaveNmspAfterAttrs = value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002EAB RID: 11947 RVA: 0x00110591 File Offset: 0x0010E791
		// (set) Token: 0x06002EAC RID: 11948 RVA: 0x00110599 File Offset: 0x0010E799
		public bool MightHaveAttributes
		{
			get
			{
				return this.mightHaveAttrs;
			}
			set
			{
				this.mightHaveAttrs = value;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x001105A2 File Offset: 0x0010E7A2
		// (set) Token: 0x06002EAE RID: 11950 RVA: 0x001105AA File Offset: 0x0010E7AA
		public bool MightHaveDuplicateAttributes
		{
			get
			{
				return this.mightHaveDupAttrs;
			}
			set
			{
				this.mightHaveDupAttrs = value;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002EAF RID: 11951 RVA: 0x001105B3 File Offset: 0x0010E7B3
		public ArrayList CallersInfo
		{
			get
			{
				if (this.callersInfo == null)
				{
					this.callersInfo = new ArrayList();
				}
				return this.callersInfo;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x001105CE File Offset: 0x0010E7CE
		public virtual string Name
		{
			get
			{
				return "ConstructInfo";
			}
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x001105D8 File Offset: 0x0010E7D8
		public override string ToString()
		{
			string text = "";
			if (this.constrMeth != XmlILConstructMethod.Iterator)
			{
				text += this.constrMeth.ToString();
				text = text + ", " + this.xstatesInitial.ToString();
				if (this.xstatesBeginLoop != PossibleXmlStates.None)
				{
					text = string.Concat(new string[]
					{
						text,
						" => ",
						this.xstatesBeginLoop.ToString(),
						" => ",
						this.xstatesEndLoop.ToString()
					});
				}
				text = text + " => " + this.xstatesFinal.ToString();
				if (!this.MightHaveAttributes)
				{
					text += ", NoAttrs";
				}
				if (!this.MightHaveDuplicateAttributes)
				{
					text += ", NoDupAttrs";
				}
				if (!this.MightHaveNamespaces)
				{
					text += ", NoNmsp";
				}
				if (!this.MightHaveNamespacesAfterAttributes)
				{
					text += ", NoNmspAfterAttrs";
				}
			}
			return text;
		}

		// Token: 0x040024EA RID: 9450
		private QilNodeType nodeType;

		// Token: 0x040024EB RID: 9451
		private PossibleXmlStates xstatesInitial;

		// Token: 0x040024EC RID: 9452
		private PossibleXmlStates xstatesFinal;

		// Token: 0x040024ED RID: 9453
		private PossibleXmlStates xstatesBeginLoop;

		// Token: 0x040024EE RID: 9454
		private PossibleXmlStates xstatesEndLoop;

		// Token: 0x040024EF RID: 9455
		private bool isNmspInScope;

		// Token: 0x040024F0 RID: 9456
		private bool mightHaveNmsp;

		// Token: 0x040024F1 RID: 9457
		private bool mightHaveAttrs;

		// Token: 0x040024F2 RID: 9458
		private bool mightHaveDupAttrs;

		// Token: 0x040024F3 RID: 9459
		private bool mightHaveNmspAfterAttrs;

		// Token: 0x040024F4 RID: 9460
		private XmlILConstructMethod constrMeth;

		// Token: 0x040024F5 RID: 9461
		private XmlILConstructInfo parentInfo;

		// Token: 0x040024F6 RID: 9462
		private ArrayList callersInfo;

		// Token: 0x040024F7 RID: 9463
		private bool isReadOnly;

		// Token: 0x040024F8 RID: 9464
		private static volatile XmlILConstructInfo Default;
	}
}

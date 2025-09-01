using System;
using System.Reflection;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004A5 RID: 1189
	internal class XmlILAnnotation : ListBase<object>
	{
		// Token: 0x06002E83 RID: 11907 RVA: 0x00110250 File Offset: 0x0010E450
		public static XmlILAnnotation Write(QilNode nd)
		{
			XmlILAnnotation xmlILAnnotation = nd.Annotation as XmlILAnnotation;
			if (xmlILAnnotation == null)
			{
				xmlILAnnotation = new XmlILAnnotation(nd.Annotation);
				nd.Annotation = xmlILAnnotation;
			}
			return xmlILAnnotation;
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x00110280 File Offset: 0x0010E480
		private XmlILAnnotation(object annPrev)
		{
			this.annPrev = annPrev;
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002E85 RID: 11909 RVA: 0x0011028F File Offset: 0x0010E48F
		// (set) Token: 0x06002E86 RID: 11910 RVA: 0x00110297 File Offset: 0x0010E497
		public MethodInfo FunctionBinding
		{
			get
			{
				return this.funcMethod;
			}
			set
			{
				this.funcMethod = value;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x001102A0 File Offset: 0x0010E4A0
		// (set) Token: 0x06002E88 RID: 11912 RVA: 0x001102A8 File Offset: 0x0010E4A8
		public int ArgumentPosition
		{
			get
			{
				return this.argPos;
			}
			set
			{
				this.argPos = value;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x001102B1 File Offset: 0x0010E4B1
		// (set) Token: 0x06002E8A RID: 11914 RVA: 0x001102B9 File Offset: 0x0010E4B9
		public IteratorDescriptor CachedIteratorDescriptor
		{
			get
			{
				return this.iterInfo;
			}
			set
			{
				this.iterInfo = value;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x001102C2 File Offset: 0x0010E4C2
		// (set) Token: 0x06002E8C RID: 11916 RVA: 0x001102CA File Offset: 0x0010E4CA
		public XmlILConstructInfo ConstructInfo
		{
			get
			{
				return this.constrInfo;
			}
			set
			{
				this.constrInfo = value;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002E8D RID: 11917 RVA: 0x001102D3 File Offset: 0x0010E4D3
		// (set) Token: 0x06002E8E RID: 11918 RVA: 0x001102DB File Offset: 0x0010E4DB
		public OptimizerPatterns Patterns
		{
			get
			{
				return this.optPatt;
			}
			set
			{
				this.optPatt = value;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002E8F RID: 11919 RVA: 0x001102E4 File Offset: 0x0010E4E4
		public override int Count
		{
			get
			{
				if (this.annPrev == null)
				{
					return 2;
				}
				return 3;
			}
		}

		// Token: 0x1700089C RID: 2204
		public override object this[int index]
		{
			get
			{
				if (this.annPrev != null)
				{
					if (index == 0)
					{
						return this.annPrev;
					}
					index--;
				}
				if (index == 0)
				{
					return this.constrInfo;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				return this.optPatt;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040024D6 RID: 9430
		private object annPrev;

		// Token: 0x040024D7 RID: 9431
		private MethodInfo funcMethod;

		// Token: 0x040024D8 RID: 9432
		private int argPos;

		// Token: 0x040024D9 RID: 9433
		private IteratorDescriptor iterInfo;

		// Token: 0x040024DA RID: 9434
		private XmlILConstructInfo constrInfo;

		// Token: 0x040024DB RID: 9435
		private OptimizerPatterns optPatt;
	}
}

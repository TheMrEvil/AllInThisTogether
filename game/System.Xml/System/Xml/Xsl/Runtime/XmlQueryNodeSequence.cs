using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200047E RID: 1150
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlQueryNodeSequence : XmlQuerySequence<XPathNavigator>, IList<XPathItem>, ICollection<XPathItem>, IEnumerable<XPathItem>, IEnumerable
	{
		// Token: 0x06002CFD RID: 11517 RVA: 0x001089F9 File Offset: 0x00106BF9
		public static XmlQueryNodeSequence CreateOrReuse(XmlQueryNodeSequence seq)
		{
			if (seq != null)
			{
				seq.Clear();
				return seq;
			}
			return new XmlQueryNodeSequence();
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x00108A0B File Offset: 0x00106C0B
		public static XmlQueryNodeSequence CreateOrReuse(XmlQueryNodeSequence seq, XPathNavigator navigator)
		{
			if (seq != null)
			{
				seq.Clear();
				seq.Add(navigator);
				return seq;
			}
			return new XmlQueryNodeSequence(navigator);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x00108A25 File Offset: 0x00106C25
		public XmlQueryNodeSequence()
		{
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x00108A2D File Offset: 0x00106C2D
		public XmlQueryNodeSequence(int capacity) : base(capacity)
		{
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x00108A38 File Offset: 0x00106C38
		public XmlQueryNodeSequence(IList<XPathNavigator> list) : base(list.Count)
		{
			for (int i = 0; i < list.Count; i++)
			{
				this.AddClone(list[i]);
			}
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x00108A6F File Offset: 0x00106C6F
		public XmlQueryNodeSequence(XPathNavigator[] array, int size) : base(array, size)
		{
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x00108A79 File Offset: 0x00106C79
		public XmlQueryNodeSequence(XPathNavigator navigator) : base(1)
		{
			this.AddClone(navigator);
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002D04 RID: 11524 RVA: 0x00108A89 File Offset: 0x00106C89
		// (set) Token: 0x06002D05 RID: 11525 RVA: 0x00108AA2 File Offset: 0x00106CA2
		public bool IsDocOrderDistinct
		{
			get
			{
				return this.docOrderDistinct == this || base.Count <= 1;
			}
			set
			{
				this.docOrderDistinct = (value ? this : null);
			}
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x00108AB4 File Offset: 0x00106CB4
		public XmlQueryNodeSequence DocOrderDistinct(IComparer<XPathNavigator> comparer)
		{
			if (this.docOrderDistinct != null)
			{
				return this.docOrderDistinct;
			}
			if (base.Count <= 1)
			{
				return this;
			}
			XPathNavigator[] array = new XPathNavigator[base.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = base[i];
			}
			Array.Sort<XPathNavigator>(array, 0, base.Count, comparer);
			int num = 0;
			for (int i = 1; i < array.Length; i++)
			{
				if (!array[num].IsSamePosition(array[i]))
				{
					num++;
					if (num != i)
					{
						array[num] = array[i];
					}
				}
			}
			this.docOrderDistinct = new XmlQueryNodeSequence(array, num + 1);
			this.docOrderDistinct.docOrderDistinct = this.docOrderDistinct;
			return this.docOrderDistinct;
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x00108B5E File Offset: 0x00106D5E
		public void AddClone(XPathNavigator navigator)
		{
			base.Add(navigator.Clone());
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x00108B6C File Offset: 0x00106D6C
		protected override void OnItemsChanged()
		{
			this.docOrderDistinct = null;
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x00108B75 File Offset: 0x00106D75
		IEnumerator<XPathItem> IEnumerable<XPathItem>.GetEnumerator()
		{
			return new IListEnumerator<XPathItem>(this);
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x0001222F File Offset: 0x0001042F
		bool ICollection<XPathItem>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x00005BD6 File Offset: 0x00003DD6
		void ICollection<XPathItem>.Add(XPathItem value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x00005BD6 File Offset: 0x00003DD6
		void ICollection<XPathItem>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x00108B82 File Offset: 0x00106D82
		bool ICollection<XPathItem>.Contains(XPathItem value)
		{
			return base.IndexOf((XPathNavigator)value) != -1;
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x00108B98 File Offset: 0x00106D98
		void ICollection<XPathItem>.CopyTo(XPathItem[] array, int index)
		{
			for (int i = 0; i < base.Count; i++)
			{
				array[index + i] = base[i];
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x00005BD6 File Offset: 0x00003DD6
		bool ICollection<XPathItem>.Remove(XPathItem value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000870 RID: 2160
		XPathItem IList<XPathItem>.this[int index]
		{
			get
			{
				if (index >= base.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return base[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x00108BDF File Offset: 0x00106DDF
		int IList<XPathItem>.IndexOf(XPathItem value)
		{
			return base.IndexOf((XPathNavigator)value);
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x00005BD6 File Offset: 0x00003DD6
		void IList<XPathItem>.Insert(int index, XPathItem value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x00005BD6 File Offset: 0x00003DD6
		void IList<XPathItem>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x00108BED File Offset: 0x00106DED
		// Note: this type is marked as 'beforefieldinit'.
		static XmlQueryNodeSequence()
		{
		}

		// Token: 0x04002312 RID: 8978
		public new static readonly XmlQueryNodeSequence Empty = new XmlQueryNodeSequence();

		// Token: 0x04002313 RID: 8979
		private XmlQueryNodeSequence docOrderDistinct;
	}
}

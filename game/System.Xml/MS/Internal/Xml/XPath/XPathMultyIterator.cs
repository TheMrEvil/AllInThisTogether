using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200065E RID: 1630
	internal class XPathMultyIterator : ResetableIterator
	{
		// Token: 0x060041FA RID: 16890 RVA: 0x00168D34 File Offset: 0x00166F34
		public XPathMultyIterator(ArrayList inputArray)
		{
			this.arr = new ResetableIterator[inputArray.Count];
			for (int i = 0; i < this.arr.Length; i++)
			{
				this.arr[i] = new XPathArrayIterator((ArrayList)inputArray[i]);
			}
			this.Init();
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x00168D8C File Offset: 0x00166F8C
		private void Init()
		{
			for (int i = 0; i < this.arr.Length; i++)
			{
				this.Advance(i);
			}
			int num = this.arr.Length - 2;
			while (this.firstNotEmpty <= num)
			{
				if (this.SiftItem(num))
				{
					num--;
				}
			}
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x00168DD8 File Offset: 0x00166FD8
		private bool Advance(int pos)
		{
			if (!this.arr[pos].MoveNext())
			{
				if (this.firstNotEmpty != pos)
				{
					ResetableIterator resetableIterator = this.arr[pos];
					Array.Copy(this.arr, this.firstNotEmpty, this.arr, this.firstNotEmpty + 1, pos - this.firstNotEmpty);
					this.arr[this.firstNotEmpty] = resetableIterator;
				}
				this.firstNotEmpty++;
				return false;
			}
			return true;
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x00168E4C File Offset: 0x0016704C
		private bool SiftItem(int item)
		{
			ResetableIterator resetableIterator = this.arr[item];
			while (item + 1 < this.arr.Length)
			{
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(resetableIterator.Current, this.arr[item + 1].Current);
				if (xmlNodeOrder == XmlNodeOrder.Before)
				{
					break;
				}
				if (xmlNodeOrder == XmlNodeOrder.After)
				{
					this.arr[item] = this.arr[item + 1];
					item++;
				}
				else
				{
					this.arr[item] = resetableIterator;
					if (!this.Advance(item))
					{
						return false;
					}
					resetableIterator = this.arr[item];
				}
			}
			this.arr[item] = resetableIterator;
			return true;
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x00168ED4 File Offset: 0x001670D4
		public override void Reset()
		{
			this.firstNotEmpty = 0;
			this.position = 0;
			for (int i = 0; i < this.arr.Length; i++)
			{
				this.arr[i].Reset();
			}
			this.Init();
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x00168F15 File Offset: 0x00167115
		public XPathMultyIterator(XPathMultyIterator it)
		{
			this.arr = (ResetableIterator[])it.arr.Clone();
			this.firstNotEmpty = it.firstNotEmpty;
			this.position = it.position;
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x00168F4B File Offset: 0x0016714B
		public override XPathNodeIterator Clone()
		{
			return new XPathMultyIterator(this);
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06004201 RID: 16897 RVA: 0x00168F53 File Offset: 0x00167153
		public override XPathNavigator Current
		{
			get
			{
				return this.arr[this.firstNotEmpty].Current;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06004202 RID: 16898 RVA: 0x00168F67 File Offset: 0x00167167
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x00168F70 File Offset: 0x00167170
		public override bool MoveNext()
		{
			if (this.firstNotEmpty >= this.arr.Length)
			{
				return false;
			}
			if (this.position != 0)
			{
				if (this.Advance(this.firstNotEmpty))
				{
					this.SiftItem(this.firstNotEmpty);
				}
				if (this.firstNotEmpty >= this.arr.Length)
				{
					return false;
				}
			}
			this.position++;
			return true;
		}

		// Token: 0x04002EAD RID: 11949
		protected ResetableIterator[] arr;

		// Token: 0x04002EAE RID: 11950
		protected int firstNotEmpty;

		// Token: 0x04002EAF RID: 11951
		protected int position;
	}
}

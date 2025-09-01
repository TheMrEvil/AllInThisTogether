using System;

namespace System.Collections
{
	// Token: 0x02000A21 RID: 2593
	[Serializable]
	internal sealed class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x06005BC8 RID: 23496 RVA: 0x00135351 File Offset: 0x00133551
		internal CompatibleComparer(IHashCodeProvider hashCodeProvider, IComparer comparer)
		{
			this._hcp = hashCodeProvider;
			this._comparer = comparer;
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06005BC9 RID: 23497 RVA: 0x00135367 File Offset: 0x00133567
		internal IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06005BCA RID: 23498 RVA: 0x0013536F File Offset: 0x0013356F
		internal IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x06005BCB RID: 23499 RVA: 0x00135377 File Offset: 0x00133577
		public bool Equals(object a, object b)
		{
			return this.Compare(a, b) == 0;
		}

		// Token: 0x06005BCC RID: 23500 RVA: 0x00135384 File Offset: 0x00133584
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			if (this._comparer != null)
			{
				return this._comparer.Compare(a, b);
			}
			IComparable comparable = a as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(b);
			}
			throw new ArgumentException("At least one object must implement IComparable.");
		}

		// Token: 0x06005BCD RID: 23501 RVA: 0x001353D3 File Offset: 0x001335D3
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._hcp == null)
			{
				return obj.GetHashCode();
			}
			return this._hcp.GetHashCode(obj);
		}

		// Token: 0x04003880 RID: 14464
		private readonly IHashCodeProvider _hcp;

		// Token: 0x04003881 RID: 14465
		private readonly IComparer _comparer;
	}
}

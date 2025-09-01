using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200022C RID: 556
	public struct StylePropertyNameCollection : IEnumerable<StylePropertyName>, IEnumerable
	{
		// Token: 0x060010EA RID: 4330 RVA: 0x0004344E File Offset: 0x0004164E
		internal StylePropertyNameCollection(List<StylePropertyName> list)
		{
			this.propertiesList = list;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00043458 File Offset: 0x00041658
		public StylePropertyNameCollection.Enumerator GetEnumerator()
		{
			return new StylePropertyNameCollection.Enumerator(this.propertiesList.GetEnumerator());
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0004347C File Offset: 0x0004167C
		IEnumerator<StylePropertyName> IEnumerable<StylePropertyName>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0004349C File Offset: 0x0004169C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000434BC File Offset: 0x000416BC
		public bool Contains(StylePropertyName stylePropertyName)
		{
			bool result;
			using (List<StylePropertyName>.Enumerator enumerator = this.propertiesList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StylePropertyName lhs = enumerator.Current;
					bool flag = lhs == stylePropertyName;
					if (flag)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x04000773 RID: 1907
		internal List<StylePropertyName> propertiesList;

		// Token: 0x0200022D RID: 557
		public struct Enumerator : IEnumerator<StylePropertyName>, IEnumerator, IDisposable
		{
			// Token: 0x060010EF RID: 4335 RVA: 0x00043520 File Offset: 0x00041720
			internal Enumerator(List<StylePropertyName>.Enumerator enumerator)
			{
				this.m_Enumerator = enumerator;
			}

			// Token: 0x060010F0 RID: 4336 RVA: 0x0004352A File Offset: 0x0004172A
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x170003B3 RID: 947
			// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00043537 File Offset: 0x00041737
			public StylePropertyName Current
			{
				get
				{
					return this.m_Enumerator.Current;
				}
			}

			// Token: 0x170003B4 RID: 948
			// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00043544 File Offset: 0x00041744
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060010F3 RID: 4339 RVA: 0x00002166 File Offset: 0x00000366
			public void Reset()
			{
			}

			// Token: 0x060010F4 RID: 4340 RVA: 0x00043551 File Offset: 0x00041751
			public void Dispose()
			{
				this.m_Enumerator.Dispose();
			}

			// Token: 0x04000774 RID: 1908
			private List<StylePropertyName>.Enumerator m_Enumerator;
		}
	}
}

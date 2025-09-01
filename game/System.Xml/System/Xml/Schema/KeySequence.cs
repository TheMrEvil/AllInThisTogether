using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020004F3 RID: 1267
	internal class KeySequence
	{
		// Token: 0x060033EA RID: 13290 RVA: 0x00126EE3 File Offset: 0x001250E3
		internal KeySequence(int dim, int line, int col)
		{
			this.dim = dim;
			this.ks = new TypedObject[dim];
			this.posline = line;
			this.poscol = col;
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060033EB RID: 13291 RVA: 0x00126F13 File Offset: 0x00125113
		public int PosLine
		{
			get
			{
				return this.posline;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060033EC RID: 13292 RVA: 0x00126F1B File Offset: 0x0012511B
		public int PosCol
		{
			get
			{
				return this.poscol;
			}
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x00126F24 File Offset: 0x00125124
		public KeySequence(TypedObject[] ks)
		{
			this.ks = ks;
			this.dim = ks.Length;
			this.posline = (this.poscol = 0);
		}

		// Token: 0x17000934 RID: 2356
		public object this[int index]
		{
			get
			{
				return this.ks[index];
			}
			set
			{
				this.ks[index] = (TypedObject)value;
			}
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x00126F78 File Offset: 0x00125178
		internal bool IsQualified()
		{
			for (int i = 0; i < this.ks.Length; i++)
			{
				if (this.ks[i] == null || this.ks[i].Value == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x00126FB4 File Offset: 0x001251B4
		public override int GetHashCode()
		{
			if (this.hashcode != -1)
			{
				return this.hashcode;
			}
			this.hashcode = 0;
			for (int i = 0; i < this.ks.Length; i++)
			{
				this.ks[i].SetDecimal();
				if (this.ks[i].IsDecimal)
				{
					for (int j = 0; j < this.ks[i].Dim; j++)
					{
						this.hashcode += this.ks[i].Dvalue[j].GetHashCode();
					}
				}
				else
				{
					Array array = this.ks[i].Value as Array;
					if (array != null)
					{
						XmlAtomicValue[] array2 = array as XmlAtomicValue[];
						if (array2 != null)
						{
							for (int k = 0; k < array2.Length; k++)
							{
								this.hashcode += ((XmlAtomicValue)array2.GetValue(k)).TypedValue.GetHashCode();
							}
						}
						else
						{
							for (int l = 0; l < ((Array)this.ks[i].Value).Length; l++)
							{
								this.hashcode += ((Array)this.ks[i].Value).GetValue(l).GetHashCode();
							}
						}
					}
					else
					{
						this.hashcode += this.ks[i].Value.GetHashCode();
					}
				}
			}
			return this.hashcode;
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x00127124 File Offset: 0x00125324
		public override bool Equals(object other)
		{
			KeySequence keySequence = (KeySequence)other;
			for (int i = 0; i < this.ks.Length; i++)
			{
				if (!this.ks[i].Equals(keySequence.ks[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x00127168 File Offset: 0x00125368
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.ks[0].ToString());
			for (int i = 1; i < this.ks.Length; i++)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(this.ks[i].ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040026C4 RID: 9924
		private TypedObject[] ks;

		// Token: 0x040026C5 RID: 9925
		private int dim;

		// Token: 0x040026C6 RID: 9926
		private int hashcode = -1;

		// Token: 0x040026C7 RID: 9927
		private int posline;

		// Token: 0x040026C8 RID: 9928
		private int poscol;
	}
}

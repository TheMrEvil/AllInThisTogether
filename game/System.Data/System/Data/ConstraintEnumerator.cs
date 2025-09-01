using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x020000AC RID: 172
	internal class ConstraintEnumerator
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002C8C9 File Offset: 0x0002AAC9
		public ConstraintEnumerator(DataSet dataSet)
		{
			this._tables = ((dataSet != null) ? dataSet.Tables.GetEnumerator() : null);
			this._currentObject = null;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002C8F0 File Offset: 0x0002AAF0
		public bool GetNext()
		{
			this._currentObject = null;
			while (this._tables != null)
			{
				if (this._constraints == null)
				{
					if (!this._tables.MoveNext())
					{
						this._tables = null;
						return false;
					}
					this._constraints = ((DataTable)this._tables.Current).Constraints.GetEnumerator();
				}
				if (!this._constraints.MoveNext())
				{
					this._constraints = null;
				}
				else
				{
					Constraint constraint = (Constraint)this._constraints.Current;
					if (this.IsValidCandidate(constraint))
					{
						this._currentObject = constraint;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002C986 File Offset: 0x0002AB86
		public Constraint GetConstraint()
		{
			return this._currentObject;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00006D61 File Offset: 0x00004F61
		protected virtual bool IsValidCandidate(Constraint constraint)
		{
			return true;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0002C986 File Offset: 0x0002AB86
		protected Constraint CurrentObject
		{
			get
			{
				return this._currentObject;
			}
		}

		// Token: 0x04000788 RID: 1928
		private IEnumerator _tables;

		// Token: 0x04000789 RID: 1929
		private IEnumerator _constraints;

		// Token: 0x0400078A RID: 1930
		private Constraint _currentObject;
	}
}

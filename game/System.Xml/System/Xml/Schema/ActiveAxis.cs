using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020004E2 RID: 1250
	internal class ActiveAxis
	{
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x0012503E File Offset: 0x0012323E
		public int CurrentDepth
		{
			get
			{
				return this._currentDepth;
			}
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x00125046 File Offset: 0x00123246
		internal void Reactivate()
		{
			this._isActive = true;
			this._currentDepth = -1;
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x00125058 File Offset: 0x00123258
		internal ActiveAxis(Asttree axisTree)
		{
			this._axisTree = axisTree;
			this._currentDepth = -1;
			this._axisStack = new ArrayList(axisTree.SubtreeArray.Count);
			for (int i = 0; i < axisTree.SubtreeArray.Count; i++)
			{
				AxisStack value = new AxisStack((ForwardAxis)axisTree.SubtreeArray[i], this);
				this._axisStack.Add(value);
			}
			this._isActive = true;
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x001250D4 File Offset: 0x001232D4
		public bool MoveToStartElement(string localname, string URN)
		{
			if (!this._isActive)
			{
				return false;
			}
			this._currentDepth++;
			bool result = false;
			for (int i = 0; i < this._axisStack.Count; i++)
			{
				AxisStack axisStack = (AxisStack)this._axisStack[i];
				if (axisStack.Subtree.IsSelfAxis)
				{
					if (axisStack.Subtree.IsDss || this.CurrentDepth == 0)
					{
						result = true;
					}
				}
				else if (this.CurrentDepth != 0 && axisStack.MoveToChild(localname, URN, this._currentDepth))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x00125164 File Offset: 0x00123364
		public virtual bool EndElement(string localname, string URN)
		{
			if (this._currentDepth == 0)
			{
				this._isActive = false;
				this._currentDepth--;
			}
			if (!this._isActive)
			{
				return false;
			}
			for (int i = 0; i < this._axisStack.Count; i++)
			{
				((AxisStack)this._axisStack[i]).MoveToParent(localname, URN, this._currentDepth);
			}
			this._currentDepth--;
			return false;
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x001251DC File Offset: 0x001233DC
		public bool MoveToAttribute(string localname, string URN)
		{
			if (!this._isActive)
			{
				return false;
			}
			bool result = false;
			for (int i = 0; i < this._axisStack.Count; i++)
			{
				if (((AxisStack)this._axisStack[i]).MoveToAttribute(localname, URN, this._currentDepth + 1))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04002677 RID: 9847
		private int _currentDepth;

		// Token: 0x04002678 RID: 9848
		private bool _isActive;

		// Token: 0x04002679 RID: 9849
		private Asttree _axisTree;

		// Token: 0x0400267A RID: 9850
		private ArrayList _axisStack;
	}
}

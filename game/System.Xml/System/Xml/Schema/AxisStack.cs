using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020004E1 RID: 1249
	internal class AxisStack
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06003353 RID: 13139 RVA: 0x00124DAD File Offset: 0x00122FAD
		internal ForwardAxis Subtree
		{
			get
			{
				return this._subtree;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x00124DB5 File Offset: 0x00122FB5
		internal int Length
		{
			get
			{
				return this._stack.Count;
			}
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x00124DC2 File Offset: 0x00122FC2
		public AxisStack(ForwardAxis faxis, ActiveAxis parent)
		{
			this._subtree = faxis;
			this._stack = new ArrayList();
			this._parent = parent;
			if (!faxis.IsDss)
			{
				this.Push(1);
			}
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x00124DF4 File Offset: 0x00122FF4
		internal void Push(int depth)
		{
			AxisElement value = new AxisElement(this._subtree.RootNode, depth);
			this._stack.Add(value);
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x00124E20 File Offset: 0x00123020
		internal void Pop()
		{
			this._stack.RemoveAt(this.Length - 1);
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x00124E35 File Offset: 0x00123035
		internal static bool Equal(string thisname, string thisURN, string name, string URN)
		{
			if (thisURN == null)
			{
				if (URN != null && URN.Length != 0)
				{
					return false;
				}
			}
			else if (thisURN.Length != 0 && thisURN != URN)
			{
				return false;
			}
			return thisname.Length == 0 || !(thisname != name);
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x00124E70 File Offset: 0x00123070
		internal void MoveToParent(string name, string URN, int depth)
		{
			if (this._subtree.IsSelfAxis)
			{
				return;
			}
			for (int i = 0; i < this._stack.Count; i++)
			{
				((AxisElement)this._stack[i]).MoveToParent(depth, this._subtree);
			}
			if (this._subtree.IsDss && AxisStack.Equal(this._subtree.RootNode.Name, this._subtree.RootNode.Urn, name, URN))
			{
				this.Pop();
			}
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x00124EFC File Offset: 0x001230FC
		internal bool MoveToChild(string name, string URN, int depth)
		{
			bool result = false;
			if (this._subtree.IsDss && AxisStack.Equal(this._subtree.RootNode.Name, this._subtree.RootNode.Urn, name, URN))
			{
				this.Push(-1);
			}
			for (int i = 0; i < this._stack.Count; i++)
			{
				if (((AxisElement)this._stack[i]).MoveToChild(name, URN, depth, this._subtree))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x00124F84 File Offset: 0x00123184
		internal bool MoveToAttribute(string name, string URN, int depth)
		{
			if (!this._subtree.IsAttribute)
			{
				return false;
			}
			if (!AxisStack.Equal(this._subtree.TopNode.Name, this._subtree.TopNode.Urn, name, URN))
			{
				return false;
			}
			bool result = false;
			if (this._subtree.TopNode.Input == null)
			{
				return this._subtree.IsDss || depth == 1;
			}
			for (int i = 0; i < this._stack.Count; i++)
			{
				AxisElement axisElement = (AxisElement)this._stack[i];
				if (axisElement.isMatch && axisElement.CurNode == this._subtree.TopNode.Input)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04002674 RID: 9844
		private ArrayList _stack;

		// Token: 0x04002675 RID: 9845
		private ForwardAxis _subtree;

		// Token: 0x04002676 RID: 9846
		private ActiveAxis _parent;
	}
}

using System;

namespace System.Xml.Schema
{
	// Token: 0x020004E0 RID: 1248
	internal class AxisElement
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x00124BF9 File Offset: 0x00122DF9
		internal DoubleLinkAxis CurNode
		{
			get
			{
				return this.curNode;
			}
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x00124C04 File Offset: 0x00122E04
		internal AxisElement(DoubleLinkAxis node, int depth)
		{
			this.curNode = node;
			this.curDepth = depth;
			this.rootDepth = depth;
			this.isMatch = false;
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x00124C38 File Offset: 0x00122E38
		internal void SetDepth(int depth)
		{
			this.curDepth = depth;
			this.rootDepth = depth;
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x00124C58 File Offset: 0x00122E58
		internal void MoveToParent(int depth, ForwardAxis parent)
		{
			if (depth != this.curDepth - 1)
			{
				if (depth == this.curDepth && this.isMatch)
				{
					this.isMatch = false;
				}
				return;
			}
			if (this.curNode.Input == parent.RootNode && parent.IsDss)
			{
				this.curNode = parent.RootNode;
				this.rootDepth = (this.curDepth = -1);
				return;
			}
			if (this.curNode.Input != null)
			{
				this.curNode = (DoubleLinkAxis)this.curNode.Input;
				this.curDepth--;
				return;
			}
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x00124CF4 File Offset: 0x00122EF4
		internal bool MoveToChild(string name, string URN, int depth, ForwardAxis parent)
		{
			if (Asttree.IsAttribute(this.curNode))
			{
				return false;
			}
			if (this.isMatch)
			{
				this.isMatch = false;
			}
			if (!AxisStack.Equal(this.curNode.Name, this.curNode.Urn, name, URN))
			{
				return false;
			}
			if (this.curDepth == -1)
			{
				this.SetDepth(depth);
			}
			else if (depth > this.curDepth)
			{
				return false;
			}
			if (this.curNode == parent.TopNode)
			{
				this.isMatch = true;
				return true;
			}
			DoubleLinkAxis ast = (DoubleLinkAxis)this.curNode.Next;
			if (Asttree.IsAttribute(ast))
			{
				this.isMatch = true;
				return false;
			}
			this.curNode = ast;
			this.curDepth++;
			return false;
		}

		// Token: 0x04002670 RID: 9840
		internal DoubleLinkAxis curNode;

		// Token: 0x04002671 RID: 9841
		internal int rootDepth;

		// Token: 0x04002672 RID: 9842
		internal int curDepth;

		// Token: 0x04002673 RID: 9843
		internal bool isMatch;
	}
}

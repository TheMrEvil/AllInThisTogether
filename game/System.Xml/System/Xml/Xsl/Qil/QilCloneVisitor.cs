using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004B8 RID: 1208
	internal class QilCloneVisitor : QilScopedVisitor
	{
		// Token: 0x06003013 RID: 12307 RVA: 0x0011FF0C File Offset: 0x0011E10C
		public QilCloneVisitor(QilFactory fac) : this(fac, new SubstitutionList())
		{
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x0011FF1A File Offset: 0x0011E11A
		public QilCloneVisitor(QilFactory fac, SubstitutionList subs)
		{
			this._fac = fac;
			this._subs = subs;
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x0011FF30 File Offset: 0x0011E130
		public QilNode Clone(QilNode node)
		{
			QilDepthChecker.Check(node);
			return this.VisitAssumeReference(node);
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x0011FF40 File Offset: 0x0011E140
		protected override QilNode Visit(QilNode oldNode)
		{
			QilNode qilNode = null;
			if (oldNode == null)
			{
				return null;
			}
			if (oldNode is QilReference)
			{
				qilNode = this.FindClonedReference(oldNode);
			}
			if (qilNode == null)
			{
				qilNode = oldNode.ShallowClone(this._fac);
			}
			return base.Visit(qilNode);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x0011FF7C File Offset: 0x0011E17C
		protected override QilNode VisitChildren(QilNode parent)
		{
			for (int i = 0; i < parent.Count; i++)
			{
				QilNode qilNode = parent[i];
				if (this.IsReference(parent, i))
				{
					parent[i] = this.VisitReference(qilNode);
					if (parent[i] == null)
					{
						parent[i] = qilNode;
					}
				}
				else
				{
					parent[i] = this.Visit(qilNode);
				}
			}
			return parent;
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x0011FFDC File Offset: 0x0011E1DC
		protected override QilNode VisitReference(QilNode oldNode)
		{
			QilNode qilNode = this.FindClonedReference(oldNode);
			return base.VisitReference((qilNode == null) ? oldNode : qilNode);
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x0011FFFE File Offset: 0x0011E1FE
		protected override void BeginScope(QilNode node)
		{
			this._subs.AddSubstitutionPair(node, node.ShallowClone(this._fac));
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x00120018 File Offset: 0x0011E218
		protected override void EndScope(QilNode node)
		{
			this._subs.RemoveLastSubstitutionPair();
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x00120025 File Offset: 0x0011E225
		protected QilNode FindClonedReference(QilNode node)
		{
			return this._subs.FindReplacement(node);
		}

		// Token: 0x040025BB RID: 9659
		private QilFactory _fac;

		// Token: 0x040025BC RID: 9660
		private SubstitutionList _subs;
	}
}

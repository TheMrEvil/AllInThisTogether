using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020004EF RID: 1263
	internal class SelectorActiveAxis : ActiveAxis
	{
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060033D1 RID: 13265 RVA: 0x001269B3 File Offset: 0x00124BB3
		public bool EmptyStack
		{
			get
			{
				return this.KSpointer == 0;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060033D2 RID: 13266 RVA: 0x001269BE File Offset: 0x00124BBE
		public int lastDepth
		{
			get
			{
				if (this.KSpointer != 0)
				{
					return ((KSStruct)this.KSs[this.KSpointer - 1]).depth;
				}
				return -1;
			}
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x001269E7 File Offset: 0x00124BE7
		public SelectorActiveAxis(Asttree axisTree, ConstraintStruct cs) : base(axisTree)
		{
			this.KSs = new ArrayList();
			this.cs = cs;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x00126A02 File Offset: 0x00124C02
		public override bool EndElement(string localname, string URN)
		{
			base.EndElement(localname, URN);
			return this.KSpointer > 0 && base.CurrentDepth == this.lastDepth;
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x00126A28 File Offset: 0x00124C28
		public int PushKS(int errline, int errcol)
		{
			KeySequence ks = new KeySequence(this.cs.TableDim, errline, errcol);
			KSStruct ksstruct;
			if (this.KSpointer < this.KSs.Count)
			{
				ksstruct = (KSStruct)this.KSs[this.KSpointer];
				ksstruct.ks = ks;
				for (int i = 0; i < this.cs.TableDim; i++)
				{
					ksstruct.fields[i].Reactivate(ks);
				}
			}
			else
			{
				ksstruct = new KSStruct(ks, this.cs.TableDim);
				for (int j = 0; j < this.cs.TableDim; j++)
				{
					ksstruct.fields[j] = new LocatedActiveAxis(this.cs.constraint.Fields[j], ks, j);
					this.cs.axisFields.Add(ksstruct.fields[j]);
				}
				this.KSs.Add(ksstruct);
			}
			ksstruct.depth = base.CurrentDepth - 1;
			int kspointer = this.KSpointer;
			this.KSpointer = kspointer + 1;
			return kspointer;
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x00126B30 File Offset: 0x00124D30
		public KeySequence PopKS()
		{
			ArrayList kss = this.KSs;
			int num = this.KSpointer - 1;
			this.KSpointer = num;
			return ((KSStruct)kss[num]).ks;
		}

		// Token: 0x040026B6 RID: 9910
		private ConstraintStruct cs;

		// Token: 0x040026B7 RID: 9911
		private ArrayList KSs;

		// Token: 0x040026B8 RID: 9912
		private int KSpointer;
	}
}

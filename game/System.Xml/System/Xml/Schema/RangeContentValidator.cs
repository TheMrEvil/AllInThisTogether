using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000508 RID: 1288
	internal sealed class RangeContentValidator : ContentValidator
	{
		// Token: 0x06003476 RID: 13430 RVA: 0x00128C00 File Offset: 0x00126E00
		internal RangeContentValidator(BitSet firstpos, BitSet[] followpos, SymbolsDictionary symbols, Positions positions, int endMarkerPos, XmlSchemaContentType contentType, bool isEmptiable, BitSet positionsWithRangeTerminals, int minmaxNodesCount) : base(contentType, false, isEmptiable)
		{
			this.firstpos = firstpos;
			this.followpos = followpos;
			this.symbols = symbols;
			this.positions = positions;
			this.positionsWithRangeTerminals = positionsWithRangeTerminals;
			this.minMaxNodesCount = minmaxNodesCount;
			this.endMarkerPos = endMarkerPos;
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x00128C50 File Offset: 0x00126E50
		public override void InitValidation(ValidationState context)
		{
			int count = this.positions.Count;
			List<RangePositionInfo> list = context.RunningPositions;
			if (list != null)
			{
				list.Clear();
			}
			else
			{
				list = new List<RangePositionInfo>();
				context.RunningPositions = list;
			}
			RangePositionInfo rangePositionInfo = new RangePositionInfo
			{
				curpos = this.firstpos.Clone(),
				rangeCounters = new decimal[this.minMaxNodesCount]
			};
			list.Add(rangePositionInfo);
			context.CurrentState.NumberOfRunningPos = 1;
			context.HasMatched = rangePositionInfo.curpos.Get(this.endMarkerPos);
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x00128CE0 File Offset: 0x00126EE0
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			errorCode = 0;
			int num = this.symbols[name];
			bool flag = false;
			List<RangePositionInfo> runningPositions = context.RunningPositions;
			int num2 = context.CurrentState.NumberOfRunningPos;
			int i = 0;
			int num3 = -1;
			int num4 = -1;
			bool flag2 = false;
			while (i < num2)
			{
				RangePositionInfo rangePositionInfo = runningPositions[i];
				BitSet curpos = rangePositionInfo.curpos;
				for (int num5 = curpos.NextSet(-1); num5 != -1; num5 = curpos.NextSet(num5))
				{
					if (num == this.positions[num5].symbol)
					{
						num3 = num5;
						if (num4 == -1)
						{
							num4 = i;
						}
						flag2 = true;
						break;
					}
				}
				if (flag2 && this.positions[num3].particle is XmlSchemaElement)
				{
					break;
				}
				i++;
			}
			if (i == num2 && num3 != -1)
			{
				i = num4;
			}
			if (i < num2)
			{
				if (i != 0)
				{
					runningPositions.RemoveRange(0, i);
				}
				num2 -= i;
				i = 0;
				while (i < num2)
				{
					RangePositionInfo rangePositionInfo = runningPositions[i];
					flag2 = rangePositionInfo.curpos.Get(num3);
					if (flag2)
					{
						rangePositionInfo.curpos = this.followpos[num3];
						runningPositions[i] = rangePositionInfo;
						i++;
					}
					else
					{
						num2--;
						if (num2 > 0)
						{
							RangePositionInfo value = runningPositions[num2];
							runningPositions[num2] = runningPositions[i];
							runningPositions[i] = value;
						}
					}
				}
			}
			else
			{
				num2 = 0;
			}
			if (num2 > 0)
			{
				if (num2 >= 10000)
				{
					context.TooComplex = true;
					num2 /= 2;
				}
				for (i = num2 - 1; i >= 0; i--)
				{
					int index = i;
					BitSet curpos2 = runningPositions[i].curpos;
					flag = (flag || curpos2.Get(this.endMarkerPos));
					while (num2 < 10000 && curpos2.Intersects(this.positionsWithRangeTerminals))
					{
						BitSet bitSet = curpos2.Clone();
						bitSet.And(this.positionsWithRangeTerminals);
						int num6 = bitSet.NextSet(-1);
						LeafRangeNode leafRangeNode = this.positions[num6].particle as LeafRangeNode;
						RangePositionInfo rangePositionInfo = runningPositions[index];
						if (num2 + 2 >= runningPositions.Count)
						{
							runningPositions.Add(default(RangePositionInfo));
							runningPositions.Add(default(RangePositionInfo));
						}
						RangePositionInfo rangePositionInfo2 = runningPositions[num2];
						if (rangePositionInfo2.rangeCounters == null)
						{
							rangePositionInfo2.rangeCounters = new decimal[this.minMaxNodesCount];
						}
						Array.Copy(rangePositionInfo.rangeCounters, 0, rangePositionInfo2.rangeCounters, 0, rangePositionInfo.rangeCounters.Length);
						decimal[] rangeCounters = rangePositionInfo2.rangeCounters;
						int pos = leafRangeNode.Pos;
						decimal num7 = ++rangeCounters[pos];
						rangeCounters[pos] = num7;
						decimal d = num7;
						if (d == leafRangeNode.Max)
						{
							rangePositionInfo2.curpos = this.followpos[num6];
							rangePositionInfo2.rangeCounters[leafRangeNode.Pos] = 0m;
							runningPositions[num2] = rangePositionInfo2;
							index = num2++;
						}
						else
						{
							if (d < leafRangeNode.Min)
							{
								rangePositionInfo2.curpos = leafRangeNode.NextIteration;
								runningPositions[num2] = rangePositionInfo2;
								num2++;
								break;
							}
							rangePositionInfo2.curpos = leafRangeNode.NextIteration;
							runningPositions[num2] = rangePositionInfo2;
							index = num2 + 1;
							rangePositionInfo2 = runningPositions[index];
							if (rangePositionInfo2.rangeCounters == null)
							{
								rangePositionInfo2.rangeCounters = new decimal[this.minMaxNodesCount];
							}
							Array.Copy(rangePositionInfo.rangeCounters, 0, rangePositionInfo2.rangeCounters, 0, rangePositionInfo.rangeCounters.Length);
							rangePositionInfo2.curpos = this.followpos[num6];
							rangePositionInfo2.rangeCounters[leafRangeNode.Pos] = 0m;
							runningPositions[index] = rangePositionInfo2;
							num2 += 2;
						}
						curpos2 = runningPositions[index].curpos;
						flag = (flag || curpos2.Get(this.endMarkerPos));
					}
				}
				context.HasMatched = flag;
				context.CurrentState.NumberOfRunningPos = num2;
				return this.positions[num3].particle;
			}
			errorCode = -1;
			context.NeedValidateChildren = false;
			return null;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x001290F4 File Offset: 0x001272F4
		public override bool CompleteValidation(ValidationState context)
		{
			return context.HasMatched;
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x001290FC File Offset: 0x001272FC
		public override ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = null;
			if (context.RunningPositions != null)
			{
				List<RangePositionInfo> runningPositions = context.RunningPositions;
				BitSet bitSet = new BitSet(this.positions.Count);
				for (int i = context.CurrentState.NumberOfRunningPos - 1; i >= 0; i--)
				{
					bitSet.Or(runningPositions[i].curpos);
				}
				for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					if (this.positions[num].symbol >= 0)
					{
						XmlSchemaParticle xmlSchemaParticle = this.positions[num].particle as XmlSchemaParticle;
						if (xmlSchemaParticle == null)
						{
							string text = this.symbols.NameOf(this.positions[num].symbol);
							if (text.Length != 0)
							{
								arrayList.Add(text);
							}
						}
						else
						{
							string nameString = xmlSchemaParticle.NameString;
							if (!arrayList.Contains(nameString))
							{
								arrayList.Add(nameString);
							}
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x00129204 File Offset: 0x00127404
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			ArrayList arrayList = new ArrayList();
			if (context.RunningPositions != null)
			{
				List<RangePositionInfo> runningPositions = context.RunningPositions;
				BitSet bitSet = new BitSet(this.positions.Count);
				for (int i = context.CurrentState.NumberOfRunningPos - 1; i >= 0; i--)
				{
					bitSet.Or(runningPositions[i].curpos);
				}
				for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
				{
					if (this.positions[num].symbol >= 0)
					{
						XmlSchemaParticle xmlSchemaParticle = this.positions[num].particle as XmlSchemaParticle;
						if (xmlSchemaParticle != null)
						{
							ContentValidator.AddParticleToExpected(xmlSchemaParticle, schemaSet, arrayList);
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x040026F8 RID: 9976
		private BitSet firstpos;

		// Token: 0x040026F9 RID: 9977
		private BitSet[] followpos;

		// Token: 0x040026FA RID: 9978
		private BitSet positionsWithRangeTerminals;

		// Token: 0x040026FB RID: 9979
		private SymbolsDictionary symbols;

		// Token: 0x040026FC RID: 9980
		private Positions positions;

		// Token: 0x040026FD RID: 9981
		private int minMaxNodesCount;

		// Token: 0x040026FE RID: 9982
		private int endMarkerPos;
	}
}

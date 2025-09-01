using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004A4 RID: 1188
	internal static class TailCallAnalyzer
	{
		// Token: 0x06002E81 RID: 11905 RVA: 0x001100C4 File Offset: 0x0010E2C4
		public static void Analyze(QilExpression qil)
		{
			foreach (QilNode qilNode in qil.FunctionList)
			{
				QilFunction qilFunction = (QilFunction)qilNode;
				if (XmlILConstructInfo.Read(qilFunction).ConstructMethod == XmlILConstructMethod.Writer)
				{
					TailCallAnalyzer.AnalyzeDefinition(qilFunction.Definition);
				}
			}
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x00110128 File Offset: 0x0010E328
		private static void AnalyzeDefinition(QilNode nd)
		{
			QilNodeType nodeType = nd.NodeType;
			if (nodeType <= QilNodeType.Sequence)
			{
				if (nodeType != QilNodeType.Nop)
				{
					switch (nodeType)
					{
					case QilNodeType.Conditional:
					{
						QilTernary qilTernary = (QilTernary)nd;
						TailCallAnalyzer.AnalyzeDefinition(qilTernary.Center);
						TailCallAnalyzer.AnalyzeDefinition(qilTernary.Right);
						return;
					}
					case QilNodeType.Choice:
					{
						QilChoice qilChoice = (QilChoice)nd;
						for (int i = 0; i < qilChoice.Branches.Count; i++)
						{
							TailCallAnalyzer.AnalyzeDefinition(qilChoice.Branches[i]);
						}
						return;
					}
					case QilNodeType.Length:
						break;
					case QilNodeType.Sequence:
					{
						QilList qilList = (QilList)nd;
						if (qilList.Count > 0)
						{
							TailCallAnalyzer.AnalyzeDefinition(qilList[qilList.Count - 1]);
							return;
						}
						break;
					}
					default:
						return;
					}
				}
				else
				{
					TailCallAnalyzer.AnalyzeDefinition(((QilUnary)nd).Child);
				}
			}
			else if (nodeType != QilNodeType.Loop)
			{
				if (nodeType == QilNodeType.Invoke && XmlILConstructInfo.Read(nd).ConstructMethod == XmlILConstructMethod.Writer)
				{
					OptimizerPatterns.Write(nd).AddPattern(OptimizerPatternName.TailCall);
					return;
				}
			}
			else
			{
				QilLoop qilLoop = (QilLoop)nd;
				if (qilLoop.Variable.NodeType == QilNodeType.Let || !qilLoop.Variable.Binding.XmlType.MaybeMany)
				{
					TailCallAnalyzer.AnalyzeDefinition(qilLoop.Body);
					return;
				}
			}
		}
	}
}

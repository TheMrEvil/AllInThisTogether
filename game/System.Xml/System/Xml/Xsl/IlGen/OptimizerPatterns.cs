using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004A1 RID: 1185
	internal class OptimizerPatterns : IQilAnnotation
	{
		// Token: 0x06002E64 RID: 11876 RVA: 0x0010FAD8 File Offset: 0x0010DCD8
		public static OptimizerPatterns Read(QilNode nd)
		{
			XmlILAnnotation xmlILAnnotation = nd.Annotation as XmlILAnnotation;
			OptimizerPatterns optimizerPatterns = (xmlILAnnotation != null) ? xmlILAnnotation.Patterns : null;
			if (optimizerPatterns == null)
			{
				if (!nd.XmlType.MaybeMany)
				{
					if (OptimizerPatterns.ZeroOrOneDefault == null)
					{
						optimizerPatterns = new OptimizerPatterns();
						optimizerPatterns.AddPattern(OptimizerPatternName.IsDocOrderDistinct);
						optimizerPatterns.AddPattern(OptimizerPatternName.SameDepth);
						optimizerPatterns.isReadOnly = true;
						OptimizerPatterns.ZeroOrOneDefault = optimizerPatterns;
					}
					else
					{
						optimizerPatterns = OptimizerPatterns.ZeroOrOneDefault;
					}
				}
				else if (nd.XmlType.IsDod)
				{
					if (OptimizerPatterns.DodDefault == null)
					{
						optimizerPatterns = new OptimizerPatterns();
						optimizerPatterns.AddPattern(OptimizerPatternName.IsDocOrderDistinct);
						optimizerPatterns.isReadOnly = true;
						OptimizerPatterns.DodDefault = optimizerPatterns;
					}
					else
					{
						optimizerPatterns = OptimizerPatterns.DodDefault;
					}
				}
				else if (OptimizerPatterns.MaybeManyDefault == null)
				{
					optimizerPatterns = new OptimizerPatterns();
					optimizerPatterns.isReadOnly = true;
					OptimizerPatterns.MaybeManyDefault = optimizerPatterns;
				}
				else
				{
					optimizerPatterns = OptimizerPatterns.MaybeManyDefault;
				}
			}
			return optimizerPatterns;
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x0010FBB4 File Offset: 0x0010DDB4
		public static OptimizerPatterns Write(QilNode nd)
		{
			XmlILAnnotation xmlILAnnotation = XmlILAnnotation.Write(nd);
			OptimizerPatterns optimizerPatterns = xmlILAnnotation.Patterns;
			if (optimizerPatterns == null || optimizerPatterns.isReadOnly)
			{
				optimizerPatterns = new OptimizerPatterns();
				xmlILAnnotation.Patterns = optimizerPatterns;
				if (!nd.XmlType.MaybeMany)
				{
					optimizerPatterns.AddPattern(OptimizerPatternName.IsDocOrderDistinct);
					optimizerPatterns.AddPattern(OptimizerPatternName.SameDepth);
				}
				else if (nd.XmlType.IsDod)
				{
					optimizerPatterns.AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				}
			}
			return optimizerPatterns;
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x0010FC1C File Offset: 0x0010DE1C
		public static void Inherit(QilNode ndSrc, QilNode ndDst, OptimizerPatternName pattern)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(ndSrc);
			if (optimizerPatterns.MatchesPattern(pattern))
			{
				OptimizerPatterns optimizerPatterns2 = OptimizerPatterns.Write(ndDst);
				optimizerPatterns2.AddPattern(pattern);
				switch (pattern)
				{
				case OptimizerPatternName.DodReverse:
				case OptimizerPatternName.JoinAndDod:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.ElementQName, optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
					return;
				case OptimizerPatternName.EqualityIndex:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.StepNode, optimizerPatterns.GetArgument(OptimizerPatternArgument.StepNode));
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.StepInput, optimizerPatterns.GetArgument(OptimizerPatternArgument.StepInput));
					return;
				case OptimizerPatternName.FilterAttributeKind:
				case OptimizerPatternName.IsDocOrderDistinct:
				case OptimizerPatternName.IsPositional:
				case OptimizerPatternName.SameDepth:
					break;
				case OptimizerPatternName.FilterContentKind:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.ElementQName, optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
					return;
				case OptimizerPatternName.FilterElements:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.ElementQName, optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
					return;
				case OptimizerPatternName.MaxPosition:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.ElementQName, optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
					return;
				case OptimizerPatternName.Step:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.StepNode, optimizerPatterns.GetArgument(OptimizerPatternArgument.StepNode));
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.StepInput, optimizerPatterns.GetArgument(OptimizerPatternArgument.StepInput));
					return;
				case OptimizerPatternName.SingleTextRtf:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.ElementQName, optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x0010FD08 File Offset: 0x0010DF08
		public void AddArgument(OptimizerPatternArgument argId, object arg)
		{
			switch (argId)
			{
			case OptimizerPatternArgument.StepNode:
				this.arg0 = arg;
				return;
			case OptimizerPatternArgument.StepInput:
				this.arg1 = arg;
				return;
			case OptimizerPatternArgument.ElementQName:
				this.arg2 = arg;
				return;
			default:
				return;
			}
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x0010FD44 File Offset: 0x0010DF44
		public object GetArgument(OptimizerPatternArgument argNum)
		{
			object result = null;
			switch (argNum)
			{
			case OptimizerPatternArgument.StepNode:
				result = this.arg0;
				break;
			case OptimizerPatternArgument.StepInput:
				result = this.arg1;
				break;
			case OptimizerPatternArgument.ElementQName:
				result = this.arg2;
				break;
			}
			return result;
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x0010FD83 File Offset: 0x0010DF83
		public void AddPattern(OptimizerPatternName pattern)
		{
			this.patterns |= 1 << (int)pattern;
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x0010FD98 File Offset: 0x0010DF98
		public bool MatchesPattern(OptimizerPatternName pattern)
		{
			return (this.patterns & 1 << (int)pattern) != 0;
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002E6B RID: 11883 RVA: 0x0010FDAA File Offset: 0x0010DFAA
		public virtual string Name
		{
			get
			{
				return "Patterns";
			}
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x0010FDB4 File Offset: 0x0010DFB4
		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < OptimizerPatterns.PatternCount; i++)
			{
				if (this.MatchesPattern((OptimizerPatternName)i))
				{
					if (text.Length != 0)
					{
						text += ", ";
					}
					string str = text;
					OptimizerPatternName optimizerPatternName = (OptimizerPatternName)i;
					text = str + optimizerPatternName.ToString();
				}
			}
			return text;
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x0000216B File Offset: 0x0000036B
		public OptimizerPatterns()
		{
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x0010FE0B File Offset: 0x0010E00B
		// Note: this type is marked as 'beforefieldinit'.
		static OptimizerPatterns()
		{
		}

		// Token: 0x040024C4 RID: 9412
		private static readonly int PatternCount = Enum.GetValues(typeof(OptimizerPatternName)).Length;

		// Token: 0x040024C5 RID: 9413
		private int patterns;

		// Token: 0x040024C6 RID: 9414
		private bool isReadOnly;

		// Token: 0x040024C7 RID: 9415
		private object arg0;

		// Token: 0x040024C8 RID: 9416
		private object arg1;

		// Token: 0x040024C9 RID: 9417
		private object arg2;

		// Token: 0x040024CA RID: 9418
		private static volatile OptimizerPatterns ZeroOrOneDefault;

		// Token: 0x040024CB RID: 9419
		private static volatile OptimizerPatterns MaybeManyDefault;

		// Token: 0x040024CC RID: 9420
		private static volatile OptimizerPatterns DodDefault;
	}
}

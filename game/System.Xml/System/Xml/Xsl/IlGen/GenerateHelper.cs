using System;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x0200049A RID: 1178
	internal class GenerateHelper
	{
		// Token: 0x06002DDF RID: 11743 RVA: 0x0010E249 File Offset: 0x0010C449
		public GenerateHelper(XmlILModule module, bool isDebug)
		{
			this.isDebug = isDebug;
			this.module = module;
			this.staticData = new StaticDataManager();
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x0010E26C File Offset: 0x0010C46C
		public void MethodBegin(MethodBase methInfo, ISourceLineInfo sourceInfo, bool initWriters)
		{
			this.methInfo = methInfo;
			this.ilgen = XmlILModule.DefineMethodBody(methInfo);
			this.lastSourceInfo = null;
			if (this.isDebug)
			{
				this.DebugStartScope();
				if (sourceInfo != null)
				{
					this.MarkSequencePoint(sourceInfo);
					this.Emit(OpCodes.Nop);
				}
			}
			else if (this.module.EmitSymbols && sourceInfo != null)
			{
				this.MarkSequencePoint(sourceInfo);
				this.lastSourceInfo = null;
			}
			this.initWriters = false;
			if (initWriters)
			{
				this.EnsureWriter();
				this.LoadQueryRuntime();
				this.Call(XmlILMethods.GetOutput);
				this.Emit(OpCodes.Stloc, this.locXOut);
			}
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x0010E308 File Offset: 0x0010C508
		public void MethodEnd()
		{
			this.Emit(OpCodes.Ret);
			if (this.isDebug)
			{
				this.DebugEndScope();
			}
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x0010E323 File Offset: 0x0010C523
		public void CallSyncToNavigator()
		{
			if (this.methSyncToNav == null)
			{
				this.methSyncToNav = this.module.FindMethod("SyncToNavigator");
			}
			this.Call(this.methSyncToNav);
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x0010E355 File Offset: 0x0010C555
		public StaticDataManager StaticData
		{
			get
			{
				return this.staticData;
			}
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x0010E360 File Offset: 0x0010C560
		public void LoadInteger(int intVal)
		{
			if (intVal >= -1 && intVal < 9)
			{
				OpCode opcode;
				switch (intVal)
				{
				case -1:
					opcode = OpCodes.Ldc_I4_M1;
					break;
				case 0:
					opcode = OpCodes.Ldc_I4_0;
					break;
				case 1:
					opcode = OpCodes.Ldc_I4_1;
					break;
				case 2:
					opcode = OpCodes.Ldc_I4_2;
					break;
				case 3:
					opcode = OpCodes.Ldc_I4_3;
					break;
				case 4:
					opcode = OpCodes.Ldc_I4_4;
					break;
				case 5:
					opcode = OpCodes.Ldc_I4_5;
					break;
				case 6:
					opcode = OpCodes.Ldc_I4_6;
					break;
				case 7:
					opcode = OpCodes.Ldc_I4_7;
					break;
				case 8:
					opcode = OpCodes.Ldc_I4_8;
					break;
				default:
					return;
				}
				this.Emit(opcode);
				return;
			}
			if (intVal >= -128 && intVal <= 127)
			{
				this.Emit(OpCodes.Ldc_I4_S, (sbyte)intVal);
				return;
			}
			this.Emit(OpCodes.Ldc_I4, intVal);
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x0010E427 File Offset: 0x0010C627
		public void LoadBoolean(bool boolVal)
		{
			this.Emit(boolVal ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x0010E43E File Offset: 0x0010C63E
		public void LoadType(Type clrTyp)
		{
			this.Emit(OpCodes.Ldtoken, clrTyp);
			this.Call(XmlILMethods.GetTypeFromHandle);
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x0010E457 File Offset: 0x0010C657
		public LocalBuilder DeclareLocal(string name, Type type)
		{
			return this.ilgen.DeclareLocal(type);
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x0010E465 File Offset: 0x0010C665
		public void LoadQueryRuntime()
		{
			this.Emit(OpCodes.Ldarg_0);
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x0010E472 File Offset: 0x0010C672
		public void LoadQueryContext()
		{
			this.Emit(OpCodes.Ldarg_0);
			this.Call(XmlILMethods.Context);
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x0010E48A File Offset: 0x0010C68A
		public void LoadXsltLibrary()
		{
			this.Emit(OpCodes.Ldarg_0);
			this.Call(XmlILMethods.XsltLib);
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x0010E4A2 File Offset: 0x0010C6A2
		public void LoadQueryOutput()
		{
			this.Emit(OpCodes.Ldloc, this.locXOut);
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x0010E4B8 File Offset: 0x0010C6B8
		public void LoadParameter(int paramPos)
		{
			switch (paramPos)
			{
			case 0:
				this.Emit(OpCodes.Ldarg_0);
				return;
			case 1:
				this.Emit(OpCodes.Ldarg_1);
				return;
			case 2:
				this.Emit(OpCodes.Ldarg_2);
				return;
			case 3:
				this.Emit(OpCodes.Ldarg_3);
				return;
			default:
				if (paramPos <= 255)
				{
					this.Emit(OpCodes.Ldarg_S, (byte)paramPos);
					return;
				}
				if (paramPos <= 65535)
				{
					this.Emit(OpCodes.Ldarg, paramPos);
					return;
				}
				throw new XslTransformException("Functions may not have more than 65535 parameters.");
			}
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x0010E544 File Offset: 0x0010C744
		public void SetParameter(object paramId)
		{
			int num = (int)paramId;
			if (num <= 255)
			{
				this.Emit(OpCodes.Starg_S, (byte)num);
				return;
			}
			if (num <= 65535)
			{
				this.Emit(OpCodes.Starg, num);
				return;
			}
			throw new XslTransformException("Functions may not have more than 65535 parameters.");
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x0010E58D File Offset: 0x0010C78D
		public void BranchAndMark(Label lblBranch, Label lblMark)
		{
			if (!lblBranch.Equals(lblMark))
			{
				this.EmitUnconditionalBranch(OpCodes.Br, lblBranch);
			}
			this.MarkLabel(lblMark);
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x0010E5AC File Offset: 0x0010C7AC
		public void TestAndBranch(int i4, Label lblBranch, OpCode opcodeBranch)
		{
			if (i4 == 0)
			{
				if (opcodeBranch.Value == OpCodes.Beq.Value)
				{
					opcodeBranch = OpCodes.Brfalse;
					goto IL_7A;
				}
				if (opcodeBranch.Value == OpCodes.Beq_S.Value)
				{
					opcodeBranch = OpCodes.Brfalse_S;
					goto IL_7A;
				}
				if (opcodeBranch.Value == OpCodes.Bne_Un.Value)
				{
					opcodeBranch = OpCodes.Brtrue;
					goto IL_7A;
				}
				if (opcodeBranch.Value == OpCodes.Bne_Un_S.Value)
				{
					opcodeBranch = OpCodes.Brtrue_S;
					goto IL_7A;
				}
			}
			this.LoadInteger(i4);
			IL_7A:
			this.Emit(opcodeBranch, lblBranch);
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x0010E63C File Offset: 0x0010C83C
		public void ConvBranchToBool(Label lblBranch, bool isTrueBranch)
		{
			Label label = this.DefineLabel();
			this.Emit(isTrueBranch ? OpCodes.Ldc_I4_0 : OpCodes.Ldc_I4_1);
			this.EmitUnconditionalBranch(OpCodes.Br_S, label);
			this.MarkLabel(lblBranch);
			this.Emit(isTrueBranch ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
			this.MarkLabel(label);
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x0010E694 File Offset: 0x0010C894
		public void TailCall(MethodInfo meth)
		{
			this.Emit(OpCodes.Tailcall);
			this.Call(meth);
			this.Emit(OpCodes.Ret);
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void TraceCall(OpCode opcode, MethodInfo meth)
		{
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x0010E6B4 File Offset: 0x0010C8B4
		public void Call(MethodInfo meth)
		{
			OpCode opcode = (meth.IsVirtual || meth.IsAbstract) ? OpCodes.Callvirt : OpCodes.Call;
			this.ilgen.Emit(opcode, meth);
			if (this.lastSourceInfo != null)
			{
				this.MarkSequencePoint(SourceLineInfo.NoSource);
			}
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x0010E700 File Offset: 0x0010C900
		public void CallToken(MethodInfo meth)
		{
			MethodBuilder methodBuilder = this.methInfo as MethodBuilder;
			if (methodBuilder != null)
			{
				OpCode opcode = (meth.IsVirtual || meth.IsAbstract) ? OpCodes.Callvirt : OpCodes.Call;
				this.ilgen.Emit(opcode, ((ModuleBuilder)methodBuilder.GetModule()).GetMethodToken(meth).Token);
				if (this.lastSourceInfo != null)
				{
					this.MarkSequencePoint(SourceLineInfo.NoSource);
					return;
				}
			}
			else
			{
				this.Call(meth);
			}
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x0010E77F File Offset: 0x0010C97F
		public void Construct(ConstructorInfo constr)
		{
			this.Emit(OpCodes.Newobj, constr);
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x0010E790 File Offset: 0x0010C990
		public void CallConcatStrings(int cStrings)
		{
			switch (cStrings)
			{
			case 0:
				this.Emit(OpCodes.Ldstr, "");
				return;
			case 1:
				break;
			case 2:
				this.Call(XmlILMethods.StrCat2);
				return;
			case 3:
				this.Call(XmlILMethods.StrCat3);
				return;
			case 4:
				this.Call(XmlILMethods.StrCat4);
				break;
			default:
				return;
			}
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x0010E7EC File Offset: 0x0010C9EC
		public void TreatAs(Type clrTypeSrc, Type clrTypeDst)
		{
			if (clrTypeSrc == clrTypeDst)
			{
				return;
			}
			if (clrTypeSrc.IsValueType)
			{
				this.Emit(OpCodes.Box, clrTypeSrc);
				return;
			}
			if (clrTypeDst.IsValueType)
			{
				this.Emit(OpCodes.Unbox, clrTypeDst);
				this.Emit(OpCodes.Ldobj, clrTypeDst);
				return;
			}
			if (clrTypeDst != typeof(object))
			{
				this.Emit(OpCodes.Castclass, clrTypeDst);
			}
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x0010E858 File Offset: 0x0010CA58
		public void ConstructLiteralDecimal(decimal dec)
		{
			if (dec >= -2147483648m && dec <= 2147483647m && decimal.Truncate(dec) == dec)
			{
				this.LoadInteger((int)dec);
				this.Construct(XmlILConstructors.DecFromInt32);
				return;
			}
			int[] bits = decimal.GetBits(dec);
			this.LoadInteger(bits[0]);
			this.LoadInteger(bits[1]);
			this.LoadInteger(bits[2]);
			this.LoadBoolean(bits[3] < 0);
			this.LoadInteger(bits[3] >> 16);
			this.Construct(XmlILConstructors.DecFromParts);
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x0010E8F4 File Offset: 0x0010CAF4
		public void ConstructLiteralQName(string localName, string namespaceName)
		{
			this.Emit(OpCodes.Ldstr, localName);
			this.Emit(OpCodes.Ldstr, namespaceName);
			this.Construct(XmlILConstructors.QName);
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x0010E91C File Offset: 0x0010CB1C
		public void CallArithmeticOp(QilNodeType opType, XmlTypeCode code)
		{
			MethodInfo meth = null;
			if (code <= XmlTypeCode.Double)
			{
				if (code == XmlTypeCode.Decimal)
				{
					switch (opType)
					{
					case QilNodeType.Negate:
						meth = XmlILMethods.DecNeg;
						break;
					case QilNodeType.Add:
						meth = XmlILMethods.DecAdd;
						break;
					case QilNodeType.Subtract:
						meth = XmlILMethods.DecSub;
						break;
					case QilNodeType.Multiply:
						meth = XmlILMethods.DecMul;
						break;
					case QilNodeType.Divide:
						meth = XmlILMethods.DecDiv;
						break;
					case QilNodeType.Modulo:
						meth = XmlILMethods.DecRem;
						break;
					}
					this.Call(meth);
					return;
				}
				if (code - XmlTypeCode.Float > 1)
				{
					return;
				}
			}
			else if (code != XmlTypeCode.Integer && code != XmlTypeCode.Int)
			{
				return;
			}
			switch (opType)
			{
			case QilNodeType.Negate:
				this.Emit(OpCodes.Neg);
				return;
			case QilNodeType.Add:
				this.Emit(OpCodes.Add);
				return;
			case QilNodeType.Subtract:
				this.Emit(OpCodes.Sub);
				return;
			case QilNodeType.Multiply:
				this.Emit(OpCodes.Mul);
				return;
			case QilNodeType.Divide:
				this.Emit(OpCodes.Div);
				return;
			case QilNodeType.Modulo:
				this.Emit(OpCodes.Rem);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x0010EA0C File Offset: 0x0010CC0C
		public void CallCompareEquals(XmlTypeCode code)
		{
			MethodInfo meth = null;
			if (code != XmlTypeCode.String)
			{
				if (code != XmlTypeCode.Decimal)
				{
					if (code == XmlTypeCode.QName)
					{
						meth = XmlILMethods.QNameEq;
					}
				}
				else
				{
					meth = XmlILMethods.DecEq;
				}
			}
			else
			{
				meth = XmlILMethods.StrEq;
			}
			this.Call(meth);
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x0010EA4C File Offset: 0x0010CC4C
		public void CallCompare(XmlTypeCode code)
		{
			MethodInfo meth = null;
			if (code != XmlTypeCode.String)
			{
				if (code == XmlTypeCode.Decimal)
				{
					meth = XmlILMethods.DecCmp;
				}
			}
			else
			{
				meth = XmlILMethods.StrCmp;
			}
			this.Call(meth);
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x0010EA7C File Offset: 0x0010CC7C
		public void CallStartRtfConstruction(string baseUri)
		{
			this.EnsureWriter();
			this.LoadQueryRuntime();
			this.Emit(OpCodes.Ldstr, baseUri);
			this.Emit(OpCodes.Ldloca, this.locXOut);
			this.Call(XmlILMethods.StartRtfConstr);
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x0010EAB2 File Offset: 0x0010CCB2
		public void CallEndRtfConstruction()
		{
			this.LoadQueryRuntime();
			this.Emit(OpCodes.Ldloca, this.locXOut);
			this.Call(XmlILMethods.EndRtfConstr);
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x0010EAD6 File Offset: 0x0010CCD6
		public void CallStartSequenceConstruction()
		{
			this.EnsureWriter();
			this.LoadQueryRuntime();
			this.Emit(OpCodes.Ldloca, this.locXOut);
			this.Call(XmlILMethods.StartSeqConstr);
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x0010EB00 File Offset: 0x0010CD00
		public void CallEndSequenceConstruction()
		{
			this.LoadQueryRuntime();
			this.Emit(OpCodes.Ldloca, this.locXOut);
			this.Call(XmlILMethods.EndSeqConstr);
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x0010EB24 File Offset: 0x0010CD24
		public void CallGetEarlyBoundObject(int idxObj, Type clrType)
		{
			this.LoadQueryRuntime();
			this.LoadInteger(idxObj);
			this.Call(XmlILMethods.GetEarly);
			this.TreatAs(typeof(object), clrType);
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x0010EB4F File Offset: 0x0010CD4F
		public void CallGetAtomizedName(int idxName)
		{
			this.LoadQueryRuntime();
			this.LoadInteger(idxName);
			this.Call(XmlILMethods.GetAtomizedName);
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x0010EB69 File Offset: 0x0010CD69
		public void CallGetNameFilter(int idxFilter)
		{
			this.LoadQueryRuntime();
			this.LoadInteger(idxFilter);
			this.Call(XmlILMethods.GetNameFilter);
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x0010EB83 File Offset: 0x0010CD83
		public void CallGetTypeFilter(XPathNodeType nodeType)
		{
			this.LoadQueryRuntime();
			this.LoadInteger((int)nodeType);
			this.Call(XmlILMethods.GetTypeFilter);
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x0010EB9D File Offset: 0x0010CD9D
		public void CallParseTagName(GenerateNameType nameType)
		{
			if (nameType == GenerateNameType.TagNameAndMappings)
			{
				this.Call(XmlILMethods.TagAndMappings);
				return;
			}
			this.Call(XmlILMethods.TagAndNamespace);
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x0010EBBA File Offset: 0x0010CDBA
		public void CallGetGlobalValue(int idxValue, Type clrType)
		{
			this.LoadQueryRuntime();
			this.LoadInteger(idxValue);
			this.Call(XmlILMethods.GetGlobalValue);
			this.TreatAs(typeof(object), clrType);
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x0010EBE5 File Offset: 0x0010CDE5
		public void CallSetGlobalValue(Type clrType)
		{
			this.TreatAs(clrType, typeof(object));
			this.Call(XmlILMethods.SetGlobalValue);
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x0010EC03 File Offset: 0x0010CE03
		public void CallGetCollation(int idxName)
		{
			this.LoadQueryRuntime();
			this.LoadInteger(idxName);
			this.Call(XmlILMethods.GetCollation);
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x0010EC1D File Offset: 0x0010CE1D
		private void EnsureWriter()
		{
			if (!this.initWriters)
			{
				this.locXOut = this.DeclareLocal("$$$xwrtChk", typeof(XmlQueryOutput));
				this.initWriters = true;
			}
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x0010EC49 File Offset: 0x0010CE49
		public void CallGetParameter(string localName, string namespaceUri)
		{
			this.LoadQueryContext();
			this.Emit(OpCodes.Ldstr, localName);
			this.Emit(OpCodes.Ldstr, namespaceUri);
			this.Call(XmlILMethods.GetParam);
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x0010EC74 File Offset: 0x0010CE74
		public void CallStartTree(XPathNodeType rootType)
		{
			this.LoadQueryOutput();
			this.LoadInteger((int)rootType);
			this.Call(XmlILMethods.StartTree);
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x0010EC8E File Offset: 0x0010CE8E
		public void CallEndTree()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.EndTree);
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x0010ECA1 File Offset: 0x0010CEA1
		public void CallWriteStartRoot()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.StartRoot);
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x0010ECB4 File Offset: 0x0010CEB4
		public void CallWriteEndRoot()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.EndRoot);
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x0010ECC8 File Offset: 0x0010CEC8
		public void CallWriteStartElement(GenerateNameType nameType, bool callChk)
		{
			MethodInfo meth = null;
			if (callChk)
			{
				switch (nameType)
				{
				case GenerateNameType.LiteralLocalName:
					meth = XmlILMethods.StartElemLocName;
					break;
				case GenerateNameType.LiteralName:
					meth = XmlILMethods.StartElemLitName;
					break;
				case GenerateNameType.CopiedName:
					meth = XmlILMethods.StartElemCopyName;
					break;
				case GenerateNameType.TagNameAndMappings:
					meth = XmlILMethods.StartElemMapName;
					break;
				case GenerateNameType.TagNameAndNamespace:
					meth = XmlILMethods.StartElemNmspName;
					break;
				case GenerateNameType.QName:
					meth = XmlILMethods.StartElemQName;
					break;
				}
			}
			else if (nameType != GenerateNameType.LiteralLocalName)
			{
				if (nameType == GenerateNameType.LiteralName)
				{
					meth = XmlILMethods.StartElemLitNameUn;
				}
			}
			else
			{
				meth = XmlILMethods.StartElemLocNameUn;
			}
			this.Call(meth);
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x0010ED48 File Offset: 0x0010CF48
		public void CallWriteEndElement(GenerateNameType nameType, bool callChk)
		{
			MethodInfo meth = null;
			if (callChk)
			{
				meth = XmlILMethods.EndElemStackName;
			}
			else if (nameType != GenerateNameType.LiteralLocalName)
			{
				if (nameType == GenerateNameType.LiteralName)
				{
					meth = XmlILMethods.EndElemLitNameUn;
				}
			}
			else
			{
				meth = XmlILMethods.EndElemLocNameUn;
			}
			this.Call(meth);
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x0010ED80 File Offset: 0x0010CF80
		public void CallStartElementContent()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.StartContentUn);
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x0010ED94 File Offset: 0x0010CF94
		public void CallWriteStartAttribute(GenerateNameType nameType, bool callChk)
		{
			MethodInfo meth = null;
			if (callChk)
			{
				switch (nameType)
				{
				case GenerateNameType.LiteralLocalName:
					meth = XmlILMethods.StartAttrLocName;
					break;
				case GenerateNameType.LiteralName:
					meth = XmlILMethods.StartAttrLitName;
					break;
				case GenerateNameType.CopiedName:
					meth = XmlILMethods.StartAttrCopyName;
					break;
				case GenerateNameType.TagNameAndMappings:
					meth = XmlILMethods.StartAttrMapName;
					break;
				case GenerateNameType.TagNameAndNamespace:
					meth = XmlILMethods.StartAttrNmspName;
					break;
				case GenerateNameType.QName:
					meth = XmlILMethods.StartAttrQName;
					break;
				}
			}
			else if (nameType != GenerateNameType.LiteralLocalName)
			{
				if (nameType == GenerateNameType.LiteralName)
				{
					meth = XmlILMethods.StartAttrLitNameUn;
				}
			}
			else
			{
				meth = XmlILMethods.StartAttrLocNameUn;
			}
			this.Call(meth);
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x0010EE14 File Offset: 0x0010D014
		public void CallWriteEndAttribute(bool callChk)
		{
			this.LoadQueryOutput();
			if (callChk)
			{
				this.Call(XmlILMethods.EndAttr);
				return;
			}
			this.Call(XmlILMethods.EndAttrUn);
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x0010EE36 File Offset: 0x0010D036
		public void CallWriteNamespaceDecl(bool callChk)
		{
			if (callChk)
			{
				this.Call(XmlILMethods.NamespaceDecl);
				return;
			}
			this.Call(XmlILMethods.NamespaceDeclUn);
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x0010EE52 File Offset: 0x0010D052
		public void CallWriteString(bool disableOutputEscaping, bool callChk)
		{
			if (callChk)
			{
				if (disableOutputEscaping)
				{
					this.Call(XmlILMethods.NoEntText);
					return;
				}
				this.Call(XmlILMethods.Text);
				return;
			}
			else
			{
				if (disableOutputEscaping)
				{
					this.Call(XmlILMethods.NoEntTextUn);
					return;
				}
				this.Call(XmlILMethods.TextUn);
				return;
			}
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x0010EE8C File Offset: 0x0010D08C
		public void CallWriteStartPI()
		{
			this.Call(XmlILMethods.StartPI);
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x0010EE99 File Offset: 0x0010D099
		public void CallWriteEndPI()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.EndPI);
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x0010EEAC File Offset: 0x0010D0AC
		public void CallWriteStartComment()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.StartComment);
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x0010EEBF File Offset: 0x0010D0BF
		public void CallWriteEndComment()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.EndComment);
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x0010EED4 File Offset: 0x0010D0D4
		public void CallCacheCount(Type itemStorageType)
		{
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[itemStorageType];
			this.Call(xmlILStorageMethods.IListCount);
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x0010EEF9 File Offset: 0x0010D0F9
		public void CallCacheItem(Type itemStorageType)
		{
			this.Call(XmlILMethods.StorageMethods[itemStorageType].IListItem);
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x0010EF14 File Offset: 0x0010D114
		public void CallValueAs(Type clrType)
		{
			MethodInfo valueAs = XmlILMethods.StorageMethods[clrType].ValueAs;
			if (valueAs == null)
			{
				this.LoadType(clrType);
				this.Emit(OpCodes.Ldnull);
				this.Call(XmlILMethods.ValueAsAny);
				this.TreatAs(typeof(object), clrType);
				return;
			}
			this.Call(valueAs);
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x0010EF74 File Offset: 0x0010D174
		public void AddSortKey(XmlQueryType keyType)
		{
			MethodInfo meth = null;
			if (keyType == null)
			{
				meth = XmlILMethods.SortKeyEmpty;
			}
			else
			{
				XmlTypeCode typeCode = keyType.TypeCode;
				if (typeCode <= XmlTypeCode.DateTime)
				{
					if (typeCode != XmlTypeCode.None)
					{
						switch (typeCode)
						{
						case XmlTypeCode.AnyAtomicType:
							return;
						case XmlTypeCode.String:
							meth = XmlILMethods.SortKeyString;
							break;
						case XmlTypeCode.Boolean:
							meth = XmlILMethods.SortKeyInt;
							break;
						case XmlTypeCode.Decimal:
							meth = XmlILMethods.SortKeyDecimal;
							break;
						case XmlTypeCode.Double:
							meth = XmlILMethods.SortKeyDouble;
							break;
						case XmlTypeCode.DateTime:
							meth = XmlILMethods.SortKeyDateTime;
							break;
						}
					}
					else
					{
						this.Emit(OpCodes.Pop);
						meth = XmlILMethods.SortKeyEmpty;
					}
				}
				else if (typeCode != XmlTypeCode.Integer)
				{
					if (typeCode == XmlTypeCode.Int)
					{
						meth = XmlILMethods.SortKeyInt;
					}
				}
				else
				{
					meth = XmlILMethods.SortKeyInteger;
				}
			}
			this.Call(meth);
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x0010F034 File Offset: 0x0010D234
		public void DebugStartScope()
		{
			this.ilgen.BeginScope();
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x0010F041 File Offset: 0x0010D241
		public void DebugEndScope()
		{
			this.ilgen.EndScope();
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x0010F04E File Offset: 0x0010D24E
		public void DebugSequencePoint(ISourceLineInfo sourceInfo)
		{
			this.Emit(OpCodes.Nop);
			this.MarkSequencePoint(sourceInfo);
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x0010F064 File Offset: 0x0010D264
		private string GetFileName(ISourceLineInfo sourceInfo)
		{
			string uri = sourceInfo.Uri;
			if (uri == this.lastUriString)
			{
				return this.lastFileName;
			}
			this.lastUriString = uri;
			this.lastFileName = SourceLineInfo.GetFileName(uri);
			return this.lastFileName;
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x0010F0A4 File Offset: 0x0010D2A4
		private void MarkSequencePoint(ISourceLineInfo sourceInfo)
		{
			if (sourceInfo.IsNoSource && this.lastSourceInfo != null && this.lastSourceInfo.IsNoSource)
			{
				return;
			}
			string fileName = this.GetFileName(sourceInfo);
			ISymbolDocumentWriter document = this.module.AddSourceDocument(fileName);
			this.ilgen.MarkSequencePoint(document, sourceInfo.Start.Line, sourceInfo.Start.Pos, sourceInfo.End.Line, sourceInfo.End.Pos);
			this.lastSourceInfo = sourceInfo;
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x0010F12F File Offset: 0x0010D32F
		public Label DefineLabel()
		{
			return this.ilgen.DefineLabel();
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x0010F13C File Offset: 0x0010D33C
		public void MarkLabel(Label lbl)
		{
			if (this.lastSourceInfo != null && !this.lastSourceInfo.IsNoSource)
			{
				this.DebugSequencePoint(SourceLineInfo.NoSource);
			}
			this.ilgen.MarkLabel(lbl);
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x0010F16A File Offset: 0x0010D36A
		public void Emit(OpCode opcode)
		{
			this.ilgen.Emit(opcode);
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x0010F178 File Offset: 0x0010D378
		public void Emit(OpCode opcode, byte byteVal)
		{
			this.ilgen.Emit(opcode, byteVal);
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x0010F187 File Offset: 0x0010D387
		public void Emit(OpCode opcode, ConstructorInfo constrInfo)
		{
			this.ilgen.Emit(opcode, constrInfo);
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x0010F196 File Offset: 0x0010D396
		public void Emit(OpCode opcode, double dblVal)
		{
			this.ilgen.Emit(opcode, dblVal);
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x0010F1A5 File Offset: 0x0010D3A5
		public void Emit(OpCode opcode, float fltVal)
		{
			this.ilgen.Emit(opcode, fltVal);
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x0010F1B4 File Offset: 0x0010D3B4
		public void Emit(OpCode opcode, FieldInfo fldInfo)
		{
			this.ilgen.Emit(opcode, fldInfo);
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x0010F1C3 File Offset: 0x0010D3C3
		public void Emit(OpCode opcode, short shrtVal)
		{
			this.ilgen.Emit(opcode, shrtVal);
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x0010F1D2 File Offset: 0x0010D3D2
		public void Emit(OpCode opcode, int intVal)
		{
			this.ilgen.Emit(opcode, intVal);
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x0010F1E1 File Offset: 0x0010D3E1
		public void Emit(OpCode opcode, long longVal)
		{
			this.ilgen.Emit(opcode, longVal);
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x0010F1F0 File Offset: 0x0010D3F0
		public void Emit(OpCode opcode, Label lblVal)
		{
			this.ilgen.Emit(opcode, lblVal);
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x0010F1FF File Offset: 0x0010D3FF
		public void Emit(OpCode opcode, Label[] arrLabels)
		{
			this.ilgen.Emit(opcode, arrLabels);
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x0010F20E File Offset: 0x0010D40E
		public void Emit(OpCode opcode, LocalBuilder locBldr)
		{
			this.ilgen.Emit(opcode, locBldr);
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x0010F21D File Offset: 0x0010D41D
		public void Emit(OpCode opcode, MethodInfo methInfo)
		{
			this.ilgen.Emit(opcode, methInfo);
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x0010F22C File Offset: 0x0010D42C
		public void Emit(OpCode opcode, sbyte sbyteVal)
		{
			this.ilgen.Emit(opcode, sbyteVal);
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x0010F23B File Offset: 0x0010D43B
		public void Emit(OpCode opcode, string strVal)
		{
			this.ilgen.Emit(opcode, strVal);
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x0010F24A File Offset: 0x0010D44A
		public void Emit(OpCode opcode, Type typVal)
		{
			this.ilgen.Emit(opcode, typVal);
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x0010F25C File Offset: 0x0010D45C
		public void EmitUnconditionalBranch(OpCode opcode, Label lblTarget)
		{
			if (!opcode.Equals(OpCodes.Br) && !opcode.Equals(OpCodes.Br_S))
			{
				this.Emit(OpCodes.Ldc_I4_1);
			}
			this.ilgen.Emit(opcode, lblTarget);
			if (this.lastSourceInfo != null && (opcode.Equals(OpCodes.Br) || opcode.Equals(OpCodes.Br_S)))
			{
				this.MarkSequencePoint(SourceLineInfo.NoSource);
			}
		}

		// Token: 0x04002485 RID: 9349
		private MethodBase methInfo;

		// Token: 0x04002486 RID: 9350
		private ILGenerator ilgen;

		// Token: 0x04002487 RID: 9351
		private LocalBuilder locXOut;

		// Token: 0x04002488 RID: 9352
		private XmlILModule module;

		// Token: 0x04002489 RID: 9353
		private bool isDebug;

		// Token: 0x0400248A RID: 9354
		private bool initWriters;

		// Token: 0x0400248B RID: 9355
		private StaticDataManager staticData;

		// Token: 0x0400248C RID: 9356
		private ISourceLineInfo lastSourceInfo;

		// Token: 0x0400248D RID: 9357
		private MethodInfo methSyncToNav;

		// Token: 0x0400248E RID: 9358
		private string lastUriString;

		// Token: 0x0400248F RID: 9359
		private string lastFileName;
	}
}

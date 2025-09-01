using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000E7 RID: 231
	public sealed class ILGenerator
	{
		// Token: 0x06000A65 RID: 2661 RVA: 0x0002413C File Offset: 0x0002233C
		internal ILGenerator(ModuleBuilder moduleBuilder, int initialCapacity)
		{
			this.code = new ByteBuffer(initialCapacity);
			this.moduleBuilder = moduleBuilder;
			this.locals = SignatureHelper.GetLocalVarSigHelper(moduleBuilder);
			if (moduleBuilder.symbolWriter != null)
			{
				this.scope = new ILGenerator.Scope(null);
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000241C4 File Offset: 0x000223C4
		public void __DisableExceptionBlockAssistance()
		{
			this.exceptionBlockAssistanceMode = 1;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000241CD File Offset: 0x000223CD
		public void __CleverExceptionBlockAssistance()
		{
			this.exceptionBlockAssistanceMode = 2;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x000241D6 File Offset: 0x000223D6
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x000241DE File Offset: 0x000223DE
		public int __MaxStackSize
		{
			get
			{
				return (int)this.maxStack;
			}
			set
			{
				this.maxStack = (ushort)value;
				this.fatHeader = true;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x000241EF File Offset: 0x000223EF
		public int __StackHeight
		{
			get
			{
				return this.stackHeight;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x000241F7 File Offset: 0x000223F7
		public int ILOffset
		{
			get
			{
				return this.code.Position;
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00024204 File Offset: 0x00022404
		public void BeginCatchBlock(Type exceptionType)
		{
			if (!(exceptionType == null))
			{
				ILGenerator.ExceptionBlock exceptionBlock = this.BeginCatchOrFilterBlock();
				exceptionBlock.kind = ExceptionHandlingClauseOptions.Clause;
				exceptionBlock.filterOffsetOrExceptionTypeToken = this.moduleBuilder.GetTypeTokenForMemberRef(exceptionType);
				exceptionBlock.handlerOffset = this.code.Position;
				return;
			}
			ILGenerator.ExceptionBlock exceptionBlock2 = this.exceptionStack.Peek();
			if (exceptionBlock2.kind != ExceptionHandlingClauseOptions.Filter || exceptionBlock2.handlerOffset != 0)
			{
				throw new ArgumentNullException("exceptionType");
			}
			if (this.exceptionBlockAssistanceMode == 0 || (this.exceptionBlockAssistanceMode == 2 && this.stackHeight != -1))
			{
				this.Emit(OpCodes.Endfilter);
			}
			this.stackHeight = 0;
			this.UpdateStack(1);
			exceptionBlock2.handlerOffset = this.code.Position;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000242B8 File Offset: 0x000224B8
		private ILGenerator.ExceptionBlock BeginCatchOrFilterBlock()
		{
			ILGenerator.ExceptionBlock exceptionBlock = this.exceptionStack.Peek();
			if (this.exceptionBlockAssistanceMode == 0 || (this.exceptionBlockAssistanceMode == 2 && this.stackHeight != -1))
			{
				this.Emit(OpCodes.Leave, exceptionBlock.labelEnd);
			}
			this.stackHeight = 0;
			this.UpdateStack(1);
			if (exceptionBlock.tryLength == 0)
			{
				exceptionBlock.tryLength = this.code.Position - exceptionBlock.tryOffset;
			}
			else
			{
				exceptionBlock.handlerLength = this.code.Position - exceptionBlock.handlerOffset;
				this.exceptionStack.Pop();
				exceptionBlock = new ILGenerator.ExceptionBlock(this.exceptions.Count)
				{
					labelEnd = exceptionBlock.labelEnd,
					tryOffset = exceptionBlock.tryOffset,
					tryLength = exceptionBlock.tryLength
				};
				this.exceptions.Add(exceptionBlock);
				this.exceptionStack.Push(exceptionBlock);
			}
			return exceptionBlock;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000243A0 File Offset: 0x000225A0
		public Label BeginExceptionBlock()
		{
			ILGenerator.ExceptionBlock exceptionBlock = new ILGenerator.ExceptionBlock(this.exceptions.Count);
			exceptionBlock.labelEnd = this.DefineLabel();
			exceptionBlock.tryOffset = this.code.Position;
			this.exceptionStack.Push(exceptionBlock);
			this.exceptions.Add(exceptionBlock);
			this.stackHeight = 0;
			return exceptionBlock.labelEnd;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00024400 File Offset: 0x00022600
		public void BeginExceptFilterBlock()
		{
			ILGenerator.ExceptionBlock exceptionBlock = this.BeginCatchOrFilterBlock();
			exceptionBlock.kind = ExceptionHandlingClauseOptions.Filter;
			exceptionBlock.filterOffsetOrExceptionTypeToken = this.code.Position;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002441F File Offset: 0x0002261F
		public void BeginFaultBlock()
		{
			this.BeginFinallyFaultBlock(ExceptionHandlingClauseOptions.Fault);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00024428 File Offset: 0x00022628
		public void BeginFinallyBlock()
		{
			this.BeginFinallyFaultBlock(ExceptionHandlingClauseOptions.Finally);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00024434 File Offset: 0x00022634
		private void BeginFinallyFaultBlock(ExceptionHandlingClauseOptions kind)
		{
			ILGenerator.ExceptionBlock exceptionBlock = this.exceptionStack.Peek();
			if (this.exceptionBlockAssistanceMode == 0 || (this.exceptionBlockAssistanceMode == 2 && this.stackHeight != -1))
			{
				this.Emit(OpCodes.Leave, exceptionBlock.labelEnd);
			}
			if (exceptionBlock.handlerOffset == 0)
			{
				exceptionBlock.tryLength = this.code.Position - exceptionBlock.tryOffset;
			}
			else
			{
				exceptionBlock.handlerLength = this.code.Position - exceptionBlock.handlerOffset;
				Label label;
				if (this.exceptionBlockAssistanceMode != 0)
				{
					label = exceptionBlock.labelEnd;
				}
				else
				{
					this.MarkLabel(exceptionBlock.labelEnd);
					label = this.DefineLabel();
					this.Emit(OpCodes.Leave, label);
				}
				this.exceptionStack.Pop();
				exceptionBlock = new ILGenerator.ExceptionBlock(this.exceptions.Count)
				{
					labelEnd = label,
					tryOffset = exceptionBlock.tryOffset,
					tryLength = this.code.Position - exceptionBlock.tryOffset
				};
				this.exceptions.Add(exceptionBlock);
				this.exceptionStack.Push(exceptionBlock);
			}
			exceptionBlock.handlerOffset = this.code.Position;
			exceptionBlock.kind = kind;
			this.stackHeight = 0;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00024564 File Offset: 0x00022764
		public void EndExceptionBlock()
		{
			ILGenerator.ExceptionBlock exceptionBlock = this.exceptionStack.Pop();
			if (this.exceptionBlockAssistanceMode == 0 || (this.exceptionBlockAssistanceMode == 2 && this.stackHeight != -1))
			{
				if (exceptionBlock.kind != ExceptionHandlingClauseOptions.Finally && exceptionBlock.kind != ExceptionHandlingClauseOptions.Fault)
				{
					this.Emit(OpCodes.Leave, exceptionBlock.labelEnd);
				}
				else
				{
					this.Emit(OpCodes.Endfinally);
				}
			}
			this.MarkLabel(exceptionBlock.labelEnd);
			exceptionBlock.handlerLength = this.code.Position - exceptionBlock.handlerOffset;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000245EC File Offset: 0x000227EC
		public void BeginScope()
		{
			ILGenerator.Scope item = new ILGenerator.Scope(this.scope);
			this.scope.children.Add(item);
			this.scope = item;
			this.scope.startOffset = this.code.Position;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0000AF70 File Offset: 0x00009170
		public void UsingNamespace(string usingNamespace)
		{
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00024633 File Offset: 0x00022833
		public LocalBuilder DeclareLocal(Type localType)
		{
			return this.DeclareLocal(localType, false);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00024640 File Offset: 0x00022840
		public LocalBuilder DeclareLocal(Type localType, bool pinned)
		{
			int num = this.localsCount;
			this.localsCount = num + 1;
			LocalBuilder localBuilder = new LocalBuilder(localType, num, pinned);
			this.locals.AddArgument(localType, pinned);
			if (this.scope != null)
			{
				this.scope.locals.Add(localBuilder);
			}
			return localBuilder;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00024690 File Offset: 0x00022890
		public LocalBuilder __DeclareLocal(Type localType, bool pinned, CustomModifiers customModifiers)
		{
			int num = this.localsCount;
			this.localsCount = num + 1;
			LocalBuilder localBuilder = new LocalBuilder(localType, num, pinned, customModifiers);
			this.locals.__AddArgument(localType, pinned, customModifiers);
			if (this.scope != null)
			{
				this.scope.locals.Add(localBuilder);
			}
			return localBuilder;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000246DF File Offset: 0x000228DF
		public Label DefineLabel()
		{
			Label result = new Label(this.labels.Count);
			this.labels.Add(-1);
			this.labelStackHeight.Add(-1);
			return result;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002470C File Offset: 0x0002290C
		public void Emit(OpCode opc)
		{
			if (opc.Value < 0)
			{
				this.code.Write((byte)(opc.Value >> 8));
			}
			this.code.Write((byte)opc.Value);
			FlowControl flowControl = opc.FlowControl;
			if (flowControl <= FlowControl.Break)
			{
				if (flowControl != FlowControl.Branch && flowControl != FlowControl.Break)
				{
					goto IL_57;
				}
			}
			else if (flowControl != FlowControl.Return && flowControl != FlowControl.Throw)
			{
				goto IL_57;
			}
			this.stackHeight = -1;
			return;
			IL_57:
			this.UpdateStack(opc.StackDiff);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002477D File Offset: 0x0002297D
		private void UpdateStack(int stackdiff)
		{
			if (this.stackHeight == -1)
			{
				this.stackHeight = 0;
			}
			this.stackHeight += stackdiff;
			this.maxStack = Math.Max(this.maxStack, (ushort)this.stackHeight);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x000247B5 File Offset: 0x000229B5
		public void Emit(OpCode opc, byte arg)
		{
			this.Emit(opc);
			this.code.Write(arg);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000247CA File Offset: 0x000229CA
		public void Emit(OpCode opc, double arg)
		{
			this.Emit(opc);
			this.code.Write(arg);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x000247E0 File Offset: 0x000229E0
		public void Emit(OpCode opc, FieldInfo field)
		{
			this.Emit(opc);
			this.WriteToken(this.moduleBuilder.GetFieldToken(field).Token);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002480E File Offset: 0x00022A0E
		public void Emit(OpCode opc, short arg)
		{
			this.Emit(opc);
			this.code.Write(arg);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00024823 File Offset: 0x00022A23
		public void Emit(OpCode opc, int arg)
		{
			this.Emit(opc);
			this.code.Write(arg);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00024838 File Offset: 0x00022A38
		public void Emit(OpCode opc, long arg)
		{
			this.Emit(opc);
			this.code.Write(arg);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00024850 File Offset: 0x00022A50
		public void Emit(OpCode opc, Label label)
		{
			int num = this.stackHeight;
			this.Emit(opc);
			if (opc == OpCodes.Leave || opc == OpCodes.Leave_S)
			{
				num = 0;
			}
			else if (opc.FlowControl != FlowControl.Branch)
			{
				num = this.stackHeight;
			}
			if (this.labels[label.Index] != -1)
			{
				if (this.labelStackHeight[label.Index] != num && (this.labelStackHeight[label.Index] != 0 || num != -1))
				{
					throw new NotSupportedException("'Backward branch constraints' violated");
				}
				if (opc.OperandType == OperandType.ShortInlineBrTarget)
				{
					this.WriteByteBranchOffset(this.labels[label.Index] - (this.code.Position + 1));
					return;
				}
				this.code.Write(this.labels[label.Index] - (this.code.Position + 4));
				return;
			}
			else
			{
				this.labelStackHeight[label.Index] = num;
				ILGenerator.LabelFixup item = default(ILGenerator.LabelFixup);
				item.label = label.Index;
				item.offset = this.code.Position;
				this.labelFixups.Add(item);
				if (opc.OperandType == OperandType.ShortInlineBrTarget)
				{
					this.code.Write(1);
					return;
				}
				this.code.Write(4);
				return;
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000249B4 File Offset: 0x00022BB4
		private void WriteByteBranchOffset(int offset)
		{
			if (offset < -128 || offset > 127)
			{
				throw new NotSupportedException(string.Concat(new object[]
				{
					"Branch offset of ",
					offset,
					" does not fit in one-byte branch target at position ",
					this.code.Position
				}));
			}
			this.code.Write((byte)offset);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00024A18 File Offset: 0x00022C18
		public void Emit(OpCode opc, Label[] labels)
		{
			this.Emit(opc);
			ILGenerator.LabelFixup item = default(ILGenerator.LabelFixup);
			item.label = -1;
			item.offset = this.code.Position;
			this.labelFixups.Add(item);
			this.code.Write(labels.Length);
			foreach (Label label in labels)
			{
				this.code.Write(label.Index);
				if (this.labels[label.Index] != -1)
				{
					if (this.labelStackHeight[label.Index] != this.stackHeight)
					{
						throw new NotSupportedException();
					}
				}
				else
				{
					this.labelStackHeight[label.Index] = this.stackHeight;
				}
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00024AE0 File Offset: 0x00022CE0
		public void Emit(OpCode opc, LocalBuilder local)
		{
			if ((opc == OpCodes.Ldloc || opc == OpCodes.Ldloca || opc == OpCodes.Stloc) && local.LocalIndex < 256)
			{
				if (opc == OpCodes.Ldloc)
				{
					switch (local.LocalIndex)
					{
					case 0:
						this.Emit(OpCodes.Ldloc_0);
						return;
					case 1:
						this.Emit(OpCodes.Ldloc_1);
						return;
					case 2:
						this.Emit(OpCodes.Ldloc_2);
						return;
					case 3:
						this.Emit(OpCodes.Ldloc_3);
						return;
					default:
						this.Emit(OpCodes.Ldloc_S);
						this.code.Write((byte)local.LocalIndex);
						return;
					}
				}
				else
				{
					if (opc == OpCodes.Ldloca)
					{
						this.Emit(OpCodes.Ldloca_S);
						this.code.Write((byte)local.LocalIndex);
						return;
					}
					if (opc == OpCodes.Stloc)
					{
						switch (local.LocalIndex)
						{
						case 0:
							this.Emit(OpCodes.Stloc_0);
							return;
						case 1:
							this.Emit(OpCodes.Stloc_1);
							return;
						case 2:
							this.Emit(OpCodes.Stloc_2);
							return;
						case 3:
							this.Emit(OpCodes.Stloc_3);
							return;
						default:
							this.Emit(OpCodes.Stloc_S);
							this.code.Write((byte)local.LocalIndex);
							return;
						}
					}
				}
			}
			else
			{
				this.Emit(opc);
				OperandType operandType = opc.OperandType;
				if (operandType == OperandType.InlineVar)
				{
					this.code.Write((ushort)local.LocalIndex);
					return;
				}
				if (operandType != OperandType.ShortInlineVar)
				{
					return;
				}
				this.code.Write((byte)local.LocalIndex);
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00024C88 File Offset: 0x00022E88
		private void WriteToken(int token)
		{
			if (ModuleBuilder.IsPseudoToken(token))
			{
				this.tokenFixups.Add(this.code.Position);
			}
			this.code.Write(token);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00024CB4 File Offset: 0x00022EB4
		private void UpdateStack(OpCode opc, bool hasthis, Type returnType, int parameterCount)
		{
			if (opc == OpCodes.Jmp)
			{
				this.stackHeight = -1;
				return;
			}
			if (opc.FlowControl == FlowControl.Call)
			{
				int num = 0;
				if ((hasthis && opc != OpCodes.Newobj) || opc == OpCodes.Calli)
				{
					num--;
				}
				num -= parameterCount;
				if (returnType != this.moduleBuilder.universe.System_Void)
				{
					num++;
				}
				this.UpdateStack(num);
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00024D2C File Offset: 0x00022F2C
		public void Emit(OpCode opc, MethodInfo method)
		{
			this.UpdateStack(opc, method.HasThis, method.ReturnType, method.ParameterCount);
			this.Emit(opc);
			this.WriteToken(this.moduleBuilder.GetMethodTokenForIL(method).Token);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00024D73 File Offset: 0x00022F73
		public void Emit(OpCode opc, ConstructorInfo constructor)
		{
			this.Emit(opc, constructor.GetMethodInfo());
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00024D82 File Offset: 0x00022F82
		public void Emit(OpCode opc, sbyte arg)
		{
			this.Emit(opc);
			this.code.Write(arg);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00024D97 File Offset: 0x00022F97
		public void Emit(OpCode opc, float arg)
		{
			this.Emit(opc);
			this.code.Write(arg);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00024DAC File Offset: 0x00022FAC
		public void Emit(OpCode opc, string str)
		{
			this.Emit(opc);
			this.code.Write(this.moduleBuilder.GetStringConstant(str).Token);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00024DE0 File Offset: 0x00022FE0
		public void Emit(OpCode opc, Type type)
		{
			this.Emit(opc);
			if (opc == OpCodes.Ldtoken)
			{
				this.code.Write(this.moduleBuilder.GetTypeToken(type).Token);
				return;
			}
			this.code.Write(this.moduleBuilder.GetTypeTokenForMemberRef(type));
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00024E38 File Offset: 0x00023038
		public void Emit(OpCode opcode, SignatureHelper signature)
		{
			this.Emit(opcode);
			this.UpdateStack(opcode, signature.HasThis, signature.ReturnType, signature.ParameterCount);
			this.code.Write(this.moduleBuilder.GetSignatureToken(signature).Token);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00024E84 File Offset: 0x00023084
		public void EmitCall(OpCode opc, MethodInfo method, Type[] optionalParameterTypes)
		{
			this.__EmitCall(opc, method, optionalParameterTypes, null);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00024E90 File Offset: 0x00023090
		public void __EmitCall(OpCode opc, MethodInfo method, Type[] optionalParameterTypes, CustomModifiers[] customModifiers)
		{
			if (optionalParameterTypes == null || optionalParameterTypes.Length == 0)
			{
				this.Emit(opc, method);
				return;
			}
			this.Emit(opc);
			this.UpdateStack(opc, method.HasThis, method.ReturnType, method.ParameterCount + optionalParameterTypes.Length);
			this.code.Write(this.moduleBuilder.__GetMethodToken(method, optionalParameterTypes, customModifiers).Token);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00024EF3 File Offset: 0x000230F3
		public void __EmitCall(OpCode opc, ConstructorInfo constructor, Type[] optionalParameterTypes)
		{
			this.EmitCall(opc, constructor.GetMethodInfo(), optionalParameterTypes);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00024F03 File Offset: 0x00023103
		public void __EmitCall(OpCode opc, ConstructorInfo constructor, Type[] optionalParameterTypes, CustomModifiers[] customModifiers)
		{
			this.__EmitCall(opc, constructor.GetMethodInfo(), optionalParameterTypes, customModifiers);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00024F18 File Offset: 0x00023118
		public void EmitCalli(OpCode opc, CallingConvention callingConvention, Type returnType, Type[] parameterTypes)
		{
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(this.moduleBuilder, callingConvention, returnType);
			methodSigHelper.AddArguments(parameterTypes, null, null);
			this.Emit(opc, methodSigHelper);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00024F48 File Offset: 0x00023148
		public void EmitCalli(OpCode opc, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(this.moduleBuilder, callingConvention, returnType);
			methodSigHelper.AddArguments(parameterTypes, null, null);
			if (optionalParameterTypes != null && optionalParameterTypes.Length != 0)
			{
				methodSigHelper.AddSentinel();
				methodSigHelper.AddArguments(optionalParameterTypes, null, null);
			}
			this.Emit(opc, methodSigHelper);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00024F90 File Offset: 0x00023190
		public void __EmitCalli(OpCode opc, __StandAloneMethodSig sig)
		{
			this.Emit(opc);
			if (sig.IsUnmanaged)
			{
				this.UpdateStack(opc, false, sig.ReturnType, sig.ParameterCount);
			}
			else
			{
				CallingConventions callingConvention = sig.CallingConvention;
				this.UpdateStack(opc, ((callingConvention & CallingConventions.HasThis) | CallingConventions.ExplicitThis) == CallingConventions.HasThis, sig.ReturnType, sig.ParameterCount);
			}
			ByteBuffer bb = new ByteBuffer(16);
			Signature.WriteStandAloneMethodSig(this.moduleBuilder, bb, sig);
			this.code.Write(285212672 | this.moduleBuilder.StandAloneSig.FindOrAddRecord(this.moduleBuilder.Blobs.Add(bb)));
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00025030 File Offset: 0x00023230
		public void EmitWriteLine(string text)
		{
			Universe universe = this.moduleBuilder.universe;
			this.Emit(OpCodes.Ldstr, text);
			this.Emit(OpCodes.Call, universe.Import(typeof(Console)).GetMethod("WriteLine", new Type[]
			{
				universe.System_String
			}));
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002508C File Offset: 0x0002328C
		public void EmitWriteLine(FieldInfo field)
		{
			Universe universe = this.moduleBuilder.universe;
			this.Emit(OpCodes.Call, universe.Import(typeof(Console)).GetMethod("get_Out"));
			if (field.IsStatic)
			{
				this.Emit(OpCodes.Ldsfld, field);
			}
			else
			{
				this.Emit(OpCodes.Ldarg_0);
				this.Emit(OpCodes.Ldfld, field);
			}
			this.Emit(OpCodes.Callvirt, universe.Import(typeof(TextWriter)).GetMethod("WriteLine", new Type[]
			{
				field.FieldType
			}));
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002512C File Offset: 0x0002332C
		public void EmitWriteLine(LocalBuilder local)
		{
			Universe universe = this.moduleBuilder.universe;
			this.Emit(OpCodes.Call, universe.Import(typeof(Console)).GetMethod("get_Out"));
			this.Emit(OpCodes.Ldloc, local);
			this.Emit(OpCodes.Callvirt, universe.Import(typeof(TextWriter)).GetMethod("WriteLine", new Type[]
			{
				local.LocalType
			}));
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000251AA File Offset: 0x000233AA
		public void EndScope()
		{
			this.scope.endOffset = this.code.Position;
			this.scope = this.scope.parent;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x000251D4 File Offset: 0x000233D4
		public void MarkLabel(Label loc)
		{
			this.labels[loc.Index] = this.code.Position;
			if (this.labelStackHeight[loc.Index] != -1)
			{
				this.stackHeight = this.labelStackHeight[loc.Index];
				return;
			}
			if (this.stackHeight == -1)
			{
				this.labelStackHeight[loc.Index] = 0;
				return;
			}
			this.labelStackHeight[loc.Index] = this.stackHeight;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00025261 File Offset: 0x00023461
		public void ThrowException(Type excType)
		{
			this.Emit(OpCodes.Newobj, excType.GetConstructor(Type.EmptyTypes));
			this.Emit(OpCodes.Throw);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00025284 File Offset: 0x00023484
		internal int WriteBody(bool initLocals)
		{
			if (this.moduleBuilder.symbolWriter != null)
			{
				this.scope.endOffset = this.code.Position;
			}
			this.ResolveBranches();
			ByteBuffer methodBodies = this.moduleBuilder.methodBodies;
			int localVarSigTok = 0;
			int result;
			if (this.localsCount == 0 && this.exceptions.Count == 0 && this.maxStack <= 8 && this.code.Length < 64 && !this.fatHeader)
			{
				result = this.WriteTinyHeaderAndCode(methodBodies);
			}
			else
			{
				if (this.localsCount != 0)
				{
					localVarSigTok = this.moduleBuilder.GetSignatureToken(this.locals).Token;
				}
				result = this.WriteFatHeaderAndCode(methodBodies, localVarSigTok, initLocals);
			}
			return result;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00025334 File Offset: 0x00023534
		private void ResolveBranches()
		{
			foreach (ILGenerator.LabelFixup labelFixup in this.labelFixups)
			{
				if (labelFixup.label == -1)
				{
					this.code.Position = labelFixup.offset;
					int int32AtCurrentPosition = this.code.GetInt32AtCurrentPosition();
					int num = labelFixup.offset + 4 + 4 * int32AtCurrentPosition;
					this.code.Position += 4;
					for (int i = 0; i < int32AtCurrentPosition; i++)
					{
						int int32AtCurrentPosition2 = this.code.GetInt32AtCurrentPosition();
						this.code.Write(this.labels[int32AtCurrentPosition2] - num);
					}
				}
				else
				{
					this.code.Position = labelFixup.offset;
					byte byteAtCurrentPosition = this.code.GetByteAtCurrentPosition();
					int num2 = this.labels[labelFixup.label] - (this.code.Position + (int)byteAtCurrentPosition);
					if (byteAtCurrentPosition == 1)
					{
						this.WriteByteBranchOffset(num2);
					}
					else
					{
						this.code.Write(num2);
					}
				}
			}
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00025464 File Offset: 0x00023664
		internal static void WriteTinyHeader(ByteBuffer bb, int length)
		{
			bb.Write((byte)(2 | length << 2));
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00025474 File Offset: 0x00023674
		private int WriteTinyHeaderAndCode(ByteBuffer bb)
		{
			int position = bb.Position;
			ILGenerator.WriteTinyHeader(bb, this.code.Length);
			ILGenerator.AddTokenFixups(bb.Position, this.moduleBuilder.tokenFixupOffsets, this.tokenFixups);
			bb.Write(this.code);
			return position;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000254C0 File Offset: 0x000236C0
		internal static void WriteFatHeader(ByteBuffer bb, bool initLocals, bool exceptions, ushort maxStack, int codeLength, int localVarSigTok)
		{
			short num = 12291;
			if (initLocals)
			{
				num |= 16;
			}
			if (exceptions)
			{
				num |= 8;
			}
			bb.Write(num);
			bb.Write(maxStack);
			bb.Write(codeLength);
			bb.Write(localVarSigTok);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00025504 File Offset: 0x00023704
		private int WriteFatHeaderAndCode(ByteBuffer bb, int localVarSigTok, bool initLocals)
		{
			bb.Align(4);
			int position = bb.Position;
			ILGenerator.WriteFatHeader(bb, initLocals, this.exceptions.Count > 0, this.maxStack, this.code.Length, localVarSigTok);
			ILGenerator.AddTokenFixups(bb.Position, this.moduleBuilder.tokenFixupOffsets, this.tokenFixups);
			bb.Write(this.code);
			if (this.exceptions.Count > 0)
			{
				this.exceptions.Sort(this.exceptions[0]);
				ILGenerator.WriteExceptionHandlers(bb, this.exceptions);
			}
			return position;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000255A0 File Offset: 0x000237A0
		internal static void WriteExceptionHandlers(ByteBuffer bb, List<ILGenerator.ExceptionBlock> exceptions)
		{
			bb.Align(4);
			bool flag = false;
			if (exceptions.Count * 12 + 4 > 255)
			{
				flag = true;
			}
			else
			{
				foreach (ILGenerator.ExceptionBlock exceptionBlock in exceptions)
				{
					if (exceptionBlock.tryOffset > 65535 || exceptionBlock.tryLength > 255 || exceptionBlock.handlerOffset > 65535 || exceptionBlock.handlerLength > 255)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				bb.Write(65);
				int num = exceptions.Count * 24 + 4;
				bb.Write((byte)num);
				bb.Write((short)(num >> 8));
				using (List<ILGenerator.ExceptionBlock>.Enumerator enumerator = exceptions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ILGenerator.ExceptionBlock exceptionBlock2 = enumerator.Current;
						bb.Write((int)exceptionBlock2.kind);
						bb.Write(exceptionBlock2.tryOffset);
						bb.Write(exceptionBlock2.tryLength);
						bb.Write(exceptionBlock2.handlerOffset);
						bb.Write(exceptionBlock2.handlerLength);
						bb.Write(exceptionBlock2.filterOffsetOrExceptionTypeToken);
					}
					return;
				}
			}
			bb.Write(1);
			bb.Write((byte)(exceptions.Count * 12 + 4));
			bb.Write(0);
			foreach (ILGenerator.ExceptionBlock exceptionBlock3 in exceptions)
			{
				bb.Write((short)exceptionBlock3.kind);
				bb.Write((short)exceptionBlock3.tryOffset);
				bb.Write((byte)exceptionBlock3.tryLength);
				bb.Write((short)exceptionBlock3.handlerOffset);
				bb.Write((byte)exceptionBlock3.handlerLength);
				bb.Write(exceptionBlock3.filterOffsetOrExceptionTypeToken);
			}
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002579C File Offset: 0x0002399C
		internal static void AddTokenFixups(int codeOffset, List<int> tokenFixupOffsets, IEnumerable<int> tokenFixups)
		{
			foreach (int num in tokenFixups)
			{
				tokenFixupOffsets.Add(num + codeOffset);
			}
		}

		// Token: 0x0400049E RID: 1182
		private readonly ModuleBuilder moduleBuilder;

		// Token: 0x0400049F RID: 1183
		private readonly ByteBuffer code;

		// Token: 0x040004A0 RID: 1184
		private readonly SignatureHelper locals;

		// Token: 0x040004A1 RID: 1185
		private int localsCount;

		// Token: 0x040004A2 RID: 1186
		private readonly List<int> tokenFixups = new List<int>();

		// Token: 0x040004A3 RID: 1187
		private readonly List<int> labels = new List<int>();

		// Token: 0x040004A4 RID: 1188
		private readonly List<int> labelStackHeight = new List<int>();

		// Token: 0x040004A5 RID: 1189
		private readonly List<ILGenerator.LabelFixup> labelFixups = new List<ILGenerator.LabelFixup>();

		// Token: 0x040004A6 RID: 1190
		private readonly List<ILGenerator.ExceptionBlock> exceptions = new List<ILGenerator.ExceptionBlock>();

		// Token: 0x040004A7 RID: 1191
		private readonly Stack<ILGenerator.ExceptionBlock> exceptionStack = new Stack<ILGenerator.ExceptionBlock>();

		// Token: 0x040004A8 RID: 1192
		private ushort maxStack;

		// Token: 0x040004A9 RID: 1193
		private bool fatHeader;

		// Token: 0x040004AA RID: 1194
		private int stackHeight;

		// Token: 0x040004AB RID: 1195
		private ILGenerator.Scope scope;

		// Token: 0x040004AC RID: 1196
		private byte exceptionBlockAssistanceMode;

		// Token: 0x040004AD RID: 1197
		private const byte EBAM_COMPAT = 0;

		// Token: 0x040004AE RID: 1198
		private const byte EBAM_DISABLE = 1;

		// Token: 0x040004AF RID: 1199
		private const byte EBAM_CLEVER = 2;

		// Token: 0x02000368 RID: 872
		private struct LabelFixup
		{
			// Token: 0x04000F10 RID: 3856
			internal int label;

			// Token: 0x04000F11 RID: 3857
			internal int offset;
		}

		// Token: 0x02000369 RID: 873
		internal sealed class ExceptionBlock : IComparer<ILGenerator.ExceptionBlock>
		{
			// Token: 0x0600264E RID: 9806 RVA: 0x000B5F0D File Offset: 0x000B410D
			internal ExceptionBlock(int ordinal)
			{
				this.ordinal = ordinal;
			}

			// Token: 0x0600264F RID: 9807 RVA: 0x000B5F1C File Offset: 0x000B411C
			internal ExceptionBlock(ExceptionHandler h)
			{
				this.ordinal = -1;
				this.tryOffset = h.TryOffset;
				this.tryLength = h.TryLength;
				this.handlerOffset = h.HandlerOffset;
				this.handlerLength = h.HandlerLength;
				this.kind = h.Kind;
				this.filterOffsetOrExceptionTypeToken = ((this.kind == ExceptionHandlingClauseOptions.Filter) ? h.FilterOffset : h.ExceptionTypeToken);
			}

			// Token: 0x06002650 RID: 9808 RVA: 0x000B5F98 File Offset: 0x000B4198
			int IComparer<ILGenerator.ExceptionBlock>.Compare(ILGenerator.ExceptionBlock x, ILGenerator.ExceptionBlock y)
			{
				if (x == y)
				{
					return 0;
				}
				if (x.tryOffset == y.tryOffset && x.tryLength == y.tryLength)
				{
					if (x.ordinal >= y.ordinal)
					{
						return 1;
					}
					return -1;
				}
				else
				{
					if (x.tryOffset >= y.tryOffset && x.handlerOffset + x.handlerLength <= y.handlerOffset + y.handlerLength)
					{
						return -1;
					}
					if (y.tryOffset >= x.tryOffset && y.handlerOffset + y.handlerLength <= x.handlerOffset + x.handlerLength)
					{
						return 1;
					}
					if (x.ordinal >= y.ordinal)
					{
						return 1;
					}
					return -1;
				}
			}

			// Token: 0x04000F12 RID: 3858
			internal readonly int ordinal;

			// Token: 0x04000F13 RID: 3859
			internal Label labelEnd;

			// Token: 0x04000F14 RID: 3860
			internal int tryOffset;

			// Token: 0x04000F15 RID: 3861
			internal int tryLength;

			// Token: 0x04000F16 RID: 3862
			internal int handlerOffset;

			// Token: 0x04000F17 RID: 3863
			internal int handlerLength;

			// Token: 0x04000F18 RID: 3864
			internal int filterOffsetOrExceptionTypeToken;

			// Token: 0x04000F19 RID: 3865
			internal ExceptionHandlingClauseOptions kind;
		}

		// Token: 0x0200036A RID: 874
		private struct SequencePoint
		{
		}

		// Token: 0x0200036B RID: 875
		private sealed class Scope
		{
			// Token: 0x06002651 RID: 9809 RVA: 0x000B6042 File Offset: 0x000B4242
			internal Scope(ILGenerator.Scope parent)
			{
				this.parent = parent;
			}

			// Token: 0x04000F1A RID: 3866
			internal readonly ILGenerator.Scope parent;

			// Token: 0x04000F1B RID: 3867
			internal readonly List<ILGenerator.Scope> children = new List<ILGenerator.Scope>();

			// Token: 0x04000F1C RID: 3868
			internal readonly List<LocalBuilder> locals = new List<LocalBuilder>();

			// Token: 0x04000F1D RID: 3869
			internal int startOffset;

			// Token: 0x04000F1E RID: 3870
			internal int endOffset;
		}
	}
}

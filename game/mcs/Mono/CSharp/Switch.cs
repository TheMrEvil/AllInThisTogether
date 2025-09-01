using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x020002B9 RID: 697
	public class Switch : LoopStatement
	{
		// Token: 0x060021A7 RID: 8615 RVA: 0x000A45D4 File Offset: 0x000A27D4
		public Switch(Expression e, ExplicitBlock block, Location l) : base(block)
		{
			this.Expr = e;
			this.block = block;
			this.loc = l;
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x000A45F2 File Offset: 0x000A27F2
		// (set) Token: 0x060021A9 RID: 8617 RVA: 0x000A45FA File Offset: 0x000A27FA
		public SwitchLabel ActiveLabel
		{
			[CompilerGenerated]
			get
			{
				return this.<ActiveLabel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ActiveLabel>k__BackingField = value;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x000A4603 File Offset: 0x000A2803
		public ExplicitBlock Block
		{
			get
			{
				return this.block;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x000A460B File Offset: 0x000A280B
		public SwitchLabel DefaultLabel
		{
			get
			{
				return this.case_default;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x000A4613 File Offset: 0x000A2813
		public bool IsNullable
		{
			get
			{
				return this.unwrap != null;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x000A461E File Offset: 0x000A281E
		public bool IsPatternMatching
		{
			get
			{
				return this.new_expr == null && this.SwitchType != null;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x000A4633 File Offset: 0x000A2833
		public List<SwitchLabel> RegisteredLabels
		{
			get
			{
				return this.case_labels;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x000A463B File Offset: 0x000A283B
		public VariableReference ExpressionValue
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x000A4644 File Offset: 0x000A2844
		private static Expression SwitchGoverningType(ResolveContext rc, Expression expr, bool unwrapExpr)
		{
			switch (expr.Type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.FirstPrimitive:
			case BuiltinTypeSpec.Type.Byte:
			case BuiltinTypeSpec.Type.SByte:
			case BuiltinTypeSpec.Type.Char:
			case BuiltinTypeSpec.Type.Short:
			case BuiltinTypeSpec.Type.UShort:
			case BuiltinTypeSpec.Type.Int:
			case BuiltinTypeSpec.Type.UInt:
			case BuiltinTypeSpec.Type.Long:
			case BuiltinTypeSpec.Type.ULong:
			case BuiltinTypeSpec.Type.String:
				return expr;
			}
			if (expr.Type.IsEnum)
			{
				return expr;
			}
			Expression expression = null;
			foreach (TypeSpec typeSpec in rc.Module.PredefinedTypes.SwitchUserTypes)
			{
				if (!unwrapExpr && typeSpec.IsNullableType && expr.Type.IsNullableType)
				{
					break;
				}
				Convert.UserConversionRestriction userConversionRestriction = Convert.UserConversionRestriction.ImplicitOnly | Convert.UserConversionRestriction.ProbingOnly;
				if (unwrapExpr)
				{
					userConversionRestriction |= Convert.UserConversionRestriction.NullableSourceOnly;
				}
				Expression expression2 = Convert.UserDefinedConversion(rc, expr, typeSpec, userConversionRestriction, Location.Null);
				if (expression2 != null && expression2 is UserCast)
				{
					if (expression != null)
					{
						return null;
					}
					expression = expression2;
				}
			}
			return expression;
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000A4734 File Offset: 0x000A2934
		public static TypeSpec[] CreateSwitchUserTypes(ModuleContainer module, TypeSpec nullable)
		{
			BuiltinTypes builtinTypes = module.Compiler.BuiltinTypes;
			TypeSpec[] array = new BuiltinTypeSpec[]
			{
				builtinTypes.SByte,
				builtinTypes.Byte,
				builtinTypes.Short,
				builtinTypes.UShort,
				builtinTypes.Int,
				builtinTypes.UInt,
				builtinTypes.Long,
				builtinTypes.ULong,
				builtinTypes.Char,
				builtinTypes.String
			};
			if (nullable != null)
			{
				Array.Resize<TypeSpec>(ref array, array.Length + 9);
				for (int i = 0; i < 9; i++)
				{
					array[10 + i] = nullable.MakeGenericType(module, new TypeSpec[]
					{
						array[i]
					});
				}
			}
			return array;
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x000A47E8 File Offset: 0x000A29E8
		public void RegisterLabel(BlockContext rc, SwitchLabel sl)
		{
			this.case_labels.Add(sl);
			if (sl.IsDefault)
			{
				if (this.case_default != null)
				{
					sl.Error_AlreadyOccurs(rc, this.case_default);
					return;
				}
				this.case_default = sl;
				return;
			}
			else
			{
				if (sl.Converted == null)
				{
					return;
				}
				try
				{
					if (this.string_labels != null)
					{
						string text = sl.Converted.GetValue() as string;
						if (text == null)
						{
							this.case_null = sl;
						}
						else
						{
							this.string_labels.Add(text, sl);
						}
					}
					else if (sl.Converted.IsNull)
					{
						this.case_null = sl;
					}
					else
					{
						this.labels.Add(sl.Converted.GetValueAsLong(), sl);
					}
				}
				catch (ArgumentException)
				{
					if (this.string_labels != null)
					{
						sl.Error_AlreadyOccurs(rc, this.string_labels[(string)sl.Converted.GetValue()]);
					}
					else
					{
						sl.Error_AlreadyOccurs(rc, this.labels[sl.Converted.GetValueAsLong()]);
					}
				}
				return;
			}
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000A48F0 File Offset: 0x000A2AF0
		private void EmitTableSwitch(EmitContext ec, Expression val)
		{
			if (this.labels != null && this.labels.Count > 0)
			{
				List<Switch.LabelsRange> list;
				if (this.string_labels != null)
				{
					list = new List<Switch.LabelsRange>(1);
					list.Add(new Switch.LabelsRange(0L, (long)(this.labels.Count - 1), this.labels.Keys));
				}
				else
				{
					long[] array = new long[this.labels.Count];
					this.labels.Keys.CopyTo(array, 0);
					Array.Sort<long>(array);
					list = new List<Switch.LabelsRange>(array.Length);
					Switch.LabelsRange labelsRange = new Switch.LabelsRange(array[0]);
					list.Add(labelsRange);
					for (int i = 1; i < array.Length; i++)
					{
						long num = array[i];
						if (!labelsRange.AddValue(num))
						{
							labelsRange = new Switch.LabelsRange(num);
							list.Add(labelsRange);
						}
					}
					list.Sort();
				}
				Label label = this.defaultLabel;
				TypeSpec typeSpec = this.SwitchType.IsEnum ? EnumSpec.GetUnderlyingType(this.SwitchType) : this.SwitchType;
				int j = list.Count - 1;
				while (j >= 0)
				{
					Switch.LabelsRange labelsRange2 = list[j];
					label = ((j == 0) ? this.defaultLabel : ec.DefineLabel());
					if (labelsRange2.Range <= 2L)
					{
						using (List<long>.Enumerator enumerator = labelsRange2.label_values.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								long key = enumerator.Current;
								SwitchLabel switchLabel = this.labels[key];
								if (switchLabel != this.case_default && switchLabel != this.case_null)
								{
									if (switchLabel.Converted.IsZeroInteger)
									{
										val.EmitBranchable(ec, switchLabel.GetILLabel(ec), false);
									}
									else
									{
										val.Emit(ec);
										switchLabel.Converted.Emit(ec);
										ec.Emit(OpCodes.Beq, switchLabel.GetILLabel(ec));
									}
								}
							}
							goto IL_321;
						}
						goto IL_1D2;
					}
					goto IL_1D2;
					IL_321:
					if (j != 0)
					{
						ec.MarkLabel(label);
					}
					j--;
					continue;
					IL_1D2:
					if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Long || typeSpec.BuiltinType == BuiltinTypeSpec.Type.ULong)
					{
						val.Emit(ec);
						ec.EmitLong(labelsRange2.min);
						ec.Emit(OpCodes.Blt, label);
						val.Emit(ec);
						ec.EmitLong(labelsRange2.max);
						ec.Emit(OpCodes.Bgt, label);
						val.Emit(ec);
						if (labelsRange2.min != 0L)
						{
							ec.EmitLong(labelsRange2.min);
							ec.Emit(OpCodes.Sub);
						}
						ec.Emit(OpCodes.Conv_I4);
					}
					else
					{
						val.Emit(ec);
						int num2 = (int)labelsRange2.min;
						if (num2 > 0)
						{
							ec.EmitInt(num2);
							ec.Emit(OpCodes.Sub);
						}
						else if (num2 < 0)
						{
							ec.EmitInt(-num2);
							ec.Emit(OpCodes.Add);
						}
					}
					int num3 = 0;
					long range = labelsRange2.Range;
					Label[] array2 = new Label[range];
					int num4 = 0;
					while ((long)num4 < range)
					{
						long num5 = labelsRange2.label_values[num3];
						if (num5 == labelsRange2.min + (long)num4)
						{
							array2[num4] = this.labels[num5].GetILLabel(ec);
							num3++;
						}
						else
						{
							array2[num4] = label;
						}
						num4++;
					}
					ec.Emit(OpCodes.Switch, array2);
					goto IL_321;
				}
				if (list.Count > 0)
				{
					ec.Emit(OpCodes.Br, label);
				}
			}
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000A4C5C File Offset: 0x000A2E5C
		public SwitchLabel FindLabel(Constant value)
		{
			SwitchLabel switchLabel = null;
			if (this.string_labels != null)
			{
				string text = value.GetValue() as string;
				if (text == null)
				{
					if (this.case_null != null)
					{
						switchLabel = this.case_null;
					}
					else if (this.case_default != null)
					{
						switchLabel = this.case_default;
					}
				}
				else
				{
					this.string_labels.TryGetValue(text, out switchLabel);
				}
			}
			else if (value is NullLiteral)
			{
				switchLabel = this.case_null;
			}
			else
			{
				this.labels.TryGetValue(value.GetValueAsLong(), out switchLabel);
			}
			if (switchLabel == null || switchLabel.SectionStart)
			{
				return switchLabel;
			}
			int num = this.case_labels.IndexOf(switchLabel);
			SwitchLabel switchLabel2;
			for (;;)
			{
				switchLabel2 = this.case_labels[num];
				if (switchLabel2.SectionStart)
				{
					break;
				}
				num--;
			}
			return switchLabel2;
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000A4D10 File Offset: 0x000A2F10
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			this.Expr.FlowAnalysis(fc);
			DefiniteAssignmentBitSet switchInitialDefinitiveAssignment = fc.SwitchInitialDefinitiveAssignment;
			DefiniteAssignmentBitSet definiteAssignmentBitSet = fc.DefiniteAssignment;
			fc.SwitchInitialDefinitiveAssignment = definiteAssignmentBitSet;
			this.block.FlowAnalysis(fc);
			fc.SwitchInitialDefinitiveAssignment = switchInitialDefinitiveAssignment;
			if (this.end_reachable_das != null)
			{
				DefiniteAssignmentBitSet b = DefiniteAssignmentBitSet.And(this.end_reachable_das);
				definiteAssignmentBitSet |= b;
				this.end_reachable_das = null;
			}
			fc.DefiniteAssignment = definiteAssignmentBitSet;
			return this.case_default != null && !this.end_reachable;
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000A4D90 File Offset: 0x000A2F90
		public override bool Resolve(BlockContext ec)
		{
			this.Expr = this.Expr.Resolve(ec);
			if (this.Expr == null)
			{
				return false;
			}
			this.new_expr = Switch.SwitchGoverningType(ec, this.Expr, false);
			if (this.new_expr == null && this.Expr.Type.IsNullableType)
			{
				this.unwrap = Unwrap.Create(this.Expr, false);
				if (this.unwrap == null)
				{
					return false;
				}
				this.new_expr = Switch.SwitchGoverningType(ec, this.unwrap, true);
			}
			Expression expr;
			if (this.new_expr == null)
			{
				if (ec.Module.Compiler.Settings.Version != LanguageVersion.Experimental)
				{
					if (this.Expr.Type != InternalType.ErrorType)
					{
						ec.Report.Error(151, this.loc, "A switch expression of type `{0}' cannot be converted to an integral type, bool, char, string, enum or nullable type", this.Expr.Type.GetSignatureForError());
					}
					return false;
				}
				expr = this.Expr;
				this.SwitchType = this.Expr.Type;
			}
			else
			{
				expr = this.new_expr;
				this.SwitchType = this.new_expr.Type;
				if (this.SwitchType.IsNullableType)
				{
					this.new_expr = (this.unwrap = Unwrap.Create(this.new_expr, true));
					this.SwitchType = NullableInfo.GetUnderlyingType(this.SwitchType);
				}
				if (this.SwitchType.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive && ec.Module.Compiler.Settings.Version == LanguageVersion.ISO_1)
				{
					ec.Report.FeatureIsNotAvailable(ec.Module.Compiler, this.loc, "switch expression of boolean type");
					return false;
				}
				if (this.block.Statements.Count == 0)
				{
					return true;
				}
				if (this.SwitchType.BuiltinType == BuiltinTypeSpec.Type.String)
				{
					this.string_labels = new Dictionary<string, SwitchLabel>();
				}
				else
				{
					this.labels = new Dictionary<long, SwitchLabel>();
				}
			}
			Constant constant = expr as Constant;
			if (constant == null)
			{
				this.value = (expr as VariableReference);
				if (this.value == null && !this.HasOnlyDefaultSection())
				{
					Block currentBlock = ec.CurrentBlock;
					ec.CurrentBlock = this.Block;
					this.value = TemporaryVariableReference.Create(this.SwitchType, ec.CurrentBlock, this.loc);
					this.value.Resolve(ec);
					ec.CurrentBlock = currentBlock;
				}
			}
			this.case_labels = new List<SwitchLabel>();
			Switch @switch = ec.Switch;
			ec.Switch = this;
			LoopStatement enclosingLoopOrSwitch = ec.EnclosingLoopOrSwitch;
			ec.EnclosingLoopOrSwitch = this;
			bool flag = base.Statement.Resolve(ec);
			ec.EnclosingLoopOrSwitch = enclosingLoopOrSwitch;
			ec.Switch = @switch;
			if (this.goto_cases != null)
			{
				foreach (Tuple<GotoCase, Constant> tuple in this.goto_cases)
				{
					if (tuple.Item1 == null)
					{
						if (this.DefaultLabel == null)
						{
							Goto.Error_UnknownLabel(ec, "default", this.loc);
						}
					}
					else
					{
						SwitchLabel switchLabel = this.FindLabel(tuple.Item2);
						if (switchLabel == null)
						{
							Goto.Error_UnknownLabel(ec, "case " + tuple.Item2.GetValueAsLiteral(), this.loc);
						}
						else
						{
							tuple.Item1.Label = switchLabel;
						}
					}
				}
			}
			if (!flag)
			{
				return false;
			}
			if (constant == null && this.SwitchType.BuiltinType == BuiltinTypeSpec.Type.String && this.string_labels.Count > 6)
			{
				this.ResolveStringSwitchMap(ec);
			}
			this.block.InsertStatement(0, new Switch.DispatchStatement(this));
			return true;
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000A5110 File Offset: 0x000A3310
		private bool HasOnlyDefaultSection()
		{
			for (int i = 0; i < this.block.Statements.Count; i++)
			{
				SwitchLabel switchLabel = this.block.Statements[i] as SwitchLabel;
				if (switchLabel != null && !switchLabel.IsDefault)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x000A5160 File Offset: 0x000A3360
		public override Reachability MarkReachable(Reachability rc)
		{
			if (rc.IsUnreachable)
			{
				return rc;
			}
			base.MarkReachable(rc);
			this.block.MarkReachableScope(rc);
			if (this.block.Statements.Count == 0)
			{
				return rc;
			}
			SwitchLabel switchLabel = null;
			Constant constant = this.new_expr as Constant;
			if (constant != null)
			{
				switchLabel = (this.FindLabel(constant) ?? this.case_default);
				if (switchLabel == null)
				{
					this.block.Statements.RemoveAt(0);
					return rc;
				}
			}
			Reachability rc2 = default(Reachability);
			SwitchLabel switchLabel2 = null;
			int i = 0;
			while (i < this.block.Statements.Count)
			{
				Statement statement = this.block.Statements[i];
				SwitchLabel switchLabel3 = statement as SwitchLabel;
				if (switchLabel3 == null || !switchLabel3.SectionStart)
				{
					goto IL_119;
				}
				if (switchLabel3.IsUnreachable)
				{
					if (switchLabel != null && switchLabel != switchLabel3)
					{
						rc2 = Reachability.CreateUnreachable();
					}
					else if (rc2.IsUnreachable)
					{
						rc2 = default(Reachability);
					}
					else if (switchLabel2 != null)
					{
						switchLabel3.SectionStart = false;
						statement = new Switch.MissingBreak(switchLabel2);
						statement.MarkReachable(rc);
						this.block.Statements.Insert(i - 1, statement);
						i++;
					}
					switchLabel2 = switchLabel3;
					goto IL_119;
				}
				rc2 = default(Reachability);
				IL_122:
				i++;
				continue;
				IL_119:
				rc2 = statement.MarkReachable(rc2);
				goto IL_122;
			}
			if (!rc2.IsUnreachable && switchLabel2 != null)
			{
				switchLabel2.SectionStart = false;
				Switch.MissingBreak missingBreak = new Switch.MissingBreak(switchLabel2)
				{
					FallOut = true
				};
				missingBreak.MarkReachable(rc);
				this.block.Statements.Add(missingBreak);
			}
			if (this.case_default == null && switchLabel == null)
			{
				return rc;
			}
			if (this.end_reachable)
			{
				return rc;
			}
			return Reachability.CreateUnreachable();
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x000A5305 File Offset: 0x000A3505
		public void RegisterGotoCase(GotoCase gotoCase, Constant value)
		{
			if (this.goto_cases == null)
			{
				this.goto_cases = new List<Tuple<GotoCase, Constant>>();
			}
			this.goto_cases.Add(Tuple.Create<GotoCase, Constant>(gotoCase, value));
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x000A532C File Offset: 0x000A352C
		private void ResolveStringSwitchMap(ResolveContext ec)
		{
			FullNamedExpression fullNamedExpression;
			if (ec.Module.PredefinedTypes.Dictionary.Define())
			{
				fullNamedExpression = new TypeExpression(ec.Module.PredefinedTypes.Dictionary.TypeSpec.MakeGenericType(ec, new BuiltinTypeSpec[]
				{
					ec.BuiltinTypes.String,
					ec.BuiltinTypes.Int
				}), this.loc);
			}
			else
			{
				if (!ec.Module.PredefinedTypes.Hashtable.Define())
				{
					ec.Module.PredefinedTypes.Dictionary.Resolve();
					return;
				}
				fullNamedExpression = new TypeExpression(ec.Module.PredefinedTypes.Hashtable.TypeSpec, this.loc);
			}
			TypeDefinition partialContainer = ec.CurrentMemberDefinition.Parent.PartialContainer;
			TypeDefinition parent = partialContainer;
			FullNamedExpression type = fullNamedExpression;
			Modifiers mod = Modifiers.PRIVATE | Modifiers.STATIC | Modifiers.COMPILER_GENERATED;
			string host = null;
			string typePrefix = "f";
			string name = "switch$map";
			ModuleContainer module = ec.Module;
			int counterSwitchTypes = module.CounterSwitchTypes;
			module.CounterSwitchTypes = counterSwitchTypes + 1;
			Field field = new Field(parent, type, mod, new MemberName(CompilerGeneratedContainer.MakeName(host, typePrefix, name, counterSwitchTypes), this.loc), null);
			if (!field.Define())
			{
				return;
			}
			partialContainer.AddField(field);
			List<Expression> list = new List<Expression>();
			int num = -1;
			this.labels = new Dictionary<long, SwitchLabel>(this.string_labels.Count);
			foreach (SwitchLabel switchLabel in this.case_labels)
			{
				if (switchLabel.SectionStart)
				{
					this.labels.Add((long)(++num), switchLabel);
				}
				if (switchLabel != this.case_default && switchLabel != this.case_null)
				{
					string s = (string)switchLabel.Converted.GetValue();
					List<Expression> list2 = new List<Expression>(2);
					list2.Add(new StringLiteral(ec.BuiltinTypes, s, switchLabel.Location));
					switchLabel.Converted = new IntConstant(ec.BuiltinTypes, num, this.loc);
					list2.Add(switchLabel.Converted);
					list.Add(new CollectionElementInitializer(list2, this.loc));
				}
			}
			Arguments arguments = new Arguments(1);
			arguments.Add(new Argument(new IntConstant(ec.BuiltinTypes, list.Count, this.loc)));
			Expression expression = new NewInitialize(fullNamedExpression, arguments, new CollectionOrObjectInitializers(list, this.loc), this.loc);
			this.switch_cache_field = new FieldExpr(field, this.loc);
			this.string_dictionary = new SimpleAssign(this.switch_cache_field, expression.Resolve(ec));
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000A55D0 File Offset: 0x000A37D0
		private void DoEmitStringSwitch(EmitContext ec)
		{
			Label label = ec.DefineLabel();
			this.value.EmitBranchable(ec, this.nullLabel, false);
			this.switch_cache_field.EmitBranchable(ec, label, true);
			using (ec.With(BuilderContext.Options.OmitDebugInfo, true))
			{
				this.string_dictionary.EmitStatement(ec);
			}
			ec.MarkLabel(label);
			LocalTemporary localTemporary = new LocalTemporary(ec.BuiltinTypes.Int);
			ResolveContext rc = new ResolveContext(ec.MemberContext);
			if (this.switch_cache_field.Type.IsGeneric)
			{
				Arguments arguments = new Arguments(2);
				arguments.Add(new Argument(this.value));
				arguments.Add(new Argument(localTemporary, Argument.AType.Out));
				Expression expression = new Invocation(new MemberAccess(this.switch_cache_field, "TryGetValue", this.loc), arguments).Resolve(rc);
				if (expression == null)
				{
					return;
				}
				expression.EmitBranchable(ec, this.defaultLabel, false);
			}
			else
			{
				Arguments arguments2 = new Arguments(1);
				arguments2.Add(new Argument(this.value));
				Expression expression2 = new ElementAccess(this.switch_cache_field, arguments2, this.loc).Resolve(rc);
				if (expression2 == null)
				{
					return;
				}
				LocalTemporary localTemporary2 = new LocalTemporary(ec.BuiltinTypes.Object);
				localTemporary2.EmitAssign(ec, expression2, true, false);
				ec.Emit(OpCodes.Brfalse, this.defaultLabel);
				((ExpressionStatement)new SimpleAssign(localTemporary, new Cast(new TypeExpression(ec.BuiltinTypes.Int, this.loc), localTemporary2, this.loc)).Resolve(rc)).EmitStatement(ec);
				localTemporary2.Release(ec);
			}
			this.EmitTableSwitch(ec, localTemporary);
			localTemporary.Release(ec);
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000A5790 File Offset: 0x000A3990
		private void EmitShortSwitch(EmitContext ec)
		{
			MethodSpec methodSpec = null;
			if (this.SwitchType.BuiltinType == BuiltinTypeSpec.Type.String)
			{
				methodSpec = ec.Module.PredefinedMembers.StringEqual.Resolve(this.loc);
			}
			if (methodSpec != null)
			{
				this.value.EmitBranchable(ec, this.nullLabel, false);
			}
			for (int i = 0; i < this.case_labels.Count; i++)
			{
				SwitchLabel switchLabel = this.case_labels[i];
				if (switchLabel != this.case_default && switchLabel != this.case_null)
				{
					Constant converted = switchLabel.Converted;
					if (converted == null)
					{
						switchLabel.Label.EmitBranchable(ec, switchLabel.GetILLabel(ec), true);
					}
					else if (methodSpec != null)
					{
						this.value.Emit(ec);
						converted.Emit(ec);
						default(CallEmitter).EmitPredefined(ec, methodSpec, new Arguments(0), false, null);
						ec.Emit(OpCodes.Brtrue, switchLabel.GetILLabel(ec));
					}
					else if (converted.IsZeroInteger && converted.Type.BuiltinType != BuiltinTypeSpec.Type.Long && converted.Type.BuiltinType != BuiltinTypeSpec.Type.ULong)
					{
						this.value.EmitBranchable(ec, switchLabel.GetILLabel(ec), false);
					}
					else
					{
						this.value.Emit(ec);
						converted.Emit(ec);
						ec.Emit(OpCodes.Beq, switchLabel.GetILLabel(ec));
					}
				}
			}
			ec.Emit(OpCodes.Br, this.defaultLabel);
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x000A5904 File Offset: 0x000A3B04
		private void EmitDispatch(EmitContext ec)
		{
			if (this.IsPatternMatching)
			{
				this.EmitShortSwitch(ec);
				return;
			}
			if (this.value == null)
			{
				int num = 0;
				using (List<SwitchLabel>.Enumerator enumerator = this.case_labels.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsUnreachable && num++ > 0)
						{
							Constant constant = (Constant)this.new_expr;
							SwitchLabel switchLabel = this.FindLabel(constant) ?? this.case_default;
							ec.Emit(OpCodes.Br, switchLabel.GetILLabel(ec));
							break;
						}
					}
				}
				return;
			}
			if (this.string_dictionary != null)
			{
				this.DoEmitStringSwitch(ec);
				return;
			}
			if (this.case_labels.Count < 4 || this.string_labels != null)
			{
				this.EmitShortSwitch(ec);
				return;
			}
			this.EmitTableSwitch(ec, this.value);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000A59E8 File Offset: 0x000A3BE8
		protected override void DoEmit(EmitContext ec)
		{
			Label loopEnd = ec.LoopEnd;
			Switch @switch = ec.Switch;
			ec.LoopEnd = ec.DefineLabel();
			ec.Switch = this;
			this.defaultLabel = ((this.case_default == null) ? ec.LoopEnd : this.case_default.GetILLabel(ec));
			this.nullLabel = ((this.case_null == null) ? this.defaultLabel : this.case_null.GetILLabel(ec));
			if (this.value != null)
			{
				ec.Mark(this.loc);
				Expression expression = this.new_expr ?? this.Expr;
				if (this.IsNullable)
				{
					this.unwrap.EmitCheck(ec);
					ec.Emit(OpCodes.Brfalse, this.nullLabel);
					this.value.EmitAssign(ec, expression, false, false);
				}
				else if (expression != this.value)
				{
					this.value.EmitAssign(ec, expression, false, false);
				}
				ec.Mark(this.block.StartLocation);
				this.block.IsCompilerGenerated = true;
			}
			else
			{
				this.new_expr.EmitSideEffect(ec);
			}
			this.block.Emit(ec);
			ec.MarkLabel(ec.LoopEnd);
			ec.LoopEnd = loopEnd;
			ec.Switch = @switch;
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000A5B24 File Offset: 0x000A3D24
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			Switch @switch = (Switch)t;
			@switch.Expr = this.Expr.Clone(clonectx);
			@switch.Statement = (@switch.block = (ExplicitBlock)this.block.Clone(clonectx));
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x000A5B68 File Offset: 0x000A3D68
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000A5B71 File Offset: 0x000A3D71
		public override void AddEndDefiniteAssignment(FlowAnalysisContext fc)
		{
			if (this.case_default == null && !(this.new_expr is Constant))
			{
				return;
			}
			if (this.end_reachable_das == null)
			{
				this.end_reachable_das = new List<DefiniteAssignmentBitSet>();
			}
			this.end_reachable_das.Add(fc.DefiniteAssignment);
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000A5BAD File Offset: 0x000A3DAD
		public override void SetEndReachable()
		{
			this.end_reachable = true;
		}

		// Token: 0x04000C66 RID: 3174
		public Expression Expr;

		// Token: 0x04000C67 RID: 3175
		private Dictionary<long, SwitchLabel> labels;

		// Token: 0x04000C68 RID: 3176
		private Dictionary<string, SwitchLabel> string_labels;

		// Token: 0x04000C69 RID: 3177
		private List<SwitchLabel> case_labels;

		// Token: 0x04000C6A RID: 3178
		private List<Tuple<GotoCase, Constant>> goto_cases;

		// Token: 0x04000C6B RID: 3179
		private List<DefiniteAssignmentBitSet> end_reachable_das;

		// Token: 0x04000C6C RID: 3180
		public TypeSpec SwitchType;

		// Token: 0x04000C6D RID: 3181
		private Expression new_expr;

		// Token: 0x04000C6E RID: 3182
		private SwitchLabel case_null;

		// Token: 0x04000C6F RID: 3183
		private SwitchLabel case_default;

		// Token: 0x04000C70 RID: 3184
		private Label defaultLabel;

		// Token: 0x04000C71 RID: 3185
		private Label nullLabel;

		// Token: 0x04000C72 RID: 3186
		private VariableReference value;

		// Token: 0x04000C73 RID: 3187
		private ExpressionStatement string_dictionary;

		// Token: 0x04000C74 RID: 3188
		private FieldExpr switch_cache_field;

		// Token: 0x04000C75 RID: 3189
		private ExplicitBlock block;

		// Token: 0x04000C76 RID: 3190
		private bool end_reachable;

		// Token: 0x04000C77 RID: 3191
		private Unwrap unwrap;

		// Token: 0x04000C78 RID: 3192
		[CompilerGenerated]
		private SwitchLabel <ActiveLabel>k__BackingField;

		// Token: 0x020003F4 RID: 1012
		private sealed class LabelsRange : IComparable<Switch.LabelsRange>
		{
			// Token: 0x060027EC RID: 10220 RVA: 0x000BD618 File Offset: 0x000BB818
			public LabelsRange(long value)
			{
				this.max = value;
				this.min = value;
				this.label_values = new List<long>();
				this.label_values.Add(value);
			}

			// Token: 0x060027ED RID: 10221 RVA: 0x000BD652 File Offset: 0x000BB852
			public LabelsRange(long min, long max, ICollection<long> values)
			{
				this.min = min;
				this.max = max;
				this.label_values = new List<long>(values);
			}

			// Token: 0x17000909 RID: 2313
			// (get) Token: 0x060027EE RID: 10222 RVA: 0x000BD674 File Offset: 0x000BB874
			public long Range
			{
				get
				{
					return this.max - this.min + 1L;
				}
			}

			// Token: 0x060027EF RID: 10223 RVA: 0x000BD688 File Offset: 0x000BB888
			public bool AddValue(long value)
			{
				long num = value - this.min + 1L;
				if (num > (long)(2 * (this.label_values.Count + 1)) || num <= 0L)
				{
					return false;
				}
				this.max = value;
				this.label_values.Add(value);
				return true;
			}

			// Token: 0x060027F0 RID: 10224 RVA: 0x000BD6D0 File Offset: 0x000BB8D0
			public int CompareTo(Switch.LabelsRange other)
			{
				int count = this.label_values.Count;
				int count2 = other.label_values.Count;
				if (count2 == count)
				{
					return (int)(other.min - this.min);
				}
				return count - count2;
			}

			// Token: 0x0400114E RID: 4430
			public readonly long min;

			// Token: 0x0400114F RID: 4431
			public long max;

			// Token: 0x04001150 RID: 4432
			public readonly List<long> label_values;
		}

		// Token: 0x020003F5 RID: 1013
		private sealed class DispatchStatement : Statement
		{
			// Token: 0x060027F1 RID: 10225 RVA: 0x000BD70B File Offset: 0x000BB90B
			public DispatchStatement(Switch body)
			{
				this.body = body;
			}

			// Token: 0x060027F2 RID: 10226 RVA: 0x00023DF4 File Offset: 0x00021FF4
			protected override void CloneTo(CloneContext clonectx, Statement target)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060027F3 RID: 10227 RVA: 0x000022F4 File Offset: 0x000004F4
			protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
			{
				return false;
			}

			// Token: 0x060027F4 RID: 10228 RVA: 0x000BD71A File Offset: 0x000BB91A
			protected override void DoEmit(EmitContext ec)
			{
				this.body.EmitDispatch(ec);
			}

			// Token: 0x04001151 RID: 4433
			private readonly Switch body;
		}

		// Token: 0x020003F6 RID: 1014
		private class MissingBreak : Statement
		{
			// Token: 0x060027F5 RID: 10229 RVA: 0x000BD728 File Offset: 0x000BB928
			public MissingBreak(SwitchLabel sl)
			{
				this.label = sl;
				this.loc = sl.loc;
			}

			// Token: 0x1700090A RID: 2314
			// (get) Token: 0x060027F6 RID: 10230 RVA: 0x000BD743 File Offset: 0x000BB943
			// (set) Token: 0x060027F7 RID: 10231 RVA: 0x000BD74B File Offset: 0x000BB94B
			public bool FallOut
			{
				[CompilerGenerated]
				get
				{
					return this.<FallOut>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<FallOut>k__BackingField = value;
				}
			}

			// Token: 0x060027F8 RID: 10232 RVA: 0x0000AF70 File Offset: 0x00009170
			protected override void DoEmit(EmitContext ec)
			{
			}

			// Token: 0x060027F9 RID: 10233 RVA: 0x0000AF70 File Offset: 0x00009170
			protected override void CloneTo(CloneContext clonectx, Statement target)
			{
			}

			// Token: 0x060027FA RID: 10234 RVA: 0x000BD754 File Offset: 0x000BB954
			protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
			{
				if (this.FallOut)
				{
					fc.Report.Error(8070, this.loc, "Control cannot fall out of switch statement through final case label `{0}'", this.label.GetSignatureForError());
				}
				else
				{
					fc.Report.Error(163, this.loc, "Control cannot fall through from one case label `{0}' to another", this.label.GetSignatureForError());
				}
				return true;
			}

			// Token: 0x04001152 RID: 4434
			private readonly SwitchLabel label;

			// Token: 0x04001153 RID: 4435
			[CompilerGenerated]
			private bool <FallOut>k__BackingField;
		}
	}
}

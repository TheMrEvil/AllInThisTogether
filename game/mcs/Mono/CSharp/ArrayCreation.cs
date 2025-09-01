using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001EA RID: 490
	public class ArrayCreation : Expression
	{
		// Token: 0x060019A9 RID: 6569 RVA: 0x0007E52C File Offset: 0x0007C72C
		public ArrayCreation(FullNamedExpression requested_base_type, List<Expression> exprs, ComposedTypeSpecifier rank, ArrayInitializer initializers, Location l) : this(requested_base_type, rank, initializers, l)
		{
			this.arguments = new List<Expression>(exprs);
			this.num_arguments = this.arguments.Count;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x0007E557 File Offset: 0x0007C757
		public ArrayCreation(FullNamedExpression requested_base_type, ComposedTypeSpecifier rank, ArrayInitializer initializers, Location loc)
		{
			this.requested_base_type = requested_base_type;
			this.rank = rank;
			this.initializers = initializers;
			this.loc = loc;
			if (rank != null)
			{
				this.num_arguments = rank.Dimension;
			}
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x0007E58B File Offset: 0x0007C78B
		public ArrayCreation(FullNamedExpression requested_base_type, ArrayInitializer initializers, Location loc) : this(requested_base_type, ComposedTypeSpecifier.SingleDimension, initializers, loc)
		{
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0007E59B File Offset: 0x0007C79B
		public ArrayCreation(FullNamedExpression requested_base_type, ArrayInitializer initializers) : this(requested_base_type, null, initializers, initializers.Location)
		{
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x0007E5AC File Offset: 0x0007C7AC
		public ComposedTypeSpecifier Rank
		{
			get
			{
				return this.rank;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x0007E5B4 File Offset: 0x0007C7B4
		public FullNamedExpression TypeExpression
		{
			get
			{
				return this.requested_base_type;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x0007E5BC File Offset: 0x0007C7BC
		public ArrayInitializer Initializers
		{
			get
			{
				return this.initializers;
			}
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0007E5C4 File Offset: 0x0007C7C4
		private bool CheckIndices(ResolveContext ec, ArrayInitializer probe, int idx, bool specified_dims, int child_bounds)
		{
			if (this.initializers != null && this.bounds == null)
			{
				this.array_data = new List<Expression>(probe.Count);
				this.bounds = new Dictionary<int, int>();
			}
			if (specified_dims)
			{
				Expression expression = this.arguments[idx];
				expression = expression.Resolve(ec);
				if (expression == null)
				{
					return false;
				}
				expression = base.ConvertExpressionToArrayIndex(ec, expression, false);
				if (expression == null)
				{
					return false;
				}
				this.arguments[idx] = expression;
				if (this.initializers != null)
				{
					Constant constant = expression as Constant;
					if (constant == null && expression is ArrayIndexCast)
					{
						constant = (((ArrayIndexCast)expression).Child as Constant);
					}
					if (constant == null)
					{
						ec.Report.Error(150, expression.Location, "A constant value is expected");
						return false;
					}
					int num;
					try
					{
						num = Convert.ToInt32(constant.GetValue());
					}
					catch
					{
						ec.Report.Error(150, expression.Location, "A constant value is expected");
						return false;
					}
					if (num != probe.Count)
					{
						ec.Report.Error(847, this.loc, "An array initializer of length `{0}' was expected", num.ToString());
						return false;
					}
					this.bounds[idx] = num;
				}
			}
			if (this.initializers == null)
			{
				return true;
			}
			for (int i = 0; i < probe.Count; i++)
			{
				Expression expression2 = probe[i];
				if (expression2 is ArrayInitializer)
				{
					ArrayInitializer arrayInitializer = expression2 as ArrayInitializer;
					if (idx + 1 >= this.dimensions)
					{
						ec.Report.Error(623, this.loc, "Array initializers can only be used in a variable or field initializer. Try using a new expression instead");
						return false;
					}
					if (!this.bounds.ContainsKey(idx + 1))
					{
						this.bounds[idx + 1] = arrayInitializer.Count;
					}
					if (this.bounds[idx + 1] != arrayInitializer.Count)
					{
						ec.Report.Error(847, arrayInitializer.Location, "An array initializer of length `{0}' was expected", this.bounds[idx + 1].ToString());
						return false;
					}
					if (!this.CheckIndices(ec, arrayInitializer, idx + 1, specified_dims, child_bounds - 1))
					{
						return false;
					}
				}
				else if (child_bounds > 1)
				{
					ec.Report.Error(846, expression2.Location, "A nested array initializer was expected");
				}
				else
				{
					Expression expression3 = this.ResolveArrayElement(ec, expression2);
					if (expression3 != null)
					{
						this.array_data.Add(expression3);
					}
				}
			}
			return true;
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0007E83C File Offset: 0x0007CA3C
		public override bool ContainsEmitWithAwait()
		{
			using (List<Expression>.Enumerator enumerator = this.arguments.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ContainsEmitWithAwait())
					{
						return true;
					}
				}
			}
			return this.InitializersContainAwait();
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0007E89C File Offset: 0x0007CA9C
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments arguments;
			if (this.array_data == null)
			{
				arguments = new Arguments(this.arguments.Count + 1);
				arguments.Add(new Argument(new TypeOf(this.array_element_type, this.loc)));
				foreach (Expression expression in this.arguments)
				{
					arguments.Add(new Argument(expression.CreateExpressionTree(ec)));
				}
				return base.CreateExpressionFactoryCall(ec, "NewArrayBounds", arguments);
			}
			if (this.dimensions > 1)
			{
				ec.Report.Error(838, this.loc, "An expression tree cannot contain a multidimensional array initializer");
				return null;
			}
			arguments = new Arguments((this.array_data == null) ? 1 : (this.array_data.Count + 1));
			arguments.Add(new Argument(new TypeOf(this.array_element_type, this.loc)));
			if (this.array_data != null)
			{
				for (int i = 0; i < this.array_data.Count; i++)
				{
					Expression expression2 = this.array_data[i];
					arguments.Add(new Argument(expression2.CreateExpressionTree(ec)));
				}
			}
			return base.CreateExpressionFactoryCall(ec, "NewArrayInit", arguments);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0007E9EC File Offset: 0x0007CBEC
		private void UpdateIndices(ResolveContext rc)
		{
			int num = 0;
			ArrayInitializer arrayInitializer = this.initializers;
			while (arrayInitializer != null)
			{
				Expression item = new IntConstant(rc.BuiltinTypes, arrayInitializer.Count, Location.Null);
				this.arguments.Add(item);
				this.bounds[num++] = arrayInitializer.Count;
				if (arrayInitializer.Count > 0 && arrayInitializer[0] is ArrayInitializer)
				{
					arrayInitializer = (ArrayInitializer)arrayInitializer[0];
				}
				else if (this.dimensions <= num)
				{
					return;
				}
			}
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0007EA70 File Offset: 0x0007CC70
		protected override void Error_NegativeArrayIndex(ResolveContext ec, Location loc)
		{
			ec.Report.Error(248, loc, "Cannot create an array with a negative size");
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0007EA88 File Offset: 0x0007CC88
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			foreach (Expression expression in this.arguments)
			{
				expression.FlowAnalysis(fc);
			}
			if (this.array_data != null)
			{
				foreach (Expression expression2 in this.array_data)
				{
					expression2.FlowAnalysis(fc);
				}
			}
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0007EB24 File Offset: 0x0007CD24
		private bool InitializersContainAwait()
		{
			if (this.array_data == null)
			{
				return false;
			}
			using (List<Expression>.Enumerator enumerator = this.array_data.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ContainsEmitWithAwait())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0007EB88 File Offset: 0x0007CD88
		protected virtual Expression ResolveArrayElement(ResolveContext ec, Expression element)
		{
			element = element.Resolve(ec);
			if (element == null)
			{
				return null;
			}
			if (element is CompoundAssign.TargetExpression)
			{
				if (this.first_emit != null)
				{
					throw new InternalErrorException("Can only handle one mutator at a time");
				}
				this.first_emit = element;
				element = (this.first_emit_temp = new LocalTemporary(element.Type));
			}
			return Convert.ImplicitConversionRequired(ec, element, this.array_element_type, this.loc);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0007EBF0 File Offset: 0x0007CDF0
		protected bool ResolveInitializers(ResolveContext ec)
		{
			if (this.arguments != null)
			{
				bool flag = true;
				for (int i = 0; i < this.arguments.Count; i++)
				{
					flag &= this.CheckIndices(ec, this.initializers, i, true, this.dimensions);
					if (this.initializers != null)
					{
						break;
					}
				}
				return flag;
			}
			this.arguments = new List<Expression>();
			if (!this.CheckIndices(ec, this.initializers, 0, false, this.dimensions))
			{
				return false;
			}
			this.UpdateIndices(ec);
			return true;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0007EC6C File Offset: 0x0007CE6C
		private bool ResolveArrayType(ResolveContext ec)
		{
			FullNamedExpression fullNamedExpression;
			if (this.num_arguments > 0)
			{
				fullNamedExpression = new ComposedCast(this.requested_base_type, this.rank);
			}
			else
			{
				fullNamedExpression = this.requested_base_type;
			}
			this.type = fullNamedExpression.ResolveAsType(ec, false);
			if (fullNamedExpression == null)
			{
				return false;
			}
			ArrayContainer arrayContainer = this.type as ArrayContainer;
			if (arrayContainer == null)
			{
				ec.Report.Error(622, this.loc, "Can only use array initializer expressions to assign to array types. Try using a new expression instead");
				return false;
			}
			this.array_element_type = arrayContainer.Element;
			this.dimensions = arrayContainer.Rank;
			return true;
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0007ECF5 File Offset: 0x0007CEF5
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (this.type != null)
			{
				return this;
			}
			if (!this.ResolveArrayType(ec))
			{
				return null;
			}
			if (!this.ResolveInitializers(ec))
			{
				return null;
			}
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0007ED20 File Offset: 0x0007CF20
		private byte[] MakeByteBlob()
		{
			int count = this.array_data.Count;
			TypeSpec underlyingType = this.array_element_type;
			if (underlyingType.IsEnum)
			{
				underlyingType = EnumSpec.GetUnderlyingType(underlyingType);
			}
			int size = BuiltinTypeSpec.GetSize(underlyingType);
			if (size == 0)
			{
				throw new Exception("unrecognized type in MakeByteBlob: " + underlyingType);
			}
			byte[] array = new byte[count * size + 3 & -4];
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				Constant constant = this.array_data[i] as Constant;
				if (constant == null)
				{
					num += size;
				}
				else
				{
					object value = constant.GetValue();
					switch (underlyingType.BuiltinType)
					{
					case BuiltinTypeSpec.Type.FirstPrimitive:
						array[num] = (((bool)value) ? 1 : 0);
						break;
					case BuiltinTypeSpec.Type.Byte:
						array[num] = (byte)value;
						break;
					case BuiltinTypeSpec.Type.SByte:
						array[num] = (byte)((sbyte)value);
						break;
					case BuiltinTypeSpec.Type.Char:
					{
						int num2 = (int)((char)value);
						array[num] = (byte)(num2 & 255);
						array[num + 1] = (byte)(num2 >> 8);
						break;
					}
					case BuiltinTypeSpec.Type.Short:
					{
						int num3 = (int)((short)value);
						array[num] = (byte)(num3 & 255);
						array[num + 1] = (byte)(num3 >> 8);
						break;
					}
					case BuiltinTypeSpec.Type.UShort:
					{
						int num4 = (int)((ushort)value);
						array[num] = (byte)(num4 & 255);
						array[num + 1] = (byte)(num4 >> 8);
						break;
					}
					case BuiltinTypeSpec.Type.Int:
					{
						int num5 = (int)value;
						array[num] = (byte)(num5 & 255);
						array[num + 1] = (byte)(num5 >> 8 & 255);
						array[num + 2] = (byte)(num5 >> 16 & 255);
						array[num + 3] = (byte)(num5 >> 24);
						break;
					}
					case BuiltinTypeSpec.Type.UInt:
					{
						uint num6 = (uint)value;
						array[num] = (byte)(num6 & 255U);
						array[num + 1] = (byte)(num6 >> 8 & 255U);
						array[num + 2] = (byte)(num6 >> 16 & 255U);
						array[num + 3] = (byte)(num6 >> 24);
						break;
					}
					case BuiltinTypeSpec.Type.Long:
					{
						long num7 = (long)value;
						for (int j = 0; j < size; j++)
						{
							array[num + j] = (byte)(num7 & 255L);
							num7 >>= 8;
						}
						break;
					}
					case BuiltinTypeSpec.Type.ULong:
					{
						ulong num8 = (ulong)value;
						for (int k = 0; k < size; k++)
						{
							array[num + k] = (byte)(num8 & 255UL);
							num8 >>= 8;
						}
						break;
					}
					case BuiltinTypeSpec.Type.Float:
					{
						int num9 = SingleConverter.SingleToInt32Bits((float)value);
						array[num] = (byte)(num9 & 255);
						array[num + 1] = (byte)(num9 >> 8 & 255);
						array[num + 2] = (byte)(num9 >> 16 & 255);
						array[num + 3] = (byte)(num9 >> 24);
						break;
					}
					case BuiltinTypeSpec.Type.Double:
					{
						byte[] bytes = BitConverter.GetBytes((double)value);
						for (int l = 0; l < size; l++)
						{
							array[num + l] = bytes[l];
						}
						if (!BitConverter.IsLittleEndian)
						{
							Array.Reverse(array, num, 8);
						}
						break;
					}
					case BuiltinTypeSpec.Type.Decimal:
					{
						int[] bits = decimal.GetBits((decimal)value);
						int num10 = num;
						int[] array2 = new int[]
						{
							bits[3],
							bits[2],
							bits[0],
							bits[1]
						};
						for (int m = 0; m < 4; m++)
						{
							array[num10++] = (byte)(array2[m] & 255);
							array[num10++] = (byte)(array2[m] >> 8 & 255);
							array[num10++] = (byte)(array2[m] >> 16 & 255);
							array[num10++] = (byte)(array2[m] >> 24);
						}
						break;
					}
					default:
						throw new Exception("Unrecognized type in MakeByteBlob: " + underlyingType);
					}
					num += size;
				}
			}
			return array;
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0007F100 File Offset: 0x0007D300
		private void EmitDynamicInitializers(EmitContext ec, bool emitConstants, StackFieldExpr stackArray)
		{
			int count = this.bounds.Count;
			int[] array = new int[count];
			for (int i = 0; i < this.array_data.Count; i++)
			{
				Expression expression = this.array_data[i];
				Constant constant = expression as Constant;
				if (constant == null || (constant != null && emitConstants && !constant.IsDefaultInitializer(this.array_element_type)))
				{
					TypeSpec type = expression.Type;
					if (stackArray != null)
					{
						if (expression.ContainsEmitWithAwait())
						{
							expression = expression.EmitToField(ec);
						}
						stackArray.EmitLoad(ec);
					}
					else
					{
						ec.Emit(OpCodes.Dup);
					}
					for (int j = 0; j < count; j++)
					{
						ec.EmitInt(array[j]);
					}
					if (count == 1 && type.IsStruct && !BuiltinTypeSpec.IsPrimitiveType(type))
					{
						ec.Emit(OpCodes.Ldelema, type);
					}
					expression.Emit(ec);
					ec.EmitArrayStore((ArrayContainer)this.type);
				}
				for (int k = count - 1; k >= 0; k--)
				{
					array[k]++;
					if (array[k] < this.bounds[k])
					{
						break;
					}
					array[k] = 0;
				}
			}
			if (stackArray != null)
			{
				stackArray.PrepareCleanup(ec);
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0007F238 File Offset: 0x0007D438
		public override void Emit(EmitContext ec)
		{
			FieldExpr fieldExpr = this.EmitToFieldSource(ec);
			if (fieldExpr != null)
			{
				fieldExpr.Emit(ec);
			}
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0007F258 File Offset: 0x0007D458
		protected sealed override FieldExpr EmitToFieldSource(EmitContext ec)
		{
			if (this.first_emit != null)
			{
				this.first_emit.Emit(ec);
				this.first_emit_temp.Store(ec);
			}
			StackFieldExpr stackFieldExpr;
			if (ec.HasSet(BuilderContext.Options.AsyncBody) && this.InitializersContainAwait())
			{
				stackFieldExpr = ec.GetTemporaryField(this.type, false);
				ec.EmitThis();
			}
			else
			{
				stackFieldExpr = null;
			}
			Expression.EmitExpressionsList(ec, this.arguments);
			ec.EmitArrayNew((ArrayContainer)this.type);
			if (this.initializers == null)
			{
				return stackFieldExpr;
			}
			if (stackFieldExpr != null)
			{
				stackFieldExpr.EmitAssignFromStack(ec);
			}
			this.EmitDynamicInitializers(ec, true, stackFieldExpr);
			if (this.first_emit_temp != null)
			{
				this.first_emit_temp.Release(ec);
			}
			return stackFieldExpr;
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0007F300 File Offset: 0x0007D500
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			if (this.arguments.Count != 1 || this.array_element_type.IsArray)
			{
				base.EncodeAttributeValue(rc, enc, targetType, parameterType);
				return;
			}
			if (this.type != targetType)
			{
				if (targetType.BuiltinType != BuiltinTypeSpec.Type.Object)
				{
					base.EncodeAttributeValue(rc, enc, targetType, parameterType);
					return;
				}
				if (enc.Encode(this.type) == AttributeEncoder.EncodedTypeProperties.DynamicType)
				{
					Attribute.Error_AttributeArgumentIsDynamic(rc, this.loc);
					return;
				}
			}
			if (this.array_data != null)
			{
				enc.Encode(this.array_data.Count);
				foreach (Expression expression in this.array_data)
				{
					expression.EncodeAttributeValue(rc, enc, this.array_element_type, parameterType);
				}
				return;
			}
			IntConstant intConstant = this.arguments[0] as IntConstant;
			if (intConstant == null || !intConstant.IsDefaultValue)
			{
				base.EncodeAttributeValue(rc, enc, targetType, parameterType);
				return;
			}
			enc.Encode(0);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0007F408 File Offset: 0x0007D608
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			ArrayCreation arrayCreation = (ArrayCreation)t;
			if (this.requested_base_type != null)
			{
				arrayCreation.requested_base_type = (FullNamedExpression)this.requested_base_type.Clone(clonectx);
			}
			if (this.arguments != null)
			{
				arrayCreation.arguments = new List<Expression>(this.arguments.Count);
				foreach (Expression expression in this.arguments)
				{
					arrayCreation.arguments.Add(expression.Clone(clonectx));
				}
			}
			if (this.initializers != null)
			{
				arrayCreation.initializers = (ArrayInitializer)this.initializers.Clone(clonectx);
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0007F4CC File Offset: 0x0007D6CC
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009D2 RID: 2514
		private FullNamedExpression requested_base_type;

		// Token: 0x040009D3 RID: 2515
		private ArrayInitializer initializers;

		// Token: 0x040009D4 RID: 2516
		protected List<Expression> arguments;

		// Token: 0x040009D5 RID: 2517
		protected TypeSpec array_element_type;

		// Token: 0x040009D6 RID: 2518
		private int num_arguments;

		// Token: 0x040009D7 RID: 2519
		protected int dimensions;

		// Token: 0x040009D8 RID: 2520
		protected readonly ComposedTypeSpecifier rank;

		// Token: 0x040009D9 RID: 2521
		private Expression first_emit;

		// Token: 0x040009DA RID: 2522
		private LocalTemporary first_emit_temp;

		// Token: 0x040009DB RID: 2523
		protected List<Expression> array_data;

		// Token: 0x040009DC RID: 2524
		private Dictionary<int, int> bounds;
	}
}

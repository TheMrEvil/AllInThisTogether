using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000E8 RID: 232
	internal sealed class ConstNode : ExpressionNode
	{
		// Token: 0x06000E07 RID: 3591 RVA: 0x0003A9FC File Offset: 0x00038BFC
		internal ConstNode(DataTable table, ValueType type, object constant) : this(table, type, constant, true)
		{
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003AA08 File Offset: 0x00038C08
		internal ConstNode(DataTable table, ValueType type, object constant, bool fParseQuotes) : base(table)
		{
			switch (type)
			{
			case ValueType.Null:
				this._val = DBNull.Value;
				return;
			case ValueType.Bool:
				this._val = Convert.ToBoolean(constant, CultureInfo.InvariantCulture);
				return;
			case ValueType.Numeric:
				this._val = this.SmallestNumeric(constant);
				return;
			case ValueType.Str:
				if (fParseQuotes)
				{
					this._val = ((string)constant).Replace("''", "'");
					return;
				}
				this._val = (string)constant;
				return;
			case ValueType.Float:
				this._val = Convert.ToDouble(constant, NumberFormatInfo.InvariantInfo);
				return;
			case ValueType.Decimal:
				this._val = this.SmallestDecimal(constant);
				return;
			case ValueType.Date:
				this._val = DateTime.Parse((string)constant, CultureInfo.InvariantCulture);
				return;
			}
			this._val = constant;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003AAED File Offset: 0x00038CED
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0003AAF6 File Offset: 0x00038CF6
		internal override object Eval()
		{
			return this._val;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003AAFE File Offset: 0x00038CFE
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.Eval();
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0003AAFE File Offset: 0x00038CFE
		internal override object Eval(int[] recordNos)
		{
			return this.Eval();
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00006D61 File Offset: 0x00004F61
		internal override bool IsConstant()
		{
			return true;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00006D61 File Offset: 0x00004F61
		internal override bool IsTableConstant()
		{
			return true;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00006D64 File Offset: 0x00004F64
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00006D64 File Offset: 0x00004F64
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00005696 File Offset: 0x00003896
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0003AB08 File Offset: 0x00038D08
		private object SmallestDecimal(object constant)
		{
			if (constant == null)
			{
				return 0.0;
			}
			string text = constant as string;
			if (text != null)
			{
				decimal num;
				if (decimal.TryParse(text, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out num))
				{
					return num;
				}
				double num2;
				if (double.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out num2))
				{
					return num2;
				}
			}
			else
			{
				IConvertible convertible = constant as IConvertible;
				if (convertible != null)
				{
					try
					{
						return convertible.ToDecimal(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException e)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e);
					}
					catch (FormatException e2)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e2);
					}
					catch (InvalidCastException e3)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e3);
					}
					catch (OverflowException e4)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e4);
					}
					try
					{
						return convertible.ToDouble(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException e5)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e5);
					}
					catch (FormatException e6)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e6);
					}
					catch (InvalidCastException e7)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e7);
					}
					catch (OverflowException e8)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e8);
					}
					return constant;
				}
			}
			return constant;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003AC40 File Offset: 0x00038E40
		private object SmallestNumeric(object constant)
		{
			if (constant == null)
			{
				return 0;
			}
			string text = constant as string;
			if (text != null)
			{
				int num;
				if (int.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
				{
					return num;
				}
				long num2;
				if (long.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2))
				{
					return num2;
				}
				double num3;
				if (double.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out num3))
				{
					return num3;
				}
			}
			else
			{
				IConvertible convertible = constant as IConvertible;
				if (convertible != null)
				{
					try
					{
						return convertible.ToInt32(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException e)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e);
					}
					catch (FormatException e2)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e2);
					}
					catch (InvalidCastException e3)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e3);
					}
					catch (OverflowException e4)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e4);
					}
					try
					{
						return convertible.ToInt64(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException e5)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e5);
					}
					catch (FormatException e6)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e6);
					}
					catch (InvalidCastException e7)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e7);
					}
					catch (OverflowException e8)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e8);
					}
					try
					{
						return convertible.ToDouble(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException e9)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e9);
					}
					catch (FormatException e10)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e10);
					}
					catch (InvalidCastException e11)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e11);
					}
					catch (OverflowException e12)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e12);
					}
					return constant;
				}
			}
			return constant;
		}

		// Token: 0x040008BF RID: 2239
		internal readonly object _val;
	}
}

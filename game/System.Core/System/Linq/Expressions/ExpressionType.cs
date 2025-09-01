using System;

namespace System.Linq.Expressions
{
	/// <summary>Describes the node types for the nodes of an expression tree.</summary>
	// Token: 0x02000259 RID: 601
	public enum ExpressionType
	{
		/// <summary>An addition operation, such as a + b, without overflow checking, for numeric operands.</summary>
		// Token: 0x04000996 RID: 2454
		Add,
		/// <summary>An addition operation, such as (a + b), with overflow checking, for numeric operands.</summary>
		// Token: 0x04000997 RID: 2455
		AddChecked,
		/// <summary>A bitwise or logical <see langword="AND" /> operation, such as (a &amp; b) in C# and (a And b) in Visual Basic.</summary>
		// Token: 0x04000998 RID: 2456
		And,
		/// <summary>A conditional <see langword="AND" /> operation that evaluates the second operand only if the first operand evaluates to <see langword="true" />. It corresponds to (a &amp;&amp; b) in C# and (a AndAlso b) in Visual Basic.</summary>
		// Token: 0x04000999 RID: 2457
		AndAlso,
		/// <summary>An operation that obtains the length of a one-dimensional array, such as array.Length.</summary>
		// Token: 0x0400099A RID: 2458
		ArrayLength,
		/// <summary>An indexing operation in a one-dimensional array, such as array[index] in C# or array(index) in Visual Basic.</summary>
		// Token: 0x0400099B RID: 2459
		ArrayIndex,
		/// <summary>A method call, such as in the obj.sampleMethod() expression.</summary>
		// Token: 0x0400099C RID: 2460
		Call,
		/// <summary>A node that represents a null coalescing operation, such as (a ?? b) in C# or If(a, b) in Visual Basic.</summary>
		// Token: 0x0400099D RID: 2461
		Coalesce,
		/// <summary>A conditional operation, such as a &gt; b ? a : b in C# or If(a &gt; b, a, b) in Visual Basic.</summary>
		// Token: 0x0400099E RID: 2462
		Conditional,
		/// <summary>A constant value.</summary>
		// Token: 0x0400099F RID: 2463
		Constant,
		/// <summary>A cast or conversion operation, such as (SampleType)obj in C#or CType(obj, SampleType) in Visual Basic. For a numeric conversion, if the converted value is too large for the destination type, no exception is thrown.</summary>
		// Token: 0x040009A0 RID: 2464
		Convert,
		/// <summary>A cast or conversion operation, such as (SampleType)obj in C#or CType(obj, SampleType) in Visual Basic. For a numeric conversion, if the converted value does not fit the destination type, an exception is thrown.</summary>
		// Token: 0x040009A1 RID: 2465
		ConvertChecked,
		/// <summary>A division operation, such as (a / b), for numeric operands.</summary>
		// Token: 0x040009A2 RID: 2466
		Divide,
		/// <summary>A node that represents an equality comparison, such as (a == b) in C# or (a = b) in Visual Basic.</summary>
		// Token: 0x040009A3 RID: 2467
		Equal,
		/// <summary>A bitwise or logical <see langword="XOR" /> operation, such as (a ^ b) in C# or (a Xor b) in Visual Basic.</summary>
		// Token: 0x040009A4 RID: 2468
		ExclusiveOr,
		/// <summary>A "greater than" comparison, such as (a &gt; b).</summary>
		// Token: 0x040009A5 RID: 2469
		GreaterThan,
		/// <summary>A "greater than or equal to" comparison, such as (a &gt;= b).</summary>
		// Token: 0x040009A6 RID: 2470
		GreaterThanOrEqual,
		/// <summary>An operation that invokes a delegate or lambda expression, such as sampleDelegate.Invoke().</summary>
		// Token: 0x040009A7 RID: 2471
		Invoke,
		/// <summary>A lambda expression, such as a =&gt; a + a in C# or Function(a) a + a in Visual Basic.</summary>
		// Token: 0x040009A8 RID: 2472
		Lambda,
		/// <summary>A bitwise left-shift operation, such as (a &lt;&lt; b).</summary>
		// Token: 0x040009A9 RID: 2473
		LeftShift,
		/// <summary>A "less than" comparison, such as (a &lt; b).</summary>
		// Token: 0x040009AA RID: 2474
		LessThan,
		/// <summary>A "less than or equal to" comparison, such as (a &lt;= b).</summary>
		// Token: 0x040009AB RID: 2475
		LessThanOrEqual,
		/// <summary>An operation that creates a new <see cref="T:System.Collections.IEnumerable" /> object and initializes it from a list of elements, such as new List&lt;SampleType&gt;(){ a, b, c } in C# or Dim sampleList = { a, b, c } in Visual Basic.</summary>
		// Token: 0x040009AC RID: 2476
		ListInit,
		/// <summary>An operation that reads from a field or property, such as obj.SampleProperty.</summary>
		// Token: 0x040009AD RID: 2477
		MemberAccess,
		/// <summary>An operation that creates a new object and initializes one or more of its members, such as new Point { X = 1, Y = 2 } in C# or New Point With {.X = 1, .Y = 2} in Visual Basic.</summary>
		// Token: 0x040009AE RID: 2478
		MemberInit,
		/// <summary>An arithmetic remainder operation, such as (a % b) in C# or (a Mod b) in Visual Basic.</summary>
		// Token: 0x040009AF RID: 2479
		Modulo,
		/// <summary>A multiplication operation, such as (a * b), without overflow checking, for numeric operands.</summary>
		// Token: 0x040009B0 RID: 2480
		Multiply,
		/// <summary>An multiplication operation, such as (a * b), that has overflow checking, for numeric operands.</summary>
		// Token: 0x040009B1 RID: 2481
		MultiplyChecked,
		/// <summary>An arithmetic negation operation, such as (-a). The object a should not be modified in place.</summary>
		// Token: 0x040009B2 RID: 2482
		Negate,
		/// <summary>A unary plus operation, such as (+a). The result of a predefined unary plus operation is the value of the operand, but user-defined implementations might have unusual results.</summary>
		// Token: 0x040009B3 RID: 2483
		UnaryPlus,
		/// <summary>An arithmetic negation operation, such as (-a), that has overflow checking. The object a should not be modified in place.</summary>
		// Token: 0x040009B4 RID: 2484
		NegateChecked,
		/// <summary>An operation that calls a constructor to create a new object, such as new SampleType().</summary>
		// Token: 0x040009B5 RID: 2485
		New,
		/// <summary>An operation that creates a new one-dimensional array and initializes it from a list of elements, such as new SampleType[]{a, b, c} in C# or New SampleType(){a, b, c} in Visual Basic.</summary>
		// Token: 0x040009B6 RID: 2486
		NewArrayInit,
		/// <summary>An operation that creates a new array, in which the bounds for each dimension are specified, such as new SampleType[dim1, dim2] in C# or New SampleType(dim1, dim2) in Visual Basic.</summary>
		// Token: 0x040009B7 RID: 2487
		NewArrayBounds,
		/// <summary>A bitwise complement or logical negation operation. In C#, it is equivalent to (~a) for integral types and to (!a) for Boolean values. In Visual Basic, it is equivalent to (Not a). The object a should not be modified in place.</summary>
		// Token: 0x040009B8 RID: 2488
		Not,
		/// <summary>An inequality comparison, such as (a != b) in C# or (a &lt;&gt; b) in Visual Basic.</summary>
		// Token: 0x040009B9 RID: 2489
		NotEqual,
		/// <summary>A bitwise or logical <see langword="OR" /> operation, such as (a | b) in C# or (a Or b) in Visual Basic.</summary>
		// Token: 0x040009BA RID: 2490
		Or,
		/// <summary>A short-circuiting conditional <see langword="OR" /> operation, such as (a || b) in C# or (a OrElse b) in Visual Basic.</summary>
		// Token: 0x040009BB RID: 2491
		OrElse,
		/// <summary>A reference to a parameter or variable that is defined in the context of the expression. For more information, see <see cref="T:System.Linq.Expressions.ParameterExpression" />.</summary>
		// Token: 0x040009BC RID: 2492
		Parameter,
		/// <summary>A mathematical operation that raises a number to a power, such as (a ^ b) in Visual Basic.</summary>
		// Token: 0x040009BD RID: 2493
		Power,
		/// <summary>An expression that has a constant value of type <see cref="T:System.Linq.Expressions.Expression" />. A <see cref="F:System.Linq.Expressions.ExpressionType.Quote" /> node can contain references to parameters that are defined in the context of the expression it represents.</summary>
		// Token: 0x040009BE RID: 2494
		Quote,
		/// <summary>A bitwise right-shift operation, such as (a &gt;&gt; b).</summary>
		// Token: 0x040009BF RID: 2495
		RightShift,
		/// <summary>A subtraction operation, such as (a - b), without overflow checking, for numeric operands.</summary>
		// Token: 0x040009C0 RID: 2496
		Subtract,
		/// <summary>An arithmetic subtraction operation, such as (a - b), that has overflow checking, for numeric operands.</summary>
		// Token: 0x040009C1 RID: 2497
		SubtractChecked,
		/// <summary>An explicit reference or boxing conversion in which <see langword="null" /> is supplied if the conversion fails, such as (obj as SampleType) in C# or TryCast(obj, SampleType) in Visual Basic.</summary>
		// Token: 0x040009C2 RID: 2498
		TypeAs,
		/// <summary>A type test, such as obj is SampleType in C# or TypeOf obj is SampleType in Visual Basic.</summary>
		// Token: 0x040009C3 RID: 2499
		TypeIs,
		/// <summary>An assignment operation, such as (a = b).</summary>
		// Token: 0x040009C4 RID: 2500
		Assign,
		/// <summary>A block of expressions.</summary>
		// Token: 0x040009C5 RID: 2501
		Block,
		/// <summary>Debugging information.</summary>
		// Token: 0x040009C6 RID: 2502
		DebugInfo,
		/// <summary>A unary decrement operation, such as (a - 1) in C# and Visual Basic. The object a should not be modified in place.</summary>
		// Token: 0x040009C7 RID: 2503
		Decrement,
		/// <summary>A dynamic operation.</summary>
		// Token: 0x040009C8 RID: 2504
		Dynamic,
		/// <summary>A default value.</summary>
		// Token: 0x040009C9 RID: 2505
		Default,
		/// <summary>An extension expression.</summary>
		// Token: 0x040009CA RID: 2506
		Extension,
		/// <summary>A "go to" expression, such as goto Label in C# or GoTo Label in Visual Basic.</summary>
		// Token: 0x040009CB RID: 2507
		Goto,
		/// <summary>A unary increment operation, such as (a + 1) in C# and Visual Basic. The object a should not be modified in place.</summary>
		// Token: 0x040009CC RID: 2508
		Increment,
		/// <summary>An index operation or an operation that accesses a property that takes arguments. </summary>
		// Token: 0x040009CD RID: 2509
		Index,
		/// <summary>A label.</summary>
		// Token: 0x040009CE RID: 2510
		Label,
		/// <summary>A list of run-time variables. For more information, see <see cref="T:System.Linq.Expressions.RuntimeVariablesExpression" />.</summary>
		// Token: 0x040009CF RID: 2511
		RuntimeVariables,
		/// <summary>A loop, such as for or while.</summary>
		// Token: 0x040009D0 RID: 2512
		Loop,
		/// <summary>A switch operation, such as <see langword="switch" /> in C# or <see langword="Select Case" /> in Visual Basic.</summary>
		// Token: 0x040009D1 RID: 2513
		Switch,
		/// <summary>An operation that throws an exception, such as throw new Exception().</summary>
		// Token: 0x040009D2 RID: 2514
		Throw,
		/// <summary>A <see langword="try-catch" /> expression.</summary>
		// Token: 0x040009D3 RID: 2515
		Try,
		/// <summary>An unbox value type operation, such as <see langword="unbox" /> and <see langword="unbox.any" /> instructions in MSIL. </summary>
		// Token: 0x040009D4 RID: 2516
		Unbox,
		/// <summary>An addition compound assignment operation, such as (a += b), without overflow checking, for numeric operands.</summary>
		// Token: 0x040009D5 RID: 2517
		AddAssign,
		/// <summary>A bitwise or logical <see langword="AND" /> compound assignment operation, such as (a &amp;= b) in C#.</summary>
		// Token: 0x040009D6 RID: 2518
		AndAssign,
		/// <summary>An division compound assignment operation, such as (a /= b), for numeric operands.</summary>
		// Token: 0x040009D7 RID: 2519
		DivideAssign,
		/// <summary>A bitwise or logical <see langword="XOR" /> compound assignment operation, such as (a ^= b) in C#.</summary>
		// Token: 0x040009D8 RID: 2520
		ExclusiveOrAssign,
		/// <summary>A bitwise left-shift compound assignment, such as (a &lt;&lt;= b).</summary>
		// Token: 0x040009D9 RID: 2521
		LeftShiftAssign,
		/// <summary>An arithmetic remainder compound assignment operation, such as (a %= b) in C#.</summary>
		// Token: 0x040009DA RID: 2522
		ModuloAssign,
		/// <summary>A multiplication compound assignment operation, such as (a *= b), without overflow checking, for numeric operands.</summary>
		// Token: 0x040009DB RID: 2523
		MultiplyAssign,
		/// <summary>A bitwise or logical <see langword="OR" /> compound assignment, such as (a |= b) in C#.</summary>
		// Token: 0x040009DC RID: 2524
		OrAssign,
		/// <summary>A compound assignment operation that raises a number to a power, such as (a ^= b) in Visual Basic.</summary>
		// Token: 0x040009DD RID: 2525
		PowerAssign,
		/// <summary>A bitwise right-shift compound assignment operation, such as (a &gt;&gt;= b).</summary>
		// Token: 0x040009DE RID: 2526
		RightShiftAssign,
		/// <summary>A subtraction compound assignment operation, such as (a -= b), without overflow checking, for numeric operands.</summary>
		// Token: 0x040009DF RID: 2527
		SubtractAssign,
		/// <summary>An addition compound assignment operation, such as (a += b), with overflow checking, for numeric operands.</summary>
		// Token: 0x040009E0 RID: 2528
		AddAssignChecked,
		/// <summary>A multiplication compound assignment operation, such as (a *= b), that has overflow checking, for numeric operands.</summary>
		// Token: 0x040009E1 RID: 2529
		MultiplyAssignChecked,
		/// <summary>A subtraction compound assignment operation, such as (a -= b), that has overflow checking, for numeric operands.</summary>
		// Token: 0x040009E2 RID: 2530
		SubtractAssignChecked,
		/// <summary>A unary prefix increment, such as (++a). The object a should be modified in place.</summary>
		// Token: 0x040009E3 RID: 2531
		PreIncrementAssign,
		/// <summary>A unary prefix decrement, such as (--a). The object a should be modified in place.</summary>
		// Token: 0x040009E4 RID: 2532
		PreDecrementAssign,
		/// <summary>A unary postfix increment, such as (a++). The object a should be modified in place.</summary>
		// Token: 0x040009E5 RID: 2533
		PostIncrementAssign,
		/// <summary>A unary postfix decrement, such as (a--). The object a should be modified in place.</summary>
		// Token: 0x040009E6 RID: 2534
		PostDecrementAssign,
		/// <summary>An exact type test.</summary>
		// Token: 0x040009E7 RID: 2535
		TypeEqual,
		/// <summary>A ones complement operation, such as (~a) in C#.</summary>
		// Token: 0x040009E8 RID: 2536
		OnesComplement,
		/// <summary>A <see langword="true" /> condition value.</summary>
		// Token: 0x040009E9 RID: 2537
		IsTrue,
		/// <summary>A <see langword="false" /> condition value.</summary>
		// Token: 0x040009EA RID: 2538
		IsFalse
	}
}

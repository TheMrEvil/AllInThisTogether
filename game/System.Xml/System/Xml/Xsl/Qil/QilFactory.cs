using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004BB RID: 1211
	internal sealed class QilFactory
	{
		// Token: 0x06003037 RID: 12343 RVA: 0x001202D7 File Offset: 0x0011E4D7
		public QilFactory()
		{
			this._typeCheck = new QilTypeChecker();
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06003038 RID: 12344 RVA: 0x001202EA File Offset: 0x0011E4EA
		public QilTypeChecker TypeChecker
		{
			get
			{
				return this._typeCheck;
			}
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x001202F4 File Offset: 0x0011E4F4
		public QilExpression QilExpression(QilNode root, QilFactory factory)
		{
			QilExpression qilExpression = new QilExpression(QilNodeType.QilExpression, root, factory);
			qilExpression.XmlType = this._typeCheck.CheckQilExpression(qilExpression);
			return qilExpression;
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x0012031D File Offset: 0x0011E51D
		public QilList ActualParameterList(IList<QilNode> values)
		{
			QilList qilList = this.ActualParameterList();
			qilList.Add(values);
			return qilList;
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x0012032C File Offset: 0x0011E52C
		public QilList FormalParameterList(IList<QilNode> values)
		{
			QilList qilList = this.FormalParameterList();
			qilList.Add(values);
			return qilList;
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x0012033B File Offset: 0x0011E53B
		public QilList BranchList(IList<QilNode> values)
		{
			QilList qilList = this.BranchList();
			qilList.Add(values);
			return qilList;
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x0012034A File Offset: 0x0011E54A
		public QilList Sequence(IList<QilNode> values)
		{
			QilList qilList = this.Sequence();
			qilList.Add(values);
			return qilList;
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x00120359 File Offset: 0x0011E559
		public QilParameter Parameter(XmlQueryType xmlType)
		{
			return this.Parameter(null, null, xmlType);
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x00120364 File Offset: 0x0011E564
		public QilStrConcat StrConcat(QilNode values)
		{
			return this.StrConcat(this.LiteralString(""), values);
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x00120378 File Offset: 0x0011E578
		public QilName LiteralQName(string local)
		{
			return this.LiteralQName(local, string.Empty, string.Empty);
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x0012038B File Offset: 0x0011E58B
		public QilTargetType TypeAssert(QilNode expr, XmlQueryType xmlType)
		{
			return this.TypeAssert(expr, this.LiteralType(xmlType));
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x0012039B File Offset: 0x0011E59B
		public QilTargetType IsType(QilNode expr, XmlQueryType xmlType)
		{
			return this.IsType(expr, this.LiteralType(xmlType));
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x001203AB File Offset: 0x0011E5AB
		public QilTargetType XsltConvert(QilNode expr, XmlQueryType xmlType)
		{
			return this.XsltConvert(expr, this.LiteralType(xmlType));
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x001203BB File Offset: 0x0011E5BB
		public QilFunction Function(QilNode arguments, QilNode sideEffects, XmlQueryType xmlType)
		{
			return this.Function(arguments, this.Unknown(xmlType), sideEffects, xmlType);
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x001203D0 File Offset: 0x0011E5D0
		public QilList FunctionList()
		{
			QilList qilList = new QilList(QilNodeType.FunctionList);
			qilList.XmlType = this._typeCheck.CheckFunctionList(qilList);
			return qilList;
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x001203F8 File Offset: 0x0011E5F8
		public QilList GlobalVariableList()
		{
			QilList qilList = new QilList(QilNodeType.GlobalVariableList);
			qilList.XmlType = this._typeCheck.CheckGlobalVariableList(qilList);
			return qilList;
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x00120420 File Offset: 0x0011E620
		public QilList GlobalParameterList()
		{
			QilList qilList = new QilList(QilNodeType.GlobalParameterList);
			qilList.XmlType = this._typeCheck.CheckGlobalParameterList(qilList);
			return qilList;
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x00120448 File Offset: 0x0011E648
		public QilList ActualParameterList()
		{
			QilList qilList = new QilList(QilNodeType.ActualParameterList);
			qilList.XmlType = this._typeCheck.CheckActualParameterList(qilList);
			return qilList;
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x00120470 File Offset: 0x0011E670
		public QilList FormalParameterList()
		{
			QilList qilList = new QilList(QilNodeType.FormalParameterList);
			qilList.XmlType = this._typeCheck.CheckFormalParameterList(qilList);
			return qilList;
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x00120498 File Offset: 0x0011E698
		public QilList SortKeyList()
		{
			QilList qilList = new QilList(QilNodeType.SortKeyList);
			qilList.XmlType = this._typeCheck.CheckSortKeyList(qilList);
			return qilList;
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x001204C0 File Offset: 0x0011E6C0
		public QilList BranchList()
		{
			QilList qilList = new QilList(QilNodeType.BranchList);
			qilList.XmlType = this._typeCheck.CheckBranchList(qilList);
			return qilList;
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x001204E8 File Offset: 0x0011E6E8
		public QilUnary OptimizeBarrier(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.OptimizeBarrier, child);
			qilUnary.XmlType = this._typeCheck.CheckOptimizeBarrier(qilUnary);
			return qilUnary;
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x00120510 File Offset: 0x0011E710
		public QilNode Unknown(XmlQueryType xmlType)
		{
			QilNode qilNode = new QilNode(QilNodeType.Unknown, xmlType);
			qilNode.XmlType = this._typeCheck.CheckUnknown(qilNode);
			return qilNode;
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x0012053C File Offset: 0x0011E73C
		public QilDataSource DataSource(QilNode name, QilNode baseUri)
		{
			QilDataSource qilDataSource = new QilDataSource(QilNodeType.DataSource, name, baseUri);
			qilDataSource.XmlType = this._typeCheck.CheckDataSource(qilDataSource);
			return qilDataSource;
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x00120568 File Offset: 0x0011E768
		public QilUnary Nop(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Nop, child);
			qilUnary.XmlType = this._typeCheck.CheckNop(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x00120594 File Offset: 0x0011E794
		public QilUnary Error(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Error, child);
			qilUnary.XmlType = this._typeCheck.CheckError(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x001205C0 File Offset: 0x0011E7C0
		public QilUnary Warning(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Warning, child);
			qilUnary.XmlType = this._typeCheck.CheckWarning(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x001205EC File Offset: 0x0011E7EC
		public QilIterator For(QilNode binding)
		{
			QilIterator qilIterator = new QilIterator(QilNodeType.For, binding);
			qilIterator.XmlType = this._typeCheck.CheckFor(qilIterator);
			return qilIterator;
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x00120618 File Offset: 0x0011E818
		public QilIterator Let(QilNode binding)
		{
			QilIterator qilIterator = new QilIterator(QilNodeType.Let, binding);
			qilIterator.XmlType = this._typeCheck.CheckLet(qilIterator);
			return qilIterator;
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x00120644 File Offset: 0x0011E844
		public QilParameter Parameter(QilNode defaultValue, QilNode name, XmlQueryType xmlType)
		{
			QilParameter qilParameter = new QilParameter(QilNodeType.Parameter, defaultValue, name, xmlType);
			qilParameter.XmlType = this._typeCheck.CheckParameter(qilParameter);
			return qilParameter;
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x00120670 File Offset: 0x0011E870
		public QilUnary PositionOf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.PositionOf, child);
			qilUnary.XmlType = this._typeCheck.CheckPositionOf(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x0012069C File Offset: 0x0011E89C
		public QilNode True()
		{
			QilNode qilNode = new QilNode(QilNodeType.True);
			qilNode.XmlType = this._typeCheck.CheckTrue(qilNode);
			return qilNode;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x001206C4 File Offset: 0x0011E8C4
		public QilNode False()
		{
			QilNode qilNode = new QilNode(QilNodeType.False);
			qilNode.XmlType = this._typeCheck.CheckFalse(qilNode);
			return qilNode;
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x001206EC File Offset: 0x0011E8EC
		public QilLiteral LiteralString(string value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralString, value);
			qilLiteral.XmlType = this._typeCheck.CheckLiteralString(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x00120718 File Offset: 0x0011E918
		public QilLiteral LiteralInt32(int value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralInt32, value);
			qilLiteral.XmlType = this._typeCheck.CheckLiteralInt32(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x00120748 File Offset: 0x0011E948
		public QilLiteral LiteralInt64(long value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralInt64, value);
			qilLiteral.XmlType = this._typeCheck.CheckLiteralInt64(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x00120778 File Offset: 0x0011E978
		public QilLiteral LiteralDouble(double value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralDouble, value);
			qilLiteral.XmlType = this._typeCheck.CheckLiteralDouble(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x001207A8 File Offset: 0x0011E9A8
		public QilLiteral LiteralDecimal(decimal value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralDecimal, value);
			qilLiteral.XmlType = this._typeCheck.CheckLiteralDecimal(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x001207D8 File Offset: 0x0011E9D8
		public QilName LiteralQName(string localName, string namespaceUri, string prefix)
		{
			QilName qilName = new QilName(QilNodeType.LiteralQName, localName, namespaceUri, prefix);
			qilName.XmlType = this._typeCheck.CheckLiteralQName(qilName);
			return qilName;
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x00120804 File Offset: 0x0011EA04
		public QilLiteral LiteralType(XmlQueryType value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralType, value);
			qilLiteral.XmlType = this._typeCheck.CheckLiteralType(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x00120830 File Offset: 0x0011EA30
		public QilLiteral LiteralObject(object value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralObject, value);
			qilLiteral.XmlType = this._typeCheck.CheckLiteralObject(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x0012085C File Offset: 0x0011EA5C
		public QilBinary And(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.And, left, right);
			qilBinary.XmlType = this._typeCheck.CheckAnd(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x00120888 File Offset: 0x0011EA88
		public QilBinary Or(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Or, left, right);
			qilBinary.XmlType = this._typeCheck.CheckOr(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x001208B4 File Offset: 0x0011EAB4
		public QilUnary Not(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Not, child);
			qilUnary.XmlType = this._typeCheck.CheckNot(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x001208E0 File Offset: 0x0011EAE0
		public QilTernary Conditional(QilNode left, QilNode center, QilNode right)
		{
			QilTernary qilTernary = new QilTernary(QilNodeType.Conditional, left, center, right);
			qilTernary.XmlType = this._typeCheck.CheckConditional(qilTernary);
			return qilTernary;
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x0012090C File Offset: 0x0011EB0C
		public QilChoice Choice(QilNode expression, QilNode branches)
		{
			QilChoice qilChoice = new QilChoice(QilNodeType.Choice, expression, branches);
			qilChoice.XmlType = this._typeCheck.CheckChoice(qilChoice);
			return qilChoice;
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x00120938 File Offset: 0x0011EB38
		public QilUnary Length(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Length, child);
			qilUnary.XmlType = this._typeCheck.CheckLength(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x00120964 File Offset: 0x0011EB64
		public QilList Sequence()
		{
			QilList qilList = new QilList(QilNodeType.Sequence);
			qilList.XmlType = this._typeCheck.CheckSequence(qilList);
			return qilList;
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x0012098C File Offset: 0x0011EB8C
		public QilBinary Union(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Union, left, right);
			qilBinary.XmlType = this._typeCheck.CheckUnion(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x001209B8 File Offset: 0x0011EBB8
		public QilBinary Intersection(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Intersection, left, right);
			qilBinary.XmlType = this._typeCheck.CheckIntersection(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x001209E4 File Offset: 0x0011EBE4
		public QilBinary Difference(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Difference, left, right);
			qilBinary.XmlType = this._typeCheck.CheckDifference(qilBinary);
			return qilBinary;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x00120A10 File Offset: 0x0011EC10
		public QilUnary Sum(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Sum, child);
			qilUnary.XmlType = this._typeCheck.CheckSum(qilUnary);
			return qilUnary;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x00120A3C File Offset: 0x0011EC3C
		public QilUnary Negate(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Negate, child);
			qilUnary.XmlType = this._typeCheck.CheckNegate(qilUnary);
			return qilUnary;
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x00120A68 File Offset: 0x0011EC68
		public QilBinary Add(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Add, left, right);
			qilBinary.XmlType = this._typeCheck.CheckAdd(qilBinary);
			return qilBinary;
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x00120A94 File Offset: 0x0011EC94
		public QilBinary Subtract(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Subtract, left, right);
			qilBinary.XmlType = this._typeCheck.CheckSubtract(qilBinary);
			return qilBinary;
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x00120AC0 File Offset: 0x0011ECC0
		public QilBinary Multiply(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Multiply, left, right);
			qilBinary.XmlType = this._typeCheck.CheckMultiply(qilBinary);
			return qilBinary;
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x00120AEC File Offset: 0x0011ECEC
		public QilBinary Divide(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Divide, left, right);
			qilBinary.XmlType = this._typeCheck.CheckDivide(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x00120B18 File Offset: 0x0011ED18
		public QilBinary Modulo(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Modulo, left, right);
			qilBinary.XmlType = this._typeCheck.CheckModulo(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x00120B44 File Offset: 0x0011ED44
		public QilUnary StrLength(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.StrLength, child);
			qilUnary.XmlType = this._typeCheck.CheckStrLength(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x00120B70 File Offset: 0x0011ED70
		public QilStrConcat StrConcat(QilNode delimiter, QilNode values)
		{
			QilStrConcat qilStrConcat = new QilStrConcat(QilNodeType.StrConcat, delimiter, values);
			qilStrConcat.XmlType = this._typeCheck.CheckStrConcat(qilStrConcat);
			return qilStrConcat;
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x00120B9C File Offset: 0x0011ED9C
		public QilBinary StrParseQName(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.StrParseQName, left, right);
			qilBinary.XmlType = this._typeCheck.CheckStrParseQName(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x00120BC8 File Offset: 0x0011EDC8
		public QilBinary Ne(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Ne, left, right);
			qilBinary.XmlType = this._typeCheck.CheckNe(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x00120BF4 File Offset: 0x0011EDF4
		public QilBinary Eq(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Eq, left, right);
			qilBinary.XmlType = this._typeCheck.CheckEq(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x00120C20 File Offset: 0x0011EE20
		public QilBinary Gt(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Gt, left, right);
			qilBinary.XmlType = this._typeCheck.CheckGt(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x00120C4C File Offset: 0x0011EE4C
		public QilBinary Ge(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Ge, left, right);
			qilBinary.XmlType = this._typeCheck.CheckGe(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x00120C78 File Offset: 0x0011EE78
		public QilBinary Lt(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Lt, left, right);
			qilBinary.XmlType = this._typeCheck.CheckLt(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x00120CA4 File Offset: 0x0011EEA4
		public QilBinary Le(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Le, left, right);
			qilBinary.XmlType = this._typeCheck.CheckLe(qilBinary);
			return qilBinary;
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x00120CD0 File Offset: 0x0011EED0
		public QilBinary Is(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Is, left, right);
			qilBinary.XmlType = this._typeCheck.CheckIs(qilBinary);
			return qilBinary;
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x00120CFC File Offset: 0x0011EEFC
		public QilBinary Before(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Before, left, right);
			qilBinary.XmlType = this._typeCheck.CheckBefore(qilBinary);
			return qilBinary;
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x00120D28 File Offset: 0x0011EF28
		public QilLoop Loop(QilNode variable, QilNode body)
		{
			QilLoop qilLoop = new QilLoop(QilNodeType.Loop, variable, body);
			qilLoop.XmlType = this._typeCheck.CheckLoop(qilLoop);
			return qilLoop;
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x00120D54 File Offset: 0x0011EF54
		public QilLoop Filter(QilNode variable, QilNode body)
		{
			QilLoop qilLoop = new QilLoop(QilNodeType.Filter, variable, body);
			qilLoop.XmlType = this._typeCheck.CheckFilter(qilLoop);
			return qilLoop;
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x00120D80 File Offset: 0x0011EF80
		public QilLoop Sort(QilNode variable, QilNode body)
		{
			QilLoop qilLoop = new QilLoop(QilNodeType.Sort, variable, body);
			qilLoop.XmlType = this._typeCheck.CheckSort(qilLoop);
			return qilLoop;
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x00120DAC File Offset: 0x0011EFAC
		public QilSortKey SortKey(QilNode key, QilNode collation)
		{
			QilSortKey qilSortKey = new QilSortKey(QilNodeType.SortKey, key, collation);
			qilSortKey.XmlType = this._typeCheck.CheckSortKey(qilSortKey);
			return qilSortKey;
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x00120DD8 File Offset: 0x0011EFD8
		public QilUnary DocOrderDistinct(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.DocOrderDistinct, child);
			qilUnary.XmlType = this._typeCheck.CheckDocOrderDistinct(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x00120E04 File Offset: 0x0011F004
		public QilFunction Function(QilNode arguments, QilNode definition, QilNode sideEffects, XmlQueryType xmlType)
		{
			QilFunction qilFunction = new QilFunction(QilNodeType.Function, arguments, definition, sideEffects, xmlType);
			qilFunction.XmlType = this._typeCheck.CheckFunction(qilFunction);
			return qilFunction;
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x00120E34 File Offset: 0x0011F034
		public QilInvoke Invoke(QilNode function, QilNode arguments)
		{
			QilInvoke qilInvoke = new QilInvoke(QilNodeType.Invoke, function, arguments);
			qilInvoke.XmlType = this._typeCheck.CheckInvoke(qilInvoke);
			return qilInvoke;
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x00120E60 File Offset: 0x0011F060
		public QilUnary Content(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Content, child);
			qilUnary.XmlType = this._typeCheck.CheckContent(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x00120E8C File Offset: 0x0011F08C
		public QilBinary Attribute(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Attribute, left, right);
			qilBinary.XmlType = this._typeCheck.CheckAttribute(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x00120EB8 File Offset: 0x0011F0B8
		public QilUnary Parent(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Parent, child);
			qilUnary.XmlType = this._typeCheck.CheckParent(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x00120EE4 File Offset: 0x0011F0E4
		public QilUnary Root(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Root, child);
			qilUnary.XmlType = this._typeCheck.CheckRoot(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x00120F10 File Offset: 0x0011F110
		public QilNode XmlContext()
		{
			QilNode qilNode = new QilNode(QilNodeType.XmlContext);
			qilNode.XmlType = this._typeCheck.CheckXmlContext(qilNode);
			return qilNode;
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x00120F38 File Offset: 0x0011F138
		public QilUnary Descendant(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Descendant, child);
			qilUnary.XmlType = this._typeCheck.CheckDescendant(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x00120F64 File Offset: 0x0011F164
		public QilUnary DescendantOrSelf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.DescendantOrSelf, child);
			qilUnary.XmlType = this._typeCheck.CheckDescendantOrSelf(qilUnary);
			return qilUnary;
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x00120F90 File Offset: 0x0011F190
		public QilUnary Ancestor(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Ancestor, child);
			qilUnary.XmlType = this._typeCheck.CheckAncestor(qilUnary);
			return qilUnary;
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x00120FBC File Offset: 0x0011F1BC
		public QilUnary AncestorOrSelf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.AncestorOrSelf, child);
			qilUnary.XmlType = this._typeCheck.CheckAncestorOrSelf(qilUnary);
			return qilUnary;
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x00120FE8 File Offset: 0x0011F1E8
		public QilUnary Preceding(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Preceding, child);
			qilUnary.XmlType = this._typeCheck.CheckPreceding(qilUnary);
			return qilUnary;
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x00121014 File Offset: 0x0011F214
		public QilUnary FollowingSibling(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.FollowingSibling, child);
			qilUnary.XmlType = this._typeCheck.CheckFollowingSibling(qilUnary);
			return qilUnary;
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x00121040 File Offset: 0x0011F240
		public QilUnary PrecedingSibling(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.PrecedingSibling, child);
			qilUnary.XmlType = this._typeCheck.CheckPrecedingSibling(qilUnary);
			return qilUnary;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x0012106C File Offset: 0x0011F26C
		public QilBinary NodeRange(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.NodeRange, left, right);
			qilBinary.XmlType = this._typeCheck.CheckNodeRange(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x00121098 File Offset: 0x0011F298
		public QilBinary Deref(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Deref, left, right);
			qilBinary.XmlType = this._typeCheck.CheckDeref(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x001210C4 File Offset: 0x0011F2C4
		public QilBinary ElementCtor(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.ElementCtor, left, right);
			qilBinary.XmlType = this._typeCheck.CheckElementCtor(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x001210F0 File Offset: 0x0011F2F0
		public QilBinary AttributeCtor(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.AttributeCtor, left, right);
			qilBinary.XmlType = this._typeCheck.CheckAttributeCtor(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x0012111C File Offset: 0x0011F31C
		public QilUnary CommentCtor(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.CommentCtor, child);
			qilUnary.XmlType = this._typeCheck.CheckCommentCtor(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x00121148 File Offset: 0x0011F348
		public QilBinary PICtor(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.PICtor, left, right);
			qilBinary.XmlType = this._typeCheck.CheckPICtor(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x00121174 File Offset: 0x0011F374
		public QilUnary TextCtor(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.TextCtor, child);
			qilUnary.XmlType = this._typeCheck.CheckTextCtor(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x001211A0 File Offset: 0x0011F3A0
		public QilUnary RawTextCtor(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.RawTextCtor, child);
			qilUnary.XmlType = this._typeCheck.CheckRawTextCtor(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x001211CC File Offset: 0x0011F3CC
		public QilUnary DocumentCtor(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.DocumentCtor, child);
			qilUnary.XmlType = this._typeCheck.CheckDocumentCtor(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x001211F8 File Offset: 0x0011F3F8
		public QilBinary NamespaceDecl(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.NamespaceDecl, left, right);
			qilBinary.XmlType = this._typeCheck.CheckNamespaceDecl(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x00121224 File Offset: 0x0011F424
		public QilBinary RtfCtor(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.RtfCtor, left, right);
			qilBinary.XmlType = this._typeCheck.CheckRtfCtor(qilBinary);
			return qilBinary;
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x00121250 File Offset: 0x0011F450
		public QilUnary NameOf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.NameOf, child);
			qilUnary.XmlType = this._typeCheck.CheckNameOf(qilUnary);
			return qilUnary;
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x0012127C File Offset: 0x0011F47C
		public QilUnary LocalNameOf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.LocalNameOf, child);
			qilUnary.XmlType = this._typeCheck.CheckLocalNameOf(qilUnary);
			return qilUnary;
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x001212A8 File Offset: 0x0011F4A8
		public QilUnary NamespaceUriOf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.NamespaceUriOf, child);
			qilUnary.XmlType = this._typeCheck.CheckNamespaceUriOf(qilUnary);
			return qilUnary;
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x001212D4 File Offset: 0x0011F4D4
		public QilUnary PrefixOf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.PrefixOf, child);
			qilUnary.XmlType = this._typeCheck.CheckPrefixOf(qilUnary);
			return qilUnary;
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x00121300 File Offset: 0x0011F500
		public QilTargetType TypeAssert(QilNode source, QilNode targetType)
		{
			QilTargetType qilTargetType = new QilTargetType(QilNodeType.TypeAssert, source, targetType);
			qilTargetType.XmlType = this._typeCheck.CheckTypeAssert(qilTargetType);
			return qilTargetType;
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x0012132C File Offset: 0x0011F52C
		public QilTargetType IsType(QilNode source, QilNode targetType)
		{
			QilTargetType qilTargetType = new QilTargetType(QilNodeType.IsType, source, targetType);
			qilTargetType.XmlType = this._typeCheck.CheckIsType(qilTargetType);
			return qilTargetType;
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x00121358 File Offset: 0x0011F558
		public QilUnary IsEmpty(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.IsEmpty, child);
			qilUnary.XmlType = this._typeCheck.CheckIsEmpty(qilUnary);
			return qilUnary;
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x00121384 File Offset: 0x0011F584
		public QilUnary XPathNodeValue(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.XPathNodeValue, child);
			qilUnary.XmlType = this._typeCheck.CheckXPathNodeValue(qilUnary);
			return qilUnary;
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x001213B0 File Offset: 0x0011F5B0
		public QilUnary XPathFollowing(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.XPathFollowing, child);
			qilUnary.XmlType = this._typeCheck.CheckXPathFollowing(qilUnary);
			return qilUnary;
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x001213DC File Offset: 0x0011F5DC
		public QilUnary XPathPreceding(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.XPathPreceding, child);
			qilUnary.XmlType = this._typeCheck.CheckXPathPreceding(qilUnary);
			return qilUnary;
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x00121408 File Offset: 0x0011F608
		public QilUnary XPathNamespace(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.XPathNamespace, child);
			qilUnary.XmlType = this._typeCheck.CheckXPathNamespace(qilUnary);
			return qilUnary;
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x00121434 File Offset: 0x0011F634
		public QilUnary XsltGenerateId(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.XsltGenerateId, child);
			qilUnary.XmlType = this._typeCheck.CheckXsltGenerateId(qilUnary);
			return qilUnary;
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x00121460 File Offset: 0x0011F660
		public QilInvokeLateBound XsltInvokeLateBound(QilNode name, QilNode arguments)
		{
			QilInvokeLateBound qilInvokeLateBound = new QilInvokeLateBound(QilNodeType.XsltInvokeLateBound, name, arguments);
			qilInvokeLateBound.XmlType = this._typeCheck.CheckXsltInvokeLateBound(qilInvokeLateBound);
			return qilInvokeLateBound;
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x0012148C File Offset: 0x0011F68C
		public QilInvokeEarlyBound XsltInvokeEarlyBound(QilNode name, QilNode clrMethod, QilNode arguments, XmlQueryType xmlType)
		{
			QilInvokeEarlyBound qilInvokeEarlyBound = new QilInvokeEarlyBound(QilNodeType.XsltInvokeEarlyBound, name, clrMethod, arguments, xmlType);
			qilInvokeEarlyBound.XmlType = this._typeCheck.CheckXsltInvokeEarlyBound(qilInvokeEarlyBound);
			return qilInvokeEarlyBound;
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x001214BC File Offset: 0x0011F6BC
		public QilBinary XsltCopy(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.XsltCopy, left, right);
			qilBinary.XmlType = this._typeCheck.CheckXsltCopy(qilBinary);
			return qilBinary;
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x001214E8 File Offset: 0x0011F6E8
		public QilUnary XsltCopyOf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.XsltCopyOf, child);
			qilUnary.XmlType = this._typeCheck.CheckXsltCopyOf(qilUnary);
			return qilUnary;
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x00121514 File Offset: 0x0011F714
		public QilTargetType XsltConvert(QilNode source, QilNode targetType)
		{
			QilTargetType qilTargetType = new QilTargetType(QilNodeType.XsltConvert, source, targetType);
			qilTargetType.XmlType = this._typeCheck.CheckXsltConvert(qilTargetType);
			return qilTargetType;
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("QIL_TRACE_NODE_CREATION")]
		public void TraceNode(QilNode n)
		{
		}

		// Token: 0x040025C6 RID: 9670
		private QilTypeChecker _typeCheck;
	}
}

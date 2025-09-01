using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004C6 RID: 1222
	internal enum QilNodeType
	{
		// Token: 0x040025D6 RID: 9686
		QilExpression,
		// Token: 0x040025D7 RID: 9687
		FunctionList,
		// Token: 0x040025D8 RID: 9688
		GlobalVariableList,
		// Token: 0x040025D9 RID: 9689
		GlobalParameterList,
		// Token: 0x040025DA RID: 9690
		ActualParameterList,
		// Token: 0x040025DB RID: 9691
		FormalParameterList,
		// Token: 0x040025DC RID: 9692
		SortKeyList,
		// Token: 0x040025DD RID: 9693
		BranchList,
		// Token: 0x040025DE RID: 9694
		OptimizeBarrier,
		// Token: 0x040025DF RID: 9695
		Unknown,
		// Token: 0x040025E0 RID: 9696
		DataSource,
		// Token: 0x040025E1 RID: 9697
		Nop,
		// Token: 0x040025E2 RID: 9698
		Error,
		// Token: 0x040025E3 RID: 9699
		Warning,
		// Token: 0x040025E4 RID: 9700
		For,
		// Token: 0x040025E5 RID: 9701
		Let,
		// Token: 0x040025E6 RID: 9702
		Parameter,
		// Token: 0x040025E7 RID: 9703
		PositionOf,
		// Token: 0x040025E8 RID: 9704
		True,
		// Token: 0x040025E9 RID: 9705
		False,
		// Token: 0x040025EA RID: 9706
		LiteralString,
		// Token: 0x040025EB RID: 9707
		LiteralInt32,
		// Token: 0x040025EC RID: 9708
		LiteralInt64,
		// Token: 0x040025ED RID: 9709
		LiteralDouble,
		// Token: 0x040025EE RID: 9710
		LiteralDecimal,
		// Token: 0x040025EF RID: 9711
		LiteralQName,
		// Token: 0x040025F0 RID: 9712
		LiteralType,
		// Token: 0x040025F1 RID: 9713
		LiteralObject,
		// Token: 0x040025F2 RID: 9714
		And,
		// Token: 0x040025F3 RID: 9715
		Or,
		// Token: 0x040025F4 RID: 9716
		Not,
		// Token: 0x040025F5 RID: 9717
		Conditional,
		// Token: 0x040025F6 RID: 9718
		Choice,
		// Token: 0x040025F7 RID: 9719
		Length,
		// Token: 0x040025F8 RID: 9720
		Sequence,
		// Token: 0x040025F9 RID: 9721
		Union,
		// Token: 0x040025FA RID: 9722
		Intersection,
		// Token: 0x040025FB RID: 9723
		Difference,
		// Token: 0x040025FC RID: 9724
		Average,
		// Token: 0x040025FD RID: 9725
		Sum,
		// Token: 0x040025FE RID: 9726
		Minimum,
		// Token: 0x040025FF RID: 9727
		Maximum,
		// Token: 0x04002600 RID: 9728
		Negate,
		// Token: 0x04002601 RID: 9729
		Add,
		// Token: 0x04002602 RID: 9730
		Subtract,
		// Token: 0x04002603 RID: 9731
		Multiply,
		// Token: 0x04002604 RID: 9732
		Divide,
		// Token: 0x04002605 RID: 9733
		Modulo,
		// Token: 0x04002606 RID: 9734
		StrLength,
		// Token: 0x04002607 RID: 9735
		StrConcat,
		// Token: 0x04002608 RID: 9736
		StrParseQName,
		// Token: 0x04002609 RID: 9737
		Ne,
		// Token: 0x0400260A RID: 9738
		Eq,
		// Token: 0x0400260B RID: 9739
		Gt,
		// Token: 0x0400260C RID: 9740
		Ge,
		// Token: 0x0400260D RID: 9741
		Lt,
		// Token: 0x0400260E RID: 9742
		Le,
		// Token: 0x0400260F RID: 9743
		Is,
		// Token: 0x04002610 RID: 9744
		After,
		// Token: 0x04002611 RID: 9745
		Before,
		// Token: 0x04002612 RID: 9746
		Loop,
		// Token: 0x04002613 RID: 9747
		Filter,
		// Token: 0x04002614 RID: 9748
		Sort,
		// Token: 0x04002615 RID: 9749
		SortKey,
		// Token: 0x04002616 RID: 9750
		DocOrderDistinct,
		// Token: 0x04002617 RID: 9751
		Function,
		// Token: 0x04002618 RID: 9752
		Invoke,
		// Token: 0x04002619 RID: 9753
		Content,
		// Token: 0x0400261A RID: 9754
		Attribute,
		// Token: 0x0400261B RID: 9755
		Parent,
		// Token: 0x0400261C RID: 9756
		Root,
		// Token: 0x0400261D RID: 9757
		XmlContext,
		// Token: 0x0400261E RID: 9758
		Descendant,
		// Token: 0x0400261F RID: 9759
		DescendantOrSelf,
		// Token: 0x04002620 RID: 9760
		Ancestor,
		// Token: 0x04002621 RID: 9761
		AncestorOrSelf,
		// Token: 0x04002622 RID: 9762
		Preceding,
		// Token: 0x04002623 RID: 9763
		FollowingSibling,
		// Token: 0x04002624 RID: 9764
		PrecedingSibling,
		// Token: 0x04002625 RID: 9765
		NodeRange,
		// Token: 0x04002626 RID: 9766
		Deref,
		// Token: 0x04002627 RID: 9767
		ElementCtor,
		// Token: 0x04002628 RID: 9768
		AttributeCtor,
		// Token: 0x04002629 RID: 9769
		CommentCtor,
		// Token: 0x0400262A RID: 9770
		PICtor,
		// Token: 0x0400262B RID: 9771
		TextCtor,
		// Token: 0x0400262C RID: 9772
		RawTextCtor,
		// Token: 0x0400262D RID: 9773
		DocumentCtor,
		// Token: 0x0400262E RID: 9774
		NamespaceDecl,
		// Token: 0x0400262F RID: 9775
		RtfCtor,
		// Token: 0x04002630 RID: 9776
		NameOf,
		// Token: 0x04002631 RID: 9777
		LocalNameOf,
		// Token: 0x04002632 RID: 9778
		NamespaceUriOf,
		// Token: 0x04002633 RID: 9779
		PrefixOf,
		// Token: 0x04002634 RID: 9780
		TypeAssert,
		// Token: 0x04002635 RID: 9781
		IsType,
		// Token: 0x04002636 RID: 9782
		IsEmpty,
		// Token: 0x04002637 RID: 9783
		XPathNodeValue,
		// Token: 0x04002638 RID: 9784
		XPathFollowing,
		// Token: 0x04002639 RID: 9785
		XPathPreceding,
		// Token: 0x0400263A RID: 9786
		XPathNamespace,
		// Token: 0x0400263B RID: 9787
		XsltGenerateId,
		// Token: 0x0400263C RID: 9788
		XsltInvokeLateBound,
		// Token: 0x0400263D RID: 9789
		XsltInvokeEarlyBound,
		// Token: 0x0400263E RID: 9790
		XsltCopy,
		// Token: 0x0400263F RID: 9791
		XsltCopyOf,
		// Token: 0x04002640 RID: 9792
		XsltConvert
	}
}

using System;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004AE RID: 1198
	internal enum XmlILOptimization
	{
		// Token: 0x04002514 RID: 9492
		None,
		// Token: 0x04002515 RID: 9493
		EliminateLiteralVariables,
		// Token: 0x04002516 RID: 9494
		TailCall,
		// Token: 0x04002517 RID: 9495
		AnnotateAncestor,
		// Token: 0x04002518 RID: 9496
		AnnotateAncestorSelf,
		// Token: 0x04002519 RID: 9497
		AnnotateAttribute,
		// Token: 0x0400251A RID: 9498
		AnnotateAttrNmspLoop,
		// Token: 0x0400251B RID: 9499
		AnnotateBarrier,
		// Token: 0x0400251C RID: 9500
		AnnotateConstruction,
		// Token: 0x0400251D RID: 9501
		AnnotateContent,
		// Token: 0x0400251E RID: 9502
		AnnotateContentLoop,
		// Token: 0x0400251F RID: 9503
		AnnotateDescendant,
		// Token: 0x04002520 RID: 9504
		AnnotateDescendantLoop,
		// Token: 0x04002521 RID: 9505
		AnnotateDescendantSelf,
		// Token: 0x04002522 RID: 9506
		AnnotateDifference,
		// Token: 0x04002523 RID: 9507
		AnnotateDod,
		// Token: 0x04002524 RID: 9508
		AnnotateDodMerge,
		// Token: 0x04002525 RID: 9509
		AnnotateDodReverse,
		// Token: 0x04002526 RID: 9510
		AnnotateFilter,
		// Token: 0x04002527 RID: 9511
		AnnotateFilterAttributeKind,
		// Token: 0x04002528 RID: 9512
		AnnotateFilterContentKind,
		// Token: 0x04002529 RID: 9513
		AnnotateFilterElements,
		// Token: 0x0400252A RID: 9514
		AnnotateFollowingSibling,
		// Token: 0x0400252B RID: 9515
		AnnotateIndex1,
		// Token: 0x0400252C RID: 9516
		AnnotateIndex2,
		// Token: 0x0400252D RID: 9517
		AnnotateIntersect,
		// Token: 0x0400252E RID: 9518
		AnnotateInvoke,
		// Token: 0x0400252F RID: 9519
		AnnotateJoinAndDod,
		// Token: 0x04002530 RID: 9520
		AnnotateLet,
		// Token: 0x04002531 RID: 9521
		AnnotateMaxLengthEq,
		// Token: 0x04002532 RID: 9522
		AnnotateMaxLengthGe,
		// Token: 0x04002533 RID: 9523
		AnnotateMaxLengthGt,
		// Token: 0x04002534 RID: 9524
		AnnotateMaxLengthLe,
		// Token: 0x04002535 RID: 9525
		AnnotateMaxLengthLt,
		// Token: 0x04002536 RID: 9526
		AnnotateMaxLengthNe,
		// Token: 0x04002537 RID: 9527
		AnnotateMaxPositionEq,
		// Token: 0x04002538 RID: 9528
		AnnotateMaxPositionLe,
		// Token: 0x04002539 RID: 9529
		AnnotateMaxPositionLt,
		// Token: 0x0400253A RID: 9530
		AnnotateNamespace,
		// Token: 0x0400253B RID: 9531
		AnnotateNodeRange,
		// Token: 0x0400253C RID: 9532
		AnnotateParent,
		// Token: 0x0400253D RID: 9533
		AnnotatePositionalIterator,
		// Token: 0x0400253E RID: 9534
		AnnotatePreceding,
		// Token: 0x0400253F RID: 9535
		AnnotatePrecedingSibling,
		// Token: 0x04002540 RID: 9536
		AnnotateRoot,
		// Token: 0x04002541 RID: 9537
		AnnotateRootLoop,
		// Token: 0x04002542 RID: 9538
		AnnotateSingleTextRtf,
		// Token: 0x04002543 RID: 9539
		AnnotateSingletonLoop,
		// Token: 0x04002544 RID: 9540
		AnnotateTrackCallers,
		// Token: 0x04002545 RID: 9541
		AnnotateUnion,
		// Token: 0x04002546 RID: 9542
		AnnotateUnionContent,
		// Token: 0x04002547 RID: 9543
		AnnotateXPathFollowing,
		// Token: 0x04002548 RID: 9544
		AnnotateXPathPreceding,
		// Token: 0x04002549 RID: 9545
		CommuteDodFilter,
		// Token: 0x0400254A RID: 9546
		CommuteFilterLoop,
		// Token: 0x0400254B RID: 9547
		EliminateAdd,
		// Token: 0x0400254C RID: 9548
		EliminateAfter,
		// Token: 0x0400254D RID: 9549
		EliminateAnd,
		// Token: 0x0400254E RID: 9550
		EliminateAverage,
		// Token: 0x0400254F RID: 9551
		EliminateBefore,
		// Token: 0x04002550 RID: 9552
		EliminateConditional,
		// Token: 0x04002551 RID: 9553
		EliminateDifference,
		// Token: 0x04002552 RID: 9554
		EliminateDivide,
		// Token: 0x04002553 RID: 9555
		EliminateDod,
		// Token: 0x04002554 RID: 9556
		EliminateEq,
		// Token: 0x04002555 RID: 9557
		EliminateFilter,
		// Token: 0x04002556 RID: 9558
		EliminateGe,
		// Token: 0x04002557 RID: 9559
		EliminateGt,
		// Token: 0x04002558 RID: 9560
		EliminateIntersection,
		// Token: 0x04002559 RID: 9561
		EliminateIs,
		// Token: 0x0400255A RID: 9562
		EliminateIsEmpty,
		// Token: 0x0400255B RID: 9563
		EliminateIsType,
		// Token: 0x0400255C RID: 9564
		EliminateIterator,
		// Token: 0x0400255D RID: 9565
		EliminateIteratorUsedAtMostOnce,
		// Token: 0x0400255E RID: 9566
		EliminateLe,
		// Token: 0x0400255F RID: 9567
		EliminateLength,
		// Token: 0x04002560 RID: 9568
		EliminateLoop,
		// Token: 0x04002561 RID: 9569
		EliminateLt,
		// Token: 0x04002562 RID: 9570
		EliminateMaximum,
		// Token: 0x04002563 RID: 9571
		EliminateMinimum,
		// Token: 0x04002564 RID: 9572
		EliminateModulo,
		// Token: 0x04002565 RID: 9573
		EliminateMultiply,
		// Token: 0x04002566 RID: 9574
		EliminateNamespaceDecl,
		// Token: 0x04002567 RID: 9575
		EliminateNe,
		// Token: 0x04002568 RID: 9576
		EliminateNegate,
		// Token: 0x04002569 RID: 9577
		EliminateNop,
		// Token: 0x0400256A RID: 9578
		EliminateNot,
		// Token: 0x0400256B RID: 9579
		EliminateOr,
		// Token: 0x0400256C RID: 9580
		EliminatePositionOf,
		// Token: 0x0400256D RID: 9581
		EliminateReturnDod,
		// Token: 0x0400256E RID: 9582
		EliminateSequence,
		// Token: 0x0400256F RID: 9583
		EliminateSort,
		// Token: 0x04002570 RID: 9584
		EliminateStrConcat,
		// Token: 0x04002571 RID: 9585
		EliminateStrConcatSingle,
		// Token: 0x04002572 RID: 9586
		EliminateStrLength,
		// Token: 0x04002573 RID: 9587
		EliminateSubtract,
		// Token: 0x04002574 RID: 9588
		EliminateSum,
		// Token: 0x04002575 RID: 9589
		EliminateTypeAssert,
		// Token: 0x04002576 RID: 9590
		EliminateTypeAssertOptional,
		// Token: 0x04002577 RID: 9591
		EliminateUnion,
		// Token: 0x04002578 RID: 9592
		EliminateUnusedGlobals,
		// Token: 0x04002579 RID: 9593
		EliminateXsltConvert,
		// Token: 0x0400257A RID: 9594
		FoldConditionalNot,
		// Token: 0x0400257B RID: 9595
		FoldNamedDescendants,
		// Token: 0x0400257C RID: 9596
		FoldNone,
		// Token: 0x0400257D RID: 9597
		FoldXsltConvertLiteral,
		// Token: 0x0400257E RID: 9598
		IntroduceDod,
		// Token: 0x0400257F RID: 9599
		IntroducePrecedingDod,
		// Token: 0x04002580 RID: 9600
		NormalizeAddEq,
		// Token: 0x04002581 RID: 9601
		NormalizeAddLiteral,
		// Token: 0x04002582 RID: 9602
		NormalizeAttribute,
		// Token: 0x04002583 RID: 9603
		NormalizeConditionalText,
		// Token: 0x04002584 RID: 9604
		NormalizeDifference,
		// Token: 0x04002585 RID: 9605
		NormalizeEqLiteral,
		// Token: 0x04002586 RID: 9606
		NormalizeGeLiteral,
		// Token: 0x04002587 RID: 9607
		NormalizeGtLiteral,
		// Token: 0x04002588 RID: 9608
		NormalizeIdEq,
		// Token: 0x04002589 RID: 9609
		NormalizeIdNe,
		// Token: 0x0400258A RID: 9610
		NormalizeIntersect,
		// Token: 0x0400258B RID: 9611
		NormalizeInvokeEmpty,
		// Token: 0x0400258C RID: 9612
		NormalizeLeLiteral,
		// Token: 0x0400258D RID: 9613
		NormalizeLengthGt,
		// Token: 0x0400258E RID: 9614
		NormalizeLengthNe,
		// Token: 0x0400258F RID: 9615
		NormalizeLoopConditional,
		// Token: 0x04002590 RID: 9616
		NormalizeLoopInvariant,
		// Token: 0x04002591 RID: 9617
		NormalizeLoopLoop,
		// Token: 0x04002592 RID: 9618
		NormalizeLoopText,
		// Token: 0x04002593 RID: 9619
		NormalizeLtLiteral,
		// Token: 0x04002594 RID: 9620
		NormalizeMuenchian,
		// Token: 0x04002595 RID: 9621
		NormalizeMultiplyLiteral,
		// Token: 0x04002596 RID: 9622
		NormalizeNeLiteral,
		// Token: 0x04002597 RID: 9623
		NormalizeNestedSequences,
		// Token: 0x04002598 RID: 9624
		NormalizeSingletonLet,
		// Token: 0x04002599 RID: 9625
		NormalizeSortXsltConvert,
		// Token: 0x0400259A RID: 9626
		NormalizeUnion,
		// Token: 0x0400259B RID: 9627
		NormalizeXsltConvertEq,
		// Token: 0x0400259C RID: 9628
		NormalizeXsltConvertGe,
		// Token: 0x0400259D RID: 9629
		NormalizeXsltConvertGt,
		// Token: 0x0400259E RID: 9630
		NormalizeXsltConvertLe,
		// Token: 0x0400259F RID: 9631
		NormalizeXsltConvertLt,
		// Token: 0x040025A0 RID: 9632
		NormalizeXsltConvertNe,
		// Token: 0x040025A1 RID: 9633
		Last_
	}
}

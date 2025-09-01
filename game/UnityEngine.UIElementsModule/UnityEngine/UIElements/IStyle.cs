using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029A RID: 666
	public interface IStyle
	{
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600165D RID: 5725
		// (set) Token: 0x0600165E RID: 5726
		StyleEnum<Align> alignContent { get; set; }

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600165F RID: 5727
		// (set) Token: 0x06001660 RID: 5728
		StyleEnum<Align> alignItems { get; set; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001661 RID: 5729
		// (set) Token: 0x06001662 RID: 5730
		StyleEnum<Align> alignSelf { get; set; }

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001663 RID: 5731
		// (set) Token: 0x06001664 RID: 5732
		StyleColor backgroundColor { get; set; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001665 RID: 5733
		// (set) Token: 0x06001666 RID: 5734
		StyleBackground backgroundImage { get; set; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001667 RID: 5735
		// (set) Token: 0x06001668 RID: 5736
		StyleColor borderBottomColor { get; set; }

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001669 RID: 5737
		// (set) Token: 0x0600166A RID: 5738
		StyleLength borderBottomLeftRadius { get; set; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600166B RID: 5739
		// (set) Token: 0x0600166C RID: 5740
		StyleLength borderBottomRightRadius { get; set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600166D RID: 5741
		// (set) Token: 0x0600166E RID: 5742
		StyleFloat borderBottomWidth { get; set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600166F RID: 5743
		// (set) Token: 0x06001670 RID: 5744
		StyleColor borderLeftColor { get; set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001671 RID: 5745
		// (set) Token: 0x06001672 RID: 5746
		StyleFloat borderLeftWidth { get; set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001673 RID: 5747
		// (set) Token: 0x06001674 RID: 5748
		StyleColor borderRightColor { get; set; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001675 RID: 5749
		// (set) Token: 0x06001676 RID: 5750
		StyleFloat borderRightWidth { get; set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001677 RID: 5751
		// (set) Token: 0x06001678 RID: 5752
		StyleColor borderTopColor { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001679 RID: 5753
		// (set) Token: 0x0600167A RID: 5754
		StyleLength borderTopLeftRadius { get; set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600167B RID: 5755
		// (set) Token: 0x0600167C RID: 5756
		StyleLength borderTopRightRadius { get; set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600167D RID: 5757
		// (set) Token: 0x0600167E RID: 5758
		StyleFloat borderTopWidth { get; set; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600167F RID: 5759
		// (set) Token: 0x06001680 RID: 5760
		StyleLength bottom { get; set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001681 RID: 5761
		// (set) Token: 0x06001682 RID: 5762
		StyleColor color { get; set; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001683 RID: 5763
		// (set) Token: 0x06001684 RID: 5764
		StyleCursor cursor { get; set; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001685 RID: 5765
		// (set) Token: 0x06001686 RID: 5766
		StyleEnum<DisplayStyle> display { get; set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001687 RID: 5767
		// (set) Token: 0x06001688 RID: 5768
		StyleLength flexBasis { get; set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001689 RID: 5769
		// (set) Token: 0x0600168A RID: 5770
		StyleEnum<FlexDirection> flexDirection { get; set; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600168B RID: 5771
		// (set) Token: 0x0600168C RID: 5772
		StyleFloat flexGrow { get; set; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600168D RID: 5773
		// (set) Token: 0x0600168E RID: 5774
		StyleFloat flexShrink { get; set; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600168F RID: 5775
		// (set) Token: 0x06001690 RID: 5776
		StyleEnum<Wrap> flexWrap { get; set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001691 RID: 5777
		// (set) Token: 0x06001692 RID: 5778
		StyleLength fontSize { get; set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001693 RID: 5779
		// (set) Token: 0x06001694 RID: 5780
		StyleLength height { get; set; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001695 RID: 5781
		// (set) Token: 0x06001696 RID: 5782
		StyleEnum<Justify> justifyContent { get; set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001697 RID: 5783
		// (set) Token: 0x06001698 RID: 5784
		StyleLength left { get; set; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001699 RID: 5785
		// (set) Token: 0x0600169A RID: 5786
		StyleLength letterSpacing { get; set; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600169B RID: 5787
		// (set) Token: 0x0600169C RID: 5788
		StyleLength marginBottom { get; set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600169D RID: 5789
		// (set) Token: 0x0600169E RID: 5790
		StyleLength marginLeft { get; set; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600169F RID: 5791
		// (set) Token: 0x060016A0 RID: 5792
		StyleLength marginRight { get; set; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060016A1 RID: 5793
		// (set) Token: 0x060016A2 RID: 5794
		StyleLength marginTop { get; set; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060016A3 RID: 5795
		// (set) Token: 0x060016A4 RID: 5796
		StyleLength maxHeight { get; set; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060016A5 RID: 5797
		// (set) Token: 0x060016A6 RID: 5798
		StyleLength maxWidth { get; set; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060016A7 RID: 5799
		// (set) Token: 0x060016A8 RID: 5800
		StyleLength minHeight { get; set; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060016A9 RID: 5801
		// (set) Token: 0x060016AA RID: 5802
		StyleLength minWidth { get; set; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060016AB RID: 5803
		// (set) Token: 0x060016AC RID: 5804
		StyleFloat opacity { get; set; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060016AD RID: 5805
		// (set) Token: 0x060016AE RID: 5806
		StyleEnum<Overflow> overflow { get; set; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060016AF RID: 5807
		// (set) Token: 0x060016B0 RID: 5808
		StyleLength paddingBottom { get; set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060016B1 RID: 5809
		// (set) Token: 0x060016B2 RID: 5810
		StyleLength paddingLeft { get; set; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060016B3 RID: 5811
		// (set) Token: 0x060016B4 RID: 5812
		StyleLength paddingRight { get; set; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060016B5 RID: 5813
		// (set) Token: 0x060016B6 RID: 5814
		StyleLength paddingTop { get; set; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060016B7 RID: 5815
		// (set) Token: 0x060016B8 RID: 5816
		StyleEnum<Position> position { get; set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060016B9 RID: 5817
		// (set) Token: 0x060016BA RID: 5818
		StyleLength right { get; set; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060016BB RID: 5819
		// (set) Token: 0x060016BC RID: 5820
		StyleRotate rotate { get; set; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060016BD RID: 5821
		// (set) Token: 0x060016BE RID: 5822
		StyleScale scale { get; set; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060016BF RID: 5823
		// (set) Token: 0x060016C0 RID: 5824
		StyleEnum<TextOverflow> textOverflow { get; set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060016C1 RID: 5825
		// (set) Token: 0x060016C2 RID: 5826
		StyleTextShadow textShadow { get; set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060016C3 RID: 5827
		// (set) Token: 0x060016C4 RID: 5828
		StyleLength top { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060016C5 RID: 5829
		// (set) Token: 0x060016C6 RID: 5830
		StyleTransformOrigin transformOrigin { get; set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060016C7 RID: 5831
		// (set) Token: 0x060016C8 RID: 5832
		StyleList<TimeValue> transitionDelay { get; set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060016C9 RID: 5833
		// (set) Token: 0x060016CA RID: 5834
		StyleList<TimeValue> transitionDuration { get; set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060016CB RID: 5835
		// (set) Token: 0x060016CC RID: 5836
		StyleList<StylePropertyName> transitionProperty { get; set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060016CD RID: 5837
		// (set) Token: 0x060016CE RID: 5838
		StyleList<EasingFunction> transitionTimingFunction { get; set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060016CF RID: 5839
		// (set) Token: 0x060016D0 RID: 5840
		StyleTranslate translate { get; set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060016D1 RID: 5841
		// (set) Token: 0x060016D2 RID: 5842
		StyleColor unityBackgroundImageTintColor { get; set; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060016D3 RID: 5843
		// (set) Token: 0x060016D4 RID: 5844
		StyleEnum<ScaleMode> unityBackgroundScaleMode { get; set; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060016D5 RID: 5845
		// (set) Token: 0x060016D6 RID: 5846
		StyleFont unityFont { get; set; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060016D7 RID: 5847
		// (set) Token: 0x060016D8 RID: 5848
		StyleFontDefinition unityFontDefinition { get; set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060016D9 RID: 5849
		// (set) Token: 0x060016DA RID: 5850
		StyleEnum<FontStyle> unityFontStyleAndWeight { get; set; }

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060016DB RID: 5851
		// (set) Token: 0x060016DC RID: 5852
		StyleEnum<OverflowClipBox> unityOverflowClipBox { get; set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060016DD RID: 5853
		// (set) Token: 0x060016DE RID: 5854
		StyleLength unityParagraphSpacing { get; set; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060016DF RID: 5855
		// (set) Token: 0x060016E0 RID: 5856
		StyleInt unitySliceBottom { get; set; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060016E1 RID: 5857
		// (set) Token: 0x060016E2 RID: 5858
		StyleInt unitySliceLeft { get; set; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060016E3 RID: 5859
		// (set) Token: 0x060016E4 RID: 5860
		StyleInt unitySliceRight { get; set; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060016E5 RID: 5861
		// (set) Token: 0x060016E6 RID: 5862
		StyleInt unitySliceTop { get; set; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060016E7 RID: 5863
		// (set) Token: 0x060016E8 RID: 5864
		StyleEnum<TextAnchor> unityTextAlign { get; set; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060016E9 RID: 5865
		// (set) Token: 0x060016EA RID: 5866
		StyleColor unityTextOutlineColor { get; set; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060016EB RID: 5867
		// (set) Token: 0x060016EC RID: 5868
		StyleFloat unityTextOutlineWidth { get; set; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060016ED RID: 5869
		// (set) Token: 0x060016EE RID: 5870
		StyleEnum<TextOverflowPosition> unityTextOverflowPosition { get; set; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060016EF RID: 5871
		// (set) Token: 0x060016F0 RID: 5872
		StyleEnum<Visibility> visibility { get; set; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060016F1 RID: 5873
		// (set) Token: 0x060016F2 RID: 5874
		StyleEnum<WhiteSpace> whiteSpace { get; set; }

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060016F3 RID: 5875
		// (set) Token: 0x060016F4 RID: 5876
		StyleLength width { get; set; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060016F5 RID: 5877
		// (set) Token: 0x060016F6 RID: 5878
		StyleLength wordSpacing { get; set; }
	}
}

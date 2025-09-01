using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000299 RID: 665
	public interface IResolvedStyle
	{
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001614 RID: 5652
		Align alignContent { get; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001615 RID: 5653
		Align alignItems { get; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001616 RID: 5654
		Align alignSelf { get; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001617 RID: 5655
		Color backgroundColor { get; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001618 RID: 5656
		Background backgroundImage { get; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001619 RID: 5657
		Color borderBottomColor { get; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600161A RID: 5658
		float borderBottomLeftRadius { get; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600161B RID: 5659
		float borderBottomRightRadius { get; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600161C RID: 5660
		float borderBottomWidth { get; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600161D RID: 5661
		Color borderLeftColor { get; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600161E RID: 5662
		float borderLeftWidth { get; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600161F RID: 5663
		Color borderRightColor { get; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001620 RID: 5664
		float borderRightWidth { get; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001621 RID: 5665
		Color borderTopColor { get; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001622 RID: 5666
		float borderTopLeftRadius { get; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001623 RID: 5667
		float borderTopRightRadius { get; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001624 RID: 5668
		float borderTopWidth { get; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001625 RID: 5669
		float bottom { get; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001626 RID: 5670
		Color color { get; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001627 RID: 5671
		DisplayStyle display { get; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001628 RID: 5672
		StyleFloat flexBasis { get; }

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001629 RID: 5673
		FlexDirection flexDirection { get; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600162A RID: 5674
		float flexGrow { get; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600162B RID: 5675
		float flexShrink { get; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600162C RID: 5676
		Wrap flexWrap { get; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600162D RID: 5677
		float fontSize { get; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600162E RID: 5678
		float height { get; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600162F RID: 5679
		Justify justifyContent { get; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001630 RID: 5680
		float left { get; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001631 RID: 5681
		float letterSpacing { get; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001632 RID: 5682
		float marginBottom { get; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001633 RID: 5683
		float marginLeft { get; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001634 RID: 5684
		float marginRight { get; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001635 RID: 5685
		float marginTop { get; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001636 RID: 5686
		StyleFloat maxHeight { get; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001637 RID: 5687
		StyleFloat maxWidth { get; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001638 RID: 5688
		StyleFloat minHeight { get; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001639 RID: 5689
		StyleFloat minWidth { get; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x0600163A RID: 5690
		float opacity { get; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600163B RID: 5691
		float paddingBottom { get; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600163C RID: 5692
		float paddingLeft { get; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600163D RID: 5693
		float paddingRight { get; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600163E RID: 5694
		float paddingTop { get; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600163F RID: 5695
		Position position { get; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001640 RID: 5696
		float right { get; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001641 RID: 5697
		Rotate rotate { get; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001642 RID: 5698
		Scale scale { get; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001643 RID: 5699
		TextOverflow textOverflow { get; }

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001644 RID: 5700
		float top { get; }

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001645 RID: 5701
		Vector3 transformOrigin { get; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001646 RID: 5702
		IEnumerable<TimeValue> transitionDelay { get; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001647 RID: 5703
		IEnumerable<TimeValue> transitionDuration { get; }

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001648 RID: 5704
		IEnumerable<StylePropertyName> transitionProperty { get; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001649 RID: 5705
		IEnumerable<EasingFunction> transitionTimingFunction { get; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600164A RID: 5706
		Vector3 translate { get; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600164B RID: 5707
		Color unityBackgroundImageTintColor { get; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600164C RID: 5708
		ScaleMode unityBackgroundScaleMode { get; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600164D RID: 5709
		Font unityFont { get; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600164E RID: 5710
		FontDefinition unityFontDefinition { get; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600164F RID: 5711
		FontStyle unityFontStyleAndWeight { get; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001650 RID: 5712
		float unityParagraphSpacing { get; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001651 RID: 5713
		int unitySliceBottom { get; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001652 RID: 5714
		int unitySliceLeft { get; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001653 RID: 5715
		int unitySliceRight { get; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001654 RID: 5716
		int unitySliceTop { get; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001655 RID: 5717
		TextAnchor unityTextAlign { get; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001656 RID: 5718
		Color unityTextOutlineColor { get; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001657 RID: 5719
		float unityTextOutlineWidth { get; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001658 RID: 5720
		TextOverflowPosition unityTextOverflowPosition { get; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001659 RID: 5721
		Visibility visibility { get; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600165A RID: 5722
		WhiteSpace whiteSpace { get; }

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600165B RID: 5723
		float width { get; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600165C RID: 5724
		float wordSpacing { get; }
	}
}

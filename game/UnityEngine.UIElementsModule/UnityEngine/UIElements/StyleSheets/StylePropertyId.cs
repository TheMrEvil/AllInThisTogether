using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200035C RID: 860
	internal enum StylePropertyId
	{
		// Token: 0x04000D86 RID: 3462
		Unknown,
		// Token: 0x04000D87 RID: 3463
		Custom = -1,
		// Token: 0x04000D88 RID: 3464
		AlignContent = 131072,
		// Token: 0x04000D89 RID: 3465
		AlignItems,
		// Token: 0x04000D8A RID: 3466
		AlignSelf,
		// Token: 0x04000D8B RID: 3467
		All = 262144,
		// Token: 0x04000D8C RID: 3468
		BackgroundColor = 458752,
		// Token: 0x04000D8D RID: 3469
		BackgroundImage,
		// Token: 0x04000D8E RID: 3470
		BorderBottomColor,
		// Token: 0x04000D8F RID: 3471
		BorderBottomLeftRadius,
		// Token: 0x04000D90 RID: 3472
		BorderBottomRightRadius,
		// Token: 0x04000D91 RID: 3473
		BorderBottomWidth = 131075,
		// Token: 0x04000D92 RID: 3474
		BorderColor = 262145,
		// Token: 0x04000D93 RID: 3475
		BorderLeftColor = 458757,
		// Token: 0x04000D94 RID: 3476
		BorderLeftWidth = 131076,
		// Token: 0x04000D95 RID: 3477
		BorderRadius = 262146,
		// Token: 0x04000D96 RID: 3478
		BorderRightColor = 458758,
		// Token: 0x04000D97 RID: 3479
		BorderRightWidth = 131077,
		// Token: 0x04000D98 RID: 3480
		BorderTopColor = 458759,
		// Token: 0x04000D99 RID: 3481
		BorderTopLeftRadius,
		// Token: 0x04000D9A RID: 3482
		BorderTopRightRadius,
		// Token: 0x04000D9B RID: 3483
		BorderTopWidth = 131078,
		// Token: 0x04000D9C RID: 3484
		BorderWidth = 262147,
		// Token: 0x04000D9D RID: 3485
		Bottom = 131079,
		// Token: 0x04000D9E RID: 3486
		Color = 65536,
		// Token: 0x04000D9F RID: 3487
		Cursor = 196608,
		// Token: 0x04000DA0 RID: 3488
		Display = 131080,
		// Token: 0x04000DA1 RID: 3489
		Flex = 262148,
		// Token: 0x04000DA2 RID: 3490
		FlexBasis = 131081,
		// Token: 0x04000DA3 RID: 3491
		FlexDirection,
		// Token: 0x04000DA4 RID: 3492
		FlexGrow,
		// Token: 0x04000DA5 RID: 3493
		FlexShrink,
		// Token: 0x04000DA6 RID: 3494
		FlexWrap,
		// Token: 0x04000DA7 RID: 3495
		FontSize = 65537,
		// Token: 0x04000DA8 RID: 3496
		Height = 131086,
		// Token: 0x04000DA9 RID: 3497
		JustifyContent,
		// Token: 0x04000DAA RID: 3498
		Left,
		// Token: 0x04000DAB RID: 3499
		LetterSpacing = 65538,
		// Token: 0x04000DAC RID: 3500
		Margin = 262149,
		// Token: 0x04000DAD RID: 3501
		MarginBottom = 131089,
		// Token: 0x04000DAE RID: 3502
		MarginLeft,
		// Token: 0x04000DAF RID: 3503
		MarginRight,
		// Token: 0x04000DB0 RID: 3504
		MarginTop,
		// Token: 0x04000DB1 RID: 3505
		MaxHeight,
		// Token: 0x04000DB2 RID: 3506
		MaxWidth,
		// Token: 0x04000DB3 RID: 3507
		MinHeight,
		// Token: 0x04000DB4 RID: 3508
		MinWidth,
		// Token: 0x04000DB5 RID: 3509
		Opacity = 458762,
		// Token: 0x04000DB6 RID: 3510
		Overflow,
		// Token: 0x04000DB7 RID: 3511
		Padding = 262150,
		// Token: 0x04000DB8 RID: 3512
		PaddingBottom = 131097,
		// Token: 0x04000DB9 RID: 3513
		PaddingLeft,
		// Token: 0x04000DBA RID: 3514
		PaddingRight,
		// Token: 0x04000DBB RID: 3515
		PaddingTop,
		// Token: 0x04000DBC RID: 3516
		Position,
		// Token: 0x04000DBD RID: 3517
		Right,
		// Token: 0x04000DBE RID: 3518
		Rotate = 327680,
		// Token: 0x04000DBF RID: 3519
		Scale,
		// Token: 0x04000DC0 RID: 3520
		TextOverflow = 196609,
		// Token: 0x04000DC1 RID: 3521
		TextShadow = 65539,
		// Token: 0x04000DC2 RID: 3522
		Top = 131103,
		// Token: 0x04000DC3 RID: 3523
		TransformOrigin = 327682,
		// Token: 0x04000DC4 RID: 3524
		Transition = 262151,
		// Token: 0x04000DC5 RID: 3525
		TransitionDelay = 393216,
		// Token: 0x04000DC6 RID: 3526
		TransitionDuration,
		// Token: 0x04000DC7 RID: 3527
		TransitionProperty,
		// Token: 0x04000DC8 RID: 3528
		TransitionTimingFunction,
		// Token: 0x04000DC9 RID: 3529
		Translate = 327683,
		// Token: 0x04000DCA RID: 3530
		UnityBackgroundImageTintColor = 196610,
		// Token: 0x04000DCB RID: 3531
		UnityBackgroundScaleMode,
		// Token: 0x04000DCC RID: 3532
		UnityFont = 65540,
		// Token: 0x04000DCD RID: 3533
		UnityFontDefinition,
		// Token: 0x04000DCE RID: 3534
		UnityFontStyleAndWeight,
		// Token: 0x04000DCF RID: 3535
		UnityOverflowClipBox = 196612,
		// Token: 0x04000DD0 RID: 3536
		UnityParagraphSpacing = 65543,
		// Token: 0x04000DD1 RID: 3537
		UnitySliceBottom = 196613,
		// Token: 0x04000DD2 RID: 3538
		UnitySliceLeft,
		// Token: 0x04000DD3 RID: 3539
		UnitySliceRight,
		// Token: 0x04000DD4 RID: 3540
		UnitySliceTop,
		// Token: 0x04000DD5 RID: 3541
		UnityTextAlign = 65544,
		// Token: 0x04000DD6 RID: 3542
		UnityTextOutline = 262152,
		// Token: 0x04000DD7 RID: 3543
		UnityTextOutlineColor = 65545,
		// Token: 0x04000DD8 RID: 3544
		UnityTextOutlineWidth,
		// Token: 0x04000DD9 RID: 3545
		UnityTextOverflowPosition = 196617,
		// Token: 0x04000DDA RID: 3546
		Visibility = 65547,
		// Token: 0x04000DDB RID: 3547
		WhiteSpace,
		// Token: 0x04000DDC RID: 3548
		Width = 131104,
		// Token: 0x04000DDD RID: 3549
		WordSpacing = 65549
	}
}

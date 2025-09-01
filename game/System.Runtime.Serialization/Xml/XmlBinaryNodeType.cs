using System;

namespace System.Xml
{
	// Token: 0x02000048 RID: 72
	internal enum XmlBinaryNodeType
	{
		// Token: 0x0400014C RID: 332
		EndElement = 1,
		// Token: 0x0400014D RID: 333
		Comment,
		// Token: 0x0400014E RID: 334
		Array,
		// Token: 0x0400014F RID: 335
		MinAttribute,
		// Token: 0x04000150 RID: 336
		ShortAttribute = 4,
		// Token: 0x04000151 RID: 337
		Attribute,
		// Token: 0x04000152 RID: 338
		ShortDictionaryAttribute,
		// Token: 0x04000153 RID: 339
		DictionaryAttribute,
		// Token: 0x04000154 RID: 340
		ShortXmlnsAttribute,
		// Token: 0x04000155 RID: 341
		XmlnsAttribute,
		// Token: 0x04000156 RID: 342
		ShortDictionaryXmlnsAttribute,
		// Token: 0x04000157 RID: 343
		DictionaryXmlnsAttribute,
		// Token: 0x04000158 RID: 344
		PrefixDictionaryAttributeA,
		// Token: 0x04000159 RID: 345
		PrefixDictionaryAttributeB,
		// Token: 0x0400015A RID: 346
		PrefixDictionaryAttributeC,
		// Token: 0x0400015B RID: 347
		PrefixDictionaryAttributeD,
		// Token: 0x0400015C RID: 348
		PrefixDictionaryAttributeE,
		// Token: 0x0400015D RID: 349
		PrefixDictionaryAttributeF,
		// Token: 0x0400015E RID: 350
		PrefixDictionaryAttributeG,
		// Token: 0x0400015F RID: 351
		PrefixDictionaryAttributeH,
		// Token: 0x04000160 RID: 352
		PrefixDictionaryAttributeI,
		// Token: 0x04000161 RID: 353
		PrefixDictionaryAttributeJ,
		// Token: 0x04000162 RID: 354
		PrefixDictionaryAttributeK,
		// Token: 0x04000163 RID: 355
		PrefixDictionaryAttributeL,
		// Token: 0x04000164 RID: 356
		PrefixDictionaryAttributeM,
		// Token: 0x04000165 RID: 357
		PrefixDictionaryAttributeN,
		// Token: 0x04000166 RID: 358
		PrefixDictionaryAttributeO,
		// Token: 0x04000167 RID: 359
		PrefixDictionaryAttributeP,
		// Token: 0x04000168 RID: 360
		PrefixDictionaryAttributeQ,
		// Token: 0x04000169 RID: 361
		PrefixDictionaryAttributeR,
		// Token: 0x0400016A RID: 362
		PrefixDictionaryAttributeS,
		// Token: 0x0400016B RID: 363
		PrefixDictionaryAttributeT,
		// Token: 0x0400016C RID: 364
		PrefixDictionaryAttributeU,
		// Token: 0x0400016D RID: 365
		PrefixDictionaryAttributeV,
		// Token: 0x0400016E RID: 366
		PrefixDictionaryAttributeW,
		// Token: 0x0400016F RID: 367
		PrefixDictionaryAttributeX,
		// Token: 0x04000170 RID: 368
		PrefixDictionaryAttributeY,
		// Token: 0x04000171 RID: 369
		PrefixDictionaryAttributeZ,
		// Token: 0x04000172 RID: 370
		PrefixAttributeA,
		// Token: 0x04000173 RID: 371
		PrefixAttributeB,
		// Token: 0x04000174 RID: 372
		PrefixAttributeC,
		// Token: 0x04000175 RID: 373
		PrefixAttributeD,
		// Token: 0x04000176 RID: 374
		PrefixAttributeE,
		// Token: 0x04000177 RID: 375
		PrefixAttributeF,
		// Token: 0x04000178 RID: 376
		PrefixAttributeG,
		// Token: 0x04000179 RID: 377
		PrefixAttributeH,
		// Token: 0x0400017A RID: 378
		PrefixAttributeI,
		// Token: 0x0400017B RID: 379
		PrefixAttributeJ,
		// Token: 0x0400017C RID: 380
		PrefixAttributeK,
		// Token: 0x0400017D RID: 381
		PrefixAttributeL,
		// Token: 0x0400017E RID: 382
		PrefixAttributeM,
		// Token: 0x0400017F RID: 383
		PrefixAttributeN,
		// Token: 0x04000180 RID: 384
		PrefixAttributeO,
		// Token: 0x04000181 RID: 385
		PrefixAttributeP,
		// Token: 0x04000182 RID: 386
		PrefixAttributeQ,
		// Token: 0x04000183 RID: 387
		PrefixAttributeR,
		// Token: 0x04000184 RID: 388
		PrefixAttributeS,
		// Token: 0x04000185 RID: 389
		PrefixAttributeT,
		// Token: 0x04000186 RID: 390
		PrefixAttributeU,
		// Token: 0x04000187 RID: 391
		PrefixAttributeV,
		// Token: 0x04000188 RID: 392
		PrefixAttributeW,
		// Token: 0x04000189 RID: 393
		PrefixAttributeX,
		// Token: 0x0400018A RID: 394
		PrefixAttributeY,
		// Token: 0x0400018B RID: 395
		PrefixAttributeZ,
		// Token: 0x0400018C RID: 396
		MaxAttribute = 63,
		// Token: 0x0400018D RID: 397
		MinElement,
		// Token: 0x0400018E RID: 398
		ShortElement = 64,
		// Token: 0x0400018F RID: 399
		Element,
		// Token: 0x04000190 RID: 400
		ShortDictionaryElement,
		// Token: 0x04000191 RID: 401
		DictionaryElement,
		// Token: 0x04000192 RID: 402
		PrefixDictionaryElementA,
		// Token: 0x04000193 RID: 403
		PrefixDictionaryElementB,
		// Token: 0x04000194 RID: 404
		PrefixDictionaryElementC,
		// Token: 0x04000195 RID: 405
		PrefixDictionaryElementD,
		// Token: 0x04000196 RID: 406
		PrefixDictionaryElementE,
		// Token: 0x04000197 RID: 407
		PrefixDictionaryElementF,
		// Token: 0x04000198 RID: 408
		PrefixDictionaryElementG,
		// Token: 0x04000199 RID: 409
		PrefixDictionaryElementH,
		// Token: 0x0400019A RID: 410
		PrefixDictionaryElementI,
		// Token: 0x0400019B RID: 411
		PrefixDictionaryElementJ,
		// Token: 0x0400019C RID: 412
		PrefixDictionaryElementK,
		// Token: 0x0400019D RID: 413
		PrefixDictionaryElementL,
		// Token: 0x0400019E RID: 414
		PrefixDictionaryElementM,
		// Token: 0x0400019F RID: 415
		PrefixDictionaryElementN,
		// Token: 0x040001A0 RID: 416
		PrefixDictionaryElementO,
		// Token: 0x040001A1 RID: 417
		PrefixDictionaryElementP,
		// Token: 0x040001A2 RID: 418
		PrefixDictionaryElementQ,
		// Token: 0x040001A3 RID: 419
		PrefixDictionaryElementR,
		// Token: 0x040001A4 RID: 420
		PrefixDictionaryElementS,
		// Token: 0x040001A5 RID: 421
		PrefixDictionaryElementT,
		// Token: 0x040001A6 RID: 422
		PrefixDictionaryElementU,
		// Token: 0x040001A7 RID: 423
		PrefixDictionaryElementV,
		// Token: 0x040001A8 RID: 424
		PrefixDictionaryElementW,
		// Token: 0x040001A9 RID: 425
		PrefixDictionaryElementX,
		// Token: 0x040001AA RID: 426
		PrefixDictionaryElementY,
		// Token: 0x040001AB RID: 427
		PrefixDictionaryElementZ,
		// Token: 0x040001AC RID: 428
		PrefixElementA,
		// Token: 0x040001AD RID: 429
		PrefixElementB,
		// Token: 0x040001AE RID: 430
		PrefixElementC,
		// Token: 0x040001AF RID: 431
		PrefixElementD,
		// Token: 0x040001B0 RID: 432
		PrefixElementE,
		// Token: 0x040001B1 RID: 433
		PrefixElementF,
		// Token: 0x040001B2 RID: 434
		PrefixElementG,
		// Token: 0x040001B3 RID: 435
		PrefixElementH,
		// Token: 0x040001B4 RID: 436
		PrefixElementI,
		// Token: 0x040001B5 RID: 437
		PrefixElementJ,
		// Token: 0x040001B6 RID: 438
		PrefixElementK,
		// Token: 0x040001B7 RID: 439
		PrefixElementL,
		// Token: 0x040001B8 RID: 440
		PrefixElementM,
		// Token: 0x040001B9 RID: 441
		PrefixElementN,
		// Token: 0x040001BA RID: 442
		PrefixElementO,
		// Token: 0x040001BB RID: 443
		PrefixElementP,
		// Token: 0x040001BC RID: 444
		PrefixElementQ,
		// Token: 0x040001BD RID: 445
		PrefixElementR,
		// Token: 0x040001BE RID: 446
		PrefixElementS,
		// Token: 0x040001BF RID: 447
		PrefixElementT,
		// Token: 0x040001C0 RID: 448
		PrefixElementU,
		// Token: 0x040001C1 RID: 449
		PrefixElementV,
		// Token: 0x040001C2 RID: 450
		PrefixElementW,
		// Token: 0x040001C3 RID: 451
		PrefixElementX,
		// Token: 0x040001C4 RID: 452
		PrefixElementY,
		// Token: 0x040001C5 RID: 453
		PrefixElementZ,
		// Token: 0x040001C6 RID: 454
		MaxElement = 119,
		// Token: 0x040001C7 RID: 455
		MinText = 128,
		// Token: 0x040001C8 RID: 456
		ZeroText = 128,
		// Token: 0x040001C9 RID: 457
		OneText = 130,
		// Token: 0x040001CA RID: 458
		FalseText = 132,
		// Token: 0x040001CB RID: 459
		TrueText = 134,
		// Token: 0x040001CC RID: 460
		Int8Text = 136,
		// Token: 0x040001CD RID: 461
		Int16Text = 138,
		// Token: 0x040001CE RID: 462
		Int32Text = 140,
		// Token: 0x040001CF RID: 463
		Int64Text = 142,
		// Token: 0x040001D0 RID: 464
		FloatText = 144,
		// Token: 0x040001D1 RID: 465
		DoubleText = 146,
		// Token: 0x040001D2 RID: 466
		DecimalText = 148,
		// Token: 0x040001D3 RID: 467
		DateTimeText = 150,
		// Token: 0x040001D4 RID: 468
		Chars8Text = 152,
		// Token: 0x040001D5 RID: 469
		Chars16Text = 154,
		// Token: 0x040001D6 RID: 470
		Chars32Text = 156,
		// Token: 0x040001D7 RID: 471
		Bytes8Text = 158,
		// Token: 0x040001D8 RID: 472
		Bytes16Text = 160,
		// Token: 0x040001D9 RID: 473
		Bytes32Text = 162,
		// Token: 0x040001DA RID: 474
		StartListText = 164,
		// Token: 0x040001DB RID: 475
		EndListText = 166,
		// Token: 0x040001DC RID: 476
		EmptyText = 168,
		// Token: 0x040001DD RID: 477
		DictionaryText = 170,
		// Token: 0x040001DE RID: 478
		UniqueIdText = 172,
		// Token: 0x040001DF RID: 479
		TimeSpanText = 174,
		// Token: 0x040001E0 RID: 480
		GuidText = 176,
		// Token: 0x040001E1 RID: 481
		UInt64Text = 178,
		// Token: 0x040001E2 RID: 482
		BoolText = 180,
		// Token: 0x040001E3 RID: 483
		UnicodeChars8Text = 182,
		// Token: 0x040001E4 RID: 484
		UnicodeChars16Text = 184,
		// Token: 0x040001E5 RID: 485
		UnicodeChars32Text = 186,
		// Token: 0x040001E6 RID: 486
		QNameDictionaryText = 188,
		// Token: 0x040001E7 RID: 487
		ZeroTextWithEndElement = 129,
		// Token: 0x040001E8 RID: 488
		OneTextWithEndElement = 131,
		// Token: 0x040001E9 RID: 489
		FalseTextWithEndElement = 133,
		// Token: 0x040001EA RID: 490
		TrueTextWithEndElement = 135,
		// Token: 0x040001EB RID: 491
		Int8TextWithEndElement = 137,
		// Token: 0x040001EC RID: 492
		Int16TextWithEndElement = 139,
		// Token: 0x040001ED RID: 493
		Int32TextWithEndElement = 141,
		// Token: 0x040001EE RID: 494
		Int64TextWithEndElement = 143,
		// Token: 0x040001EF RID: 495
		FloatTextWithEndElement = 145,
		// Token: 0x040001F0 RID: 496
		DoubleTextWithEndElement = 147,
		// Token: 0x040001F1 RID: 497
		DecimalTextWithEndElement = 149,
		// Token: 0x040001F2 RID: 498
		DateTimeTextWithEndElement = 151,
		// Token: 0x040001F3 RID: 499
		Chars8TextWithEndElement = 153,
		// Token: 0x040001F4 RID: 500
		Chars16TextWithEndElement = 155,
		// Token: 0x040001F5 RID: 501
		Chars32TextWithEndElement = 157,
		// Token: 0x040001F6 RID: 502
		Bytes8TextWithEndElement = 159,
		// Token: 0x040001F7 RID: 503
		Bytes16TextWithEndElement = 161,
		// Token: 0x040001F8 RID: 504
		Bytes32TextWithEndElement = 163,
		// Token: 0x040001F9 RID: 505
		StartListTextWithEndElement = 165,
		// Token: 0x040001FA RID: 506
		EndListTextWithEndElement = 167,
		// Token: 0x040001FB RID: 507
		EmptyTextWithEndElement = 169,
		// Token: 0x040001FC RID: 508
		DictionaryTextWithEndElement = 171,
		// Token: 0x040001FD RID: 509
		UniqueIdTextWithEndElement = 173,
		// Token: 0x040001FE RID: 510
		TimeSpanTextWithEndElement = 175,
		// Token: 0x040001FF RID: 511
		GuidTextWithEndElement = 177,
		// Token: 0x04000200 RID: 512
		UInt64TextWithEndElement = 179,
		// Token: 0x04000201 RID: 513
		BoolTextWithEndElement = 181,
		// Token: 0x04000202 RID: 514
		UnicodeChars8TextWithEndElement = 183,
		// Token: 0x04000203 RID: 515
		UnicodeChars16TextWithEndElement = 185,
		// Token: 0x04000204 RID: 516
		UnicodeChars32TextWithEndElement = 187,
		// Token: 0x04000205 RID: 517
		QNameDictionaryTextWithEndElement = 189,
		// Token: 0x04000206 RID: 518
		MaxText = 189
	}
}

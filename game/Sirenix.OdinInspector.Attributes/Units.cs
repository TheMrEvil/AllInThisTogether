using System;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200007A RID: 122
	public enum Units
	{
		// Token: 0x04000161 RID: 353
		Unset = -1,
		// Token: 0x04000162 RID: 354
		Nanometer,
		// Token: 0x04000163 RID: 355
		Micrometer,
		// Token: 0x04000164 RID: 356
		Millimeter,
		// Token: 0x04000165 RID: 357
		Centimeter,
		// Token: 0x04000166 RID: 358
		Meter,
		// Token: 0x04000167 RID: 359
		Kilometer,
		// Token: 0x04000168 RID: 360
		Inch,
		// Token: 0x04000169 RID: 361
		Feet,
		// Token: 0x0400016A RID: 362
		Mile,
		// Token: 0x0400016B RID: 363
		Yard,
		// Token: 0x0400016C RID: 364
		NauticalMile,
		// Token: 0x0400016D RID: 365
		LightYear,
		// Token: 0x0400016E RID: 366
		Parsec,
		// Token: 0x0400016F RID: 367
		AstronomicalUnit,
		// Token: 0x04000170 RID: 368
		CubicMeter,
		// Token: 0x04000171 RID: 369
		CubicKilometer,
		// Token: 0x04000172 RID: 370
		CubicCentimeter,
		// Token: 0x04000173 RID: 371
		CubicMillimeter,
		// Token: 0x04000174 RID: 372
		Liter,
		// Token: 0x04000175 RID: 373
		Milliliter,
		// Token: 0x04000176 RID: 374
		Centiliter,
		// Token: 0x04000177 RID: 375
		Deciliter,
		// Token: 0x04000178 RID: 376
		Hectoliter,
		// Token: 0x04000179 RID: 377
		CubicInch,
		// Token: 0x0400017A RID: 378
		CubicFeet,
		// Token: 0x0400017B RID: 379
		CubicYard,
		// Token: 0x0400017C RID: 380
		AcreFeet,
		// Token: 0x0400017D RID: 381
		BarrelOil,
		// Token: 0x0400017E RID: 382
		TeaspoonUS,
		// Token: 0x0400017F RID: 383
		TablespoonUS,
		// Token: 0x04000180 RID: 384
		CupUS,
		// Token: 0x04000181 RID: 385
		GillUS,
		// Token: 0x04000182 RID: 386
		PintUS,
		// Token: 0x04000183 RID: 387
		QuartUS,
		// Token: 0x04000184 RID: 388
		GallonUS,
		// Token: 0x04000185 RID: 389
		BarrelUS,
		// Token: 0x04000186 RID: 390
		FluidOunceUS,
		// Token: 0x04000187 RID: 391
		BarrelUK,
		// Token: 0x04000188 RID: 392
		FluidOunceUK,
		// Token: 0x04000189 RID: 393
		TeaspoonUK,
		// Token: 0x0400018A RID: 394
		TablespoonUK,
		// Token: 0x0400018B RID: 395
		CupUK,
		// Token: 0x0400018C RID: 396
		GillUK,
		// Token: 0x0400018D RID: 397
		PintUK,
		// Token: 0x0400018E RID: 398
		QuartUK,
		// Token: 0x0400018F RID: 399
		GallonUK,
		// Token: 0x04000190 RID: 400
		SquareMeter,
		// Token: 0x04000191 RID: 401
		SquareKilometer,
		// Token: 0x04000192 RID: 402
		SquareCentimeter,
		// Token: 0x04000193 RID: 403
		SquareMillimeter,
		// Token: 0x04000194 RID: 404
		SquareMicrometer,
		// Token: 0x04000195 RID: 405
		SquareInch,
		// Token: 0x04000196 RID: 406
		SquareFeet,
		// Token: 0x04000197 RID: 407
		SquareYard,
		// Token: 0x04000198 RID: 408
		SquareMile,
		// Token: 0x04000199 RID: 409
		Hectare,
		// Token: 0x0400019A RID: 410
		Acre,
		// Token: 0x0400019B RID: 411
		Are,
		// Token: 0x0400019C RID: 412
		Joule,
		// Token: 0x0400019D RID: 413
		Kilojoule,
		// Token: 0x0400019E RID: 414
		WattHour,
		// Token: 0x0400019F RID: 415
		KilowattHour,
		// Token: 0x040001A0 RID: 416
		HorsepowerHour,
		// Token: 0x040001A1 RID: 417
		Newton,
		// Token: 0x040001A2 RID: 418
		Kilonewton,
		// Token: 0x040001A3 RID: 419
		Meganewton,
		// Token: 0x040001A4 RID: 420
		Giganewton,
		// Token: 0x040001A5 RID: 421
		Teranewton,
		// Token: 0x040001A6 RID: 422
		Centinewton,
		// Token: 0x040001A7 RID: 423
		Millinewton,
		// Token: 0x040001A8 RID: 424
		JouleMeter,
		// Token: 0x040001A9 RID: 425
		JouleCentimeter,
		// Token: 0x040001AA RID: 426
		GramForce,
		// Token: 0x040001AB RID: 427
		KilogramForce,
		// Token: 0x040001AC RID: 428
		TonForce,
		// Token: 0x040001AD RID: 429
		PoundForce,
		// Token: 0x040001AE RID: 430
		KilopoundForce,
		// Token: 0x040001AF RID: 431
		OunceForce,
		// Token: 0x040001B0 RID: 432
		MetersPerSecond,
		// Token: 0x040001B1 RID: 433
		MetersPerMinute,
		// Token: 0x040001B2 RID: 434
		MetersPerHour,
		// Token: 0x040001B3 RID: 435
		KilometersPerSecond,
		// Token: 0x040001B4 RID: 436
		KilometersPerMinute,
		// Token: 0x040001B5 RID: 437
		KilometersPerHour,
		// Token: 0x040001B6 RID: 438
		CentimetersPerSecond,
		// Token: 0x040001B7 RID: 439
		CentimetersPerMinute,
		// Token: 0x040001B8 RID: 440
		CentimetersPerHour,
		// Token: 0x040001B9 RID: 441
		MillimetersPerSecond,
		// Token: 0x040001BA RID: 442
		MillimetersPerMinute,
		// Token: 0x040001BB RID: 443
		MillimetersPerHour,
		// Token: 0x040001BC RID: 444
		FeetPerSecond,
		// Token: 0x040001BD RID: 445
		FeetPerMinute,
		// Token: 0x040001BE RID: 446
		FeetPerHour,
		// Token: 0x040001BF RID: 447
		YardsPerSecond,
		// Token: 0x040001C0 RID: 448
		YardsPerMinute,
		// Token: 0x040001C1 RID: 449
		YardsPerHour,
		// Token: 0x040001C2 RID: 450
		MilesPerSecond,
		// Token: 0x040001C3 RID: 451
		MilesPerMinute,
		// Token: 0x040001C4 RID: 452
		MilesPerHour,
		// Token: 0x040001C5 RID: 453
		Knot,
		// Token: 0x040001C6 RID: 454
		KnotUK,
		// Token: 0x040001C7 RID: 455
		SpeedOfLight,
		// Token: 0x040001C8 RID: 456
		Bit,
		// Token: 0x040001C9 RID: 457
		Kilobit,
		// Token: 0x040001CA RID: 458
		Megabit,
		// Token: 0x040001CB RID: 459
		Gigabit,
		// Token: 0x040001CC RID: 460
		Terabit,
		// Token: 0x040001CD RID: 461
		Petabit,
		// Token: 0x040001CE RID: 462
		Byte,
		// Token: 0x040001CF RID: 463
		Kilobyte,
		// Token: 0x040001D0 RID: 464
		Kibibyte,
		// Token: 0x040001D1 RID: 465
		Megabyte,
		// Token: 0x040001D2 RID: 466
		Mebibyte,
		// Token: 0x040001D3 RID: 467
		Gigabyte,
		// Token: 0x040001D4 RID: 468
		Gibibyte,
		// Token: 0x040001D5 RID: 469
		Terabyte,
		// Token: 0x040001D6 RID: 470
		Tebibyte,
		// Token: 0x040001D7 RID: 471
		Petabyte,
		// Token: 0x040001D8 RID: 472
		Pebibyte,
		// Token: 0x040001D9 RID: 473
		Kilogram,
		// Token: 0x040001DA RID: 474
		Hectogram,
		// Token: 0x040001DB RID: 475
		Dekagram,
		// Token: 0x040001DC RID: 476
		Gram,
		// Token: 0x040001DD RID: 477
		Decigram,
		// Token: 0x040001DE RID: 478
		Centigram,
		// Token: 0x040001DF RID: 479
		Milligram,
		// Token: 0x040001E0 RID: 480
		MetricTon,
		// Token: 0x040001E1 RID: 481
		Pounds,
		// Token: 0x040001E2 RID: 482
		ShortTon,
		// Token: 0x040001E3 RID: 483
		LongTon,
		// Token: 0x040001E4 RID: 484
		Ounce,
		// Token: 0x040001E5 RID: 485
		StoneUS,
		// Token: 0x040001E6 RID: 486
		StoneUK,
		// Token: 0x040001E7 RID: 487
		QuarterUS,
		// Token: 0x040001E8 RID: 488
		QuarterUK,
		// Token: 0x040001E9 RID: 489
		Slug,
		// Token: 0x040001EA RID: 490
		Grain,
		// Token: 0x040001EB RID: 491
		Celsius,
		// Token: 0x040001EC RID: 492
		Fahrenheit,
		// Token: 0x040001ED RID: 493
		Kelvin,
		// Token: 0x040001EE RID: 494
		Pascal,
		// Token: 0x040001EF RID: 495
		Decipascal,
		// Token: 0x040001F0 RID: 496
		Centipascal,
		// Token: 0x040001F1 RID: 497
		Millipascal,
		// Token: 0x040001F2 RID: 498
		Micropascal,
		// Token: 0x040001F3 RID: 499
		Kilopascal,
		// Token: 0x040001F4 RID: 500
		Megapascal,
		// Token: 0x040001F5 RID: 501
		Gigapascal,
		// Token: 0x040001F6 RID: 502
		Bar,
		// Token: 0x040001F7 RID: 503
		Millibar,
		// Token: 0x040001F8 RID: 504
		Microbar,
		// Token: 0x040001F9 RID: 505
		PSI,
		// Token: 0x040001FA RID: 506
		KSI,
		// Token: 0x040001FB RID: 507
		StandardAtmosphere,
		// Token: 0x040001FC RID: 508
		Watt,
		// Token: 0x040001FD RID: 509
		Kilowatt,
		// Token: 0x040001FE RID: 510
		Megawatt,
		// Token: 0x040001FF RID: 511
		Gigawatt,
		// Token: 0x04000200 RID: 512
		Terawatt,
		// Token: 0x04000201 RID: 513
		Horsepower,
		// Token: 0x04000202 RID: 514
		JouleSecond,
		// Token: 0x04000203 RID: 515
		JouleMinute,
		// Token: 0x04000204 RID: 516
		JouleHour,
		// Token: 0x04000205 RID: 517
		KilojouleSecond,
		// Token: 0x04000206 RID: 518
		KilojouleMinute,
		// Token: 0x04000207 RID: 519
		KilojouleHour,
		// Token: 0x04000208 RID: 520
		Second,
		// Token: 0x04000209 RID: 521
		Millisecond,
		// Token: 0x0400020A RID: 522
		Microsecond,
		// Token: 0x0400020B RID: 523
		Nanosecond,
		// Token: 0x0400020C RID: 524
		Minute,
		// Token: 0x0400020D RID: 525
		Hour,
		// Token: 0x0400020E RID: 526
		Day,
		// Token: 0x0400020F RID: 527
		Week,
		// Token: 0x04000210 RID: 528
		Radian,
		// Token: 0x04000211 RID: 529
		Degree,
		// Token: 0x04000212 RID: 530
		Turn,
		// Token: 0x04000213 RID: 531
		Grad,
		// Token: 0x04000214 RID: 532
		SecondsOfAngle,
		// Token: 0x04000215 RID: 533
		MinutesOfAngle,
		// Token: 0x04000216 RID: 534
		Mil,
		// Token: 0x04000217 RID: 535
		MetersPerSecondSquared,
		// Token: 0x04000218 RID: 536
		DecimetersPerSecondSquared,
		// Token: 0x04000219 RID: 537
		CentimetersPerSecondSquared,
		// Token: 0x0400021A RID: 538
		MillimetersPerSecondSquared,
		// Token: 0x0400021B RID: 539
		MicrometersPerSecondSquared,
		// Token: 0x0400021C RID: 540
		DekametersPerSecondSquared,
		// Token: 0x0400021D RID: 541
		HectometersPerSecondSquared,
		// Token: 0x0400021E RID: 542
		KilometersPerSecondSquared,
		// Token: 0x0400021F RID: 543
		MilePerSecondSquared,
		// Token: 0x04000220 RID: 544
		YardPerSecondSquared,
		// Token: 0x04000221 RID: 545
		FeetPerSecondSquared,
		// Token: 0x04000222 RID: 546
		InchPerSecondSquared,
		// Token: 0x04000223 RID: 547
		GForce,
		// Token: 0x04000224 RID: 548
		NewtonMeter,
		// Token: 0x04000225 RID: 549
		NewtonCentimeter,
		// Token: 0x04000226 RID: 550
		NewtonMillimeter,
		// Token: 0x04000227 RID: 551
		KilonewtonMeter,
		// Token: 0x04000228 RID: 552
		KilogramForceMeter,
		// Token: 0x04000229 RID: 553
		KilogramForceCentimeter,
		// Token: 0x0400022A RID: 554
		KilogramForceMillimeter,
		// Token: 0x0400022B RID: 555
		GramForceMeter,
		// Token: 0x0400022C RID: 556
		GramForceCentimeter,
		// Token: 0x0400022D RID: 557
		GramForceMillimeter,
		// Token: 0x0400022E RID: 558
		PoundFeet,
		// Token: 0x0400022F RID: 559
		PoundInch,
		// Token: 0x04000230 RID: 560
		OuncecFeet,
		// Token: 0x04000231 RID: 561
		OuncecInch,
		// Token: 0x04000232 RID: 562
		RadiansPerSecond,
		// Token: 0x04000233 RID: 563
		RadiansPerMinute,
		// Token: 0x04000234 RID: 564
		RadiansPerHour,
		// Token: 0x04000235 RID: 565
		RadiansPerDay,
		// Token: 0x04000236 RID: 566
		DegreesPerSecond,
		// Token: 0x04000237 RID: 567
		DegreesPerMinute,
		// Token: 0x04000238 RID: 568
		DegreesPerHour,
		// Token: 0x04000239 RID: 569
		DegreesPerDay,
		// Token: 0x0400023A RID: 570
		RevolutionsPerSecond,
		// Token: 0x0400023B RID: 571
		RevolutionsPerMinute,
		// Token: 0x0400023C RID: 572
		RevolutionsPerHour,
		// Token: 0x0400023D RID: 573
		RevolutionsPerDay,
		// Token: 0x0400023E RID: 574
		Hertz,
		// Token: 0x0400023F RID: 575
		Kilohertz,
		// Token: 0x04000240 RID: 576
		Megahertz,
		// Token: 0x04000241 RID: 577
		Gigahertz,
		// Token: 0x04000242 RID: 578
		PercentMultiplier,
		// Token: 0x04000243 RID: 579
		Percent,
		// Token: 0x04000244 RID: 580
		Permille,
		// Token: 0x04000245 RID: 581
		Permyriad
	}
}

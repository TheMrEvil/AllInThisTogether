using System;

namespace InControl
{
	// Token: 0x0200002A RID: 42
	public enum InputControlType
	{
		// Token: 0x0400015D RID: 349
		None,
		// Token: 0x0400015E RID: 350
		LeftStickUp,
		// Token: 0x0400015F RID: 351
		LeftStickDown,
		// Token: 0x04000160 RID: 352
		LeftStickLeft,
		// Token: 0x04000161 RID: 353
		LeftStickRight,
		// Token: 0x04000162 RID: 354
		LeftStickButton,
		// Token: 0x04000163 RID: 355
		RightStickUp,
		// Token: 0x04000164 RID: 356
		RightStickDown,
		// Token: 0x04000165 RID: 357
		RightStickLeft,
		// Token: 0x04000166 RID: 358
		RightStickRight,
		// Token: 0x04000167 RID: 359
		RightStickButton,
		// Token: 0x04000168 RID: 360
		DPadUp,
		// Token: 0x04000169 RID: 361
		DPadDown,
		// Token: 0x0400016A RID: 362
		DPadLeft,
		// Token: 0x0400016B RID: 363
		DPadRight,
		// Token: 0x0400016C RID: 364
		LeftTrigger,
		// Token: 0x0400016D RID: 365
		RightTrigger,
		// Token: 0x0400016E RID: 366
		LeftBumper,
		// Token: 0x0400016F RID: 367
		RightBumper,
		// Token: 0x04000170 RID: 368
		Action1,
		// Token: 0x04000171 RID: 369
		Action2,
		// Token: 0x04000172 RID: 370
		Action3,
		// Token: 0x04000173 RID: 371
		Action4,
		// Token: 0x04000174 RID: 372
		Action5,
		// Token: 0x04000175 RID: 373
		Action6,
		// Token: 0x04000176 RID: 374
		Action7,
		// Token: 0x04000177 RID: 375
		Action8,
		// Token: 0x04000178 RID: 376
		Action9,
		// Token: 0x04000179 RID: 377
		Action10,
		// Token: 0x0400017A RID: 378
		Action11,
		// Token: 0x0400017B RID: 379
		Action12,
		// Token: 0x0400017C RID: 380
		Back = 100,
		// Token: 0x0400017D RID: 381
		Start,
		// Token: 0x0400017E RID: 382
		Select,
		// Token: 0x0400017F RID: 383
		System,
		// Token: 0x04000180 RID: 384
		Options,
		// Token: 0x04000181 RID: 385
		Pause,
		// Token: 0x04000182 RID: 386
		Menu,
		// Token: 0x04000183 RID: 387
		Share,
		// Token: 0x04000184 RID: 388
		Home,
		// Token: 0x04000185 RID: 389
		View,
		// Token: 0x04000186 RID: 390
		Power,
		// Token: 0x04000187 RID: 391
		Capture,
		// Token: 0x04000188 RID: 392
		Assistant,
		// Token: 0x04000189 RID: 393
		Plus,
		// Token: 0x0400018A RID: 394
		Minus,
		// Token: 0x0400018B RID: 395
		Create,
		// Token: 0x0400018C RID: 396
		Mute,
		// Token: 0x0400018D RID: 397
		Guide,
		// Token: 0x0400018E RID: 398
		PedalLeft = 150,
		// Token: 0x0400018F RID: 399
		PedalRight,
		// Token: 0x04000190 RID: 400
		PedalMiddle,
		// Token: 0x04000191 RID: 401
		GearUp,
		// Token: 0x04000192 RID: 402
		GearDown,
		// Token: 0x04000193 RID: 403
		Pitch = 200,
		// Token: 0x04000194 RID: 404
		Roll,
		// Token: 0x04000195 RID: 405
		Yaw,
		// Token: 0x04000196 RID: 406
		PitchUp,
		// Token: 0x04000197 RID: 407
		PitchDown,
		// Token: 0x04000198 RID: 408
		RollLeft,
		// Token: 0x04000199 RID: 409
		RollRight,
		// Token: 0x0400019A RID: 410
		YawLeft,
		// Token: 0x0400019B RID: 411
		YawRight,
		// Token: 0x0400019C RID: 412
		ThrottleUp,
		// Token: 0x0400019D RID: 413
		ThrottleDown,
		// Token: 0x0400019E RID: 414
		ThrottleLeft,
		// Token: 0x0400019F RID: 415
		ThrottleRight,
		// Token: 0x040001A0 RID: 416
		POVUp,
		// Token: 0x040001A1 RID: 417
		POVDown,
		// Token: 0x040001A2 RID: 418
		POVLeft,
		// Token: 0x040001A3 RID: 419
		POVRight,
		// Token: 0x040001A4 RID: 420
		TiltX = 250,
		// Token: 0x040001A5 RID: 421
		TiltY,
		// Token: 0x040001A6 RID: 422
		TiltZ,
		// Token: 0x040001A7 RID: 423
		GyroscopeX = 250,
		// Token: 0x040001A8 RID: 424
		GyroscopeY,
		// Token: 0x040001A9 RID: 425
		GyroscopeZ,
		// Token: 0x040001AA RID: 426
		AccelerometerX,
		// Token: 0x040001AB RID: 427
		AccelerometerY,
		// Token: 0x040001AC RID: 428
		AccelerometerZ,
		// Token: 0x040001AD RID: 429
		ScrollWheel,
		// Token: 0x040001AE RID: 430
		[Obsolete("Use InputControlType.TouchPadButton instead.", true)]
		TouchPadTap,
		// Token: 0x040001AF RID: 431
		TouchPadButton,
		// Token: 0x040001B0 RID: 432
		TouchPadXAxis,
		// Token: 0x040001B1 RID: 433
		TouchPadYAxis,
		// Token: 0x040001B2 RID: 434
		LeftSL,
		// Token: 0x040001B3 RID: 435
		LeftSR,
		// Token: 0x040001B4 RID: 436
		RightSL,
		// Token: 0x040001B5 RID: 437
		RightSR,
		// Token: 0x040001B6 RID: 438
		Paddle1,
		// Token: 0x040001B7 RID: 439
		Paddle2,
		// Token: 0x040001B8 RID: 440
		Paddle3,
		// Token: 0x040001B9 RID: 441
		Paddle4,
		// Token: 0x040001BA RID: 442
		Command = 300,
		// Token: 0x040001BB RID: 443
		LeftStickX,
		// Token: 0x040001BC RID: 444
		LeftStickY,
		// Token: 0x040001BD RID: 445
		RightStickX,
		// Token: 0x040001BE RID: 446
		RightStickY,
		// Token: 0x040001BF RID: 447
		DPadX,
		// Token: 0x040001C0 RID: 448
		DPadY,
		// Token: 0x040001C1 RID: 449
		LeftCommand,
		// Token: 0x040001C2 RID: 450
		RightCommand,
		// Token: 0x040001C3 RID: 451
		Analog0 = 400,
		// Token: 0x040001C4 RID: 452
		Analog1,
		// Token: 0x040001C5 RID: 453
		Analog2,
		// Token: 0x040001C6 RID: 454
		Analog3,
		// Token: 0x040001C7 RID: 455
		Analog4,
		// Token: 0x040001C8 RID: 456
		Analog5,
		// Token: 0x040001C9 RID: 457
		Analog6,
		// Token: 0x040001CA RID: 458
		Analog7,
		// Token: 0x040001CB RID: 459
		Analog8,
		// Token: 0x040001CC RID: 460
		Analog9,
		// Token: 0x040001CD RID: 461
		Analog10,
		// Token: 0x040001CE RID: 462
		Analog11,
		// Token: 0x040001CF RID: 463
		Analog12,
		// Token: 0x040001D0 RID: 464
		Analog13,
		// Token: 0x040001D1 RID: 465
		Analog14,
		// Token: 0x040001D2 RID: 466
		Analog15,
		// Token: 0x040001D3 RID: 467
		Analog16,
		// Token: 0x040001D4 RID: 468
		Analog17,
		// Token: 0x040001D5 RID: 469
		Analog18,
		// Token: 0x040001D6 RID: 470
		Analog19,
		// Token: 0x040001D7 RID: 471
		Button0 = 500,
		// Token: 0x040001D8 RID: 472
		Button1,
		// Token: 0x040001D9 RID: 473
		Button2,
		// Token: 0x040001DA RID: 474
		Button3,
		// Token: 0x040001DB RID: 475
		Button4,
		// Token: 0x040001DC RID: 476
		Button5,
		// Token: 0x040001DD RID: 477
		Button6,
		// Token: 0x040001DE RID: 478
		Button7,
		// Token: 0x040001DF RID: 479
		Button8,
		// Token: 0x040001E0 RID: 480
		Button9,
		// Token: 0x040001E1 RID: 481
		Button10,
		// Token: 0x040001E2 RID: 482
		Button11,
		// Token: 0x040001E3 RID: 483
		Button12,
		// Token: 0x040001E4 RID: 484
		Button13,
		// Token: 0x040001E5 RID: 485
		Button14,
		// Token: 0x040001E6 RID: 486
		Button15,
		// Token: 0x040001E7 RID: 487
		Button16,
		// Token: 0x040001E8 RID: 488
		Button17,
		// Token: 0x040001E9 RID: 489
		Button18,
		// Token: 0x040001EA RID: 490
		Button19,
		// Token: 0x040001EB RID: 491
		Button20,
		// Token: 0x040001EC RID: 492
		Button21,
		// Token: 0x040001ED RID: 493
		Button22,
		// Token: 0x040001EE RID: 494
		Button23,
		// Token: 0x040001EF RID: 495
		Button24,
		// Token: 0x040001F0 RID: 496
		Button25,
		// Token: 0x040001F1 RID: 497
		Button26,
		// Token: 0x040001F2 RID: 498
		Button27,
		// Token: 0x040001F3 RID: 499
		Button28,
		// Token: 0x040001F4 RID: 500
		Button29,
		// Token: 0x040001F5 RID: 501
		Count
	}
}

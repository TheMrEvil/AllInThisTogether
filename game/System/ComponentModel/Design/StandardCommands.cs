using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers for the standard set of commands that are available to most applications.</summary>
	// Token: 0x02000475 RID: 1141
	public class StandardCommands
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.StandardCommands" /> class.</summary>
		// Token: 0x060024B2 RID: 9394 RVA: 0x0000219B File Offset: 0x0000039B
		public StandardCommands()
		{
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x00081E84 File Offset: 0x00080084
		// Note: this type is marked as 'beforefieldinit'.
		static StandardCommands()
		{
		}

		// Token: 0x040010EB RID: 4331
		private static readonly Guid s_standardCommandSet = StandardCommands.ShellGuids.VSStandardCommandSet97;

		// Token: 0x040010EC RID: 4332
		private static readonly Guid s_ndpCommandSet = new Guid("{74D21313-2AEE-11d1-8BFB-00A0C90F26F7}");

		// Token: 0x040010ED RID: 4333
		private const int cmdidDesignerVerbFirst = 8192;

		// Token: 0x040010EE RID: 4334
		private const int cmdidDesignerVerbLast = 8448;

		// Token: 0x040010EF RID: 4335
		private const int cmdidArrangeIcons = 12298;

		// Token: 0x040010F0 RID: 4336
		private const int cmdidLineupIcons = 12299;

		// Token: 0x040010F1 RID: 4337
		private const int cmdidShowLargeIcons = 12300;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignBottom command. This field is read-only.</summary>
		// Token: 0x040010F2 RID: 4338
		public static readonly CommandID AlignBottom = new CommandID(StandardCommands.s_standardCommandSet, 1);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignHorizontalCenters command. This field is read-only.</summary>
		// Token: 0x040010F3 RID: 4339
		public static readonly CommandID AlignHorizontalCenters = new CommandID(StandardCommands.s_standardCommandSet, 2);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignLeft command. This field is read-only.</summary>
		// Token: 0x040010F4 RID: 4340
		public static readonly CommandID AlignLeft = new CommandID(StandardCommands.s_standardCommandSet, 3);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignRight command. This field is read-only.</summary>
		// Token: 0x040010F5 RID: 4341
		public static readonly CommandID AlignRight = new CommandID(StandardCommands.s_standardCommandSet, 4);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignToGrid command. This field is read-only.</summary>
		// Token: 0x040010F6 RID: 4342
		public static readonly CommandID AlignToGrid = new CommandID(StandardCommands.s_standardCommandSet, 5);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignTop command. This field is read-only.</summary>
		// Token: 0x040010F7 RID: 4343
		public static readonly CommandID AlignTop = new CommandID(StandardCommands.s_standardCommandSet, 6);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignVerticalCenters command. This field is read-only.</summary>
		// Token: 0x040010F8 RID: 4344
		public static readonly CommandID AlignVerticalCenters = new CommandID(StandardCommands.s_standardCommandSet, 7);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeBottom command. This field is read-only.</summary>
		// Token: 0x040010F9 RID: 4345
		public static readonly CommandID ArrangeBottom = new CommandID(StandardCommands.s_standardCommandSet, 8);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeRight command. This field is read-only.</summary>
		// Token: 0x040010FA RID: 4346
		public static readonly CommandID ArrangeRight = new CommandID(StandardCommands.s_standardCommandSet, 9);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the BringForward command. This field is read-only.</summary>
		// Token: 0x040010FB RID: 4347
		public static readonly CommandID BringForward = new CommandID(StandardCommands.s_standardCommandSet, 10);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the BringToFront command. This field is read-only.</summary>
		// Token: 0x040010FC RID: 4348
		public static readonly CommandID BringToFront = new CommandID(StandardCommands.s_standardCommandSet, 11);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the CenterHorizontally command. This field is read-only.</summary>
		// Token: 0x040010FD RID: 4349
		public static readonly CommandID CenterHorizontally = new CommandID(StandardCommands.s_standardCommandSet, 12);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the CenterVertically command. This field is read-only.</summary>
		// Token: 0x040010FE RID: 4350
		public static readonly CommandID CenterVertically = new CommandID(StandardCommands.s_standardCommandSet, 13);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ViewCode command. This field is read-only.</summary>
		// Token: 0x040010FF RID: 4351
		public static readonly CommandID ViewCode = new CommandID(StandardCommands.s_standardCommandSet, 333);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Document Outline command. This field is read-only.</summary>
		// Token: 0x04001100 RID: 4352
		public static readonly CommandID DocumentOutline = new CommandID(StandardCommands.s_standardCommandSet, 239);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Copy command. This field is read-only.</summary>
		// Token: 0x04001101 RID: 4353
		public static readonly CommandID Copy = new CommandID(StandardCommands.s_standardCommandSet, 15);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Cut command. This field is read-only.</summary>
		// Token: 0x04001102 RID: 4354
		public static readonly CommandID Cut = new CommandID(StandardCommands.s_standardCommandSet, 16);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Delete command. This field is read-only.</summary>
		// Token: 0x04001103 RID: 4355
		public static readonly CommandID Delete = new CommandID(StandardCommands.s_standardCommandSet, 17);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Group command. This field is read-only.</summary>
		// Token: 0x04001104 RID: 4356
		public static readonly CommandID Group = new CommandID(StandardCommands.s_standardCommandSet, 20);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceConcatenate command. This field is read-only.</summary>
		// Token: 0x04001105 RID: 4357
		public static readonly CommandID HorizSpaceConcatenate = new CommandID(StandardCommands.s_standardCommandSet, 21);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceDecrease command. This field is read-only.</summary>
		// Token: 0x04001106 RID: 4358
		public static readonly CommandID HorizSpaceDecrease = new CommandID(StandardCommands.s_standardCommandSet, 22);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceIncrease command. This field is read-only.</summary>
		// Token: 0x04001107 RID: 4359
		public static readonly CommandID HorizSpaceIncrease = new CommandID(StandardCommands.s_standardCommandSet, 23);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceMakeEqual command. This field is read-only.</summary>
		// Token: 0x04001108 RID: 4360
		public static readonly CommandID HorizSpaceMakeEqual = new CommandID(StandardCommands.s_standardCommandSet, 24);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Paste command. This field is read-only.</summary>
		// Token: 0x04001109 RID: 4361
		public static readonly CommandID Paste = new CommandID(StandardCommands.s_standardCommandSet, 26);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Properties command. This field is read-only.</summary>
		// Token: 0x0400110A RID: 4362
		public static readonly CommandID Properties = new CommandID(StandardCommands.s_standardCommandSet, 28);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Redo command. This field is read-only.</summary>
		// Token: 0x0400110B RID: 4363
		public static readonly CommandID Redo = new CommandID(StandardCommands.s_standardCommandSet, 29);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the MultiLevelRedo command. This field is read-only.</summary>
		// Token: 0x0400110C RID: 4364
		public static readonly CommandID MultiLevelRedo = new CommandID(StandardCommands.s_standardCommandSet, 30);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SelectAll command. This field is read-only.</summary>
		// Token: 0x0400110D RID: 4365
		public static readonly CommandID SelectAll = new CommandID(StandardCommands.s_standardCommandSet, 31);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SendBackward command. This field is read-only.</summary>
		// Token: 0x0400110E RID: 4366
		public static readonly CommandID SendBackward = new CommandID(StandardCommands.s_standardCommandSet, 32);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SendToBack command. This field is read-only.</summary>
		// Token: 0x0400110F RID: 4367
		public static readonly CommandID SendToBack = new CommandID(StandardCommands.s_standardCommandSet, 33);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControl command. This field is read-only.</summary>
		// Token: 0x04001110 RID: 4368
		public static readonly CommandID SizeToControl = new CommandID(StandardCommands.s_standardCommandSet, 35);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControlHeight command. This field is read-only.</summary>
		// Token: 0x04001111 RID: 4369
		public static readonly CommandID SizeToControlHeight = new CommandID(StandardCommands.s_standardCommandSet, 36);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControlWidth command. This field is read-only.</summary>
		// Token: 0x04001112 RID: 4370
		public static readonly CommandID SizeToControlWidth = new CommandID(StandardCommands.s_standardCommandSet, 37);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToFit command. This field is read-only.</summary>
		// Token: 0x04001113 RID: 4371
		public static readonly CommandID SizeToFit = new CommandID(StandardCommands.s_standardCommandSet, 38);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToGrid command. This field is read-only.</summary>
		// Token: 0x04001114 RID: 4372
		public static readonly CommandID SizeToGrid = new CommandID(StandardCommands.s_standardCommandSet, 39);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SnapToGrid command. This field is read-only.</summary>
		// Token: 0x04001115 RID: 4373
		public static readonly CommandID SnapToGrid = new CommandID(StandardCommands.s_standardCommandSet, 40);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the TabOrder command. This field is read-only.</summary>
		// Token: 0x04001116 RID: 4374
		public static readonly CommandID TabOrder = new CommandID(StandardCommands.s_standardCommandSet, 41);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Undo command. This field is read-only.</summary>
		// Token: 0x04001117 RID: 4375
		public static readonly CommandID Undo = new CommandID(StandardCommands.s_standardCommandSet, 43);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the MultiLevelUndo command. This field is read-only.</summary>
		// Token: 0x04001118 RID: 4376
		public static readonly CommandID MultiLevelUndo = new CommandID(StandardCommands.s_standardCommandSet, 44);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Ungroup command. This field is read-only.</summary>
		// Token: 0x04001119 RID: 4377
		public static readonly CommandID Ungroup = new CommandID(StandardCommands.s_standardCommandSet, 45);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceConcatenate command. This field is read-only.</summary>
		// Token: 0x0400111A RID: 4378
		public static readonly CommandID VertSpaceConcatenate = new CommandID(StandardCommands.s_standardCommandSet, 46);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceDecrease command. This field is read-only.</summary>
		// Token: 0x0400111B RID: 4379
		public static readonly CommandID VertSpaceDecrease = new CommandID(StandardCommands.s_standardCommandSet, 47);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceIncrease command. This field is read-only.</summary>
		// Token: 0x0400111C RID: 4380
		public static readonly CommandID VertSpaceIncrease = new CommandID(StandardCommands.s_standardCommandSet, 48);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceMakeEqual command. This field is read-only.</summary>
		// Token: 0x0400111D RID: 4381
		public static readonly CommandID VertSpaceMakeEqual = new CommandID(StandardCommands.s_standardCommandSet, 49);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ShowGrid command. This field is read-only.</summary>
		// Token: 0x0400111E RID: 4382
		public static readonly CommandID ShowGrid = new CommandID(StandardCommands.s_standardCommandSet, 103);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ViewGrid command. This field is read-only.</summary>
		// Token: 0x0400111F RID: 4383
		public static readonly CommandID ViewGrid = new CommandID(StandardCommands.s_standardCommandSet, 125);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Replace command. This field is read-only.</summary>
		// Token: 0x04001120 RID: 4384
		public static readonly CommandID Replace = new CommandID(StandardCommands.s_standardCommandSet, 230);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the PropertiesWindow command. This field is read-only.</summary>
		// Token: 0x04001121 RID: 4385
		public static readonly CommandID PropertiesWindow = new CommandID(StandardCommands.s_standardCommandSet, 235);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the LockControls command. This field is read-only.</summary>
		// Token: 0x04001122 RID: 4386
		public static readonly CommandID LockControls = new CommandID(StandardCommands.s_standardCommandSet, 369);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the F1Help command. This field is read-only.</summary>
		// Token: 0x04001123 RID: 4387
		public static readonly CommandID F1Help = new CommandID(StandardCommands.s_standardCommandSet, 377);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeIcons command. This field is read-only.</summary>
		// Token: 0x04001124 RID: 4388
		public static readonly CommandID ArrangeIcons = new CommandID(StandardCommands.s_ndpCommandSet, 12298);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the LineupIcons command. This field is read-only.</summary>
		// Token: 0x04001125 RID: 4389
		public static readonly CommandID LineupIcons = new CommandID(StandardCommands.s_ndpCommandSet, 12299);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ShowLargeIcons command. This field is read-only.</summary>
		// Token: 0x04001126 RID: 4390
		public static readonly CommandID ShowLargeIcons = new CommandID(StandardCommands.s_ndpCommandSet, 12300);

		/// <summary>Gets the first of a set of verbs. This field is read-only.</summary>
		// Token: 0x04001127 RID: 4391
		public static readonly CommandID VerbFirst = new CommandID(StandardCommands.s_ndpCommandSet, 8192);

		/// <summary>Gets the last of a set of verbs. This field is read-only.</summary>
		// Token: 0x04001128 RID: 4392
		public static readonly CommandID VerbLast = new CommandID(StandardCommands.s_ndpCommandSet, 8448);

		// Token: 0x02000476 RID: 1142
		private static class VSStandardCommands
		{
			// Token: 0x04001129 RID: 4393
			internal const int cmdidAlignBottom = 1;

			// Token: 0x0400112A RID: 4394
			internal const int cmdidAlignHorizontalCenters = 2;

			// Token: 0x0400112B RID: 4395
			internal const int cmdidAlignLeft = 3;

			// Token: 0x0400112C RID: 4396
			internal const int cmdidAlignRight = 4;

			// Token: 0x0400112D RID: 4397
			internal const int cmdidAlignToGrid = 5;

			// Token: 0x0400112E RID: 4398
			internal const int cmdidAlignTop = 6;

			// Token: 0x0400112F RID: 4399
			internal const int cmdidAlignVerticalCenters = 7;

			// Token: 0x04001130 RID: 4400
			internal const int cmdidArrangeBottom = 8;

			// Token: 0x04001131 RID: 4401
			internal const int cmdidArrangeRight = 9;

			// Token: 0x04001132 RID: 4402
			internal const int cmdidBringForward = 10;

			// Token: 0x04001133 RID: 4403
			internal const int cmdidBringToFront = 11;

			// Token: 0x04001134 RID: 4404
			internal const int cmdidCenterHorizontally = 12;

			// Token: 0x04001135 RID: 4405
			internal const int cmdidCenterVertically = 13;

			// Token: 0x04001136 RID: 4406
			internal const int cmdidCode = 14;

			// Token: 0x04001137 RID: 4407
			internal const int cmdidCopy = 15;

			// Token: 0x04001138 RID: 4408
			internal const int cmdidCut = 16;

			// Token: 0x04001139 RID: 4409
			internal const int cmdidDelete = 17;

			// Token: 0x0400113A RID: 4410
			internal const int cmdidFontName = 18;

			// Token: 0x0400113B RID: 4411
			internal const int cmdidFontSize = 19;

			// Token: 0x0400113C RID: 4412
			internal const int cmdidGroup = 20;

			// Token: 0x0400113D RID: 4413
			internal const int cmdidHorizSpaceConcatenate = 21;

			// Token: 0x0400113E RID: 4414
			internal const int cmdidHorizSpaceDecrease = 22;

			// Token: 0x0400113F RID: 4415
			internal const int cmdidHorizSpaceIncrease = 23;

			// Token: 0x04001140 RID: 4416
			internal const int cmdidHorizSpaceMakeEqual = 24;

			// Token: 0x04001141 RID: 4417
			internal const int cmdidLockControls = 369;

			// Token: 0x04001142 RID: 4418
			internal const int cmdidInsertObject = 25;

			// Token: 0x04001143 RID: 4419
			internal const int cmdidPaste = 26;

			// Token: 0x04001144 RID: 4420
			internal const int cmdidPrint = 27;

			// Token: 0x04001145 RID: 4421
			internal const int cmdidProperties = 28;

			// Token: 0x04001146 RID: 4422
			internal const int cmdidRedo = 29;

			// Token: 0x04001147 RID: 4423
			internal const int cmdidMultiLevelRedo = 30;

			// Token: 0x04001148 RID: 4424
			internal const int cmdidSelectAll = 31;

			// Token: 0x04001149 RID: 4425
			internal const int cmdidSendBackward = 32;

			// Token: 0x0400114A RID: 4426
			internal const int cmdidSendToBack = 33;

			// Token: 0x0400114B RID: 4427
			internal const int cmdidShowTable = 34;

			// Token: 0x0400114C RID: 4428
			internal const int cmdidSizeToControl = 35;

			// Token: 0x0400114D RID: 4429
			internal const int cmdidSizeToControlHeight = 36;

			// Token: 0x0400114E RID: 4430
			internal const int cmdidSizeToControlWidth = 37;

			// Token: 0x0400114F RID: 4431
			internal const int cmdidSizeToFit = 38;

			// Token: 0x04001150 RID: 4432
			internal const int cmdidSizeToGrid = 39;

			// Token: 0x04001151 RID: 4433
			internal const int cmdidSnapToGrid = 40;

			// Token: 0x04001152 RID: 4434
			internal const int cmdidTabOrder = 41;

			// Token: 0x04001153 RID: 4435
			internal const int cmdidToolbox = 42;

			// Token: 0x04001154 RID: 4436
			internal const int cmdidUndo = 43;

			// Token: 0x04001155 RID: 4437
			internal const int cmdidMultiLevelUndo = 44;

			// Token: 0x04001156 RID: 4438
			internal const int cmdidUngroup = 45;

			// Token: 0x04001157 RID: 4439
			internal const int cmdidVertSpaceConcatenate = 46;

			// Token: 0x04001158 RID: 4440
			internal const int cmdidVertSpaceDecrease = 47;

			// Token: 0x04001159 RID: 4441
			internal const int cmdidVertSpaceIncrease = 48;

			// Token: 0x0400115A RID: 4442
			internal const int cmdidVertSpaceMakeEqual = 49;

			// Token: 0x0400115B RID: 4443
			internal const int cmdidZoomPercent = 50;

			// Token: 0x0400115C RID: 4444
			internal const int cmdidBackColor = 51;

			// Token: 0x0400115D RID: 4445
			internal const int cmdidBold = 52;

			// Token: 0x0400115E RID: 4446
			internal const int cmdidBorderColor = 53;

			// Token: 0x0400115F RID: 4447
			internal const int cmdidBorderDashDot = 54;

			// Token: 0x04001160 RID: 4448
			internal const int cmdidBorderDashDotDot = 55;

			// Token: 0x04001161 RID: 4449
			internal const int cmdidBorderDashes = 56;

			// Token: 0x04001162 RID: 4450
			internal const int cmdidBorderDots = 57;

			// Token: 0x04001163 RID: 4451
			internal const int cmdidBorderShortDashes = 58;

			// Token: 0x04001164 RID: 4452
			internal const int cmdidBorderSolid = 59;

			// Token: 0x04001165 RID: 4453
			internal const int cmdidBorderSparseDots = 60;

			// Token: 0x04001166 RID: 4454
			internal const int cmdidBorderWidth1 = 61;

			// Token: 0x04001167 RID: 4455
			internal const int cmdidBorderWidth2 = 62;

			// Token: 0x04001168 RID: 4456
			internal const int cmdidBorderWidth3 = 63;

			// Token: 0x04001169 RID: 4457
			internal const int cmdidBorderWidth4 = 64;

			// Token: 0x0400116A RID: 4458
			internal const int cmdidBorderWidth5 = 65;

			// Token: 0x0400116B RID: 4459
			internal const int cmdidBorderWidth6 = 66;

			// Token: 0x0400116C RID: 4460
			internal const int cmdidBorderWidthHairline = 67;

			// Token: 0x0400116D RID: 4461
			internal const int cmdidFlat = 68;

			// Token: 0x0400116E RID: 4462
			internal const int cmdidForeColor = 69;

			// Token: 0x0400116F RID: 4463
			internal const int cmdidItalic = 70;

			// Token: 0x04001170 RID: 4464
			internal const int cmdidJustifyCenter = 71;

			// Token: 0x04001171 RID: 4465
			internal const int cmdidJustifyGeneral = 72;

			// Token: 0x04001172 RID: 4466
			internal const int cmdidJustifyLeft = 73;

			// Token: 0x04001173 RID: 4467
			internal const int cmdidJustifyRight = 74;

			// Token: 0x04001174 RID: 4468
			internal const int cmdidRaised = 75;

			// Token: 0x04001175 RID: 4469
			internal const int cmdidSunken = 76;

			// Token: 0x04001176 RID: 4470
			internal const int cmdidUnderline = 77;

			// Token: 0x04001177 RID: 4471
			internal const int cmdidChiseled = 78;

			// Token: 0x04001178 RID: 4472
			internal const int cmdidEtched = 79;

			// Token: 0x04001179 RID: 4473
			internal const int cmdidShadowed = 80;

			// Token: 0x0400117A RID: 4474
			internal const int cmdidCompDebug1 = 81;

			// Token: 0x0400117B RID: 4475
			internal const int cmdidCompDebug2 = 82;

			// Token: 0x0400117C RID: 4476
			internal const int cmdidCompDebug3 = 83;

			// Token: 0x0400117D RID: 4477
			internal const int cmdidCompDebug4 = 84;

			// Token: 0x0400117E RID: 4478
			internal const int cmdidCompDebug5 = 85;

			// Token: 0x0400117F RID: 4479
			internal const int cmdidCompDebug6 = 86;

			// Token: 0x04001180 RID: 4480
			internal const int cmdidCompDebug7 = 87;

			// Token: 0x04001181 RID: 4481
			internal const int cmdidCompDebug8 = 88;

			// Token: 0x04001182 RID: 4482
			internal const int cmdidCompDebug9 = 89;

			// Token: 0x04001183 RID: 4483
			internal const int cmdidCompDebug10 = 90;

			// Token: 0x04001184 RID: 4484
			internal const int cmdidCompDebug11 = 91;

			// Token: 0x04001185 RID: 4485
			internal const int cmdidCompDebug12 = 92;

			// Token: 0x04001186 RID: 4486
			internal const int cmdidCompDebug13 = 93;

			// Token: 0x04001187 RID: 4487
			internal const int cmdidCompDebug14 = 94;

			// Token: 0x04001188 RID: 4488
			internal const int cmdidCompDebug15 = 95;

			// Token: 0x04001189 RID: 4489
			internal const int cmdidExistingSchemaEdit = 96;

			// Token: 0x0400118A RID: 4490
			internal const int cmdidFind = 97;

			// Token: 0x0400118B RID: 4491
			internal const int cmdidGetZoom = 98;

			// Token: 0x0400118C RID: 4492
			internal const int cmdidQueryOpenDesign = 99;

			// Token: 0x0400118D RID: 4493
			internal const int cmdidQueryOpenNew = 100;

			// Token: 0x0400118E RID: 4494
			internal const int cmdidSingleTableDesign = 101;

			// Token: 0x0400118F RID: 4495
			internal const int cmdidSingleTableNew = 102;

			// Token: 0x04001190 RID: 4496
			internal const int cmdidShowGrid = 103;

			// Token: 0x04001191 RID: 4497
			internal const int cmdidNewTable = 104;

			// Token: 0x04001192 RID: 4498
			internal const int cmdidCollapsedView = 105;

			// Token: 0x04001193 RID: 4499
			internal const int cmdidFieldView = 106;

			// Token: 0x04001194 RID: 4500
			internal const int cmdidVerifySQL = 107;

			// Token: 0x04001195 RID: 4501
			internal const int cmdidHideTable = 108;

			// Token: 0x04001196 RID: 4502
			internal const int cmdidPrimaryKey = 109;

			// Token: 0x04001197 RID: 4503
			internal const int cmdidSave = 110;

			// Token: 0x04001198 RID: 4504
			internal const int cmdidSaveAs = 111;

			// Token: 0x04001199 RID: 4505
			internal const int cmdidSortAscending = 112;

			// Token: 0x0400119A RID: 4506
			internal const int cmdidSortDescending = 113;

			// Token: 0x0400119B RID: 4507
			internal const int cmdidAppendQuery = 114;

			// Token: 0x0400119C RID: 4508
			internal const int cmdidCrosstabQuery = 115;

			// Token: 0x0400119D RID: 4509
			internal const int cmdidDeleteQuery = 116;

			// Token: 0x0400119E RID: 4510
			internal const int cmdidMakeTableQuery = 117;

			// Token: 0x0400119F RID: 4511
			internal const int cmdidSelectQuery = 118;

			// Token: 0x040011A0 RID: 4512
			internal const int cmdidUpdateQuery = 119;

			// Token: 0x040011A1 RID: 4513
			internal const int cmdidParameters = 120;

			// Token: 0x040011A2 RID: 4514
			internal const int cmdidTotals = 121;

			// Token: 0x040011A3 RID: 4515
			internal const int cmdidViewCollapsed = 122;

			// Token: 0x040011A4 RID: 4516
			internal const int cmdidViewFieldList = 123;

			// Token: 0x040011A5 RID: 4517
			internal const int cmdidViewKeys = 124;

			// Token: 0x040011A6 RID: 4518
			internal const int cmdidViewGrid = 125;

			// Token: 0x040011A7 RID: 4519
			internal const int cmdidInnerJoin = 126;

			// Token: 0x040011A8 RID: 4520
			internal const int cmdidRightOuterJoin = 127;

			// Token: 0x040011A9 RID: 4521
			internal const int cmdidLeftOuterJoin = 128;

			// Token: 0x040011AA RID: 4522
			internal const int cmdidFullOuterJoin = 129;

			// Token: 0x040011AB RID: 4523
			internal const int cmdidUnionJoin = 130;

			// Token: 0x040011AC RID: 4524
			internal const int cmdidShowSQLPane = 131;

			// Token: 0x040011AD RID: 4525
			internal const int cmdidShowGraphicalPane = 132;

			// Token: 0x040011AE RID: 4526
			internal const int cmdidShowDataPane = 133;

			// Token: 0x040011AF RID: 4527
			internal const int cmdidShowQBEPane = 134;

			// Token: 0x040011B0 RID: 4528
			internal const int cmdidSelectAllFields = 135;

			// Token: 0x040011B1 RID: 4529
			internal const int cmdidOLEObjectMenuButton = 136;

			// Token: 0x040011B2 RID: 4530
			internal const int cmdidObjectVerbList0 = 137;

			// Token: 0x040011B3 RID: 4531
			internal const int cmdidObjectVerbList1 = 138;

			// Token: 0x040011B4 RID: 4532
			internal const int cmdidObjectVerbList2 = 139;

			// Token: 0x040011B5 RID: 4533
			internal const int cmdidObjectVerbList3 = 140;

			// Token: 0x040011B6 RID: 4534
			internal const int cmdidObjectVerbList4 = 141;

			// Token: 0x040011B7 RID: 4535
			internal const int cmdidObjectVerbList5 = 142;

			// Token: 0x040011B8 RID: 4536
			internal const int cmdidObjectVerbList6 = 143;

			// Token: 0x040011B9 RID: 4537
			internal const int cmdidObjectVerbList7 = 144;

			// Token: 0x040011BA RID: 4538
			internal const int cmdidObjectVerbList8 = 145;

			// Token: 0x040011BB RID: 4539
			internal const int cmdidObjectVerbList9 = 146;

			// Token: 0x040011BC RID: 4540
			internal const int cmdidConvertObject = 147;

			// Token: 0x040011BD RID: 4541
			internal const int cmdidCustomControl = 148;

			// Token: 0x040011BE RID: 4542
			internal const int cmdidCustomizeItem = 149;

			// Token: 0x040011BF RID: 4543
			internal const int cmdidRename = 150;

			// Token: 0x040011C0 RID: 4544
			internal const int cmdidImport = 151;

			// Token: 0x040011C1 RID: 4545
			internal const int cmdidNewPage = 152;

			// Token: 0x040011C2 RID: 4546
			internal const int cmdidMove = 153;

			// Token: 0x040011C3 RID: 4547
			internal const int cmdidCancel = 154;

			// Token: 0x040011C4 RID: 4548
			internal const int cmdidFont = 155;

			// Token: 0x040011C5 RID: 4549
			internal const int cmdidExpandLinks = 156;

			// Token: 0x040011C6 RID: 4550
			internal const int cmdidExpandImages = 157;

			// Token: 0x040011C7 RID: 4551
			internal const int cmdidExpandPages = 158;

			// Token: 0x040011C8 RID: 4552
			internal const int cmdidRefocusDiagram = 159;

			// Token: 0x040011C9 RID: 4553
			internal const int cmdidTransitiveClosure = 160;

			// Token: 0x040011CA RID: 4554
			internal const int cmdidCenterDiagram = 161;

			// Token: 0x040011CB RID: 4555
			internal const int cmdidZoomIn = 162;

			// Token: 0x040011CC RID: 4556
			internal const int cmdidZoomOut = 163;

			// Token: 0x040011CD RID: 4557
			internal const int cmdidRemoveFilter = 164;

			// Token: 0x040011CE RID: 4558
			internal const int cmdidHidePane = 165;

			// Token: 0x040011CF RID: 4559
			internal const int cmdidDeleteTable = 166;

			// Token: 0x040011D0 RID: 4560
			internal const int cmdidDeleteRelationship = 167;

			// Token: 0x040011D1 RID: 4561
			internal const int cmdidRemove = 168;

			// Token: 0x040011D2 RID: 4562
			internal const int cmdidJoinLeftAll = 169;

			// Token: 0x040011D3 RID: 4563
			internal const int cmdidJoinRightAll = 170;

			// Token: 0x040011D4 RID: 4564
			internal const int cmdidAddToOutput = 171;

			// Token: 0x040011D5 RID: 4565
			internal const int cmdidOtherQuery = 172;

			// Token: 0x040011D6 RID: 4566
			internal const int cmdidGenerateChangeScript = 173;

			// Token: 0x040011D7 RID: 4567
			internal const int cmdidSaveSelection = 174;

			// Token: 0x040011D8 RID: 4568
			internal const int cmdidAutojoinCurrent = 175;

			// Token: 0x040011D9 RID: 4569
			internal const int cmdidAutojoinAlways = 176;

			// Token: 0x040011DA RID: 4570
			internal const int cmdidEditPage = 177;

			// Token: 0x040011DB RID: 4571
			internal const int cmdidViewLinks = 178;

			// Token: 0x040011DC RID: 4572
			internal const int cmdidStop = 179;

			// Token: 0x040011DD RID: 4573
			internal const int cmdidPause = 180;

			// Token: 0x040011DE RID: 4574
			internal const int cmdidResume = 181;

			// Token: 0x040011DF RID: 4575
			internal const int cmdidFilterDiagram = 182;

			// Token: 0x040011E0 RID: 4576
			internal const int cmdidShowAllObjects = 183;

			// Token: 0x040011E1 RID: 4577
			internal const int cmdidShowApplications = 184;

			// Token: 0x040011E2 RID: 4578
			internal const int cmdidShowOtherObjects = 185;

			// Token: 0x040011E3 RID: 4579
			internal const int cmdidShowPrimRelationships = 186;

			// Token: 0x040011E4 RID: 4580
			internal const int cmdidExpand = 187;

			// Token: 0x040011E5 RID: 4581
			internal const int cmdidCollapse = 188;

			// Token: 0x040011E6 RID: 4582
			internal const int cmdidRefresh = 189;

			// Token: 0x040011E7 RID: 4583
			internal const int cmdidLayout = 190;

			// Token: 0x040011E8 RID: 4584
			internal const int cmdidShowResources = 191;

			// Token: 0x040011E9 RID: 4585
			internal const int cmdidInsertHTMLWizard = 192;

			// Token: 0x040011EA RID: 4586
			internal const int cmdidShowDownloads = 193;

			// Token: 0x040011EB RID: 4587
			internal const int cmdidShowExternals = 194;

			// Token: 0x040011EC RID: 4588
			internal const int cmdidShowInBoundLinks = 195;

			// Token: 0x040011ED RID: 4589
			internal const int cmdidShowOutBoundLinks = 196;

			// Token: 0x040011EE RID: 4590
			internal const int cmdidShowInAndOutBoundLinks = 197;

			// Token: 0x040011EF RID: 4591
			internal const int cmdidPreview = 198;

			// Token: 0x040011F0 RID: 4592
			internal const int cmdidOpen = 261;

			// Token: 0x040011F1 RID: 4593
			internal const int cmdidOpenWith = 199;

			// Token: 0x040011F2 RID: 4594
			internal const int cmdidShowPages = 200;

			// Token: 0x040011F3 RID: 4595
			internal const int cmdidRunQuery = 201;

			// Token: 0x040011F4 RID: 4596
			internal const int cmdidClearQuery = 202;

			// Token: 0x040011F5 RID: 4597
			internal const int cmdidRecordFirst = 203;

			// Token: 0x040011F6 RID: 4598
			internal const int cmdidRecordLast = 204;

			// Token: 0x040011F7 RID: 4599
			internal const int cmdidRecordNext = 205;

			// Token: 0x040011F8 RID: 4600
			internal const int cmdidRecordPrevious = 206;

			// Token: 0x040011F9 RID: 4601
			internal const int cmdidRecordGoto = 207;

			// Token: 0x040011FA RID: 4602
			internal const int cmdidRecordNew = 208;

			// Token: 0x040011FB RID: 4603
			internal const int cmdidInsertNewMenu = 209;

			// Token: 0x040011FC RID: 4604
			internal const int cmdidInsertSeparator = 210;

			// Token: 0x040011FD RID: 4605
			internal const int cmdidEditMenuNames = 211;

			// Token: 0x040011FE RID: 4606
			internal const int cmdidDebugExplorer = 212;

			// Token: 0x040011FF RID: 4607
			internal const int cmdidDebugProcesses = 213;

			// Token: 0x04001200 RID: 4608
			internal const int cmdidViewThreadsWindow = 214;

			// Token: 0x04001201 RID: 4609
			internal const int cmdidWindowUIList = 215;

			// Token: 0x04001202 RID: 4610
			internal const int cmdidNewProject = 216;

			// Token: 0x04001203 RID: 4611
			internal const int cmdidOpenProject = 217;

			// Token: 0x04001204 RID: 4612
			internal const int cmdidOpenSolution = 218;

			// Token: 0x04001205 RID: 4613
			internal const int cmdidCloseSolution = 219;

			// Token: 0x04001206 RID: 4614
			internal const int cmdidFileNew = 221;

			// Token: 0x04001207 RID: 4615
			internal const int cmdidFileOpen = 222;

			// Token: 0x04001208 RID: 4616
			internal const int cmdidFileClose = 223;

			// Token: 0x04001209 RID: 4617
			internal const int cmdidSaveSolution = 224;

			// Token: 0x0400120A RID: 4618
			internal const int cmdidSaveSolutionAs = 225;

			// Token: 0x0400120B RID: 4619
			internal const int cmdidSaveProjectItemAs = 226;

			// Token: 0x0400120C RID: 4620
			internal const int cmdidPageSetup = 227;

			// Token: 0x0400120D RID: 4621
			internal const int cmdidPrintPreview = 228;

			// Token: 0x0400120E RID: 4622
			internal const int cmdidExit = 229;

			// Token: 0x0400120F RID: 4623
			internal const int cmdidReplace = 230;

			// Token: 0x04001210 RID: 4624
			internal const int cmdidGoto = 231;

			// Token: 0x04001211 RID: 4625
			internal const int cmdidPropertyPages = 232;

			// Token: 0x04001212 RID: 4626
			internal const int cmdidFullScreen = 233;

			// Token: 0x04001213 RID: 4627
			internal const int cmdidProjectExplorer = 234;

			// Token: 0x04001214 RID: 4628
			internal const int cmdidPropertiesWindow = 235;

			// Token: 0x04001215 RID: 4629
			internal const int cmdidTaskListWindow = 236;

			// Token: 0x04001216 RID: 4630
			internal const int cmdidOutputWindow = 237;

			// Token: 0x04001217 RID: 4631
			internal const int cmdidObjectBrowser = 238;

			// Token: 0x04001218 RID: 4632
			internal const int cmdidDocOutlineWindow = 239;

			// Token: 0x04001219 RID: 4633
			internal const int cmdidImmediateWindow = 240;

			// Token: 0x0400121A RID: 4634
			internal const int cmdidWatchWindow = 241;

			// Token: 0x0400121B RID: 4635
			internal const int cmdidLocalsWindow = 242;

			// Token: 0x0400121C RID: 4636
			internal const int cmdidCallStack = 243;

			// Token: 0x0400121D RID: 4637
			internal const int cmdidAutosWindow = 747;

			// Token: 0x0400121E RID: 4638
			internal const int cmdidThisWindow = 748;

			// Token: 0x0400121F RID: 4639
			internal const int cmdidAddNewItem = 220;

			// Token: 0x04001220 RID: 4640
			internal const int cmdidAddExistingItem = 244;

			// Token: 0x04001221 RID: 4641
			internal const int cmdidNewFolder = 245;

			// Token: 0x04001222 RID: 4642
			internal const int cmdidSetStartupProject = 246;

			// Token: 0x04001223 RID: 4643
			internal const int cmdidProjectSettings = 247;

			// Token: 0x04001224 RID: 4644
			internal const int cmdidProjectReferences = 367;

			// Token: 0x04001225 RID: 4645
			internal const int cmdidStepInto = 248;

			// Token: 0x04001226 RID: 4646
			internal const int cmdidStepOver = 249;

			// Token: 0x04001227 RID: 4647
			internal const int cmdidStepOut = 250;

			// Token: 0x04001228 RID: 4648
			internal const int cmdidRunToCursor = 251;

			// Token: 0x04001229 RID: 4649
			internal const int cmdidAddWatch = 252;

			// Token: 0x0400122A RID: 4650
			internal const int cmdidEditWatch = 253;

			// Token: 0x0400122B RID: 4651
			internal const int cmdidQuickWatch = 254;

			// Token: 0x0400122C RID: 4652
			internal const int cmdidToggleBreakpoint = 255;

			// Token: 0x0400122D RID: 4653
			internal const int cmdidClearBreakpoints = 256;

			// Token: 0x0400122E RID: 4654
			internal const int cmdidShowBreakpoints = 257;

			// Token: 0x0400122F RID: 4655
			internal const int cmdidSetNextStatement = 258;

			// Token: 0x04001230 RID: 4656
			internal const int cmdidShowNextStatement = 259;

			// Token: 0x04001231 RID: 4657
			internal const int cmdidEditBreakpoint = 260;

			// Token: 0x04001232 RID: 4658
			internal const int cmdidDetachDebugger = 262;

			// Token: 0x04001233 RID: 4659
			internal const int cmdidCustomizeKeyboard = 263;

			// Token: 0x04001234 RID: 4660
			internal const int cmdidToolsOptions = 264;

			// Token: 0x04001235 RID: 4661
			internal const int cmdidNewWindow = 265;

			// Token: 0x04001236 RID: 4662
			internal const int cmdidSplit = 266;

			// Token: 0x04001237 RID: 4663
			internal const int cmdidCascade = 267;

			// Token: 0x04001238 RID: 4664
			internal const int cmdidTileHorz = 268;

			// Token: 0x04001239 RID: 4665
			internal const int cmdidTileVert = 269;

			// Token: 0x0400123A RID: 4666
			internal const int cmdidTechSupport = 270;

			// Token: 0x0400123B RID: 4667
			internal const int cmdidAbout = 271;

			// Token: 0x0400123C RID: 4668
			internal const int cmdidDebugOptions = 272;

			// Token: 0x0400123D RID: 4669
			internal const int cmdidDeleteWatch = 274;

			// Token: 0x0400123E RID: 4670
			internal const int cmdidCollapseWatch = 275;

			// Token: 0x0400123F RID: 4671
			internal const int cmdidPbrsToggleStatus = 282;

			// Token: 0x04001240 RID: 4672
			internal const int cmdidPropbrsHide = 283;

			// Token: 0x04001241 RID: 4673
			internal const int cmdidDockingView = 284;

			// Token: 0x04001242 RID: 4674
			internal const int cmdidHideActivePane = 285;

			// Token: 0x04001243 RID: 4675
			internal const int cmdidPaneNextTab = 286;

			// Token: 0x04001244 RID: 4676
			internal const int cmdidPanePrevTab = 287;

			// Token: 0x04001245 RID: 4677
			internal const int cmdidPaneCloseToolWindow = 288;

			// Token: 0x04001246 RID: 4678
			internal const int cmdidPaneActivateDocWindow = 289;

			// Token: 0x04001247 RID: 4679
			internal const int cmdidDockingViewFloater = 291;

			// Token: 0x04001248 RID: 4680
			internal const int cmdidAutoHideWindow = 292;

			// Token: 0x04001249 RID: 4681
			internal const int cmdidMoveToDropdownBar = 293;

			// Token: 0x0400124A RID: 4682
			internal const int cmdidFindCmd = 294;

			// Token: 0x0400124B RID: 4683
			internal const int cmdidStart = 295;

			// Token: 0x0400124C RID: 4684
			internal const int cmdidRestart = 296;

			// Token: 0x0400124D RID: 4685
			internal const int cmdidAddinManager = 297;

			// Token: 0x0400124E RID: 4686
			internal const int cmdidMultiLevelUndoList = 298;

			// Token: 0x0400124F RID: 4687
			internal const int cmdidMultiLevelRedoList = 299;

			// Token: 0x04001250 RID: 4688
			internal const int cmdidToolboxAddTab = 300;

			// Token: 0x04001251 RID: 4689
			internal const int cmdidToolboxDeleteTab = 301;

			// Token: 0x04001252 RID: 4690
			internal const int cmdidToolboxRenameTab = 302;

			// Token: 0x04001253 RID: 4691
			internal const int cmdidToolboxTabMoveUp = 303;

			// Token: 0x04001254 RID: 4692
			internal const int cmdidToolboxTabMoveDown = 304;

			// Token: 0x04001255 RID: 4693
			internal const int cmdidToolboxRenameItem = 305;

			// Token: 0x04001256 RID: 4694
			internal const int cmdidToolboxListView = 306;

			// Token: 0x04001257 RID: 4695
			internal const int cmdidWindowUIGetList = 308;

			// Token: 0x04001258 RID: 4696
			internal const int cmdidInsertValuesQuery = 309;

			// Token: 0x04001259 RID: 4697
			internal const int cmdidShowProperties = 310;

			// Token: 0x0400125A RID: 4698
			internal const int cmdidThreadSuspend = 311;

			// Token: 0x0400125B RID: 4699
			internal const int cmdidThreadResume = 312;

			// Token: 0x0400125C RID: 4700
			internal const int cmdidThreadSetFocus = 313;

			// Token: 0x0400125D RID: 4701
			internal const int cmdidDisplayRadix = 314;

			// Token: 0x0400125E RID: 4702
			internal const int cmdidOpenProjectItem = 315;

			// Token: 0x0400125F RID: 4703
			internal const int cmdidPaneNextPane = 316;

			// Token: 0x04001260 RID: 4704
			internal const int cmdidPanePrevPane = 317;

			// Token: 0x04001261 RID: 4705
			internal const int cmdidClearPane = 318;

			// Token: 0x04001262 RID: 4706
			internal const int cmdidGotoErrorTag = 319;

			// Token: 0x04001263 RID: 4707
			internal const int cmdidTaskListSortByCategory = 320;

			// Token: 0x04001264 RID: 4708
			internal const int cmdidTaskListSortByFileLine = 321;

			// Token: 0x04001265 RID: 4709
			internal const int cmdidTaskListSortByPriority = 322;

			// Token: 0x04001266 RID: 4710
			internal const int cmdidTaskListSortByDefaultSort = 323;

			// Token: 0x04001267 RID: 4711
			internal const int cmdidTaskListFilterByNothing = 325;

			// Token: 0x04001268 RID: 4712
			internal const int cmdidTaskListFilterByCategoryCodeSense = 326;

			// Token: 0x04001269 RID: 4713
			internal const int cmdidTaskListFilterByCategoryCompiler = 327;

			// Token: 0x0400126A RID: 4714
			internal const int cmdidTaskListFilterByCategoryComment = 328;

			// Token: 0x0400126B RID: 4715
			internal const int cmdidToolboxAddItem = 329;

			// Token: 0x0400126C RID: 4716
			internal const int cmdidToolboxReset = 330;

			// Token: 0x0400126D RID: 4717
			internal const int cmdidSaveProjectItem = 331;

			// Token: 0x0400126E RID: 4718
			internal const int cmdidViewForm = 332;

			// Token: 0x0400126F RID: 4719
			internal const int cmdidViewCode = 333;

			// Token: 0x04001270 RID: 4720
			internal const int cmdidPreviewInBrowser = 334;

			// Token: 0x04001271 RID: 4721
			internal const int cmdidBrowseWith = 336;

			// Token: 0x04001272 RID: 4722
			internal const int cmdidSearchSetCombo = 307;

			// Token: 0x04001273 RID: 4723
			internal const int cmdidSearchCombo = 337;

			// Token: 0x04001274 RID: 4724
			internal const int cmdidEditLabel = 338;

			// Token: 0x04001275 RID: 4725
			internal const int cmdidExceptions = 339;

			// Token: 0x04001276 RID: 4726
			internal const int cmdidToggleSelMode = 341;

			// Token: 0x04001277 RID: 4727
			internal const int cmdidToggleInsMode = 342;

			// Token: 0x04001278 RID: 4728
			internal const int cmdidLoadUnloadedProject = 343;

			// Token: 0x04001279 RID: 4729
			internal const int cmdidUnloadLoadedProject = 344;

			// Token: 0x0400127A RID: 4730
			internal const int cmdidElasticColumn = 345;

			// Token: 0x0400127B RID: 4731
			internal const int cmdidHideColumn = 346;

			// Token: 0x0400127C RID: 4732
			internal const int cmdidTaskListPreviousView = 347;

			// Token: 0x0400127D RID: 4733
			internal const int cmdidZoomDialog = 348;

			// Token: 0x0400127E RID: 4734
			internal const int cmdidFindNew = 349;

			// Token: 0x0400127F RID: 4735
			internal const int cmdidFindMatchCase = 350;

			// Token: 0x04001280 RID: 4736
			internal const int cmdidFindWholeWord = 351;

			// Token: 0x04001281 RID: 4737
			internal const int cmdidFindSimplePattern = 276;

			// Token: 0x04001282 RID: 4738
			internal const int cmdidFindRegularExpression = 352;

			// Token: 0x04001283 RID: 4739
			internal const int cmdidFindBackwards = 353;

			// Token: 0x04001284 RID: 4740
			internal const int cmdidFindInSelection = 354;

			// Token: 0x04001285 RID: 4741
			internal const int cmdidFindStop = 355;

			// Token: 0x04001286 RID: 4742
			internal const int cmdidFindHelp = 356;

			// Token: 0x04001287 RID: 4743
			internal const int cmdidFindInFiles = 277;

			// Token: 0x04001288 RID: 4744
			internal const int cmdidReplaceInFiles = 278;

			// Token: 0x04001289 RID: 4745
			internal const int cmdidNextLocation = 279;

			// Token: 0x0400128A RID: 4746
			internal const int cmdidPreviousLocation = 280;

			// Token: 0x0400128B RID: 4747
			internal const int cmdidTaskListNextError = 357;

			// Token: 0x0400128C RID: 4748
			internal const int cmdidTaskListPrevError = 358;

			// Token: 0x0400128D RID: 4749
			internal const int cmdidTaskListFilterByCategoryUser = 359;

			// Token: 0x0400128E RID: 4750
			internal const int cmdidTaskListFilterByCategoryShortcut = 360;

			// Token: 0x0400128F RID: 4751
			internal const int cmdidTaskListFilterByCategoryHTML = 361;

			// Token: 0x04001290 RID: 4752
			internal const int cmdidTaskListFilterByCurrentFile = 362;

			// Token: 0x04001291 RID: 4753
			internal const int cmdidTaskListFilterByChecked = 363;

			// Token: 0x04001292 RID: 4754
			internal const int cmdidTaskListFilterByUnchecked = 364;

			// Token: 0x04001293 RID: 4755
			internal const int cmdidTaskListSortByDescription = 365;

			// Token: 0x04001294 RID: 4756
			internal const int cmdidTaskListSortByChecked = 366;

			// Token: 0x04001295 RID: 4757
			internal const int cmdidStartNoDebug = 368;

			// Token: 0x04001296 RID: 4758
			internal const int cmdidFindNext = 370;

			// Token: 0x04001297 RID: 4759
			internal const int cmdidFindPrev = 371;

			// Token: 0x04001298 RID: 4760
			internal const int cmdidFindSelectedNext = 372;

			// Token: 0x04001299 RID: 4761
			internal const int cmdidFindSelectedPrev = 373;

			// Token: 0x0400129A RID: 4762
			internal const int cmdidSearchGetList = 374;

			// Token: 0x0400129B RID: 4763
			internal const int cmdidInsertBreakpoint = 375;

			// Token: 0x0400129C RID: 4764
			internal const int cmdidEnableBreakpoint = 376;

			// Token: 0x0400129D RID: 4765
			internal const int cmdidF1Help = 377;

			// Token: 0x0400129E RID: 4766
			internal const int cmdidPropSheetOrProperties = 397;

			// Token: 0x0400129F RID: 4767
			internal const int cmdidTshellStep = 398;

			// Token: 0x040012A0 RID: 4768
			internal const int cmdidTshellRun = 399;

			// Token: 0x040012A1 RID: 4769
			internal const int cmdidMarkerCmd0 = 400;

			// Token: 0x040012A2 RID: 4770
			internal const int cmdidMarkerCmd1 = 401;

			// Token: 0x040012A3 RID: 4771
			internal const int cmdidMarkerCmd2 = 402;

			// Token: 0x040012A4 RID: 4772
			internal const int cmdidMarkerCmd3 = 403;

			// Token: 0x040012A5 RID: 4773
			internal const int cmdidMarkerCmd4 = 404;

			// Token: 0x040012A6 RID: 4774
			internal const int cmdidMarkerCmd5 = 405;

			// Token: 0x040012A7 RID: 4775
			internal const int cmdidMarkerCmd6 = 406;

			// Token: 0x040012A8 RID: 4776
			internal const int cmdidMarkerCmd7 = 407;

			// Token: 0x040012A9 RID: 4777
			internal const int cmdidMarkerCmd8 = 408;

			// Token: 0x040012AA RID: 4778
			internal const int cmdidMarkerCmd9 = 409;

			// Token: 0x040012AB RID: 4779
			internal const int cmdidMarkerLast = 409;

			// Token: 0x040012AC RID: 4780
			internal const int cmdidMarkerEnd = 410;

			// Token: 0x040012AD RID: 4781
			internal const int cmdidReloadProject = 412;

			// Token: 0x040012AE RID: 4782
			internal const int cmdidUnloadProject = 413;

			// Token: 0x040012AF RID: 4783
			internal const int cmdidDetachAttachOutline = 420;

			// Token: 0x040012B0 RID: 4784
			internal const int cmdidShowHideOutline = 421;

			// Token: 0x040012B1 RID: 4785
			internal const int cmdidSyncOutline = 422;

			// Token: 0x040012B2 RID: 4786
			internal const int cmdidRunToCallstCursor = 423;

			// Token: 0x040012B3 RID: 4787
			internal const int cmdidNoCmdsAvailable = 424;

			// Token: 0x040012B4 RID: 4788
			internal const int cmdidContextWindow = 427;

			// Token: 0x040012B5 RID: 4789
			internal const int cmdidAlias = 428;

			// Token: 0x040012B6 RID: 4790
			internal const int cmdidGotoCommandLine = 429;

			// Token: 0x040012B7 RID: 4791
			internal const int cmdidEvaluateExpression = 430;

			// Token: 0x040012B8 RID: 4792
			internal const int cmdidImmediateMode = 431;

			// Token: 0x040012B9 RID: 4793
			internal const int cmdidEvaluateStatement = 432;

			// Token: 0x040012BA RID: 4794
			internal const int cmdidFindResultWindow1 = 433;

			// Token: 0x040012BB RID: 4795
			internal const int cmdidFindResultWindow2 = 434;

			// Token: 0x040012BC RID: 4796
			internal const int cmdidWindow1 = 570;

			// Token: 0x040012BD RID: 4797
			internal const int cmdidWindow2 = 571;

			// Token: 0x040012BE RID: 4798
			internal const int cmdidWindow3 = 572;

			// Token: 0x040012BF RID: 4799
			internal const int cmdidWindow4 = 573;

			// Token: 0x040012C0 RID: 4800
			internal const int cmdidWindow5 = 574;

			// Token: 0x040012C1 RID: 4801
			internal const int cmdidWindow6 = 575;

			// Token: 0x040012C2 RID: 4802
			internal const int cmdidWindow7 = 576;

			// Token: 0x040012C3 RID: 4803
			internal const int cmdidWindow8 = 577;

			// Token: 0x040012C4 RID: 4804
			internal const int cmdidWindow9 = 578;

			// Token: 0x040012C5 RID: 4805
			internal const int cmdidWindow10 = 579;

			// Token: 0x040012C6 RID: 4806
			internal const int cmdidWindow11 = 580;

			// Token: 0x040012C7 RID: 4807
			internal const int cmdidWindow12 = 581;

			// Token: 0x040012C8 RID: 4808
			internal const int cmdidWindow13 = 582;

			// Token: 0x040012C9 RID: 4809
			internal const int cmdidWindow14 = 583;

			// Token: 0x040012CA RID: 4810
			internal const int cmdidWindow15 = 584;

			// Token: 0x040012CB RID: 4811
			internal const int cmdidWindow16 = 585;

			// Token: 0x040012CC RID: 4812
			internal const int cmdidWindow17 = 586;

			// Token: 0x040012CD RID: 4813
			internal const int cmdidWindow18 = 587;

			// Token: 0x040012CE RID: 4814
			internal const int cmdidWindow19 = 588;

			// Token: 0x040012CF RID: 4815
			internal const int cmdidWindow20 = 589;

			// Token: 0x040012D0 RID: 4816
			internal const int cmdidWindow21 = 590;

			// Token: 0x040012D1 RID: 4817
			internal const int cmdidWindow22 = 591;

			// Token: 0x040012D2 RID: 4818
			internal const int cmdidWindow23 = 592;

			// Token: 0x040012D3 RID: 4819
			internal const int cmdidWindow24 = 593;

			// Token: 0x040012D4 RID: 4820
			internal const int cmdidWindow25 = 594;

			// Token: 0x040012D5 RID: 4821
			internal const int cmdidMoreWindows = 595;

			// Token: 0x040012D6 RID: 4822
			internal const int cmdidTaskListTaskHelp = 598;

			// Token: 0x040012D7 RID: 4823
			internal const int cmdidClassView = 599;

			// Token: 0x040012D8 RID: 4824
			internal const int cmdidMRUProj1 = 600;

			// Token: 0x040012D9 RID: 4825
			internal const int cmdidMRUProj2 = 601;

			// Token: 0x040012DA RID: 4826
			internal const int cmdidMRUProj3 = 602;

			// Token: 0x040012DB RID: 4827
			internal const int cmdidMRUProj4 = 603;

			// Token: 0x040012DC RID: 4828
			internal const int cmdidMRUProj5 = 604;

			// Token: 0x040012DD RID: 4829
			internal const int cmdidMRUProj6 = 605;

			// Token: 0x040012DE RID: 4830
			internal const int cmdidMRUProj7 = 606;

			// Token: 0x040012DF RID: 4831
			internal const int cmdidMRUProj8 = 607;

			// Token: 0x040012E0 RID: 4832
			internal const int cmdidMRUProj9 = 608;

			// Token: 0x040012E1 RID: 4833
			internal const int cmdidMRUProj10 = 609;

			// Token: 0x040012E2 RID: 4834
			internal const int cmdidMRUProj11 = 610;

			// Token: 0x040012E3 RID: 4835
			internal const int cmdidMRUProj12 = 611;

			// Token: 0x040012E4 RID: 4836
			internal const int cmdidMRUProj13 = 612;

			// Token: 0x040012E5 RID: 4837
			internal const int cmdidMRUProj14 = 613;

			// Token: 0x040012E6 RID: 4838
			internal const int cmdidMRUProj15 = 614;

			// Token: 0x040012E7 RID: 4839
			internal const int cmdidMRUProj16 = 615;

			// Token: 0x040012E8 RID: 4840
			internal const int cmdidMRUProj17 = 616;

			// Token: 0x040012E9 RID: 4841
			internal const int cmdidMRUProj18 = 617;

			// Token: 0x040012EA RID: 4842
			internal const int cmdidMRUProj19 = 618;

			// Token: 0x040012EB RID: 4843
			internal const int cmdidMRUProj20 = 619;

			// Token: 0x040012EC RID: 4844
			internal const int cmdidMRUProj21 = 620;

			// Token: 0x040012ED RID: 4845
			internal const int cmdidMRUProj22 = 621;

			// Token: 0x040012EE RID: 4846
			internal const int cmdidMRUProj23 = 622;

			// Token: 0x040012EF RID: 4847
			internal const int cmdidMRUProj24 = 623;

			// Token: 0x040012F0 RID: 4848
			internal const int cmdidMRUProj25 = 624;

			// Token: 0x040012F1 RID: 4849
			internal const int cmdidSplitNext = 625;

			// Token: 0x040012F2 RID: 4850
			internal const int cmdidSplitPrev = 626;

			// Token: 0x040012F3 RID: 4851
			internal const int cmdidCloseAllDocuments = 627;

			// Token: 0x040012F4 RID: 4852
			internal const int cmdidNextDocument = 628;

			// Token: 0x040012F5 RID: 4853
			internal const int cmdidPrevDocument = 629;

			// Token: 0x040012F6 RID: 4854
			internal const int cmdidTool1 = 630;

			// Token: 0x040012F7 RID: 4855
			internal const int cmdidTool2 = 631;

			// Token: 0x040012F8 RID: 4856
			internal const int cmdidTool3 = 632;

			// Token: 0x040012F9 RID: 4857
			internal const int cmdidTool4 = 633;

			// Token: 0x040012FA RID: 4858
			internal const int cmdidTool5 = 634;

			// Token: 0x040012FB RID: 4859
			internal const int cmdidTool6 = 635;

			// Token: 0x040012FC RID: 4860
			internal const int cmdidTool7 = 636;

			// Token: 0x040012FD RID: 4861
			internal const int cmdidTool8 = 637;

			// Token: 0x040012FE RID: 4862
			internal const int cmdidTool9 = 638;

			// Token: 0x040012FF RID: 4863
			internal const int cmdidTool10 = 639;

			// Token: 0x04001300 RID: 4864
			internal const int cmdidTool11 = 640;

			// Token: 0x04001301 RID: 4865
			internal const int cmdidTool12 = 641;

			// Token: 0x04001302 RID: 4866
			internal const int cmdidTool13 = 642;

			// Token: 0x04001303 RID: 4867
			internal const int cmdidTool14 = 643;

			// Token: 0x04001304 RID: 4868
			internal const int cmdidTool15 = 644;

			// Token: 0x04001305 RID: 4869
			internal const int cmdidTool16 = 645;

			// Token: 0x04001306 RID: 4870
			internal const int cmdidTool17 = 646;

			// Token: 0x04001307 RID: 4871
			internal const int cmdidTool18 = 647;

			// Token: 0x04001308 RID: 4872
			internal const int cmdidTool19 = 648;

			// Token: 0x04001309 RID: 4873
			internal const int cmdidTool20 = 649;

			// Token: 0x0400130A RID: 4874
			internal const int cmdidTool21 = 650;

			// Token: 0x0400130B RID: 4875
			internal const int cmdidTool22 = 651;

			// Token: 0x0400130C RID: 4876
			internal const int cmdidTool23 = 652;

			// Token: 0x0400130D RID: 4877
			internal const int cmdidTool24 = 653;

			// Token: 0x0400130E RID: 4878
			internal const int cmdidExternalCommands = 654;

			// Token: 0x0400130F RID: 4879
			internal const int cmdidPasteNextTBXCBItem = 655;

			// Token: 0x04001310 RID: 4880
			internal const int cmdidToolboxShowAllTabs = 656;

			// Token: 0x04001311 RID: 4881
			internal const int cmdidProjectDependencies = 657;

			// Token: 0x04001312 RID: 4882
			internal const int cmdidCloseDocument = 658;

			// Token: 0x04001313 RID: 4883
			internal const int cmdidToolboxSortItems = 659;

			// Token: 0x04001314 RID: 4884
			internal const int cmdidViewBarView1 = 660;

			// Token: 0x04001315 RID: 4885
			internal const int cmdidViewBarView2 = 661;

			// Token: 0x04001316 RID: 4886
			internal const int cmdidViewBarView3 = 662;

			// Token: 0x04001317 RID: 4887
			internal const int cmdidViewBarView4 = 663;

			// Token: 0x04001318 RID: 4888
			internal const int cmdidViewBarView5 = 664;

			// Token: 0x04001319 RID: 4889
			internal const int cmdidViewBarView6 = 665;

			// Token: 0x0400131A RID: 4890
			internal const int cmdidViewBarView7 = 666;

			// Token: 0x0400131B RID: 4891
			internal const int cmdidViewBarView8 = 667;

			// Token: 0x0400131C RID: 4892
			internal const int cmdidViewBarView9 = 668;

			// Token: 0x0400131D RID: 4893
			internal const int cmdidViewBarView10 = 669;

			// Token: 0x0400131E RID: 4894
			internal const int cmdidViewBarView11 = 670;

			// Token: 0x0400131F RID: 4895
			internal const int cmdidViewBarView12 = 671;

			// Token: 0x04001320 RID: 4896
			internal const int cmdidViewBarView13 = 672;

			// Token: 0x04001321 RID: 4897
			internal const int cmdidViewBarView14 = 673;

			// Token: 0x04001322 RID: 4898
			internal const int cmdidViewBarView15 = 674;

			// Token: 0x04001323 RID: 4899
			internal const int cmdidViewBarView16 = 675;

			// Token: 0x04001324 RID: 4900
			internal const int cmdidViewBarView17 = 676;

			// Token: 0x04001325 RID: 4901
			internal const int cmdidViewBarView18 = 677;

			// Token: 0x04001326 RID: 4902
			internal const int cmdidViewBarView19 = 678;

			// Token: 0x04001327 RID: 4903
			internal const int cmdidViewBarView20 = 679;

			// Token: 0x04001328 RID: 4904
			internal const int cmdidViewBarView21 = 680;

			// Token: 0x04001329 RID: 4905
			internal const int cmdidViewBarView22 = 681;

			// Token: 0x0400132A RID: 4906
			internal const int cmdidViewBarView23 = 682;

			// Token: 0x0400132B RID: 4907
			internal const int cmdidViewBarView24 = 683;

			// Token: 0x0400132C RID: 4908
			internal const int cmdidSolutionCfg = 684;

			// Token: 0x0400132D RID: 4909
			internal const int cmdidSolutionCfgGetList = 685;

			// Token: 0x0400132E RID: 4910
			internal const int cmdidManageIndexes = 675;

			// Token: 0x0400132F RID: 4911
			internal const int cmdidManageRelationships = 676;

			// Token: 0x04001330 RID: 4912
			internal const int cmdidManageConstraints = 677;

			// Token: 0x04001331 RID: 4913
			internal const int cmdidTaskListCustomView1 = 678;

			// Token: 0x04001332 RID: 4914
			internal const int cmdidTaskListCustomView2 = 679;

			// Token: 0x04001333 RID: 4915
			internal const int cmdidTaskListCustomView3 = 680;

			// Token: 0x04001334 RID: 4916
			internal const int cmdidTaskListCustomView4 = 681;

			// Token: 0x04001335 RID: 4917
			internal const int cmdidTaskListCustomView5 = 682;

			// Token: 0x04001336 RID: 4918
			internal const int cmdidTaskListCustomView6 = 683;

			// Token: 0x04001337 RID: 4919
			internal const int cmdidTaskListCustomView7 = 684;

			// Token: 0x04001338 RID: 4920
			internal const int cmdidTaskListCustomView8 = 685;

			// Token: 0x04001339 RID: 4921
			internal const int cmdidTaskListCustomView9 = 686;

			// Token: 0x0400133A RID: 4922
			internal const int cmdidTaskListCustomView10 = 687;

			// Token: 0x0400133B RID: 4923
			internal const int cmdidTaskListCustomView11 = 688;

			// Token: 0x0400133C RID: 4924
			internal const int cmdidTaskListCustomView12 = 689;

			// Token: 0x0400133D RID: 4925
			internal const int cmdidTaskListCustomView13 = 690;

			// Token: 0x0400133E RID: 4926
			internal const int cmdidTaskListCustomView14 = 691;

			// Token: 0x0400133F RID: 4927
			internal const int cmdidTaskListCustomView15 = 692;

			// Token: 0x04001340 RID: 4928
			internal const int cmdidTaskListCustomView16 = 693;

			// Token: 0x04001341 RID: 4929
			internal const int cmdidTaskListCustomView17 = 694;

			// Token: 0x04001342 RID: 4930
			internal const int cmdidTaskListCustomView18 = 695;

			// Token: 0x04001343 RID: 4931
			internal const int cmdidTaskListCustomView19 = 696;

			// Token: 0x04001344 RID: 4932
			internal const int cmdidTaskListCustomView20 = 697;

			// Token: 0x04001345 RID: 4933
			internal const int cmdidTaskListCustomView21 = 698;

			// Token: 0x04001346 RID: 4934
			internal const int cmdidTaskListCustomView22 = 699;

			// Token: 0x04001347 RID: 4935
			internal const int cmdidTaskListCustomView23 = 700;

			// Token: 0x04001348 RID: 4936
			internal const int cmdidTaskListCustomView24 = 701;

			// Token: 0x04001349 RID: 4937
			internal const int cmdidTaskListCustomView25 = 702;

			// Token: 0x0400134A RID: 4938
			internal const int cmdidTaskListCustomView26 = 703;

			// Token: 0x0400134B RID: 4939
			internal const int cmdidTaskListCustomView27 = 704;

			// Token: 0x0400134C RID: 4940
			internal const int cmdidTaskListCustomView28 = 705;

			// Token: 0x0400134D RID: 4941
			internal const int cmdidTaskListCustomView29 = 706;

			// Token: 0x0400134E RID: 4942
			internal const int cmdidTaskListCustomView30 = 707;

			// Token: 0x0400134F RID: 4943
			internal const int cmdidTaskListCustomView31 = 708;

			// Token: 0x04001350 RID: 4944
			internal const int cmdidTaskListCustomView32 = 709;

			// Token: 0x04001351 RID: 4945
			internal const int cmdidTaskListCustomView33 = 710;

			// Token: 0x04001352 RID: 4946
			internal const int cmdidTaskListCustomView34 = 711;

			// Token: 0x04001353 RID: 4947
			internal const int cmdidTaskListCustomView35 = 712;

			// Token: 0x04001354 RID: 4948
			internal const int cmdidTaskListCustomView36 = 713;

			// Token: 0x04001355 RID: 4949
			internal const int cmdidTaskListCustomView37 = 714;

			// Token: 0x04001356 RID: 4950
			internal const int cmdidTaskListCustomView38 = 715;

			// Token: 0x04001357 RID: 4951
			internal const int cmdidTaskListCustomView39 = 716;

			// Token: 0x04001358 RID: 4952
			internal const int cmdidTaskListCustomView40 = 717;

			// Token: 0x04001359 RID: 4953
			internal const int cmdidTaskListCustomView41 = 718;

			// Token: 0x0400135A RID: 4954
			internal const int cmdidTaskListCustomView42 = 719;

			// Token: 0x0400135B RID: 4955
			internal const int cmdidTaskListCustomView43 = 720;

			// Token: 0x0400135C RID: 4956
			internal const int cmdidTaskListCustomView44 = 721;

			// Token: 0x0400135D RID: 4957
			internal const int cmdidTaskListCustomView45 = 722;

			// Token: 0x0400135E RID: 4958
			internal const int cmdidTaskListCustomView46 = 723;

			// Token: 0x0400135F RID: 4959
			internal const int cmdidTaskListCustomView47 = 724;

			// Token: 0x04001360 RID: 4960
			internal const int cmdidTaskListCustomView48 = 725;

			// Token: 0x04001361 RID: 4961
			internal const int cmdidTaskListCustomView49 = 726;

			// Token: 0x04001362 RID: 4962
			internal const int cmdidTaskListCustomView50 = 727;

			// Token: 0x04001363 RID: 4963
			internal const int cmdidObjectSearch = 728;

			// Token: 0x04001364 RID: 4964
			internal const int cmdidCommandWindow = 729;

			// Token: 0x04001365 RID: 4965
			internal const int cmdidCommandWindowMarkMode = 730;

			// Token: 0x04001366 RID: 4966
			internal const int cmdidLogCommandWindow = 731;

			// Token: 0x04001367 RID: 4967
			internal const int cmdidShell = 732;

			// Token: 0x04001368 RID: 4968
			internal const int cmdidSingleChar = 733;

			// Token: 0x04001369 RID: 4969
			internal const int cmdidZeroOrMore = 734;

			// Token: 0x0400136A RID: 4970
			internal const int cmdidOneOrMore = 735;

			// Token: 0x0400136B RID: 4971
			internal const int cmdidBeginLine = 736;

			// Token: 0x0400136C RID: 4972
			internal const int cmdidEndLine = 737;

			// Token: 0x0400136D RID: 4973
			internal const int cmdidBeginWord = 738;

			// Token: 0x0400136E RID: 4974
			internal const int cmdidEndWord = 739;

			// Token: 0x0400136F RID: 4975
			internal const int cmdidCharInSet = 740;

			// Token: 0x04001370 RID: 4976
			internal const int cmdidCharNotInSet = 741;

			// Token: 0x04001371 RID: 4977
			internal const int cmdidOr = 742;

			// Token: 0x04001372 RID: 4978
			internal const int cmdidEscape = 743;

			// Token: 0x04001373 RID: 4979
			internal const int cmdidTagExp = 744;

			// Token: 0x04001374 RID: 4980
			internal const int cmdidPatternMatchHelp = 745;

			// Token: 0x04001375 RID: 4981
			internal const int cmdidRegExList = 746;

			// Token: 0x04001376 RID: 4982
			internal const int cmdidDebugReserved1 = 747;

			// Token: 0x04001377 RID: 4983
			internal const int cmdidDebugReserved2 = 748;

			// Token: 0x04001378 RID: 4984
			internal const int cmdidDebugReserved3 = 749;

			// Token: 0x04001379 RID: 4985
			internal const int cmdidWildZeroOrMore = 754;

			// Token: 0x0400137A RID: 4986
			internal const int cmdidWildSingleChar = 755;

			// Token: 0x0400137B RID: 4987
			internal const int cmdidWildSingleDigit = 756;

			// Token: 0x0400137C RID: 4988
			internal const int cmdidWildCharInSet = 757;

			// Token: 0x0400137D RID: 4989
			internal const int cmdidWildCharNotInSet = 758;

			// Token: 0x0400137E RID: 4990
			internal const int cmdidFindWhatText = 759;

			// Token: 0x0400137F RID: 4991
			internal const int cmdidTaggedExp1 = 760;

			// Token: 0x04001380 RID: 4992
			internal const int cmdidTaggedExp2 = 761;

			// Token: 0x04001381 RID: 4993
			internal const int cmdidTaggedExp3 = 762;

			// Token: 0x04001382 RID: 4994
			internal const int cmdidTaggedExp4 = 763;

			// Token: 0x04001383 RID: 4995
			internal const int cmdidTaggedExp5 = 764;

			// Token: 0x04001384 RID: 4996
			internal const int cmdidTaggedExp6 = 765;

			// Token: 0x04001385 RID: 4997
			internal const int cmdidTaggedExp7 = 766;

			// Token: 0x04001386 RID: 4998
			internal const int cmdidTaggedExp8 = 767;

			// Token: 0x04001387 RID: 4999
			internal const int cmdidTaggedExp9 = 768;

			// Token: 0x04001388 RID: 5000
			internal const int cmdidEditorWidgetClick = 769;

			// Token: 0x04001389 RID: 5001
			internal const int cmdidCmdWinUpdateAC = 770;

			// Token: 0x0400138A RID: 5002
			internal const int cmdidSlnCfgMgr = 771;

			// Token: 0x0400138B RID: 5003
			internal const int cmdidAddNewProject = 772;

			// Token: 0x0400138C RID: 5004
			internal const int cmdidAddExistingProject = 773;

			// Token: 0x0400138D RID: 5005
			internal const int cmdidAddNewSolutionItem = 774;

			// Token: 0x0400138E RID: 5006
			internal const int cmdidAddExistingSolutionItem = 775;

			// Token: 0x0400138F RID: 5007
			internal const int cmdidAutoHideContext1 = 776;

			// Token: 0x04001390 RID: 5008
			internal const int cmdidAutoHideContext2 = 777;

			// Token: 0x04001391 RID: 5009
			internal const int cmdidAutoHideContext3 = 778;

			// Token: 0x04001392 RID: 5010
			internal const int cmdidAutoHideContext4 = 779;

			// Token: 0x04001393 RID: 5011
			internal const int cmdidAutoHideContext5 = 780;

			// Token: 0x04001394 RID: 5012
			internal const int cmdidAutoHideContext6 = 781;

			// Token: 0x04001395 RID: 5013
			internal const int cmdidAutoHideContext7 = 782;

			// Token: 0x04001396 RID: 5014
			internal const int cmdidAutoHideContext8 = 783;

			// Token: 0x04001397 RID: 5015
			internal const int cmdidAutoHideContext9 = 784;

			// Token: 0x04001398 RID: 5016
			internal const int cmdidAutoHideContext10 = 785;

			// Token: 0x04001399 RID: 5017
			internal const int cmdidAutoHideContext11 = 786;

			// Token: 0x0400139A RID: 5018
			internal const int cmdidAutoHideContext12 = 787;

			// Token: 0x0400139B RID: 5019
			internal const int cmdidAutoHideContext13 = 788;

			// Token: 0x0400139C RID: 5020
			internal const int cmdidAutoHideContext14 = 789;

			// Token: 0x0400139D RID: 5021
			internal const int cmdidAutoHideContext15 = 790;

			// Token: 0x0400139E RID: 5022
			internal const int cmdidAutoHideContext16 = 791;

			// Token: 0x0400139F RID: 5023
			internal const int cmdidAutoHideContext17 = 792;

			// Token: 0x040013A0 RID: 5024
			internal const int cmdidAutoHideContext18 = 793;

			// Token: 0x040013A1 RID: 5025
			internal const int cmdidAutoHideContext19 = 794;

			// Token: 0x040013A2 RID: 5026
			internal const int cmdidAutoHideContext20 = 795;

			// Token: 0x040013A3 RID: 5027
			internal const int cmdidAutoHideContext21 = 796;

			// Token: 0x040013A4 RID: 5028
			internal const int cmdidAutoHideContext22 = 797;

			// Token: 0x040013A5 RID: 5029
			internal const int cmdidAutoHideContext23 = 798;

			// Token: 0x040013A6 RID: 5030
			internal const int cmdidAutoHideContext24 = 799;

			// Token: 0x040013A7 RID: 5031
			internal const int cmdidAutoHideContext25 = 800;

			// Token: 0x040013A8 RID: 5032
			internal const int cmdidAutoHideContext26 = 801;

			// Token: 0x040013A9 RID: 5033
			internal const int cmdidAutoHideContext27 = 802;

			// Token: 0x040013AA RID: 5034
			internal const int cmdidAutoHideContext28 = 803;

			// Token: 0x040013AB RID: 5035
			internal const int cmdidAutoHideContext29 = 804;

			// Token: 0x040013AC RID: 5036
			internal const int cmdidAutoHideContext30 = 805;

			// Token: 0x040013AD RID: 5037
			internal const int cmdidAutoHideContext31 = 806;

			// Token: 0x040013AE RID: 5038
			internal const int cmdidAutoHideContext32 = 807;

			// Token: 0x040013AF RID: 5039
			internal const int cmdidAutoHideContext33 = 808;

			// Token: 0x040013B0 RID: 5040
			internal const int cmdidShellNavBackward = 809;

			// Token: 0x040013B1 RID: 5041
			internal const int cmdidShellNavForward = 810;

			// Token: 0x040013B2 RID: 5042
			internal const int cmdidShellNavigate1 = 811;

			// Token: 0x040013B3 RID: 5043
			internal const int cmdidShellNavigate2 = 812;

			// Token: 0x040013B4 RID: 5044
			internal const int cmdidShellNavigate3 = 813;

			// Token: 0x040013B5 RID: 5045
			internal const int cmdidShellNavigate4 = 814;

			// Token: 0x040013B6 RID: 5046
			internal const int cmdidShellNavigate5 = 815;

			// Token: 0x040013B7 RID: 5047
			internal const int cmdidShellNavigate6 = 816;

			// Token: 0x040013B8 RID: 5048
			internal const int cmdidShellNavigate7 = 817;

			// Token: 0x040013B9 RID: 5049
			internal const int cmdidShellNavigate8 = 818;

			// Token: 0x040013BA RID: 5050
			internal const int cmdidShellNavigate9 = 819;

			// Token: 0x040013BB RID: 5051
			internal const int cmdidShellNavigate10 = 820;

			// Token: 0x040013BC RID: 5052
			internal const int cmdidShellNavigate11 = 821;

			// Token: 0x040013BD RID: 5053
			internal const int cmdidShellNavigate12 = 822;

			// Token: 0x040013BE RID: 5054
			internal const int cmdidShellNavigate13 = 823;

			// Token: 0x040013BF RID: 5055
			internal const int cmdidShellNavigate14 = 824;

			// Token: 0x040013C0 RID: 5056
			internal const int cmdidShellNavigate15 = 825;

			// Token: 0x040013C1 RID: 5057
			internal const int cmdidShellNavigate16 = 826;

			// Token: 0x040013C2 RID: 5058
			internal const int cmdidShellNavigate17 = 827;

			// Token: 0x040013C3 RID: 5059
			internal const int cmdidShellNavigate18 = 828;

			// Token: 0x040013C4 RID: 5060
			internal const int cmdidShellNavigate19 = 829;

			// Token: 0x040013C5 RID: 5061
			internal const int cmdidShellNavigate20 = 830;

			// Token: 0x040013C6 RID: 5062
			internal const int cmdidShellNavigate21 = 831;

			// Token: 0x040013C7 RID: 5063
			internal const int cmdidShellNavigate22 = 832;

			// Token: 0x040013C8 RID: 5064
			internal const int cmdidShellNavigate23 = 833;

			// Token: 0x040013C9 RID: 5065
			internal const int cmdidShellNavigate24 = 834;

			// Token: 0x040013CA RID: 5066
			internal const int cmdidShellNavigate25 = 835;

			// Token: 0x040013CB RID: 5067
			internal const int cmdidShellNavigate26 = 836;

			// Token: 0x040013CC RID: 5068
			internal const int cmdidShellNavigate27 = 837;

			// Token: 0x040013CD RID: 5069
			internal const int cmdidShellNavigate28 = 838;

			// Token: 0x040013CE RID: 5070
			internal const int cmdidShellNavigate29 = 839;

			// Token: 0x040013CF RID: 5071
			internal const int cmdidShellNavigate30 = 840;

			// Token: 0x040013D0 RID: 5072
			internal const int cmdidShellNavigate31 = 841;

			// Token: 0x040013D1 RID: 5073
			internal const int cmdidShellNavigate32 = 842;

			// Token: 0x040013D2 RID: 5074
			internal const int cmdidShellNavigate33 = 843;

			// Token: 0x040013D3 RID: 5075
			internal const int cmdidShellWindowNavigate1 = 844;

			// Token: 0x040013D4 RID: 5076
			internal const int cmdidShellWindowNavigate2 = 845;

			// Token: 0x040013D5 RID: 5077
			internal const int cmdidShellWindowNavigate3 = 846;

			// Token: 0x040013D6 RID: 5078
			internal const int cmdidShellWindowNavigate4 = 847;

			// Token: 0x040013D7 RID: 5079
			internal const int cmdidShellWindowNavigate5 = 848;

			// Token: 0x040013D8 RID: 5080
			internal const int cmdidShellWindowNavigate6 = 849;

			// Token: 0x040013D9 RID: 5081
			internal const int cmdidShellWindowNavigate7 = 850;

			// Token: 0x040013DA RID: 5082
			internal const int cmdidShellWindowNavigate8 = 851;

			// Token: 0x040013DB RID: 5083
			internal const int cmdidShellWindowNavigate9 = 852;

			// Token: 0x040013DC RID: 5084
			internal const int cmdidShellWindowNavigate10 = 853;

			// Token: 0x040013DD RID: 5085
			internal const int cmdidShellWindowNavigate11 = 854;

			// Token: 0x040013DE RID: 5086
			internal const int cmdidShellWindowNavigate12 = 855;

			// Token: 0x040013DF RID: 5087
			internal const int cmdidShellWindowNavigate13 = 856;

			// Token: 0x040013E0 RID: 5088
			internal const int cmdidShellWindowNavigate14 = 857;

			// Token: 0x040013E1 RID: 5089
			internal const int cmdidShellWindowNavigate15 = 858;

			// Token: 0x040013E2 RID: 5090
			internal const int cmdidShellWindowNavigate16 = 859;

			// Token: 0x040013E3 RID: 5091
			internal const int cmdidShellWindowNavigate17 = 860;

			// Token: 0x040013E4 RID: 5092
			internal const int cmdidShellWindowNavigate18 = 861;

			// Token: 0x040013E5 RID: 5093
			internal const int cmdidShellWindowNavigate19 = 862;

			// Token: 0x040013E6 RID: 5094
			internal const int cmdidShellWindowNavigate20 = 863;

			// Token: 0x040013E7 RID: 5095
			internal const int cmdidShellWindowNavigate21 = 864;

			// Token: 0x040013E8 RID: 5096
			internal const int cmdidShellWindowNavigate22 = 865;

			// Token: 0x040013E9 RID: 5097
			internal const int cmdidShellWindowNavigate23 = 866;

			// Token: 0x040013EA RID: 5098
			internal const int cmdidShellWindowNavigate24 = 867;

			// Token: 0x040013EB RID: 5099
			internal const int cmdidShellWindowNavigate25 = 868;

			// Token: 0x040013EC RID: 5100
			internal const int cmdidShellWindowNavigate26 = 869;

			// Token: 0x040013ED RID: 5101
			internal const int cmdidShellWindowNavigate27 = 870;

			// Token: 0x040013EE RID: 5102
			internal const int cmdidShellWindowNavigate28 = 871;

			// Token: 0x040013EF RID: 5103
			internal const int cmdidShellWindowNavigate29 = 872;

			// Token: 0x040013F0 RID: 5104
			internal const int cmdidShellWindowNavigate30 = 873;

			// Token: 0x040013F1 RID: 5105
			internal const int cmdidShellWindowNavigate31 = 874;

			// Token: 0x040013F2 RID: 5106
			internal const int cmdidShellWindowNavigate32 = 875;

			// Token: 0x040013F3 RID: 5107
			internal const int cmdidShellWindowNavigate33 = 876;

			// Token: 0x040013F4 RID: 5108
			internal const int cmdidOBSDoFind = 877;

			// Token: 0x040013F5 RID: 5109
			internal const int cmdidOBSMatchCase = 878;

			// Token: 0x040013F6 RID: 5110
			internal const int cmdidOBSMatchSubString = 879;

			// Token: 0x040013F7 RID: 5111
			internal const int cmdidOBSMatchWholeWord = 880;

			// Token: 0x040013F8 RID: 5112
			internal const int cmdidOBSMatchPrefix = 881;

			// Token: 0x040013F9 RID: 5113
			internal const int cmdidBuildSln = 882;

			// Token: 0x040013FA RID: 5114
			internal const int cmdidRebuildSln = 883;

			// Token: 0x040013FB RID: 5115
			internal const int cmdidDeploySln = 884;

			// Token: 0x040013FC RID: 5116
			internal const int cmdidCleanSln = 885;

			// Token: 0x040013FD RID: 5117
			internal const int cmdidBuildSel = 886;

			// Token: 0x040013FE RID: 5118
			internal const int cmdidRebuildSel = 887;

			// Token: 0x040013FF RID: 5119
			internal const int cmdidDeploySel = 888;

			// Token: 0x04001400 RID: 5120
			internal const int cmdidCleanSel = 889;

			// Token: 0x04001401 RID: 5121
			internal const int cmdidCancelBuild = 890;

			// Token: 0x04001402 RID: 5122
			internal const int cmdidBatchBuildDlg = 891;

			// Token: 0x04001403 RID: 5123
			internal const int cmdidBuildCtx = 892;

			// Token: 0x04001404 RID: 5124
			internal const int cmdidRebuildCtx = 893;

			// Token: 0x04001405 RID: 5125
			internal const int cmdidDeployCtx = 894;

			// Token: 0x04001406 RID: 5126
			internal const int cmdidCleanCtx = 895;

			// Token: 0x04001407 RID: 5127
			internal const int cmdidMRUFile1 = 900;

			// Token: 0x04001408 RID: 5128
			internal const int cmdidMRUFile2 = 901;

			// Token: 0x04001409 RID: 5129
			internal const int cmdidMRUFile3 = 902;

			// Token: 0x0400140A RID: 5130
			internal const int cmdidMRUFile4 = 903;

			// Token: 0x0400140B RID: 5131
			internal const int cmdidMRUFile5 = 904;

			// Token: 0x0400140C RID: 5132
			internal const int cmdidMRUFile6 = 905;

			// Token: 0x0400140D RID: 5133
			internal const int cmdidMRUFile7 = 906;

			// Token: 0x0400140E RID: 5134
			internal const int cmdidMRUFile8 = 907;

			// Token: 0x0400140F RID: 5135
			internal const int cmdidMRUFile9 = 908;

			// Token: 0x04001410 RID: 5136
			internal const int cmdidMRUFile10 = 909;

			// Token: 0x04001411 RID: 5137
			internal const int cmdidMRUFile11 = 910;

			// Token: 0x04001412 RID: 5138
			internal const int cmdidMRUFile12 = 911;

			// Token: 0x04001413 RID: 5139
			internal const int cmdidMRUFile13 = 912;

			// Token: 0x04001414 RID: 5140
			internal const int cmdidMRUFile14 = 913;

			// Token: 0x04001415 RID: 5141
			internal const int cmdidMRUFile15 = 914;

			// Token: 0x04001416 RID: 5142
			internal const int cmdidMRUFile16 = 915;

			// Token: 0x04001417 RID: 5143
			internal const int cmdidMRUFile17 = 916;

			// Token: 0x04001418 RID: 5144
			internal const int cmdidMRUFile18 = 917;

			// Token: 0x04001419 RID: 5145
			internal const int cmdidMRUFile19 = 918;

			// Token: 0x0400141A RID: 5146
			internal const int cmdidMRUFile20 = 919;

			// Token: 0x0400141B RID: 5147
			internal const int cmdidMRUFile21 = 920;

			// Token: 0x0400141C RID: 5148
			internal const int cmdidMRUFile22 = 921;

			// Token: 0x0400141D RID: 5149
			internal const int cmdidMRUFile23 = 922;

			// Token: 0x0400141E RID: 5150
			internal const int cmdidMRUFile24 = 923;

			// Token: 0x0400141F RID: 5151
			internal const int cmdidMRUFile25 = 924;

			// Token: 0x04001420 RID: 5152
			internal const int cmdidGotoDefn = 925;

			// Token: 0x04001421 RID: 5153
			internal const int cmdidGotoDecl = 926;

			// Token: 0x04001422 RID: 5154
			internal const int cmdidBrowseDefn = 927;

			// Token: 0x04001423 RID: 5155
			internal const int cmdidShowMembers = 928;

			// Token: 0x04001424 RID: 5156
			internal const int cmdidShowBases = 929;

			// Token: 0x04001425 RID: 5157
			internal const int cmdidShowDerived = 930;

			// Token: 0x04001426 RID: 5158
			internal const int cmdidShowDefns = 931;

			// Token: 0x04001427 RID: 5159
			internal const int cmdidShowRefs = 932;

			// Token: 0x04001428 RID: 5160
			internal const int cmdidShowCallers = 933;

			// Token: 0x04001429 RID: 5161
			internal const int cmdidShowCallees = 934;

			// Token: 0x0400142A RID: 5162
			internal const int cmdidDefineSubset = 935;

			// Token: 0x0400142B RID: 5163
			internal const int cmdidSetSubset = 936;

			// Token: 0x0400142C RID: 5164
			internal const int cmdidCVGroupingNone = 950;

			// Token: 0x0400142D RID: 5165
			internal const int cmdidCVGroupingSortOnly = 951;

			// Token: 0x0400142E RID: 5166
			internal const int cmdidCVGroupingGrouped = 952;

			// Token: 0x0400142F RID: 5167
			internal const int cmdidCVShowPackages = 953;

			// Token: 0x04001430 RID: 5168
			internal const int cmdidQryManageIndexes = 954;

			// Token: 0x04001431 RID: 5169
			internal const int cmdidBrowseComponent = 955;

			// Token: 0x04001432 RID: 5170
			internal const int cmdidPrintDefault = 956;

			// Token: 0x04001433 RID: 5171
			internal const int cmdidBrowseDoc = 957;

			// Token: 0x04001434 RID: 5172
			internal const int cmdidStandardMax = 1000;

			// Token: 0x04001435 RID: 5173
			internal const int cmdidFormsFirst = 24576;

			// Token: 0x04001436 RID: 5174
			internal const int cmdidFormsLast = 28671;

			// Token: 0x04001437 RID: 5175
			internal const int cmdidVBEFirst = 32768;

			// Token: 0x04001438 RID: 5176
			internal const int msotcidBookmarkWellMenu = 32769;

			// Token: 0x04001439 RID: 5177
			internal const int cmdidZoom200 = 32770;

			// Token: 0x0400143A RID: 5178
			internal const int cmdidZoom150 = 32771;

			// Token: 0x0400143B RID: 5179
			internal const int cmdidZoom100 = 32772;

			// Token: 0x0400143C RID: 5180
			internal const int cmdidZoom75 = 32773;

			// Token: 0x0400143D RID: 5181
			internal const int cmdidZoom50 = 32774;

			// Token: 0x0400143E RID: 5182
			internal const int cmdidZoom25 = 32775;

			// Token: 0x0400143F RID: 5183
			internal const int cmdidZoom10 = 32784;

			// Token: 0x04001440 RID: 5184
			internal const int msotcidZoomWellMenu = 32785;

			// Token: 0x04001441 RID: 5185
			internal const int msotcidDebugPopWellMenu = 32786;

			// Token: 0x04001442 RID: 5186
			internal const int msotcidAlignWellMenu = 32787;

			// Token: 0x04001443 RID: 5187
			internal const int msotcidArrangeWellMenu = 32788;

			// Token: 0x04001444 RID: 5188
			internal const int msotcidCenterWellMenu = 32789;

			// Token: 0x04001445 RID: 5189
			internal const int msotcidSizeWellMenu = 32790;

			// Token: 0x04001446 RID: 5190
			internal const int msotcidHorizontalSpaceWellMenu = 32791;

			// Token: 0x04001447 RID: 5191
			internal const int msotcidVerticalSpaceWellMenu = 32800;

			// Token: 0x04001448 RID: 5192
			internal const int msotcidDebugWellMenu = 32801;

			// Token: 0x04001449 RID: 5193
			internal const int msotcidDebugMenuVB = 32802;

			// Token: 0x0400144A RID: 5194
			internal const int msotcidStatementBuilderWellMenu = 32803;

			// Token: 0x0400144B RID: 5195
			internal const int msotcidProjWinInsertMenu = 32804;

			// Token: 0x0400144C RID: 5196
			internal const int msotcidToggleMenu = 32805;

			// Token: 0x0400144D RID: 5197
			internal const int msotcidNewObjInsertWellMenu = 32806;

			// Token: 0x0400144E RID: 5198
			internal const int msotcidSizeToWellMenu = 32807;

			// Token: 0x0400144F RID: 5199
			internal const int msotcidCommandBars = 32808;

			// Token: 0x04001450 RID: 5200
			internal const int msotcidVBOrderMenu = 32809;

			// Token: 0x04001451 RID: 5201
			internal const int msotcidMSOnTheWeb = 32810;

			// Token: 0x04001452 RID: 5202
			internal const int msotcidVBDesignerMenu = 32816;

			// Token: 0x04001453 RID: 5203
			internal const int msotcidNewProjectWellMenu = 32817;

			// Token: 0x04001454 RID: 5204
			internal const int msotcidProjectWellMenu = 32818;

			// Token: 0x04001455 RID: 5205
			internal const int msotcidVBCode1ContextMenu = 32819;

			// Token: 0x04001456 RID: 5206
			internal const int msotcidVBCode2ContextMenu = 32820;

			// Token: 0x04001457 RID: 5207
			internal const int msotcidVBWatchContextMenu = 32821;

			// Token: 0x04001458 RID: 5208
			internal const int msotcidVBImmediateContextMenu = 32822;

			// Token: 0x04001459 RID: 5209
			internal const int msotcidVBLocalsContextMenu = 32823;

			// Token: 0x0400145A RID: 5210
			internal const int msotcidVBFormContextMenu = 32824;

			// Token: 0x0400145B RID: 5211
			internal const int msotcidVBControlContextMenu = 32825;

			// Token: 0x0400145C RID: 5212
			internal const int msotcidVBProjWinContextMenu = 32826;

			// Token: 0x0400145D RID: 5213
			internal const int msotcidVBProjWinContextBreakMenu = 32827;

			// Token: 0x0400145E RID: 5214
			internal const int msotcidVBPreviewWinContextMenu = 32828;

			// Token: 0x0400145F RID: 5215
			internal const int msotcidVBOBContextMenu = 32829;

			// Token: 0x04001460 RID: 5216
			internal const int msotcidVBForms3ContextMenu = 32830;

			// Token: 0x04001461 RID: 5217
			internal const int msotcidVBForms3ControlCMenu = 32831;

			// Token: 0x04001462 RID: 5218
			internal const int msotcidVBForms3ControlCMenuGroup = 32832;

			// Token: 0x04001463 RID: 5219
			internal const int msotcidVBForms3ControlPalette = 32833;

			// Token: 0x04001464 RID: 5220
			internal const int msotcidVBForms3ToolboxCMenu = 32834;

			// Token: 0x04001465 RID: 5221
			internal const int msotcidVBForms3MPCCMenu = 32835;

			// Token: 0x04001466 RID: 5222
			internal const int msotcidVBForms3DragDropCMenu = 32836;

			// Token: 0x04001467 RID: 5223
			internal const int msotcidVBToolBoxContextMenu = 32837;

			// Token: 0x04001468 RID: 5224
			internal const int msotcidVBToolBoxGroupContextMenu = 32838;

			// Token: 0x04001469 RID: 5225
			internal const int msotcidVBPropBrsHostContextMenu = 32839;

			// Token: 0x0400146A RID: 5226
			internal const int msotcidVBPropBrsContextMenu = 32840;

			// Token: 0x0400146B RID: 5227
			internal const int msotcidVBPalContextMenu = 32841;

			// Token: 0x0400146C RID: 5228
			internal const int msotcidVBProjWinProjectContextMenu = 32842;

			// Token: 0x0400146D RID: 5229
			internal const int msotcidVBProjWinFormContextMenu = 32843;

			// Token: 0x0400146E RID: 5230
			internal const int msotcidVBProjWinModClassContextMenu = 32844;

			// Token: 0x0400146F RID: 5231
			internal const int msotcidVBProjWinRelDocContextMenu = 32845;

			// Token: 0x04001470 RID: 5232
			internal const int msotcidVBDockedWindowContextMenu = 32846;

			// Token: 0x04001471 RID: 5233
			internal const int msotcidVBShortCutForms = 32847;

			// Token: 0x04001472 RID: 5234
			internal const int msotcidVBShortCutCodeWindows = 32848;

			// Token: 0x04001473 RID: 5235
			internal const int msotcidVBShortCutMisc = 32849;

			// Token: 0x04001474 RID: 5236
			internal const int msotcidVBBuiltInMenus = 32850;

			// Token: 0x04001475 RID: 5237
			internal const int msotcidPreviewWinFormPos = 32851;

			// Token: 0x04001476 RID: 5238
			internal const int msotcidVBAddinFirst = 33280;
		}

		// Token: 0x02000477 RID: 1143
		private static class ShellGuids
		{
			// Token: 0x060024B4 RID: 9396 RVA: 0x0008226C File Offset: 0x0008046C
			// Note: this type is marked as 'beforefieldinit'.
			static ShellGuids()
			{
			}

			// Token: 0x04001477 RID: 5239
			internal static readonly Guid VSStandardCommandSet97 = new Guid("{5efc7975-14bc-11cf-9b2b-00aa00573819}");

			// Token: 0x04001478 RID: 5240
			internal static readonly Guid guidDsdCmdId = new Guid("{1F0FD094-8e53-11d2-8f9c-0060089fc486}");

			// Token: 0x04001479 RID: 5241
			internal static readonly Guid SID_SOleComponentUIManager = new Guid("{5efc7974-14bc-11cf-9b2b-00aa00573819}");

			// Token: 0x0400147A RID: 5242
			internal static readonly Guid GUID_VSTASKCATEGORY_DATADESIGNER = new Guid("{6B32EAED-13BB-11d3-A64F-00C04F683820}");

			// Token: 0x0400147B RID: 5243
			internal static readonly Guid GUID_PropertyBrowserToolWindow = new Guid(-285584864, -7528, 4560, new byte[]
			{
				143,
				120,
				0,
				160,
				201,
				17,
				0,
				87
			});
		}
	}
}

using System;

namespace Steamworks
{
	// Token: 0x02000076 RID: 118
	internal enum ControllerActionOrigin
	{
		// Token: 0x040004DF RID: 1247
		None,
		// Token: 0x040004E0 RID: 1248
		A,
		// Token: 0x040004E1 RID: 1249
		B,
		// Token: 0x040004E2 RID: 1250
		X,
		// Token: 0x040004E3 RID: 1251
		Y,
		// Token: 0x040004E4 RID: 1252
		LeftBumper,
		// Token: 0x040004E5 RID: 1253
		RightBumper,
		// Token: 0x040004E6 RID: 1254
		LeftGrip,
		// Token: 0x040004E7 RID: 1255
		RightGrip,
		// Token: 0x040004E8 RID: 1256
		Start,
		// Token: 0x040004E9 RID: 1257
		Back,
		// Token: 0x040004EA RID: 1258
		LeftPad_Touch,
		// Token: 0x040004EB RID: 1259
		LeftPad_Swipe,
		// Token: 0x040004EC RID: 1260
		LeftPad_Click,
		// Token: 0x040004ED RID: 1261
		LeftPad_DPadNorth,
		// Token: 0x040004EE RID: 1262
		LeftPad_DPadSouth,
		// Token: 0x040004EF RID: 1263
		LeftPad_DPadWest,
		// Token: 0x040004F0 RID: 1264
		LeftPad_DPadEast,
		// Token: 0x040004F1 RID: 1265
		RightPad_Touch,
		// Token: 0x040004F2 RID: 1266
		RightPad_Swipe,
		// Token: 0x040004F3 RID: 1267
		RightPad_Click,
		// Token: 0x040004F4 RID: 1268
		RightPad_DPadNorth,
		// Token: 0x040004F5 RID: 1269
		RightPad_DPadSouth,
		// Token: 0x040004F6 RID: 1270
		RightPad_DPadWest,
		// Token: 0x040004F7 RID: 1271
		RightPad_DPadEast,
		// Token: 0x040004F8 RID: 1272
		LeftTrigger_Pull,
		// Token: 0x040004F9 RID: 1273
		LeftTrigger_Click,
		// Token: 0x040004FA RID: 1274
		RightTrigger_Pull,
		// Token: 0x040004FB RID: 1275
		RightTrigger_Click,
		// Token: 0x040004FC RID: 1276
		LeftStick_Move,
		// Token: 0x040004FD RID: 1277
		LeftStick_Click,
		// Token: 0x040004FE RID: 1278
		LeftStick_DPadNorth,
		// Token: 0x040004FF RID: 1279
		LeftStick_DPadSouth,
		// Token: 0x04000500 RID: 1280
		LeftStick_DPadWest,
		// Token: 0x04000501 RID: 1281
		LeftStick_DPadEast,
		// Token: 0x04000502 RID: 1282
		Gyro_Move,
		// Token: 0x04000503 RID: 1283
		Gyro_Pitch,
		// Token: 0x04000504 RID: 1284
		Gyro_Yaw,
		// Token: 0x04000505 RID: 1285
		Gyro_Roll,
		// Token: 0x04000506 RID: 1286
		PS4_X,
		// Token: 0x04000507 RID: 1287
		PS4_Circle,
		// Token: 0x04000508 RID: 1288
		PS4_Triangle,
		// Token: 0x04000509 RID: 1289
		PS4_Square,
		// Token: 0x0400050A RID: 1290
		PS4_LeftBumper,
		// Token: 0x0400050B RID: 1291
		PS4_RightBumper,
		// Token: 0x0400050C RID: 1292
		PS4_Options,
		// Token: 0x0400050D RID: 1293
		PS4_Share,
		// Token: 0x0400050E RID: 1294
		PS4_LeftPad_Touch,
		// Token: 0x0400050F RID: 1295
		PS4_LeftPad_Swipe,
		// Token: 0x04000510 RID: 1296
		PS4_LeftPad_Click,
		// Token: 0x04000511 RID: 1297
		PS4_LeftPad_DPadNorth,
		// Token: 0x04000512 RID: 1298
		PS4_LeftPad_DPadSouth,
		// Token: 0x04000513 RID: 1299
		PS4_LeftPad_DPadWest,
		// Token: 0x04000514 RID: 1300
		PS4_LeftPad_DPadEast,
		// Token: 0x04000515 RID: 1301
		PS4_RightPad_Touch,
		// Token: 0x04000516 RID: 1302
		PS4_RightPad_Swipe,
		// Token: 0x04000517 RID: 1303
		PS4_RightPad_Click,
		// Token: 0x04000518 RID: 1304
		PS4_RightPad_DPadNorth,
		// Token: 0x04000519 RID: 1305
		PS4_RightPad_DPadSouth,
		// Token: 0x0400051A RID: 1306
		PS4_RightPad_DPadWest,
		// Token: 0x0400051B RID: 1307
		PS4_RightPad_DPadEast,
		// Token: 0x0400051C RID: 1308
		PS4_CenterPad_Touch,
		// Token: 0x0400051D RID: 1309
		PS4_CenterPad_Swipe,
		// Token: 0x0400051E RID: 1310
		PS4_CenterPad_Click,
		// Token: 0x0400051F RID: 1311
		PS4_CenterPad_DPadNorth,
		// Token: 0x04000520 RID: 1312
		PS4_CenterPad_DPadSouth,
		// Token: 0x04000521 RID: 1313
		PS4_CenterPad_DPadWest,
		// Token: 0x04000522 RID: 1314
		PS4_CenterPad_DPadEast,
		// Token: 0x04000523 RID: 1315
		PS4_LeftTrigger_Pull,
		// Token: 0x04000524 RID: 1316
		PS4_LeftTrigger_Click,
		// Token: 0x04000525 RID: 1317
		PS4_RightTrigger_Pull,
		// Token: 0x04000526 RID: 1318
		PS4_RightTrigger_Click,
		// Token: 0x04000527 RID: 1319
		PS4_LeftStick_Move,
		// Token: 0x04000528 RID: 1320
		PS4_LeftStick_Click,
		// Token: 0x04000529 RID: 1321
		PS4_LeftStick_DPadNorth,
		// Token: 0x0400052A RID: 1322
		PS4_LeftStick_DPadSouth,
		// Token: 0x0400052B RID: 1323
		PS4_LeftStick_DPadWest,
		// Token: 0x0400052C RID: 1324
		PS4_LeftStick_DPadEast,
		// Token: 0x0400052D RID: 1325
		PS4_RightStick_Move,
		// Token: 0x0400052E RID: 1326
		PS4_RightStick_Click,
		// Token: 0x0400052F RID: 1327
		PS4_RightStick_DPadNorth,
		// Token: 0x04000530 RID: 1328
		PS4_RightStick_DPadSouth,
		// Token: 0x04000531 RID: 1329
		PS4_RightStick_DPadWest,
		// Token: 0x04000532 RID: 1330
		PS4_RightStick_DPadEast,
		// Token: 0x04000533 RID: 1331
		PS4_DPad_North,
		// Token: 0x04000534 RID: 1332
		PS4_DPad_South,
		// Token: 0x04000535 RID: 1333
		PS4_DPad_West,
		// Token: 0x04000536 RID: 1334
		PS4_DPad_East,
		// Token: 0x04000537 RID: 1335
		PS4_Gyro_Move,
		// Token: 0x04000538 RID: 1336
		PS4_Gyro_Pitch,
		// Token: 0x04000539 RID: 1337
		PS4_Gyro_Yaw,
		// Token: 0x0400053A RID: 1338
		PS4_Gyro_Roll,
		// Token: 0x0400053B RID: 1339
		XBoxOne_A,
		// Token: 0x0400053C RID: 1340
		XBoxOne_B,
		// Token: 0x0400053D RID: 1341
		XBoxOne_X,
		// Token: 0x0400053E RID: 1342
		XBoxOne_Y,
		// Token: 0x0400053F RID: 1343
		XBoxOne_LeftBumper,
		// Token: 0x04000540 RID: 1344
		XBoxOne_RightBumper,
		// Token: 0x04000541 RID: 1345
		XBoxOne_Menu,
		// Token: 0x04000542 RID: 1346
		XBoxOne_View,
		// Token: 0x04000543 RID: 1347
		XBoxOne_LeftTrigger_Pull,
		// Token: 0x04000544 RID: 1348
		XBoxOne_LeftTrigger_Click,
		// Token: 0x04000545 RID: 1349
		XBoxOne_RightTrigger_Pull,
		// Token: 0x04000546 RID: 1350
		XBoxOne_RightTrigger_Click,
		// Token: 0x04000547 RID: 1351
		XBoxOne_LeftStick_Move,
		// Token: 0x04000548 RID: 1352
		XBoxOne_LeftStick_Click,
		// Token: 0x04000549 RID: 1353
		XBoxOne_LeftStick_DPadNorth,
		// Token: 0x0400054A RID: 1354
		XBoxOne_LeftStick_DPadSouth,
		// Token: 0x0400054B RID: 1355
		XBoxOne_LeftStick_DPadWest,
		// Token: 0x0400054C RID: 1356
		XBoxOne_LeftStick_DPadEast,
		// Token: 0x0400054D RID: 1357
		XBoxOne_RightStick_Move,
		// Token: 0x0400054E RID: 1358
		XBoxOne_RightStick_Click,
		// Token: 0x0400054F RID: 1359
		XBoxOne_RightStick_DPadNorth,
		// Token: 0x04000550 RID: 1360
		XBoxOne_RightStick_DPadSouth,
		// Token: 0x04000551 RID: 1361
		XBoxOne_RightStick_DPadWest,
		// Token: 0x04000552 RID: 1362
		XBoxOne_RightStick_DPadEast,
		// Token: 0x04000553 RID: 1363
		XBoxOne_DPad_North,
		// Token: 0x04000554 RID: 1364
		XBoxOne_DPad_South,
		// Token: 0x04000555 RID: 1365
		XBoxOne_DPad_West,
		// Token: 0x04000556 RID: 1366
		XBoxOne_DPad_East,
		// Token: 0x04000557 RID: 1367
		XBox360_A,
		// Token: 0x04000558 RID: 1368
		XBox360_B,
		// Token: 0x04000559 RID: 1369
		XBox360_X,
		// Token: 0x0400055A RID: 1370
		XBox360_Y,
		// Token: 0x0400055B RID: 1371
		XBox360_LeftBumper,
		// Token: 0x0400055C RID: 1372
		XBox360_RightBumper,
		// Token: 0x0400055D RID: 1373
		XBox360_Start,
		// Token: 0x0400055E RID: 1374
		XBox360_Back,
		// Token: 0x0400055F RID: 1375
		XBox360_LeftTrigger_Pull,
		// Token: 0x04000560 RID: 1376
		XBox360_LeftTrigger_Click,
		// Token: 0x04000561 RID: 1377
		XBox360_RightTrigger_Pull,
		// Token: 0x04000562 RID: 1378
		XBox360_RightTrigger_Click,
		// Token: 0x04000563 RID: 1379
		XBox360_LeftStick_Move,
		// Token: 0x04000564 RID: 1380
		XBox360_LeftStick_Click,
		// Token: 0x04000565 RID: 1381
		XBox360_LeftStick_DPadNorth,
		// Token: 0x04000566 RID: 1382
		XBox360_LeftStick_DPadSouth,
		// Token: 0x04000567 RID: 1383
		XBox360_LeftStick_DPadWest,
		// Token: 0x04000568 RID: 1384
		XBox360_LeftStick_DPadEast,
		// Token: 0x04000569 RID: 1385
		XBox360_RightStick_Move,
		// Token: 0x0400056A RID: 1386
		XBox360_RightStick_Click,
		// Token: 0x0400056B RID: 1387
		XBox360_RightStick_DPadNorth,
		// Token: 0x0400056C RID: 1388
		XBox360_RightStick_DPadSouth,
		// Token: 0x0400056D RID: 1389
		XBox360_RightStick_DPadWest,
		// Token: 0x0400056E RID: 1390
		XBox360_RightStick_DPadEast,
		// Token: 0x0400056F RID: 1391
		XBox360_DPad_North,
		// Token: 0x04000570 RID: 1392
		XBox360_DPad_South,
		// Token: 0x04000571 RID: 1393
		XBox360_DPad_West,
		// Token: 0x04000572 RID: 1394
		XBox360_DPad_East,
		// Token: 0x04000573 RID: 1395
		SteamV2_A,
		// Token: 0x04000574 RID: 1396
		SteamV2_B,
		// Token: 0x04000575 RID: 1397
		SteamV2_X,
		// Token: 0x04000576 RID: 1398
		SteamV2_Y,
		// Token: 0x04000577 RID: 1399
		SteamV2_LeftBumper,
		// Token: 0x04000578 RID: 1400
		SteamV2_RightBumper,
		// Token: 0x04000579 RID: 1401
		SteamV2_LeftGrip_Lower,
		// Token: 0x0400057A RID: 1402
		SteamV2_LeftGrip_Upper,
		// Token: 0x0400057B RID: 1403
		SteamV2_RightGrip_Lower,
		// Token: 0x0400057C RID: 1404
		SteamV2_RightGrip_Upper,
		// Token: 0x0400057D RID: 1405
		SteamV2_LeftBumper_Pressure,
		// Token: 0x0400057E RID: 1406
		SteamV2_RightBumper_Pressure,
		// Token: 0x0400057F RID: 1407
		SteamV2_LeftGrip_Pressure,
		// Token: 0x04000580 RID: 1408
		SteamV2_RightGrip_Pressure,
		// Token: 0x04000581 RID: 1409
		SteamV2_LeftGrip_Upper_Pressure,
		// Token: 0x04000582 RID: 1410
		SteamV2_RightGrip_Upper_Pressure,
		// Token: 0x04000583 RID: 1411
		SteamV2_Start,
		// Token: 0x04000584 RID: 1412
		SteamV2_Back,
		// Token: 0x04000585 RID: 1413
		SteamV2_LeftPad_Touch,
		// Token: 0x04000586 RID: 1414
		SteamV2_LeftPad_Swipe,
		// Token: 0x04000587 RID: 1415
		SteamV2_LeftPad_Click,
		// Token: 0x04000588 RID: 1416
		SteamV2_LeftPad_Pressure,
		// Token: 0x04000589 RID: 1417
		SteamV2_LeftPad_DPadNorth,
		// Token: 0x0400058A RID: 1418
		SteamV2_LeftPad_DPadSouth,
		// Token: 0x0400058B RID: 1419
		SteamV2_LeftPad_DPadWest,
		// Token: 0x0400058C RID: 1420
		SteamV2_LeftPad_DPadEast,
		// Token: 0x0400058D RID: 1421
		SteamV2_RightPad_Touch,
		// Token: 0x0400058E RID: 1422
		SteamV2_RightPad_Swipe,
		// Token: 0x0400058F RID: 1423
		SteamV2_RightPad_Click,
		// Token: 0x04000590 RID: 1424
		SteamV2_RightPad_Pressure,
		// Token: 0x04000591 RID: 1425
		SteamV2_RightPad_DPadNorth,
		// Token: 0x04000592 RID: 1426
		SteamV2_RightPad_DPadSouth,
		// Token: 0x04000593 RID: 1427
		SteamV2_RightPad_DPadWest,
		// Token: 0x04000594 RID: 1428
		SteamV2_RightPad_DPadEast,
		// Token: 0x04000595 RID: 1429
		SteamV2_LeftTrigger_Pull,
		// Token: 0x04000596 RID: 1430
		SteamV2_LeftTrigger_Click,
		// Token: 0x04000597 RID: 1431
		SteamV2_RightTrigger_Pull,
		// Token: 0x04000598 RID: 1432
		SteamV2_RightTrigger_Click,
		// Token: 0x04000599 RID: 1433
		SteamV2_LeftStick_Move,
		// Token: 0x0400059A RID: 1434
		SteamV2_LeftStick_Click,
		// Token: 0x0400059B RID: 1435
		SteamV2_LeftStick_DPadNorth,
		// Token: 0x0400059C RID: 1436
		SteamV2_LeftStick_DPadSouth,
		// Token: 0x0400059D RID: 1437
		SteamV2_LeftStick_DPadWest,
		// Token: 0x0400059E RID: 1438
		SteamV2_LeftStick_DPadEast,
		// Token: 0x0400059F RID: 1439
		SteamV2_Gyro_Move,
		// Token: 0x040005A0 RID: 1440
		SteamV2_Gyro_Pitch,
		// Token: 0x040005A1 RID: 1441
		SteamV2_Gyro_Yaw,
		// Token: 0x040005A2 RID: 1442
		SteamV2_Gyro_Roll,
		// Token: 0x040005A3 RID: 1443
		Switch_A,
		// Token: 0x040005A4 RID: 1444
		Switch_B,
		// Token: 0x040005A5 RID: 1445
		Switch_X,
		// Token: 0x040005A6 RID: 1446
		Switch_Y,
		// Token: 0x040005A7 RID: 1447
		Switch_LeftBumper,
		// Token: 0x040005A8 RID: 1448
		Switch_RightBumper,
		// Token: 0x040005A9 RID: 1449
		Switch_Plus,
		// Token: 0x040005AA RID: 1450
		Switch_Minus,
		// Token: 0x040005AB RID: 1451
		Switch_Capture,
		// Token: 0x040005AC RID: 1452
		Switch_LeftTrigger_Pull,
		// Token: 0x040005AD RID: 1453
		Switch_LeftTrigger_Click,
		// Token: 0x040005AE RID: 1454
		Switch_RightTrigger_Pull,
		// Token: 0x040005AF RID: 1455
		Switch_RightTrigger_Click,
		// Token: 0x040005B0 RID: 1456
		Switch_LeftStick_Move,
		// Token: 0x040005B1 RID: 1457
		Switch_LeftStick_Click,
		// Token: 0x040005B2 RID: 1458
		Switch_LeftStick_DPadNorth,
		// Token: 0x040005B3 RID: 1459
		Switch_LeftStick_DPadSouth,
		// Token: 0x040005B4 RID: 1460
		Switch_LeftStick_DPadWest,
		// Token: 0x040005B5 RID: 1461
		Switch_LeftStick_DPadEast,
		// Token: 0x040005B6 RID: 1462
		Switch_RightStick_Move,
		// Token: 0x040005B7 RID: 1463
		Switch_RightStick_Click,
		// Token: 0x040005B8 RID: 1464
		Switch_RightStick_DPadNorth,
		// Token: 0x040005B9 RID: 1465
		Switch_RightStick_DPadSouth,
		// Token: 0x040005BA RID: 1466
		Switch_RightStick_DPadWest,
		// Token: 0x040005BB RID: 1467
		Switch_RightStick_DPadEast,
		// Token: 0x040005BC RID: 1468
		Switch_DPad_North,
		// Token: 0x040005BD RID: 1469
		Switch_DPad_South,
		// Token: 0x040005BE RID: 1470
		Switch_DPad_West,
		// Token: 0x040005BF RID: 1471
		Switch_DPad_East,
		// Token: 0x040005C0 RID: 1472
		Switch_ProGyro_Move,
		// Token: 0x040005C1 RID: 1473
		Switch_ProGyro_Pitch,
		// Token: 0x040005C2 RID: 1474
		Switch_ProGyro_Yaw,
		// Token: 0x040005C3 RID: 1475
		Switch_ProGyro_Roll,
		// Token: 0x040005C4 RID: 1476
		Switch_RightGyro_Move,
		// Token: 0x040005C5 RID: 1477
		Switch_RightGyro_Pitch,
		// Token: 0x040005C6 RID: 1478
		Switch_RightGyro_Yaw,
		// Token: 0x040005C7 RID: 1479
		Switch_RightGyro_Roll,
		// Token: 0x040005C8 RID: 1480
		Switch_LeftGyro_Move,
		// Token: 0x040005C9 RID: 1481
		Switch_LeftGyro_Pitch,
		// Token: 0x040005CA RID: 1482
		Switch_LeftGyro_Yaw,
		// Token: 0x040005CB RID: 1483
		Switch_LeftGyro_Roll,
		// Token: 0x040005CC RID: 1484
		Switch_LeftGrip_Lower,
		// Token: 0x040005CD RID: 1485
		Switch_LeftGrip_Upper,
		// Token: 0x040005CE RID: 1486
		Switch_RightGrip_Lower,
		// Token: 0x040005CF RID: 1487
		Switch_RightGrip_Upper,
		// Token: 0x040005D0 RID: 1488
		PS4_DPad_Move,
		// Token: 0x040005D1 RID: 1489
		XBoxOne_DPad_Move,
		// Token: 0x040005D2 RID: 1490
		XBox360_DPad_Move,
		// Token: 0x040005D3 RID: 1491
		Switch_DPad_Move,
		// Token: 0x040005D4 RID: 1492
		Count,
		// Token: 0x040005D5 RID: 1493
		MaximumPossibleValue = 32767
	}
}

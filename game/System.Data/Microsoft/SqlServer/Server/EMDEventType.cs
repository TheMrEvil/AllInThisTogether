using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000060 RID: 96
	internal enum EMDEventType
	{
		// Token: 0x0400056A RID: 1386
		x_eet_Invalid,
		// Token: 0x0400056B RID: 1387
		x_eet_Insert,
		// Token: 0x0400056C RID: 1388
		x_eet_Update,
		// Token: 0x0400056D RID: 1389
		x_eet_Delete,
		// Token: 0x0400056E RID: 1390
		x_eet_Create_Table = 21,
		// Token: 0x0400056F RID: 1391
		x_eet_Alter_Table,
		// Token: 0x04000570 RID: 1392
		x_eet_Drop_Table,
		// Token: 0x04000571 RID: 1393
		x_eet_Create_Index,
		// Token: 0x04000572 RID: 1394
		x_eet_Alter_Index,
		// Token: 0x04000573 RID: 1395
		x_eet_Drop_Index,
		// Token: 0x04000574 RID: 1396
		x_eet_Create_Stats,
		// Token: 0x04000575 RID: 1397
		x_eet_Update_Stats,
		// Token: 0x04000576 RID: 1398
		x_eet_Drop_Stats,
		// Token: 0x04000577 RID: 1399
		x_eet_Create_Secexpr = 31,
		// Token: 0x04000578 RID: 1400
		x_eet_Drop_Secexpr = 33,
		// Token: 0x04000579 RID: 1401
		x_eet_Create_Synonym,
		// Token: 0x0400057A RID: 1402
		x_eet_Drop_Synonym = 36,
		// Token: 0x0400057B RID: 1403
		x_eet_Create_View = 41,
		// Token: 0x0400057C RID: 1404
		x_eet_Alter_View,
		// Token: 0x0400057D RID: 1405
		x_eet_Drop_View,
		// Token: 0x0400057E RID: 1406
		x_eet_Create_Procedure = 51,
		// Token: 0x0400057F RID: 1407
		x_eet_Alter_Procedure,
		// Token: 0x04000580 RID: 1408
		x_eet_Drop_Procedure,
		// Token: 0x04000581 RID: 1409
		x_eet_Create_Function = 61,
		// Token: 0x04000582 RID: 1410
		x_eet_Alter_Function,
		// Token: 0x04000583 RID: 1411
		x_eet_Drop_Function,
		// Token: 0x04000584 RID: 1412
		x_eet_Create_Trigger = 71,
		// Token: 0x04000585 RID: 1413
		x_eet_Alter_Trigger,
		// Token: 0x04000586 RID: 1414
		x_eet_Drop_Trigger,
		// Token: 0x04000587 RID: 1415
		x_eet_Create_Event_Notification,
		// Token: 0x04000588 RID: 1416
		x_eet_Drop_Event_Notification = 76,
		// Token: 0x04000589 RID: 1417
		x_eet_Create_Type = 91,
		// Token: 0x0400058A RID: 1418
		x_eet_Drop_Type = 93,
		// Token: 0x0400058B RID: 1419
		x_eet_Create_Assembly = 101,
		// Token: 0x0400058C RID: 1420
		x_eet_Alter_Assembly,
		// Token: 0x0400058D RID: 1421
		x_eet_Drop_Assembly,
		// Token: 0x0400058E RID: 1422
		x_eet_Create_User = 131,
		// Token: 0x0400058F RID: 1423
		x_eet_Alter_User,
		// Token: 0x04000590 RID: 1424
		x_eet_Drop_User,
		// Token: 0x04000591 RID: 1425
		x_eet_Create_Role,
		// Token: 0x04000592 RID: 1426
		x_eet_Alter_Role,
		// Token: 0x04000593 RID: 1427
		x_eet_Drop_Role,
		// Token: 0x04000594 RID: 1428
		x_eet_Create_AppRole,
		// Token: 0x04000595 RID: 1429
		x_eet_Alter_AppRole,
		// Token: 0x04000596 RID: 1430
		x_eet_Drop_AppRole,
		// Token: 0x04000597 RID: 1431
		x_eet_Create_Schema = 141,
		// Token: 0x04000598 RID: 1432
		x_eet_Alter_Schema,
		// Token: 0x04000599 RID: 1433
		x_eet_Drop_Schema,
		// Token: 0x0400059A RID: 1434
		x_eet_Create_Login,
		// Token: 0x0400059B RID: 1435
		x_eet_Alter_Login,
		// Token: 0x0400059C RID: 1436
		x_eet_Drop_Login,
		// Token: 0x0400059D RID: 1437
		x_eet_Create_MsgType = 151,
		// Token: 0x0400059E RID: 1438
		x_eet_Alter_MsgType,
		// Token: 0x0400059F RID: 1439
		x_eet_Drop_MsgType,
		// Token: 0x040005A0 RID: 1440
		x_eet_Create_Contract,
		// Token: 0x040005A1 RID: 1441
		x_eet_Alter_Contract,
		// Token: 0x040005A2 RID: 1442
		x_eet_Drop_Contract,
		// Token: 0x040005A3 RID: 1443
		x_eet_Create_Queue,
		// Token: 0x040005A4 RID: 1444
		x_eet_Alter_Queue,
		// Token: 0x040005A5 RID: 1445
		x_eet_Drop_Queue,
		// Token: 0x040005A6 RID: 1446
		x_eet_Create_Service = 161,
		// Token: 0x040005A7 RID: 1447
		x_eet_Alter_Service,
		// Token: 0x040005A8 RID: 1448
		x_eet_Drop_Service,
		// Token: 0x040005A9 RID: 1449
		x_eet_Create_Route,
		// Token: 0x040005AA RID: 1450
		x_eet_Alter_Route,
		// Token: 0x040005AB RID: 1451
		x_eet_Drop_Route,
		// Token: 0x040005AC RID: 1452
		x_eet_Grant_Statement,
		// Token: 0x040005AD RID: 1453
		x_eet_Deny_Statement,
		// Token: 0x040005AE RID: 1454
		x_eet_Revoke_Statement,
		// Token: 0x040005AF RID: 1455
		x_eet_Grant_Object,
		// Token: 0x040005B0 RID: 1456
		x_eet_Deny_Object,
		// Token: 0x040005B1 RID: 1457
		x_eet_Revoke_Object,
		// Token: 0x040005B2 RID: 1458
		x_eet_Activation,
		// Token: 0x040005B3 RID: 1459
		x_eet_Create_Binding,
		// Token: 0x040005B4 RID: 1460
		x_eet_Alter_Binding,
		// Token: 0x040005B5 RID: 1461
		x_eet_Drop_Binding,
		// Token: 0x040005B6 RID: 1462
		x_eet_Create_XmlSchema,
		// Token: 0x040005B7 RID: 1463
		x_eet_Alter_XmlSchema,
		// Token: 0x040005B8 RID: 1464
		x_eet_Drop_XmlSchema,
		// Token: 0x040005B9 RID: 1465
		x_eet_Create_HttpEndpoint = 181,
		// Token: 0x040005BA RID: 1466
		x_eet_Alter_HttpEndpoint,
		// Token: 0x040005BB RID: 1467
		x_eet_Drop_HttpEndpoint,
		// Token: 0x040005BC RID: 1468
		x_eet_Create_Partition_Function = 191,
		// Token: 0x040005BD RID: 1469
		x_eet_Alter_Partition_Function,
		// Token: 0x040005BE RID: 1470
		x_eet_Drop_Partition_Function,
		// Token: 0x040005BF RID: 1471
		x_eet_Create_Partition_Scheme,
		// Token: 0x040005C0 RID: 1472
		x_eet_Alter_Partition_Scheme,
		// Token: 0x040005C1 RID: 1473
		x_eet_Drop_Partition_Scheme,
		// Token: 0x040005C2 RID: 1474
		x_eet_Create_Database = 201,
		// Token: 0x040005C3 RID: 1475
		x_eet_Alter_Database,
		// Token: 0x040005C4 RID: 1476
		x_eet_Drop_Database,
		// Token: 0x040005C5 RID: 1477
		x_eet_Trace_Start = 1000,
		// Token: 0x040005C6 RID: 1478
		x_eet_Trace_End = 1999
	}
}

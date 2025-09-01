using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>The <see cref="T:Microsoft.SqlServer.Server.TriggerAction" /> enumeration is used by the <see cref="T:Microsoft.SqlServer.Server.SqlTriggerContext" /> class to indicate what action fired the trigger.</summary>
	// Token: 0x02000061 RID: 97
	public enum TriggerAction
	{
		/// <summary>An invalid trigger action, one that is not exposed to the user, occurred.</summary>
		// Token: 0x040005C8 RID: 1480
		Invalid,
		/// <summary>An INSERT Transact-SQL statement was executed.</summary>
		// Token: 0x040005C9 RID: 1481
		Insert,
		/// <summary>An UPDATE Transact-SQL statement was executed.</summary>
		// Token: 0x040005CA RID: 1482
		Update,
		/// <summary>A DELETE Transact-SQL statement was executed.</summary>
		// Token: 0x040005CB RID: 1483
		Delete,
		/// <summary>A CREATE TABLE Transact-SQL statement was executed.</summary>
		// Token: 0x040005CC RID: 1484
		CreateTable = 21,
		/// <summary>An ALTER TABLE Transact-SQL statement was executed.</summary>
		// Token: 0x040005CD RID: 1485
		AlterTable,
		/// <summary>A DROP TABLE Transact-SQL statement was executed.</summary>
		// Token: 0x040005CE RID: 1486
		DropTable,
		/// <summary>A CREATE INDEX Transact-SQL statement was executed.</summary>
		// Token: 0x040005CF RID: 1487
		CreateIndex,
		/// <summary>An ALTER INDEX Transact-SQL statement was executed.</summary>
		// Token: 0x040005D0 RID: 1488
		AlterIndex,
		/// <summary>A DROP INDEX Transact-SQL statement was executed.</summary>
		// Token: 0x040005D1 RID: 1489
		DropIndex,
		/// <summary>A CREATE SYNONYM Transact-SQL statement was executed.</summary>
		// Token: 0x040005D2 RID: 1490
		CreateSynonym = 34,
		/// <summary>A DROP SYNONYM Transact-SQL statement was executed.</summary>
		// Token: 0x040005D3 RID: 1491
		DropSynonym = 36,
		/// <summary>Not available.</summary>
		// Token: 0x040005D4 RID: 1492
		CreateSecurityExpression = 31,
		/// <summary>Not available.</summary>
		// Token: 0x040005D5 RID: 1493
		DropSecurityExpression = 33,
		/// <summary>A CREATE VIEW Transact-SQL statement was executed.</summary>
		// Token: 0x040005D6 RID: 1494
		CreateView = 41,
		/// <summary>An ALTER VIEW Transact-SQL statement was executed.</summary>
		// Token: 0x040005D7 RID: 1495
		AlterView,
		/// <summary>A DROP VIEW Transact-SQL statement was executed.</summary>
		// Token: 0x040005D8 RID: 1496
		DropView,
		/// <summary>A CREATE PROCEDURE Transact-SQL statement was executed.</summary>
		// Token: 0x040005D9 RID: 1497
		CreateProcedure = 51,
		/// <summary>An ALTER PROCEDURE Transact-SQL statement was executed.</summary>
		// Token: 0x040005DA RID: 1498
		AlterProcedure,
		/// <summary>A DROP PROCEDURE Transact-SQL statement was executed.</summary>
		// Token: 0x040005DB RID: 1499
		DropProcedure,
		/// <summary>A CREATE FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x040005DC RID: 1500
		CreateFunction = 61,
		/// <summary>An ALTER FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x040005DD RID: 1501
		AlterFunction,
		/// <summary>A DROP FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x040005DE RID: 1502
		DropFunction,
		/// <summary>A CREATE TRIGGER Transact-SQL statement was executed.</summary>
		// Token: 0x040005DF RID: 1503
		CreateTrigger = 71,
		/// <summary>An ALTER TRIGGER Transact-SQL statement was executed.</summary>
		// Token: 0x040005E0 RID: 1504
		AlterTrigger,
		/// <summary>A DROP TRIGGER Transact-SQL statement was executed.</summary>
		// Token: 0x040005E1 RID: 1505
		DropTrigger,
		/// <summary>A CREATE EVENT NOTIFICATION Transact-SQL statement was executed.</summary>
		// Token: 0x040005E2 RID: 1506
		CreateEventNotification,
		/// <summary>A DROP EVENT NOTIFICATION Transact-SQL statement was executed.</summary>
		// Token: 0x040005E3 RID: 1507
		DropEventNotification = 76,
		/// <summary>A CREATE TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x040005E4 RID: 1508
		CreateType = 91,
		/// <summary>A DROP TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x040005E5 RID: 1509
		DropType = 93,
		/// <summary>A CREATE ASSEMBLY Transact-SQL statement was executed.</summary>
		// Token: 0x040005E6 RID: 1510
		CreateAssembly = 101,
		/// <summary>An ALTER ASSEMBLY Transact-SQL statement was executed.</summary>
		// Token: 0x040005E7 RID: 1511
		AlterAssembly,
		/// <summary>A DROP ASSEMBLY Transact-SQL statement was executed.</summary>
		// Token: 0x040005E8 RID: 1512
		DropAssembly,
		/// <summary>A CREATE USER Transact-SQL statement was executed.</summary>
		// Token: 0x040005E9 RID: 1513
		CreateUser = 131,
		/// <summary>An ALTER USER Transact-SQL statement was executed.</summary>
		// Token: 0x040005EA RID: 1514
		AlterUser,
		/// <summary>A DROP USER Transact-SQL statement was executed.</summary>
		// Token: 0x040005EB RID: 1515
		DropUser,
		/// <summary>A CREATE ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x040005EC RID: 1516
		CreateRole,
		/// <summary>An ALTER ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x040005ED RID: 1517
		AlterRole,
		/// <summary>A DROP ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x040005EE RID: 1518
		DropRole,
		/// <summary>A CREATE APPLICATION ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x040005EF RID: 1519
		CreateAppRole,
		/// <summary>An ALTER APPLICATION ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x040005F0 RID: 1520
		AlterAppRole,
		/// <summary>A DROP APPLICATION ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x040005F1 RID: 1521
		DropAppRole,
		/// <summary>A CREATE SCHEMA Transact-SQL statement was executed.</summary>
		// Token: 0x040005F2 RID: 1522
		CreateSchema = 141,
		/// <summary>An ALTER SCHEMA Transact-SQL statement was executed.</summary>
		// Token: 0x040005F3 RID: 1523
		AlterSchema,
		/// <summary>A DROP SCHEMA Transact-SQL statement was executed.</summary>
		// Token: 0x040005F4 RID: 1524
		DropSchema,
		/// <summary>A CREATE LOGIN Transact-SQL statement was executed.</summary>
		// Token: 0x040005F5 RID: 1525
		CreateLogin,
		/// <summary>An ALTER LOGIN Transact-SQL statement was executed.</summary>
		// Token: 0x040005F6 RID: 1526
		AlterLogin,
		/// <summary>A DROP LOGIN Transact-SQL statement was executed.</summary>
		// Token: 0x040005F7 RID: 1527
		DropLogin,
		/// <summary>A CREATE MESSAGE TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x040005F8 RID: 1528
		CreateMsgType = 151,
		/// <summary>A DROP MESSAGE TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x040005F9 RID: 1529
		DropMsgType = 153,
		/// <summary>A CREATE CONTRACT Transact-SQL statement was executed.</summary>
		// Token: 0x040005FA RID: 1530
		CreateContract,
		/// <summary>A DROP CONTRACT Transact-SQL statement was executed.</summary>
		// Token: 0x040005FB RID: 1531
		DropContract = 156,
		/// <summary>A CREATE QUEUE Transact-SQL statement was executed.</summary>
		// Token: 0x040005FC RID: 1532
		CreateQueue,
		/// <summary>An ALTER QUEUE Transact-SQL statement was executed.</summary>
		// Token: 0x040005FD RID: 1533
		AlterQueue,
		/// <summary>A DROP QUEUE Transact-SQL statement was executed.</summary>
		// Token: 0x040005FE RID: 1534
		DropQueue,
		/// <summary>A CREATE SERVICE Transact-SQL statement was executed.</summary>
		// Token: 0x040005FF RID: 1535
		CreateService = 161,
		/// <summary>An ALTER SERVICE Transact-SQL statement was executed.</summary>
		// Token: 0x04000600 RID: 1536
		AlterService,
		/// <summary>A DROP SERVICE Transact-SQL statement was executed.</summary>
		// Token: 0x04000601 RID: 1537
		DropService,
		/// <summary>A CREATE ROUTE Transact-SQL statement was executed.</summary>
		// Token: 0x04000602 RID: 1538
		CreateRoute,
		/// <summary>An ALTER ROUTE Transact-SQL statement was executed.</summary>
		// Token: 0x04000603 RID: 1539
		AlterRoute,
		/// <summary>A DROP ROUTE Transact-SQL statement was executed.</summary>
		// Token: 0x04000604 RID: 1540
		DropRoute,
		/// <summary>A GRANT Transact-SQL statement was executed.</summary>
		// Token: 0x04000605 RID: 1541
		GrantStatement,
		/// <summary>A DENY Transact-SQL statement was executed.</summary>
		// Token: 0x04000606 RID: 1542
		DenyStatement,
		/// <summary>A REVOKE Transact-SQL statement was executed.</summary>
		// Token: 0x04000607 RID: 1543
		RevokeStatement,
		/// <summary>A GRANT OBJECT Transact-SQL statement was executed.</summary>
		// Token: 0x04000608 RID: 1544
		GrantObject,
		/// <summary>A DENY Object Permissions Transact-SQL statement was executed.</summary>
		// Token: 0x04000609 RID: 1545
		DenyObject,
		/// <summary>A REVOKE OBJECT Transact-SQL statement was executed.</summary>
		// Token: 0x0400060A RID: 1546
		RevokeObject,
		/// <summary>A CREATE_REMOTE_SERVICE_BINDING event type was specified when an event notification was created on the database or server instance.</summary>
		// Token: 0x0400060B RID: 1547
		CreateBinding = 174,
		/// <summary>An ALTER_REMOTE_SERVICE_BINDING event type was specified when an event notification was created on the database or server instance.</summary>
		// Token: 0x0400060C RID: 1548
		AlterBinding,
		/// <summary>A DROP_REMOTE_SERVICE_BINDING event type was specified when an event notification was created on the database or server instance.</summary>
		// Token: 0x0400060D RID: 1549
		DropBinding,
		/// <summary>A CREATE PARTITION FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x0400060E RID: 1550
		CreatePartitionFunction = 191,
		/// <summary>An ALTER PARTITION FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x0400060F RID: 1551
		AlterPartitionFunction,
		/// <summary>A DROP PARTITION FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x04000610 RID: 1552
		DropPartitionFunction,
		/// <summary>A CREATE PARTITION SCHEME Transact-SQL statement was executed.</summary>
		// Token: 0x04000611 RID: 1553
		CreatePartitionScheme,
		/// <summary>An ALTER PARTITION SCHEME Transact-SQL statement was executed.</summary>
		// Token: 0x04000612 RID: 1554
		AlterPartitionScheme,
		/// <summary>A DROP PARTITION SCHEME Transact-SQL statement was executed.</summary>
		// Token: 0x04000613 RID: 1555
		DropPartitionScheme
	}
}

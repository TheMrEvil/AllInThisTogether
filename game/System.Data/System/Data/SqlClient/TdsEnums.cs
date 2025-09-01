using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200023D RID: 573
	internal static class TdsEnums
	{
		// Token: 0x06001BCF RID: 7119 RVA: 0x0007DC2C File Offset: 0x0007BE2C
		internal static string GetSniContextEnumName(SniContext sniContext)
		{
			switch (sniContext)
			{
			case SniContext.Undefined:
				return "Undefined";
			case SniContext.Snix_Connect:
				return "Snix_Connect";
			case SniContext.Snix_PreLoginBeforeSuccessfulWrite:
				return "Snix_PreLoginBeforeSuccessfulWrite";
			case SniContext.Snix_PreLogin:
				return "Snix_PreLogin";
			case SniContext.Snix_LoginSspi:
				return "Snix_LoginSspi";
			case SniContext.Snix_ProcessSspi:
				return "Snix_ProcessSspi";
			case SniContext.Snix_Login:
				return "Snix_Login";
			case SniContext.Snix_EnableMars:
				return "Snix_EnableMars";
			case SniContext.Snix_AutoEnlist:
				return "Snix_AutoEnlist";
			case SniContext.Snix_GetMarsSession:
				return "Snix_GetMarsSession";
			case SniContext.Snix_Execute:
				return "Snix_Execute";
			case SniContext.Snix_Read:
				return "Snix_Read";
			case SniContext.Snix_Close:
				return "Snix_Close";
			case SniContext.Snix_SendRows:
				return "Snix_SendRows";
			default:
				return null;
			}
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0007DCD0 File Offset: 0x0007BED0
		// Note: this type is marked as 'beforefieldinit'.
		static TdsEnums()
		{
		}

		// Token: 0x04001163 RID: 4451
		public const string SQL_PROVIDER_NAME = "Core .Net SqlClient Data Provider";

		// Token: 0x04001164 RID: 4452
		public static readonly decimal SQL_SMALL_MONEY_MIN = new decimal(-214748.3648);

		// Token: 0x04001165 RID: 4453
		public static readonly decimal SQL_SMALL_MONEY_MAX = new decimal(214748.3647);

		// Token: 0x04001166 RID: 4454
		public const SqlDbType SmallVarBinary = (SqlDbType)24;

		// Token: 0x04001167 RID: 4455
		public const string TCP = "tcp";

		// Token: 0x04001168 RID: 4456
		public const string NP = "np";

		// Token: 0x04001169 RID: 4457
		public const string RPC = "rpc";

		// Token: 0x0400116A RID: 4458
		public const string BV = "bv";

		// Token: 0x0400116B RID: 4459
		public const string ADSP = "adsp";

		// Token: 0x0400116C RID: 4460
		public const string SPX = "spx";

		// Token: 0x0400116D RID: 4461
		public const string VIA = "via";

		// Token: 0x0400116E RID: 4462
		public const string LPC = "lpc";

		// Token: 0x0400116F RID: 4463
		public const string ADMIN = "admin";

		// Token: 0x04001170 RID: 4464
		public const string INIT_SSPI_PACKAGE = "InitSSPIPackage";

		// Token: 0x04001171 RID: 4465
		public const string INIT_SESSION = "InitSession";

		// Token: 0x04001172 RID: 4466
		public const string CONNECTION_GET_SVR_USER = "ConnectionGetSvrUser";

		// Token: 0x04001173 RID: 4467
		public const string GEN_CLIENT_CONTEXT = "GenClientContext";

		// Token: 0x04001174 RID: 4468
		public const byte SOFTFLUSH = 0;

		// Token: 0x04001175 RID: 4469
		public const byte HARDFLUSH = 1;

		// Token: 0x04001176 RID: 4470
		public const byte IGNORE = 2;

		// Token: 0x04001177 RID: 4471
		public const int HEADER_LEN = 8;

		// Token: 0x04001178 RID: 4472
		public const int HEADER_LEN_FIELD_OFFSET = 2;

		// Token: 0x04001179 RID: 4473
		public const int YUKON_HEADER_LEN = 12;

		// Token: 0x0400117A RID: 4474
		public const int MARS_ID_OFFSET = 8;

		// Token: 0x0400117B RID: 4475
		public const int HEADERTYPE_QNOTIFICATION = 1;

		// Token: 0x0400117C RID: 4476
		public const int HEADERTYPE_MARS = 2;

		// Token: 0x0400117D RID: 4477
		public const int HEADERTYPE_TRACE = 3;

		// Token: 0x0400117E RID: 4478
		public const int SUCCEED = 1;

		// Token: 0x0400117F RID: 4479
		public const int FAIL = 0;

		// Token: 0x04001180 RID: 4480
		public const short TYPE_SIZE_LIMIT = 8000;

		// Token: 0x04001181 RID: 4481
		public const int MIN_PACKET_SIZE = 512;

		// Token: 0x04001182 RID: 4482
		public const int DEFAULT_LOGIN_PACKET_SIZE = 4096;

		// Token: 0x04001183 RID: 4483
		public const int MAX_PRELOGIN_PAYLOAD_LENGTH = 1024;

		// Token: 0x04001184 RID: 4484
		public const int MAX_PACKET_SIZE = 32768;

		// Token: 0x04001185 RID: 4485
		public const int MAX_SERVER_USER_NAME = 256;

		// Token: 0x04001186 RID: 4486
		public const byte MIN_ERROR_CLASS = 11;

		// Token: 0x04001187 RID: 4487
		public const byte MAX_USER_CORRECTABLE_ERROR_CLASS = 16;

		// Token: 0x04001188 RID: 4488
		public const byte FATAL_ERROR_CLASS = 20;

		// Token: 0x04001189 RID: 4489
		public const byte MT_SQL = 1;

		// Token: 0x0400118A RID: 4490
		public const byte MT_LOGIN = 2;

		// Token: 0x0400118B RID: 4491
		public const byte MT_RPC = 3;

		// Token: 0x0400118C RID: 4492
		public const byte MT_TOKENS = 4;

		// Token: 0x0400118D RID: 4493
		public const byte MT_BINARY = 5;

		// Token: 0x0400118E RID: 4494
		public const byte MT_ATTN = 6;

		// Token: 0x0400118F RID: 4495
		public const byte MT_BULK = 7;

		// Token: 0x04001190 RID: 4496
		public const byte MT_OPEN = 8;

		// Token: 0x04001191 RID: 4497
		public const byte MT_CLOSE = 9;

		// Token: 0x04001192 RID: 4498
		public const byte MT_ERROR = 10;

		// Token: 0x04001193 RID: 4499
		public const byte MT_ACK = 11;

		// Token: 0x04001194 RID: 4500
		public const byte MT_ECHO = 12;

		// Token: 0x04001195 RID: 4501
		public const byte MT_LOGOUT = 13;

		// Token: 0x04001196 RID: 4502
		public const byte MT_TRANS = 14;

		// Token: 0x04001197 RID: 4503
		public const byte MT_OLEDB = 15;

		// Token: 0x04001198 RID: 4504
		public const byte MT_LOGIN7 = 16;

		// Token: 0x04001199 RID: 4505
		public const byte MT_SSPI = 17;

		// Token: 0x0400119A RID: 4506
		public const byte MT_PRELOGIN = 18;

		// Token: 0x0400119B RID: 4507
		public const byte ST_EOM = 1;

		// Token: 0x0400119C RID: 4508
		public const byte ST_AACK = 2;

		// Token: 0x0400119D RID: 4509
		public const byte ST_IGNORE = 2;

		// Token: 0x0400119E RID: 4510
		public const byte ST_BATCH = 4;

		// Token: 0x0400119F RID: 4511
		public const byte ST_RESET_CONNECTION = 8;

		// Token: 0x040011A0 RID: 4512
		public const byte ST_RESET_CONNECTION_PRESERVE_TRANSACTION = 16;

		// Token: 0x040011A1 RID: 4513
		public const byte SQLCOLFMT = 161;

		// Token: 0x040011A2 RID: 4514
		public const byte SQLPROCID = 124;

		// Token: 0x040011A3 RID: 4515
		public const byte SQLCOLNAME = 160;

		// Token: 0x040011A4 RID: 4516
		public const byte SQLTABNAME = 164;

		// Token: 0x040011A5 RID: 4517
		public const byte SQLCOLINFO = 165;

		// Token: 0x040011A6 RID: 4518
		public const byte SQLALTNAME = 167;

		// Token: 0x040011A7 RID: 4519
		public const byte SQLALTFMT = 168;

		// Token: 0x040011A8 RID: 4520
		public const byte SQLERROR = 170;

		// Token: 0x040011A9 RID: 4521
		public const byte SQLINFO = 171;

		// Token: 0x040011AA RID: 4522
		public const byte SQLRETURNVALUE = 172;

		// Token: 0x040011AB RID: 4523
		public const byte SQLRETURNSTATUS = 121;

		// Token: 0x040011AC RID: 4524
		public const byte SQLRETURNTOK = 219;

		// Token: 0x040011AD RID: 4525
		public const byte SQLALTCONTROL = 175;

		// Token: 0x040011AE RID: 4526
		public const byte SQLROW = 209;

		// Token: 0x040011AF RID: 4527
		public const byte SQLNBCROW = 210;

		// Token: 0x040011B0 RID: 4528
		public const byte SQLALTROW = 211;

		// Token: 0x040011B1 RID: 4529
		public const byte SQLDONE = 253;

		// Token: 0x040011B2 RID: 4530
		public const byte SQLDONEPROC = 254;

		// Token: 0x040011B3 RID: 4531
		public const byte SQLDONEINPROC = 255;

		// Token: 0x040011B4 RID: 4532
		public const byte SQLOFFSET = 120;

		// Token: 0x040011B5 RID: 4533
		public const byte SQLORDER = 169;

		// Token: 0x040011B6 RID: 4534
		public const byte SQLDEBUG_CMD = 96;

		// Token: 0x040011B7 RID: 4535
		public const byte SQLLOGINACK = 173;

		// Token: 0x040011B8 RID: 4536
		public const byte SQLFEATUREEXTACK = 174;

		// Token: 0x040011B9 RID: 4537
		public const byte SQLSESSIONSTATE = 228;

		// Token: 0x040011BA RID: 4538
		public const byte SQLENVCHANGE = 227;

		// Token: 0x040011BB RID: 4539
		public const byte SQLSECLEVEL = 237;

		// Token: 0x040011BC RID: 4540
		public const byte SQLROWCRC = 57;

		// Token: 0x040011BD RID: 4541
		public const byte SQLCOLMETADATA = 129;

		// Token: 0x040011BE RID: 4542
		public const byte SQLALTMETADATA = 136;

		// Token: 0x040011BF RID: 4543
		public const byte SQLSSPI = 237;

		// Token: 0x040011C0 RID: 4544
		public const byte ENV_DATABASE = 1;

		// Token: 0x040011C1 RID: 4545
		public const byte ENV_LANG = 2;

		// Token: 0x040011C2 RID: 4546
		public const byte ENV_CHARSET = 3;

		// Token: 0x040011C3 RID: 4547
		public const byte ENV_PACKETSIZE = 4;

		// Token: 0x040011C4 RID: 4548
		public const byte ENV_LOCALEID = 5;

		// Token: 0x040011C5 RID: 4549
		public const byte ENV_COMPFLAGS = 6;

		// Token: 0x040011C6 RID: 4550
		public const byte ENV_COLLATION = 7;

		// Token: 0x040011C7 RID: 4551
		public const byte ENV_BEGINTRAN = 8;

		// Token: 0x040011C8 RID: 4552
		public const byte ENV_COMMITTRAN = 9;

		// Token: 0x040011C9 RID: 4553
		public const byte ENV_ROLLBACKTRAN = 10;

		// Token: 0x040011CA RID: 4554
		public const byte ENV_ENLISTDTC = 11;

		// Token: 0x040011CB RID: 4555
		public const byte ENV_DEFECTDTC = 12;

		// Token: 0x040011CC RID: 4556
		public const byte ENV_LOGSHIPNODE = 13;

		// Token: 0x040011CD RID: 4557
		public const byte ENV_PROMOTETRANSACTION = 15;

		// Token: 0x040011CE RID: 4558
		public const byte ENV_TRANSACTIONMANAGERADDRESS = 16;

		// Token: 0x040011CF RID: 4559
		public const byte ENV_TRANSACTIONENDED = 17;

		// Token: 0x040011D0 RID: 4560
		public const byte ENV_SPRESETCONNECTIONACK = 18;

		// Token: 0x040011D1 RID: 4561
		public const byte ENV_USERINSTANCE = 19;

		// Token: 0x040011D2 RID: 4562
		public const byte ENV_ROUTING = 20;

		// Token: 0x040011D3 RID: 4563
		public const int DONE_MORE = 1;

		// Token: 0x040011D4 RID: 4564
		public const int DONE_ERROR = 2;

		// Token: 0x040011D5 RID: 4565
		public const int DONE_INXACT = 4;

		// Token: 0x040011D6 RID: 4566
		public const int DONE_PROC = 8;

		// Token: 0x040011D7 RID: 4567
		public const int DONE_COUNT = 16;

		// Token: 0x040011D8 RID: 4568
		public const int DONE_ATTN = 32;

		// Token: 0x040011D9 RID: 4569
		public const int DONE_INPROC = 64;

		// Token: 0x040011DA RID: 4570
		public const int DONE_RPCINBATCH = 128;

		// Token: 0x040011DB RID: 4571
		public const int DONE_SRVERROR = 256;

		// Token: 0x040011DC RID: 4572
		public const int DONE_FMTSENT = 32768;

		// Token: 0x040011DD RID: 4573
		public const byte FEATUREEXT_TERMINATOR = 255;

		// Token: 0x040011DE RID: 4574
		public const byte FEATUREEXT_SRECOVERY = 1;

		// Token: 0x040011DF RID: 4575
		public const byte FEATUREEXT_GLOBALTRANSACTIONS = 5;

		// Token: 0x040011E0 RID: 4576
		public const byte FEATUREEXT_FEDAUTH = 2;

		// Token: 0x040011E1 RID: 4577
		public const byte FEDAUTHLIB_LIVEID = 0;

		// Token: 0x040011E2 RID: 4578
		public const byte FEDAUTHLIB_SECURITYTOKEN = 1;

		// Token: 0x040011E3 RID: 4579
		public const byte FEDAUTHLIB_ADAL = 2;

		// Token: 0x040011E4 RID: 4580
		public const byte FEDAUTHLIB_RESERVED = 127;

		// Token: 0x040011E5 RID: 4581
		public const byte MAX_LOG_NAME = 30;

		// Token: 0x040011E6 RID: 4582
		public const byte MAX_PROG_NAME = 10;

		// Token: 0x040011E7 RID: 4583
		public const byte SEC_COMP_LEN = 8;

		// Token: 0x040011E8 RID: 4584
		public const byte MAX_PK_LEN = 6;

		// Token: 0x040011E9 RID: 4585
		public const byte MAX_NIC_SIZE = 6;

		// Token: 0x040011EA RID: 4586
		public const byte SQLVARIANT_SIZE = 2;

		// Token: 0x040011EB RID: 4587
		public const byte VERSION_SIZE = 4;

		// Token: 0x040011EC RID: 4588
		public const int CLIENT_PROG_VER = 100663296;

		// Token: 0x040011ED RID: 4589
		public const int YUKON_LOG_REC_FIXED_LEN = 94;

		// Token: 0x040011EE RID: 4590
		public const int TEXT_TIME_STAMP_LEN = 8;

		// Token: 0x040011EF RID: 4591
		public const int COLLATION_INFO_LEN = 4;

		// Token: 0x040011F0 RID: 4592
		public const int YUKON_MAJOR = 114;

		// Token: 0x040011F1 RID: 4593
		public const int KATMAI_MAJOR = 115;

		// Token: 0x040011F2 RID: 4594
		public const int DENALI_MAJOR = 116;

		// Token: 0x040011F3 RID: 4595
		public const int YUKON_INCREMENT = 9;

		// Token: 0x040011F4 RID: 4596
		public const int KATMAI_INCREMENT = 11;

		// Token: 0x040011F5 RID: 4597
		public const int DENALI_INCREMENT = 0;

		// Token: 0x040011F6 RID: 4598
		public const int YUKON_RTM_MINOR = 2;

		// Token: 0x040011F7 RID: 4599
		public const int KATMAI_MINOR = 3;

		// Token: 0x040011F8 RID: 4600
		public const int DENALI_MINOR = 4;

		// Token: 0x040011F9 RID: 4601
		public const int ORDER_68000 = 1;

		// Token: 0x040011FA RID: 4602
		public const int USE_DB_ON = 1;

		// Token: 0x040011FB RID: 4603
		public const int INIT_DB_FATAL = 1;

		// Token: 0x040011FC RID: 4604
		public const int SET_LANG_ON = 1;

		// Token: 0x040011FD RID: 4605
		public const int INIT_LANG_FATAL = 1;

		// Token: 0x040011FE RID: 4606
		public const int ODBC_ON = 1;

		// Token: 0x040011FF RID: 4607
		public const int SSPI_ON = 1;

		// Token: 0x04001200 RID: 4608
		public const int REPL_ON = 3;

		// Token: 0x04001201 RID: 4609
		public const int READONLY_INTENT_ON = 1;

		// Token: 0x04001202 RID: 4610
		public const byte SQLLenMask = 48;

		// Token: 0x04001203 RID: 4611
		public const byte SQLFixedLen = 48;

		// Token: 0x04001204 RID: 4612
		public const byte SQLVarLen = 32;

		// Token: 0x04001205 RID: 4613
		public const byte SQLZeroLen = 16;

		// Token: 0x04001206 RID: 4614
		public const byte SQLVarCnt = 0;

		// Token: 0x04001207 RID: 4615
		public const byte SQLDifferentName = 32;

		// Token: 0x04001208 RID: 4616
		public const byte SQLExpression = 4;

		// Token: 0x04001209 RID: 4617
		public const byte SQLKey = 8;

		// Token: 0x0400120A RID: 4618
		public const byte SQLHidden = 16;

		// Token: 0x0400120B RID: 4619
		public const byte Nullable = 1;

		// Token: 0x0400120C RID: 4620
		public const byte Identity = 16;

		// Token: 0x0400120D RID: 4621
		public const byte Updatability = 11;

		// Token: 0x0400120E RID: 4622
		public const byte ClrFixedLen = 1;

		// Token: 0x0400120F RID: 4623
		public const byte IsColumnSet = 4;

		// Token: 0x04001210 RID: 4624
		public const uint VARLONGNULL = 4294967295U;

		// Token: 0x04001211 RID: 4625
		public const int VARNULL = 65535;

		// Token: 0x04001212 RID: 4626
		public const int MAXSIZE = 8000;

		// Token: 0x04001213 RID: 4627
		public const byte FIXEDNULL = 0;

		// Token: 0x04001214 RID: 4628
		public const ulong UDTNULL = 18446744073709551615UL;

		// Token: 0x04001215 RID: 4629
		public const int SQLVOID = 31;

		// Token: 0x04001216 RID: 4630
		public const int SQLTEXT = 35;

		// Token: 0x04001217 RID: 4631
		public const int SQLVARBINARY = 37;

		// Token: 0x04001218 RID: 4632
		public const int SQLINTN = 38;

		// Token: 0x04001219 RID: 4633
		public const int SQLVARCHAR = 39;

		// Token: 0x0400121A RID: 4634
		public const int SQLBINARY = 45;

		// Token: 0x0400121B RID: 4635
		public const int SQLIMAGE = 34;

		// Token: 0x0400121C RID: 4636
		public const int SQLCHAR = 47;

		// Token: 0x0400121D RID: 4637
		public const int SQLINT1 = 48;

		// Token: 0x0400121E RID: 4638
		public const int SQLBIT = 50;

		// Token: 0x0400121F RID: 4639
		public const int SQLINT2 = 52;

		// Token: 0x04001220 RID: 4640
		public const int SQLINT4 = 56;

		// Token: 0x04001221 RID: 4641
		public const int SQLMONEY = 60;

		// Token: 0x04001222 RID: 4642
		public const int SQLDATETIME = 61;

		// Token: 0x04001223 RID: 4643
		public const int SQLFLT8 = 62;

		// Token: 0x04001224 RID: 4644
		public const int SQLFLTN = 109;

		// Token: 0x04001225 RID: 4645
		public const int SQLMONEYN = 110;

		// Token: 0x04001226 RID: 4646
		public const int SQLDATETIMN = 111;

		// Token: 0x04001227 RID: 4647
		public const int SQLFLT4 = 59;

		// Token: 0x04001228 RID: 4648
		public const int SQLMONEY4 = 122;

		// Token: 0x04001229 RID: 4649
		public const int SQLDATETIM4 = 58;

		// Token: 0x0400122A RID: 4650
		public const int SQLDECIMALN = 106;

		// Token: 0x0400122B RID: 4651
		public const int SQLNUMERICN = 108;

		// Token: 0x0400122C RID: 4652
		public const int SQLUNIQUEID = 36;

		// Token: 0x0400122D RID: 4653
		public const int SQLBIGCHAR = 175;

		// Token: 0x0400122E RID: 4654
		public const int SQLBIGVARCHAR = 167;

		// Token: 0x0400122F RID: 4655
		public const int SQLBIGBINARY = 173;

		// Token: 0x04001230 RID: 4656
		public const int SQLBIGVARBINARY = 165;

		// Token: 0x04001231 RID: 4657
		public const int SQLBITN = 104;

		// Token: 0x04001232 RID: 4658
		public const int SQLNCHAR = 239;

		// Token: 0x04001233 RID: 4659
		public const int SQLNVARCHAR = 231;

		// Token: 0x04001234 RID: 4660
		public const int SQLNTEXT = 99;

		// Token: 0x04001235 RID: 4661
		public const int SQLUDT = 240;

		// Token: 0x04001236 RID: 4662
		public const int AOPCNTB = 9;

		// Token: 0x04001237 RID: 4663
		public const int AOPSTDEV = 48;

		// Token: 0x04001238 RID: 4664
		public const int AOPSTDEVP = 49;

		// Token: 0x04001239 RID: 4665
		public const int AOPVAR = 50;

		// Token: 0x0400123A RID: 4666
		public const int AOPVARP = 51;

		// Token: 0x0400123B RID: 4667
		public const int AOPCNT = 75;

		// Token: 0x0400123C RID: 4668
		public const int AOPSUM = 77;

		// Token: 0x0400123D RID: 4669
		public const int AOPAVG = 79;

		// Token: 0x0400123E RID: 4670
		public const int AOPMIN = 81;

		// Token: 0x0400123F RID: 4671
		public const int AOPMAX = 82;

		// Token: 0x04001240 RID: 4672
		public const int AOPANY = 83;

		// Token: 0x04001241 RID: 4673
		public const int AOPNOOP = 86;

		// Token: 0x04001242 RID: 4674
		public const int SQLTIMESTAMP = 80;

		// Token: 0x04001243 RID: 4675
		public const int MAX_NUMERIC_LEN = 17;

		// Token: 0x04001244 RID: 4676
		public const int DEFAULT_NUMERIC_PRECISION = 29;

		// Token: 0x04001245 RID: 4677
		public const int SPHINX_DEFAULT_NUMERIC_PRECISION = 28;

		// Token: 0x04001246 RID: 4678
		public const int MAX_NUMERIC_PRECISION = 38;

		// Token: 0x04001247 RID: 4679
		public const byte UNKNOWN_PRECISION_SCALE = 255;

		// Token: 0x04001248 RID: 4680
		public const int SQLINT8 = 127;

		// Token: 0x04001249 RID: 4681
		public const int SQLVARIANT = 98;

		// Token: 0x0400124A RID: 4682
		public const int SQLXMLTYPE = 241;

		// Token: 0x0400124B RID: 4683
		public const int XMLUNICODEBOM = 65279;

		// Token: 0x0400124C RID: 4684
		public static readonly byte[] XMLUNICODEBOMBYTES = new byte[]
		{
			byte.MaxValue,
			254
		};

		// Token: 0x0400124D RID: 4685
		public const int SQLTABLE = 243;

		// Token: 0x0400124E RID: 4686
		public const int SQLDATE = 40;

		// Token: 0x0400124F RID: 4687
		public const int SQLTIME = 41;

		// Token: 0x04001250 RID: 4688
		public const int SQLDATETIME2 = 42;

		// Token: 0x04001251 RID: 4689
		public const int SQLDATETIMEOFFSET = 43;

		// Token: 0x04001252 RID: 4690
		public const int DEFAULT_VARTIME_SCALE = 7;

		// Token: 0x04001253 RID: 4691
		public const ulong SQL_PLP_NULL = 18446744073709551615UL;

		// Token: 0x04001254 RID: 4692
		public const ulong SQL_PLP_UNKNOWNLEN = 18446744073709551614UL;

		// Token: 0x04001255 RID: 4693
		public const int SQL_PLP_CHUNK_TERMINATOR = 0;

		// Token: 0x04001256 RID: 4694
		public const ushort SQL_USHORTVARMAXLEN = 65535;

		// Token: 0x04001257 RID: 4695
		public const byte TVP_ROWCOUNT_ESTIMATE = 18;

		// Token: 0x04001258 RID: 4696
		public const byte TVP_ROW_TOKEN = 1;

		// Token: 0x04001259 RID: 4697
		public const byte TVP_END_TOKEN = 0;

		// Token: 0x0400125A RID: 4698
		public const ushort TVP_NOMETADATA_TOKEN = 65535;

		// Token: 0x0400125B RID: 4699
		public const byte TVP_ORDER_UNIQUE_TOKEN = 16;

		// Token: 0x0400125C RID: 4700
		public const int TVP_DEFAULT_COLUMN = 512;

		// Token: 0x0400125D RID: 4701
		public const byte TVP_ORDERASC_FLAG = 1;

		// Token: 0x0400125E RID: 4702
		public const byte TVP_ORDERDESC_FLAG = 2;

		// Token: 0x0400125F RID: 4703
		public const byte TVP_UNIQUE_FLAG = 4;

		// Token: 0x04001260 RID: 4704
		public const string SP_EXECUTESQL = "sp_executesql";

		// Token: 0x04001261 RID: 4705
		public const string SP_PREPEXEC = "sp_prepexec";

		// Token: 0x04001262 RID: 4706
		public const string SP_PREPARE = "sp_prepare";

		// Token: 0x04001263 RID: 4707
		public const string SP_EXECUTE = "sp_execute";

		// Token: 0x04001264 RID: 4708
		public const string SP_UNPREPARE = "sp_unprepare";

		// Token: 0x04001265 RID: 4709
		public const string SP_PARAMS = "sp_procedure_params_rowset";

		// Token: 0x04001266 RID: 4710
		public const string SP_PARAMS_MANAGED = "sp_procedure_params_managed";

		// Token: 0x04001267 RID: 4711
		public const string SP_PARAMS_MGD10 = "sp_procedure_params_100_managed";

		// Token: 0x04001268 RID: 4712
		public const ushort RPC_PROCID_CURSOR = 1;

		// Token: 0x04001269 RID: 4713
		public const ushort RPC_PROCID_CURSOROPEN = 2;

		// Token: 0x0400126A RID: 4714
		public const ushort RPC_PROCID_CURSORPREPARE = 3;

		// Token: 0x0400126B RID: 4715
		public const ushort RPC_PROCID_CURSOREXECUTE = 4;

		// Token: 0x0400126C RID: 4716
		public const ushort RPC_PROCID_CURSORPREPEXEC = 5;

		// Token: 0x0400126D RID: 4717
		public const ushort RPC_PROCID_CURSORUNPREPARE = 6;

		// Token: 0x0400126E RID: 4718
		public const ushort RPC_PROCID_CURSORFETCH = 7;

		// Token: 0x0400126F RID: 4719
		public const ushort RPC_PROCID_CURSOROPTION = 8;

		// Token: 0x04001270 RID: 4720
		public const ushort RPC_PROCID_CURSORCLOSE = 9;

		// Token: 0x04001271 RID: 4721
		public const ushort RPC_PROCID_EXECUTESQL = 10;

		// Token: 0x04001272 RID: 4722
		public const ushort RPC_PROCID_PREPARE = 11;

		// Token: 0x04001273 RID: 4723
		public const ushort RPC_PROCID_EXECUTE = 12;

		// Token: 0x04001274 RID: 4724
		public const ushort RPC_PROCID_PREPEXEC = 13;

		// Token: 0x04001275 RID: 4725
		public const ushort RPC_PROCID_PREPEXECRPC = 14;

		// Token: 0x04001276 RID: 4726
		public const ushort RPC_PROCID_UNPREPARE = 15;

		// Token: 0x04001277 RID: 4727
		public const string TRANS_BEGIN = "BEGIN TRANSACTION";

		// Token: 0x04001278 RID: 4728
		public const string TRANS_COMMIT = "COMMIT TRANSACTION";

		// Token: 0x04001279 RID: 4729
		public const string TRANS_ROLLBACK = "ROLLBACK TRANSACTION";

		// Token: 0x0400127A RID: 4730
		public const string TRANS_IF_ROLLBACK = "IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION";

		// Token: 0x0400127B RID: 4731
		public const string TRANS_SAVE = "SAVE TRANSACTION";

		// Token: 0x0400127C RID: 4732
		public const string TRANS_READ_COMMITTED = "SET TRANSACTION ISOLATION LEVEL READ COMMITTED";

		// Token: 0x0400127D RID: 4733
		public const string TRANS_READ_UNCOMMITTED = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";

		// Token: 0x0400127E RID: 4734
		public const string TRANS_REPEATABLE_READ = "SET TRANSACTION ISOLATION LEVEL REPEATABLE READ";

		// Token: 0x0400127F RID: 4735
		public const string TRANS_SERIALIZABLE = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE";

		// Token: 0x04001280 RID: 4736
		public const string TRANS_SNAPSHOT = "SET TRANSACTION ISOLATION LEVEL SNAPSHOT";

		// Token: 0x04001281 RID: 4737
		public const byte SHILOH_RPCBATCHFLAG = 128;

		// Token: 0x04001282 RID: 4738
		public const byte YUKON_RPCBATCHFLAG = 255;

		// Token: 0x04001283 RID: 4739
		public const byte RPC_RECOMPILE = 1;

		// Token: 0x04001284 RID: 4740
		public const byte RPC_NOMETADATA = 2;

		// Token: 0x04001285 RID: 4741
		public const byte RPC_PARAM_BYREF = 1;

		// Token: 0x04001286 RID: 4742
		public const byte RPC_PARAM_DEFAULT = 2;

		// Token: 0x04001287 RID: 4743
		public const byte RPC_PARAM_IS_LOB_COOKIE = 8;

		// Token: 0x04001288 RID: 4744
		public const string PARAM_OUTPUT = "output";

		// Token: 0x04001289 RID: 4745
		public const int MAX_PARAMETER_NAME_LENGTH = 128;

		// Token: 0x0400128A RID: 4746
		public const string FMTONLY_ON = " SET FMTONLY ON;";

		// Token: 0x0400128B RID: 4747
		public const string FMTONLY_OFF = " SET FMTONLY OFF;";

		// Token: 0x0400128C RID: 4748
		public const string BROWSE_ON = " SET NO_BROWSETABLE ON;";

		// Token: 0x0400128D RID: 4749
		public const string BROWSE_OFF = " SET NO_BROWSETABLE OFF;";

		// Token: 0x0400128E RID: 4750
		public const string TABLE = "Table";

		// Token: 0x0400128F RID: 4751
		public const int EXEC_THRESHOLD = 3;

		// Token: 0x04001290 RID: 4752
		public const short TIMEOUT_EXPIRED = -2;

		// Token: 0x04001291 RID: 4753
		public const short ENCRYPTION_NOT_SUPPORTED = 20;

		// Token: 0x04001292 RID: 4754
		public const int LOGON_FAILED = 18456;

		// Token: 0x04001293 RID: 4755
		public const int PASSWORD_EXPIRED = 18488;

		// Token: 0x04001294 RID: 4756
		public const int IMPERSONATION_FAILED = 1346;

		// Token: 0x04001295 RID: 4757
		public const int P_TOKENTOOLONG = 103;

		// Token: 0x04001296 RID: 4758
		public const uint SNI_UNINITIALIZED = 4294967295U;

		// Token: 0x04001297 RID: 4759
		public const uint SNI_SUCCESS = 0U;

		// Token: 0x04001298 RID: 4760
		public const uint SNI_ERROR = 1U;

		// Token: 0x04001299 RID: 4761
		public const uint SNI_WAIT_TIMEOUT = 258U;

		// Token: 0x0400129A RID: 4762
		public const uint SNI_SUCCESS_IO_PENDING = 997U;

		// Token: 0x0400129B RID: 4763
		public const short SNI_WSAECONNRESET = 10054;

		// Token: 0x0400129C RID: 4764
		public const uint SNI_QUEUE_FULL = 1048576U;

		// Token: 0x0400129D RID: 4765
		public const uint SNI_SSL_VALIDATE_CERTIFICATE = 1U;

		// Token: 0x0400129E RID: 4766
		public const uint SNI_SSL_USE_SCHANNEL_CACHE = 2U;

		// Token: 0x0400129F RID: 4767
		public const uint SNI_SSL_IGNORE_CHANNEL_BINDINGS = 16U;

		// Token: 0x040012A0 RID: 4768
		public const string DEFAULT_ENGLISH_CODE_PAGE_STRING = "iso_1";

		// Token: 0x040012A1 RID: 4769
		public const short DEFAULT_ENGLISH_CODE_PAGE_VALUE = 1252;

		// Token: 0x040012A2 RID: 4770
		public const short CHARSET_CODE_PAGE_OFFSET = 2;

		// Token: 0x040012A3 RID: 4771
		internal const int MAX_SERVERNAME = 255;

		// Token: 0x040012A4 RID: 4772
		internal const ushort SELECT = 193;

		// Token: 0x040012A5 RID: 4773
		internal const ushort INSERT = 195;

		// Token: 0x040012A6 RID: 4774
		internal const ushort DELETE = 196;

		// Token: 0x040012A7 RID: 4775
		internal const ushort UPDATE = 197;

		// Token: 0x040012A8 RID: 4776
		internal const ushort ABORT = 210;

		// Token: 0x040012A9 RID: 4777
		internal const ushort BEGINXACT = 212;

		// Token: 0x040012AA RID: 4778
		internal const ushort ENDXACT = 213;

		// Token: 0x040012AB RID: 4779
		internal const ushort BULKINSERT = 240;

		// Token: 0x040012AC RID: 4780
		internal const ushort OPENCURSOR = 32;

		// Token: 0x040012AD RID: 4781
		internal const ushort MERGE = 279;

		// Token: 0x040012AE RID: 4782
		internal const ushort MAXLEN_HOSTNAME = 128;

		// Token: 0x040012AF RID: 4783
		internal const ushort MAXLEN_USERNAME = 128;

		// Token: 0x040012B0 RID: 4784
		internal const ushort MAXLEN_PASSWORD = 128;

		// Token: 0x040012B1 RID: 4785
		internal const ushort MAXLEN_APPNAME = 128;

		// Token: 0x040012B2 RID: 4786
		internal const ushort MAXLEN_SERVERNAME = 128;

		// Token: 0x040012B3 RID: 4787
		internal const ushort MAXLEN_CLIENTINTERFACE = 128;

		// Token: 0x040012B4 RID: 4788
		internal const ushort MAXLEN_LANGUAGE = 128;

		// Token: 0x040012B5 RID: 4789
		internal const ushort MAXLEN_DATABASE = 128;

		// Token: 0x040012B6 RID: 4790
		internal const ushort MAXLEN_ATTACHDBFILE = 260;

		// Token: 0x040012B7 RID: 4791
		internal const ushort MAXLEN_NEWPASSWORD = 128;

		// Token: 0x040012B8 RID: 4792
		public static readonly ushort[] CODE_PAGE_FROM_SORT_ID = new ushort[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			437,
			437,
			437,
			437,
			437,
			0,
			0,
			0,
			0,
			0,
			850,
			850,
			850,
			850,
			850,
			0,
			0,
			0,
			0,
			850,
			1252,
			1252,
			1252,
			1252,
			1252,
			850,
			850,
			850,
			850,
			850,
			850,
			850,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1252,
			1252,
			1252,
			1252,
			1252,
			0,
			0,
			0,
			0,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			1250,
			0,
			0,
			0,
			0,
			0,
			1251,
			1251,
			1251,
			1251,
			1251,
			0,
			0,
			0,
			1253,
			1253,
			1253,
			0,
			0,
			0,
			0,
			0,
			1253,
			1253,
			1253,
			0,
			1253,
			0,
			0,
			0,
			1254,
			1254,
			1254,
			0,
			0,
			0,
			0,
			0,
			1255,
			1255,
			1255,
			0,
			0,
			0,
			0,
			0,
			1256,
			1256,
			1256,
			0,
			0,
			0,
			0,
			0,
			1257,
			1257,
			1257,
			1257,
			1257,
			1257,
			1257,
			1257,
			1257,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1252,
			1252,
			1252,
			1252,
			0,
			0,
			0,
			0,
			0,
			932,
			932,
			949,
			949,
			950,
			950,
			936,
			936,
			932,
			949,
			950,
			936,
			874,
			874,
			874,
			0,
			0,
			0,
			1252,
			1252,
			1252,
			1252,
			1252,
			1252,
			1252,
			1252,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0
		};

		// Token: 0x040012B9 RID: 4793
		internal static readonly long[] TICKS_FROM_SCALE = new long[]
		{
			10000000L,
			1000000L,
			100000L,
			10000L,
			1000L,
			100L,
			10L,
			1L
		};

		// Token: 0x040012BA RID: 4794
		internal const int WHIDBEY_DATE_LENGTH = 10;

		// Token: 0x040012BB RID: 4795
		internal static readonly int[] WHIDBEY_TIME_LENGTH = new int[]
		{
			8,
			10,
			11,
			12,
			13,
			14,
			15,
			16
		};

		// Token: 0x040012BC RID: 4796
		internal static readonly int[] WHIDBEY_DATETIME2_LENGTH = new int[]
		{
			19,
			21,
			22,
			23,
			24,
			25,
			26,
			27
		};

		// Token: 0x040012BD RID: 4797
		internal static readonly int[] WHIDBEY_DATETIMEOFFSET_LENGTH = new int[]
		{
			26,
			28,
			29,
			30,
			31,
			32,
			33,
			34
		};

		// Token: 0x0200023E RID: 574
		public enum EnvChangeType : byte
		{
			// Token: 0x040012BF RID: 4799
			ENVCHANGE_DATABASE = 1,
			// Token: 0x040012C0 RID: 4800
			ENVCHANGE_LANG,
			// Token: 0x040012C1 RID: 4801
			ENVCHANGE_CHARSET,
			// Token: 0x040012C2 RID: 4802
			ENVCHANGE_PACKETSIZE,
			// Token: 0x040012C3 RID: 4803
			ENVCHANGE_LOCALEID,
			// Token: 0x040012C4 RID: 4804
			ENVCHANGE_COMPFLAGS,
			// Token: 0x040012C5 RID: 4805
			ENVCHANGE_COLLATION,
			// Token: 0x040012C6 RID: 4806
			ENVCHANGE_BEGINTRAN,
			// Token: 0x040012C7 RID: 4807
			ENVCHANGE_COMMITTRAN,
			// Token: 0x040012C8 RID: 4808
			ENVCHANGE_ROLLBACKTRAN,
			// Token: 0x040012C9 RID: 4809
			ENVCHANGE_ENLISTDTC,
			// Token: 0x040012CA RID: 4810
			ENVCHANGE_DEFECTDTC,
			// Token: 0x040012CB RID: 4811
			ENVCHANGE_LOGSHIPNODE,
			// Token: 0x040012CC RID: 4812
			ENVCHANGE_PROMOTETRANSACTION = 15,
			// Token: 0x040012CD RID: 4813
			ENVCHANGE_TRANSACTIONMANAGERADDRESS,
			// Token: 0x040012CE RID: 4814
			ENVCHANGE_TRANSACTIONENDED,
			// Token: 0x040012CF RID: 4815
			ENVCHANGE_SPRESETCONNECTIONACK,
			// Token: 0x040012D0 RID: 4816
			ENVCHANGE_USERINSTANCE,
			// Token: 0x040012D1 RID: 4817
			ENVCHANGE_ROUTING
		}

		// Token: 0x0200023F RID: 575
		[Flags]
		public enum FeatureExtension : uint
		{
			// Token: 0x040012D3 RID: 4819
			None = 0U,
			// Token: 0x040012D4 RID: 4820
			SessionRecovery = 1U,
			// Token: 0x040012D5 RID: 4821
			FedAuth = 2U,
			// Token: 0x040012D6 RID: 4822
			GlobalTransactions = 8U
		}

		// Token: 0x02000240 RID: 576
		public enum FedAuthLibrary : byte
		{
			// Token: 0x040012D8 RID: 4824
			LiveId,
			// Token: 0x040012D9 RID: 4825
			SecurityToken,
			// Token: 0x040012DA RID: 4826
			ADAL,
			// Token: 0x040012DB RID: 4827
			Default = 127
		}

		// Token: 0x02000241 RID: 577
		internal enum TransactionManagerRequestType
		{
			// Token: 0x040012DD RID: 4829
			GetDTCAddress,
			// Token: 0x040012DE RID: 4830
			Propagate,
			// Token: 0x040012DF RID: 4831
			Begin = 5,
			// Token: 0x040012E0 RID: 4832
			Promote,
			// Token: 0x040012E1 RID: 4833
			Commit,
			// Token: 0x040012E2 RID: 4834
			Rollback,
			// Token: 0x040012E3 RID: 4835
			Save
		}

		// Token: 0x02000242 RID: 578
		internal enum TransactionManagerIsolationLevel
		{
			// Token: 0x040012E5 RID: 4837
			Unspecified,
			// Token: 0x040012E6 RID: 4838
			ReadUncommitted,
			// Token: 0x040012E7 RID: 4839
			ReadCommitted,
			// Token: 0x040012E8 RID: 4840
			RepeatableRead,
			// Token: 0x040012E9 RID: 4841
			Serializable,
			// Token: 0x040012EA RID: 4842
			Snapshot
		}

		// Token: 0x02000243 RID: 579
		internal enum GenericType
		{
			// Token: 0x040012EC RID: 4844
			MultiSet = 131
		}
	}
}

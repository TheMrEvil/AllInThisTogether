using System;
using System.Data.Common;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x020002AB RID: 683
	public static class ODBC32
	{
		// Token: 0x06001F5D RID: 8029 RVA: 0x00093872 File Offset: 0x00091A72
		internal static string RetcodeToString(ODBC32.RetCode retcode)
		{
			switch (retcode)
			{
			case ODBC32.RetCode.INVALID_HANDLE:
				return "INVALID_HANDLE";
			case ODBC32.RetCode.ERROR:
				break;
			case ODBC32.RetCode.SUCCESS:
				return "SUCCESS";
			case ODBC32.RetCode.SUCCESS_WITH_INFO:
				return "SUCCESS_WITH_INFO";
			default:
				if (retcode == ODBC32.RetCode.NO_DATA)
				{
					return "NO_DATA";
				}
				break;
			}
			return "ERROR";
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x000938B1 File Offset: 0x00091AB1
		internal static OdbcErrorCollection GetDiagErrors(string source, OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			OdbcErrorCollection odbcErrorCollection = new OdbcErrorCollection();
			ODBC32.GetDiagErrors(odbcErrorCollection, source, hrHandle, retcode);
			return odbcErrorCollection;
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x000938C4 File Offset: 0x00091AC4
		internal static void GetDiagErrors(OdbcErrorCollection errors, string source, OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			if (retcode != ODBC32.RetCode.SUCCESS)
			{
				short num = 0;
				short num2 = 0;
				StringBuilder stringBuilder = new StringBuilder(1024);
				bool flag = true;
				while (flag)
				{
					num += 1;
					string state;
					int nativeerror;
					retcode = hrHandle.GetDiagnosticRecord(num, out state, stringBuilder, out nativeerror, out num2);
					if (ODBC32.RetCode.SUCCESS_WITH_INFO == retcode && stringBuilder.Capacity - 1 < (int)num2)
					{
						stringBuilder.Capacity = (int)(num2 + 1);
						retcode = hrHandle.GetDiagnosticRecord(num, out state, stringBuilder, out nativeerror, out num2);
					}
					flag = (retcode == ODBC32.RetCode.SUCCESS || retcode == ODBC32.RetCode.SUCCESS_WITH_INFO);
					if (flag)
					{
						errors.Add(new OdbcError(source, stringBuilder.ToString(), state, nativeerror));
					}
				}
			}
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00093950 File Offset: 0x00091B50
		// Note: this type is marked as 'beforefieldinit'.
		static ODBC32()
		{
		}

		// Token: 0x040015B2 RID: 5554
		internal const short SQL_COMMIT = 0;

		// Token: 0x040015B3 RID: 5555
		internal const short SQL_ROLLBACK = 1;

		// Token: 0x040015B4 RID: 5556
		internal static readonly IntPtr SQL_AUTOCOMMIT_OFF = ADP.PtrZero;

		// Token: 0x040015B5 RID: 5557
		internal static readonly IntPtr SQL_AUTOCOMMIT_ON = new IntPtr(1);

		// Token: 0x040015B6 RID: 5558
		private const int SIGNED_OFFSET = -20;

		// Token: 0x040015B7 RID: 5559
		private const int UNSIGNED_OFFSET = -22;

		// Token: 0x040015B8 RID: 5560
		internal const short SQL_ALL_TYPES = 0;

		// Token: 0x040015B9 RID: 5561
		internal static readonly IntPtr SQL_HANDLE_NULL = ADP.PtrZero;

		// Token: 0x040015BA RID: 5562
		internal const int SQL_NULL_DATA = -1;

		// Token: 0x040015BB RID: 5563
		internal const int SQL_NO_TOTAL = -4;

		// Token: 0x040015BC RID: 5564
		internal const int SQL_DEFAULT_PARAM = -5;

		// Token: 0x040015BD RID: 5565
		internal const int COLUMN_NAME = 4;

		// Token: 0x040015BE RID: 5566
		internal const int COLUMN_TYPE = 5;

		// Token: 0x040015BF RID: 5567
		internal const int DATA_TYPE = 6;

		// Token: 0x040015C0 RID: 5568
		internal const int COLUMN_SIZE = 8;

		// Token: 0x040015C1 RID: 5569
		internal const int DECIMAL_DIGITS = 10;

		// Token: 0x040015C2 RID: 5570
		internal const int NUM_PREC_RADIX = 11;

		// Token: 0x040015C3 RID: 5571
		internal static readonly IntPtr SQL_OV_ODBC3 = new IntPtr(3);

		// Token: 0x040015C4 RID: 5572
		internal const int SQL_NTS = -3;

		// Token: 0x040015C5 RID: 5573
		internal static readonly IntPtr SQL_CP_OFF = new IntPtr(0);

		// Token: 0x040015C6 RID: 5574
		internal static readonly IntPtr SQL_CP_ONE_PER_DRIVER = new IntPtr(1);

		// Token: 0x040015C7 RID: 5575
		internal static readonly IntPtr SQL_CP_ONE_PER_HENV = new IntPtr(2);

		// Token: 0x040015C8 RID: 5576
		internal const int SQL_CD_TRUE = 1;

		// Token: 0x040015C9 RID: 5577
		internal const int SQL_CD_FALSE = 0;

		// Token: 0x040015CA RID: 5578
		internal const int SQL_DTC_DONE = 0;

		// Token: 0x040015CB RID: 5579
		internal const int SQL_IS_POINTER = -4;

		// Token: 0x040015CC RID: 5580
		internal const int SQL_IS_PTR = 1;

		// Token: 0x040015CD RID: 5581
		internal const int MAX_CONNECTION_STRING_LENGTH = 1024;

		// Token: 0x040015CE RID: 5582
		internal const short SQL_DIAG_SQLSTATE = 4;

		// Token: 0x040015CF RID: 5583
		internal const short SQL_RESULT_COL = 3;

		// Token: 0x020002AC RID: 684
		internal enum SQL_HANDLE : short
		{
			// Token: 0x040015D1 RID: 5585
			ENV = 1,
			// Token: 0x040015D2 RID: 5586
			DBC,
			// Token: 0x040015D3 RID: 5587
			STMT,
			// Token: 0x040015D4 RID: 5588
			DESC
		}

		// Token: 0x020002AD RID: 685
		public enum RETCODE
		{
			// Token: 0x040015D6 RID: 5590
			SUCCESS,
			// Token: 0x040015D7 RID: 5591
			SUCCESS_WITH_INFO,
			// Token: 0x040015D8 RID: 5592
			ERROR = -1,
			// Token: 0x040015D9 RID: 5593
			INVALID_HANDLE = -2,
			// Token: 0x040015DA RID: 5594
			NO_DATA = 100
		}

		// Token: 0x020002AE RID: 686
		internal enum RetCode : short
		{
			// Token: 0x040015DC RID: 5596
			SUCCESS,
			// Token: 0x040015DD RID: 5597
			SUCCESS_WITH_INFO,
			// Token: 0x040015DE RID: 5598
			ERROR = -1,
			// Token: 0x040015DF RID: 5599
			INVALID_HANDLE = -2,
			// Token: 0x040015E0 RID: 5600
			NO_DATA = 100
		}

		// Token: 0x020002AF RID: 687
		internal enum SQL_CONVERT : ushort
		{
			// Token: 0x040015E2 RID: 5602
			BIGINT = 53,
			// Token: 0x040015E3 RID: 5603
			BINARY,
			// Token: 0x040015E4 RID: 5604
			BIT,
			// Token: 0x040015E5 RID: 5605
			CHAR,
			// Token: 0x040015E6 RID: 5606
			DATE,
			// Token: 0x040015E7 RID: 5607
			DECIMAL,
			// Token: 0x040015E8 RID: 5608
			DOUBLE,
			// Token: 0x040015E9 RID: 5609
			FLOAT,
			// Token: 0x040015EA RID: 5610
			INTEGER,
			// Token: 0x040015EB RID: 5611
			LONGVARCHAR,
			// Token: 0x040015EC RID: 5612
			NUMERIC,
			// Token: 0x040015ED RID: 5613
			REAL,
			// Token: 0x040015EE RID: 5614
			SMALLINT,
			// Token: 0x040015EF RID: 5615
			TIME,
			// Token: 0x040015F0 RID: 5616
			TIMESTAMP,
			// Token: 0x040015F1 RID: 5617
			TINYINT,
			// Token: 0x040015F2 RID: 5618
			VARBINARY,
			// Token: 0x040015F3 RID: 5619
			VARCHAR,
			// Token: 0x040015F4 RID: 5620
			LONGVARBINARY
		}

		// Token: 0x020002B0 RID: 688
		[Flags]
		internal enum SQL_CVT
		{
			// Token: 0x040015F6 RID: 5622
			CHAR = 1,
			// Token: 0x040015F7 RID: 5623
			NUMERIC = 2,
			// Token: 0x040015F8 RID: 5624
			DECIMAL = 4,
			// Token: 0x040015F9 RID: 5625
			INTEGER = 8,
			// Token: 0x040015FA RID: 5626
			SMALLINT = 16,
			// Token: 0x040015FB RID: 5627
			FLOAT = 32,
			// Token: 0x040015FC RID: 5628
			REAL = 64,
			// Token: 0x040015FD RID: 5629
			DOUBLE = 128,
			// Token: 0x040015FE RID: 5630
			VARCHAR = 256,
			// Token: 0x040015FF RID: 5631
			LONGVARCHAR = 512,
			// Token: 0x04001600 RID: 5632
			BINARY = 1024,
			// Token: 0x04001601 RID: 5633
			VARBINARY = 2048,
			// Token: 0x04001602 RID: 5634
			BIT = 4096,
			// Token: 0x04001603 RID: 5635
			TINYINT = 8192,
			// Token: 0x04001604 RID: 5636
			BIGINT = 16384,
			// Token: 0x04001605 RID: 5637
			DATE = 32768,
			// Token: 0x04001606 RID: 5638
			TIME = 65536,
			// Token: 0x04001607 RID: 5639
			TIMESTAMP = 131072,
			// Token: 0x04001608 RID: 5640
			LONGVARBINARY = 262144,
			// Token: 0x04001609 RID: 5641
			INTERVAL_YEAR_MONTH = 524288,
			// Token: 0x0400160A RID: 5642
			INTERVAL_DAY_TIME = 1048576,
			// Token: 0x0400160B RID: 5643
			WCHAR = 2097152,
			// Token: 0x0400160C RID: 5644
			WLONGVARCHAR = 4194304,
			// Token: 0x0400160D RID: 5645
			WVARCHAR = 8388608,
			// Token: 0x0400160E RID: 5646
			GUID = 16777216
		}

		// Token: 0x020002B1 RID: 689
		internal enum STMT : short
		{
			// Token: 0x04001610 RID: 5648
			CLOSE,
			// Token: 0x04001611 RID: 5649
			DROP,
			// Token: 0x04001612 RID: 5650
			UNBIND,
			// Token: 0x04001613 RID: 5651
			RESET_PARAMS
		}

		// Token: 0x020002B2 RID: 690
		internal enum SQL_MAX
		{
			// Token: 0x04001615 RID: 5653
			NUMERIC_LEN = 16
		}

		// Token: 0x020002B3 RID: 691
		internal enum SQL_IS
		{
			// Token: 0x04001617 RID: 5655
			POINTER = -4,
			// Token: 0x04001618 RID: 5656
			INTEGER = -6,
			// Token: 0x04001619 RID: 5657
			UINTEGER,
			// Token: 0x0400161A RID: 5658
			SMALLINT = -8
		}

		// Token: 0x020002B4 RID: 692
		internal enum SQL_HC
		{
			// Token: 0x0400161C RID: 5660
			OFF,
			// Token: 0x0400161D RID: 5661
			ON
		}

		// Token: 0x020002B5 RID: 693
		internal enum SQL_NB
		{
			// Token: 0x0400161F RID: 5663
			OFF,
			// Token: 0x04001620 RID: 5664
			ON
		}

		// Token: 0x020002B6 RID: 694
		internal enum SQL_CA_SS
		{
			// Token: 0x04001622 RID: 5666
			BASE = 1200,
			// Token: 0x04001623 RID: 5667
			COLUMN_HIDDEN = 1211,
			// Token: 0x04001624 RID: 5668
			COLUMN_KEY,
			// Token: 0x04001625 RID: 5669
			VARIANT_TYPE = 1215,
			// Token: 0x04001626 RID: 5670
			VARIANT_SQL_TYPE,
			// Token: 0x04001627 RID: 5671
			VARIANT_SERVER_TYPE
		}

		// Token: 0x020002B7 RID: 695
		internal enum SQL_SOPT_SS
		{
			// Token: 0x04001629 RID: 5673
			BASE = 1225,
			// Token: 0x0400162A RID: 5674
			HIDDEN_COLUMNS = 1227,
			// Token: 0x0400162B RID: 5675
			NOBROWSETABLE
		}

		// Token: 0x020002B8 RID: 696
		internal enum SQL_TRANSACTION
		{
			// Token: 0x0400162D RID: 5677
			READ_UNCOMMITTED = 1,
			// Token: 0x0400162E RID: 5678
			READ_COMMITTED,
			// Token: 0x0400162F RID: 5679
			REPEATABLE_READ = 4,
			// Token: 0x04001630 RID: 5680
			SERIALIZABLE = 8,
			// Token: 0x04001631 RID: 5681
			SNAPSHOT = 32
		}

		// Token: 0x020002B9 RID: 697
		internal enum SQL_PARAM
		{
			// Token: 0x04001633 RID: 5683
			INPUT = 1,
			// Token: 0x04001634 RID: 5684
			INPUT_OUTPUT,
			// Token: 0x04001635 RID: 5685
			OUTPUT = 4,
			// Token: 0x04001636 RID: 5686
			RETURN_VALUE
		}

		// Token: 0x020002BA RID: 698
		internal enum SQL_API : ushort
		{
			// Token: 0x04001638 RID: 5688
			SQLCOLUMNS = 40,
			// Token: 0x04001639 RID: 5689
			SQLEXECDIRECT = 11,
			// Token: 0x0400163A RID: 5690
			SQLGETTYPEINFO = 47,
			// Token: 0x0400163B RID: 5691
			SQLPROCEDURECOLUMNS = 66,
			// Token: 0x0400163C RID: 5692
			SQLPROCEDURES,
			// Token: 0x0400163D RID: 5693
			SQLSTATISTICS = 53,
			// Token: 0x0400163E RID: 5694
			SQLTABLES
		}

		// Token: 0x020002BB RID: 699
		internal enum SQL_DESC : short
		{
			// Token: 0x04001640 RID: 5696
			COUNT = 1001,
			// Token: 0x04001641 RID: 5697
			TYPE,
			// Token: 0x04001642 RID: 5698
			LENGTH,
			// Token: 0x04001643 RID: 5699
			OCTET_LENGTH_PTR,
			// Token: 0x04001644 RID: 5700
			PRECISION,
			// Token: 0x04001645 RID: 5701
			SCALE,
			// Token: 0x04001646 RID: 5702
			DATETIME_INTERVAL_CODE,
			// Token: 0x04001647 RID: 5703
			NULLABLE,
			// Token: 0x04001648 RID: 5704
			INDICATOR_PTR,
			// Token: 0x04001649 RID: 5705
			DATA_PTR,
			// Token: 0x0400164A RID: 5706
			NAME,
			// Token: 0x0400164B RID: 5707
			UNNAMED,
			// Token: 0x0400164C RID: 5708
			OCTET_LENGTH,
			// Token: 0x0400164D RID: 5709
			ALLOC_TYPE = 1099,
			// Token: 0x0400164E RID: 5710
			CONCISE_TYPE = 2,
			// Token: 0x0400164F RID: 5711
			DISPLAY_SIZE = 6,
			// Token: 0x04001650 RID: 5712
			UNSIGNED = 8,
			// Token: 0x04001651 RID: 5713
			UPDATABLE = 10,
			// Token: 0x04001652 RID: 5714
			AUTO_UNIQUE_VALUE,
			// Token: 0x04001653 RID: 5715
			TYPE_NAME = 14,
			// Token: 0x04001654 RID: 5716
			TABLE_NAME,
			// Token: 0x04001655 RID: 5717
			SCHEMA_NAME,
			// Token: 0x04001656 RID: 5718
			CATALOG_NAME,
			// Token: 0x04001657 RID: 5719
			BASE_COLUMN_NAME = 22,
			// Token: 0x04001658 RID: 5720
			BASE_TABLE_NAME
		}

		// Token: 0x020002BC RID: 700
		internal enum SQL_COLUMN
		{
			// Token: 0x0400165A RID: 5722
			COUNT,
			// Token: 0x0400165B RID: 5723
			NAME,
			// Token: 0x0400165C RID: 5724
			TYPE,
			// Token: 0x0400165D RID: 5725
			LENGTH,
			// Token: 0x0400165E RID: 5726
			PRECISION,
			// Token: 0x0400165F RID: 5727
			SCALE,
			// Token: 0x04001660 RID: 5728
			DISPLAY_SIZE,
			// Token: 0x04001661 RID: 5729
			NULLABLE,
			// Token: 0x04001662 RID: 5730
			UNSIGNED,
			// Token: 0x04001663 RID: 5731
			MONEY,
			// Token: 0x04001664 RID: 5732
			UPDATABLE,
			// Token: 0x04001665 RID: 5733
			AUTO_INCREMENT,
			// Token: 0x04001666 RID: 5734
			CASE_SENSITIVE,
			// Token: 0x04001667 RID: 5735
			SEARCHABLE,
			// Token: 0x04001668 RID: 5736
			TYPE_NAME,
			// Token: 0x04001669 RID: 5737
			TABLE_NAME,
			// Token: 0x0400166A RID: 5738
			OWNER_NAME,
			// Token: 0x0400166B RID: 5739
			QUALIFIER_NAME,
			// Token: 0x0400166C RID: 5740
			LABEL
		}

		// Token: 0x020002BD RID: 701
		internal enum SQL_GROUP_BY
		{
			// Token: 0x0400166E RID: 5742
			NOT_SUPPORTED,
			// Token: 0x0400166F RID: 5743
			GROUP_BY_EQUALS_SELECT,
			// Token: 0x04001670 RID: 5744
			GROUP_BY_CONTAINS_SELECT,
			// Token: 0x04001671 RID: 5745
			NO_RELATION,
			// Token: 0x04001672 RID: 5746
			COLLATE
		}

		// Token: 0x020002BE RID: 702
		internal enum SQL_SQL92_RELATIONAL_JOIN_OPERATORS
		{
			// Token: 0x04001674 RID: 5748
			CORRESPONDING_CLAUSE = 1,
			// Token: 0x04001675 RID: 5749
			CROSS_JOIN,
			// Token: 0x04001676 RID: 5750
			EXCEPT_JOIN = 4,
			// Token: 0x04001677 RID: 5751
			FULL_OUTER_JOIN = 8,
			// Token: 0x04001678 RID: 5752
			INNER_JOIN = 16,
			// Token: 0x04001679 RID: 5753
			INTERSECT_JOIN = 32,
			// Token: 0x0400167A RID: 5754
			LEFT_OUTER_JOIN = 64,
			// Token: 0x0400167B RID: 5755
			NATURAL_JOIN = 128,
			// Token: 0x0400167C RID: 5756
			RIGHT_OUTER_JOIN = 256,
			// Token: 0x0400167D RID: 5757
			UNION_JOIN = 512
		}

		// Token: 0x020002BF RID: 703
		internal enum SQL_OJ_CAPABILITIES
		{
			// Token: 0x0400167F RID: 5759
			LEFT = 1,
			// Token: 0x04001680 RID: 5760
			RIGHT,
			// Token: 0x04001681 RID: 5761
			FULL = 4,
			// Token: 0x04001682 RID: 5762
			NESTED = 8,
			// Token: 0x04001683 RID: 5763
			NOT_ORDERED = 16,
			// Token: 0x04001684 RID: 5764
			INNER = 32,
			// Token: 0x04001685 RID: 5765
			ALL_COMPARISON_OPS = 64
		}

		// Token: 0x020002C0 RID: 704
		internal enum SQL_UPDATABLE
		{
			// Token: 0x04001687 RID: 5767
			READONLY,
			// Token: 0x04001688 RID: 5768
			WRITE,
			// Token: 0x04001689 RID: 5769
			READWRITE_UNKNOWN
		}

		// Token: 0x020002C1 RID: 705
		internal enum SQL_IDENTIFIER_CASE
		{
			// Token: 0x0400168B RID: 5771
			UPPER = 1,
			// Token: 0x0400168C RID: 5772
			LOWER,
			// Token: 0x0400168D RID: 5773
			SENSITIVE,
			// Token: 0x0400168E RID: 5774
			MIXED
		}

		// Token: 0x020002C2 RID: 706
		internal enum SQL_INDEX : short
		{
			// Token: 0x04001690 RID: 5776
			UNIQUE,
			// Token: 0x04001691 RID: 5777
			ALL
		}

		// Token: 0x020002C3 RID: 707
		internal enum SQL_STATISTICS_RESERVED : short
		{
			// Token: 0x04001693 RID: 5779
			QUICK,
			// Token: 0x04001694 RID: 5780
			ENSURE
		}

		// Token: 0x020002C4 RID: 708
		internal enum SQL_SPECIALCOLS : ushort
		{
			// Token: 0x04001696 RID: 5782
			BEST_ROWID = 1,
			// Token: 0x04001697 RID: 5783
			ROWVER
		}

		// Token: 0x020002C5 RID: 709
		internal enum SQL_SCOPE : ushort
		{
			// Token: 0x04001699 RID: 5785
			CURROW,
			// Token: 0x0400169A RID: 5786
			TRANSACTION,
			// Token: 0x0400169B RID: 5787
			SESSION
		}

		// Token: 0x020002C6 RID: 710
		internal enum SQL_NULLABILITY : ushort
		{
			// Token: 0x0400169D RID: 5789
			NO_NULLS,
			// Token: 0x0400169E RID: 5790
			NULLABLE,
			// Token: 0x0400169F RID: 5791
			UNKNOWN
		}

		// Token: 0x020002C7 RID: 711
		internal enum SQL_SEARCHABLE
		{
			// Token: 0x040016A1 RID: 5793
			UNSEARCHABLE,
			// Token: 0x040016A2 RID: 5794
			LIKE_ONLY,
			// Token: 0x040016A3 RID: 5795
			ALL_EXCEPT_LIKE,
			// Token: 0x040016A4 RID: 5796
			SEARCHABLE
		}

		// Token: 0x020002C8 RID: 712
		internal enum SQL_UNNAMED
		{
			// Token: 0x040016A6 RID: 5798
			NAMED,
			// Token: 0x040016A7 RID: 5799
			UNNAMED
		}

		// Token: 0x020002C9 RID: 713
		internal enum HANDLER
		{
			// Token: 0x040016A9 RID: 5801
			IGNORE,
			// Token: 0x040016AA RID: 5802
			THROW
		}

		// Token: 0x020002CA RID: 714
		internal enum SQL_STATISTICSTYPE
		{
			// Token: 0x040016AC RID: 5804
			TABLE_STAT,
			// Token: 0x040016AD RID: 5805
			INDEX_CLUSTERED,
			// Token: 0x040016AE RID: 5806
			INDEX_HASHED,
			// Token: 0x040016AF RID: 5807
			INDEX_OTHER
		}

		// Token: 0x020002CB RID: 715
		internal enum SQL_PROCEDURETYPE
		{
			// Token: 0x040016B1 RID: 5809
			UNKNOWN,
			// Token: 0x040016B2 RID: 5810
			PROCEDURE,
			// Token: 0x040016B3 RID: 5811
			FUNCTION
		}

		// Token: 0x020002CC RID: 716
		internal enum SQL_C : short
		{
			// Token: 0x040016B5 RID: 5813
			CHAR = 1,
			// Token: 0x040016B6 RID: 5814
			WCHAR = -8,
			// Token: 0x040016B7 RID: 5815
			SLONG = -16,
			// Token: 0x040016B8 RID: 5816
			SSHORT,
			// Token: 0x040016B9 RID: 5817
			REAL = 7,
			// Token: 0x040016BA RID: 5818
			DOUBLE,
			// Token: 0x040016BB RID: 5819
			BIT = -7,
			// Token: 0x040016BC RID: 5820
			UTINYINT = -28,
			// Token: 0x040016BD RID: 5821
			SBIGINT = -25,
			// Token: 0x040016BE RID: 5822
			UBIGINT = -27,
			// Token: 0x040016BF RID: 5823
			BINARY = -2,
			// Token: 0x040016C0 RID: 5824
			TIMESTAMP = 11,
			// Token: 0x040016C1 RID: 5825
			TYPE_DATE = 91,
			// Token: 0x040016C2 RID: 5826
			TYPE_TIME,
			// Token: 0x040016C3 RID: 5827
			TYPE_TIMESTAMP,
			// Token: 0x040016C4 RID: 5828
			NUMERIC = 2,
			// Token: 0x040016C5 RID: 5829
			GUID = -11,
			// Token: 0x040016C6 RID: 5830
			DEFAULT = 99,
			// Token: 0x040016C7 RID: 5831
			ARD_TYPE = -99
		}

		// Token: 0x020002CD RID: 717
		internal enum SQL_TYPE : short
		{
			// Token: 0x040016C9 RID: 5833
			CHAR = 1,
			// Token: 0x040016CA RID: 5834
			VARCHAR = 12,
			// Token: 0x040016CB RID: 5835
			LONGVARCHAR = -1,
			// Token: 0x040016CC RID: 5836
			WCHAR = -8,
			// Token: 0x040016CD RID: 5837
			WVARCHAR = -9,
			// Token: 0x040016CE RID: 5838
			WLONGVARCHAR = -10,
			// Token: 0x040016CF RID: 5839
			DECIMAL = 3,
			// Token: 0x040016D0 RID: 5840
			NUMERIC = 2,
			// Token: 0x040016D1 RID: 5841
			SMALLINT = 5,
			// Token: 0x040016D2 RID: 5842
			INTEGER = 4,
			// Token: 0x040016D3 RID: 5843
			REAL = 7,
			// Token: 0x040016D4 RID: 5844
			FLOAT = 6,
			// Token: 0x040016D5 RID: 5845
			DOUBLE = 8,
			// Token: 0x040016D6 RID: 5846
			BIT = -7,
			// Token: 0x040016D7 RID: 5847
			TINYINT,
			// Token: 0x040016D8 RID: 5848
			BIGINT,
			// Token: 0x040016D9 RID: 5849
			BINARY = -2,
			// Token: 0x040016DA RID: 5850
			VARBINARY = -3,
			// Token: 0x040016DB RID: 5851
			LONGVARBINARY = -4,
			// Token: 0x040016DC RID: 5852
			TYPE_DATE = 91,
			// Token: 0x040016DD RID: 5853
			TYPE_TIME,
			// Token: 0x040016DE RID: 5854
			TIMESTAMP = 11,
			// Token: 0x040016DF RID: 5855
			TYPE_TIMESTAMP = 93,
			// Token: 0x040016E0 RID: 5856
			GUID = -11,
			// Token: 0x040016E1 RID: 5857
			SS_VARIANT = -150,
			// Token: 0x040016E2 RID: 5858
			SS_UDT = -151,
			// Token: 0x040016E3 RID: 5859
			SS_XML = -152,
			// Token: 0x040016E4 RID: 5860
			SS_UTCDATETIME = -153,
			// Token: 0x040016E5 RID: 5861
			SS_TIME_EX = -154
		}

		// Token: 0x020002CE RID: 718
		internal enum SQL_ATTR
		{
			// Token: 0x040016E7 RID: 5863
			APP_ROW_DESC = 10010,
			// Token: 0x040016E8 RID: 5864
			APP_PARAM_DESC,
			// Token: 0x040016E9 RID: 5865
			IMP_ROW_DESC,
			// Token: 0x040016EA RID: 5866
			IMP_PARAM_DESC,
			// Token: 0x040016EB RID: 5867
			METADATA_ID,
			// Token: 0x040016EC RID: 5868
			ODBC_VERSION = 200,
			// Token: 0x040016ED RID: 5869
			CONNECTION_POOLING,
			// Token: 0x040016EE RID: 5870
			AUTOCOMMIT = 102,
			// Token: 0x040016EF RID: 5871
			TXN_ISOLATION = 108,
			// Token: 0x040016F0 RID: 5872
			CURRENT_CATALOG,
			// Token: 0x040016F1 RID: 5873
			LOGIN_TIMEOUT = 103,
			// Token: 0x040016F2 RID: 5874
			QUERY_TIMEOUT = 0,
			// Token: 0x040016F3 RID: 5875
			CONNECTION_DEAD = 1209,
			// Token: 0x040016F4 RID: 5876
			SQL_COPT_SS_BASE = 1200,
			// Token: 0x040016F5 RID: 5877
			SQL_COPT_SS_ENLIST_IN_DTC = 1207,
			// Token: 0x040016F6 RID: 5878
			SQL_COPT_SS_TXN_ISOLATION = 1227
		}

		// Token: 0x020002CF RID: 719
		internal enum SQL_INFO : ushort
		{
			// Token: 0x040016F8 RID: 5880
			DATA_SOURCE_NAME = 2,
			// Token: 0x040016F9 RID: 5881
			SERVER_NAME = 13,
			// Token: 0x040016FA RID: 5882
			DRIVER_NAME = 6,
			// Token: 0x040016FB RID: 5883
			DRIVER_VER,
			// Token: 0x040016FC RID: 5884
			ODBC_VER = 10,
			// Token: 0x040016FD RID: 5885
			SEARCH_PATTERN_ESCAPE = 14,
			// Token: 0x040016FE RID: 5886
			DBMS_VER = 18,
			// Token: 0x040016FF RID: 5887
			DBMS_NAME = 17,
			// Token: 0x04001700 RID: 5888
			IDENTIFIER_CASE = 28,
			// Token: 0x04001701 RID: 5889
			IDENTIFIER_QUOTE_CHAR,
			// Token: 0x04001702 RID: 5890
			CATALOG_NAME_SEPARATOR = 41,
			// Token: 0x04001703 RID: 5891
			DRIVER_ODBC_VER = 77,
			// Token: 0x04001704 RID: 5892
			GROUP_BY = 88,
			// Token: 0x04001705 RID: 5893
			KEYWORDS,
			// Token: 0x04001706 RID: 5894
			ORDER_BY_COLUMNS_IN_SELECT,
			// Token: 0x04001707 RID: 5895
			QUOTED_IDENTIFIER_CASE = 93,
			// Token: 0x04001708 RID: 5896
			SQL_OJ_CAPABILITIES_30 = 115,
			// Token: 0x04001709 RID: 5897
			SQL_OJ_CAPABILITIES_20 = 65003,
			// Token: 0x0400170A RID: 5898
			SQL_SQL92_RELATIONAL_JOIN_OPERATORS = 161
		}

		// Token: 0x020002D0 RID: 720
		internal enum SQL_DRIVER
		{
			// Token: 0x0400170C RID: 5900
			NOPROMPT,
			// Token: 0x0400170D RID: 5901
			COMPLETE,
			// Token: 0x0400170E RID: 5902
			PROMPT,
			// Token: 0x0400170F RID: 5903
			COMPLETE_REQUIRED
		}

		// Token: 0x020002D1 RID: 721
		internal enum SQL_PRIMARYKEYS : short
		{
			// Token: 0x04001711 RID: 5905
			COLUMNNAME = 4
		}

		// Token: 0x020002D2 RID: 722
		internal enum SQL_STATISTICS : short
		{
			// Token: 0x04001713 RID: 5907
			INDEXNAME = 6,
			// Token: 0x04001714 RID: 5908
			ORDINAL_POSITION = 8,
			// Token: 0x04001715 RID: 5909
			COLUMN_NAME
		}

		// Token: 0x020002D3 RID: 723
		internal enum SQL_SPECIALCOLUMNSET : short
		{
			// Token: 0x04001717 RID: 5911
			COLUMN_NAME = 2
		}
	}
}

using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000408 RID: 1032
	internal static class DefaultPolicies
	{
		// Token: 0x06002A41 RID: 10817 RVA: 0x00099044 File Offset: 0x00097244
		public static PermissionSet GetSpecialPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 2314740779U)
			{
				if (num != 734303062U)
				{
					if (num != 753551658U)
					{
						if (num == 2314740779U)
						{
							if (name == "LocalIntranet")
							{
								return DefaultPolicies.LocalIntranet;
							}
						}
					}
					else if (name == "Nothing")
					{
						return DefaultPolicies.Nothing;
					}
				}
				else if (name == "FullTrust")
				{
					return DefaultPolicies.FullTrust;
				}
			}
			else if (num <= 3132872517U)
			{
				if (num != 2939433820U)
				{
					if (num == 3132872517U)
					{
						if (name == "SkipVerification")
						{
							return DefaultPolicies.SkipVerification;
						}
					}
				}
				else if (name == "Internet")
				{
					return DefaultPolicies.Internet;
				}
			}
			else if (num != 3650199797U)
			{
				if (num == 4030759744U)
				{
					if (name == "Everything")
					{
						return DefaultPolicies.Everything;
					}
				}
			}
			else if (name == "Execution")
			{
				return DefaultPolicies.Execution;
			}
			return null;
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x00099157 File Offset: 0x00097357
		public static PermissionSet FullTrust
		{
			get
			{
				if (DefaultPolicies._fullTrust == null)
				{
					DefaultPolicies._fullTrust = DefaultPolicies.BuildFullTrust();
				}
				return DefaultPolicies._fullTrust;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x0009916F File Offset: 0x0009736F
		public static PermissionSet LocalIntranet
		{
			get
			{
				if (DefaultPolicies._localIntranet == null)
				{
					DefaultPolicies._localIntranet = DefaultPolicies.BuildLocalIntranet();
				}
				return DefaultPolicies._localIntranet;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x00099187 File Offset: 0x00097387
		public static PermissionSet Internet
		{
			get
			{
				if (DefaultPolicies._internet == null)
				{
					DefaultPolicies._internet = DefaultPolicies.BuildInternet();
				}
				return DefaultPolicies._internet;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x0009919F File Offset: 0x0009739F
		public static PermissionSet SkipVerification
		{
			get
			{
				if (DefaultPolicies._skipVerification == null)
				{
					DefaultPolicies._skipVerification = DefaultPolicies.BuildSkipVerification();
				}
				return DefaultPolicies._skipVerification;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x000991B7 File Offset: 0x000973B7
		public static PermissionSet Execution
		{
			get
			{
				if (DefaultPolicies._execution == null)
				{
					DefaultPolicies._execution = DefaultPolicies.BuildExecution();
				}
				return DefaultPolicies._execution;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x000991CF File Offset: 0x000973CF
		public static PermissionSet Nothing
		{
			get
			{
				if (DefaultPolicies._nothing == null)
				{
					DefaultPolicies._nothing = DefaultPolicies.BuildNothing();
				}
				return DefaultPolicies._nothing;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x000991E7 File Offset: 0x000973E7
		public static PermissionSet Everything
		{
			get
			{
				if (DefaultPolicies._everything == null)
				{
					DefaultPolicies._everything = DefaultPolicies.BuildEverything();
				}
				return DefaultPolicies._everything;
			}
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x00099200 File Offset: 0x00097400
		public static StrongNameMembershipCondition FullTrustMembership(string name, DefaultPolicies.Key key)
		{
			StrongNamePublicKeyBlob blob = null;
			if (key != DefaultPolicies.Key.Ecma)
			{
				if (key == DefaultPolicies.Key.MsFinal)
				{
					if (DefaultPolicies._msFinal == null)
					{
						DefaultPolicies._msFinal = new StrongNamePublicKeyBlob(DefaultPolicies._msFinalKey);
					}
					blob = DefaultPolicies._msFinal;
				}
			}
			else
			{
				if (DefaultPolicies._ecma == null)
				{
					DefaultPolicies._ecma = new StrongNamePublicKeyBlob(DefaultPolicies._ecmaKey);
				}
				blob = DefaultPolicies._ecma;
			}
			if (DefaultPolicies._fxVersion == null)
			{
				DefaultPolicies._fxVersion = new Version("4.0.0.0");
			}
			return new StrongNameMembershipCondition(blob, name, DefaultPolicies._fxVersion);
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x0009927A File Offset: 0x0009747A
		private static NamedPermissionSet BuildFullTrust()
		{
			return new NamedPermissionSet("FullTrust", PermissionState.Unrestricted);
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x00099288 File Offset: 0x00097488
		private static NamedPermissionSet BuildLocalIntranet()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("LocalIntranet", PermissionState.None);
			namedPermissionSet.AddPermission(new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME;USER"));
			namedPermissionSet.AddPermission(new FileDialogPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.None)
			{
				UsageAllowed = IsolatedStorageContainment.AssemblyIsolationByUser,
				UserQuota = long.MaxValue
			});
			namedPermissionSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.ReflectionEmit));
			SecurityPermissionFlag flag = SecurityPermissionFlag.Assertion | SecurityPermissionFlag.Execution;
			namedPermissionSet.AddPermission(new SecurityPermission(flag));
			namedPermissionSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.DnsPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create(DefaultPolicies.PrintingPermission("SafePrinting")));
			return namedPermissionSet;
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x00099338 File Offset: 0x00097538
		private static NamedPermissionSet BuildInternet()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Internet", PermissionState.None);
			namedPermissionSet.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.Open));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.None)
			{
				UsageAllowed = IsolatedStorageContainment.DomainIsolationByUser,
				UserQuota = 512000L
			});
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			namedPermissionSet.AddPermission(new UIPermission(UIPermissionWindow.SafeTopLevelWindows, UIPermissionClipboard.OwnClipboard));
			namedPermissionSet.AddPermission(PermissionBuilder.Create(DefaultPolicies.PrintingPermission("SafePrinting")));
			return namedPermissionSet;
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000993B1 File Offset: 0x000975B1
		private static NamedPermissionSet BuildSkipVerification()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("SkipVerification", PermissionState.None);
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.SkipVerification));
			return namedPermissionSet;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000993CB File Offset: 0x000975CB
		private static NamedPermissionSet BuildExecution()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Execution", PermissionState.None);
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			return namedPermissionSet;
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000993E5 File Offset: 0x000975E5
		private static NamedPermissionSet BuildNothing()
		{
			return new NamedPermissionSet("Nothing", PermissionState.None);
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000993F4 File Offset: 0x000975F4
		private static NamedPermissionSet BuildEverything()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Everything", PermissionState.None);
			namedPermissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new FileDialogPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new ReflectionPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new RegistryPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new KeyContainerPermission(PermissionState.Unrestricted));
			SecurityPermissionFlag securityPermissionFlag = SecurityPermissionFlag.AllFlags;
			securityPermissionFlag &= ~SecurityPermissionFlag.SkipVerification;
			namedPermissionSet.AddPermission(new SecurityPermission(securityPermissionFlag));
			namedPermissionSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.DnsPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Drawing.Printing.PrintingPermission, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Diagnostics.EventLogPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.SocketPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.WebPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Diagnostics.PerformanceCounterPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.DirectoryServices.DirectoryServicesPermission, System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Messaging.MessageQueuePermission, System.Messaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.ServiceProcess.ServiceControllerPermission, System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Data.OleDb.OleDbPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Data.SqlClient.SqlClientPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			return namedPermissionSet;
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x00099552 File Offset: 0x00097752
		private static SecurityElement PrintingPermission(string level)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", "System.Drawing.Printing.PrintingPermission, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Level", level);
			return securityElement;
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x0009958A File Offset: 0x0009778A
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultPolicies()
		{
			byte[] array = new byte[16];
			array[8] = 4;
			DefaultPolicies._ecmaKey = array;
			DefaultPolicies._msFinalKey = new byte[]
			{
				0,
				36,
				0,
				0,
				4,
				128,
				0,
				0,
				148,
				0,
				0,
				0,
				6,
				2,
				0,
				0,
				0,
				36,
				0,
				0,
				82,
				83,
				65,
				49,
				0,
				4,
				0,
				0,
				1,
				0,
				1,
				0,
				7,
				209,
				250,
				87,
				196,
				174,
				217,
				240,
				163,
				46,
				132,
				170,
				15,
				174,
				253,
				13,
				233,
				232,
				253,
				106,
				236,
				143,
				135,
				251,
				3,
				118,
				108,
				131,
				76,
				153,
				146,
				30,
				178,
				59,
				231,
				154,
				217,
				213,
				220,
				193,
				221,
				154,
				210,
				54,
				19,
				33,
				2,
				144,
				11,
				114,
				60,
				249,
				128,
				149,
				127,
				196,
				225,
				119,
				16,
				143,
				198,
				7,
				119,
				79,
				41,
				232,
				50,
				14,
				146,
				234,
				5,
				236,
				228,
				232,
				33,
				192,
				165,
				239,
				232,
				241,
				100,
				92,
				76,
				12,
				147,
				193,
				171,
				153,
				40,
				93,
				98,
				44,
				170,
				101,
				44,
				29,
				250,
				214,
				61,
				116,
				93,
				111,
				45,
				229,
				241,
				126,
				94,
				175,
				15,
				196,
				150,
				61,
				38,
				28,
				138,
				18,
				67,
				101,
				24,
				32,
				109,
				192,
				147,
				52,
				77,
				90,
				210,
				147
			};
		}

		// Token: 0x04001F6F RID: 8047
		private const string DnsPermissionClass = "System.Net.DnsPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001F70 RID: 8048
		private const string EventLogPermissionClass = "System.Diagnostics.EventLogPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001F71 RID: 8049
		private const string PrintingPermissionClass = "System.Drawing.Printing.PrintingPermission, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001F72 RID: 8050
		private const string SocketPermissionClass = "System.Net.SocketPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001F73 RID: 8051
		private const string WebPermissionClass = "System.Net.WebPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001F74 RID: 8052
		private const string PerformanceCounterPermissionClass = "System.Diagnostics.PerformanceCounterPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001F75 RID: 8053
		private const string DirectoryServicesPermissionClass = "System.DirectoryServices.DirectoryServicesPermission, System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001F76 RID: 8054
		private const string MessageQueuePermissionClass = "System.Messaging.MessageQueuePermission, System.Messaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001F77 RID: 8055
		private const string ServiceControllerPermissionClass = "System.ServiceProcess.ServiceControllerPermission, System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001F78 RID: 8056
		private const string OleDbPermissionClass = "System.Data.OleDb.OleDbPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001F79 RID: 8057
		private const string SqlClientPermissionClass = "System.Data.SqlClient.SqlClientPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001F7A RID: 8058
		private static Version _fxVersion;

		// Token: 0x04001F7B RID: 8059
		private static byte[] _ecmaKey;

		// Token: 0x04001F7C RID: 8060
		private static StrongNamePublicKeyBlob _ecma;

		// Token: 0x04001F7D RID: 8061
		private static byte[] _msFinalKey;

		// Token: 0x04001F7E RID: 8062
		private static StrongNamePublicKeyBlob _msFinal;

		// Token: 0x04001F7F RID: 8063
		private static NamedPermissionSet _fullTrust;

		// Token: 0x04001F80 RID: 8064
		private static NamedPermissionSet _localIntranet;

		// Token: 0x04001F81 RID: 8065
		private static NamedPermissionSet _internet;

		// Token: 0x04001F82 RID: 8066
		private static NamedPermissionSet _skipVerification;

		// Token: 0x04001F83 RID: 8067
		private static NamedPermissionSet _execution;

		// Token: 0x04001F84 RID: 8068
		private static NamedPermissionSet _nothing;

		// Token: 0x04001F85 RID: 8069
		private static NamedPermissionSet _everything;

		// Token: 0x02000409 RID: 1033
		public static class ReservedNames
		{
			// Token: 0x06002A53 RID: 10835 RVA: 0x000995B8 File Offset: 0x000977B8
			public static bool IsReserved(string name)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 2314740779U)
				{
					if (num != 734303062U)
					{
						if (num != 753551658U)
						{
							if (num != 2314740779U)
							{
								return false;
							}
							if (!(name == "LocalIntranet"))
							{
								return false;
							}
						}
						else if (!(name == "Nothing"))
						{
							return false;
						}
					}
					else if (!(name == "FullTrust"))
					{
						return false;
					}
				}
				else if (num <= 3132872517U)
				{
					if (num != 2939433820U)
					{
						if (num != 3132872517U)
						{
							return false;
						}
						if (!(name == "SkipVerification"))
						{
							return false;
						}
					}
					else if (!(name == "Internet"))
					{
						return false;
					}
				}
				else if (num != 3650199797U)
				{
					if (num != 4030759744U)
					{
						return false;
					}
					if (!(name == "Everything"))
					{
						return false;
					}
				}
				else if (!(name == "Execution"))
				{
					return false;
				}
				return true;
			}

			// Token: 0x04001F86 RID: 8070
			public const string FullTrust = "FullTrust";

			// Token: 0x04001F87 RID: 8071
			public const string LocalIntranet = "LocalIntranet";

			// Token: 0x04001F88 RID: 8072
			public const string Internet = "Internet";

			// Token: 0x04001F89 RID: 8073
			public const string SkipVerification = "SkipVerification";

			// Token: 0x04001F8A RID: 8074
			public const string Execution = "Execution";

			// Token: 0x04001F8B RID: 8075
			public const string Nothing = "Nothing";

			// Token: 0x04001F8C RID: 8076
			public const string Everything = "Everything";
		}

		// Token: 0x0200040A RID: 1034
		public enum Key
		{
			// Token: 0x04001F8E RID: 8078
			Ecma,
			// Token: 0x04001F8F RID: 8079
			MsFinal
		}
	}
}

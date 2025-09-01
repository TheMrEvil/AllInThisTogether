using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x02000070 RID: 112
	public sealed class Universe : IDisposable
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x00011FD6 File Offset: 0x000101D6
		public Universe() : this(UniverseOptions.None)
		{
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00011FE0 File Offset: 0x000101E0
		public Universe(UniverseOptions options)
		{
			this.options = options;
			this.enableFunctionPointers = ((options & UniverseOptions.EnableFunctionPointers) > UniverseOptions.None);
			this.useNativeFusion = ((options & UniverseOptions.DisableFusion) == UniverseOptions.None && Universe.GetUseNativeFusion());
			this.returnPseudoCustomAttributes = ((options & UniverseOptions.DisablePseudoCustomAttributeRetrieval) == UniverseOptions.None);
			this.automaticallyProvideDefaultConstructor = ((options & UniverseOptions.DontProvideAutomaticDefaultConstructor) == UniverseOptions.None);
			this.resolveMissingMembers = ((options & UniverseOptions.ResolveMissingMembers) > UniverseOptions.None);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00012080 File Offset: 0x00010280
		private static bool GetUseNativeFusion()
		{
			bool result;
			try
			{
				result = (Environment.OSVersion.Platform == PlatformID.Win32NT && !Universe.MonoRuntime && Environment.GetEnvironmentVariable("IKVM_DISABLE_FUSION") == null);
			}
			catch (SecurityException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x000120CC File Offset: 0x000102CC
		internal Assembly Mscorlib
		{
			get
			{
				return this.Load("mscorlib");
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000120D9 File Offset: 0x000102D9
		private Type ImportMscorlibType(string ns, string name)
		{
			if (this.Mscorlib.__IsMissing)
			{
				return this.Mscorlib.ResolveType(null, new TypeName(ns, name));
			}
			return this.Mscorlib.FindType(new TypeName(ns, name));
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001210E File Offset: 0x0001030E
		private Type ResolvePrimitive(string name)
		{
			return this.Mscorlib.FindType(new TypeName("System", name)) ?? this.GetMissingType(null, this.Mscorlib.ManifestModule, null, new TypeName("System", name));
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00012148 File Offset: 0x00010348
		internal Type System_Object
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Object) == null)
				{
					result = (this.typeof_System_Object = this.ResolvePrimitive("Object"));
				}
				return result;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00012174 File Offset: 0x00010374
		internal Type System_ValueType
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_ValueType) == null)
				{
					result = (this.typeof_System_ValueType = this.ResolvePrimitive("ValueType"));
				}
				return result;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x000121A0 File Offset: 0x000103A0
		internal Type System_Enum
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Enum) == null)
				{
					result = (this.typeof_System_Enum = this.ResolvePrimitive("Enum"));
				}
				return result;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x000121CC File Offset: 0x000103CC
		internal Type System_Void
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Void) == null)
				{
					result = (this.typeof_System_Void = this.ResolvePrimitive("Void"));
				}
				return result;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x000121F8 File Offset: 0x000103F8
		internal Type System_Boolean
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Boolean) == null)
				{
					result = (this.typeof_System_Boolean = this.ResolvePrimitive("Boolean"));
				}
				return result;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00012224 File Offset: 0x00010424
		internal Type System_Char
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Char) == null)
				{
					result = (this.typeof_System_Char = this.ResolvePrimitive("Char"));
				}
				return result;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00012250 File Offset: 0x00010450
		internal Type System_SByte
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_SByte) == null)
				{
					result = (this.typeof_System_SByte = this.ResolvePrimitive("SByte"));
				}
				return result;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001227C File Offset: 0x0001047C
		internal Type System_Byte
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Byte) == null)
				{
					result = (this.typeof_System_Byte = this.ResolvePrimitive("Byte"));
				}
				return result;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x000122A8 File Offset: 0x000104A8
		internal Type System_Int16
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Int16) == null)
				{
					result = (this.typeof_System_Int16 = this.ResolvePrimitive("Int16"));
				}
				return result;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x000122D4 File Offset: 0x000104D4
		internal Type System_UInt16
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_UInt16) == null)
				{
					result = (this.typeof_System_UInt16 = this.ResolvePrimitive("UInt16"));
				}
				return result;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00012300 File Offset: 0x00010500
		internal Type System_Int32
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Int32) == null)
				{
					result = (this.typeof_System_Int32 = this.ResolvePrimitive("Int32"));
				}
				return result;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001232C File Offset: 0x0001052C
		internal Type System_UInt32
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_UInt32) == null)
				{
					result = (this.typeof_System_UInt32 = this.ResolvePrimitive("UInt32"));
				}
				return result;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00012358 File Offset: 0x00010558
		internal Type System_Int64
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Int64) == null)
				{
					result = (this.typeof_System_Int64 = this.ResolvePrimitive("Int64"));
				}
				return result;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00012384 File Offset: 0x00010584
		internal Type System_UInt64
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_UInt64) == null)
				{
					result = (this.typeof_System_UInt64 = this.ResolvePrimitive("UInt64"));
				}
				return result;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x000123B0 File Offset: 0x000105B0
		internal Type System_Single
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Single) == null)
				{
					result = (this.typeof_System_Single = this.ResolvePrimitive("Single"));
				}
				return result;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x000123DC File Offset: 0x000105DC
		internal Type System_Double
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Double) == null)
				{
					result = (this.typeof_System_Double = this.ResolvePrimitive("Double"));
				}
				return result;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00012408 File Offset: 0x00010608
		internal Type System_String
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_String) == null)
				{
					result = (this.typeof_System_String = this.ResolvePrimitive("String"));
				}
				return result;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00012434 File Offset: 0x00010634
		internal Type System_IntPtr
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_IntPtr) == null)
				{
					result = (this.typeof_System_IntPtr = this.ResolvePrimitive("IntPtr"));
				}
				return result;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00012460 File Offset: 0x00010660
		internal Type System_UIntPtr
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_UIntPtr) == null)
				{
					result = (this.typeof_System_UIntPtr = this.ResolvePrimitive("UIntPtr"));
				}
				return result;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001248C File Offset: 0x0001068C
		internal Type System_TypedReference
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_TypedReference) == null)
				{
					result = (this.typeof_System_TypedReference = this.ResolvePrimitive("TypedReference"));
				}
				return result;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x000124B8 File Offset: 0x000106B8
		internal Type System_Type
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Type) == null)
				{
					result = (this.typeof_System_Type = this.ResolvePrimitive("Type"));
				}
				return result;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x000124E4 File Offset: 0x000106E4
		internal Type System_Array
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Array) == null)
				{
					result = (this.typeof_System_Array = this.ResolvePrimitive("Array"));
				}
				return result;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00012510 File Offset: 0x00010710
		internal Type System_DateTime
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_DateTime) == null)
				{
					result = (this.typeof_System_DateTime = this.ImportMscorlibType("System", "DateTime"));
				}
				return result;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x00012540 File Offset: 0x00010740
		internal Type System_DBNull
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_DBNull) == null)
				{
					result = (this.typeof_System_DBNull = this.ImportMscorlibType("System", "DBNull"));
				}
				return result;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00012570 File Offset: 0x00010770
		internal Type System_Decimal
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Decimal) == null)
				{
					result = (this.typeof_System_Decimal = this.ImportMscorlibType("System", "Decimal"));
				}
				return result;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000125A0 File Offset: 0x000107A0
		internal Type System_AttributeUsageAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_AttributeUsageAttribute) == null)
				{
					result = (this.typeof_System_AttributeUsageAttribute = this.ImportMscorlibType("System", "AttributeUsageAttribute"));
				}
				return result;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x000125D0 File Offset: 0x000107D0
		internal Type System_Runtime_InteropServices_DllImportAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Runtime_InteropServices_DllImportAttribute) == null)
				{
					result = (this.typeof_System_Runtime_InteropServices_DllImportAttribute = this.ImportMscorlibType("System.Runtime.InteropServices", "DllImportAttribute"));
				}
				return result;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00012600 File Offset: 0x00010800
		internal Type System_Runtime_InteropServices_FieldOffsetAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Runtime_InteropServices_FieldOffsetAttribute) == null)
				{
					result = (this.typeof_System_Runtime_InteropServices_FieldOffsetAttribute = this.ImportMscorlibType("System.Runtime.InteropServices", "FieldOffsetAttribute"));
				}
				return result;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00012630 File Offset: 0x00010830
		internal Type System_Runtime_InteropServices_MarshalAsAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Runtime_InteropServices_MarshalAsAttribute) == null)
				{
					result = (this.typeof_System_Runtime_InteropServices_MarshalAsAttribute = this.ImportMscorlibType("System.Runtime.InteropServices", "MarshalAsAttribute"));
				}
				return result;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00012660 File Offset: 0x00010860
		internal Type System_Runtime_InteropServices_UnmanagedType
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Runtime_InteropServices_UnmanagedType) == null)
				{
					result = (this.typeof_System_Runtime_InteropServices_UnmanagedType = this.ImportMscorlibType("System.Runtime.InteropServices", "UnmanagedType"));
				}
				return result;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00012690 File Offset: 0x00010890
		internal Type System_Runtime_InteropServices_VarEnum
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Runtime_InteropServices_VarEnum) == null)
				{
					result = (this.typeof_System_Runtime_InteropServices_VarEnum = this.ImportMscorlibType("System.Runtime.InteropServices", "VarEnum"));
				}
				return result;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x000126C0 File Offset: 0x000108C0
		internal Type System_Runtime_InteropServices_PreserveSigAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Runtime_InteropServices_PreserveSigAttribute) == null)
				{
					result = (this.typeof_System_Runtime_InteropServices_PreserveSigAttribute = this.ImportMscorlibType("System.Runtime.InteropServices", "PreserveSigAttribute"));
				}
				return result;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x000126F0 File Offset: 0x000108F0
		internal Type System_Runtime_InteropServices_CallingConvention
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Runtime_InteropServices_CallingConvention) == null)
				{
					result = (this.typeof_System_Runtime_InteropServices_CallingConvention = this.ImportMscorlibType("System.Runtime.InteropServices", "CallingConvention"));
				}
				return result;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00012720 File Offset: 0x00010920
		internal Type System_Runtime_InteropServices_CharSet
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Runtime_InteropServices_CharSet) == null)
				{
					result = (this.typeof_System_Runtime_InteropServices_CharSet = this.ImportMscorlibType("System.Runtime.InteropServices", "CharSet"));
				}
				return result;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00012750 File Offset: 0x00010950
		internal Type System_Runtime_CompilerServices_DecimalConstantAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Runtime_CompilerServices_DecimalConstantAttribute) == null)
				{
					result = (this.typeof_System_Runtime_CompilerServices_DecimalConstantAttribute = this.ImportMscorlibType("System.Runtime.CompilerServices", "DecimalConstantAttribute"));
				}
				return result;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00012780 File Offset: 0x00010980
		internal Type System_Reflection_AssemblyCopyrightAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Reflection_AssemblyCopyrightAttribute) == null)
				{
					result = (this.typeof_System_Reflection_AssemblyCopyrightAttribute = this.ImportMscorlibType("System.Reflection", "AssemblyCopyrightAttribute"));
				}
				return result;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x000127B0 File Offset: 0x000109B0
		internal Type System_Reflection_AssemblyTrademarkAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Reflection_AssemblyTrademarkAttribute) == null)
				{
					result = (this.typeof_System_Reflection_AssemblyTrademarkAttribute = this.ImportMscorlibType("System.Reflection", "AssemblyTrademarkAttribute"));
				}
				return result;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x000127E0 File Offset: 0x000109E0
		internal Type System_Reflection_AssemblyProductAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Reflection_AssemblyProductAttribute) == null)
				{
					result = (this.typeof_System_Reflection_AssemblyProductAttribute = this.ImportMscorlibType("System.Reflection", "AssemblyProductAttribute"));
				}
				return result;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00012810 File Offset: 0x00010A10
		internal Type System_Reflection_AssemblyCompanyAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Reflection_AssemblyCompanyAttribute) == null)
				{
					result = (this.typeof_System_Reflection_AssemblyCompanyAttribute = this.ImportMscorlibType("System.Reflection", "AssemblyCompanyAttribute"));
				}
				return result;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00012840 File Offset: 0x00010A40
		internal Type System_Reflection_AssemblyDescriptionAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Reflection_AssemblyDescriptionAttribute) == null)
				{
					result = (this.typeof_System_Reflection_AssemblyDescriptionAttribute = this.ImportMscorlibType("System.Reflection", "AssemblyDescriptionAttribute"));
				}
				return result;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00012870 File Offset: 0x00010A70
		internal Type System_Reflection_AssemblyTitleAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Reflection_AssemblyTitleAttribute) == null)
				{
					result = (this.typeof_System_Reflection_AssemblyTitleAttribute = this.ImportMscorlibType("System.Reflection", "AssemblyTitleAttribute"));
				}
				return result;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x000128A0 File Offset: 0x00010AA0
		internal Type System_Reflection_AssemblyInformationalVersionAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Reflection_AssemblyInformationalVersionAttribute) == null)
				{
					result = (this.typeof_System_Reflection_AssemblyInformationalVersionAttribute = this.ImportMscorlibType("System.Reflection", "AssemblyInformationalVersionAttribute"));
				}
				return result;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x000128D0 File Offset: 0x00010AD0
		internal Type System_Reflection_AssemblyFileVersionAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Reflection_AssemblyFileVersionAttribute) == null)
				{
					result = (this.typeof_System_Reflection_AssemblyFileVersionAttribute = this.ImportMscorlibType("System.Reflection", "AssemblyFileVersionAttribute"));
				}
				return result;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00012900 File Offset: 0x00010B00
		internal Type System_Security_Permissions_CodeAccessSecurityAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Security_Permissions_CodeAccessSecurityAttribute) == null)
				{
					result = (this.typeof_System_Security_Permissions_CodeAccessSecurityAttribute = this.ImportMscorlibType("System.Security.Permissions", "CodeAccessSecurityAttribute"));
				}
				return result;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00012930 File Offset: 0x00010B30
		internal Type System_Security_Permissions_PermissionSetAttribute
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Security_Permissions_PermissionSetAttribute) == null)
				{
					result = (this.typeof_System_Security_Permissions_PermissionSetAttribute = this.ImportMscorlibType("System.Security.Permissions", "PermissionSetAttribute"));
				}
				return result;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00012960 File Offset: 0x00010B60
		internal Type System_Security_Permissions_SecurityAction
		{
			get
			{
				Type result;
				if ((result = this.typeof_System_Security_Permissions_SecurityAction) == null)
				{
					result = (this.typeof_System_Security_Permissions_SecurityAction = this.ImportMscorlibType("System.Security.Permissions", "SecurityAction"));
				}
				return result;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00012990 File Offset: 0x00010B90
		internal bool HasMscorlib
		{
			get
			{
				return this.GetLoadedAssembly("mscorlib") != null;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600063F RID: 1599 RVA: 0x000129A0 File Offset: 0x00010BA0
		// (remove) Token: 0x06000640 RID: 1600 RVA: 0x000129AE File Offset: 0x00010BAE
		public event ResolveEventHandler AssemblyResolve
		{
			add
			{
				this.resolvers.Add(value);
			}
			remove
			{
				this.resolvers.Remove(value);
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000129C0 File Offset: 0x00010BC0
		public Type Import(Type type)
		{
			Type type2;
			if (!this.importedTypes.TryGetValue(type, out type2))
			{
				type2 = this.ImportImpl(type);
				if (type2 != null)
				{
					this.importedTypes.Add(type, type2);
				}
			}
			return type2;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000129FC File Offset: 0x00010BFC
		private Type ImportImpl(Type type)
		{
			if (type.Assembly == typeof(Type).Assembly)
			{
				throw new ArgumentException("Did you really want to import " + type.FullName + "?");
			}
			if (type.HasElementType)
			{
				if (type.IsArray)
				{
					if (type.Name.EndsWith("[]"))
					{
						return this.Import(type.GetElementType()).MakeArrayType();
					}
					return this.Import(type.GetElementType()).MakeArrayType(type.GetArrayRank());
				}
				else
				{
					if (type.IsByRef)
					{
						return this.Import(type.GetElementType()).MakeByRefType();
					}
					if (type.IsPointer)
					{
						return this.Import(type.GetElementType()).MakePointerType();
					}
					throw new InvalidOperationException();
				}
			}
			else if (type.IsGenericParameter)
			{
				if (type.DeclaringMethod != null)
				{
					throw new NotImplementedException();
				}
				return this.Import(type.DeclaringType).GetGenericArguments()[type.GenericParameterPosition];
			}
			else
			{
				if (type.IsGenericType && !type.IsGenericTypeDefinition)
				{
					Type[] genericArguments = type.GetGenericArguments();
					Type[] array = new Type[genericArguments.Length];
					for (int i = 0; i < genericArguments.Length; i++)
					{
						array[i] = this.Import(genericArguments[i]);
					}
					return this.Import(type.GetGenericTypeDefinition()).MakeGenericType(array);
				}
				if (type.Assembly == typeof(object).Assembly)
				{
					return this.ResolveType(this.Mscorlib, type.FullName);
				}
				return this.ResolveType(this.Import(type.Assembly), type.FullName);
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00012B7F File Offset: 0x00010D7F
		private Assembly Import(Assembly asm)
		{
			return this.Load(asm.FullName);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00012B90 File Offset: 0x00010D90
		public RawModule OpenRawModule(string path)
		{
			path = Path.GetFullPath(path);
			FileStream fileStream = null;
			RawModule result;
			try
			{
				fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
				result = this.OpenRawModule(fileStream, path);
				if (!this.MetadataOnly)
				{
					fileStream = null;
				}
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return result;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00012BE4 File Offset: 0x00010DE4
		public RawModule OpenRawModule(Stream stream, string location)
		{
			return this.OpenRawModule(stream, location, false);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00012BEF File Offset: 0x00010DEF
		public RawModule OpenMappedRawModule(Stream stream, string location)
		{
			return this.OpenRawModule(stream, location, true);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00012BFA File Offset: 0x00010DFA
		private RawModule OpenRawModule(Stream stream, string location, bool mapped)
		{
			if (!stream.CanRead || !stream.CanSeek || stream.Position != 0L)
			{
				throw new ArgumentException("Stream must support read/seek and current position must be zero.", "stream");
			}
			return new RawModule(new ModuleReader(null, this, stream, location, mapped));
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00012C34 File Offset: 0x00010E34
		public Assembly LoadAssembly(RawModule module)
		{
			string fullName = module.GetAssemblyName().FullName;
			Assembly assembly = this.GetLoadedAssembly(fullName);
			if (assembly == null)
			{
				AssemblyReader assemblyReader = module.ToAssembly();
				this.assemblies.Add(assemblyReader);
				assembly = assemblyReader;
			}
			return assembly;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00012C70 File Offset: 0x00010E70
		public Assembly LoadFile(string path)
		{
			Assembly result;
			try
			{
				using (RawModule rawModule = this.OpenRawModule(path))
				{
					result = this.LoadAssembly(rawModule);
				}
			}
			catch (IOException ex)
			{
				throw new FileNotFoundException(ex.Message, ex);
			}
			catch (UnauthorizedAccessException ex2)
			{
				throw new FileNotFoundException(ex2.Message, ex2);
			}
			return result;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00012CE0 File Offset: 0x00010EE0
		private static string GetSimpleAssemblyName(string refname)
		{
			int num;
			string result;
			if (Fusion.ParseAssemblySimpleName(refname, out num, out result) != ParseAssemblyResult.OK)
			{
				throw new ArgumentException();
			}
			return result;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00012D00 File Offset: 0x00010F00
		private Assembly GetLoadedAssembly(string refname)
		{
			Assembly assembly;
			if (!this.assembliesByName.TryGetValue(refname, out assembly))
			{
				string simpleAssemblyName = Universe.GetSimpleAssemblyName(refname);
				for (int i = 0; i < this.assemblies.Count; i++)
				{
					AssemblyComparisonResult assemblyComparisonResult;
					if (simpleAssemblyName.Equals(this.assemblies[i].Name, StringComparison.OrdinalIgnoreCase) && this.CompareAssemblyIdentity(refname, false, this.assemblies[i].FullName, false, out assemblyComparisonResult))
					{
						assembly = this.assemblies[i];
						this.assembliesByName.Add(refname, assembly);
						break;
					}
				}
			}
			return assembly;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00012D90 File Offset: 0x00010F90
		private Assembly GetDynamicAssembly(string refname)
		{
			string simpleAssemblyName = Universe.GetSimpleAssemblyName(refname);
			foreach (AssemblyBuilder assemblyBuilder in this.dynamicAssemblies)
			{
				AssemblyComparisonResult assemblyComparisonResult;
				if (simpleAssemblyName.Equals(assemblyBuilder.Name, StringComparison.OrdinalIgnoreCase) && this.CompareAssemblyIdentity(refname, false, assemblyBuilder.FullName, false, out assemblyComparisonResult))
				{
					return assemblyBuilder;
				}
			}
			return null;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00012E10 File Offset: 0x00011010
		public Assembly Load(string refname)
		{
			return this.Load(refname, null, true);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00012E1C File Offset: 0x0001101C
		internal Assembly Load(string refname, Module requestingModule, bool throwOnError)
		{
			Assembly assembly = this.GetLoadedAssembly(refname);
			if (assembly != null)
			{
				return assembly;
			}
			if (this.resolvers.Count == 0)
			{
				assembly = this.DefaultResolver(refname, throwOnError);
			}
			else
			{
				ResolveEventArgs args = new ResolveEventArgs(refname, (requestingModule == null) ? null : requestingModule.Assembly);
				foreach (ResolveEventHandler resolveEventHandler in this.resolvers)
				{
					assembly = resolveEventHandler(this, args);
					if (assembly != null)
					{
						break;
					}
				}
				if (assembly == null)
				{
					assembly = this.GetDynamicAssembly(refname);
				}
			}
			if (assembly != null)
			{
				string fullName = assembly.FullName;
				if (refname != fullName)
				{
					this.assembliesByName.Add(refname, assembly);
				}
				return assembly;
			}
			if (throwOnError)
			{
				throw new FileNotFoundException(refname);
			}
			return null;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00012EE4 File Offset: 0x000110E4
		private Assembly DefaultResolver(string refname, bool throwOnError)
		{
			Assembly dynamicAssembly = this.GetDynamicAssembly(refname);
			if (dynamicAssembly != null)
			{
				return dynamicAssembly;
			}
			string location;
			if (throwOnError)
			{
				try
				{
					location = Assembly.ReflectionOnlyLoad(refname).Location;
					goto IL_4F;
				}
				catch (BadImageFormatException ex)
				{
					throw new BadImageFormatException(ex.Message, ex);
				}
			}
			try
			{
				location = Assembly.ReflectionOnlyLoad(refname).Location;
			}
			catch (BadImageFormatException ex2)
			{
				throw new BadImageFormatException(ex2.Message, ex2);
			}
			catch (FileNotFoundException)
			{
				return null;
			}
			IL_4F:
			return this.LoadFile(location);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00012F74 File Offset: 0x00011174
		public Type GetType(string assemblyQualifiedTypeName)
		{
			return this.GetType(null, assemblyQualifiedTypeName, false, false);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00012F80 File Offset: 0x00011180
		public Type GetType(string assemblyQualifiedTypeName, bool throwOnError)
		{
			return this.GetType(null, assemblyQualifiedTypeName, throwOnError, false);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00012F8C File Offset: 0x0001118C
		public Type GetType(string assemblyQualifiedTypeName, bool throwOnError, bool ignoreCase)
		{
			return this.GetType(null, assemblyQualifiedTypeName, throwOnError, ignoreCase);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00012F98 File Offset: 0x00011198
		public Type GetType(Assembly context, string assemblyQualifiedTypeName, bool throwOnError)
		{
			return this.GetType(context, assemblyQualifiedTypeName, throwOnError, false);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00012FA4 File Offset: 0x000111A4
		public Type GetType(Assembly context, string assemblyQualifiedTypeName, bool throwOnError, bool ignoreCase)
		{
			TypeNameParser typeNameParser = TypeNameParser.Parse(assemblyQualifiedTypeName, throwOnError);
			if (typeNameParser.Error)
			{
				return null;
			}
			return typeNameParser.GetType(this, (context == null) ? null : context.ManifestModule, throwOnError, assemblyQualifiedTypeName, false, ignoreCase);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00012FE0 File Offset: 0x000111E0
		public Type ResolveType(Assembly context, string assemblyQualifiedTypeName)
		{
			TypeNameParser typeNameParser = TypeNameParser.Parse(assemblyQualifiedTypeName, false);
			if (typeNameParser.Error)
			{
				return null;
			}
			return typeNameParser.GetType(this, (context == null) ? null : context.ManifestModule, false, assemblyQualifiedTypeName, true, false);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00013018 File Offset: 0x00011218
		public Type GetBuiltInType(string ns, string name)
		{
			if (ns != "System")
			{
				return null;
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 2187444805U)
			{
				if (num <= 765439473U)
				{
					if (num <= 679076413U)
					{
						if (num != 423635464U)
						{
							if (num == 679076413U)
							{
								if (name == "Char")
								{
									return this.System_Char;
								}
							}
						}
						else if (name == "SByte")
						{
							return this.System_SByte;
						}
					}
					else if (num != 697196164U)
					{
						if (num == 765439473U)
						{
							if (name == "Int16")
							{
								return this.System_Int16;
							}
						}
					}
					else if (name == "Int64")
					{
						return this.System_Int64;
					}
				}
				else if (num <= 1324880019U)
				{
					if (num != 1323747186U)
					{
						if (num == 1324880019U)
						{
							if (name == "UInt64")
							{
								return this.System_UInt64;
							}
						}
					}
					else if (name == "UInt16")
					{
						return this.System_UInt16;
					}
				}
				else if (num != 1489158872U)
				{
					if (num != 1615808600U)
					{
						if (num == 2187444805U)
						{
							if (name == "UIntPtr")
							{
								return this.System_UIntPtr;
							}
						}
					}
					else if (name == "String")
					{
						return this.System_String;
					}
				}
				else if (name == "IntPtr")
				{
					return this.System_IntPtr;
				}
			}
			else if (num <= 3370340735U)
			{
				if (num <= 2711245919U)
				{
					if (num != 2386971688U)
					{
						if (num == 2711245919U)
						{
							if (name == "Int32")
							{
								return this.System_Int32;
							}
						}
					}
					else if (name == "Double")
					{
						return this.System_Double;
					}
				}
				else if (num != 3145356080U)
				{
					if (num == 3370340735U)
					{
						if (name == "Void")
						{
							return this.System_Void;
						}
					}
				}
				else if (name == "TypedReference")
				{
					return this.System_TypedReference;
				}
			}
			else if (num <= 3538687084U)
			{
				if (num != 3409549631U)
				{
					if (num == 3538687084U)
					{
						if (name == "UInt32")
						{
							return this.System_UInt32;
						}
					}
				}
				else if (name == "Byte")
				{
					return this.System_Byte;
				}
			}
			else if (num != 3851314394U)
			{
				if (num != 3969205087U)
				{
					if (num == 4051133705U)
					{
						if (name == "Single")
						{
							return this.System_Single;
						}
					}
				}
				else if (name == "Boolean")
				{
					return this.System_Boolean;
				}
			}
			else if (name == "Object")
			{
				return this.System_Object;
			}
			return null;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00013350 File Offset: 0x00011550
		public Assembly[] GetAssemblies()
		{
			Assembly[] array = new Assembly[this.assemblies.Count + this.dynamicAssemblies.Count];
			for (int i = 0; i < this.assemblies.Count; i++)
			{
				array[i] = this.assemblies[i];
			}
			int num = 0;
			for (int j = this.assemblies.Count; j < array.Length; j++)
			{
				array[j] = this.dynamicAssemblies[num];
				num++;
			}
			return array;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000133CD File Offset: 0x000115CD
		public bool CompareAssemblyIdentity(string assemblyIdentity1, bool unified1, string assemblyIdentity2, bool unified2, out AssemblyComparisonResult result)
		{
			if (!this.useNativeFusion)
			{
				return Fusion.CompareAssemblyIdentityPure(assemblyIdentity1, unified1, assemblyIdentity2, unified2, out result);
			}
			return Fusion.CompareAssemblyIdentityNative(assemblyIdentity1, unified1, assemblyIdentity2, unified2, out result);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000133F0 File Offset: 0x000115F0
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			return new AssemblyBuilder(this, name, null, null);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000133FB File Offset: 0x000115FB
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			return new AssemblyBuilder(this, name, null, assemblyAttributes);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00013406 File Offset: 0x00011606
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir)
		{
			return new AssemblyBuilder(this, name, dir, null);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00013411 File Offset: 0x00011611
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			AssemblyBuilder assemblyBuilder = new AssemblyBuilder(this, name, dir, null);
			Universe.AddLegacyPermissionSet(assemblyBuilder, requiredPermissions, SecurityAction.RequestMinimum);
			Universe.AddLegacyPermissionSet(assemblyBuilder, optionalPermissions, SecurityAction.RequestOptional);
			Universe.AddLegacyPermissionSet(assemblyBuilder, refusedPermissions, SecurityAction.RequestRefuse);
			return assemblyBuilder;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00013439 File Offset: 0x00011639
		private static void AddLegacyPermissionSet(AssemblyBuilder ab, PermissionSet permissionSet, SecurityAction action)
		{
			if (permissionSet != null)
			{
				ab.__AddDeclarativeSecurity(CustomAttributeBuilder.__FromBlob(CustomAttributeBuilder.LegacyPermissionSet, (int)action, Encoding.Unicode.GetBytes(permissionSet.ToXml().ToString())));
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00013464 File Offset: 0x00011664
		internal void RegisterDynamicAssembly(AssemblyBuilder asm)
		{
			this.dynamicAssemblies.Add(asm);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00013474 File Offset: 0x00011674
		internal void RenameAssembly(Assembly assembly, AssemblyName oldName)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, Assembly> keyValuePair in this.assembliesByName)
			{
				if (keyValuePair.Value == assembly)
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (string key in list)
			{
				this.assembliesByName.Remove(key);
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00013524 File Offset: 0x00011724
		public void Dispose()
		{
			foreach (AssemblyReader assemblyReader in this.assemblies)
			{
				Module[] loadedModules = assemblyReader.GetLoadedModules();
				for (int i = 0; i < loadedModules.Length; i++)
				{
					loadedModules[i].Dispose();
				}
			}
			foreach (AssemblyBuilder assemblyBuilder in this.dynamicAssemblies)
			{
				Module[] loadedModules = assemblyBuilder.GetLoadedModules();
				for (int i = 0; i < loadedModules.Length; i++)
				{
					loadedModules[i].Dispose();
				}
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000135E4 File Offset: 0x000117E4
		public Assembly CreateMissingAssembly(string assemblyName)
		{
			Assembly assembly = new MissingAssembly(this, assemblyName);
			string fullName = assembly.FullName;
			if (!this.assembliesByName.ContainsKey(fullName))
			{
				this.assembliesByName.Add(fullName, assembly);
			}
			return assembly;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001361C File Offset: 0x0001181C
		[Obsolete("Please set UniverseOptions.ResolveMissingMembers instead.")]
		public void EnableMissingMemberResolution()
		{
			this.resolveMissingMembers = true;
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00013625 File Offset: 0x00011825
		internal bool MissingMemberResolution
		{
			get
			{
				return this.resolveMissingMembers;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001362D File Offset: 0x0001182D
		internal bool EnableFunctionPointers
		{
			get
			{
				return this.enableFunctionPointers;
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00013638 File Offset: 0x00011838
		private Type GetMissingType(Module requester, Module module, Type declaringType, TypeName typeName)
		{
			if (this.missingTypes == null)
			{
				this.missingTypes = new Dictionary<Universe.ScopedTypeName, Type>();
			}
			Universe.ScopedTypeName key = new Universe.ScopedTypeName(declaringType ?? module, typeName);
			Type type;
			if (!this.missingTypes.TryGetValue(key, out type))
			{
				type = new MissingType(module, declaringType, typeName.Namespace, typeName.Name);
				this.missingTypes.Add(key, type);
			}
			if (this.ResolvedMissingMember != null && !module.Assembly.__IsMissing)
			{
				this.ResolvedMissingMember(requester, type);
			}
			return type;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000136C0 File Offset: 0x000118C0
		internal Type GetMissingTypeOrThrow(Module requester, Module module, Type declaringType, TypeName typeName)
		{
			if (this.resolveMissingMembers || module.Assembly.__IsMissing)
			{
				return this.GetMissingType(requester, module, declaringType, typeName);
			}
			string text = TypeNameParser.Escape(typeName.ToString());
			if (declaringType != null)
			{
				text = declaringType.FullName + "+" + text;
			}
			throw new TypeLoadException(string.Format("Type '{0}' not found in assembly '{1}'", text, module.Assembly.FullName));
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00013738 File Offset: 0x00011938
		internal MethodBase GetMissingMethodOrThrow(Module requester, Type declaringType, string name, MethodSignature signature)
		{
			if (this.resolveMissingMembers)
			{
				MethodBase methodBase = new MissingMethod(declaringType, name, signature);
				if (name == ".ctor")
				{
					methodBase = new ConstructorInfoImpl((MethodInfo)methodBase);
				}
				if (this.ResolvedMissingMember != null)
				{
					this.ResolvedMissingMember(requester, methodBase);
				}
				return methodBase;
			}
			throw new MissingMethodException(declaringType.ToString(), name);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00013794 File Offset: 0x00011994
		internal FieldInfo GetMissingFieldOrThrow(Module requester, Type declaringType, string name, FieldSignature signature)
		{
			if (this.resolveMissingMembers)
			{
				FieldInfo fieldInfo = new MissingField(declaringType, name, signature);
				if (this.ResolvedMissingMember != null)
				{
					this.ResolvedMissingMember(requester, fieldInfo);
				}
				return fieldInfo;
			}
			throw new MissingFieldException(declaringType.ToString(), name);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000137D8 File Offset: 0x000119D8
		internal PropertyInfo GetMissingPropertyOrThrow(Module requester, Type declaringType, string name, PropertySignature propertySignature)
		{
			if (this.resolveMissingMembers || declaringType.__IsMissing)
			{
				PropertyInfo propertyInfo = new MissingProperty(declaringType, name, propertySignature);
				if (this.ResolvedMissingMember != null && !declaringType.__IsMissing)
				{
					this.ResolvedMissingMember(requester, propertyInfo);
				}
				return propertyInfo;
			}
			throw new MissingMemberException(declaringType.ToString(), name);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001382C File Offset: 0x00011A2C
		internal Type CanonicalizeType(Type type)
		{
			Type type2;
			if (!this.canonicalizedTypes.TryGetValue(type, out type2))
			{
				type2 = type;
				Dictionary<Type, Type> dictionary = this.canonicalizedTypes;
				Type type3 = type2;
				dictionary.Add(type3, type3);
			}
			return type2;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00013859 File Offset: 0x00011A59
		public Type MakeFunctionPointer(__StandAloneMethodSig sig)
		{
			return FunctionPointerType.Make(this, sig);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00013864 File Offset: 0x00011A64
		public __StandAloneMethodSig MakeStandAloneMethodSig(CallingConvention callingConvention, Type returnType, CustomModifiers returnTypeCustomModifiers, Type[] parameterTypes, CustomModifiers[] parameterTypeCustomModifiers)
		{
			return new __StandAloneMethodSig(true, callingConvention, (CallingConventions)0, returnType ?? this.System_Void, Util.Copy(parameterTypes), Type.EmptyTypes, PackedCustomModifiers.CreateFromExternal(returnTypeCustomModifiers, parameterTypeCustomModifiers, Util.NullSafeLength<Type>(parameterTypes)));
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000138A0 File Offset: 0x00011AA0
		public __StandAloneMethodSig MakeStandAloneMethodSig(CallingConventions callingConvention, Type returnType, CustomModifiers returnTypeCustomModifiers, Type[] parameterTypes, Type[] optionalParameterTypes, CustomModifiers[] parameterTypeCustomModifiers)
		{
			return new __StandAloneMethodSig(false, (CallingConvention)0, callingConvention, returnType ?? this.System_Void, Util.Copy(parameterTypes), Util.Copy(optionalParameterTypes), PackedCustomModifiers.CreateFromExternal(returnTypeCustomModifiers, parameterTypeCustomModifiers, Util.NullSafeLength<Type>(parameterTypes) + Util.NullSafeLength<Type>(optionalParameterTypes)));
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600066E RID: 1646 RVA: 0x000138E8 File Offset: 0x00011AE8
		// (remove) Token: 0x0600066F RID: 1647 RVA: 0x00013920 File Offset: 0x00011B20
		public event ResolvedMissingMemberHandler ResolvedMissingMember
		{
			[CompilerGenerated]
			add
			{
				ResolvedMissingMemberHandler resolvedMissingMemberHandler = this.ResolvedMissingMember;
				ResolvedMissingMemberHandler resolvedMissingMemberHandler2;
				do
				{
					resolvedMissingMemberHandler2 = resolvedMissingMemberHandler;
					ResolvedMissingMemberHandler value2 = (ResolvedMissingMemberHandler)Delegate.Combine(resolvedMissingMemberHandler2, value);
					resolvedMissingMemberHandler = Interlocked.CompareExchange<ResolvedMissingMemberHandler>(ref this.ResolvedMissingMember, value2, resolvedMissingMemberHandler2);
				}
				while (resolvedMissingMemberHandler != resolvedMissingMemberHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ResolvedMissingMemberHandler resolvedMissingMemberHandler = this.ResolvedMissingMember;
				ResolvedMissingMemberHandler resolvedMissingMemberHandler2;
				do
				{
					resolvedMissingMemberHandler2 = resolvedMissingMemberHandler;
					ResolvedMissingMemberHandler value2 = (ResolvedMissingMemberHandler)Delegate.Remove(resolvedMissingMemberHandler2, value);
					resolvedMissingMemberHandler = Interlocked.CompareExchange<ResolvedMissingMemberHandler>(ref this.ResolvedMissingMember, value2, resolvedMissingMemberHandler2);
				}
				while (resolvedMissingMemberHandler != resolvedMissingMemberHandler2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000670 RID: 1648 RVA: 0x00013955 File Offset: 0x00011B55
		// (remove) Token: 0x06000671 RID: 1649 RVA: 0x00013971 File Offset: 0x00011B71
		public event Predicate<Type> MissingTypeIsValueType
		{
			add
			{
				if (this.missingTypeIsValueType != null)
				{
					throw new InvalidOperationException("Only a single MissingTypeIsValueType handler can be registered.");
				}
				this.missingTypeIsValueType = value;
			}
			remove
			{
				if (value.Equals(this.missingTypeIsValueType))
				{
					this.missingTypeIsValueType = null;
				}
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00013988 File Offset: 0x00011B88
		public static Universe FromAssembly(Assembly assembly)
		{
			return assembly.universe;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00013990 File Offset: 0x00011B90
		internal bool ResolveMissingTypeIsValueType(MissingType missingType)
		{
			if (this.missingTypeIsValueType != null)
			{
				return this.missingTypeIsValueType(missingType);
			}
			throw new MissingMemberException(missingType);
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x000139AD File Offset: 0x00011BAD
		internal bool ReturnPseudoCustomAttributes
		{
			get
			{
				return this.returnPseudoCustomAttributes;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x000139B5 File Offset: 0x00011BB5
		internal bool AutomaticallyProvideDefaultConstructor
		{
			get
			{
				return this.automaticallyProvideDefaultConstructor;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x000139BD File Offset: 0x00011BBD
		internal bool MetadataOnly
		{
			get
			{
				return (this.options & UniverseOptions.MetadataOnly) > UniverseOptions.None;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x000139CB File Offset: 0x00011BCB
		internal bool WindowsRuntimeProjection
		{
			get
			{
				return (this.options & UniverseOptions.DisableWindowsRuntimeProjection) == UniverseOptions.None;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x000139D9 File Offset: 0x00011BD9
		internal bool DecodeVersionInfoAttributeBlobs
		{
			get
			{
				return (this.options & UniverseOptions.DecodeVersionInfoAttributeBlobs) > UniverseOptions.None;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x000139EA File Offset: 0x00011BEA
		internal bool Deterministic
		{
			get
			{
				return (this.options & UniverseOptions.DeterministicOutput) > UniverseOptions.None;
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000139FB File Offset: 0x00011BFB
		// Note: this type is marked as 'beforefieldinit'.
		static Universe()
		{
		}

		// Token: 0x04000236 RID: 566
		internal static readonly bool MonoRuntime = Type.GetType("Mono.Runtime") != null;

		// Token: 0x04000237 RID: 567
		private readonly Dictionary<Type, Type> canonicalizedTypes = new Dictionary<Type, Type>();

		// Token: 0x04000238 RID: 568
		private readonly List<AssemblyReader> assemblies = new List<AssemblyReader>();

		// Token: 0x04000239 RID: 569
		private readonly List<AssemblyBuilder> dynamicAssemblies = new List<AssemblyBuilder>();

		// Token: 0x0400023A RID: 570
		private readonly Dictionary<string, Assembly> assembliesByName = new Dictionary<string, Assembly>();

		// Token: 0x0400023B RID: 571
		private readonly Dictionary<Type, Type> importedTypes = new Dictionary<Type, Type>();

		// Token: 0x0400023C RID: 572
		private Dictionary<Universe.ScopedTypeName, Type> missingTypes;

		// Token: 0x0400023D RID: 573
		private bool resolveMissingMembers;

		// Token: 0x0400023E RID: 574
		private readonly bool enableFunctionPointers;

		// Token: 0x0400023F RID: 575
		private readonly bool useNativeFusion;

		// Token: 0x04000240 RID: 576
		private readonly bool returnPseudoCustomAttributes;

		// Token: 0x04000241 RID: 577
		private readonly bool automaticallyProvideDefaultConstructor;

		// Token: 0x04000242 RID: 578
		private readonly UniverseOptions options;

		// Token: 0x04000243 RID: 579
		private Type typeof_System_Object;

		// Token: 0x04000244 RID: 580
		private Type typeof_System_ValueType;

		// Token: 0x04000245 RID: 581
		private Type typeof_System_Enum;

		// Token: 0x04000246 RID: 582
		private Type typeof_System_Void;

		// Token: 0x04000247 RID: 583
		private Type typeof_System_Boolean;

		// Token: 0x04000248 RID: 584
		private Type typeof_System_Char;

		// Token: 0x04000249 RID: 585
		private Type typeof_System_SByte;

		// Token: 0x0400024A RID: 586
		private Type typeof_System_Byte;

		// Token: 0x0400024B RID: 587
		private Type typeof_System_Int16;

		// Token: 0x0400024C RID: 588
		private Type typeof_System_UInt16;

		// Token: 0x0400024D RID: 589
		private Type typeof_System_Int32;

		// Token: 0x0400024E RID: 590
		private Type typeof_System_UInt32;

		// Token: 0x0400024F RID: 591
		private Type typeof_System_Int64;

		// Token: 0x04000250 RID: 592
		private Type typeof_System_UInt64;

		// Token: 0x04000251 RID: 593
		private Type typeof_System_Single;

		// Token: 0x04000252 RID: 594
		private Type typeof_System_Double;

		// Token: 0x04000253 RID: 595
		private Type typeof_System_String;

		// Token: 0x04000254 RID: 596
		private Type typeof_System_IntPtr;

		// Token: 0x04000255 RID: 597
		private Type typeof_System_UIntPtr;

		// Token: 0x04000256 RID: 598
		private Type typeof_System_TypedReference;

		// Token: 0x04000257 RID: 599
		private Type typeof_System_Type;

		// Token: 0x04000258 RID: 600
		private Type typeof_System_Array;

		// Token: 0x04000259 RID: 601
		private Type typeof_System_DateTime;

		// Token: 0x0400025A RID: 602
		private Type typeof_System_DBNull;

		// Token: 0x0400025B RID: 603
		private Type typeof_System_Decimal;

		// Token: 0x0400025C RID: 604
		private Type typeof_System_AttributeUsageAttribute;

		// Token: 0x0400025D RID: 605
		private Type typeof_System_Runtime_InteropServices_DllImportAttribute;

		// Token: 0x0400025E RID: 606
		private Type typeof_System_Runtime_InteropServices_FieldOffsetAttribute;

		// Token: 0x0400025F RID: 607
		private Type typeof_System_Runtime_InteropServices_MarshalAsAttribute;

		// Token: 0x04000260 RID: 608
		private Type typeof_System_Runtime_InteropServices_UnmanagedType;

		// Token: 0x04000261 RID: 609
		private Type typeof_System_Runtime_InteropServices_VarEnum;

		// Token: 0x04000262 RID: 610
		private Type typeof_System_Runtime_InteropServices_PreserveSigAttribute;

		// Token: 0x04000263 RID: 611
		private Type typeof_System_Runtime_InteropServices_CallingConvention;

		// Token: 0x04000264 RID: 612
		private Type typeof_System_Runtime_InteropServices_CharSet;

		// Token: 0x04000265 RID: 613
		private Type typeof_System_Runtime_CompilerServices_DecimalConstantAttribute;

		// Token: 0x04000266 RID: 614
		private Type typeof_System_Reflection_AssemblyCopyrightAttribute;

		// Token: 0x04000267 RID: 615
		private Type typeof_System_Reflection_AssemblyTrademarkAttribute;

		// Token: 0x04000268 RID: 616
		private Type typeof_System_Reflection_AssemblyProductAttribute;

		// Token: 0x04000269 RID: 617
		private Type typeof_System_Reflection_AssemblyCompanyAttribute;

		// Token: 0x0400026A RID: 618
		private Type typeof_System_Reflection_AssemblyDescriptionAttribute;

		// Token: 0x0400026B RID: 619
		private Type typeof_System_Reflection_AssemblyTitleAttribute;

		// Token: 0x0400026C RID: 620
		private Type typeof_System_Reflection_AssemblyInformationalVersionAttribute;

		// Token: 0x0400026D RID: 621
		private Type typeof_System_Reflection_AssemblyFileVersionAttribute;

		// Token: 0x0400026E RID: 622
		private Type typeof_System_Security_Permissions_CodeAccessSecurityAttribute;

		// Token: 0x0400026F RID: 623
		private Type typeof_System_Security_Permissions_PermissionSetAttribute;

		// Token: 0x04000270 RID: 624
		private Type typeof_System_Security_Permissions_SecurityAction;

		// Token: 0x04000271 RID: 625
		private List<ResolveEventHandler> resolvers = new List<ResolveEventHandler>();

		// Token: 0x04000272 RID: 626
		private Predicate<Type> missingTypeIsValueType;

		// Token: 0x04000273 RID: 627
		[CompilerGenerated]
		private ResolvedMissingMemberHandler ResolvedMissingMember;

		// Token: 0x02000337 RID: 823
		private struct ScopedTypeName : IEquatable<Universe.ScopedTypeName>
		{
			// Token: 0x060025D3 RID: 9683 RVA: 0x000B4641 File Offset: 0x000B2841
			internal ScopedTypeName(object scope, TypeName name)
			{
				this.scope = scope;
				this.name = name;
			}

			// Token: 0x060025D4 RID: 9684 RVA: 0x000B4654 File Offset: 0x000B2854
			public override bool Equals(object obj)
			{
				Universe.ScopedTypeName? scopedTypeName = obj as Universe.ScopedTypeName?;
				return scopedTypeName != null && ((IEquatable<Universe.ScopedTypeName>)scopedTypeName.Value).Equals(this);
			}

			// Token: 0x060025D5 RID: 9685 RVA: 0x000B4690 File Offset: 0x000B2890
			public override int GetHashCode()
			{
				return this.scope.GetHashCode() * 7 + this.name.GetHashCode();
			}

			// Token: 0x060025D6 RID: 9686 RVA: 0x000B46BF File Offset: 0x000B28BF
			bool IEquatable<Universe.ScopedTypeName>.Equals(Universe.ScopedTypeName other)
			{
				return other.scope == this.scope && other.name == this.name;
			}

			// Token: 0x04000E74 RID: 3700
			private readonly object scope;

			// Token: 0x04000E75 RID: 3701
			private readonly TypeName name;
		}
	}
}

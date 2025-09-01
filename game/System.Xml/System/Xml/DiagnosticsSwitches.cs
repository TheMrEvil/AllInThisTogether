using System;
using System.Diagnostics;

namespace System.Xml
{
	// Token: 0x020001A2 RID: 418
	internal static class DiagnosticsSwitches
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0006230A File Offset: 0x0006050A
		public static BooleanSwitch XmlSchemaContentModel
		{
			get
			{
				if (DiagnosticsSwitches.xmlSchemaContentModel == null)
				{
					DiagnosticsSwitches.xmlSchemaContentModel = new BooleanSwitch("XmlSchemaContentModel", "Enable tracing for the XmlSchema content model.");
				}
				return DiagnosticsSwitches.xmlSchemaContentModel;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00062332 File Offset: 0x00060532
		public static TraceSwitch XmlSchema
		{
			get
			{
				if (DiagnosticsSwitches.xmlSchema == null)
				{
					DiagnosticsSwitches.xmlSchema = new TraceSwitch("XmlSchema", "Enable tracing for the XmlSchema class.");
				}
				return DiagnosticsSwitches.xmlSchema;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0006235A File Offset: 0x0006055A
		public static BooleanSwitch KeepTempFiles
		{
			get
			{
				if (DiagnosticsSwitches.keepTempFiles == null)
				{
					DiagnosticsSwitches.keepTempFiles = new BooleanSwitch("XmlSerialization.Compilation", "Keep XmlSerialization generated (temp) files.");
				}
				return DiagnosticsSwitches.keepTempFiles;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00062382 File Offset: 0x00060582
		public static BooleanSwitch PregenEventLog
		{
			get
			{
				if (DiagnosticsSwitches.pregenEventLog == null)
				{
					DiagnosticsSwitches.pregenEventLog = new BooleanSwitch("XmlSerialization.PregenEventLog", "Log failures while loading pre-generated XmlSerialization assembly.");
				}
				return DiagnosticsSwitches.pregenEventLog;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x000623AA File Offset: 0x000605AA
		public static TraceSwitch XmlSerialization
		{
			get
			{
				if (DiagnosticsSwitches.xmlSerialization == null)
				{
					DiagnosticsSwitches.xmlSerialization = new TraceSwitch("XmlSerialization", "Enable tracing for the System.Xml.Serialization component.");
				}
				return DiagnosticsSwitches.xmlSerialization;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x000623D2 File Offset: 0x000605D2
		public static TraceSwitch XslTypeInference
		{
			get
			{
				if (DiagnosticsSwitches.xslTypeInference == null)
				{
					DiagnosticsSwitches.xslTypeInference = new TraceSwitch("XslTypeInference", "Enable tracing for the XSLT type inference algorithm.");
				}
				return DiagnosticsSwitches.xslTypeInference;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x000623FA File Offset: 0x000605FA
		public static BooleanSwitch NonRecursiveTypeLoading
		{
			get
			{
				if (DiagnosticsSwitches.nonRecursiveTypeLoading == null)
				{
					DiagnosticsSwitches.nonRecursiveTypeLoading = new BooleanSwitch("XmlSerialization.NonRecursiveTypeLoading", "Turn on non-recursive algorithm generating XmlMappings for CLR types.");
				}
				return DiagnosticsSwitches.nonRecursiveTypeLoading;
			}
		}

		// Token: 0x04000FDB RID: 4059
		private static volatile BooleanSwitch xmlSchemaContentModel;

		// Token: 0x04000FDC RID: 4060
		private static volatile TraceSwitch xmlSchema;

		// Token: 0x04000FDD RID: 4061
		private static volatile BooleanSwitch keepTempFiles;

		// Token: 0x04000FDE RID: 4062
		private static volatile BooleanSwitch pregenEventLog;

		// Token: 0x04000FDF RID: 4063
		private static volatile TraceSwitch xmlSerialization;

		// Token: 0x04000FE0 RID: 4064
		private static volatile TraceSwitch xslTypeInference;

		// Token: 0x04000FE1 RID: 4065
		private static volatile BooleanSwitch nonRecursiveTypeLoading;
	}
}

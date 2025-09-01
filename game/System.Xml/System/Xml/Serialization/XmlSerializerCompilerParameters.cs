using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x02000277 RID: 631
	internal sealed class XmlSerializerCompilerParameters
	{
		// Token: 0x060017F7 RID: 6135 RVA: 0x0008CF51 File Offset: 0x0008B151
		private XmlSerializerCompilerParameters(CompilerParameters parameters, bool needTempDirAccess)
		{
			this.needTempDirAccess = needTempDirAccess;
			this.parameters = parameters;
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0008CF67 File Offset: 0x0008B167
		internal bool IsNeedTempDirAccess
		{
			get
			{
				return this.needTempDirAccess;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0008CF6F File Offset: 0x0008B16F
		internal CompilerParameters CodeDomParameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0008CF78 File Offset: 0x0008B178
		internal static XmlSerializerCompilerParameters Create(string location)
		{
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.GenerateInMemory = true;
			if (string.IsNullOrEmpty(location))
			{
				XmlSerializerSection xmlSerializerSection = ConfigurationManager.GetSection(ConfigurationStrings.XmlSerializerSectionPath) as XmlSerializerSection;
				location = ((xmlSerializerSection == null) ? location : xmlSerializerSection.TempFilesLocation);
				if (!string.IsNullOrEmpty(location))
				{
					location = location.Trim();
				}
			}
			compilerParameters.TempFiles = new TempFileCollection(location);
			return new XmlSerializerCompilerParameters(compilerParameters, string.IsNullOrEmpty(location));
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0008CFDE File Offset: 0x0008B1DE
		internal static XmlSerializerCompilerParameters Create(CompilerParameters parameters, bool needTempDirAccess)
		{
			return new XmlSerializerCompilerParameters(parameters, needTempDirAccess);
		}

		// Token: 0x04001899 RID: 6297
		private bool needTempDirAccess;

		// Token: 0x0400189A RID: 6298
		private CompilerParameters parameters;
	}
}

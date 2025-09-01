using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003E7 RID: 999
	internal class KeywordsTable
	{
		// Token: 0x060027B2 RID: 10162 RVA: 0x000EBC00 File Offset: 0x000E9E00
		public KeywordsTable(XmlNameTable nt)
		{
			this.NameTable = nt;
			this.AnalyzeString = nt.Add("analyze-string");
			this.ApplyImports = nt.Add("apply-imports");
			this.ApplyTemplates = nt.Add("apply-templates");
			this.Assembly = nt.Add("assembly");
			this.Attribute = nt.Add("attribute");
			this.AttributeSet = nt.Add("attribute-set");
			this.CallTemplate = nt.Add("call-template");
			this.CaseOrder = nt.Add("case-order");
			this.CDataSectionElements = nt.Add("cdata-section-elements");
			this.Character = nt.Add("character");
			this.CharacterMap = nt.Add("character-map");
			this.Choose = nt.Add("choose");
			this.Comment = nt.Add("comment");
			this.Copy = nt.Add("copy");
			this.CopyOf = nt.Add("copy-of");
			this.Count = nt.Add("count");
			this.DataType = nt.Add("data-type");
			this.DecimalFormat = nt.Add("decimal-format");
			this.DecimalSeparator = nt.Add("decimal-separator");
			this.DefaultCollation = nt.Add("default-collation");
			this.DefaultValidation = nt.Add("default-validation");
			this.Digit = nt.Add("digit");
			this.DisableOutputEscaping = nt.Add("disable-output-escaping");
			this.DocTypePublic = nt.Add("doctype-public");
			this.DocTypeSystem = nt.Add("doctype-system");
			this.Document = nt.Add("document");
			this.Element = nt.Add("element");
			this.Elements = nt.Add("elements");
			this.Encoding = nt.Add("encoding");
			this.ExcludeResultPrefixes = nt.Add("exclude-result-prefixes");
			this.ExtensionElementPrefixes = nt.Add("extension-element-prefixes");
			this.Fallback = nt.Add("fallback");
			this.ForEach = nt.Add("for-each");
			this.ForEachGroup = nt.Add("for-each-group");
			this.Format = nt.Add("format");
			this.From = nt.Add("from");
			this.Function = nt.Add("function");
			this.GroupingSeparator = nt.Add("grouping-separator");
			this.GroupingSize = nt.Add("grouping-size");
			this.Href = nt.Add("href");
			this.Id = nt.Add("id");
			this.If = nt.Add("if");
			this.ImplementsPrefix = nt.Add("implements-prefix");
			this.Import = nt.Add("import");
			this.ImportSchema = nt.Add("import-schema");
			this.Include = nt.Add("include");
			this.Indent = nt.Add("indent");
			this.Infinity = nt.Add("infinity");
			this.Key = nt.Add("key");
			this.Lang = nt.Add("lang");
			this.Language = nt.Add("language");
			this.LetterValue = nt.Add("letter-value");
			this.Level = nt.Add("level");
			this.Match = nt.Add("match");
			this.MatchingSubstring = nt.Add("matching-substring");
			this.MediaType = nt.Add("media-type");
			this.Message = nt.Add("message");
			this.Method = nt.Add("method");
			this.MinusSign = nt.Add("minus-sign");
			this.Mode = nt.Add("mode");
			this.Name = nt.Add("name");
			this.Namespace = nt.Add("namespace");
			this.NamespaceAlias = nt.Add("namespace-alias");
			this.NaN = nt.Add("NaN");
			this.NextMatch = nt.Add("next-match");
			this.NonMatchingSubstring = nt.Add("non-matching-substring");
			this.Number = nt.Add("number");
			this.OmitXmlDeclaration = nt.Add("omit-xml-declaration");
			this.Otherwise = nt.Add("otherwise");
			this.Order = nt.Add("order");
			this.Output = nt.Add("output");
			this.OutputCharacter = nt.Add("output-character");
			this.OutputVersion = nt.Add("output-version");
			this.Param = nt.Add("param");
			this.PatternSeparator = nt.Add("pattern-separator");
			this.Percent = nt.Add("percent");
			this.PerformSort = nt.Add("perform-sort");
			this.PerMille = nt.Add("per-mille");
			this.PreserveSpace = nt.Add("preserve-space");
			this.Priority = nt.Add("priority");
			this.ProcessingInstruction = nt.Add("processing-instruction");
			this.Required = nt.Add("required");
			this.ResultDocument = nt.Add("result-document");
			this.ResultPrefix = nt.Add("result-prefix");
			this.Script = nt.Add("script");
			this.Select = nt.Add("select");
			this.Separator = nt.Add("separator");
			this.Sequence = nt.Add("sequence");
			this.Sort = nt.Add("sort");
			this.Space = nt.Add("space");
			this.Standalone = nt.Add("standalone");
			this.StripSpace = nt.Add("strip-space");
			this.Stylesheet = nt.Add("stylesheet");
			this.StylesheetPrefix = nt.Add("stylesheet-prefix");
			this.Template = nt.Add("template");
			this.Terminate = nt.Add("terminate");
			this.Test = nt.Add("test");
			this.Text = nt.Add("text");
			this.Transform = nt.Add("transform");
			this.UrnMsxsl = nt.Add("urn:schemas-microsoft-com:xslt");
			this.UriXml = nt.Add("http://www.w3.org/XML/1998/namespace");
			this.UriXsl = nt.Add("http://www.w3.org/1999/XSL/Transform");
			this.UriWdXsl = nt.Add("http://www.w3.org/TR/WD-xsl");
			this.Use = nt.Add("use");
			this.UseAttributeSets = nt.Add("use-attribute-sets");
			this.UseWhen = nt.Add("use-when");
			this.Using = nt.Add("using");
			this.Value = nt.Add("value");
			this.ValueOf = nt.Add("value-of");
			this.Variable = nt.Add("variable");
			this.Version = nt.Add("version");
			this.When = nt.Add("when");
			this.WithParam = nt.Add("with-param");
			this.Xml = nt.Add("xml");
			this.Xmlns = nt.Add("xmlns");
			this.XPathDefaultNamespace = nt.Add("xpath-default-namespace");
			this.ZeroDigit = nt.Add("zero-digit");
		}

		// Token: 0x04001F19 RID: 7961
		public XmlNameTable NameTable;

		// Token: 0x04001F1A RID: 7962
		public string AnalyzeString;

		// Token: 0x04001F1B RID: 7963
		public string ApplyImports;

		// Token: 0x04001F1C RID: 7964
		public string ApplyTemplates;

		// Token: 0x04001F1D RID: 7965
		public string Assembly;

		// Token: 0x04001F1E RID: 7966
		public string Attribute;

		// Token: 0x04001F1F RID: 7967
		public string AttributeSet;

		// Token: 0x04001F20 RID: 7968
		public string CallTemplate;

		// Token: 0x04001F21 RID: 7969
		public string CaseOrder;

		// Token: 0x04001F22 RID: 7970
		public string CDataSectionElements;

		// Token: 0x04001F23 RID: 7971
		public string Character;

		// Token: 0x04001F24 RID: 7972
		public string CharacterMap;

		// Token: 0x04001F25 RID: 7973
		public string Choose;

		// Token: 0x04001F26 RID: 7974
		public string Comment;

		// Token: 0x04001F27 RID: 7975
		public string Copy;

		// Token: 0x04001F28 RID: 7976
		public string CopyOf;

		// Token: 0x04001F29 RID: 7977
		public string Count;

		// Token: 0x04001F2A RID: 7978
		public string DataType;

		// Token: 0x04001F2B RID: 7979
		public string DecimalFormat;

		// Token: 0x04001F2C RID: 7980
		public string DecimalSeparator;

		// Token: 0x04001F2D RID: 7981
		public string DefaultCollation;

		// Token: 0x04001F2E RID: 7982
		public string DefaultValidation;

		// Token: 0x04001F2F RID: 7983
		public string Digit;

		// Token: 0x04001F30 RID: 7984
		public string DisableOutputEscaping;

		// Token: 0x04001F31 RID: 7985
		public string DocTypePublic;

		// Token: 0x04001F32 RID: 7986
		public string DocTypeSystem;

		// Token: 0x04001F33 RID: 7987
		public string Document;

		// Token: 0x04001F34 RID: 7988
		public string Element;

		// Token: 0x04001F35 RID: 7989
		public string Elements;

		// Token: 0x04001F36 RID: 7990
		public string Encoding;

		// Token: 0x04001F37 RID: 7991
		public string ExcludeResultPrefixes;

		// Token: 0x04001F38 RID: 7992
		public string ExtensionElementPrefixes;

		// Token: 0x04001F39 RID: 7993
		public string Fallback;

		// Token: 0x04001F3A RID: 7994
		public string ForEach;

		// Token: 0x04001F3B RID: 7995
		public string ForEachGroup;

		// Token: 0x04001F3C RID: 7996
		public string Format;

		// Token: 0x04001F3D RID: 7997
		public string From;

		// Token: 0x04001F3E RID: 7998
		public string Function;

		// Token: 0x04001F3F RID: 7999
		public string GroupingSeparator;

		// Token: 0x04001F40 RID: 8000
		public string GroupingSize;

		// Token: 0x04001F41 RID: 8001
		public string Href;

		// Token: 0x04001F42 RID: 8002
		public string Id;

		// Token: 0x04001F43 RID: 8003
		public string If;

		// Token: 0x04001F44 RID: 8004
		public string ImplementsPrefix;

		// Token: 0x04001F45 RID: 8005
		public string Import;

		// Token: 0x04001F46 RID: 8006
		public string ImportSchema;

		// Token: 0x04001F47 RID: 8007
		public string Include;

		// Token: 0x04001F48 RID: 8008
		public string Indent;

		// Token: 0x04001F49 RID: 8009
		public string Infinity;

		// Token: 0x04001F4A RID: 8010
		public string Key;

		// Token: 0x04001F4B RID: 8011
		public string Lang;

		// Token: 0x04001F4C RID: 8012
		public string Language;

		// Token: 0x04001F4D RID: 8013
		public string LetterValue;

		// Token: 0x04001F4E RID: 8014
		public string Level;

		// Token: 0x04001F4F RID: 8015
		public string Match;

		// Token: 0x04001F50 RID: 8016
		public string MatchingSubstring;

		// Token: 0x04001F51 RID: 8017
		public string MediaType;

		// Token: 0x04001F52 RID: 8018
		public string Message;

		// Token: 0x04001F53 RID: 8019
		public string Method;

		// Token: 0x04001F54 RID: 8020
		public string MinusSign;

		// Token: 0x04001F55 RID: 8021
		public string Mode;

		// Token: 0x04001F56 RID: 8022
		public string Name;

		// Token: 0x04001F57 RID: 8023
		public string Namespace;

		// Token: 0x04001F58 RID: 8024
		public string NamespaceAlias;

		// Token: 0x04001F59 RID: 8025
		public string NaN;

		// Token: 0x04001F5A RID: 8026
		public string NextMatch;

		// Token: 0x04001F5B RID: 8027
		public string NonMatchingSubstring;

		// Token: 0x04001F5C RID: 8028
		public string Number;

		// Token: 0x04001F5D RID: 8029
		public string OmitXmlDeclaration;

		// Token: 0x04001F5E RID: 8030
		public string Order;

		// Token: 0x04001F5F RID: 8031
		public string Otherwise;

		// Token: 0x04001F60 RID: 8032
		public string Output;

		// Token: 0x04001F61 RID: 8033
		public string OutputCharacter;

		// Token: 0x04001F62 RID: 8034
		public string OutputVersion;

		// Token: 0x04001F63 RID: 8035
		public string Param;

		// Token: 0x04001F64 RID: 8036
		public string PatternSeparator;

		// Token: 0x04001F65 RID: 8037
		public string Percent;

		// Token: 0x04001F66 RID: 8038
		public string PerformSort;

		// Token: 0x04001F67 RID: 8039
		public string PerMille;

		// Token: 0x04001F68 RID: 8040
		public string PreserveSpace;

		// Token: 0x04001F69 RID: 8041
		public string Priority;

		// Token: 0x04001F6A RID: 8042
		public string ProcessingInstruction;

		// Token: 0x04001F6B RID: 8043
		public string Required;

		// Token: 0x04001F6C RID: 8044
		public string ResultDocument;

		// Token: 0x04001F6D RID: 8045
		public string ResultPrefix;

		// Token: 0x04001F6E RID: 8046
		public string Script;

		// Token: 0x04001F6F RID: 8047
		public string Select;

		// Token: 0x04001F70 RID: 8048
		public string Separator;

		// Token: 0x04001F71 RID: 8049
		public string Sequence;

		// Token: 0x04001F72 RID: 8050
		public string Sort;

		// Token: 0x04001F73 RID: 8051
		public string Space;

		// Token: 0x04001F74 RID: 8052
		public string Standalone;

		// Token: 0x04001F75 RID: 8053
		public string StripSpace;

		// Token: 0x04001F76 RID: 8054
		public string Stylesheet;

		// Token: 0x04001F77 RID: 8055
		public string StylesheetPrefix;

		// Token: 0x04001F78 RID: 8056
		public string Template;

		// Token: 0x04001F79 RID: 8057
		public string Terminate;

		// Token: 0x04001F7A RID: 8058
		public string Test;

		// Token: 0x04001F7B RID: 8059
		public string Text;

		// Token: 0x04001F7C RID: 8060
		public string Transform;

		// Token: 0x04001F7D RID: 8061
		public string UrnMsxsl;

		// Token: 0x04001F7E RID: 8062
		public string UriXml;

		// Token: 0x04001F7F RID: 8063
		public string UriXsl;

		// Token: 0x04001F80 RID: 8064
		public string UriWdXsl;

		// Token: 0x04001F81 RID: 8065
		public string Use;

		// Token: 0x04001F82 RID: 8066
		public string UseAttributeSets;

		// Token: 0x04001F83 RID: 8067
		public string UseWhen;

		// Token: 0x04001F84 RID: 8068
		public string Using;

		// Token: 0x04001F85 RID: 8069
		public string Value;

		// Token: 0x04001F86 RID: 8070
		public string ValueOf;

		// Token: 0x04001F87 RID: 8071
		public string Variable;

		// Token: 0x04001F88 RID: 8072
		public string Version;

		// Token: 0x04001F89 RID: 8073
		public string When;

		// Token: 0x04001F8A RID: 8074
		public string WithParam;

		// Token: 0x04001F8B RID: 8075
		public string Xml;

		// Token: 0x04001F8C RID: 8076
		public string Xmlns;

		// Token: 0x04001F8D RID: 8077
		public string XPathDefaultNamespace;

		// Token: 0x04001F8E RID: 8078
		public string ZeroDigit;
	}
}

using System;

namespace System.Xml.Utils
{
	// Token: 0x0200024D RID: 589
	internal static class Res
	{
		// Token: 0x060015C5 RID: 5573 RVA: 0x00002068 File Offset: 0x00000268
		public static string GetString(string name)
		{
			return name;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00084E88 File Offset: 0x00083088
		public static string GetString(string name, params object[] args)
		{
			return SR.GetString(name, args);
		}

		// Token: 0x04001723 RID: 5923
		public const string Xml_UserException = "{0}";

		// Token: 0x04001724 RID: 5924
		public const string Xml_ErrorFilePosition = "An error occurred at {0}({1},{2}).";

		// Token: 0x04001725 RID: 5925
		public const string Xml_InvalidOperation = "Operation is not valid due to the current state of the object.";

		// Token: 0x04001726 RID: 5926
		public const string Xml_EndOfInnerExceptionStack = "--- End of inner exception stack trace ---";

		// Token: 0x04001727 RID: 5927
		public const string XPath_UnclosedString = "String literal was not closed.";

		// Token: 0x04001728 RID: 5928
		public const string XPath_ScientificNotation = "Scientific notation is not allowed.";

		// Token: 0x04001729 RID: 5929
		public const string XPath_UnexpectedToken = "Unexpected token '{0}' in the expression.";

		// Token: 0x0400172A RID: 5930
		public const string XPath_NodeTestExpected = "Expected a node test, found '{0}'.";

		// Token: 0x0400172B RID: 5931
		public const string XPath_EofExpected = "Expected end of the expression, found '{0}'.";

		// Token: 0x0400172C RID: 5932
		public const string XPath_TokenExpected = "Expected token '{0}', found '{1}'.";

		// Token: 0x0400172D RID: 5933
		public const string XPath_InvalidAxisInPattern = "Only 'child' and 'attribute' axes are allowed in a pattern outside predicates.";

		// Token: 0x0400172E RID: 5934
		public const string XPath_PredicateAfterDot = "Abbreviated step '.' cannot be followed by a predicate. Use the full form 'self::node()[predicate]' instead.";

		// Token: 0x0400172F RID: 5935
		public const string XPath_PredicateAfterDotDot = "Abbreviated step '..' cannot be followed by a predicate. Use the full form 'parent::node()[predicate]' instead.";

		// Token: 0x04001730 RID: 5936
		public const string XPath_NArgsExpected = "Function '{0}()' must have {1} argument(s).";

		// Token: 0x04001731 RID: 5937
		public const string XPath_NOrMArgsExpected = "Function '{0}()' must have {1} or {2} argument(s).";

		// Token: 0x04001732 RID: 5938
		public const string XPath_AtLeastNArgsExpected = "Function '{0}()' must have at least {1} argument(s).";

		// Token: 0x04001733 RID: 5939
		public const string XPath_AtMostMArgsExpected = "Function '{0}()' must have no more than {2} arguments.";

		// Token: 0x04001734 RID: 5940
		public const string XPath_NodeSetArgumentExpected = "Argument {1} of function '{0}()' cannot be converted to a node-set.";

		// Token: 0x04001735 RID: 5941
		public const string XPath_NodeSetExpected = "Expression must evaluate to a node-set.";

		// Token: 0x04001736 RID: 5942
		public const string XPath_RtfInPathExpr = "To use a result tree fragment in a path expression, first convert it to a node-set using the msxsl:node-set() function.";

		// Token: 0x04001737 RID: 5943
		public const string Xslt_WarningAsError = "Warning as Error: {0}";

		// Token: 0x04001738 RID: 5944
		public const string Xslt_InputTooComplex = "The stylesheet is too complex.";

		// Token: 0x04001739 RID: 5945
		public const string Xslt_CannotLoadStylesheet = "Cannot load the stylesheet object referenced by URI '{0}', because the provided XmlResolver returned an object of type '{1}'. One of Stream, XmlReader, and IXPathNavigable types was expected.";

		// Token: 0x0400173A RID: 5946
		public const string Xslt_WrongStylesheetElement = "Stylesheet must start either with an 'xsl:stylesheet' or an 'xsl:transform' element, or with a literal result element that has an 'xsl:version' attribute, where prefix 'xsl' denotes the 'http://www.w3.org/1999/XSL/Transform' namespace.";

		// Token: 0x0400173B RID: 5947
		public const string Xslt_WdXslNamespace = "The 'http://www.w3.org/TR/WD-xsl' namespace is no longer supported.";

		// Token: 0x0400173C RID: 5948
		public const string Xslt_NotAtTop = "'{0}' element children must precede all other children of the '{1}' element.";

		// Token: 0x0400173D RID: 5949
		public const string Xslt_UnexpectedElement = "'{0}' cannot be a child of the '{1}' element.";

		// Token: 0x0400173E RID: 5950
		public const string Xslt_NullNsAtTopLevel = "Top-level element '{0}' may not have a null namespace URI.";

		// Token: 0x0400173F RID: 5951
		public const string Xslt_TextNodesNotAllowed = "'{0}' element cannot have text node children.";

		// Token: 0x04001740 RID: 5952
		public const string Xslt_NotEmptyContents = "The contents of '{0}' must be empty.";

		// Token: 0x04001741 RID: 5953
		public const string Xslt_InvalidAttribute = "'{0}' is an invalid attribute for the '{1}' element.";

		// Token: 0x04001742 RID: 5954
		public const string Xslt_MissingAttribute = "Missing mandatory attribute '{0}'.";

		// Token: 0x04001743 RID: 5955
		public const string Xslt_InvalidAttrValue = "'{1}' is an invalid value for the '{0}' attribute.";

		// Token: 0x04001744 RID: 5956
		public const string Xslt_BistateAttribute = "The value of the '{0}' attribute must be '{1}' or '{2}'.";

		// Token: 0x04001745 RID: 5957
		public const string Xslt_CharAttribute = "The value of the '{0}' attribute must be a single character.";

		// Token: 0x04001746 RID: 5958
		public const string Xslt_CircularInclude = "Stylesheet '{0}' cannot directly or indirectly include or import itself.";

		// Token: 0x04001747 RID: 5959
		public const string Xslt_SingleRightBraceInAvt = "The right curly brace in an attribute value template '{0}' outside an expression must be doubled.";

		// Token: 0x04001748 RID: 5960
		public const string Xslt_VariableCntSel2 = "The variable or parameter '{0}' cannot have both a 'select' attribute and non-empty content.";

		// Token: 0x04001749 RID: 5961
		public const string Xslt_KeyCntUse = "'xsl:key' has a 'use' attribute and has non-empty content, or it has empty content and no 'use' attribute.";

		// Token: 0x0400174A RID: 5962
		public const string Xslt_DupTemplateName = "'{0}' is a duplicate template name.";

		// Token: 0x0400174B RID: 5963
		public const string Xslt_BothMatchNameAbsent = "'xsl:template' must have either a 'match' attribute or a 'name' attribute, or both.";

		// Token: 0x0400174C RID: 5964
		public const string Xslt_InvalidVariable = "The variable or parameter '{0}' is either not defined or it is out of scope.";

		// Token: 0x0400174D RID: 5965
		public const string Xslt_DupGlobalVariable = "The variable or parameter '{0}' was duplicated with the same import precedence.";

		// Token: 0x0400174E RID: 5966
		public const string Xslt_DupLocalVariable = "The variable or parameter '{0}' was duplicated within the same scope.";

		// Token: 0x0400174F RID: 5967
		public const string Xslt_DupNsAlias = "Namespace URI '{0}' is declared to be an alias for multiple different namespace URIs with the same import precedence.";

		// Token: 0x04001750 RID: 5968
		public const string Xslt_EmptyAttrValue = "The value of the '{0}' attribute cannot be empty.";

		// Token: 0x04001751 RID: 5969
		public const string Xslt_EmptyNsAlias = "The value of the '{0}' attribute cannot be empty. Use '#default' to specify the default namespace.";

		// Token: 0x04001752 RID: 5970
		public const string Xslt_UnknownXsltFunction = "'{0}()' is an unknown XSLT function.";

		// Token: 0x04001753 RID: 5971
		public const string Xslt_UnsupportedXsltFunction = "'{0}()' is an unsupported XSLT function.";

		// Token: 0x04001754 RID: 5972
		public const string Xslt_NoAttributeSet = "A reference to attribute set '{0}' cannot be resolved. An 'xsl:attribute-set' of this name must be declared at the top level of the stylesheet.";

		// Token: 0x04001755 RID: 5973
		public const string Xslt_UndefinedKey = "A reference to key '{0}' cannot be resolved. An 'xsl:key' of this name must be declared at the top level of the stylesheet.";

		// Token: 0x04001756 RID: 5974
		public const string Xslt_CircularAttributeSet = "Circular reference in the definition of attribute set '{0}'.";

		// Token: 0x04001757 RID: 5975
		public const string Xslt_InvalidCallTemplate = "The named template '{0}' does not exist.";

		// Token: 0x04001758 RID: 5976
		public const string Xslt_InvalidPrefix = "Prefix '{0}' is not defined.";

		// Token: 0x04001759 RID: 5977
		public const string Xslt_ScriptXsltNamespace = "Script block cannot implement the XSLT namespace.";

		// Token: 0x0400175A RID: 5978
		public const string Xslt_ScriptInvalidLanguage = "Scripting language '{0}' is not supported.";

		// Token: 0x0400175B RID: 5979
		public const string Xslt_ScriptMixedLanguages = "All script blocks implementing the namespace '{0}' must use the same language.";

		// Token: 0x0400175C RID: 5980
		public const string Xslt_ScriptCompileException = "Error occurred while compiling the script: {0}";

		// Token: 0x0400175D RID: 5981
		public const string Xslt_ScriptNotAtTop = "Element '{0}' must precede script code.";

		// Token: 0x0400175E RID: 5982
		public const string Xslt_AssemblyNameHref = "'msxsl:assembly' must have either a 'name' attribute or an 'href' attribute, but not both.";

		// Token: 0x0400175F RID: 5983
		public const string Xslt_ScriptAndExtensionClash = "Cannot have both an extension object and a script implementing the same namespace '{0}'.";

		// Token: 0x04001760 RID: 5984
		public const string Xslt_NoDecimalFormat = "Decimal format '{0}' is not defined.";

		// Token: 0x04001761 RID: 5985
		public const string Xslt_DecimalFormatSignsNotDistinct = "The '{0}' and '{1}' attributes of 'xsl:decimal-format' must have distinct values.";

		// Token: 0x04001762 RID: 5986
		public const string Xslt_DecimalFormatRedefined = "The '{0}' attribute of 'xsl:decimal-format' cannot be redefined with a value of '{1}'.";

		// Token: 0x04001763 RID: 5987
		public const string Xslt_UnknownExtensionElement = "'{0}' is not a recognized extension element.";

		// Token: 0x04001764 RID: 5988
		public const string Xslt_ModeWithoutMatch = "An 'xsl:template' element without a 'match' attribute cannot have a 'mode' attribute.";

		// Token: 0x04001765 RID: 5989
		public const string Xslt_ModeListEmpty = "List of modes in 'xsl:template' element can't be empty.";

		// Token: 0x04001766 RID: 5990
		public const string Xslt_ModeListDup = "List of modes in 'xsl:template' element can't contain duplicates ('{0}').";

		// Token: 0x04001767 RID: 5991
		public const string Xslt_ModeListAll = "List of modes in 'xsl:template' element can't contain token '#all' together with any other value.";

		// Token: 0x04001768 RID: 5992
		public const string Xslt_PriorityWithoutMatch = "An 'xsl:template' element without a 'match' attribute cannot have a 'priority' attribute.";

		// Token: 0x04001769 RID: 5993
		public const string Xslt_InvalidApplyImports = "An 'xsl:apply-imports' element can only occur within an 'xsl:template' element with a 'match' attribute, and cannot occur within an 'xsl:for-each' element.";

		// Token: 0x0400176A RID: 5994
		public const string Xslt_DuplicateWithParam = "Value of parameter '{0}' cannot be specified more than once within a single 'xsl:call-template' or 'xsl:apply-templates' element.";

		// Token: 0x0400176B RID: 5995
		public const string Xslt_ReservedNS = "Elements and attributes cannot belong to the reserved namespace '{0}'.";

		// Token: 0x0400176C RID: 5996
		public const string Xslt_XmlnsAttr = "An attribute with a local name 'xmlns' and a null namespace URI cannot be created.";

		// Token: 0x0400176D RID: 5997
		public const string Xslt_NoWhen = "An 'xsl:choose' element must have at least one 'xsl:when' child.";

		// Token: 0x0400176E RID: 5998
		public const string Xslt_WhenAfterOtherwise = "'xsl:when' must precede the 'xsl:otherwise' element.";

		// Token: 0x0400176F RID: 5999
		public const string Xslt_DupOtherwise = "An 'xsl:choose' element can have only one 'xsl:otherwise' child.";

		// Token: 0x04001770 RID: 6000
		public const string Xslt_AttributeRedefinition = "Attribute '{0}' of 'xsl:output' cannot be defined more than once with the same import precedence.";

		// Token: 0x04001771 RID: 6001
		public const string Xslt_InvalidMethod = "'{0}' is not a supported output method. Supported methods are 'xml', 'html', and 'text'.";

		// Token: 0x04001772 RID: 6002
		public const string Xslt_InvalidEncoding = "'{0}' is not a supported encoding name.";

		// Token: 0x04001773 RID: 6003
		public const string Xslt_InvalidLanguage = "'{0}' is not a supported language identifier.";

		// Token: 0x04001774 RID: 6004
		public const string Xslt_InvalidCompareOption = "String comparison option(s) '{0}' are either invalid or cannot be used together.";

		// Token: 0x04001775 RID: 6005
		public const string Xslt_KeyNotAllowed = "The 'key()' function cannot be used in 'use' and 'match' attributes of 'xsl:key' element.";

		// Token: 0x04001776 RID: 6006
		public const string Xslt_VariablesNotAllowed = "Variables cannot be used within this expression.";

		// Token: 0x04001777 RID: 6007
		public const string Xslt_CurrentNotAllowed = "The 'current()' function cannot be used in a pattern.";

		// Token: 0x04001778 RID: 6008
		public const string Xslt_DocumentFuncProhibited = "Execution of the 'document()' function was prohibited. Use the XsltSettings.EnableDocumentFunction property to enable it.";

		// Token: 0x04001779 RID: 6009
		public const string Xslt_ScriptsProhibited = "Execution of scripts was prohibited. Use the XsltSettings.EnableScript property to enable it.";

		// Token: 0x0400177A RID: 6010
		public const string Xslt_ItemNull = "Extension functions cannot return null values.";

		// Token: 0x0400177B RID: 6011
		public const string Xslt_NodeSetNotNode = "Cannot convert a node-set which contains zero nodes or more than one node to a single node.";

		// Token: 0x0400177C RID: 6012
		public const string Xslt_UnsupportedClrType = "Extension function parameters or return values which have Clr type '{0}' are not supported.";

		// Token: 0x0400177D RID: 6013
		public const string Xslt_NotYetImplemented = "'{0}' is not yet implemented.";

		// Token: 0x0400177E RID: 6014
		public const string Xslt_SchemaDeclaration = "'{0}' declaration is not permitted in non-schema aware processor.";

		// Token: 0x0400177F RID: 6015
		public const string Xslt_SchemaAttribute = "Attribute '{0}' is not permitted in basic XSLT processor (http://www.w3.org/TR/xslt20/#dt-basic-xslt-processor).";

		// Token: 0x04001780 RID: 6016
		public const string Xslt_SchemaAttributeValue = "Value '{1}' of attribute '{0}' is not permitted in basic XSLT processor (http://www.w3.org/TR/xslt20/#dt-basic-xslt-processor).";

		// Token: 0x04001781 RID: 6017
		public const string Xslt_ElementCntSel = "The element '{0}' cannot have both a 'select' attribute and non-empty content.";

		// Token: 0x04001782 RID: 6018
		public const string Xslt_PerformSortCntSel = "The element 'xsl:perform-sort' cannot have 'select' attribute any content other than 'xsl:sort' and 'xsl:fallback' instructions.";

		// Token: 0x04001783 RID: 6019
		public const string Xslt_RequiredAndSelect = "Mandatory parameter '{0}' must be empty and must not have a 'select' attribute.";

		// Token: 0x04001784 RID: 6020
		public const string Xslt_NoSelectNoContent = "Element '{0}' must have either 'select' attribute or non-empty content.";

		// Token: 0x04001785 RID: 6021
		public const string Xslt_NonTemplateTunnel = "Stylesheet or function parameter '{0}' cannot have attribute 'tunnel'.";

		// Token: 0x04001786 RID: 6022
		public const string Xslt_RequiredOnFunction = "The 'required' attribute must not be specified for parameter '{0}'. Function parameters are always mandatory.";

		// Token: 0x04001787 RID: 6023
		public const string Xslt_ExcludeDefault = "Value '#default' is used within the 'exclude-result-prefixes' attribute and the parent element of this attribute has no default namespace.";

		// Token: 0x04001788 RID: 6024
		public const string Xslt_CollationSyntax = "The value of an 'default-collation' attribute contains no recognized collation URI.";

		// Token: 0x04001789 RID: 6025
		public const string Xslt_AnalyzeStringDupChild = "'xsl:analyze-string' cannot have second child with name '{0}'.";

		// Token: 0x0400178A RID: 6026
		public const string Xslt_AnalyzeStringChildOrder = "When both 'xsl:matching-string' and 'xsl:non-matching-string' elements are present, 'xsl:matching-string' element must come first.";

		// Token: 0x0400178B RID: 6027
		public const string Xslt_AnalyzeStringEmpty = "'xsl:analyze-string' must contain either 'xsl:matching-string' or 'xsl:non-matching-string' elements or both.";

		// Token: 0x0400178C RID: 6028
		public const string Xslt_SortStable = "Only the first 'xsl:sort' element may have 'stable' attribute.";

		// Token: 0x0400178D RID: 6029
		public const string Xslt_InputTypeAnnotations = "It is an error if there is a stylesheet module in the stylesheet that specifies 'input-type-annotations'=\"strip\" and another stylesheet module that specifies 'input-type-annotations'=\"preserve\".";

		// Token: 0x0400178E RID: 6030
		public const string Coll_BadOptFormat = "Collation option '{0}' is invalid. Options must have the following format: <option-name>=<option-value>.";

		// Token: 0x0400178F RID: 6031
		public const string Coll_Unsupported = "The collation '{0}' is not supported.";

		// Token: 0x04001790 RID: 6032
		public const string Coll_UnsupportedLanguage = "Collation language '{0}' is not supported.";

		// Token: 0x04001791 RID: 6033
		public const string Coll_UnsupportedOpt = "Unsupported option '{0}' in collation.";

		// Token: 0x04001792 RID: 6034
		public const string Coll_UnsupportedOptVal = "Collation option '{0}' cannot have the value '{1}'.";

		// Token: 0x04001793 RID: 6035
		public const string Coll_UnsupportedSortOpt = "Unsupported sort option '{0}' in collation.";

		// Token: 0x04001794 RID: 6036
		public const string Qil_Validation = "QIL Validation Error! '{0}'.";

		// Token: 0x04001795 RID: 6037
		public const string XmlIl_TooManyParameters = "Functions may not have more than 65535 parameters.";

		// Token: 0x04001796 RID: 6038
		public const string XmlIl_BadXmlState = "An item of type '{0}' cannot be constructed within a node of type '{1}'.";

		// Token: 0x04001797 RID: 6039
		public const string XmlIl_BadXmlStateAttr = "Attribute and namespace nodes cannot be added to the parent element after a text, comment, pi, or sub-element node has already been added.";

		// Token: 0x04001798 RID: 6040
		public const string XmlIl_NmspAfterAttr = "Namespace nodes cannot be added to the parent element after an attribute node has already been added.";

		// Token: 0x04001799 RID: 6041
		public const string XmlIl_NmspConflict = "Cannot construct namespace declaration xmlns{0}{1}='{2}'. Prefix '{1}' is already mapped to namespace '{3}'.";

		// Token: 0x0400179A RID: 6042
		public const string XmlIl_CantResolveEntity = "Cannot query the data source object referenced by URI '{0}', because the provided XmlResolver returned an object of type '{1}'. Only Stream, XmlReader, and IXPathNavigable data source objects are currently supported.";

		// Token: 0x0400179B RID: 6043
		public const string XmlIl_NoDefaultDocument = "Query requires a default data source, but no default was supplied to the query engine.";

		// Token: 0x0400179C RID: 6044
		public const string XmlIl_UnknownDocument = "Data source '{0}' cannot be located.";

		// Token: 0x0400179D RID: 6045
		public const string XmlIl_UnknownParam = "Supplied XsltArgumentList does not contain a parameter with local name '{0}' and namespace '{1}'.";

		// Token: 0x0400179E RID: 6046
		public const string XmlIl_UnknownExtObj = "Cannot find a script or an extension object associated with namespace '{0}'.";

		// Token: 0x0400179F RID: 6047
		public const string XmlIl_CantStripNav = "White space cannot be stripped from input documents that have already been loaded. Provide the input document as an XmlReader instead.";

		// Token: 0x040017A0 RID: 6048
		public const string XmlIl_ExtensionError = "An error occurred during a call to extension function '{0}'. See InnerException for a complete description of the error.";

		// Token: 0x040017A1 RID: 6049
		public const string XmlIl_TopLevelAttrNmsp = "XmlWriter cannot process the sequence returned by the query, because it contains an attribute or namespace node.";

		// Token: 0x040017A2 RID: 6050
		public const string XmlIl_NoExtensionMethod = "Extension object '{0}' does not contain a matching '{1}' method that has {2} parameter(s).";

		// Token: 0x040017A3 RID: 6051
		public const string XmlIl_AmbiguousExtensionMethod = "Ambiguous method call. Extension object '{0}' contains multiple '{1}' methods that have {2} parameter(s).";

		// Token: 0x040017A4 RID: 6052
		public const string XmlIl_NonPublicExtensionMethod = "Method '{1}' of extension object '{0}' cannot be called because it is not public.";

		// Token: 0x040017A5 RID: 6053
		public const string XmlIl_GenericExtensionMethod = "Method '{1}' of extension object '{0}' cannot be called because it is generic.";

		// Token: 0x040017A6 RID: 6054
		public const string XmlIl_ByRefType = "Method '{1}' of extension object '{0}' cannot be called because it has one or more ByRef parameters.";

		// Token: 0x040017A7 RID: 6055
		public const string XmlIl_DocumentLoadError = "An error occurred while loading document '{0}'. See InnerException for a complete description of the error.";

		// Token: 0x040017A8 RID: 6056
		public const string Xslt_CompileError = "XSLT compile error at {0}({1},{2}). See InnerException for details.";

		// Token: 0x040017A9 RID: 6057
		public const string Xslt_CompileError2 = "XSLT compile error.";

		// Token: 0x040017AA RID: 6058
		public const string Xslt_UnsuppFunction = "'{0}()' is an unsupported XSLT function.";

		// Token: 0x040017AB RID: 6059
		public const string Xslt_NotFirstImport = "'xsl:import' instructions must precede all other element children of an 'xsl:stylesheet' element.";

		// Token: 0x040017AC RID: 6060
		public const string Xslt_UnexpectedKeyword = "'{0}' cannot be a child of the '{1}' element.";

		// Token: 0x040017AD RID: 6061
		public const string Xslt_InvalidContents = "The contents of '{0}' are invalid.";

		// Token: 0x040017AE RID: 6062
		public const string Xslt_CantResolve = "Cannot resolve the referenced document '{0}'.";

		// Token: 0x040017AF RID: 6063
		public const string Xslt_SingleRightAvt = "Right curly brace in the attribute value template '{0}' must be doubled.";

		// Token: 0x040017B0 RID: 6064
		public const string Xslt_OpenBracesAvt = "The braces are not closed in AVT expression '{0}'.";

		// Token: 0x040017B1 RID: 6065
		public const string Xslt_OpenLiteralAvt = "The literal in AVT expression is not correctly closed '{0}'.";

		// Token: 0x040017B2 RID: 6066
		public const string Xslt_NestedAvt = "AVT cannot be nested in AVT '{0}'.";

		// Token: 0x040017B3 RID: 6067
		public const string Xslt_EmptyAvtExpr = "XPath Expression in AVT cannot be empty: '{0}'.";

		// Token: 0x040017B4 RID: 6068
		public const string Xslt_InvalidXPath = "'{0}' is an invalid XPath expression.";

		// Token: 0x040017B5 RID: 6069
		public const string Xslt_InvalidQName = "'{0}' is an invalid QName.";

		// Token: 0x040017B6 RID: 6070
		public const string Xslt_NoStylesheetLoaded = "No stylesheet was loaded.";

		// Token: 0x040017B7 RID: 6071
		public const string Xslt_TemplateNoAttrib = "The 'xsl:template' instruction must have the 'match' and/or 'name' attribute present.";

		// Token: 0x040017B8 RID: 6072
		public const string Xslt_DupVarName = "Variable or parameter '{0}' was duplicated within the same scope.";

		// Token: 0x040017B9 RID: 6073
		public const string Xslt_WrongNumberArgs = "XSLT function '{0}()' has the wrong number of arguments.";

		// Token: 0x040017BA RID: 6074
		public const string Xslt_NoNodeSetConversion = "Cannot convert the operand to a node-set.";

		// Token: 0x040017BB RID: 6075
		public const string Xslt_NoNavigatorConversion = "Cannot convert the operand to 'Result tree fragment'.";

		// Token: 0x040017BC RID: 6076
		public const string Xslt_FunctionFailed = "Function '{0}()' has failed.";

		// Token: 0x040017BD RID: 6077
		public const string Xslt_InvalidFormat = "Format cannot be empty.";

		// Token: 0x040017BE RID: 6078
		public const string Xslt_InvalidFormat1 = "Format '{0}' cannot have digit symbol after zero digit symbol before a decimal point.";

		// Token: 0x040017BF RID: 6079
		public const string Xslt_InvalidFormat2 = "Format '{0}' cannot have zero digit symbol after digit symbol after decimal point.";

		// Token: 0x040017C0 RID: 6080
		public const string Xslt_InvalidFormat3 = "Format '{0}' has two pattern separators.";

		// Token: 0x040017C1 RID: 6081
		public const string Xslt_InvalidFormat4 = "Format '{0}' cannot end with a pattern separator.";

		// Token: 0x040017C2 RID: 6082
		public const string Xslt_InvalidFormat5 = "Format '{0}' cannot have two decimal separators.";

		// Token: 0x040017C3 RID: 6083
		public const string Xslt_InvalidFormat8 = "Format string should have at least one digit or zero digit.";

		// Token: 0x040017C4 RID: 6084
		public const string Xslt_ScriptCompileErrors = "Script compile errors:\n{0}";

		// Token: 0x040017C5 RID: 6085
		public const string Xslt_ScriptInvalidPrefix = "Cannot find the script or external object that implements prefix '{0}'.";

		// Token: 0x040017C6 RID: 6086
		public const string Xslt_ScriptDub = "Namespace '{0}' has a duplicate implementation.";

		// Token: 0x040017C7 RID: 6087
		public const string Xslt_ScriptEmpty = "The 'msxsl:script' element cannot be empty.";

		// Token: 0x040017C8 RID: 6088
		public const string Xslt_DupDecimalFormat = "Decimal format '{0}' has a duplicate declaration.";

		// Token: 0x040017C9 RID: 6089
		public const string Xslt_CircularReference = "Circular reference in the definition of variable '{0}'.";

		// Token: 0x040017CA RID: 6090
		public const string Xslt_InvalidExtensionNamespace = "Extension namespace cannot be 'null' or an XSLT namespace URI.";

		// Token: 0x040017CB RID: 6091
		public const string Xslt_InvalidModeAttribute = "An 'xsl:template' element without a 'match' attribute cannot have a 'mode' attribute.";

		// Token: 0x040017CC RID: 6092
		public const string Xslt_MultipleRoots = "There are multiple root elements in the output XML.";

		// Token: 0x040017CD RID: 6093
		public const string Xslt_ApplyImports = "The 'xsl:apply-imports' instruction cannot be included within the content of an 'xsl:for-each' instruction or within an 'xsl:template' instruction without the 'match' attribute.";

		// Token: 0x040017CE RID: 6094
		public const string Xslt_Terminate = "Transform terminated: '{0}'.";

		// Token: 0x040017CF RID: 6095
		public const string Xslt_InvalidPattern = "'{0}' is an invalid XSLT pattern.";

		// Token: 0x040017D0 RID: 6096
		public const string Xslt_EmptyTagRequired = "The tag '{0}' must be empty.";

		// Token: 0x040017D1 RID: 6097
		public const string Xslt_WrongNamespace = "The wrong namespace was used for XSL. Use 'http://www.w3.org/1999/XSL/Transform'.";

		// Token: 0x040017D2 RID: 6098
		public const string Xslt_InvalidFormat6 = "Format '{0}' has both  '*' and '_' which is invalid.";

		// Token: 0x040017D3 RID: 6099
		public const string Xslt_InvalidFormat7 = "Format '{0}' has '{1}' which is invalid.";

		// Token: 0x040017D4 RID: 6100
		public const string Xslt_ScriptMixLang = "Multiple scripting languages for the same namespace is not supported.";

		// Token: 0x040017D5 RID: 6101
		public const string Xslt_ScriptInvalidLang = "The scripting language '{0}' is not supported.";

		// Token: 0x040017D6 RID: 6102
		public const string Xslt_InvalidExtensionPermitions = "Extension object should not have wider permissions than the caller of the AddExtensionObject(). If wider permissions are needed, wrap the extension object.";

		// Token: 0x040017D7 RID: 6103
		public const string Xslt_InvalidParamNamespace = "Parameter cannot belong to XSLT namespace.";

		// Token: 0x040017D8 RID: 6104
		public const string Xslt_DuplicateParametr = "Duplicate parameter: '{0}'.";

		// Token: 0x040017D9 RID: 6105
		public const string Xslt_VariableCntSel = "The '{0}' variable has both a select attribute of '{1}' and non-empty contents.";
	}
}

using System;

// Token: 0x02000004 RID: 4
internal class SR
{
	// Token: 0x06000004 RID: 4 RVA: 0x0000205B File Offset: 0x0000025B
	public static string Format(string format, params object[] args)
	{
		return string.Format(format, args);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
	public SR()
	{
	}

	// Token: 0x0400002A RID: 42
	public const string CantTellPrinterName = "(printer name protected due to security restrictions)";

	// Token: 0x0400002B RID: 43
	public const string CantChangeImmutableObjects = "Changes cannot be made to {0} because permissions are not valid.";

	// Token: 0x0400002C RID: 44
	public const string CantMakeIconTransparent = "Bitmaps that are icons cannot be made transparent. Icons natively support transparency. Use the Icon constructor to create an icon.";

	// Token: 0x0400002D RID: 45
	public const string ColorNotSystemColor = "The color {0} is not a system color.";

	// Token: 0x0400002E RID: 46
	public const string DotNET_ComponentType = ".NET Component";

	// Token: 0x0400002F RID: 47
	public const string GdiplusAborted = "Function was ended.";

	// Token: 0x04000030 RID: 48
	public const string GdiplusAccessDenied = "File access is denied.";

	// Token: 0x04000031 RID: 49
	public const string GdiplusCannotCreateGraphicsFromIndexedPixelFormat = "A Graphics object cannot be created from an image that has an indexed pixel format.";

	// Token: 0x04000032 RID: 50
	public const string GdiplusCannotSetPixelFromIndexedPixelFormat = "SetPixel is not supported for images with indexed pixel formats.";

	// Token: 0x04000033 RID: 51
	public const string GdiplusDestPointsInvalidParallelogram = "Destination points define a parallelogram which must have a length of 3. These points will represent the upper-left, upper-right, and lower-left coordinates (defined in that order).";

	// Token: 0x04000034 RID: 52
	public const string GdiplusDestPointsInvalidLength = "Destination points must be an array with a length of 3 or 4. A length of 3 defines a parallelogram with the upper-left, upper-right, and lower-left corners. A length of 4 defines a quadrilateral with the fourth element of the array specifying the lower-right coordinate.";

	// Token: 0x04000035 RID: 53
	public const string GdiplusFileNotFound = "File not found.";

	// Token: 0x04000036 RID: 54
	public const string GdiplusFontFamilyNotFound = "Font '{0}' cannot be found.";

	// Token: 0x04000037 RID: 55
	public const string GdiplusFontStyleNotFound = "Font '{0}' does not support style '{1}'.";

	// Token: 0x04000038 RID: 56
	public const string GdiplusGenericError = "A generic error occurred in GDI+.";

	// Token: 0x04000039 RID: 57
	public const string GdiplusInsufficientBuffer = "Buffer is too small (internal GDI+ error).";

	// Token: 0x0400003A RID: 58
	public const string GdiplusInvalidParameter = "Parameter is not valid.";

	// Token: 0x0400003B RID: 59
	public const string GdiplusInvalidRectangle = "Rectangle '{0}' cannot have a width or height equal to 0.";

	// Token: 0x0400003C RID: 60
	public const string GdiplusInvalidSize = "Operation requires a transformation of the image from GDI+ to GDI. GDI does not support images with a width or height greater than 32767.";

	// Token: 0x0400003D RID: 61
	public const string GdiplusOutOfMemory = "Out of memory.";

	// Token: 0x0400003E RID: 62
	public const string GdiplusNotImplemented = "Not implemented.";

	// Token: 0x0400003F RID: 63
	public const string GdiplusNotInitialized = "GDI+ is not properly initialized (internal GDI+ error).";

	// Token: 0x04000040 RID: 64
	public const string GdiplusNotTrueTypeFont = "Only TrueType fonts are supported. '{0}' is not a TrueType font.";

	// Token: 0x04000041 RID: 65
	public const string GdiplusNotTrueTypeFont_NoName = "Only TrueType fonts are supported. This is not a TrueType font.";

	// Token: 0x04000042 RID: 66
	public const string GdiplusObjectBusy = "Object is currently in use elsewhere.";

	// Token: 0x04000043 RID: 67
	public const string GdiplusOverflow = "Overflow error.";

	// Token: 0x04000044 RID: 68
	public const string GdiplusPropertyNotFoundError = "Property cannot be found.";

	// Token: 0x04000045 RID: 69
	public const string GdiplusPropertyNotSupportedError = "Property is not supported.";

	// Token: 0x04000046 RID: 70
	public const string GdiplusUnknown = "Unknown GDI+ error occurred.";

	// Token: 0x04000047 RID: 71
	public const string GdiplusUnknownImageFormat = "Image format is unknown.";

	// Token: 0x04000048 RID: 72
	public const string GdiplusUnsupportedGdiplusVersion = "Current version of GDI+ does not support this feature.";

	// Token: 0x04000049 RID: 73
	public const string GdiplusWrongState = "Bitmap region is already locked.";

	// Token: 0x0400004A RID: 74
	public const string GlobalAssemblyCache = " (Global Assembly Cache)";

	// Token: 0x0400004B RID: 75
	public const string GraphicsBufferCurrentlyBusy = "BufferedGraphicsContext cannot be disposed of because a buffer operation is currently in progress.";

	// Token: 0x0400004C RID: 76
	public const string GraphicsBufferQueryFail = "Screen-compatible bitmap cannot be created. The screen bitmap format cannot be determined.";

	// Token: 0x0400004D RID: 77
	public const string ToolboxItemLocked = "Toolbox item cannot be modified.";

	// Token: 0x0400004E RID: 78
	public const string ToolboxItemInvalidPropertyType = "Property {0} requires an argument of type {1}.";

	// Token: 0x0400004F RID: 79
	public const string ToolboxItemValueNotSerializable = "Data type {0} is not serializable. Items added to a property dictionary must be serializable.";

	// Token: 0x04000050 RID: 80
	public const string ToolboxItemInvalidKey = "Argument should be a non-empty string.";

	// Token: 0x04000051 RID: 81
	public const string IllegalState = "Internal state of the {0} class is invalid.";

	// Token: 0x04000052 RID: 82
	public const string InterpolationColorsColorBlendNotSet = "Property must be set to a valid ColorBlend object to use interpolation colors.";

	// Token: 0x04000053 RID: 83
	public const string InterpolationColorsCommon = "{0}{1} ColorBlend objects must be constructed with the same number of positions and color values. Positions must be between 0.0 and 1.0, 1.0 indicating the last element in the array.";

	// Token: 0x04000054 RID: 84
	public const string InterpolationColorsInvalidColorBlendObject = "ColorBlend object that was set is not valid.";

	// Token: 0x04000055 RID: 85
	public const string InterpolationColorsInvalidStartPosition = "Position's first element must be equal to 0.";

	// Token: 0x04000056 RID: 86
	public const string InterpolationColorsInvalidEndPosition = "Position's last element must be equal to 1.0.";

	// Token: 0x04000057 RID: 87
	public const string InterpolationColorsLength = "Array of colors and positions must contain at least two elements.";

	// Token: 0x04000058 RID: 88
	public const string InterpolationColorsLengthsDiffer = "Colors and positions do not have the same number of elements.";

	// Token: 0x04000059 RID: 89
	public const string InvalidArgument = "Value of '{1}' is not valid for '{0}'.";

	// Token: 0x0400005A RID: 90
	public const string InvalidBoundArgument = "Value of '{1}' is not valid for '{0}'. '{0}' should be greater than {2} and less than or equal to {3}.";

	// Token: 0x0400005B RID: 91
	public const string InvalidClassName = "Class name is not valid.";

	// Token: 0x0400005C RID: 92
	public const string InvalidColor = "Color '{0}' is not valid.";

	// Token: 0x0400005D RID: 93
	public const string InvalidDashPattern = "DashPattern value is not valid.";

	// Token: 0x0400005E RID: 94
	public const string InvalidEx2BoundArgument = "Value of '{1}' is not valid for '{0}'. '{0}' should be greater than or equal to {2} and less than or equal to {3}.";

	// Token: 0x0400005F RID: 95
	public const string InvalidFrame = "Frame is not valid. Frame must be between 0 and FrameCount.";

	// Token: 0x04000060 RID: 96
	public const string InvalidGDIHandle = "Win32 handle that was passed to {0} is not valid or is the wrong type.";

	// Token: 0x04000061 RID: 97
	public const string InvalidImage = "Image type is unknown.";

	// Token: 0x04000062 RID: 98
	public const string InvalidLowBoundArgumentEx = "Value of '{1}' is not valid for '{0}'. '{0}' must be greater than or equal to {2}.";

	// Token: 0x04000063 RID: 99
	public const string InvalidPermissionLevel = "Permission level is not valid.";

	// Token: 0x04000064 RID: 100
	public const string InvalidPermissionState = "Permission state is not valid.";

	// Token: 0x04000065 RID: 101
	public const string InvalidPictureType = "Argument '{0}' must be a picture that can be used as a {1}.";

	// Token: 0x04000066 RID: 102
	public const string InvalidPrinterException_InvalidPrinter = "Settings to access printer '{0}' are not valid.";

	// Token: 0x04000067 RID: 103
	public const string InvalidPrinterException_NoDefaultPrinter = "No printers are installed.";

	// Token: 0x04000068 RID: 104
	public const string InvalidPrinterHandle = "Handle {0} is not valid.";

	// Token: 0x04000069 RID: 105
	public const string ValidRangeX = "Parameter must be positive and < Width.";

	// Token: 0x0400006A RID: 106
	public const string ValidRangeY = "Parameter must be positive and < Height.";

	// Token: 0x0400006B RID: 107
	public const string NativeHandle0 = "Native handle is 0.";

	// Token: 0x0400006C RID: 108
	public const string NoDefaultPrinter = "Default printer is not set.";

	// Token: 0x0400006D RID: 109
	public const string NotImplemented = "Not implemented.";

	// Token: 0x0400006E RID: 110
	public const string PDOCbeginPrintDescr = "Occurs when the document is about to be printed.";

	// Token: 0x0400006F RID: 111
	public const string PDOCdocumentNameDescr = "The name of the document shown to the user.";

	// Token: 0x04000070 RID: 112
	public const string PDOCdocumentPageSettingsDescr = "The page settings of the page currently being printed.";

	// Token: 0x04000071 RID: 113
	public const string PDOCendPrintDescr = "Occurs after the document has been printed.";

	// Token: 0x04000072 RID: 114
	public const string PDOCoriginAtMarginsDescr = "Indicates that the graphics origin is located at the user-specified page margins.";

	// Token: 0x04000073 RID: 115
	public const string PDOCprintControllerDescr = "Retrieves the print controller for this document.";

	// Token: 0x04000074 RID: 116
	public const string PDOCprintPageDescr = "Occurs once for each page to be printed.";

	// Token: 0x04000075 RID: 117
	public const string PDOCprinterSettingsDescr = "Retrieves the settings for the printer the document is currently being printed to.";

	// Token: 0x04000076 RID: 118
	public const string PDOCqueryPageSettingsDescr = "Occurs before each page is printed.  Useful for changing PageSettings for a particular page.";

	// Token: 0x04000077 RID: 119
	public const string PrintDocumentDesc = "Defines an object that sends output to a printer.";

	// Token: 0x04000078 RID: 120
	public const string PrintingPermissionBadXml = "XML is not valid.";

	// Token: 0x04000079 RID: 121
	public const string PrintingPermissionAttributeInvalidPermissionLevel = "Permission level must be between PrintingPermissionLevel.NoPrinting and PrintingPermissionLevel.AllPrinting.";

	// Token: 0x0400007A RID: 122
	public const string PropertyValueInvalidEntry = "IDictionary parameter contains at least one entry that is not valid. Ensure all values are consistent with the object's properties.";

	// Token: 0x0400007B RID: 123
	public const string PSizeNotCustom = "PaperSize cannot be changed unless the Kind property is set to Custom.";

	// Token: 0x0400007C RID: 124
	public const string ResourceNotFound = "Resource '{1}' cannot be found in class '{0}'.";

	// Token: 0x0400007D RID: 125
	public const string TargetNotPrintingPermission = "Target does not have permission to print.";

	// Token: 0x0400007E RID: 126
	public const string TextParseFailedFormat = "Text \"{0}\" cannot be parsed. The expected text format is \"{1}\".";

	// Token: 0x0400007F RID: 127
	public const string TriStateCompareError = "TriState.Default cannot be converted into a Boolean.";

	// Token: 0x04000080 RID: 128
	public const string toStringIcon = "(Icon)";

	// Token: 0x04000081 RID: 129
	public const string toStringNone = "(none)";

	// Token: 0x04000082 RID: 130
	public const string DCTypeInvalid = "GetObjectType on this dc returned an invalid value.";

	// Token: 0x04000083 RID: 131
	public const string InvalidEnumArgument = "The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.";

	// Token: 0x04000084 RID: 132
	public const string ConvertInvalidPrimitive = "{0} is not a valid value for {1}.";

	// Token: 0x04000085 RID: 133
	public const string LibgdiplusNotFound = "The native library \"libgdiplus\" is not installed on the system, or was otherwise unable to be loaded.";
}

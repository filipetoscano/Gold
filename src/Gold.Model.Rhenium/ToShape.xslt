<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:au="urn:gold/forms"
    xmlns:eo="urn:eo-util"
    exclude-result-prefixes="msxsl au eo">

    <xsl:param name="FileName" />
    <xsl:param name="Namespace" />
    <xsl:param name="UriDirectory" />

    <xsl:output method="text" indent="yes" />

    <xsl:variable name="NewLine">
        <xsl:text>
</xsl:text>
    </xsl:variable>


    <!-- ================================================================
    ~
    ~ flow
    ~
    ================================================================= -->
    <xsl:template match=" au:form ">
        <xsl:text>// autogenerate: do NOT manually edit
using Gold;
using System;
using System.Xml.Serialization;
</xsl:text>

        <xsl:text>
namespace </xsl:text>
        <xsl:value-of select=" $Namespace " />
        <xsl:text>
{</xsl:text>
        <xsl:value-of select=" $NewLine " />

        <!-- Shape definition -->
        <xsl:text>    public class </xsl:text>
        <xsl:value-of select=" $FileName "/>
        <xsl:text>Definition</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:text>    {</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:text>    }</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:value-of select=" $NewLine " />
        <xsl:value-of select=" $NewLine " />

        <!-- Shape properties -->
        <xsl:text>    [XmlRoot( ElementName = "r" )]</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:text>    public partial class </xsl:text>
        <xsl:value-of select=" $FileName "/>
        <xsl:text> : ShapeBase, IShape</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:text>    {</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:apply-templates select=" au:section/au:* " mode="au:property" />
        <xsl:text>    }</xsl:text>
        <xsl:value-of select=" $NewLine " />

        <xsl:text>}</xsl:text>
    </xsl:template>


    <xsl:template match=" * " mode="au:property">
        <xsl:text>        /// &lt;summary /&gt;</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:text>        public </xsl:text>
        <xsl:apply-templates select=" . " mode="au:property-type" />
        <xsl:text> </xsl:text>
        <xsl:value-of select=" @id " />
        <xsl:text> { get; set; }</xsl:text>
        <xsl:value-of select=" $NewLine " />

        <xsl:if test=" position() != last() ">
            <xsl:value-of select=" $NewLine " />
        </xsl:if>
    </xsl:template>

    <xsl:template match=" * " mode="au:property-type">
        <xsl:text>string</xsl:text>
    </xsl:template>

    <xsl:template match=" au:checkbox " mode="au:property-type">
        <xsl:text>bool</xsl:text>
    </xsl:template>

</xsl:stylesheet>

<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:au="urn:gold"
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
    <xsl:template match=" au:shape ">
        <xsl:text>// autogenerate: do NOT manually edit
using Gold;
using System;
using System.Xml;
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
        <xsl:text>Definition : ShapeDefinitionBase&lt;</xsl:text>
        <xsl:value-of select=" $FileName " />
        <xsl:text>&gt;, IShapeDefinition</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:text>    {</xsl:text>
        <xsl:value-of select=" $NewLine " />

        <xsl:text>        /// &lt;summary /&gt;</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:text>        public string Name</xsl:text>
        <xsl:value-of select=" $NewLine "/>
        <xsl:text>        { get { return "</xsl:text>
        <xsl:value-of select=" $FileName "/>
        <xsl:text>"; } }</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:value-of select=" $NewLine " />

        <xsl:text>        /// &lt;summary /&gt;</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:text>        public string FriendlyName</xsl:text>
        <xsl:value-of select=" $NewLine "/>
        <xsl:text>        { get { return "</xsl:text>
        <xsl:value-of select=" au:name "/>
        <xsl:text>"; } }</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:value-of select=" $NewLine " />

        <xsl:if test=" au:definition/au:sequence[ @prefix and @format ] ">
            <xsl:text>        /// &lt;summary /&gt;</xsl:text>
            <xsl:value-of select=" $NewLine " />
            <xsl:text>        public override string ShapeCodePrefix</xsl:text>
            <xsl:value-of select=" $NewLine "/>
            <xsl:text>        { get { return "</xsl:text>
            <xsl:value-of select=" au:definition/au:sequence/@prefix "/>
            <xsl:text>"; } }</xsl:text>
            <xsl:value-of select=" $NewLine " />
            <xsl:value-of select=" $NewLine " />

            <xsl:text>        /// &lt;summary /&gt;</xsl:text>
            <xsl:value-of select=" $NewLine " />
            <xsl:text>        public override string ShapeCodeFormat</xsl:text>
            <xsl:value-of select=" $NewLine "/>
            <xsl:text>        { get { return "</xsl:text>
            <xsl:value-of select=" au:definition/au:sequence/@format "/>
            <xsl:text>"; } }</xsl:text>
            <xsl:value-of select=" $NewLine " />
            <xsl:value-of select=" $NewLine " />
        </xsl:if>

        <xsl:text>        /// &lt;summary /&gt;</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:text>        public XmlDocument FormDefinition</xsl:text>
        <xsl:value-of select=" $NewLine "/>
        <xsl:text>        { get { return FormDefinitionLoad(); } }</xsl:text>
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

        <xsl:text>        /// &lt;summary /&gt;</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:text>        protected override bool ValidatePropertiesAuto( ValidationMode mode )</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:text>        {</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:apply-templates select=" au:section/au:* " mode="au:property-validation" />
        <xsl:text>            return true;</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:text>        }</xsl:text>
        <xsl:value-of select=" $NewLine " />

        <xsl:text>    }</xsl:text>
        <xsl:value-of select=" $NewLine " />

        <xsl:text>}</xsl:text>
    </xsl:template>


    <!--- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    ~
    ~ Shape / Property definition
    ~
    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
    <xsl:template match=" * " mode="au:property">
        <xsl:text>        /// &lt;summary /&gt;</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:text>        public </xsl:text>
        <xsl:apply-templates select=" . " mode="au:property-type" />
        <xsl:text> </xsl:text>
        <xsl:value-of select=" @id " />
        <xsl:text> { get; set; }</xsl:text>
        <xsl:value-of select=" $NewLine " />
        <xsl:value-of select=" $NewLine " />
    </xsl:template>

    <xsl:template match=" * " mode="au:property-type">
        <xsl:text>string</xsl:text>
    </xsl:template>

    <xsl:template match=" au:checkbox " mode="au:property-type">
        <xsl:text>bool?</xsl:text>
    </xsl:template>

    <xsl:template match=" au:integer " mode="au:property-type">
        <xsl:text>int?</xsl:text>
    </xsl:template>


    <!--- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    ~
    ~ Shape / Property validation
    ~
    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
    <xsl:template match=" * " mode="au:property-validation">
        <xsl:choose>
            <xsl:when test=" au:required[ @for ] ">
                <xsl:text>            if ( IsRequired( ValidationMode.</xsl:text>
                <xsl:value-of select=" au:required/@for " />
                <xsl:text>, mode ) == true &amp;&amp; this.</xsl:text>
                <xsl:value-of select=" @id " />
                <xsl:text> == null )</xsl:text>
                <xsl:value-of select=" $NewLine " />
                <xsl:text>                return false;</xsl:text>
                <xsl:value-of select=" $NewLine " />
                <xsl:value-of select=" $NewLine " />
            </xsl:when>

            <xsl:when test=" au:required ">
                <xsl:text>            if ( this.</xsl:text>
                <xsl:value-of select=" @id " />
                <xsl:text> == null )</xsl:text>
                <xsl:value-of select=" $NewLine " />
                <xsl:text>                return false;</xsl:text>
                <xsl:value-of select=" $NewLine " />
                <xsl:value-of select=" $NewLine " />
            </xsl:when>
        </xsl:choose>

        <xsl:if test=" au:length/@max ">
            <xsl:text>            if ( this.</xsl:text>
            <xsl:value-of select=" @id " />
            <xsl:text> != null &amp;&amp; this.</xsl:text>
            <xsl:value-of select=" @id " />
            <xsl:text>.Length > </xsl:text>
            <xsl:value-of select=" au:length/@max " />
            <xsl:text> )</xsl:text>
            <xsl:value-of select=" $NewLine " />
            <xsl:text>                return false;</xsl:text>
            <xsl:value-of select=" $NewLine " />
            <xsl:value-of select=" $NewLine " />
        </xsl:if>
    </xsl:template>

</xsl:stylesheet>

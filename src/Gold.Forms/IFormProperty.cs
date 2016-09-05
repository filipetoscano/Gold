using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Gold.Forms
{
    public interface IFormProperty
    {
        /// <summary>
        /// Initializes the property according to the XML definition.
        /// </summary>
        /// <param name="propertyDefinition">XML definition.</param>
        /// <param name="mode">Edit mode, which will affect whether some fields
        /// are required or not.</param>
        void Initialize( IXPathNavigable propertyDefinition, string mode );

        /// <summary>
        /// Gets the property identifier for the current property.
        /// </summary>
        string Id
        {
            get;
        }

        /// <summary>
        /// Gets the label for the current property.
        /// </summary>
        string Label
        {
            get;
        }

        /// <summary>
        /// Gets or sets the string value associated with the current
        /// property.
        /// </summary>
        string Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the UI control which is rendering the current
        /// property.
        /// </summary>
        Control Control
        {
            get;
        }

        /// <summary>
        /// Gets whether the control has 'conditional' visibility:
        /// if true, then whenever a control fires the RebuildConditionals
        /// event this control should be notified and should be allowed
        /// to re-evaluate whether it should be showing.
        /// </summary>
        bool IsConditional
        {
            get;
        }

        /// <summary>
        /// Gets whether the current property is visible, or not.
        /// </summary>
        bool IsVisible
        {
            get;
        }

        /// <summary>
        /// Sets the focus on the most significant part of the composite control.
        /// </summary>
        /// <returns>True if focus was set, False otherwise.</returns>
        [SuppressMessage( "Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix" )]
        bool FocusEx();

        /// <summary>
        /// Based on the current property bag, evaluates whether the
        /// property should be displayed or not.
        /// </summary>
        /// <param name="properties"></param>
        void EvaluateConditional( Dictionary<string,string> properties );

        /// <summary>
        /// Event that is fired when the control value changes.
        /// </summary>
        event EventHandler RebuildConditionals;
    }
}

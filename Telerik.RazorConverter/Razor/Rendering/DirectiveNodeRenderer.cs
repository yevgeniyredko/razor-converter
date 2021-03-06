﻿using System;
using Telerik.RazorConverter.Razor.Converters;

namespace Telerik.RazorConverter.Razor.Rendering
{
    using Telerik.RazorConverter.Razor.DOM;

    public class DirectiveNodeRenderer : IRazorNodeRenderer
    {
        public string RenderNode(IRazorNode node)
        {
            var directiveNode = (IRazorDirectiveNode) node;
            if (directiveNode.Directive == DirectiveNames.LAYOUT)
                return $@"@{{Layout = ""{directiveNode.Parameters}"";}}";
            return $"@{directiveNode.Directive} {directiveNode.Parameters}".Trim();
        }

        public bool CanRenderNode(IRazorNode node)
        {
            var directiveNode = node as IRazorDirectiveNode;
            return directiveNode != null &&
                !(directiveNode.Directive == DirectiveNames.LAYOUT && 
                !TemplateSettings.CurrentSettings.ShowDefaultLayout &&
                string.Equals(directiveNode.Parameters, TemplateSettings.CurrentSettings.DefaultLayoutPath, StringComparison.OrdinalIgnoreCase));
        }
    }
}

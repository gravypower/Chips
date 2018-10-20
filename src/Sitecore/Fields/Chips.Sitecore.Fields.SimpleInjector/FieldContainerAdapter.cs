using System;
using Chips.DependencyInjection.SimpleInjector;
using Sitecore.Diagnostics;
using Sitecore.Web.UI.HtmlControls;

namespace Chips.Sitecore.Fields
{
    public class FieldContainerAdapter : AbstractFieldContainerAdapter
    {
        protected override Control ResolveControl(Type controlType)
        {
            return (Control)SimpleInjectorBootstrapper.Container.GetInstance(controlType);
        }
    }
}
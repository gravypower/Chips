using System;
using Chips.DependencyInjection.SimpleInjector;
using Sitecore.Web.UI.HtmlControls;

namespace Chips.Sitecore.Fields
{
    public class FieldContainerAdapter : AbstractFieldContainerAdapter
    {
        protected override Control ResolveScheduleTask(Type controlType)
        {
            return (Control)SimpleInjectorBootstrapper.Container.GetInstance(controlType);
        }
    }
}
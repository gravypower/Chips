using System;
using System.Web.UI;
using Control = Sitecore.Web.UI.HtmlControls.Control;

namespace Chips.Sitecore.Fields
{
    public abstract class AbstractFieldContainerAdapter : Control
    {
        protected abstract Control ResolveScheduleTask(Type controlType);
        protected override void Render(HtmlTextWriter output)
        {
            var t = true;
        }
    }
}

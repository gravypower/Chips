using System;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Web.UI.HtmlControls;

namespace Chips.Sitecore.Fields.UnboundDroplist
{
    public class UnboundDroplist : ValueLookupEx
    {
        protected override void OnLoad(EventArgs e)
        {
            if(Controls.Count == 0)
            {
                try
                {
                    var options = Source.Split('|');

                    foreach (var option in options)
                    {
                        Controls.Add(new ListItem
                        {
                            ID = "UnboundDroplist_" + Guid.NewGuid().ToString("N") + "_" + option,
                            Header = option,
                            Value = option
                        });
                    }
                }
                catch (Exception ex)
                {
                    //Sitecore.Diagnostics.Log.Error("Error loading EnumList control", ex, this);

                    Controls.Add(new ListItem
                    {
                        ID = "UnboundDroplist_" + Guid.NewGuid().ToString("N") + "_NotFound",
                        Header = "Could not load enumeration"
                    });
                }
            }

            base.OnLoad(e);
        }
    }
}

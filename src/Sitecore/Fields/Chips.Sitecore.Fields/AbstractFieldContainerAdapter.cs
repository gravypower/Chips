using System;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using Sitecore.Configuration;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Shell.Applications.ContentManager;
using Control = Sitecore.Web.UI.HtmlControls.Control;
using FieldInfo = Sitecore.Shell.Applications.ContentManager.FieldInfo;

namespace Chips.Sitecore.Fields
{
    public abstract class AbstractFieldContainerAdapter : Control, IContentField
    {
        private Control _control;

        protected Control Control
        {
            get
            {
                if(_control == null)
                    ResolveControlFromField();

                return _control;
            }
        }

        protected abstract Control ResolveControl(Type controlType);

        private void ResolveControlFromField()
        {
            var contentEditorPage = Page as ContentEditorPage;

            if (!(contentEditorPage?.CodeBeside is ContentEditorForm codeBeside)) return;
            
            var itemUri = typeof(ContentEditorForm)
                .GetProperty("CurrentItem", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                ?.GetValue(codeBeside) as ItemUri;

            if (!(codeBeside.FieldInfo[ID] is FieldInfo fieldInfo) || itemUri == null) return;

            var database = Factory.GetDatabase(itemUri.DatabaseName);
            var item = database.GetItem(itemUri.ItemID);

            var feild = item.Fields[fieldInfo.FieldID];
            var fieldType = database.GetItem(feild.ID);
            var fieldTypeValue = fieldType["Type"];

            const string indexName = "sitecore_core_index";

            using (var index = ContentSearchManager.GetIndex(indexName).CreateSearchContext())
            {
                var query = index.GetQueryable<SearchResultItem>()
                    .Where(i => i.TemplateId == new ID())
                    .Where(i => i.Name == fieldTypeValue);


                var searchResults = query.GetResults();

                var typeValue = searchResults.Hits.SingleOrDefault()?.Document.Fields["Type"];

                var controlType = Type.GetType(typeValue.ToString());
                _control = ResolveControl(controlType);
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            var render = _control.GetType().GetMethod("Render",
                BindingFlags.NonPublic | BindingFlags.Instance);
            render.Invoke(_control, new object[] { output });
        }

        public string GetValue()
        {
            var getValue = _control.GetType().GetMethod("GetValue",
                BindingFlags.NonPublic | BindingFlags.Instance);
            return getValue.Invoke(_control, new object[] {}) as string;
        }

        public void SetValue(string value)
        {
            var setValue = _control.GetType().GetMethod("SetValue",
                BindingFlags.NonPublic | BindingFlags.Instance);
            setValue.Invoke(_control, new object[] { value });
        }
    }
}

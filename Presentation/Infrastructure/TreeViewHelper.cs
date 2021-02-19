using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;

namespace Presentation.Infrastructure
{
    public static class TreeViewHtmlHelper
    {
        public static IHtmlContent TreeView(this IHtmlHelper html, List<TreeViewModel> models)
        {
            return html.Create(models, null, new RouteValueDictionary(null));
        }
        public static IHtmlContent TreeView(this IHtmlHelper html, List<TreeViewModel> models, object treeViewAttributes)
        {
            return html.Create(models, null, new RouteValueDictionary(treeViewAttributes));
        }
        public static IHtmlContent TreeView(this IHtmlHelper html, List<TreeViewModel> models, object itemSelected, object treeViewAttributes)
        {
            return html.Create(models, itemSelected, new RouteValueDictionary(treeViewAttributes));
        }
        #region Private
        private static IHtmlContent Create(this IHtmlHelper html, List<TreeViewModel> models, object itemSelected, IDictionary<string, object> htmlAttributes)
        {
            var select = new TagBuilder("select");
            if (htmlAttributes != null)
                foreach (var item in htmlAttributes)
                    select.MergeAttribute(item.Key, item.Value.ToString());

            select.MergeAttribute("dir", "rtl");
            select.SetOption(models, null, "", itemSelected?.ToString());
            return select;
        }
        private static void SetOption(this TagBuilder select, List<TreeViewModel> models, string parentId, string space, string itemSelected)
        {
            var m = models.Where(p => p.ParentId?.ToString() == parentId);
            foreach (var item in m)
            {
                var option = new TagBuilder("option");
                option.MergeAttribute("Value", item.Id.ToString());
                option.MergeAttribute("ParentId", item.ParentId?.ToString());
                if (item.Id.ToString() == itemSelected)
                {
                    option.MergeAttribute("Selected", "Selected");
                }
                option.InnerHtml.AppendHtml(space + item.Text);

                select.InnerHtml.AppendHtml(option.ToHtmlString());

                if (models.Any(p => p.ParentId == item.Id))
                {
                    select.SetOption(models, item.Id.ToString(), space + "&nbsp;&nbsp;&nbsp;&nbsp;", itemSelected);
                }
            }
        }
        private static string ToHtmlString(this TagBuilder tagBuilder)
        {
            var writer = new System.IO.StringWriter();
            tagBuilder.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
        #endregion
    }

    #region Dto
    public class TreeViewModel
    {
        public object Id { get; set; }
        public string Text { get; set; }
        public object ParentId { get; set; }
    }
    #endregion
}

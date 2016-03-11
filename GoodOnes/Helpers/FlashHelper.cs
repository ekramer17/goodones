using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace GoodOnes.Helpers
{
    public static class FlashHelper
    {
        private enum Severity
        {
            Success,
            Info,
            Warning,
            Danger
        }

        private class Message
        {
            public Severity Severity;
            public string Text;
            public string Css;

            public Message(Severity severity, String message, String klass)
            {
                Severity = severity;
                Text = message;
                Css = klass;
            }
        }

        internal const string FLASH_TEMP_KEY = "FLASH_MESSAGES";

        /// <summary>
        /// Gets flash messages partial view
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="dismissible">If set to <c>true</c>, renders flash messages as dismissible.</param>
        /// <returns>Flash messages partial view</returns>
        public static MvcHtmlString RenderFlash(this HtmlHelper html, bool dismissible = true)
        {
            var b = new StringBuilder();
            var t = html.ViewContext.TempData;
            var f = t[FLASH_TEMP_KEY] as List<Message> ?? new List<Message>();

            if (f.Count == 0) return null;
            
            foreach (Message m in f)
            {
                var tag = new TagBuilder("div");
                tag.MergeAttribute("role", "alert");
                tag.MergeAttribute("style", "display:none");
                tag.AddCssClass("alert");
                tag.AddCssClass("alert-block");
                tag.AddCssClass(String.Format("alert-{0}", m.Severity.ToString().ToLower()));
                tag.AddCssClass("flash-msg");
                tag.AddCssClass(m.Css);

                if (dismissible)
                {
                    var button = new TagBuilder("button");
                    button.MergeAttribute("type", "button");
                    button.MergeAttribute("aria-label", "Close");
                    button.AddCssClass("close");
                    button.InnerHtml = "<span aria-hidden=\"true\">&times;</span></button>";
                    tag.InnerHtml = button.ToString();
                }

                tag.InnerHtml += String.Format("<span>{0}</span>", m.Text);

                b.AppendLine(tag.ToString());
            }

            return MvcHtmlString.Create(b.ToString());
        }

        #region C O N T R O L L E R   E X T E N S I O N S
        /// <summary>
        /// Flashes the success message
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        public static void FlashSuccess(this Controller controller, string message, string css = "")
        {
            SetFlash(controller, Severity.Success, message, css);
        }

        /// <summary>
        /// Flashes the warning message
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        public static void FlashWarning(this Controller controller, string message, string css = "")
        {
            SetFlash(controller, Severity.Warning, message, css);
        }

        /// <summary>
        /// Flashes the danger message
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        public static void FlashDanger(this Controller controller, string message, string css = "")
        {
            SetFlash(controller, Severity.Danger, message, css);
        }

        /// <summary>
        /// Flashes the information message
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        public static void FlashInfo(this Controller controller, string message, string css = "")
        {
            SetFlash(controller, Severity.Info, message, css);
        }
        
        /// <summary>
        /// Flashes the specified message
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        private static void SetFlash(Controller controller, Severity severity, string message, string css)
        {
            object o;

            if (controller.TempData.TryGetValue(FLASH_TEMP_KEY, out o))
            {
                ((List<Message>)o).Add(new Message(severity, message, css));
                controller.TempData.Keep(FLASH_TEMP_KEY);
            }
            else
            {
                controller.TempData[FLASH_TEMP_KEY] = new List<Message> { new Message(severity, message, css) };
            }
        }
        #endregion
    }
}
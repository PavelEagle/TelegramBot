using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using HtmlAgilityPack;

namespace TelegramBot.Common
{
  public static class HtmlParserHelper
  {
    public static string RemoveUnwantedTagsFromHtmlCollection(IHtmlCollection<IElement> htmlCollection)
    {
      var result = new StringBuilder();
      foreach (var html in htmlCollection)
      {
        if (html.InnerHtml.Length > 100)
        {
          result.Append(RemoveUnwantedTagsFromInnerText(html.InnerHtml));
          result.Append("\n");
        }
      }

      return RemoveClosingBrackets(result.ToString());
    }

    private static string RemoveUnwantedTagsFromInnerText(string data)
    {
      if (string.IsNullOrEmpty(data)) return string.Empty;

      var document = new HtmlDocument();
      document.LoadHtml(data);

      var acceptableTags = new[] {"strong", "em", "u"};

      var nodes = new Queue<HtmlNode>(document.DocumentNode.SelectNodes("./*|./text()"));
      while (nodes.Count > 0)
      {
        var node = nodes.Dequeue();
        var parentNode = node.ParentNode;

        if (!acceptableTags.Contains(node.Name) && node.Name != "#text")
        {
          var childNodes = node.SelectNodes("./*|./text()");

          if (childNodes != null)
          {
            foreach (var child in childNodes)
            {
              nodes.Enqueue(child);
              parentNode.InsertBefore(child, node);
            }
          }
          parentNode.RemoveChild(node);
        }
      }

      return document.DocumentNode.InnerHtml;
    }

    private static string RemoveClosingBrackets(string str)
    {
      return Regex.Replace(str, @"[\[\d-\]]", string.Empty);
    }
  }
}

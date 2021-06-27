using System;
using System.Text;
using Chushka.Shared.Models;

namespace Chushka.Web.Helpers
{
    public static class ProductPageBuilder
    {
        public static string GetRowStart()
        {
            return "<div class='row d-flex justify-content-around'>";
        }

        public static string BuildProductObject(ProductDtoModel model)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append($"<a href='/Products/Details/{model.Id}' class='col-md-3' style='text-decoration: none;'>");
            sb.Append("<div class='product p-1 chushka-bg-color rounded-top rounded-bottom'>");
            sb.Append($"<h5 class='text-center mt-3'>{model.Name}</h5>");
            sb.Append("<hr class='hr-1 bg-white'/>");
            sb.Append("<p class='text-white text-center'>");
            sb.Append($"{StringHelpers.Truncate(model.Description,50)}");
            sb.Append("</p>");
            sb.Append("<hr class='hr-1 bg-white' />");
            sb.Append($"<h6 class='text-center text-white mb-3'>${model.Price}</h6>");
            sb.Append("</div>");
            sb.Append("</a>");

            return sb.ToString();
        }

        public static string GetRowEnd()
        {
            return "</div><br/>";
        }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace GimenaCreations.Pages.Admin;

public static class ManageNavPages
{
    public static string Catalog => "Catalog";
    public static string CatalogTypes => "CatalogTypes";
    public static string Sales => "Sales";
    public static string Purchases => "Purchases";
    public static string Roles => "Roles";
    public static string Suppliers => "Suppliers";
    public static string Users => "Users";
    public static string WebsiteTraffic => "WebsiteTraffic";
    public static string Analytics => "Analytics";            
    public static string CatalogNavClass(ViewContext viewContext) => PageNavClass(viewContext, Catalog);
    public static string CatalogTypesNavClass(ViewContext viewContext) => PageNavClass(viewContext, CatalogTypes);
    public static string SalesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Sales);
    public static string PurchasesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Purchases);
    public static string RolesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Roles);
    public static string SuppliersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Suppliers);
    public static string UsersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Users);
    public static string WebsiteTrafficNavClass(ViewContext viewContext) => PageNavClass(viewContext, WebsiteTraffic);
    public static string AnalyticsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Analytics);            

    public static string PageNavClass(ViewContext viewContext, string page) => string
            .Equals(viewContext.ViewData["ActivePage"] as string ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName), page, StringComparison.OrdinalIgnoreCase)
            ? "active"
            : null;
}

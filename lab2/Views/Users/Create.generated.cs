﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace lab2.Views.Users
{
    using System;
    using System.Collections.Generic;
    
    #line 1 "..\..\Views\Users\Create.cshtml"
    using System.Globalization;
    
    #line default
    #line hidden
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using lab2;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Users/Create.cshtml")]
    public partial class Create : System.Web.Mvc.WebViewPage<lab2.ViewModels.UsersViewModel>
    {
        public Create()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Views\Users\Create.cshtml"
  
	ViewBag.Title = "Create";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>Create</h2>\r\n");

            
            #line 9 "..\..\Views\Users\Create.cshtml"
 using (Html.BeginForm("Register", "Users"))
{ 

            
            #line default
            #line hidden
WriteLiteral("\t<div");

WriteLiteral(" class=\"form-horizontal\"");

WriteLiteral(">\r\n\t\t<h4>User</h4>\r\n\t\t<hr />\r\n");

WriteLiteral("\t\t");

            
            #line 14 "..\..\Views\Users\Create.cshtml"
   Write(Html.ValidationSummary(true));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\r\n\t\t<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("\t\t\t");

            
            #line 17 "..\..\Views\Users\Create.cshtml"
       Write(Html.LabelFor(model => model.UserName, new { @class = "control-label col-md-2" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t<div");

WriteLiteral(" class=\"col-md-10\"");

WriteLiteral(">\r\n");

WriteLiteral("\t\t\t\t");

            
            #line 19 "..\..\Views\Users\Create.cshtml"
           Write(Html.EditorFor(model => model.UserName));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("\t\t\t\t");

            
            #line 20 "..\..\Views\Users\Create.cshtml"
           Write(Html.ValidationMessageFor(model => model.UserName));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t\t\r\n\t\t<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("\t\t\t");

            
            #line 25 "..\..\Views\Users\Create.cshtml"
       Write(Html.LabelFor(model => model.Email, new { @class = "control-label col-md-2" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t<div");

WriteLiteral(" class=\"col-md-10\"");

WriteLiteral(">\r\n");

WriteLiteral("\t\t\t\t");

            
            #line 27 "..\..\Views\Users\Create.cshtml"
           Write(Html.EditorFor(model => model.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("\t\t\t\t");

            
            #line 28 "..\..\Views\Users\Create.cshtml"
           Write(Html.ValidationMessageFor(model => model.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t</div>\r\n\t\t</div>\r\n\r\n\t\t<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("\t\t\t");

            
            #line 33 "..\..\Views\Users\Create.cshtml"
       Write(Html.LabelFor(model => model.Password, new { @class = "control-label col-md-2" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t<div");

WriteLiteral(" class=\"col-md-10\"");

WriteLiteral(">\r\n");

WriteLiteral("\t\t\t\t");

            
            #line 35 "..\..\Views\Users\Create.cshtml"
           Write(Html.EditorFor(model => model.Password));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("\t\t\t\t");

            
            #line 36 "..\..\Views\Users\Create.cshtml"
           Write(Html.ValidationMessageFor(model => model.Password));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t</div>\r\n\t\t</div>\r\n\r\n\t\t<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n\t\t\t<div");

WriteLiteral(" class=\"col-md-offset-2 col-md-10\"");

WriteLiteral(">\r\n\t\t\t\t<input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" value=\"Create\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" />\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t</div>\r\n");

            
            #line 46 "..\..\Views\Users\Create.cshtml"
}

            
            #line default
            #line hidden
DefineSection("Scripts", () => {

WriteLiteral("\r\n");

WriteLiteral("\t");

            
            #line 48 "..\..\Views\Users\Create.cshtml"
Write(Scripts.Render("~/bundles/jqueryval"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

        }
    }
}
#pragma warning restore 1591

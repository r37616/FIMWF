<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomCodeControl.ascx.cs" Inherits="Insight.FIM.Controls.CustomCodeControl,Insight.FIM.Controls,Version=1.0.0.0,Culture=neutral,PublicKeyToken=daa4bcd323c6d191" %>
<asp:Table ID="Table1" runat="server" Width="100%">
     <asp:TableRow>
        <asp:TableCell CssClass="ms-siteaction" Width="99%">
            <asp:Label ID="lblActivityName" runat="server" Text="The Activity Name" Font-Bold="True"></asp:Label>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-siteaction" HorizontalAlign="Right">
            <asp:TextBox ID="txtActivityName" runat="server" CssClass="ms-lookuptypeintextbox" Rows="15" Width="400px"></asp:TextBox>
        </asp:TableCell>
    </asp:TableRow>
     <asp:TableRow>
        <asp:TableCell CssClass="ms-siteaction" Width="99%">
            <asp:Label ID="lblActor" runat="server" Text="Workflow Activity Actor" Font-Bold="True"></asp:Label>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-siteaction" HorizontalAlign="Right">
            <asp:DropDownList ID="ddlActor" runat="server" Width="400px" CssClass="ms-lookuptypeintextbox">
                <asp:ListItem Text="Inherit From Request" Value="00000000-0000-0000-0000-000000000000"
                    Selected="True"></asp:ListItem>
                <asp:ListItem Text="FIM Sync Account" Value="fb89aefa-5ea1-47f1-8890-abe7797d6497"></asp:ListItem>
                <asp:ListItem Text="FIM Service Account" Value="e05d1f1b-3d5e-4014-baa6-94dee7d68c89"></asp:ListItem>
                <asp:ListItem Text="Anonymous" Value="b0b36673-d43b-4cfa-a7a2-aff14fd90522"></asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-siteaction" Width="99%">
            <asp:Label ID="lblLanguage" runat="server" Text="Language" Font-Bold="True"></asp:Label>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-siteaction" HorizontalAlign="Right">
            <asp:DropDownList ID="ddlLanguage" runat="server" Width="400px" CssClass="ms-lookuptypeintextbox">
                <asp:ListItem Text="C#" Value="CSharp" Selected="True"></asp:ListItem>
                <asp:ListItem Text="VB" Value="VB"></asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-siteaction" Width="99%">
            <asp:Label ID="lblIncludes" runat="server" Text="Namespaces" Font-Bold="True"></asp:Label><br />
            Namespaces to make available to the code as using/Imports statement.  The System, System.Reflection, and System.IO
            namespaces are automatically included.  Seperate multiple entries using a semicolon.  Be sure to specify any
            assemblies that contain the namespace specified.<br />
            Example:  
            <ul>
                <li>System.Net</li>
                <li>System.Web; System.DirectoryServices</li>
            </ul>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-siteaction" HorizontalAlign="Right">
            <asp:TextBox ID="txtIncludes" Width="400px" runat="server" CssClass="ms-lookuptypeintextbox"></asp:TextBox>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-siteaction" Width="99%">
            <asp:Label ID="lblAssemblies" runat="server" Text="Assemblies" Font-Bold="True"></asp:Label><br />
            Assemblies to reference in the code.  Assemblies must be referenced using the file name/path to the file on the server.
            Assemblies in known locations can be added by simply using the dll name. 
            Seperate multiple entries using a semicolon. <br />
            Examples:  
            <ul>
                <li>System.DirectoryServices.dll;C:\Assemblies<br />\myAssembly.dll</li>
                <li>C:\Windows\assembly\GAC_MSIL\<br />myGacUtil\1.0.0.0__abcdefhij1234567\myGACUtil.dll</li>
            </ul>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-siteaction" HorizontalAlign="Right">
            <asp:TextBox ID="txtAssemblies" Width="400px" runat="server" CssClass="ms-lookuptypeintextbox"></asp:TextBox>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-siteaction" Width="99%" VerticalAlign="Top">
            <asp:Label ID="lblParams" runat="server" Text="Input Parameters" Font-Bold="True"></asp:Label><br />
            Parameters to pass to the code. Commas are currently not allowed in parameter value.<br />
            Examples:
            <ul>
                <li>[//Target/DisplayName]</li>
                <li>[//Requestor/Manager]</li>
                <li>[//WorkflowData/CustAttrib1]</li>
                <li>text value</li>
                <li>123</li>
                <li>01/01/2001</li>
            </ul>        
        </asp:TableCell>
        <asp:TableCell CssClass="ms-siteaction" HorizontalAlign="Right" VerticalAlign="Top">
            <asp:Table runat="server" ID="tblParams" Width="100%"></asp:Table> 
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-siteaction" Width="99%">
            <asp:TextBox ID="txtPropertyCount" runat="server" style="display:none">0</asp:TextBox>           
            <asp:TextBox ID="txtParamList" runat="server" style="display:none"></asp:TextBox>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-siteaction" HorizontalAlign="Right">
            <asp:LinkButton ID="btnAddParam" runat="server" Font-Underline="true" OnClick="btnAddParam_Click" Text="Add" Width="50px"></asp:LinkButton> &nbsp; <asp:LinkButton ID="btnRemoveParam" runat="server" Font-Underline="true" OnClick="btnRemoveParam_Click" Text="Remove" Width="50px"></asp:LinkButton> &nbsp; 
        </asp:TableCell>
    </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell CssClass="ms-siteaction" Width="99%">
            <asp:Label ID="lblReturnType" runat="server" Text="Return Type" Font-Bold="True"></asp:Label><br />
            Examples:
            <ul>
                <li>String</li>
                <li>Integer</li>
            </ul>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-siteaction" HorizontalAlign="Right">
            <asp:DropDownList ID="ddlReturnType" Width="400px" runat="server" CssClass="ms-lookuptypeintextbox">
                <asp:ListItem Text="String" Value="String" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Integer" Value="Integer"></asp:ListItem>
                <asp:ListItem Text="DateTime" Value="DateTime"></asp:ListItem>
                <asp:ListItem Text="Boolean" Value="Boolean"></asp:ListItem>
                <asp:ListItem Text="Reference" Value="Reference"></asp:ListItem>
            </asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-siteaction" Width="99%">
            <asp:Label ID="lblCode" runat="server" Text="Custom Code" Font-Bold="True"></asp:Label><br />
            Supports basic code execution for the language selected.  Variables being passed in can be accessed using a zero based index
            on the Parameters Object Array.  All parameters are passed as objects, so type casting may be necessary to use the object's 
            properties. The code should return a single value that can be passed to the Destination Attribute below. Return value will
            be cast as the return type provided above.<br />
            Example: <br />            
                 &nbsp; &nbsp; &nbsp; string firstName = (string) Parameters[0];<br />
                 &nbsp; &nbsp; &nbsp; string lastName = (string) Parameters[1];<br />
                 &nbsp; &nbsp; &nbsp; string initials = firstName.Substring(0, 1) <br />
                 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + lastName.Substring(0,1);<br />
                 &nbsp; &nbsp; &nbsp; return initials;
        </asp:TableCell>
        <asp:TableCell CssClass="ms-siteaction" HorizontalAlign="Right">
            <asp:TextBox ID="tbCode" runat="server" CssClass="ms-lookuptypeintextbox" Rows="15" TextMode="MultiLine" Width="400px"></asp:TextBox>
        </asp:TableCell>
    </asp:TableRow>
     <asp:TableRow>
        <asp:TableCell CssClass="ms-siteaction" Width="99%">
            <asp:Label ID="lblTarget" runat="server" Text="Destination Attribute" Font-Bold="True"></asp:Label><br />
            Examples:
            <ul>
                <li>[//Target/DisplayName]</li>
                <li>[//Requestor/Manager]</li>
                <li>[//WorkflowData/CustAttrib1]</li>
            </ul>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-siteaction" HorizontalAlign="Right">
            <asp:TextBox ID="tbTarget" runat="server" CssClass="ms-lookuptypeintextbox" Width="300px"></asp:TextBox>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>



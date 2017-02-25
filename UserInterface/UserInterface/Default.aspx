<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserInterface.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KUugle Search Engine</title>
    </head>
<body>
    <form id="form1" runat="server">
        <center>
            
    <div style="margin-left:auto;margin-right:auto; margin-top:3vh; margin-bottom:auto;">
        <asp:Label ID="TitleLabel" runat="server" Text="KUugle Search Engine" Font-Size="X-Large" />
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Width="444px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Go !" />
    
        <br />
        <br />
        <asp:ListView ID="ListView1" runat="server" DataKeyNames="ID" DataSourceID="SqlDataSource1">
            <AlternatingItemTemplate>
                <span>
                <asp:HyperLink ID="URLLabel" runat="server" NavigateUrl='<%# Eval("URL") %>' Text='<%# string.IsNullOrWhiteSpace((string)Eval("Title").ToString()) ? Eval("URL") : Eval("Title") %>' Font-Size="Larger" />
                <br />
                <asp:Label ID="ValueLabel1" runat="server" Text="Page Rank:" Font-Size="Smaller" />
                <asp:Label ID="ValueLabel" runat="server" Text='<%# Eval("Value") %>' Font-Size="Smaller" />
                <br />
                <asp:Label ID="Label1" runat="server" Text="Page ID:" Font-Size="XX-Small" />
                <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' Font-Size="XX-Small" />
                <br />
                <br /></span>
            </AlternatingItemTemplate>
            <EmptyDataTemplate>
                <span>No results</span>
            </EmptyDataTemplate>
            <ItemTemplate>
                <span>
                <asp:HyperLink ID="URLLabel" runat="server" NavigateUrl='<%# Eval("URL") %>' Text='<%# string.IsNullOrWhiteSpace((string)Eval("Title").ToString()) ? Eval("URL") : Eval("Title") %>' Font-Size="Larger" />
                <br />
                <asp:Label ID="ValueLabel1" runat="server" Text="Page Rank:" Font-Size="Smaller" />
                <asp:Label ID="ValueLabel" runat="server" Text='<%# Eval("Value") %>' Font-Size="Smaller" />
                <br />
                <asp:Label ID="Label1" runat="server" Text="Page ID:" Font-Size="XX-Small" />
                <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' Font-Size="XX-Small" />
                <br />
                <br /></span>
            </ItemTemplate>
            <LayoutTemplate>
                <div id="itemPlaceholderContainer" runat="server" style="font-family: Verdana, Arial, Helvetica, sans-serif;">
                    <span runat="server" id="itemPlaceholder" />
                </div>
                <div style="text-align: center;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                    <asp:DataPager ID="DataPager1" runat="server">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="False" ShowNextPageButton="False" ShowPreviousPageButton="True" />
                            <asp:NumericPagerField />
                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="False" ShowNextPageButton="True" ShowPreviousPageButton="False" />
                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </LayoutTemplate>
        </asp:ListView>
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DB %>" SelectCommand="SearchForKeyword" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="TextBox1" Name="Keyword" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </center>
    </form>
</body>
</html>

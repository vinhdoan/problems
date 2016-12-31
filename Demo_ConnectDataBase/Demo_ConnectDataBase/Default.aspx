<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Demo_ConnectDataBase._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        Tổng số học sinh của lớp:
        <asp:Label ID="lblTongSo" runat="server"></asp:Label>
        
        <br />
        MSHS:
        <asp:TextBox ID="txtMSHS" runat="server"></asp:TextBox>
        <br />
        Học tên:
        <asp:TextBox ID="txtHoTen" runat="server"></asp:TextBox>
        <br />
        Điểm:
        <asp:TextBox ID="txtDiem" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnThem" runat="server" onclick="btnThem_Click" Text="Thêm" />
        
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Play.aspx.cs" Inherits="LKReportingSystem.Forms.Play" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        debugger;
        function CalcSellPrice2(CurrentPrice, DiscountCtrlID, CustomerCtrlID) {
            var Discount = parseFloat(document.getElementById(DiscountCtrlID).value);
            var SellPrice = document.getElementById(CustomerCtrlID);
            var SellPriceValue = parseFloat(CurrentPrice - ((CurrentPrice * Discount) / 100));
            //var SellPriceValueRound = Math.round(SellPriceValue,4);
            var SellPriceValueRound = SellPriceValue;
           SellPrice.value = SellPriceValueRound;

        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="List Price" Visible="True">
                        <ItemTemplate>
                            <asp:Label ID="lblCurrentListPrice" runat="server"
                                Text='<%# Eval("Price") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDiscountOnItem" Width="100px"
                                runat="Server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sell Price">
                        <ItemTemplate>
                            <asp:TextBox ID="lblSellPrice" runat="Server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>

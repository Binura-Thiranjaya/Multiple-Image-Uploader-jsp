<%@ Page Title="Informarion" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="info.aspx.cs" Inherits="imageUploader.info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
       <div class="d-grid gap-2 col-6 mx-auto">
           <asp:Button Text="Back" runat="server" ID="goBack" type="button" class="btn btn-danger" OnClick="GoBack" />
       </div>
        <div class="text-center mt-2 d-flex justify-content-center " >           
                <asp:GridView ID="grid1" runat="server" AutoGenerateColumns="False"  CellPadding="4" ForeColor="#333333" GridLines="None" >  
                 <AlternatingRowStyle  />  
                 <columns>                      
                     <asp:TemplateField HeaderText="Record Id" ControlStyle-Width="200px">  
                         <ItemTemplate>
                              <asp:Label ID="ImageRecordId" runat="server"  Text='<%#Bind("record_id") %>'></asp:Label> 
                         </ItemTemplate>  
                         </asp:TemplateField>  
                         <asp:TemplateField HeaderText="Name" ControlStyle-Width="200px">  
                             <ItemTemplate>  
                                 <asp:Label ID="ImageName" runat="server"  Text='<%#Bind("Name") %>'></asp:Label>  
                             </ItemTemplate>  
                         </asp:TemplateField>  
                         <asp:TemplateField HeaderText="Image">  
                            <ItemTemplate>  
                               <asp:Image runat="server" ID="img" ImageUrl='<%# "data:image/png;base64," + Eval("Image")%>' class="rounded mx-auto d-block " Height="200px" Width="200px" />
                            </ItemTemplate>  
                         </asp:TemplateField>                       
                   </columns>  
                 </asp:GridView>
            </div>
       </div>
</asp:Content>

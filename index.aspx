<%@ Page Title="Image Uploader" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="imageUploader.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div>
            <div class="input-group mb-3">
              <input type="text" class="form-control" placeholder="Name" id="Name" name="Name" aria-label="Name" aria-describedby="basic-addon2" minlength="4" required>
            </div>
            <div class="input-group">
                    <asp:FileUpload  type="file" class="form-control" id="FileUpload1" aria-describedby="" aria-label="Upload"  runat="server" AllowMultiple="true"  onchange="loadFile(event)" />
                    <asp:Button Text="Submit" runat="server" ID="submit" type="button" class="btn btn-outline-secondary" OnClick="uploadImage" />
            </div>
             <asp:Label ID="Label1" style="color:red" runat="server" Font-Size="Small"></asp:Label> 
             <asp:Label ID="Label2" style="color:red" runat="server" Font-Size="Small"></asp:Label> 
        </div>
        <div class="container text-center mt-2 d-flex justify-content-center " >
            <asp:GridView ID="grid1" runat="server" AutoGenerateColumns="False"  CellPadding="4" ForeColor="#333333" GridLines="None" >  
             <AlternatingRowStyle BackColor="White" />  
             <columns>                      
                 <asp:TemplateField HeaderText="Record Id" ControlStyle-Width="250px">  
                     <ItemTemplate>
                          <asp:Label ID="ImageRecordId" runat="server"  Text='<%#Bind("record_id") %>'></asp:Label> 
                     </ItemTemplate>  
                     </asp:TemplateField>  
                     <asp:TemplateField HeaderText="Name" ControlStyle-Width="250px">  
                         <ItemTemplate>  
                             <asp:Label ID="ImageName" runat="server"  Text='<%#Bind("Name") %>'></asp:Label>  
                         </ItemTemplate>  
                     </asp:TemplateField>  
                     <asp:TemplateField HeaderText="Image">  
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkView" runat="server"  OnClick="lnkView_Click" CommandArgument='<%#Eval("record_id") %>' CssClass="btn btn-outline-info">View</asp:LinkButton>
                        </ItemTemplate>  
                     </asp:TemplateField>
               </columns>  
             </asp:GridView> 
        </div>
    </div>
</asp:Content>

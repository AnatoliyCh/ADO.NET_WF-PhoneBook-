<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPhoneBook.aspx.cs" Inherits="PhoneBook.EditPhoneBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Редактирование записей!</title>
    <link rel="stylesheet" href="css/Styles.css" />
</head>
<body>
    <div>
        <p>Телефонный справочник!</p>
    </div>
     <form id="PhoneBook" runat="server">
         <div>
            <p>Редактирование и удаление записей</p>
        </div>
        <asp:ValidationSummary ID="validationSummary" runat="server" ShowModelStateErrors="true" />
         <div>
            <label>Выберете ID:</label>
             <asp:DropDownList id="listId" AutoPostBack="True" OnSelectedIndexChanged="SelectId" runat="server">
                 <asp:ListItem Selected="True" Value="not"> -- </asp:ListItem> 
             </asp:DropDownList>
        </div>
        <div>
            <label>Фамилия:</label><input type="text" id="LastName" runat="server"/></div>
        <div> 
            <label>Имя:</label><input type="text" id="FirstName" runat="server"/></div>
        <div> 
            <label>Отчество:</label><input type="text" id="MiddleName" runat="server"/></div>
        <div> 
            <label>Email:</label><input type="text" id="Email" runat="server"/></div>
        <div>
            <label>Телефон:</label><input type="text" id="Phone" runat="server"/></div>
        <div>
            <asp:Button ID="Button1" runat="server" onclick="DeleteButton" Text="Удалить запись"/>
            <asp:Button ID="edit" runat="server" onclick="EditButton" Text="Изменить запись"/>
        </div>
    </form>
        <table>
            <thead>
                <tr>
                    <th>id</th>
                    <th>Фамилия</th>
                    <th>Имя</th>
                    <th>Отчество</th>
                    <th>Телефон</th>
                    <th>Email</th>
                </tr>
            </thead>
            <tbody>
                <%= ShowHtmlListCellBook()%>
            </tbody>
        </table>
</body>
</html>

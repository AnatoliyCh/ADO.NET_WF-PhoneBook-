<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PhoneBook.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Добавление записи!</title>
    <link rel="stylesheet" href="css/Styles.css" />
</head>
<body>
    <form id="PhoneBook" runat="server">
        <div>
            <p>Телефонный справочник!</p>
        </div>
        <asp:ValidationSummary ID="validationSummary" runat="server" ShowModelStateErrors="true" />
        <div> 
            <label>Фамилия:</label><input type="text" id="LastName" runat="server"/></div>
        <div> 
            <label>Имя:</label><input type="text" id="FirstName" runat="server"/></div>
        <div> 
            <label>Отчество:</label><input type="text" id="MiddleName" runat="server"/></div>
        <div> 
            <label>Ваш email:</label><input type="text" id="Email" runat="server"/></div>
        <div>
            <label>Ваш телефон:</label><input type="text" id="Phone" runat="server"/></div>
        <div>
            <button type="submit">Создать запись!</button>
            <asp:Button ID="edit" runat="server" onclick="EditButton" Text="Редактирование =>"/>
        </div>
    </form>
    <table>
        <thead>
            <tr>
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

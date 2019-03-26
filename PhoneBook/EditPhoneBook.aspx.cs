using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

using PhoneBook.Domain.Core;
using PhoneBook.Infrastructure.Data;

namespace PhoneBook
{
    public partial class EditPhoneBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var data = RecordLineRepository.GetRepository().GetAllId();
                foreach (var item in data)
                    listId.Items.Add(item.ToString());
            }
        }
        protected string ShowHtmlListCellBook()
        {
            StringBuilder html = new StringBuilder();
            var data = RecordLineRepository.GetRepository().ReadingAll();
            foreach (var item in data)
            {
                html.Append(String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td>",
                        item.Id, item.LastName, item.FirstName, item.MiddleName, item.Phone, item.Email));
            }
            return html.ToString();
        }
        protected void DeleteButton(Object sender, EventArgs e)
        {
            if (listId.Text != "not")
            {
                RecordLineRepository.GetRepository().Deleting(Convert.ToInt32(listId.Text));
                listId.Items.Remove(listId.Items.FindByValue(listId.Text));
                LastName.Value = "";
                FirstName.Value = "";
                MiddleName.Value = "";
                Phone.Value = "";
                Email.Value = "";
            }
        }
        protected void EditButton(Object sender, EventArgs e)
        {
            if (listId.Text != "not")
            {
                var data = RecordLineRepository.GetRepository().ReadingById(Convert.ToInt32(listId.Text));
                data.LastName = LastName.Value;
                data.FirstName = FirstName.Value;
                data.MiddleName = MiddleName.Value;
                data.Phone = Phone.Value;
                data.Email = Email.Value;
                RecordLineRepository.GetRepository().Updating(data);
                LastName.Value = "";
                FirstName.Value = "";
                MiddleName.Value = "";
                Phone.Value = "";
                Email.Value = "";
                listId.SelectedIndex = 0;
            }
        }
        protected void SelectId(Object sender, EventArgs e)
        {
            if (listId.Text != "not")
            {
                var data = RecordLineRepository.GetRepository().ReadingById(Convert.ToInt32(listId.Text));
                LastName.Value = data.LastName;
                FirstName.Value = data.FirstName;
                MiddleName.Value = data.MiddleName;
                Phone.Value = data.Phone;
                Email.Value = data.Email;
            }
        }
    }
}
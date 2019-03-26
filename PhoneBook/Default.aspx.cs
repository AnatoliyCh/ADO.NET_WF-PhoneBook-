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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                RecordLine tmpRecordLine = new RecordLine();
                if (TryUpdateModel(tmpRecordLine, new FormValueProvider(ModelBindingExecutionContext)))
                {
                    RecordLineRepository.GetRepository().Creating(tmpRecordLine);
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
            }
        }
        protected string ShowHtmlListCellBook()
        {
            StringBuilder html = new StringBuilder();
            var data = RecordLineRepository.GetRepository().ReadingAll();
            foreach (var item in data)
            {
                html.Append(String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>",
                        item.LastName, item.FirstName, item.MiddleName, item.Phone, item.Email));
            }
            return html.ToString();
        }
        protected void EditButton(Object sender, EventArgs e)
        {
            Response.Redirect("EditPhoneBook.aspx");
        }
    }
}
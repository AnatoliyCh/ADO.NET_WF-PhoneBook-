using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PhoneBook.Domain.Core;


namespace PhoneBook.Domain.Interfaces
{
    public interface IRecordLineRepository
    {
        //CRUD
        void Creating(RecordLine _cellBook);
        RecordLine ReadingById(int _id);
        IEnumerable<RecordLine> ReadingAll();
        void Updating(RecordLine _cellBook);
        void Deleting(int _id);
        //get all id RecordLines
        IEnumerable<int> GetAllId();
    }
}

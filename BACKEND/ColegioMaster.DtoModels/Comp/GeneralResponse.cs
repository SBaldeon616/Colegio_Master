using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMaster.DtoModels.Comp
{
    public class GeneralResponse
    {
        public bool Success { get; set; }
        public string Title { get; set; }
        public string Messahe { get; set; }
    }

    public class GeneralResponse<T>
    {
        public bool Success { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public T? Content { get; set; }
        public bool ShowAlert { get; set; }
    }
}

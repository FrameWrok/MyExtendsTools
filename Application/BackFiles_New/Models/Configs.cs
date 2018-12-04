using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BackFiles_New.Models
{
    [DataContractAttribute]
    public class Configs
    {
        [DataMemberAttribute]
        public List<string> BasePaths { get; set; }
        [DataMemberAttribute]
        public List<string> RootPath { get; set; }
        [DataMemberAttribute]
        public bool OperationedOpenRootPath { get; set; }
        [DataMemberAttribute]
        public string TfsUserName { get; set; }
        [DataMemberAttribute]
        public string TfsPwd { get; set; }
    }
}

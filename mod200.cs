using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadArticles200
{
    public class mod200
    {

        public long Id { get; set; }      
        public string ArtNr { get; set; }

        public string DLNr { get; set; }

        public string SA { get; set; }

        public string BezNr { get; set; }

        public string ArtNrLimipa { get; set; }


    }
}


//ArtNr 000 22 U(*) Article Number in the data supplier format
//  DLNr 022 4 N Data Supplier Number Constant (-> 040)
//  SA 026 3 N Data Table Constant « 200 »
//  BezNr 029 9 (N) Description text number in the language key table
//  KZSB 038 1 (N) Self - service packing = 1, otherwise 0
//  KZMat 039 1 (N) Mandatory material certification = 1, otherwise 0
//  KZAT 040 1 (N) Exchange Part = 1, otherwise 0
//  KZZub 041 1 (N) Accessory = 1, otherwise 0
//  LosGr1 042 5 (N) Batch size 1 (multiple of VPE)
//  LosGr2 047 5 (N) Batch size 2 (multiple of LosGr1)
//  Lösch-Flag 052 1 N const = 0
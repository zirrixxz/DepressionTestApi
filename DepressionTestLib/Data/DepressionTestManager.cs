using DepressionTestLib.DBContext;
using DepressionTestLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DepressionTestLib.Data
{
    public class DepressionTestManager
    {
        DepressionTestDBContext db;
        public DepressionTestManager(DepressionTestDBContext db)
        {
            this.db = db;
        }

        public string GetAdvice(int level)
             
        {
            if (level < 7)
            {
                return "ไม่มีอาการของโรคซึมเศร้าหรือมีอาการของโรคซึมเศร้าระดับน้อยมาก";
            }
            else if (level == 7 && level <= 12)
            {
                return "มีอาการของโรคซึมเศร้าระดับน้อย ";
            }
            else if (level == 13 && level <= 18)
            {
                return "มีอาการของโรคซึมเศร้าระดับปานกลาง ";
            }
            else if (level >= 19)
            {
                return "มีอาหารของโรคซึมเศร้าระดับรุนแรง และควรปรึกษาแพทย์โดยด่วน";
            }
            else
            {
                return "";
            }
            
        }
        
    }
}

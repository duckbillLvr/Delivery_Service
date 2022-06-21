using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeriveryService
{
    // 유저 정보를 담은 유저 클래스
    public class User
    {
        public int userId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string level { get; set; } = "일반고객";
        public int total { get; set; } = 0;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeriveryService
{
    public  class Package
    {
        public int packageId { get; set; } // 패키지 ID
        public string type { get; set; } // 물품 종류
        public int fee { get; set; } // 물품 배송료
        public string start { get; set; } // 물품 배송시작점
        public string end { get; set; } // 물품 배송완료점
        public string address { get; set; } // 현재 물품 위치
        public DateTime TransTime { get; set; } // 발송일
        public DateTime ReceiveTime { get; set; } // 수신일
        public int date { get; set; } // 배송 소요 시간
        public int dday { get; set; } // 남은 배송일
        public bool status { get; set; } = false; // 배송 상태: True: 배송완료 False: 배송중
        public int transUserId { get; set; } // 발송 유저의 ID
        public int receiveUserId { get; set; } // 수신 유저의 ID
        public string mod { get; set; } // mod;
        public List<City> root { get; set; }// 물품 운송 루트
    }
}

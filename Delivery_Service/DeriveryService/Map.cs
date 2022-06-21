using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeriveryService
{
    public class Map
    {
        public int row { get; set; }
        public int col { get; set; }
        private List<City> Citys;
        public List<List<City>> map;

        public Map(List<City> Citys)
        {
            this.Citys = Citys;
            SetMapping();
        }
        // 도시 정보를 이용하여 지도 제작
        private void SetMapping()
        {
            SortCityList();

            map = new List<List<City>>();

            for (int i=0; i<this.row+1; i++)
            {
                List<City> temp = new List<City>();
                int idx = Citys.FindIndex(c => c.x == i);
                if (idx == -1) continue;
                for (int j=idx; j<Citys.Count; j++)
                {
                    if (Citys[j].x == i)
                    {
                        temp.Add(Citys[j]);
                    }
                    else break;
                }
                map.Add(temp);
            }
        }
        // 도시 정보를 x, y축으로 정렬
        private void SortCityList()
        {
            Citys = Citys.OrderBy(c => c.x).ThenBy(c => c.y).ToList();

            row = Citys[0].x;
            col = Citys[0].y;
            foreach(City c in Citys)
            {
                if (row < c.x)
                    row = c.x;
                if (col < c.y)
                    col = c.y;
            }
        }

        public List<List<City>> GetMap()
        {
            return map;
        }
    }
}
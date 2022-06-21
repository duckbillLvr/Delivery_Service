using System.Collections.Generic;
using System.Linq;

namespace DeriveryService
{
    public class Calc
    {
        private readonly int[] dx = new int[] { -1, 1, 0, 0 };
        private readonly int[] dy = new int[] { 0, 0, -1, 1 };
        private List<List<City>> cities;
        private bool[,] visited;
        private List<City>[,] root;
        public Calc(List<List<City>> cities, int row, int col)
        {
            this.cities = cities;
            this.visited = new bool[row+1, col+1];
            this.root = new List<City>[row + 1, col + 1];
        }

        // 택배를 최단거리로 배송하기 위한 알고리즘
        // BFS 알고리즘을 이용
        public List<City> CalcFeeForCost(City send_city, City receive_city)
        {
            initFactor();
            int sx = send_city.x;
            int sy = send_city.y;

            Queue<Point> q = new Queue<Point>();
            q.Enqueue(new Point(sx, sy, send_city));
            visited[sx, sy] = true;
            root[sx, sy].Add(cities[sx][sy]);

            // 상하좌우 탐색 가능
            while (q.Count > 0)
            {
                Point cur = q.Dequeue();
                
                if (cities[cur.x][cur.y].Name == receive_city.Name)
                    return root[cur.x, cur.y];

                for (int i = 0; i < 4; i++)
                {
                    int nx = cur.x + dx[i];
                    int ny = cur.y + dy[i];

                    if (nx < 0 || ny < 0)
                        continue;
                    if (nx >= cities.Count || ny >= cities[nx].Count)
                        continue;
                    if (visited[nx, ny] == true)
                        continue;


                    q.Enqueue(new Point(nx, ny, cities[nx][ny]));
                    visited[nx, ny] = true;
                    root[nx, ny] = root[cur.x, cur.y].ToList();
                    root[nx, ny].Add(cities[nx][ny]);
                }
            }
            return null;
        }
        // 거리를 구하기 위하여 초기화
        private void initFactor()
        {
            for (int i = 0; i < visited.GetLength(0); i++)
            {
                for (int j = 0; j < visited.GetLength(1); j++)
                {
                    visited[i, j] = false;
                }
            }
            for (int i=0; i<root.GetLength(0); i++)
            {
                for (int j=0; j<root.GetLength(1); j++)
                {
                    root[i, j] = new List<City>();
                }
            }
        }
        // Point 클래스를 이용하여 도시 좌표를 구함
        private class Point
        {
            public Point(int x, int y, City city)
            {
                this.x = x;
                this.y = y;
                this.city = city;
            }
            public int x { get; set; }
            public int y { get; set; }
            public City city { get; set; }
        }
    }
}

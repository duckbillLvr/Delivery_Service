using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DeriveryService
{
    public class DataManager
    {
        public static List<User> Users = new List<User>();// User 정보
        public static List<Package> Packages = new List<Package>(); // Package(물품) 정보
        public static List<City> Citys = new List<City>(); // 도시 정보
        static DataManager()
        {
            LoadCitys();
            LoadUser();
            LoadPackages();
        }
        // 도시 정보를 XML에서 로드
        public static void LoadCitys()
        {
            try
            {
                string cityInfo = File.ReadAllText(@"./Citys.xml");
                XElement cityElement = XElement.Parse(cityInfo);
                Citys = (from item in cityElement.Descendants("city")
                         select new City()
                         {
                             CityId = int.Parse(item.Element("id").Value),
                             Name = item.Element("name").Value,
                             Cost = int.Parse(item.Element("cost").Value),
                             x = int.Parse(item.Element("x").Value),
                             y = int.Parse(item.Element("y").Value),
                         }).ToList<City>();
            }
            catch (FileLoadException e)
            {
                Console.WriteLine("File not exists: " + e.Message);
            }
        }
        // 유저 정보를 XML에서 로드
        public static void LoadUser()
        {
            try
            {
                string usersInfo = File.ReadAllText(@"./Users.xml");
                XElement usersElement = XElement.Parse(usersInfo);
                Users = (from item in usersElement.Descendants("user")
                         select new User()
                         {
                             userId = int.Parse(item.Element("id").Value),
                             name = item.Element("name").Value,
                             phone = item.Element("phone").Value,
                             address = item.Element("address").Value,
                             level = item.Element("level").Value,
                             total = int.Parse(item.Element("total").Value),
                         }).ToList<User>();

            }
            catch (FileLoadException e)
            {
                Console.WriteLine("File not exists: " + e.Message);
                SaveUser();
            }
        }
        // 유저 정보를 XML에서 로드
        public static void SaveUser()
        {
            // 유저 XML 생성
            string usersOutput = "";
            usersOutput += "<users>\n";
            foreach (var item in Users)
            {
                usersOutput += "<user>\n";
                usersOutput += string.Format($"      <id>{item.userId}</id>\n");
                usersOutput += string.Format($"      <name>{item.name}</name>\n");
                usersOutput += string.Format($"      <phone>{item.phone}</phone>\n");
                usersOutput += string.Format($"      <address>{item.address}</address>\n");
                usersOutput += string.Format($"      <level>{item.level}</level>\n");
                usersOutput += string.Format($"      <total>{item.total}</total>\n");
                usersOutput += "</user>\n";
            }
            usersOutput += "</users>";

            File.WriteAllText(@"./Users.xml", usersOutput);
        }
        // 물품 정보를 XML에서 로드
        public static void LoadPackages()
        {
            try
            {
                string packagesInfo = File.ReadAllText(@"./Packages.xml");
                XElement packagesElement = XElement.Parse(packagesInfo);
                Packages = (from item in packagesElement.Descendants("package")
                            select new Package()
                            {
                                packageId = int.Parse(item.Element("id").Value),
                                type = item.Element("type").Value,
                                fee = int.Parse(item.Element("fee").Value),
                                start = item.Element("start").Value,
                                end = item.Element("end").Value,
                                address = item.Element("address").Value,
                                TransTime = DateTime.Parse(item.Element("trans").Value),
                                ReceiveTime = DateTime.Parse(item.Element("receive").Value),
                                dday = int.Parse(item.Element("dday").Value),
                                status = bool.Parse(item.Element("status").Value),
                                transUserId = int.Parse(item.Element("tid").Value),
                                receiveUserId = int.Parse(item.Element("rid").Value),
                                mod = item.Element("mod").Value
                            }).ToList<Package>();
            }
            catch (FileLoadException e)
            {
                Console.WriteLine("File not exists: " + e.Message);
                SavePackages();
            }
        }
        // 물품 정보를 XML에 저장
        public static void SavePackages()
        {
            // 패키지 XML 생성
            string packagesOutput = "";
            packagesOutput += "<packages>\n";
            foreach (var item in Packages)
            {
                packagesOutput += "<package>\n";
                packagesOutput += String.Format($"      <id>{item.packageId}</id>\n");
                packagesOutput += String.Format($"      <type>{item.type}</type>\n");
                packagesOutput += String.Format($"      <fee>{item.fee}</fee>\n");
                packagesOutput += String.Format($"      <start>{item.start}</start>\n");
                packagesOutput += String.Format($"      <end>{item.end}</end>\n");
                packagesOutput += String.Format($"      <address>{item.address}</address>\n");
                packagesOutput += String.Format($"      <trans>{item.TransTime}</trans>\n");
                packagesOutput += String.Format($"      <receive>{item.ReceiveTime}</receive>\n");
                packagesOutput += String.Format($"      <dday>{item.dday}</dday>\n");
                packagesOutput += String.Format($"      <status>{item.status}</status>\n");
                packagesOutput += String.Format($"      <tid>{item.transUserId}</tid>\n");
                packagesOutput += String.Format($"      <rid>{item.receiveUserId}</rid>\n");
                packagesOutput += String.Format($"      <mod>{item.mod}</mod>\n");
                packagesOutput += "</package>\n";
            }
            packagesOutput += "</packages>\n";

            File.WriteAllText(@"./Packages.xml", packagesOutput);
        }
        // 새로 할당 가능한 유저 ID 요청
        public static int GetNewUserId()
        {
            int lastId = Users[Users.Count - 1].userId;
            return lastId + 1;
        }
        // 새로 할당 가능한 물품 ID 요청
        public static int GetNewPackageId()
        {
            int lastId = Packages[Packages.Count - 1].packageId;
            return lastId + 1;
        }
        // 도시 이름에 대하여 도시의 정보 요청
        public static City FindCity(string name)
        {
            City city = null;
            foreach (City c in Citys)
            {
                if (c.Name == name)
                {
                    city = c;
                    break;
                }
            }
            return city;
        }
        // 유저 이름과 전화번호에 대하여 유저 정보 요청
        public static User FindUser(string name, string phone)
        {
            User user = null;
            foreach (User u in Users)
            {
                if (u.name == name && u.phone == phone)
                {
                    user = u;
                    break;
                }
            }
            return user;
        }
        // 하루가 지남 메소드 물품 정보를 새로고침 해준다.
        public static void One_Day_Pass(DateTime now)
        {
            foreach (Package p in Packages)
            {
                if (p.status == false)
                {
                    p.dday -= 1;
                    if (p.root == null)
                    {
                        string start = p.end;
                        p.address = start;
                    }
                    else
                    {
                        // 일반, 기업 택배
                        if (p.root.Count > 3)
                        {
                            p.root.RemoveAt(1);
                            p.root.RemoveAt(1);
                            p.root.RemoveAt(1);
                        }
                        else
                            p.address = p.end;
                        // 급속택배
                        if (p.mod.Equals("급속택배"))
                        {
                            if (p.root.Count > 2)
                            {
                                p.root.RemoveAt(1);
                                p.root.RemoveAt(1);
                            }
                            else
                                p.address = p.end;
                        }
                        City c = p.root[1];
                        p.address = c.Name;
                    }
                }
                if (p.dday < 1 && p.status == false)
                {
                    p.status = true;
                    p.address = p.end;
                    p.ReceiveTime = now;
                }
            }

            SavePackages();
        }
    }
}

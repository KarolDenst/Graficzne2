using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graficzne2
{
    internal class Obj
    {
        public List<Face> Faces;
        public List<MyPoint> Points;

        public Obj(List<Face> faces, List<MyPoint> points)
        {
            Faces = faces;
            Points = points;
        }

        public Obj(string path, double scale, double offset)
        {
            Faces = new List<Face>();
            Points = new List<MyPoint>();

            using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while((line = streamReader.ReadLine()) is not null)
                    {
                        string[] strings = line.Split(' ');

                        switch (strings[0])
                        {
                            case "v":
                                double x = (Convert.ToDouble(strings[1]) + offset) * scale;
                                double y = (Convert.ToDouble(strings[2]) + offset) * scale;
                                double z = (Convert.ToDouble(strings[3]) + offset) * scale;
                                Points.Add(new MyPoint((int)z, (int)x, (int)y));
                                break;
                            case "f":
                                string[] vertexes = strings[1].Split('/');
                                int p1 = Convert.ToInt32(vertexes[0]) - 1;
                                vertexes = strings[2].Split('/');
                                int p2 = Convert.ToInt32(vertexes[0]) - 1;
                                vertexes = strings[3].Split('/');
                                int p3 = Convert.ToInt32(vertexes[0]) - 1;
                                Faces.Add(new Face(p1, p2, p3));
                                break;
                            default:
                                break;
                        }
                    }
                } 
            }
        }
    }
}

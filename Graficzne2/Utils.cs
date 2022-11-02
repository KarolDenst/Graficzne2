using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graficzne2.Objects;

namespace Graficzne2
{
    internal static class Utils
    {
        public static Face[] SetUpFaceArrayFromFile(string path, double scale, double offset)
        {
            var faces = new List<Face>();
            var points = new List<Point3d>();

            using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) is not null)
                    {
                        string[] strings = line.Split(' ');

                        switch (strings[0])
                        {
                            case "v":
                                double y = (Convert.ToDouble(strings[1]) + offset) * scale;
                                double z = (Convert.ToDouble(strings[2])) * scale;
                                double x = (Convert.ToDouble(strings[3]) + offset) * scale;
                                points.Add(new Point3d((int)x, (int)y, (int)z));
                                break;
                            case "f":
                                string[] vertexes = strings[1].Split('/');
                                int p1 = Convert.ToInt32(vertexes[0]) - 1;
                                vertexes = strings[2].Split('/');
                                int p2 = Convert.ToInt32(vertexes[0]) - 1;
                                vertexes = strings[3].Split('/');
                                int p3 = Convert.ToInt32(vertexes[0]) - 1;
                                faces.Add(new Face(points[p1], points[p2], points[p3]));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return faces.ToArray();
        }

    }
}

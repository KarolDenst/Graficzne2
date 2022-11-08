﻿using System;
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
            var normals = new List<Vector3d>();

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
                            case "vn":
                                double yn = Convert.ToDouble(strings[1]);
                                double zn = Convert.ToDouble(strings[2]);
                                double xn = Convert.ToDouble(strings[3]);
                                normals.Add(new Vector3d(xn, yn, zn));
                                break;
                            case "f":
                                string[] vertexes = strings[1].Split('/');
                                int p1 = Convert.ToInt32(vertexes[0]) - 1;
                                int v1 = Convert.ToInt32(vertexes[2]) - 1;
                                vertexes = strings[2].Split('/');
                                int p2 = Convert.ToInt32(vertexes[0]) - 1;
                                int v2 = Convert.ToInt32(vertexes[2]) - 1;
                                vertexes = strings[3].Split('/');
                                int p3 = Convert.ToInt32(vertexes[0]) - 1;
                                int v3 = Convert.ToInt32(vertexes[2]) - 1;
                                faces.Add(new Face((points[p1], normals[v1]), (points[p2], normals[v2]), (points[p3], normals[v3])));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return faces.ToArray();
        }

        public static Vector3d GetVector(Face face, Point p)
        {
            double area = face.Area;
            Point p1 = face.P1.TwoD();
            Point p2 = face.P2.TwoD();
            Point p3 = face.P3.TwoD();

            double p1Area = Geometry.Get2dArea(p, p2, p3);
            double p2Area = Geometry.Get2dArea(p, p1, p3);
            double p3Area = Geometry.Get2dArea(p, p2, p1);

            double p1Ratio = p1Area / area;
            double p2Ratio = p2Area / area;
            double p3Ratio = p3Area / area;

            double X = face.V1.X * p1Ratio + face.V2.X * p2Ratio + face.V3.X * p3Ratio;
            double Y = face.V1.Y * p1Ratio + face.V2.Y * p2Ratio + face.V3.Y * p3Ratio;
            double Z = face.V1.Z * p1Ratio + face.V2.Z * p2Ratio + face.V3.Z * p3Ratio;

            return new Vector3d(X, Y, Z);
        }

    }
}

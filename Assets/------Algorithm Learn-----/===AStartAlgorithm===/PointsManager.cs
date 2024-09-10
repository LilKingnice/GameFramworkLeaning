using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstartAlgorithm
{
    public class PointsManager : MonoBehaviour
    {
        public int[,] PointList { get; set; }

        public List<Point> CloseList { get; set; }
        public List<Point> OpenList { get; set; }

        
        public int MapWidth { get; set; }
        public int MapHigh { get; set; }


        /// <summary>
        /// Initializes the map grid
        /// </summary>
        /// <param name="w">width</param>
        /// <param name="h">high</param>
        public void InitNodeMap(int w,int h)
        {
            //TODO 根据宽高创建格子
            OpenList.Sort(SortOpenList);
        }


        void Test()
        {
            OpenList.Sort(SortOpenList);
        }

        public int SortOpenList(Point a,Point b)
        {
            if (a.F>b.F)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Find map grid
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        public List<Point> SearchNode(Vector2Int startPoint,Vector2 endPoint)
        {
            return null;
        }
    } 
}


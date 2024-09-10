using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstartAlgorithm
{
    public class Point
    {
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        
        public int X{get;set;}

        public int Y{get;set;}
        
        public Point Parent{get;set;}
        
        public PointType PointType{get;set;}

        public Point()
        {
            
        }
    }


    public enum PointType
    {
        Normal,
        Block
    }
}

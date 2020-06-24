using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> grid;
    public int x;
    public int y;
    public int gCost;
    public int hCost;
    public int fCost;
    public bool isWalkable;
    public string ret;
    public PathNode cameFromNode;
    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.ret = x + "," + y;
        /*this.isWalkable = true;

        if (this.x > 3 && this.x < 8 && this.y > 4 && this.y < 9)
            this.isWalkable = false;
            */

    }
    public PathNode(Grid<PathNode> grid, int x, int y, int z)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;

        this.isWalkable = true;

        if(z==0)
        {
            if((this.x>1 && this.x < 9&& this.y >1 && this.y <21)|| this.x > 1 && this.x < 9 && this.y > 25 && this.y < 42)
                this.isWalkable = false;
            if(this.x == 4 && this.y == 41)
            {
                this.isWalkable = true;
            }
            if ((this.x == 2 && this.y == 34) || (this.x == 2 && this.y == 20) || (this.x == 2 && this.y == 16))                this.isWalkable = true;
        }

        if(z==1)
        {
            if ((this.x > 8 && this.x < 15 && this.y > 1 && this.y < 17)|| (this.x > 8 && this.x < 15 && this.y > 23 && this.y < 39) || (this.x < 7 && this.y < 19) || (this.x > 16 && this.y < 39) || (this.x < 7 && this.y > 24 && this.y < 35) || (this.x > 5 && this.x < 19 && this.y > 40))
                this.isWalkable = false;
            if ((this.x == 6 && this.y == 1)|| (this.x == 6 && this.y == 10) || (this.x == 9 && this.y == 12) || (this.x == 14 && this.y == 5) || (this.x == 14 && this.y == 30) || (this.x == 14 && this.y == 7) || (this.x == 14 && this.y == 12))
                this.isWalkable = true;
        }

        if(z==2)
        {
            if ((this.x < 12 && this.y > 2 && this.y < 28)|| (this.x < 2&&this.y>2) || (this.x < 12 && this.y > 31 && this.y < 53))
                this.isWalkable = false;
        }


    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    public override string ToString()
    {
            return "  ";   
        //return ret;
    }
    
}
